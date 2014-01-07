CREATE TABLE [dbo].[Milestone]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ProjectId] INT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [Time] DATETIME NULL, 
    [ImagePath] NVARCHAR(MAX) NULL
)
