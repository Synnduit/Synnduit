CREATE PROCEDURE [dbo].[CreateIdentitySourceSystemEntity]
	@operationId UNIQUEIDENTIFIER,
	@timeStamp DATETIMEOFFSET(7),
	@sourceSystemEntityIdentityId UNIQUEIDENTIFIER,
	@lastAccessCorrelationId UNIQUEIDENTIFIER,
	@serializedEntityId UNIQUEIDENTIFIER
AS
BEGIN
	DECLARE @identityObjectId UNIQUEIDENTIFIER
	SELECT @identityObjectId = iobj.[Id]
		FROM [dbo].[IdentityObject] iobj
			INNER JOIN [dbo].[IdentitySourceSystemEntity] isse ON iobj.[Id] = isse.[Id]
		WHERE iobj.[SourceSystemEntityIdentityId] = @sourceSystemEntityIdentityId
			AND isse.[SerializedEntityId] = @serializedEntityId
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
			@sourceSystemEntityIdentityId,
			@identityObjectId OUTPUT
		INSERT INTO [dbo].[IdentitySourceSystemEntity]
			VALUES(@identityObjectId, @serializedEntityId)
	END
END
