--OBTENER MENU POR ID
CREATE PROCEDURE [dbo].[Menu_Obtener_Por_Id] 
(
    @Id INT
) 
AS
BEGIN
    SELECT * FROM Menu
    WHERE Menu_ID = @Id
END