CREATE TABLE [dbo].[Award]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserName] NVARCHAR(50) NULL, 
    [Name] NVARCHAR(50) NULL, 
    [Year] INT NULL, 
    [Time] DATETIME NULL, 
    CONSTRAINT [FK_Award_User] FOREIGN KEY ([UserName]) REFERENCES [User]([Name])
)
