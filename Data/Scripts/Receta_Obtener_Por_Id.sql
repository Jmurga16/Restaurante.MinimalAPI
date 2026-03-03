--OBTENER RECETA POR ID
CREATE PROCEDURE [dbo].[Receta_Obtener_Por_Id] 
(
    @Id INT
) 
AS
BEGIN
    SELECT * FROM Receta
    WHERE Receta_ID = @Id
END