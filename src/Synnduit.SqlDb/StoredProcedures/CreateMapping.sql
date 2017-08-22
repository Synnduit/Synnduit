CREATE PROCEDURE [dbo].[CreateMapping]
	@id UNIQUEIDENTIFIER,
	@operationId UNIQUEIDENTIFIER,
	@timeStamp DATETIMEOFFSET(7),
	@entityDataHash VARCHAR(24),
	@entityData VARBINARY(MAX),
	@entityLabel NVARCHAR(256),
	@entityTypeId UNIQUEIDENTIFIER,
	@sourceSystemId UNIQUEIDENTIFIER,
	@sourceSystemEntityId NVARCHAR(512),
	@destinationSystemEntityId NVARCHAR(512),
	@origin INT,
	@state INT
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

	INSERT INTO [dbo].[Mapping] VALUES(
		@id,
		@sourceSystemEntityIdentityId,
		@destinationSystemEntityId,
		@operationId,
		@origin,
		@state)
END
