CREATE PROCEDURE [dbo].[CreateOrUpdateEntityType]
	@id UNIQUEIDENTIFIER,
	@destinationSystemId UNIQUEIDENTIFIER,
	@name VARCHAR(128),
	@typeName VARCHAR(1024),
	@sinkTypeFullName VARCHAR(256),
	@cacheFeedTypeFullName VARCHAR(256),
	@isMutable BIT,
	@isDuplicable BIT
AS
	DECLARE @entityTypeCount INT
BEGIN
	SELECT @entityTypeCount = COUNT(*)
		FROM [dbo].[EntityType]
		WHERE [Id] = @id
	IF (@entityTypeCount > 0)
	BEGIN
		UPDATE [dbo].[EntityType]
			SET [DestinationSystemId] = @destinationSystemId,
				[Name] = @name,
				[TypeName] = @typeName,
				[SinkTypeFullName] = @sinkTypeFullName,
				[CacheFeedTypeFullName] = @cacheFeedTypeFullName,
				[IsMutable] = @isMutable,
				[IsDuplicable] = @isDuplicable
			WHERE [Id] = @id
	END
	ELSE
	BEGIN
		INSERT INTO [dbo].[EntityType]
			(
				[Id],
				[DestinationSystemId],
				[Name],
				[TypeName],
				[SinkTypeFullName],
				[CacheFeedTypeFullName],
				[IsMutable],
				[IsDuplicable]
			)
			VALUES
			(
				@id,
				@destinationSystemId,
				@name,
				@typeName,
				@sinkTypeFullName,
				@cacheFeedTypeFullName,
				@isMutable,
				@isDuplicable
			)
	END
END
