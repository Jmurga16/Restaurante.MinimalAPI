

if exists(SELECT * FROM sys.sysdatabases where name='DBLosAromos')
drop database DBLosAromos
go

create database DBLosAromos
go

-- Usar la nueva base de datos
USE DBLosAromos;
GO
 

-- CREACION DE TABLAS

-- Tabla Menu
create table Menu (
    Menu_ID int identity(1,1) primary key,
    Menu_Nombre varchar(50) not null unique,
    Menu_Descripcion varchar(200) not null
)
go

-- Tabla Ingredientes
create table Ingredientes (
    Ingrediente_ID  int identity(1,1) primary key,
    Ingrediente_Nombre varchar(30) not null unique,
    Ingrediente_Unidad_Medida varchar (15) not null,
    Ingrediente_Cantidad_Disp int not null
)
go

-- Tabla Encargado
create table Encargado (
    Encargado_ID int identity(1,1) primary key,
    Encargado_Nombre varchar(40) not null,
    Encargado_Apellido varchar(50) not null,
    Encargado_Tipo_Doc varchar(10) not null,
    Encargado_Documento int not null unique,
    Encargado_Mail varchar(50) unique,
    Encargado_Tel varchar(15),
    Encargado_Direccion varchar(100),
	constraint UK_TipoDoc_NroDoc unique (Encargado_Tipo_Doc,Encargado_Documento)
)
go

-- Tabla Categoria
create table Categoria (
    Categoria_ID int identity(1,1) primary key,
    Categoria_Nombre  varchar (40) not null unique,
    Categoria_Descripcion varchar (255) not null,
	Encargado_ID int foreign key references Encargado(Encargado_ID)
)
go

-- Tabla Plato
create table Plato (
    Plato_ID int identity(1,1) primary key,
    Plato_Nombre varchar(50) not null unique,
    Plato_Descripcion varchar(200) not null,
    Plato_Foto varchar(200) not null,
    Plato_Dificultad varchar(20) not null,
    Plato_Precio int not null,
	Categoria_ID int foreign key references Categoria(Categoria_ID)
)
go

-- Tabla Receta
create table Receta (
    Receta_ID int identity(1,1) primary key,
    Receta_Preparacion varchar(300) not null,
	Plato_ID int foreign key references Plato(Plato_ID) on delete cascade
)
go

-- Tabla Menu_Plato
create table Menu_Plato (
    Menu_ID int foreign key references Menu(Menu_ID),
    Plato_ID int foreign key references Plato(Plato_ID),
	constraint UKMenuPlato primary key (Plato_ID, Menu_ID)
)
go

-- Tabla Receta_Ingredientes
create table Receta_Ingredientes (
    Receta_ID int foreign key references Receta(Receta_ID),
    Ingrediente_ID int foreign key references Ingredientes(Ingrediente_ID),
    REC_ING_Cantidad_Requerida decimal(10, 2) not null,
	constraint UKRecetaIngrediente primary key (Receta_ID, Ingrediente_ID)
)
go

-- FIN CREACIÓN DE TABLAS

-- CREACIÓN STORED PROCEDURES

-- Insertar Tabla Menu
create procedure Insert_Menu
    @Nombre varchar(50),
    @Descripcion varchar(200)
as
begin
    insert into Menu (Menu_Nombre, Menu_Descripcion)
    values (@Nombre, @Descripcion);
end
go

-- Insertar Tabla Ingrediente
create procedure Insert_Ingredientes
    @Nombre varchar(30),
    @Unidad_Medida varchar(15),
    @Cantidad_Disp int
as
begin
    insert into Ingredientes (Ingrediente_Nombre, Ingrediente_Unidad_Medida, Ingrediente_Cantidad_Disp)
    values (@Nombre, @Unidad_Medida, @Cantidad_Disp);
end
go

-- Insertar Tabla Encargado
create procedure Insert_Encargado
    @Nombre varchar(40),
    @Apellido varchar(50),
    @Tipo_Doc varchar(10),
    @Documento int,
    @Mail varchar(50) = NULL,
    @Tel varchar(15) = NULL,
    @Direccion varchar(100) = NULL
as
begin
    insert into Encargado (Encargado_Nombre, Encargado_Apellido, Encargado_Tipo_Doc, Encargado_Documento, Encargado_Mail, Encargado_Tel, Encargado_Direccion)
    values (@Nombre, @Apellido, @Tipo_Doc, @Documento, @Mail, @Tel, @Direccion);
end
go

-- Insertar Tabla Categoria
create procedure Insert_Categoria
    @Nombre varchar(40),
    @Descripcion varchar(255),
    @Encargado_ID int
