CREATE TABLE [dbo].[Project]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(MAX) NOT NULL, 
    [Advisor] NVARCHAR(50) NULL, 
    [Description] TEXT NULL, 
    CONSTRAINT [FK_Project_User] FOREIGN KEY ([Advisor]) REFERENCES [User]([Name])
)
