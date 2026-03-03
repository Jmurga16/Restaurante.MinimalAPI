--OBTENER ENCARGADOS CON CATEGORIAS
CREATE PROCEDURE Encargado_Categoria_Listar
AS
BEGIN
    SELECT 
		e.Encargado_ID,
        e.Encargado_Nombre,
        e.Encargado_Apellido,
        e.Encargado_Tipo_Doc,
        e.Encargado_Documento,
        e.Encargado_Mail,
        e.Encargado_Tel,
        e.Encargado_Direccion,

        c.Categoria_ID,
		c.Categoria_Nombre,
        c.Categoria_Descripcion,
        c.Encargado_ID
    FROM Categoria c
    INNER JOIN Encargado e ON c.Encargado_ID = e.Encargado_ID
END
GO