as
begin
    insert into Categoria (Categoria_Nombre, Categoria_Descripcion, Encargado_ID)
    values (@Nombre, @Descripcion, @Encargado_ID);
end
go

-- Insertar Tabla Plato
create procedure Insert_Plato
    @Nombre varchar(50),
    @Descripcion varchar(200),
    @Foto varchar(200),
    @Dificultad varchar(20),
    @Precio int,
    @Categoria_ID int
as
begin
    insert into Plato (Plato_Nombre, Plato_Descripcion, Plato_Foto, Plato_Dificultad, Plato_Precio, Categoria_ID)
    values (@Nombre, @Descripcion, @Foto, @Dificultad, @Precio, @Categoria_ID);
end
go

-- Insertar Tabla Receta
create procedure Insert_Receta
    @Receta_Preparacion varchar(300),
    @Plato_ID int
as
begin
    insert into Receta (Receta_Preparacion, Plato_ID)
    values (@Receta_Preparacion, @Plato_ID);
end
go

-- Insertar Tabla MenuPlato
create procedure Insert_MenuPlato
    @Menu_ID int,
    @Plato_ID int
as
begin
    insert into Menu_Plato (Menu_ID, Plato_ID)
    values (@Menu_ID, @Plato_ID);
end
go

-- Insertar Tabla RecetaIngrediente
create procedure Insert_RecetaIngrediente
    @Receta_ID int,
    @Ingrediente_ID int,
	@Cantidad_Requerida decimal(10, 2)
as
begin
    insert into Receta_Ingredientes (Receta_ID, Ingrediente_ID, REC_ING_Cantidad_Requerida)
    values (@Receta_ID, @Ingrediente_ID, @Cantidad_Requerida);
end
go

-- Modificar Plato
CREATE PROCEDURE Update_Plato (
	@ID int,
	@Nombre varchar(50),
	@Descripcion varchar(200),
	@Foto varchar(200),
	@Dificultad varchar(20),
	@Precio int,
	@Categoria_ID int)
 AS
BEGIN
    update Plato set 
	Plato_Nombre=@Nombre,
	Plato_Descripcion=@Descripcion,
	Plato_Foto=@Foto,
	Plato_Precio=@Precio,
	Plato_Dificultad=@Dificultad,
	Categoria_ID=@Categoria_ID
	where Plato_ID=@ID
END
go

---------------------

-- Modificar Receta
CREATE PROCEDURE Update_Receta (
	@Receta_Preparacion varchar(200),
	@ID int)
 AS
BEGIN
    update Receta set 
	Receta_Preparacion=@Receta_Preparacion
	where Receta_ID=@ID
END
go

-- Borrar Plato
create procedure Delete_Plato
    @PlatoID INT
AS
BEGIN
    DELETE FROM Plato
    WHERE Plato_ID = @PlatoID;
END
go

-- Eliminar Receta
CREATE PROCEDURE Eliminar_Receta
    @Receta_ID INT
AS
BEGIN
    DELETE FROM Receta WHERE Receta_ID = @Receta_ID;
END;
GO

--MODIFICAR MENU
CREATE PROCEDURE Menu_Update (@Menu_ID int, @Menu_Nombre varchar(50), @Menu_Descripcion varchar(200)) AS
BEGIN
UPDATE Menu
SET
Menu_Nombre = @Menu_Nombre, 
Menu_Descripcion = @Menu_Descripcion
WHERE Menu_ID = @Menu_ID
END
GO

--MODIFICAR INGREDIENTES
CREATE PROCEDURE Ingredientes_Update(@Ingrediente_ID int, @Ingrediente_Nombre varchar(30), @Ingrediente_Cantidad_Disp int, @Ingrediente_Unidad_Medida varchar(15)) AS
BEGIN
UPDATE Ingredientes
SET
Ingrediente_Nombre = @Ingrediente_Nombre,
Ingrediente_Cantidad_Disp = @Ingrediente_Cantidad_Disp,
Ingrediente_Unidad_Medida = @Ingrediente_Unidad_Medida 
WHERE Ingrediente_ID = @Ingrediente_ID
END
GO

--ELIMINAR MENU
CREATE PROCEDURE Menu_Delete (@Menu_ID int) AS
DELETE Menu WHERE Menu_ID = @Menu_ID;

--ELIMINAR MENU_PLATO EN CASCADA
ALTER TABLE Menu_Plato
	ADD CONSTRAINT menu_plato_fk
		FOREIGN KEY(Menu_ID)
		REFERENCES Menu(Menu_ID)
		ON DELETE CASCADE
		ON UPDATE CASCADE
GO

--ELIMINAR INGREDIENTES
CREATE PROCEDURE Ingredientes_Delete (@Ingrediente_ID int) AS
DELETE Ingredientes WHERE Ingrediente_ID = @Ingrediente_ID;

