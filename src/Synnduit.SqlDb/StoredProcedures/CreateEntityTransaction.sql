CREATE PROCEDURE [dbo].[CreateEntityTransaction]
	@operationId UNIQUEIDENTIFIER,
	@timeStamp DATETIMEOFFSET(7),
	@outcome INT
AS
BEGIN
	EXECUTE [dbo].[EnsureOperationExists] @operationId, @timeStamp
	INSERT INTO [dbo].[EntityTransaction] VALUES(@operationId, @outcome)
END
