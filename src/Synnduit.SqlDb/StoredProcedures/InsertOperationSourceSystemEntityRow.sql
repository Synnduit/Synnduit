CREATE PROCEDURE [dbo].[InsertOperationSourceSystemEntityRow]
	@operationId UNIQUEIDENTIFIER,
	@serializedEntityId UNIQUEIDENTIFIER
AS
BEGIN
	-- insert the record, unless the very same operation/serialized entity combination
	-- exists already; this check ensures that the procedure is idempotent, provided
	-- you are trying the associate the operation with the same entity it is associated
	-- with already; any attempt to associate it with a different entity (i.e., two
	-- different entities, in effect) will still fail (it will result in a primary key
	-- UNIQUE constraint violation)
	DECLARE @count INT
	SELECT @count = COUNT(*)
		FROM [dbo].[OperationSourceSystemEntity]
		WHERE [Id] = @operationId
			AND [SerializedEntityId] = @serializedEntityId
	IF (@count = 0)
	BEGIN
		INSERT INTO [dbo].[OperationSourceSystemEntity]
			VALUES(@operationId, @serializedEntityId)
	END
END
