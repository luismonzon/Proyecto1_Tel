use PROYECT_1
  	
	--se elimino la columna de largo en la tabla producto
	--se agrego como llave foranea la venta en la tabla deuda
	--DBCC CHECKIDENT (<Nombre de tabla>, RESEED,0); --SIRVE PARA REINICIAR EL CONTADOR DE LAS TABLAS
	
--	SELECT  p.Abreviatura, p.Descripcion, Convert(Decimal(15,0), sum(d.Cantidad), 0)  Cantidad, Convert(Decimal(15,2), SUM(d.Cantidad*i.Precio), 2) Total 
-- FROM Producto p, Venta v, DetalleVenta d, Inventario i 
--where d.Venta = v.Venta 
--and p.Producto = d.Producto 
--and i.Producto = p.Producto 
--and v.Cliente = 500 
--group by p.Abreviatura, p.Descripcion;
ALTER TABLE Cliente
ALTER COLUMN Direccion VARCHAR(200) NULL;


ALTER TABLE DetalleVenta
ALTER COLUMN SubTotal numeric(18,2) NULL;




SELECT CONVERT(VARCHAR(8),G.hora,108) Hora, G.Descripcion, G.valor , U.nombre
 FROM  Gasto G, Usuario U
WHERE G.usuario = U.Usuario 
AND G.fecha_gasto = '20160920' 
order by G.hora DESC;


SELECT SUM(G.Valor) as Total 
FROM Gasto G
WHERE G.fecha_gasto = '20160919'
GROUP BY G.fecha_gasto; 

SELECT LEFT(G.fecha_gasto,10) as Fecha, CONVERT(VARCHAR(8),G.hora,108) Hora, G.Descripcion, G.valor , U.nombre 
 FROM  Gasto G, Usuario U
WHERE G.usuario = U.Usuario 
and fecha_gasto Between '20160918' and '20160924' 
order by G.fecha_gasto ASC;


-- TOTAL VENTAS DEL DIA
SELECT SUM(V.Total) AS Tot_Ventas
FROM Venta as V
WHERE V.Fecha = CONVERT(date, GETDATE());

SELECT CONVERT(date, GETDATE());

-- GASTOS DEL DIA
SELECT SUM(G.valor) AS Tot_Gasto
FROM Gasto as G
WHERE G.fecha_gasto = CONVERT(date, GETDATE());

-- CANTIDAD DE ORDENES
SELECT COUNT(V.Venta)
FROM Venta as V
WHERE V.Fecha = CONVERT(date, GETDATE());

--BALANCE GENERAL
SELECT SUM(ISNULL(Ventas.Tot_Ventas ,0) - ISNULL(Gasto.Tot_Gasto,0)) as BALANCE
FROM ( SELECT SUM(V.Total) AS Tot_Ventas
FROM Venta as V
WHERE V.Fecha = CONVERT(date, GETDATE())) as Ventas, (SELECT SUM(G.valor) AS Tot_Gasto
FROM Gasto as G
WHERE G.fecha_gasto = CONVERT(date, GETDATE())) as Gasto;




SELECT p.Abreviatura, p.Descripcion, d.Cantidad, d.Metros, d.SubTotal
FROM Producto p, Venta v, DetalleVenta d
where d.Venta = v.Venta 
and p.Producto = d.Producto 
and v.Venta = 171;

SELECT p.Abreviatura, p.Descripcion, d.Cantidad, d.Metros, d.SubTotal SubTotal FROM Producto p, Venta v, DetalleVenta d 
where d.Venta = v.Venta 
and p.Producto = d.Producto 
and v.Venta = 131 
;

select c.Direccion , p.Abreviatura, p.Descripcion, d.cantidad, d.metros, d.subtotal
from Cliente c, Venta v, Producto p, DetalleVenta d
where c.Cliente = v.Cliente
and p.Producto = d.Producto
and d.Venta = v.Venta
and v.Venta = 526
group by c.Direccion, v.Hora, p.Abreviatura, p.Descripcion, d.cantidad, d.metros, d.subtotal;

SELECT  v.Venta Venta,CONVERT(VARCHAR(8),v.Hora,108) Hora, c.Cliente Cliente, c.Nombre Nombre, c.Apellido Apellido, c.Direccion Direccion, u.NickName Vendedor, Tipo_Pago, Total 
 FROM  Venta v, Cliente c, Usuario u 
