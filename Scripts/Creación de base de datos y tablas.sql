--Crear base de datos para la prueba t�cnica
CREATE DATABASE PruebaTecnica;
USE PruebaTecnica;

--Creaci�n de tabla productos
CREATE TABLE productos
(
  productoId INT PRIMARY KEY IDENTITY(1,1),
  nombre VARCHAR(100) NOT NULL,
  precio DECIMAL(10,2) NOT NULL,
  stock INT NOT NULL
)

--Creaci�n de tabla clientes
CREATE TABLE clientes
(
  clienteId INT PRIMARY KEY IDENTITY(1,1),
  nombre VARCHAR(100) NOT NULL,
  email VARCHAR(100) NOT NULL UNIQUE
)

--Creaci�n de tabla ventas
CREATE TABLE ventas
(
  ventaId INT PRIMARY KEY IDENTITY(1,1),
  fecha DATE NOT NULL,
  clienteId INT NOT NULL,
  FOREIGN KEY (clienteId) REFERENCES clientes(clienteId)
)

--Creaci�n de tabla DetalleVenta
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