--ELIMINAR RECETA_INGREDIENTES EN CASCADA
ALTER TABLE Receta_Ingredientes
	ADD CONSTRAINT receta_ingredientes_fk
		FOREIGN KEY(Ingrediente_ID)
		REFERENCES Ingredientes(Ingrediente_ID)
		ON DELETE CASCADE
		ON UPDATE CASCADE
GO

--CREACIÓN DE ELIMINAR

-- ELIMINAR ENCARGADO
CREATE PROCEDURE Delete_Encargado (
	@Encargado_ID int)
as 
DELETE Encargado where Encargado_ID=@Encargado_ID
go

-- ELIMINAR CATEGORIA
CREATE PROCEDURE Delete_Categoria (
	@Categoria_ID int)
as
DELETE Categoria where Categoria_ID=@Categoria_ID
go

--FIN DE ELIMINAR



--CREACIÓN DE MODIFICAR

--MODIFICAR ENCARGADO
CREATE PROCEDURE Update_Encargado(
	@Encargado_ID int,
	@Encargado_Nombre varchar(40),
	@Encargado_Apellido varchar(50),
	@Encargado_Tipo_Doc varchar(10),
	@Encargado_Documento int,
	@Encargado_Mail varchar(50),
	@Encargado_Tel varchar(15),
	@Encargado_Direccion varchar(100)
)
AS
UPDATE Encargado SET
	Encargado_Nombre = @Encargado_Nombre,
	Encargado_Apellido = @Encargado_Apellido,
	Encargado_Tipo_Doc = @Encargado_Tipo_Doc,
	Encargado_Documento = @Encargado_Documento,
	Encargado_Mail = @Encargado_Mail,
	Encargado_Tel = @Encargado_Tel,
	Encargado_Direccion = @Encargado_Direccion
WHERE
Encargado_ID = @Encargado_ID
GO


--MODIFICAR CATEGORIA
CREATE PROCEDURE Update_Categoria(
	@Categoria_ID int,
	@Categoria_Nombre varchar (40),
	@Categoria_Descripcion varchar (255)
)
AS
UPDATE Categoria SET
	Categoria_Nombre = @Categoria_Nombre,
	Categoria_Descripcion = @Categoria_Descripcion 
WHERE
Categoria_ID = @Categoria_ID
GO

--FIN DE MODIFICAR


-- CHECK CONSTRAINT

-- ELIMINAR ENCARGADO_CATEGORIA EN CASCADA
ALTER TABLE Categoria
	ADD CONSTRAINT fk_Encargado_Categoria
	FOREIGN KEY (Encargado_ID)
	REFERENCES Encargado(Encargado_ID)
ON DELETE CASCADE
ON UPDATE CASCADE;
GO



-- FIN CREACION STORED PROCEDURES

-- INSERTS TABLAS

-- Insertar Menús
exec Insert_Menu @Nombre = 'Menú Ejecutivo', @Descripcion = 'Menú diario con opciones variadas para el almuerzo de trabajo.';
exec Insert_Menu @Nombre = 'Menú Degustación', @Descripcion = 'Menú especial con una selección de platos gourmet para una experiencia gastronómica completa.';
exec Insert_Menu @Nombre = 'Menú Infantil', @Descripcion = 'Menú con opciones adaptadas para niños, incluyendo platos pequeños y saludables.';

SELECT * FROM Menu;

