CREATE TABLE [dbo].[EntityType]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[DestinationSystemId] UNIQUEIDENTIFIER NOT NULL,
	[Name] VARCHAR(128) NOT NULL,
	[TypeName] VARCHAR(1024) NOT NULL,
	[SinkTypeFullName] VARCHAR(256) NOT NULL,
	[CacheFeedTypeFullName] VARCHAR(256),
	[IsMutable] BIT NOT NULL,
	[IsDuplicable] BIT NOT NULL
	CONSTRAINT [PK_EntityType] PRIMARY KEY NONCLUSTERED ([Id]),
	CONSTRAINT [FK_EntityType_DestinationSystem]
		FOREIGN KEY ([DestinationSystemId])
		REFERENCES [dbo].[ExternalSystem] ([Id]),
	CONSTRAINT [EntityType_Unique_Name] UNIQUE ([Name])
)
