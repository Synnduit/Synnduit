CREATE PROCEDURE [dbo].[EnsureSourceSystemEntityIdentityExists]
	@entityTypeId UNIQUEIDENTIFIER,
	@sourceSystemId UNIQUEIDENTIFIER,
	@sourceSystemEntityId NVARCHAR(512),
	@correlationId UNIQUEIDENTIFIER,
	@id UNIQUEIDENTIFIER OUTPUT,
	@lastAccessCorrelationId UNIQUEIDENTIFIER OUTPUT
AS
BEGIN
	SELECT @id = [Id],
			@lastAccessCorrelationId = [LastAccessCorrelationId]
		FROM [dbo].[SourceSystemEntityIdentity]
		WHERE [EntityTypeId] = @entityTypeId
			AND [SourceSystemId] = @sourceSystemId
			AND [SourceSystemEntityId] = @sourceSystemEntityId
	IF (@id IS NULL)
	BEGIN
		SET @id = NEWID()
		INSERT INTO [dbo].[SourceSystemEntityIdentity] VALUES(
			@id, @entityTypeId, @sourceSystemId, @sourceSystemEntityId, @correlationId)
	END
END
