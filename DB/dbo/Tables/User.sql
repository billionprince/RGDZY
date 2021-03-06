﻿CREATE TABLE [dbo].[User]
(
    [Name] NVARCHAR(50) NOT NULL, 
    [StudentId] NVARCHAR(MAX) NULL, 
    [Authority] INT NOT NULL DEFAULT 1, 
    [Password] NVARCHAR(50) NOT NULL DEFAULT 123456, 
    [Introduction] TEXT NULL, 
    [Link] NVARCHAR(MAX) NULL, 
    [Hometown] NVARCHAR(50) NULL, 
    [Birthday] DATE NULL, 
    [University] NVARCHAR(50) NULL, 
    [Email] NVARCHAR(50) NULL, 
    [Phone] NVARCHAR(50) NULL, 
    [RealName] NVARCHAR(50) NULL, 
    [FTPUsername] NVARCHAR(50) NULL, 
    [FTPPassword] NVARCHAR(50) NULL, 
    [SVNUsername] NVARCHAR(50) NULL, 
    [SVNPassword] NVARCHAR(50) NULL, 
	[GroupName] NVARCHAR(50) NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([Name])
)
