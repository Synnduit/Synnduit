CREATE PROCEDURE [dbo].[CreateOperationMessage]
	@operationId UNIQUEIDENTIFIER,
	@timeStamp DATETIMEOFFSET(7),
	@type INT,
	@textHash VARCHAR(24),
	@text NVARCHAR(MAX)
AS
BEGIN
	EXECUTE [dbo].[EnsureOperationExists] @operationId, @timeStamp

	DECLARE @messageId UNIQUEIDENTIFIER
	EXECUTE [dbo].[EnsureMessageExists]
		@type,
		@textHash,
		@text,
		@messageId OUTPUT

	EXECUTE [dbo].[InsertOperationMessageRow] @operationId, @messageId
END
