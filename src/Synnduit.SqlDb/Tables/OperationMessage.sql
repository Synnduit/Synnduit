CREATE TABLE [dbo].[OperationMessage]
(
	[OperationId] UNIQUEIDENTIFIER NOT NULL,
	[MessageId] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT [PK_OperationMessage]
		PRIMARY KEY NONCLUSTERED ([OperationId], [MessageId]),
	CONSTRAINT [PK_OperationMessage_Operation]
		FOREIGN KEY ([OperationId])
		REFERENCES [dbo].[Operation] ([Id]),
	CONSTRAINT [PK_OperationMessage_Message]
		FOREIGN KEY ([MessageId])
		REFERENCES [dbo].[Message] ([Id])
)
