﻿CREATE TABLE [dbo].[Seminar]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(MAX) NOT NULL, 
    [Day] NVARCHAR(10) NULL, 
	[BeginTime] NVARCHAR(50) NULL, 
    [EndTime] NVARCHAR(50) NULL, 
    [Participator] NVARCHAR(MAX) NULL, 
    [CalendarId] UNIQUEIDENTIFIER NULL, 
)
