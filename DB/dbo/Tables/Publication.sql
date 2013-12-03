CREATE TABLE [dbo].[Publication]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserName] NVARCHAR(50) NULL, 
    [PaperName] NVARCHAR(50) NULL, 
    [Conference] NVARCHAR(50) NULL, 
    [Year] INT NULL, 
    CONSTRAINT [FK_Publication_User] FOREIGN KEY ([UserName]) REFERENCES [User]([Name])
)
