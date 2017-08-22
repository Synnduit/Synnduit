CREATE FUNCTION [dbo].[OperationExists]
(
	@id UNIQUEIDENTIFIER
)
RETURNS BIT
AS
BEGIN
	DECLARE @operationExists BIT
	DECLARE @count INT
	SELECT @count = COUNT(*) FROM [dbo].[Operation] WHERE [Id] = @id
	IF (@count > 0)
	BEGIN
		SET @operationExists = 1
	END
	ELSE
	BEGIN
		SET @operationExists = 0
	END
	RETURN @operationExists
END
