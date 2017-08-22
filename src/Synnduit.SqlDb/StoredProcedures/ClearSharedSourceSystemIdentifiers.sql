CREATE PROCEDURE [dbo].[ClearSharedSourceSystemIdentifiers]
	@entityTypeId UNIQUEIDENTIFIER
AS
BEGIN
	DELETE FROM [dbo].[SharedSourceSystemIdentifier]
		WHERE [EntityTypeId] = @entityTypeId
END
