CREATE PROCEDURE [dbo].[SetParameterValue]
	@id UNIQUEIDENTIFIER,
	@value VARCHAR(1024)
AS
BEGIN
	UPDATE [dbo].[Parameter] SET [Value] = @value WHERE [Id] = @id
END
