CREATE TABLE [dbo].[UserProject]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NULL, 
    [ProjectId] INT NULL, 
    CONSTRAINT [FK_UserProject_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]), 
    CONSTRAINT [FK_UserProject_Project] FOREIGN KEY ([ProjectId]) REFERENCES [Project]([Id])
)
