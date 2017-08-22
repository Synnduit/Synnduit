CREATE TABLE [dbo].[EntityDeletion]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[EntityTypeId] UNIQUEIDENTIFIER NOT NULL,
	[DestinationSystemEntityId] NVARCHAR(512) NOT NULL,
	[Outcome] INT NOT NULL,
	CONSTRAINT [PK_EntityDeletion] PRIMARY KEY NONCLUSTERED ([Id]),
	CONSTRAINT [FK_EntityDeletion_Operation]
		FOREIGN KEY ([Id])
		REFERENCES [dbo].[Operation] ([Id]),
	CONSTRAINT [FK_EntityDeletion_EntityType]
		FOREIGN KEY ([EntityTypeId])
		REFERENCES [dbo].[EntityType] ([Id])
)
