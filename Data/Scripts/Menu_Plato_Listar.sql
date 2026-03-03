--OBTENER CARTA
CREATE PROCEDURE Menu_Plato_Listar
AS
BEGIN
    SELECT 
		m.Menu_ID,
		m.Menu_Nombre,
		m.Menu_Descripcion,

		p.Plato_ID,
		p.Plato_Nombre,
		p.Plato_Descripcion,
		p.Plato_Foto,
		p.Plato_Dificultad,
		p.Plato_Precio,
		p.Categoria_ID
    FROM [dbo].[Menu_Plato] mp
    INNER JOIN Menu  m ON mp.Menu_ID  = m.Menu_ID
	INNER JOIN Plato p ON mp.Plato_ID = p.Plato_ID
END
GO