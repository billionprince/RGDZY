CREATE TABLE [dbo].[Message]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Content] TEXT NULL, 
    [Timestamp] DATETIME NULL, 
    [From] INT NULL, 
    [To] INT NULL, 
    [HaveRead] INT NULL, 
    CONSTRAINT [FK_Message_FromUser] FOREIGN KEY ([From]) REFERENCES [User]([Id]), 
    CONSTRAINT [FK_Message_ToUser] FOREIGN KEY ([To]) REFERENCES [User]([Id])
)
