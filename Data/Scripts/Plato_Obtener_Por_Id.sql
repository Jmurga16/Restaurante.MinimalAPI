--OBTENER PLATO POR ID
CREATE PROCEDURE [dbo].[Plato_Obtener_Por_Id] 
(
    @Id INT
) 
AS
BEGIN
    SELECT * FROM Plato
    WHERE Plato_ID = @Id
END