-- Insertar Ingredientes
exec Insert_Ingredientes @Nombre = 'Tomate', @Cantidad_Disp = 10, @Unidad_Medida = 'kg';
exec Insert_Ingredientes @Nombre = 'Aceite de Oliva', @Cantidad_Disp = 10, @Unidad_Medida = 'litro';
exec Insert_Ingredientes @Nombre = 'Queso Feta', @Cantidad_Disp = 20, @Unidad_Medida = 'kg';
exec Insert_Ingredientes @Nombre = 'Aceitunas', @Cantidad_Disp = 5, @Unidad_Medida = 'kg';
exec Insert_Ingredientes @Nombre = 'Mariscos', @Cantidad_Disp = 20, @Unidad_Medida = 'kg';
exec Insert_Ingredientes @Nombre = 'Carne', @Cantidad_Disp = 50, @Unidad_Medida = 'kg';
exec Insert_Ingredientes @Nombre = 'Cebolla', @Cantidad_Disp = 15, @Unidad_Medida = 'kg';
exec Insert_Ingredientes @Nombre = 'Zanahoria', @Cantidad_Disp = 10, @Unidad_Medida = 'kg';
exec Insert_Ingredientes @Nombre = 'Morrón', @Cantidad_Disp = 10, @Unidad_Medida = 'kg';
exec Insert_Ingredientes @Nombre = 'Leche', @Cantidad_Disp = 30, @Unidad_Medida = 'litro';
exec Insert_Ingredientes @Nombre = 'Harina', @Cantidad_Disp = 20, @Unidad_Medida = 'kg';
exec Insert_Ingredientes @Nombre = 'Azúcar', @Cantidad_Disp = 25, @Unidad_Medida = 'kg';
exec Insert_Ingredientes @Nombre = 'Huevos', @Cantidad_Disp = 96, @Unidad_Medida = 'u';
exec Insert_Ingredientes @Nombre = 'Manteca', @Cantidad_Disp = 2, @Unidad_Medida = 'kg';
exec Insert_Ingredientes @Nombre = 'Nueces', @Cantidad_Disp = 5, @Unidad_Medida = 'kg';
exec Insert_Ingredientes @Nombre = 'Lechuga', @Cantidad_Disp = 10, @Unidad_Medida = 'kg';
exec Insert_Ingredientes @Nombre = 'Pasta', @Cantidad_Disp = 30, @Unidad_Medida = 'kg';
exec Insert_Ingredientes @Nombre = 'Queso Mozzarella', @Cantidad_Disp = 15, @Unidad_Medida = 'kg';
exec Insert_Ingredientes @Nombre = 'Café', @Cantidad_Disp = 5, @Unidad_Medida = 'kg';
exec Insert_Ingredientes @Nombre = 'Queso crema', @Cantidad_Disp = 10, @Unidad_Medida = 'kg';
exec Insert_Ingredientes @Nombre = 'Pollo', @Cantidad_Disp = 40, @Unidad_Medida = 'kg';
exec Insert_Ingredientes @Nombre = 'Pimienta', @Cantidad_Disp = 2, @Unidad_Medida = 'kg';
exec Insert_Ingredientes @Nombre = 'Albahaca', @Cantidad_Disp = 3, @Unidad_Medida = 'kg';
exec Insert_Ingredientes @Nombre = 'Queso rayado', @Cantidad_Disp = 5, @Unidad_Medida = 'kg';
exec Insert_Ingredientes @Nombre = 'Especias', @Cantidad_Disp = 2, @Unidad_Medida = 'kg';
exec Insert_Ingredientes @Nombre = 'Mermelada', @Cantidad_Disp = 6, @Unidad_Medida = 'kg';
exec Insert_Ingredientes @Nombre = 'Galleta', @Cantidad_Disp = 3, @Unidad_Medida = 'kg';
exec Insert_Ingredientes @Nombre = 'Salsa de Tomate', @Cantidad_Disp = 10, @Unidad_Medida = 'litro';
exec Insert_Ingredientes @Nombre = 'Queso Mascarpone', @Cantidad_Disp = 15, @Unidad_Medida = 'kg';
exec Insert_Ingredientes @Nombre = 'ajo', @Cantidad_Disp = 2, @Unidad_Medida = 'kg';

SELECT * FROM Ingredientes;

-- Insertar Encargados
exec Insert_Encargado @Nombre = 'Ana', @Apellido = 'Martínez', @Tipo_Doc = 'DNI', @Documento = 23456789, @Mail = 'ana.martinez@gmail.com', @Tel = '1153456789', @Direccion = 'Av. del Libertador 1580, Olivos';
exec Insert_Encargado @Nombre = 'Carlos', @Apellido = 'López', @Tipo_Doc = 'DNI', @Documento = 34567890, @Mail = 'carlos.lopez@gmail.com', @Tel = '1146567890', @Direccion = 'Mitre 1698, Villa Lynch';
exec Insert_Encargado @Nombre = 'Hector', @Apellido = 'Cavenaghi', @Tipo_Doc = 'DNI', @Documento = 31003154, @Mail = 'hector.cavenaghi@gmail.com', @Tel = '1146785512', @Direccion = 'Capdevilla 1788, Munro';

SELECT * FROM Encargado;

-- Insertar Categorías
exec Insert_Categoria @Nombre = 'Entradas', @Descripcion = 'Pequeñas porciones para comenzar.', @Encargado_ID = 1;
exec Insert_Categoria @Nombre = 'Platos Principales', @Descripcion = 'Platos fuertes para el almuerzo o cena.', @Encargado_ID = 2;
exec Insert_Categoria @Nombre = 'Postres', @Descripcion = 'Opciones dulces para terminar la comida.', @Encargado_ID = 3;

--query categorias y encargados
SELECT 
    C.*,
    E.Encargado_Nombre
FROM 
    Categoria C
JOIN 
    Encargado E ON C.Encargado_ID = E.Encargado_ID;


