CREATE TABLE [dbo].[DeviceUse]
(
	[DeviceId] INT NOT NULL PRIMARY KEY, 
    [UserId] NVARCHAR(50) NOT NULL, 
    [StartDate] DATE NULL, 
    [EndDate] DATE NULL, 
    CONSTRAINT [FK_DeviceUse_Device] FOREIGN KEY ([DeviceId]) REFERENCES [Device]([Id]),
	CONSTRAINT [FK_DeviceUse_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id])
)
