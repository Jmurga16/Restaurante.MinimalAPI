--OBTENER CATEGORIA POR ID
CREATE PROCEDURE [dbo].[Categoria_Obtener_Por_Id] 
(
    @Id INT
) 
AS
BEGIN
    SELECT * FROM Categoria
    WHERE Categoria_ID = @Id
END