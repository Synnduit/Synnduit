CREATE PROCEDURE [dbo].[CreateParameter]
	@id UNIQUEIDENTIFIER,
	@destinationSystemId UNIQUEIDENTIFIER,
	@entityTypeId UNIQUEIDENTIFIER,
	@sourceSystemId UNIQUEIDENTIFIER,
	@name VARCHAR(128),
	@value VARCHAR(1024)
AS
BEGIN
	INSERT INTO [dbo].[Parameter] VALUES(
		@id, @destinationSystemId, @entityTypeId, @sourceSystemId, @name, @value)
END
