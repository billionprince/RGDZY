CREATE TABLE [dbo].[Message]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Content] TEXT NULL, 
    [Timestamp] DATETIME NULL, 
    [From] NVARCHAR(50) NULL, 
    [To] NVARCHAR(50) NULL, 
    [HaveRead] INT NULL, 
    CONSTRAINT [FK_Message_FromUser] FOREIGN KEY ([From]) REFERENCES [User]([Name]), 
    CONSTRAINT [FK_Message_ToUser] FOREIGN KEY ([To]) REFERENCES [User]([Name])
)
