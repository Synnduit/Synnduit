CREATE TABLE [dbo].[Parameter]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[DestinationSystemId] UNIQUEIDENTIFIER NULL,
	[EntityTypeId] UNIQUEIDENTIFIER NULL,
	[SourceSystemId] UNIQUEIDENTIFIER NULL,
	[Name] VARCHAR(128) NOT NULL,
	[Value] VARCHAR(1024) NOT NULL,
	CONSTRAINT [PK_Parameter] PRIMARY KEY NONCLUSTERED ([Id]),
	CONSTRAINT [FK_Parameter_DestinationSystem]
		FOREIGN KEY ([DestinationSystemId])
		REFERENCES [dbo].[ExternalSystem] ([Id]),
	CONSTRAINT [FK_Parameter_EntityType]
		FOREIGN KEY ([EntityTypeId])
		REFERENCES [dbo].[EntityType] ([Id]),
	CONSTRAINT [FK_Parameter_SourceSystem]
		FOREIGN KEY ([SourceSystemId])
		REFERENCES [dbo].[ExternalSystem] ([Id])
)
