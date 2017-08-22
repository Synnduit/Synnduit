CREATE PROCEDURE [dbo].[CreateEntityDeletion]
	@operationId UNIQUEIDENTIFIER,
	@timeStamp DATETIMEOFFSET(7),
	@entityTypeId UNIQUEIDENTIFIER,
	@destinationSystemEntityId NVARCHAR(512),
	@outcome INT
AS
BEGIN
	EXECUTE [dbo].[EnsureOperationExists] @operationId, @timeStamp

	INSERT INTO [dbo].[EntityDeletion] VALUES(
		@operationId,
		@entityTypeId,
		@destinationSystemEntityId,
		@outcome)
END
