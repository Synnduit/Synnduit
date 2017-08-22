CREATE PROCEDURE [dbo].[CreateOrUpdateExternalSystem]
	@id UNIQUEIDENTIFIER,
	@name VARCHAR(128)
AS
	DECLARE @externalSystemCount INT
BEGIN
	SELECT @externalSystemCount = COUNT(*)
		FROM [dbo].[ExternalSystem]
		WHERE [Id] = @id
	IF (@externalSystemCount > 0)
	BEGIN
		UPDATE [dbo].[ExternalSystem]
			SET [Name] = @name
			WHERE [Id] = @id
	END
	ELSE
	BEGIN
		INSERT INTO [dbo].[ExternalSystem]
			([Id], [Name])
			VALUES(@id, @name)
	END
END
