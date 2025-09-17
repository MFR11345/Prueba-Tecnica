--Consulta total vendido por cliente
SELECT c.nombre AS Cliente,
       SUM(dv.cantidad * dv.precioUnitario) AS 'Total Vendido'
 FROM dbo.clientes c
      INNER JOIN dbo.ventas v ON c.clienteId = v.clienteId
      INNER JOIN dbo.detalleVenta dv ON v.ventaId =  dv.ventaId
GROUP BY c.nombre
ORDER BY 'Total Vendido' DESC

--Productos stock actual
SELECT p.nombre AS Producto,
       p.stock - ISNULL(SUM(dv.cantidad),0) AS StockActual
  FROM dbo.productos p
       INNER JOIN dbo.detalleVenta dv ON p.productoId = dv.productoId
GROUP BY p.productoID,
         p.nombre,
         p.stock

--Producto más vendido
SELECT p.nombre AS Cliente,
       SUM(dv.cantidad) AS 'Cantidad Vendida'
 FROM dbo.productos p
      INNER JOIN dbo.detalleVenta dv ON p.productoId =  dv.productoId
GROUP BY p.nombre
ORDER BY 'Cantidad Vendida' DESC