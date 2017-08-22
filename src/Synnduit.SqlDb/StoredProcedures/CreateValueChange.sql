CREATE PROCEDURE [dbo].[CreateValueChange]
	@id UNIQUEIDENTIFIER,
	@mappingEntityTransactionId UNIQUEIDENTIFIER,
	@valueName VARCHAR(256),
	@previousValue NVARCHAR(MAX),
	@newValue NVARCHAR(MAX)
AS
BEGIN
	INSERT INTO [dbo].[ValueChange] VALUES(
		@id,
		@mappingEntityTransactionId,
		@valueName,
		@previousValue,
		@newValue)
END
