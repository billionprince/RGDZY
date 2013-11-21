CREATE TABLE [dbo].[Device]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Type] NVARCHAR(MAX) NULL, 
    [DeviceId] NVARCHAR(MAX) NULL, 
    [Owner] INT NULL, 
    CONSTRAINT [FK_Device_User] FOREIGN KEY ([Owner]) REFERENCES [User]([Id]) 
)
