CREATE TABLE [dbo].[SeminarRecord]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Date] NVARCHAR(10) NULL, 
	[Recorder] NVARCHAR(50) NULL, 
	[Agenda] NVARCHAR(MAX) NULL, 
    [Appendix] NVARCHAR(MAX) NULL, 
    [SeminarId] INT NULL, 
)