where v.Cliente = c.Cliente 
and v.Usuario = u.Usuario 
and Fecha = '20160425' 
;



SELECT * FROM Venta;
select * from DetalleVentA;

SELECT Ps.Pago, d.Venta, c.Cliente, c.Nombre, c.Apellido, sum(d.Cantidad) Credito , sc.Abono Abonado, SUM(d.Cantidad) - sc.Abono Deuda 
  FROM Pago ps, Venta v, Deuda d join Cliente c on c.Cliente = d.Cliente left join ( 
   select d2.Deuda, SUM(p2.Abono) Abono 
   from Pago p2, Deuda d2 
    where p2.Deuda = d2.Deuda 
    group by d2.Deuda 
) sc on sc.Deuda = d.Deuda WHERE sc.Deuda = ps.Deuda 
 group by ps.Pago, d.Venta, c.Cliente,c.Nombre,c.Apellido, sc.Abono 
having SUM(d.Cantidad) > sc.Abono or sc.Abono is Null 
order by SUM(d.Cantidad) desc 
;
SELECT  D.Deuda, v.Venta, c.Cliente, c.Nombre, c.Apellido, sum(d.Cantidad) Credito , sc.Abono Abonado, SUM(d.Cantidad) - sc.Abono Deuda 
FROM Pago ps, Venta v, Deuda d, Cliente c,(select d2.Deuda, SUM(p2.Abono) Abono 
   from Pago p2, Deuda d2 
    where p2.Deuda = d2.Deuda 
    group by d2.Deuda 
) sc 
WHERE v.Venta = d.Venta
AND sc.Deuda = ps.Deuda
group by D.Deuda, v.Venta, c.Cliente,c.Nombre,c.Apellido, sc.Abono 
having SUM(d.Cantidad) > sc.Abono or sc.Abono is Null 
order by SUM(d.Cantidad) desc 
;



UPDATE  Inventario  SET  Metros_Cuadrados = Metros_Cuadrados - 16.8  WHERE  Producto = 1 ;

--VENTA ANUAL

--SELECT SUM(Total) Total 
-- FROM  Venta v, Cliente c, Usuario u 
--where v.Cliente = c.Cliente 
--and v.Usuario = u.Usuario 
--and Fecha = '20160409' 
;


go
--Si ya eliminaron las tablas dejen asi
--si no descomenten lo de drop table

drop table DetalleVenta;
drop table Pago;
drop table Deuda;
drop table Venta;
drop table Bodega;
drop table Cliente;
drop table Inventario;
drop table Sucursal;
drop table Producto;
drop table Tipo;
drop table Usuario;
drop table Rol;
DROP TABLE Gasto;


go

create table Sucursal(
	Sucursal int identity(1,1) not null,
	Direccion varchar(50) not null,
	Nit varchar(15) null,
	Gerente varchar(25) not null,
	Constraint pk_Sucursal primary key(Sucursal)
)

go

Create table Rol (
	Rol int identity (1,1) not null,
	Nombre varchar(50) not null,
	Constraint  Pk_Rol Primary Key (Rol)
)

go

Create table Usuario (
	Usuario int identity(1,1) not null,
	NickName varchar(30) not null,
	Nombre varchar(50) not null,
	Apellido varchar(50) null,
	Dpi varchar(13) null,
	Rol int not null,
	Contrasenia varchar(50) not null,
	Constraint pk_usuario Primary Key (Usuario),
	Constraint fk_Usuario_Rol Foreign Key (Rol) references Rol(Rol) 
)

go

create table Cliente (
	Cliente int identity(500,1) not null,
	Nombre varchar(50) not null,
	Apellido varchar(50) null,
	Nit varchar(15) null,
	Direccion varchar(50) null,
	Telefono varchar(50) null,
	Constraint pk_Cliente Primary key (Cliente)
) 



go

Create table Venta (
	Venta int identity (1,1) not null,
	Cliente int not null,
	Usuario int not null,
	Fecha date not null,
	Total numeric(9,2) not null,
	Tipo_Pago varchar(50) not null,
	Constraint pk_venta Primary Key (Venta),
	Constraint fk_venta_cliente Foreign Key (Cliente) references Cliente(Cliente), 
	Constraint fk_venta_usuario Foreign Key (Usuario) references Usuario(Usuario) 
	
)

