CREATE VIEW [dbo].[SharedIdentifierSourceSystem]
AS
SELECT parent.[SourceSystemId],
		parent.[EntityTypeId],
		child.[SourceSystemId] AS [SharedIdentifierSourceSystemId]
	FROM [dbo].[SharedSourceSystemIdentifier] parent,
			[dbo].[SharedSourceSystemIdentifier] child
	WHERE parent.[EntityTypeId] = child.[EntityTypeId]
		AND parent.[GroupNumber] = child.[GroupNumber]
		AND parent.[SourceSystemId] <> child.[SourceSystemId]
