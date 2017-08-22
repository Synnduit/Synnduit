CREATE TABLE [dbo].[OperationSourceSystemEntity]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[SerializedEntityId] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT [PK_OperationSourceSystemEntity] PRIMARY KEY NONCLUSTERED ([Id]),
	CONSTRAINT [FK_OperationSourceSystemEntity_Operation]
		FOREIGN KEY ([Id])
		REFERENCES [dbo].[Operation] ([Id]),
	CONSTRAINT [FK_OperationSourceSystemEntity_SerializedEntity]
		FOREIGN KEY ([SerializedEntityId])
		REFERENCES [dbo].[SerializedEntity] ([Id])
)
