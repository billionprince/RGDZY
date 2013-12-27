CREATE TABLE [dbo].[PrintFile]
(
	[id] INT NOT NULL IDENTITY, 
	[username] varchar(50) NOT NULL, 
    [time] varchar(50) NULL, 
    [filename] nvarchar(MAX) NULL, 
    [single] int NULL, 
	PRIMARY KEY ([id], [username])
)
