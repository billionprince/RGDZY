CREATE TABLE [dbo].[UserProject]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserName] NVARCHAR(50) NULL, 
    [ProjectId] INT NULL, 
)
