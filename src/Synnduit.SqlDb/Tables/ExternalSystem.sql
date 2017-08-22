CREATE TABLE [dbo].[ExternalSystem]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[Name] VARCHAR(128) NOT NULL,
	CONSTRAINT [PK_ExternalSystem] PRIMARY KEY NONCLUSTERED ([Id]),
	CONSTRAINT [ExternalSystem_Unique_Name] UNIQUE ([Name])
)
