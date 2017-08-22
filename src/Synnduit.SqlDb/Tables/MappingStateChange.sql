CREATE TABLE [dbo].[MappingStateChange]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[MappingId] UNIQUEIDENTIFIER NOT NULL,
	[PreviousState] CHAR(1) NOT NULL,
	CONSTRAINT [PK_MappingStateChange] PRIMARY KEY NONCLUSTERED ([Id]),
	CONSTRAINT [FK_MappingStateChange_Operation]
		FOREIGN KEY ([Id])
		REFERENCES [dbo].[Operation] ([Id]),
	CONSTRAINT [FK_MappingStateChange_Mapping]
		FOREIGN KEY ([MappingId])
		REFERENCES [dbo].[Mapping] ([Id])
)
