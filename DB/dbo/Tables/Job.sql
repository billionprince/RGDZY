CREATE TABLE [dbo].[Job]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(MAX) NULL, 
    [Detail] TEXT NULL, 
    [StartTime] DATETIME NULL, 
    [EndTime] DATETIME NULL, 
    [TaskId] INT NULL, 
    [UserName] NVARCHAR(50) NULL, 
    CONSTRAINT [FK_Job_Task] FOREIGN KEY ([TaskId]) REFERENCES [Task]([Id]), 
    CONSTRAINT [FK_Job_User] FOREIGN KEY ([UserName]) REFERENCES [User]([Name]) 
)
