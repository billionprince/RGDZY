CREATE TABLE [dbo].[PrinterLog]
(
    [PrintTime] VARCHAR(100) NULL, 
    [UserName] VARCHAR(50) NULL, 
    [FileName] VARCHAR(100) NULL, 
    [Id] INT NOT NULL IDENTITY, 
    CONSTRAINT [PK_PrinterLog] PRIMARY KEY ([Id])
)