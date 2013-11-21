CREATE TABLE [dbo].[UserEvent]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NULL, 
    [EventId] INT NULL, 
    CONSTRAINT [FK_UserEvent_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]), 
    CONSTRAINT [FK_UserEvent_Event] FOREIGN KEY ([EventId]) REFERENCES [Event]([Id])
)
