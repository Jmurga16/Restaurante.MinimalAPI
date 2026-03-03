--OBTENER ENCARGADO POR ID
CREATE PROCEDURE [dbo].[Encargado_Obtener_Por_Id] 
(
    @Id INT
) 
AS
BEGIN
    SELECT * FROM Encargado
    WHERE Encargado_ID = @Id
END