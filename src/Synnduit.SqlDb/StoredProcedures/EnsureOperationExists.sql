CREATE PROCEDURE [dbo].[EnsureOperationExists]
	@id UNIQUEIDENTIFIER,
	@timeStamp DATETIMEOFFSET(7)
AS
BEGIN
	IF (NOT EXISTS(SELECT [Id] FROM [dbo].[Operation] WHERE [Id] = @id))
	BEGIN
		INSERT INTO [dbo].[Operation] VALUES(@id, @timeStamp)
	END
END
