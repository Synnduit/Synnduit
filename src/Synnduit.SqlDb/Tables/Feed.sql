CREATE TABLE [dbo].[Feed]
(
	[EntityTypeId] UNIQUEIDENTIFIER NOT NULL,
	[SourceSystemId] UNIQUEIDENTIFIER NOT NULL,
	[FeedTypeFullName] VARCHAR(256) NOT NULL,
	CONSTRAINT [PK_Feed] PRIMARY KEY NONCLUSTERED ([EntityTypeId], [SourceSystemId]),
	CONSTRAINT [FK_Feed_EntityType]
		FOREIGN KEY ([EntityTypeId])
		REFERENCES [dbo].[EntityType] ([Id]),
	CONSTRAINT [FK_Feed_SourceSystem]
		FOREIGN KEY ([SourceSystemId])
		REFERENCES [dbo].[ExternalSystem] ([Id])
)
