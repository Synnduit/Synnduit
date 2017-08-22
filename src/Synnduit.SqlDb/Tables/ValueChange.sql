CREATE TABLE [dbo].[ValueChange]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[MappingEntityTransactionId] UNIQUEIDENTIFIER NOT NULL,
	[ValueName] VARCHAR(256) NOT NULL,
	[PreviousValue] NVARCHAR(MAX),
	[NewValue] NVARCHAR(MAX),
	CONSTRAINT [PK_ValueChange] PRIMARY KEY NONCLUSTERED ([Id]),
	CONSTRAINT [FK_ValueChange_EntityTransaction]
		FOREIGN KEY ([MappingEntityTransactionId])
		REFERENCES [dbo].[MappingEntityTransaction] ([Id])
)
