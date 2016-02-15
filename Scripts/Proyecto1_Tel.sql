use PROYECT_1


Create table Rol (
	Rol int identity (1,1) not null,
	Nombre varchar(50) not null,
	Constraint  Pk_Rol Primary Key (Rol)
)

Create table Usuario (
	Usuario int identity(1,1) not null,
	Nombre varchar(50) not null,
	Rol int not null,
	Contrasenia varchar(50) not null,
	Constraint pk_usuario Primary Key (Usuario),
	Constraint fk_Usuario_Rol Foreign Key (Rol) references Rol(Rol) 
)


create table Cliente (
	Cliente int identity(1,1) not null,
	Nombre varchar(50) not null,
	Nit varchar(15) not null,
	Constraint pk_Cliente Primary key (Cliente)
)

Create table Venta (
	Venta int identity (1,1) not null,
	Cliente int not null,
	Usuario int not null,
	Constraint pk_venta Primary Key (Venta),
	Constraint fk_venta_cliente Foreign Key (Cliente) references Cliente(Cliente), 
	Constraint fk_venta_usuario Foreign Key (Usuario) references Usuario(Usuario) 
	
)

Create table Producto(
	Producto int identity(1,1) not null,
	Ancho numeric not null,
	Marca varchar(50)  null,
	Porcentaje numeric null,
	Caracteristica varchar (100) null,
	Precio_m2 numeric  null,
	Tamanio varchar(50)  null,
	constraint pk_Producto Primary Key (Producto)
)

Create table DetalleVenta(
	Venta int not null,
	Producto int not null,
	Cantidad numeric not null,
	Constraint fk_venta_detalle foreign key (Venta) references Venta(Venta),
	Constraint fk_producto_detalle foreign key (Producto) references Producto(Producto)
)

