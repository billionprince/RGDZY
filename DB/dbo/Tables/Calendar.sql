CREATE TABLE [dbo].[Calendar]
(
	[Id] UNIQUEIDENTIFIER NOT NULL , 
    [type] INT NOT NULL, 
    [owner] VARCHAR(50) NOT NULL, 
    [title] VARCHAR(50) NOT NULL, 
    [start_time] VARCHAR(50) NULL, 
    [end_time] VARCHAR(50) NULL, 
    [allday] INT NULL, 
    [creator] VARCHAR(50) NULL, 
    [url] VARCHAR(100) NULL, 
    [detail] VARCHAR(100) NULL, 
	[sendemail] varchar(50) NULL,
    PRIMARY KEY ([owner], [Id], [type])
)
