CREATE PROCEDURE [dbo].[CreateOperationSourceSystemEntity]
	@operationId UNIQUEIDENTIFIER,
	@timeStamp DATETIMEOFFSET(7),
	@entityDataHash VARCHAR(24),
	@entityData VARBINARY(MAX),
	@entityLabel NVARCHAR(256),
	@serializedEntityId UNIQUEIDENTIFIER OUTPUT
AS
BEGIN
	EXECUTE [dbo].[EnsureOperationExists] @operationId, @timeStamp

	EXECUTE [dbo].[EnsureSerializedEntityExists]
		@entityDataHash,
		@entityData,
		@entityLabel,
		@serializedEntityId OUTPUT

	EXECUTE [dbo].[InsertOperationSourceSystemEntityRow]
		@operationId, @serializedEntityId
END
