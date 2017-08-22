CREATE PROCEDURE [dbo].[CreateIdentityEntityTransaction]
	@operationId UNIQUEIDENTIFIER,
	@timeStamp DATETIMEOFFSET(7),
	@outcome INT,
	@entityTypeId UNIQUEIDENTIFIER,
	@sourceSystemId UNIQUEIDENTIFIER,
	@sourceSystemEntityId NVARCHAR(512)
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

	EXECUTE [dbo].[CreateEntityTransaction] @operationId, @timeStamp, @outcome
	INSERT INTO [dbo].[IdentityEntityTransaction]
		VALUES(@operationId, @sourceSystemEntityIdentityId)
END
