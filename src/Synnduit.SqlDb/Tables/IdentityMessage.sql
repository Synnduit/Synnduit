CREATE TABLE [dbo].[IdentityMessage]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[MessageId] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT [PK_IdentityMessage] PRIMARY KEY NONCLUSTERED ([Id]),
	CONSTRAINT [FK_IdentityMessage_IdentityObject]
		FOREIGN KEY ([Id])
		REFERENCES [dbo].[IdentityObject] ([Id]),
	CONSTRAINT [FK_IdentityMessage_Message]
		FOREIGN KEY ([MessageId])
		REFERENCES [dbo].[Message] ([Id])
)
