CREATE PROCEDURE Plato_Receta_Ingredientes_Listar
    @Plato_ID INT
AS
BEGIN
    -- Seleccionar receta e ingredientes para un plato específico
    SELECT 
        p.Plato_ID,
        p.Plato_Nombre,
        p.Plato_Descripcion,
        p.Plato_Foto,
        p.Plato_Dificultad,
        p.Plato_Precio,

        r.Receta_ID,
        r.Receta_Preparacion,

        i.Ingrediente_ID,
        i.Ingrediente_Nombre,
        i.Ingrediente_Unidad_Medida,

        ri.REC_ING_Cantidad_Requerida

    FROM Plato p
    LEFT JOIN Receta r ON p.Plato_ID = r.Plato_ID
    LEFT JOIN Receta_Ingredientes ri ON r.Receta_ID = ri.Receta_ID
    LEFT JOIN Ingredientes i ON ri.Ingrediente_ID = i.Ingrediente_ID
    WHERE p.Plato_ID = @Plato_ID
END
GO