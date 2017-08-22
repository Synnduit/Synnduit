CREATE TABLE [dbo].[EntityTransaction]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[Outcome] INT NOT NULL,
	CONSTRAINT [PK_EntityTransaction] PRIMARY KEY NONCLUSTERED ([Id]),
	CONSTRAINT [FK_EntityTransaction_Operation]
		FOREIGN KEY ([Id])
		REFERENCES [dbo].[Operation] ([Id])
)
