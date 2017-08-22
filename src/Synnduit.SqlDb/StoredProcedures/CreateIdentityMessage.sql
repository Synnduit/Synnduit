CREATE PROCEDURE [dbo].[CreateIdentityMessage]
	@operationId UNIQUEIDENTIFIER,
	@timeStamp DATETIMEOFFSET(7),
	@sourceSystemEntityIdentityId UNIQUEIDENTIFIER,
	@lastAccessCorrelationId UNIQUEIDENTIFIER,
	@messageId UNIQUEIDENTIFIER
AS
BEGIN
	DECLARE @identityObjectId UNIQUEIDENTIFIER
	SELECT @identityObjectId = iobj.[Id]
		FROM [dbo].[IdentityObject] iobj
			INNER JOIN [dbo].[IdentityMessage] im ON iobj.[Id] = im.[Id]
		WHERE iobj.[SourceSystemEntityIdentityId] = @sourceSystemEntityIdentityId
			AND im.[MessageId] = @messageId
			AND iobj.[LastAccessCorrelationId] = @lastAccessCorrelationId
	IF (@identityObjectId IS NOT NULL)
	BEGIN
		EXECUTE [dbo].[UpdateIdentityObject]
			@identityObjectId, @operationId, @timeStamp
	END
	ELSE
	BEGIN
		EXECUTE [dbo].[InsertIdentityObjectRow]
			@operationId,
			@timeStamp,
			@sourcesystemEntityIdentityId,
			@identityObjectId OUTPUT
		INSERT INTO [dbo].[IdentityMessage] VALUES(@identityObjectId, @messageId)
	END
END
