CREATE PROCEDURE [dbo].[SetMappingState]
	@mappingId UNIQUEIDENTIFIER,
	@operationId UNIQUEIDENTIFIER,
	@timeStamp DATETIMEOFFSET(7),
	@state INT
AS
	DECLARE @previousState CHAR(1)
BEGIN
	EXECUTE [dbo].[EnsureOperationExists] @operationId, @timeStamp

	SELECT @previousState = [State] FROM [dbo].[Mapping] WHERE [Id] = @mappingId
	UPDATE [dbo].[Mapping] SET [State] = @state WHERE [Id] = @mappingId
	INSERT INTO [dbo].[MappingStateChange]
		VALUES(@operationId, @mappingId, @previousState)
END
