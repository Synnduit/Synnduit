CREATE PROCEDURE [dbo].[EnsureSerializedEntityExists]
	@dataHash VARCHAR(24),
	@data VARBINARY(MAX),
	@label NVARCHAR(256),
	@id UNIQUEIDENTIFIER OUTPUT
AS
BEGIN
	SELECT @id = [Id]
		FROM [dbo].[SerializedEntity]
		WHERE [DataHash] = @dataHash AND [Data] = @data AND [Label] = @label
	IF (@id IS NULL)
	BEGIN
		SET @id = NEWID()
		INSERT INTO [dbo].[SerializedEntity] VALUES(@id, @dataHash, @data, @label)
	END
END
