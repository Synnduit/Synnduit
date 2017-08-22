CREATE PROCEDURE [dbo].[EnsureMessageExists]
	@type INT,
	@textHash VARCHAR(24),
	@text NVARCHAR(MAX),
	@id UNIQUEIDENTIFIER OUTPUT
AS
BEGIN
	SELECT @id = [Id]
		FROM [dbo].[Message]
		WHERE [Type] = @type
			AND [TextHash] = @textHash
			AND [Text] = @text
	IF (@id IS NULL)
	BEGIN
		SET @id = NEWID()
		INSERT INTO [dbo].[Message] VALUES(@id, @type, @textHash, @text)
	END
END
