CREATE TABLE [dbo].[UserProject]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserName] NVARCHAR(50) NULL, 
    [ProjectId] INT NULL, 
    CONSTRAINT [FK_UserProject_User] FOREIGN KEY ([UserName]) REFERENCES [User]([Name]), 
    CONSTRAINT [FK_UserProject_Project] FOREIGN KEY ([ProjectId]) REFERENCES [Project]([Id])
)
