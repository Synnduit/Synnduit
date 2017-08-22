CREATE VIEW [dbo].[EntityValueChange]
AS
SELECT ds.[Name] AS [DestinationSystem],
		et.[Name] AS [EntityType],
		m.[DestinationSystemEntityId],
		ss.[Name] AS [SourceSystem],
		ssei.[SourceSystemEntityId],
		o.[TimeStamp],
		trn.[Outcome],
		vc.[ValueName],
		vc.[PreviousValue],
		vc.[NewValue]
	FROM [dbo].[EntityTransaction] trn
		INNER JOIN [dbo].[Operation] o ON trn.[Id] = o.[Id]
		INNER JOIN [dbo].[MappingEntityTransaction] met ON trn.[Id] = met.[Id]
		INNER JOIN [dbo].[Mapping] m ON met.[MappingId] = m.[Id]
		INNER JOIN [dbo].[SourceSystemEntityIdentity] ssei
			ON m.[SourceSystemEntityIdentityId] = ssei.[Id]
		INNER JOIN [dbo].[EntityType] et ON ssei.[EntityTypeId] = et.[Id]
		INNER JOIN [dbo].[ExternalSystem] ds ON et.[DestinationSystemId] = ds.[Id]
		INNER JOIN [dbo].[ExternalSystem] ss ON ssei.[SourceSystemId] = ss.[Id]
		INNER JOIN [dbo].[ValueChange] vc ON met.[Id] = vc.[MappingEntityTransactionId]
