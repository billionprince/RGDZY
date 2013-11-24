CREATE TABLE [dbo].[Project]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(MAX) NOT NULL, 
    [Advisor] INT NULL, 
    [Description] TEXT NULL, 
    CONSTRAINT [FK_Project_User] FOREIGN KEY ([Advisor]) REFERENCES [User]([Id])
)
