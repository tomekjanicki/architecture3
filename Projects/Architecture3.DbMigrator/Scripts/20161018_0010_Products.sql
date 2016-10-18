CREATE TABLE [dbo].[Products]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Code] NVARCHAR(50) NOT NULL, 
    [Name] NVARCHAR(100) NOT NULL, 
    [Price] DECIMAL NOT NULL, 
    [Version] ROWVERSION NOT NULL 
)

GO

CREATE UNIQUE INDEX [IX_Products_Name] ON [dbo].[Products] ([Name])