SELECT C.*FROM Categoria C JOIN Encargado E ON C.Encargado_ID = E.Encargado_ID
-- modificar consulta de categoria (indicar nombre de encargado ademàs del ID)
-- Insertar Platos
exec Insert_Plato @Nombre = 'Ensalada Griega', @Descripcion = 'Ensalada fresca con queso feta, aceitunas y aderezo de orégano.', @Foto = 'ensalada_griega.jpg', @Dificultad = 'Fácil', @Precio = 8000, @Categoria_ID = 1;
exec Insert_Plato @Nombre = 'Paella Mixta', @Descripcion = 'Paella con mariscos y carne.', @Foto = 'paella_mixta.jpg', @Dificultad = 'Media', @Precio = 15000, @Categoria_ID = 2;
exec Insert_Plato @Nombre = 'Brownie de Chocolate', @Descripcion = 'Brownie con trozos de chocolate y nueces.', @Foto = 'brownie_chocolate.jpg', @Dificultad = 'Fácil', @Precio = 3500, @Categoria_ID = 3;
exec Insert_Plato @Nombre = 'Ensalada Mixta', @Descripcion = 'Ensalada fresca con lechuga, cebolla, tomate', @Foto ='ensalada_mixta.jpg', @Dificultad =  'Fácil', @Precio = 6500, @Categoria_ID = 1;
exec Insert_Plato @Nombre = 'Pizza Mozzarella', @Descripcion = 'Pizza con salsa de tomate y queso', @Foto ='pizza_mozza.jpg', @Dificultad =  'Intermedio', @Precio = 10000, @Categoria_ID = 2;
exec Insert_Plato @Nombre = 'Tiramisú', @Descripcion = 'Postre clásico italiano con café y queso mascarpone', @Foto ='tiramisu.jpg', @Dificultad =  'Fácil', @Precio = 5500, @Categoria_ID = 3;
exec Insert_Plato @Nombre = 'Sopa de Pollo', @Descripcion = 'Sopa reconfortante con trozos de pollo y vegetales', @Foto ='sopa.jpg', @Dificultad =  'Fácil', @Precio = 9000, @Categoria_ID = 1;
exec Insert_Plato @Nombre = 'Lasaña Boloñesa', @Descripcion = 'Lasaña con carne, salsa de tomate y queso.', @Foto = 'lasagna_bolonesa.jpg', @Dificultad = 'Media', @Precio = 16000, @Categoria_ID = 2;
exec Insert_Plato @Nombre = 'Cheesecake', @Descripcion = 'Postre cremoso con base de galleta y mermelada.', @Foto = 'cheesecake.jpg', @Dificultad = 'Media', @Precio = 5500, @Categoria_ID = 3;
-- modificar consulta de plato (indicar nombre de categoria ademàs del ID)

--query categorias y encargados
SELECT 
    P.*,
    C.Categoria_Nombre
FROM 
    Plato P
JOIN 
    Categoria C ON P.Categoria_ID = C.Categoria_ID;

-- Insertar Recetas
exec Insert_Receta @Receta_Preparacion = 'Mezclar tomates, pepino, cebolla, queso feta, aceite de oliva y aderezo de orégano. Servir frío.', @Plato_ID = 1;
exec Insert_Receta @Receta_Preparacion = 'Cocinar arroz con mariscos y carne, añadir verduras y sazonar al gusto.', @Plato_ID = 2;
exec Insert_Receta @Receta_Preparacion = 'Preparar mezcla de harina, huevos, leche, azúcar y manteca, añadir chocolate y nueces, hornear hasta que esté cocido.', @Plato_ID = 3;
exec Insert_Receta @Receta_Preparacion = 'Mezcla de lechuga, tomate y cebolla.', @Plato_ID = 4;
exec Insert_Receta @Receta_Preparacion = 'Preparar la masa, añadir salsa, queso mozzarella y hornear.', @Plato_ID = 5;
exec Insert_Receta @Receta_Preparacion = 'Mezclar café y queso mascarpone, leche, huevos, refrigerar y servir.', @Plato_ID = 6;
exec Insert_Receta @Receta_Preparacion = 'Mezclar pollo hervido con verduras y fideos.', @Plato_ID = 7;
exec Insert_Receta @Receta_Preparacion = 'Calentar el aceite y cocinar la cebolla y el ajo. Agregar la carne molida, cocinar hasta dorar y añadir la salsa de tomate ,colocar capas de salsa de carne, láminas de lasaña y queso hasta cocinar', @Plato_ID = 8;
exec Insert_Receta @Receta_Preparacion = 'Triturar las galletas y mezclarlas con manteca derretida. Presionar la mezcla en el fondo de un molde. Batir el queso crema con el azúcar y leche. Verter la mezcla y decorar con mermelada antes de servir', @Plato_ID = 9;
-- modificar consulta (indicar nombre y ID de receta y plato)


