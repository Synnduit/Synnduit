CREATE VIEW [dbo].[MappedEntityIdentifier]
AS
SELECT m.[Id],
		ssei.[EntityTypeId],
		ssei.[SourceSystemId],
		ssei.[SourceSystemEntityId],
		m.[DestinationSystemEntityId],
		m.[Origin],
		m.[State]
	FROM [dbo].[Mapping] m
		INNER JOIN [dbo].[SourceSystemEntityIdentity] ssei
			ON m.[SourceSystemEntityIdentityId] = ssei.[Id]
