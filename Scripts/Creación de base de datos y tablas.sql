--Crear base de datos para la prueba técnica
CREATE DATABASE PruebaTecnica;
USE PruebaTecnica;

--Creación de tabla productos
CREATE TABLE productos
(
  productoId INT PRIMARY KEY IDENTITY(1,1),
  nombre VARCHAR(100) NOT NULL,
  precio DECIMAL(10,2) NOT NULL,
  stock INT NOT NULL
)

--Creación de tabla clientes
CREATE TABLE clientes
(
  clienteId INT PRIMARY KEY IDENTITY(1,1),
  nombre VARCHAR(100) NOT NULL,
  email VARCHAR(100) NOT NULL UNIQUE
)

--Creación de tabla ventas
CREATE TABLE ventas
(
  ventaId INT PRIMARY KEY IDENTITY(1,1),
  fecha DATE NOT NULL,
  clienteId INT NOT NULL,
  FOREIGN KEY (clienteId) REFERENCES clientes(clienteId)
)

--Creación de tabla DetalleVenta
CREATE TABLE detalleVenta
(
  detalleID INT PRIMARY KEY IDENTITY(1,1),
  ventaId INT NOT NULL,
  productoId INT NOT NULL,
  cantidad INT NOT NULL,
  precioUnitario DECIMAL(10,2) NOT NULL,
  FOREIGN KEY (ventaId) REFERENCES ventas(ventaId),
  FOREIGN KEY (productoId) REFERENCES productos(productoId)
)