SELECT 
    R.*,
    P.Plato_Nombre
FROM 
    Receta R
JOIN 
    Plato P ON R.Plato_ID = P.Plato_ID;

-- Insertar Relación Menú-Plato

exec Insert_MenuPlato @Plato_ID = 1, @Menu_ID = 1;  -- Ensalada Griega en Menú Ejecutivo
exec Insert_MenuPlato @Plato_ID = 2, @Menu_ID = 1;  -- Paella Mixta en Menú Ejecutivo
exec Insert_MenuPlato @Plato_ID = 3, @Menu_ID = 1;  -- Brownie de Chocolate en Menú Ejecutivo
exec Insert_MenuPlato @Plato_ID = 6, @Menu_ID = 1;  -- Tiramisú en Menú Ejecutivo
exec Insert_MenuPlato @Plato_ID = 7, @Menu_ID = 1;  -- Sopa de Pollo en Menú Ejecutivo
exec Insert_MenuPlato @Plato_ID = 8, @Menu_ID = 1;  -- Lasaña Boloñesa en Menú Ejecutivo

exec Insert_MenuPlato @Plato_ID = 2, @Menu_ID = 2;  -- Paella Mixta en Menú Degustación
exec Insert_MenuPlato @Plato_ID = 3, @Menu_ID = 2;  -- Brownie de Chocolate en Menú Degustación
exec Insert_MenuPlato @Plato_ID = 4, @Menu_ID = 2;  -- Ensalada Mixta en Menú Degustación
exec Insert_MenuPlato @Plato_ID = 6, @Menu_ID = 2;  -- Tiramisú en Menú Degustación
exec Insert_MenuPlato @Plato_ID = 7, @Menu_ID = 2;  -- Sopa de Pollo en Menú Degustación
exec Insert_MenuPlato @Plato_ID = 9, @Menu_ID = 2;  -- Cheesecake en Menú Degustación

exec Insert_MenuPlato @Plato_ID = 1, @Menu_ID = 3;  -- Ensalada Griega en Menú Infantil
exec Insert_MenuPlato @Plato_ID = 2, @Menu_ID = 3;  -- Paella Mixta en Menú Infantil
exec Insert_MenuPlato @Plato_ID = 3, @Menu_ID = 3;  -- Brownie de Chocolate en Menú Infantil
exec Insert_MenuPlato @Plato_ID = 5, @Menu_ID = 3;  -- Pizza Mozzarella en Menú Infantil
exec Insert_MenuPlato @Plato_ID = 9, @Menu_ID = 3;  -- Cheesecake en Menú Infantil
-- modificar consulta (indicar nombre y ID de menù y plato)


SELECT 
    MP.Menu_ID,
    M.Menu_Nombre,
    MP.Plato_ID,
    P.Plato_Nombre
FROM 
    Menu_Plato MP
JOIN 
    Menu M ON MP.Menu_ID = M.Menu_ID
JOIN 
    Plato P ON MP.Plato_ID = P.Plato_ID;


-- Insertar Relación Receta-Ingredientes para "Ensalada Griega"
exec Insert_RecetaIngrediente @Receta_ID = 1, @Ingrediente_ID = 1, @Cantidad_Requerida = 0.2;  -- 200 gramos de tomate
exec Insert_RecetaIngrediente @Receta_ID = 1, @Ingrediente_ID = 2, @Cantidad_Requerida = 0.05; -- 0.05 litro de aceite de oliva
exec Insert_RecetaIngrediente @Receta_ID = 1, @Ingrediente_ID = 3, @Cantidad_Requerida = 0.05; -- 50 gramos de queso feta
exec Insert_RecetaIngrediente @Receta_ID = 1, @Ingrediente_ID = 4, @Cantidad_Requerida = 0.05; -- 50 gramos de aceitunas
exec Insert_RecetaIngrediente @Receta_ID = 1, @Ingrediente_ID = 7, @Cantidad_Requerida = 0.1;  -- 100 gramos de Cebolla
exec Insert_RecetaIngrediente @Receta_ID = 1, @Ingrediente_ID = 15, @Cantidad_Requerida = 0.03;  -- 30 gramos de Nueces

