CREATE TABLE [dbo].[MappingEntityTransaction]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[MappingId] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT [PK_MappingEntityTransaction] PRIMARY KEY NONCLUSTERED ([Id]),
	CONSTRAINT [FK_MappingEntityTransaction_EntityTransaction]
		FOREIGN KEY ([Id])
		REFERENCES [dbo].[EntityTransaction] ([Id]),
	CONSTRAINT [FK_MappingEntityTransaction_Mapping]
		FOREIGN KEY ([MappingId])
		REFERENCES [dbo].[Mapping] ([Id])
)
