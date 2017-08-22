CREATE PROCEDURE [dbo].[CreateOperationDestinationSystemEntity]
	@operationId UNIQUEIDENTIFIER,
	@timeStamp DATETIMEOFFSET(7),
	@entityDataHash VARCHAR(24),
	@entityData VARBINARY(MAX),
	@entityLabel NVARCHAR(256)
AS
BEGIN
	EXECUTE [dbo].[EnsureOperationExists] @operationId, @timeStamp

	DECLARE @serializedEntityId UNIQUEIDENTIFIER
	EXECUTE [dbo].[EnsureSerializedEntityExists]
		@entityDataHash,
		@entityData,
		@entityLabel,
		@serializedEntityId OUTPUT

	-- insert the record, unless the very same operation/serialized entity combination
	-- exists already; this check ensures that the procedure is idempotent, provided
	-- you are trying the associate the operation with the same entity it is associated
	-- with already; any attempt to associate it with a different entity (i.e., two
	-- different entities, in effect) will still fail (it will result in a primary key
	-- UNIQUE constraint violation)
	DECLARE @count INT
	SELECT @count = COUNT(*)
		FROM [dbo].[OperationDestinationSystemEntity]
		WHERE [Id] = @operationId
			AND [SerializedEntityId] = @serializedEntityId
	IF (@count = 0)
	BEGIN
		INSERT INTO [dbo].[OperationDestinationSystemEntity]
			VALUES(@operationId, @serializedEntityId)
	END
END
