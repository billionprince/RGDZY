CREATE TABLE [dbo].[RegisterForm]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Authority] INT NOT NULL DEFAULT 1,
	[Email] NVARCHAR(50) NOT NULL, 
	[Hashcode1] NVARCHAR(50) NOT NULL,
	[Hashcode2] NVARCHAR(50) NOT NULL,
    [Datetime] DATETIME NOT NULL, 
    [Name] NVARCHAR(50) NULL,   
    CONSTRAINT [FK_RegisterForm_User] FOREIGN KEY ([Name]) REFERENCES [User]([Name]), 
)
