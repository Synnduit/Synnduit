CREATE PROCEDURE [dbo].[InsertOperationMessageRow]
	@operationId UNIQUEIDENTIFIER,
	@messageId UNIQUEIDENTIFIER
AS
BEGIN
	DECLARE @count INT
	SELECT @count = COUNT(*)
		FROM [dbo].[OperationMessage]
		WHERE [OperationId] = @operationId AND [MessageId] = @messageId
	IF (@count = 0)
	BEGIN
		INSERT INTO [dbo].[OperationMessage] VALUES(@operationId, @messageId)
	END
END
