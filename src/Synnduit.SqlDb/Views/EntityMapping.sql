CREATE VIEW [dbo].[EntityMapping]
AS
SELECT m.[Id],
		ssei.[EntityTypeId],
		ssei.[SourceSystemId],
		ssei.[SourceSystemEntityId],
		et.[DestinationSystemId],
		m.[DestinationSystemEntityId],
		m.[Origin],
		m.[State],
		se.[DataHash] AS [SerializedEntityHash]
	FROM [dbo].[Mapping] m
		INNER JOIN [dbo].[SourceSystemEntityIdentity] ssei
			ON m.[SourceSystemEntityIdentityId] = ssei.[Id]
		INNER JOIN [dbo].[EntityType] et ON ssei.[EntityTypeId] = et.[Id]
		INNER JOIN [dbo].[OperationSourceSystemEntity] osse
			ON m.[CurrentOperationSourceSystemEntityId] = osse.[Id]
		INNER JOIN [dbo].[SerializedEntity] se
			ON osse.[SerializedEntityId] = se.[Id]
