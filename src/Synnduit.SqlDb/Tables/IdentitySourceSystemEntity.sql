CREATE TABLE [dbo].[IdentitySourceSystemEntity]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[SerializedEntityId] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT [PK_IdentitySourceSystemEntity] PRIMARY KEY NONCLUSTERED ([Id]),
	CONSTRAINT [FK_IdentitySourceSystemEntity_IdentityObject]
		FOREIGN KEY ([Id])
		REFERENCES [dbo].[IdentityObject] ([Id]),
	CONSTRAINT [FK_IdentitySourceSystemEntity_SerializedEntity]
		FOREIGN KEY ([SerializedEntityId])
		REFERENCES [dbo].[SerializedEntity] ([Id])
)
