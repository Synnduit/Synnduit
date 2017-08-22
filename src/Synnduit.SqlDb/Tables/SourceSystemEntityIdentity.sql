CREATE TABLE [dbo].[SourceSystemEntityIdentity]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[EntityTypeId] UNIQUEIDENTIFIER NOT NULL,
	[SourceSystemId] UNIQUEIDENTIFIER NOT NULL,
	[SourceSystemEntityId] NVARCHAR(512) NOT NULL,
	[LastAccessCorrelationId] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT [PK_SourceSystemEntityIdentity] PRIMARY KEY NONCLUSTERED ([Id]),
	CONSTRAINT [FK_SourceSystemEntityIdentity_EntityType]
		FOREIGN KEY ([EntityTypeId])
		REFERENCES [dbo].[EntityType] ([Id]),
	CONSTRAINT [FK_SourceSystemEntityIdentity_SourceSystem]
		FOREIGN KEY ([SourceSystemId])
		REFERENCES [dbo].[ExternalSystem] ([Id]),
	CONSTRAINT [UNQ_EntityTypeSourceSystemEntityId]
		UNIQUE ([EntityTypeId], [SourceSystemId], [SourceSystemEntityId])
)