go

Create Table Tipo(
	Tipo int identity(1,1) not null,
	Descripcion varchar(25)  null,
	Constraint pk_Tipo_Producto Primary Key(Tipo)
)

go

Create table Producto(
	Producto int identity(1,1) not null,
	Abreviatura varchar(50) not null,
	Descripcion varchar (100) null,
	Tipo int not null,
	Porcentaje varchar(50) null,
	Ancho numeric(9,2) null,
	Marca varchar(50) null,
	constraint pk_Producto Primary Key (Producto),
	constraint fk_Producto_Tipo foreign key (Tipo) references Tipo(Tipo)
)

go

Create table DetalleVenta(
	Venta int not null,
	Producto int not null,
	Cantidad numeric(9,2) not null,
	Constraint fk_venta_detalle foreign key (Venta) references Venta(Venta),
	Constraint fk_producto_detalle foreign key (Producto) references Producto(Producto)
)

go

create table Deuda(
	Deuda int identity(1,1) not null,
	Cliente int not null,
	Cantidad numeric(9,2) not null,
	Venta int not null,
	Constraint fk_Venta foreign key (Venta) references Venta(Venta), 
	Constraint pk_Deuda Primary key (Deuda),
	Constraint fk_Deuda_Cliente foreign key (Cliente) references Cliente(Cliente) 
)	

go

create table Pago(
	Pago int identity(1,1) not null,
	Deuda int not null,
	Fecha Date not null,
	Abono numeric(9,2) not null,
	Usuario int not null,
	Constraint fk_Pago_Deuda foreign key (Deuda) references Deuda(Deuda),
	Constraint fk_Pago_Usuario foreign key (Usuario) references Usuario(Usuario),
	Constraint pk_Pago Primary Key (Pago,Deuda)
) 
go

create table Inventario(
	Sucursal int not null,
	Producto int not null,
	Precio int not null,
	Cantidad int null,
	Metros_Cuadrados numeric(9,2) null,
	
	Constraint fk_Inventario_Sucursal foreign key (Sucursal) references Sucursal(Sucursal),
	Constraint fk_Inventario_Producto foreign key (Producto) references Producto(Producto),
)

go

create table Bodega(
	Bodega int identity(1,1) not null,
	Producto int not null,
	Cantidad int not null,
	Constraint fk_Bodega_Producto foreign key (Producto) references Producto(Producto),
	Constraint pk_Bodega primary key(Bodega)
)

go
--nueva tabla

create table Gasto(
	Gasto int identity(1,1) not null,
	descripcion varchar(255),
	valor numeric(9,2) not null,
	fecha_gasto date not null,
	hora Time(7) not null,
	usuario int not null,
	Constraint fk_Gasto_usuario foreign key (usuario) references Usuario(usuario),
	Constraint pk_Gasto primary key(Gasto),

	)

go

--Agregar columna descuento a detalle_venta
alter table DetalleVenta
add Descuento numeric(9,2) not null default 0;


SELECT   ISNULL(SUM(DV.Descuento),0)   AS Total_Credi 
 FROM DetalleVenta DV, Venta as V 
 WHERE V.Fecha = '20160911' 
 AND DV.Venta = V.Venta
 AND V.Tipo_Pago = 'Deuda'
 AND V.Usuario = '1' 
;


 SELECT   ISNULL(SUM(dv.Descuento),0)   AS Total_Credi 
 FROM  DetalleVenta dv, Venta as V 
 WHERE V.Fecha = '20161025' 
 AND V.Usuario = '4' 
 ;

----- Vales 20/01/17 -----

create table TipoCliente(
tipocliente int identity(1,1) not null,
descripcion varchar(50) null,
Constraint pk_tipocliente Primary Key(tipocliente)
)

alter table Cliente add  tipocliente int  null default 1

ALTER TABLE Cliente
ADD CONSTRAINT fk_clientetipo
FOREIGN KEY (tipocliente)
REFERENCES TipoCliente(tipocliente)

insert into TipoCliente (descripcion) values('normal')
insert into TipoCliente (descripcion) values('vale')
