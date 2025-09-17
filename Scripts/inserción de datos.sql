--Insertar a tabla producto
INSERT INTO dbo.productos (nombre, precio, stock)
VALUES ('Shampoo', 34.00, 200),
       ('Jabón', 25.80, 400),
       ('Detergente', 250, 1450),
       ('Cloro', 15, 859),
       ('Perfume', 400.00, 600)

-- Insertar a la tabla clientes
INSERT INTO dbo.clientes (nombre, email)
VALUES ('Maria Estrada', 'mafer11345@gmail.com'),
       ('Carlos Lanuza', 'zjradiodemons@gmail.com'), --correos falsos
       ('Josue López', 'josue.lopez@outlook.com'),
       ('Rebeca Paiz', 'rebeca.cocacola@gmail.com'),
       ('Andrea Estrada', 'michelle.estrada@gmail.com')

--Insertar a tabla ventas
INSERT INTO dbo.ventas (fecha, clienteId)
VALUES ('2025-01-01', 1),
       ('2025-02-01', 2),
       ('2025-02-01', 4),
       ('2025-09-16', 5),
       ('2025-09-02', 1)

--Insertar a tabla detalleVenta
INSERT INTO dbo.detalleVenta (ventaId, productoId, cantidad, precioUnitario)
VALUES (1,1,14,34.00),
       (4,2,25,25.80),
       (3,3,1,250.00),
       (4,1,2,34.00),
       (2,5,1,400.00)

