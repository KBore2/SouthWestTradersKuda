SET XACT_ABORT ON;
BEGIN TRANSACTION;
GO


IF NOT EXISTS (SELECT * FROM sys.databases 
WHERE name = 'ProductsDB')
CREATE DATABASE ProductsDB  
ON   
( NAME =Products_dat,  
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\productsdbdat.mdf',  
    SIZE = 10,  
    MAXSIZE = 50,  
    FILEGROWTH = 5 )  
LOG ON  
( NAME = Products_log,  
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\productsdblog.ldf',  
    SIZE = 5MB,  
    MAXSIZE = 25MB,  
    FILEGROWTH = 5MB );  
GO  

IF NOT EXISTS (SELECT * FROM sys.tables
WHERE name = N'Product' AND type = 'U')
BEGIN
	CREATE TABLE [ProductsDB].[dbo].[Product]
	(
	[ProductId] bigint IDENTITY(1, 1) NOT NULL,
	[Name] nvarchar(150) NOT NULL,
	[Description] nvarchar(250) NOT NULL,
	[Price] decimal NOT NULL,
	CONSTRAINT PK_Product_Id PRIMARY KEY ([ProductId]),
	CONSTRAINT CK_Product_Name CHECK (len([Name])>(0)),
	CONSTRAINT CK_Product_Description CHECK (len([Description])>(0)),
	CONSTRAINT [UQ_IX_Product_Name] UNIQUE NONCLUSTERED 
	(	
		[Name] 
	)
	)
END	
GO

IF NOT EXISTS (SELECT * FROM sys.tables
WHERE name = N'OrderState' AND type = 'U')
BEGIN
	CREATE TABLE [ProductsDB].[dbo].[OrderState]
	(
	[OrderStateId] int NOT NULL,
	[State] nvarchar(150) NOT NULL,
	CONSTRAINT PK_OrderState_Id PRIMARY KEY ([OrderStateId]),
	CONSTRAINT CK_OrderState_State CHECK (len([State])>(0)),
	CONSTRAINT [UQ_OrderState_Name] UNIQUE  
	(	
		[State] 
	)
	)
END	
GO

IF NOT EXISTS (SELECT * FROM sys.tables
WHERE name = N'Order' AND type = 'U')
BEGIN
	CREATE TABLE [ProductsDB].[dbo].[Order]
	(
	[OrderId] bigint IDENTITY(1, 1) NOT NULL,
	[Name] nvarchar(150) NOT NULL,
	[CreatedDateUTC] Datetime2 NOT NULL,
	[Quantity] int NOT NULL,
	[OrderStateId] int not null,
	CONSTRAINT PK_Order_Id PRIMARY KEY ([OrderId]),
	CONSTRAINT CK_Order_Name CHECK (len([Name])>(0)),
	CONSTRAINT [UQ_IX_Order_Name] UNIQUE NONCLUSTERED 
	(	
		[Name] 
	),
	CONSTRAINT [FK_Order_OrderState] FOREIGN KEY
	(	
		[OrderStateId] 
	) REFERENCES [ProductsDB].[dbo].[OrderState] (OrderStateId)
	)
END	
GO

IF NOT EXISTS (SELECT * FROM sys.tables
WHERE name = N'Stock' AND type = 'U')
BEGIN
	CREATE TABLE [ProductsDB].[dbo].[Stock]
	(
	[StockId] bigint IDENTITY(1, 1) NOT NULL,
	[ProductId] bigint NOT NULL,
	[AvailableStock] int NOT NULL,
	CONSTRAINT PK_Stock_Id PRIMARY KEY ([StockId]),
	CONSTRAINT FK_Stock_ProductId FOREIGN KEY   
	(	
	 [ProductId] 
	) REFERENCES [ProductsDB].[dbo].[Product] (ProductId)
	)
END	
GO

DECLARE @OrderState table 
(
  [OrderStateId] int NOT NULL,	
  [State] nvarchar(150) NOT NULL, 	
  PRIMARY KEY CLUSTERED ([OrderStateId])
);

INSERT INTO @OrderState ([OrderStateId], [State])
VALUES
 (1, N'Reserved'), 
 (2, N'Cancelled'), 	 
 (3, N'Completed')

 --delete outdated order states
DELETE [T]
FROM [ProductsDB].[dbo].[OrderState] AS [T]
 LEFT OUTER JOIN @OrderState AS [S]
  ON [S].[OrderStateId] = [T].[OrderStateId]
 WHERE [S].[OrderStateId] IS NULL;

 --upsert Order State
MERGE INTO [ProductsDB].[dbo].[OrderState] AS [T]
USING @OrderState AS [S]
 ON [S].[State] = [T].[State]
  WHEN NOT MATCHED BY TARGET
   THEN INSERT ([OrderStateId], [State])
     VALUES([S].[OrderStateId], [S].[State]);


COMMIT TRANSACTION;
GO
