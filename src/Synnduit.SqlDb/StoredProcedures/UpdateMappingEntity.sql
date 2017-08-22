CREATE PROCEDURE [dbo].[UpdateMappingEntity]
	@mappingId UNIQUEIDENTIFIER,
	@operationId UNIQUEIDENTIFIER,
	@timeStamp DATETIMEOFFSET(7),
	@entityDataHash VARCHAR(24),
	@entityData VARBINARY(MAX),
	@entityLabel VARCHAR(256)
AS
BEGIN
	DECLARE @sourceSystemEntityIdentityId UNIQUEIDENTIFIER
	DECLARE @lastAccessCorrelationId UNIQUEIDENTIFIER
	SELECT @sourceSystemEntityIdentityId = ssei.[Id],
			@lastAccessCorrelationId = ssei.[LastAccessCorrelationId]
		FROM [dbo].[Mapping] m
			INNER JOIN [dbo].[SourceSystemEntityIdentity] ssei
				ON m.[SourceSystemEntityIdentityId] = ssei.[Id]
		WHERE m.[Id] = @mappingId

	DECLARE @serializedEntityId UNIQUEIDENTIFIER
	EXECUTE [dbo].[CreateOperationSourceSystemEntity]
		@operationId,
		@timeStamp,
		@entityDataHash,
		@entityData,
		@entityLabel,
		@serializedEntityId OUTPUT

	EXECUTE [dbo].[CreateIdentitySourceSystemEntity]
		@operationId,
		@timeStamp,
		@sourceSystemEntityIdentityId,
		@lastAccessCorrelationId,
		@serializedEntityId

	UPDATE [dbo].[Mapping]
		SET [CurrentOperationSourceSystemEntityId] = @operationId
		WHERE [Id] = @mappingId
END
