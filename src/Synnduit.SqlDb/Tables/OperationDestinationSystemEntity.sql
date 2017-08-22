CREATE TABLE [dbo].[OperationDestinationSystemEntity]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[SerializedEntityId] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT [PK_OperationDestinationSystemEntity]
		PRIMARY KEY NONCLUSTERED ([Id]),
	CONSTRAINT [FK_OperationDestinationSystemEntity_Operation]
		FOREIGN KEY ([Id])
		REFERENCES [dbo].[Operation] ([Id]),
	CONSTRAINT [FK_OperationDestinationSystemEntity_SerializedEntity]
		FOREIGN KEY ([SerializedEntityId])
		REFERENCES [dbo].[SerializedEntity] ([Id])
)
