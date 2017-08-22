CREATE TABLE [dbo].[IdentityObject]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[SourceSystemEntityIdentityId] UNIQUEIDENTIFIER NOT NULL,
	[FirstObserved] DATETIMEOFFSET(7) NOT NULL,
	[LastObserved] DATETIMEOFFSET(7) NOT NULL,
	[OccurrenceCount] INT NOT NULL,
	[LastAccessCorrelationId] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT [PK_IdentityObject] PRIMARY KEY NONCLUSTERED ([Id]),
	CONSTRAINT [FK_IdentityObject_SourceSystemEntityIdentity]
		FOREIGN KEY ([SourceSystemEntityIdentityId])
		REFERENCES [dbo].[SourceSystemEntityIdentity] ([Id])
)
