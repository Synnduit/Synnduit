CREATE PROCEDURE [dbo].[CreateIdentityOperationSourceSystemEntity]
	@operationId UNIQUEIDENTIFIER,
	@timeStamp DATETIMEOFFSET(7),
	@entityTypeId UNIQUEIDENTIFIER,
	@sourceSystemId UNIQUEIDENTIFIER,
	@sourceSystemEntityId NVARCHAR(512),
	@entityDataHash VARCHAR(24),
	@entityData VARBINARY(MAX),
	@entityLabel NVARCHAR(256)
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
	EXECUTE [dbo].[EnsureSerializedEntityExists]
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

	IF ([dbo].[OperationExists] (@operationId) = 1)
	BEGIN
		EXECUTE [dbo].[InsertOperationSourceSystemEntityRow]
			@operationId, @serializedEntityId
	END
END
