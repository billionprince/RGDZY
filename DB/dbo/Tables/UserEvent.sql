CREATE TABLE [dbo].[UserEvent]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserName] NVARCHAR(50) NULL, 
    [EventId] INT NULL, 
    CONSTRAINT [FK_UserEvent_User] FOREIGN KEY ([UserName]) REFERENCES [User]([Name]), 
    CONSTRAINT [FK_UserEvent_Event] FOREIGN KEY ([EventId]) REFERENCES [Event]([Id])
)
