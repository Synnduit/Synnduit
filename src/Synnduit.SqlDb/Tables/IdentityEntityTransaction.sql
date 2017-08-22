CREATE TABLE [dbo].[IdentityEntityTransaction]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[SourceSystemEntityIdentityId] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT [PK_IdentityEntityTransaction] PRIMARY KEY NONCLUSTERED ([Id]),
	CONSTRAINT [FK_IdentityEntityTransaction_EntityTransaction]
		FOREIGN KEY ([Id])
		REFERENCES [dbo].[EntityTransaction] ([Id]),
	CONSTRAINT [FK_IdentityEntityTransaction_SourceSystemEntityIdentity]
		FOREIGN KEY ([SourceSystemEntityIdentityId])
		REFERENCES [dbo].[SourceSystemEntityIdentity] ([Id])
)
