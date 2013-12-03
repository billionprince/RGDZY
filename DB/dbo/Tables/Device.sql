CREATE TABLE [dbo].[Device]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [AssetNum] NVARCHAR(MAX) NULL, 
    [Type] NVARCHAR(MAX) NULL, 
    [Version] NVARCHAR(MAX) NULL, 
    [Cpu] NVARCHAR(MAX) NULL, 
    [Memory] NVARCHAR(MAX) NULL, 
    [Disk] NVARCHAR(MAX) NULL, 
    [PurchaseDate] DATE NULL, 
    [Remark] NVARCHAR(MAX) NULL, 
)
