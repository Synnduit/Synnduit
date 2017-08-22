CREATE PROCEDURE [dbo].[InsertIdentityObjectRow]
	@operationId UNIQUEIDENTIFIER,
	@timeStamp DATETIMEOFFSET(7),
	@sourceSystemEntityIdentityId UNIQUEIDENTIFIER,
	@identityObjectId UNIQUEIDENTIFIER OUTPUT
AS
BEGIN
	SET @identityObjectId = NEWID()
	INSERT INTO [dbo].[IdentityObject] VALUES(
		@identityObjectId,
		@sourceSystemEntityIdentityId,
		@timeStamp,
		@timeStamp,
		1,
		@operationId)
END
