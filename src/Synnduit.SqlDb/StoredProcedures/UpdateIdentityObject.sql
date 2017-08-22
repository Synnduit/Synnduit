CREATE PROCEDURE [dbo].[UpdateIdentityObject]
	@identityObjectId UNIQUEIDENTIFIER,
	@operationId UNIQUEIDENTIFIER,
	@timeStamp DATETIMEOFFSET(7)
AS
BEGIN
	UPDATE [dbo].[IdentityObject]
		SET [LastObserved] = @timeStamp,
			[OccurrenceCount] = [OccurrenceCount] + 1,
			[LastAccessCorrelationId] = @operationId
		WHERE [Id] = @identityObjectId
END
