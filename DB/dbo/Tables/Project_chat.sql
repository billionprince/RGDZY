CREATE TABLE [dbo].[Project_chat]
(
	[id] INT NOT NULL IDENTITY, 
	[project_id] INT NOT NULL, 
    [owner] varchar(50) NULL, 
    [chat_content] nvarchar(MAX) NULL, 
    [chat_time] varchar(50) NULL, 
	PRIMARY KEY ([id], [project_id])
)
