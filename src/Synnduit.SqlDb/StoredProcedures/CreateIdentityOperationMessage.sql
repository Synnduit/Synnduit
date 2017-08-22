CREATE PROCEDURE [dbo].[CreateIdentityOperationMessage]
	@operationId UNIQUEIDENTIFIER,
	@timeStamp DATETIMEOFFSET(7),
	@entityTypeId UNIQUEIDENTIFIER,
	@sourceSystemId UNIQUEIDENTIFIER,
	@sourceSystemEntityId NVARCHAR(512),
	@type INT,
	@textHash VARCHAR(24),
	@text NVARCHAR(MAX)
AS
BEGIN
	DECLARE @sourceSystemEntityIdentityId UNIQUEIDENTIFIER
	DECLARE @lastAccessCorrelationId UNIQUEIDENTIFIER
	EXECUTE [dbo].[EnsureSourceSystemEntityIdentityExists]
		@entityTypeId,
		@sourceSystemId,
		@sourceSystemEntityId,
		@operationId,
		@sourceSystemEntityIdentityId OUTPUT,
		@lastAccessCorrelationId OUTPUT

	DECLARE @messageId UNIQUEIDENTIFIER
	EXECUTE [dbo].[EnsureMessageExists]
		@type,
		@textHash,
		@text,
		@messageId OUTPUT

	EXECUTE [dbo].[CreateIdentityMessage]
		@operationId,
		@timeStamp,
		@sourceSystemEntityIdentityId,
		@lastAccessCorrelationId,
		@messageId

	IF ([dbo].[OperationExists] (@operationId) = 1)
	BEGIN
		EXECUTE [dbo].[InsertOperationMessageRow] @operationId, @messageId
	END
END
