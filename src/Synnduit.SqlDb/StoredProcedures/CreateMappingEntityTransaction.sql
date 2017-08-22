CREATE PROCEDURE [dbo].[CreateMappingEntityTransaction]
	@operationId UNIQUEIDENTIFIER,
	@timeStamp DATETIMEOFFSET(7),
	@outcome INT,
	@mappingId UNIQUEIDENTIFIER
AS
BEGIN
	EXECUTE [dbo].[CreateEntityTransaction] @operationId, @timeStamp, @outcome
	INSERT INTO [dbo].[MappingEntityTransaction] VALUES(@operationId, @mappingId)
END
