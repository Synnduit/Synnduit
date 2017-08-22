CREATE TABLE [dbo].[SharedSourceSystemIdentifier]
(
	[EntityTypeId] UNIQUEIDENTIFIER NOT NULL,
	[SourceSystemId] UNIQUEIDENTIFIER NOT NULL,
	[GroupNumber] INT NOT NULL,
	CONSTRAINT [PK_SharedSourceSystemIdentifier]
		PRIMARY KEY NONCLUSTERED ([EntityTypeId], [SourceSystemId]),
	CONSTRAINT [FK_SharedSourceSystemIdentifier_EntityType]
		FOREIGN KEY ([EntityTypeId])
		REFERENCES [dbo].[EntityType] ([Id]),
	CONSTRAINT [FK_SharedSourceSystemIdentifier_SourceSystem]
		FOREIGN KEY ([SourceSystemId])
		REFERENCES [dbo].[ExternalSystem] ([Id])
)
