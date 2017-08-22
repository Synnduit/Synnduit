CREATE PROCEDURE [dbo].[CreateSharedSourceSystemIdentifier]
	@entityTypeId UNIQUEIDENTIFIER,
	@sourceSystemId UNIQUEIDENTIFIER,
	@groupNumber INT
AS
BEGIN
	INSERT INTO [dbo].[SharedSourceSystemIdentifier]
		VALUES(@entityTypeId, @sourceSystemId, @groupNumber)
END