-- Insertar Relación Receta-Ingredientes para "Paella Mixta"
exec Insert_RecetaIngrediente @Receta_ID = 2, @Ingrediente_ID = 5, @Cantidad_Requerida = 0.1;  -- 100 gramos de Mariscos
exec Insert_RecetaIngrediente @Receta_ID = 2, @Ingrediente_ID = 6, @Cantidad_Requerida = 0.1;  -- 100 gramos de Carne
exec Insert_RecetaIngrediente @Receta_ID = 2, @Ingrediente_ID = 7, @Cantidad_Requerida = 0.1;  -- 100 gramos de Cebolla
exec Insert_RecetaIngrediente @Receta_ID = 2, @Ingrediente_ID = 8, @Cantidad_Requerida = 0.08;  -- 80 gramos de Zanahoria
exec Insert_RecetaIngrediente @Receta_ID = 2, @Ingrediente_ID = 9, @Cantidad_Requerida = 0.07;  -- 70 gramos de Morrón

-- Insertar Relación Receta-Ingredientes para "Brownie de Chocolate"
exec Insert_RecetaIngrediente @Receta_ID = 3, @Ingrediente_ID = 10, @Cantidad_Requerida = 0.05;  -- 0.05 litro de leche
exec Insert_RecetaIngrediente @Receta_ID = 3, @Ingrediente_ID = 11, @Cantidad_Requerida = 0.15;  -- 150 gramos de harina
exec Insert_RecetaIngrediente @Receta_ID = 3, @Ingrediente_ID = 12, @Cantidad_Requerida = 0.2;  -- 200 gramos de azucar
exec Insert_RecetaIngrediente @Receta_ID = 3, @Ingrediente_ID = 13, @Cantidad_Requerida = 0.2;  -- 2 huevos
exec Insert_RecetaIngrediente @Receta_ID = 3, @Ingrediente_ID = 14, @Cantidad_Requerida = 0.02;  -- 20 gramos de manteca

---- Insertar Relación Receta-Ingredientes para "Ensalada Mixta"

exec Insert_RecetaIngrediente @Receta_ID = 4, @Ingrediente_ID = 1, @Cantidad_Requerida = 0.2;  -- 200 gramos de tomate
exec Insert_RecetaIngrediente @Receta_ID = 4, @Ingrediente_ID = 7, @Cantidad_Requerida = 0.1;  -- 100 gramos de Cebolla
exec Insert_RecetaIngrediente @Receta_ID = 4, @Ingrediente_ID = 16, @Cantidad_Requerida = 0.1;  -- 100 gramos de lechuga

---- Insertar Relación Receta-Ingredientes para "Pizza Mozzarella"

exec Insert_RecetaIngrediente @Receta_ID = 5, @Ingrediente_ID = 11, @Cantidad_Requerida = 0.25;  -- 250 gramos de harina
exec Insert_RecetaIngrediente @Receta_ID = 5, @Ingrediente_ID = 28, @Cantidad_Requerida = 0.150;  -- 0,150 litros de salsa de tomate
exec Insert_RecetaIngrediente @Receta_ID = 5, @Ingrediente_ID = 18, @Cantidad_Requerida = 0.25;  -- 250 gramos de queso mozzarella
exec Insert_RecetaIngrediente @Receta_ID = 5, @Ingrediente_ID = 4, @Cantidad_Requerida = 0.05;  -- 50 gramos de aceitunas

---Insertar Relación Receta-Ingredientes para "Tiramisú"

exec Insert_RecetaIngrediente @Receta_ID = 6, @Ingrediente_ID = 19, @Cantidad_Requerida = 0.02;  -- 20 gramos de cafe
exec Insert_RecetaIngrediente @Receta_ID = 6, @Ingrediente_ID = 29, @Cantidad_Requerida = 0.150;  -- 150 gramos de queso mascarpone
exec Insert_RecetaIngrediente @Receta_ID = 6, @Ingrediente_ID = 10, @Cantidad_Requerida = 0.1;  -- 0.1 litros de leche
exec Insert_RecetaIngrediente @Receta_ID = 6, @Ingrediente_ID = 13, @Cantidad_Requerida = 2;  -- 2 huevos

----Insertar Relación Receta-Ingredientes para "Sopa de Pollo"

exec Insert_RecetaIngrediente @Receta_ID = 7, @Ingrediente_ID = 21, @Cantidad_Requerida = 0.200;  -- 200 gramos de pollo
exec Insert_RecetaIngrediente @Receta_ID = 7, @Ingrediente_ID = 7, @Cantidad_Requerida = 0.05;  -- 50 gramos de Cebolla
exec Insert_RecetaIngrediente @Receta_ID = 7, @Ingrediente_ID = 8, @Cantidad_Requerida = 0.05;  -- 50 gramos de Zanahoria
exec Insert_RecetaIngrediente @Receta_ID = 7, @Ingrediente_ID = 23, @Cantidad_Requerida = 0.01;  -- 10 gramos de albahaca
exec Insert_RecetaIngrediente @Receta_ID = 7, @Ingrediente_ID = 17, @Cantidad_Requerida = 0.100;  -- 100 gramos de pastas
exec Insert_RecetaIngrediente @Receta_ID = 7, @Ingrediente_ID = 22, @Cantidad_Requerida = 0.003;  -- 3 gramos de pimienta
exec Insert_RecetaIngrediente @Receta_ID = 7, @Ingrediente_ID = 25, @Cantidad_Requerida = 0.005;  -- 5 gramos de especias

