CREATE TABLE [dbo].[Mapping]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[SourceSystemEntityIdentityId] UNIQUEIDENTIFIER NOT NULL,
	[DestinationSystemEntityId] NVARCHAR(512) NOT NULL,
	[CurrentOperationSourceSystemEntityId] UNIQUEIDENTIFIER NOT NULL,
	[Origin] INT NOT NULL,
	[State] INT NOT NULL,
	CONSTRAINT [PK_Mapping] PRIMARY KEY NONCLUSTERED ([Id]),
	CONSTRAINT [FK_Mapping_SourceSystemEntityIdentity]
		FOREIGN KEY ([SourceSystemEntityIdentityId])
		REFERENCES [dbo].[SourceSystemEntityIdentity] ([Id]),
	CONSTRAINT [FK_Mapping_LastUpdateOperationId]
		FOREIGN KEY ([CurrentOperationSourceSystemEntityId])
		REFERENCES [dbo].[OperationSourceSystemEntity] ([Id])
)
