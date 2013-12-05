CREATE TABLE [dbo].[UserGroup]
(
	[groupname] NVARCHAR(50) NOT NULL , 
    [username] NVARCHAR(50) NOT NULL, 
    PRIMARY KEY ([groupname], [username])
)
