using PruebaTecnica.Data;
using PruebaTecnica.DTOs;
using PruebaTecnica.Models;

namespace PruebaTecnica
{
    public class SalesService
    {
        private readonly AppDbContext _db;
        public SalesService(AppDbContext db) => _db = db;

        public async Task<(bool ok, string? error, Sale? sale)> CreateSaleAsync(SaleCreateDto dto)
        {
            // Validaciones básicas
            if (dto.items == null || !dto.items.Any())
                return (false, "La venta debe contener al menos un item.", null);

            // Validar cliente
            cliente client = null!;
            if (dto.clienteId.HasValue)
            {
                client = await _db.Clientes.FindAsync(dto.clienteId.Value);
                if (client == null)
                    return (false, "Cliente no encontrado.", null);
                // Validar email formato
                if (!IsValidEmail(client.email))
                    return (false, "Email del cliente inválido.", null);
            }
            else
            {
                if (dto.clienteId == null)
                    return (false, "Debe indicar el cliente o los datos del mismo.", null);
                if (!IsValidEmail(dto.Client.email))
                    return (false, "Email del cliente inválido.", null);
                client = new cliente { nombre = dto.Client.nombre, email = dto.Client.email };
                _db.Clientes.Add(client);
                await _db.SaveChangesAsync();
            }

            // Empieza transacción
            using var tx = await _db.Database.BeginTransactionAsync();
            try
            {
                var sale = new Sale { fecha = dto.fecha, clienteId = client.clienteId };
                _db.sales.Add(sale);
                await _db.SaveChangesAsync(); // para tener VentaID

                // Validar stock de cada producto
                foreach (var item in dto.items)
                {
                    var product = await _db.productos.FindAsync(item.productoId);
                    if (product == null)
                        throw new Exception($"Producto {item.productoId} no existe.");
                    if (product.stock < item.cantidad)
                        throw new Exception($"Stock insuficiente para producto '{product.nombre}'. Stock actual: {product.stock}.");

                    // Crear detalle
                    var detail = new saleDetail
                    {
                        ventaId = sale.ventaId,
                        productoId = product.productoId,
                        cantidad = item.cantidad,
                        precioUnitario = item.precioUnitario
                    };
                    _db.detalleVenta.Add(detail);

                    // Reducir stock
                    product.stock -= item.cantidad;
                    _db.productos.Update(product);
                }

                await _db.SaveChangesAsync();
                await tx.CommitAsync();

                // Cargar detalles para respuesta
                await _db.Entry(sale).Collection(s => s.Detalles).LoadAsync();
                await _db.Entry(sale).Reference(s => s.cliente).LoadAsync();

                return (true, null, sale);
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                return (false, ex.Message, null);
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch { return false; }
        }
    }
}