----Insertar Relación Receta-Ingredientes para "Lasaña Boloñesa"

exec Insert_RecetaIngrediente @Receta_ID = 8, @Ingrediente_ID = 30, @Cantidad_Requerida = 0.01;  -- 10 gramos de ajo
exec Insert_RecetaIngrediente @Receta_ID = 8, @Ingrediente_ID = 24, @Cantidad_Requerida = 0.1;  -- 100 gramos de queso rayado
exec Insert_RecetaIngrediente @Receta_ID = 8, @Ingrediente_ID = 7, @Cantidad_Requerida = 0.05;  -- 50 gramos de Cebolla
exec Insert_RecetaIngrediente @Receta_ID = 8, @Ingrediente_ID = 6, @Cantidad_Requerida = 0.1;  -- 100 gramos de Carne
exec Insert_RecetaIngrediente @Receta_ID = 8, @Ingrediente_ID = 28, @Cantidad_Requerida = 0.15;  -- 0,150 litros de salsa de tomate
exec Insert_RecetaIngrediente @Receta_ID = 8, @Ingrediente_ID = 11, @Cantidad_Requerida = 0.2;  -- 200 gramos de harina
exec Insert_RecetaIngrediente @Receta_ID = 8, @Ingrediente_ID = 13, @Cantidad_Requerida = 2;  -- 2 huevos

----Insertar Relación Receta-Ingredientes para "Cheesecake"

exec Insert_RecetaIngrediente @Receta_ID = 9, @Ingrediente_ID = 14, @Cantidad_Requerida = 0.02;  -- 20 gramos de manteca
exec Insert_RecetaIngrediente @Receta_ID = 9, @Ingrediente_ID = 27, @Cantidad_Requerida = 0.07;  -- 70 gramos de galletas
exec Insert_RecetaIngrediente @Receta_ID = 9, @Ingrediente_ID = 20, @Cantidad_Requerida = 0.1;  -- 100 gramos queso crema
exec Insert_RecetaIngrediente @Receta_ID = 9, @Ingrediente_ID = 12, @Cantidad_Requerida = 0.2;  -- 200 gramos de azucar
exec Insert_RecetaIngrediente @Receta_ID = 9, @Ingrediente_ID = 10, @Cantidad_Requerida = 0.1;  -- 0.1 litros de leche
exec Insert_RecetaIngrediente @Receta_ID = 9, @Ingrediente_ID = 26, @Cantidad_Requerida = 0.05;  -- 50 gramos de mermelada
-- receta ID + receta nombre + ingrediente ID + Ingrediente nombre + unidad ingrediente + cantidad ingrediente


SELECT 
    R.Receta_ID,
    RI.Ingrediente_ID,
    I.Ingrediente_Nombre,
    I.Ingrediente_Unidad_Medida,
    RI.REC_ING_Cantidad_Requerida
FROM 
    Receta R
JOIN 
    Receta_Ingredientes RI ON R.Receta_ID = RI.Receta_ID
JOIN 
    Ingredientes I ON RI.Ingrediente_ID = I.Ingrediente_ID;


-- Modificar Plato 2:
--exec Update_Plato 2,'Pizza Mozzarella','Pizza con salsa de tomate y queso','pizza_mozza.jpg','Intermedio',10000,2



-- Agregar una clave externa con acción ON DELETE CASCADE
ALTER TABLE Receta
ADD CONSTRAINT FK_Plato_Receta
FOREIGN KEY (Plato_ID)
REFERENCES Plato(Plato_ID)
ON DELETE CASCADE;
-- Agregar una clave externa con acción ON DELETE CASCADE
ALTER TABLE Menu_Plato
ADD CONSTRAINT FK_Plato_MenuPlato
FOREIGN KEY (Plato_ID)
REFERENCES Plato(Plato_ID)
ON DELETE CASCADE;
-- Agregar una clave externa con acción ON DELETE CASCADE
ALTER TABLE Receta_Ingredientes
ADD CONSTRAINT FK_Receta_Ingredientes
FOREIGN KEY (Receta_ID)
REFERENCES Receta(Receta_ID)
ON DELETE CASCADE;





EXEC Eliminar_Receta @Receta_ID = 1;

select * from Receta

