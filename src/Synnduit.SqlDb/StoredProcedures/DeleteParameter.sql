CREATE PROCEDURE [dbo].[DeleteParameter]
	@id UNIQUEIDENTIFIER
AS
BEGIN
	DELETE FROM [dbo].[Parameter] WHERE [Id] = @id
END
