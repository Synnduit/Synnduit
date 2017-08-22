CREATE VIEW [dbo].[MappingEntity]
AS
SELECT m.[Id] AS [MappingId],
		se.[Data] AS [EntityData] 
	FROM [dbo].[Mapping] m
		INNER JOIN [dbo].[OperationSourceSystemEntity] osse
			ON m.[CurrentOperationSourceSystemEntityId] = osse.[Id]
		INNER JOIN [dbo].[SerializedEntity] se
			ON osse.[SerializedEntityId] = se.[Id]
