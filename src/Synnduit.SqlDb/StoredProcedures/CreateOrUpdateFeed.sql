CREATE PROCEDURE [dbo].[CreateOrUpdateFeed]
	@entityTypeId UNIQUEIDENTIFIER,
	@sourceSystemId UNIQUEIDENTIFIER,
	@feedTypeFullName VARCHAR(256)
AS
	DECLARE @feedCount INT
BEGIN
	SELECT @feedCount = COUNT(*)
		FROM [dbo].[Feed]
		WHERE [EntityTypeId] = @entityTypeId
			AND [SourceSystemId] = @sourceSystemId
	IF (@feedCount > 0)
	BEGIN
		UPDATE [dbo].[Feed]
			SET [FeedTypeFullName] = @feedTypeFullName
			WHERE [EntityTypeId] = @entityTypeId
				AND [SourceSystemId] = @sourceSystemId
	END
	ELSE
	BEGIN
		INSERT INTO [dbo].[Feed]
			([EntityTypeId], [SourceSystemId], [FeedTypeFullName])
			VALUES(@entityTypeId, @sourceSystemId, @feedTypeFullName)
	END
END
