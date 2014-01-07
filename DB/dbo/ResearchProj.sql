CREATE TABLE [dbo].[ResearchProj]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[UserName] NVARCHAR(50) NULL, 
    [Name] NVARCHAR(50) NULL, 
    [Url] NVARCHAR(MAX) NULL, 
    [Time] DATETIME NULL, 
    [Introduction] TEXT NULL, 
    CONSTRAINT [FK_ResearchProj_User] FOREIGN KEY ([UserName]) REFERENCES [User]([Name])
)
