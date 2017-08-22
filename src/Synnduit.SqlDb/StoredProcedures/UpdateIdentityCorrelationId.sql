CREATE PROCEDURE [dbo].[UpdateIdentityCorrelationId]
	@operationId UNIQUEIDENTIFIER,
	@entityTypeId UNIQUEIDENTIFIER,
	@sourceSystemId UNIQUEIDENTIFIER,
	@sourceSystemEntityId NVARCHAR(512)
AS
BEGIN
	UPDATE [dbo].[SourceSystemEntityIdentity]
		SET [LastAccessCorrelationId] = @operationId
		WHERE [EntityTypeId] = @entityTypeId
			AND [SourceSystemId] = @sourceSystemId
			AND [SourceSystemEntityId] = @sourceSystemEntityId
END
