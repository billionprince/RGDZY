CREATE TABLE [dbo].[Event]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(MAX) NULL, 
    [StartTime] TIME NULL, 
    [EndTime] TIME NULL, 
    [Repeat] INT NULL, 
    [StartDate] DATE NULL, 
    [EndDate] DATE NULL
)
