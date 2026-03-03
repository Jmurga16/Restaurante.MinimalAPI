--OBTENER INGREDIENTE POR ID
CREATE PROCEDURE [dbo].[Ingredientes_Obtener_Por_Id] 
(
    @Id INT
) 
AS
BEGIN
    SELECT * FROM Ingredientes
    WHERE Ingrediente_ID = @Id
END