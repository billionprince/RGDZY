CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(MAX) NOT NULL, 
    [StudentId] NVARCHAR(MAX) NOT NULL, 
    [Authority] INT NOT NULL
)
