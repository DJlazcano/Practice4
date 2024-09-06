CREATE DATABASE Practice4_JM;
go
USE Practice4_JM;
go

---Tables Creation
CREATE TABLE Products(
	ProductId INT PRIMARY KEY IDENTITY(1,1),
	ProductName VARCHAR(200) NOT NULL,
	Description CHAR(255),
	Price MONEY NOT NULL,
	QuantityInStock INT
);


CREATE TABLE Sales(
	SaleId INT PRIMARY KEY IDENTITY(1,1),
	Quantity_Sold INT NOT NULL,
	Sale_Date DATETIME DEFAULT CURRENT_TIMESTAMP,
	Sale_Price MONEY NOT NULL,
	TotalAmount INT NOT NULL,
	CustomerName VARCHAR(255),
	ProductId INT,
	FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);


CREATE TABLE Purchases(
	PurchaseId INT PRIMARY KEY IDENTITY(1,1),
	Quantity_Purchased INT NOT NULL,
	Purchase_Price MONEY NOT NULL,
	Purchase_Date DATETIME DEFAULT CURRENT_TIMESTAMP,
	Total_Cost MONEY,
	ProductId INT,
	FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);

--Insert Values into Tables

--Insert INTO Products
INSERT INTO Products(
	ProductName,
	Description,
	Price,
	QuantityInStock
)
VALUES('Lemon','They are small but sweet',15,100),
	('Mango','Large and no seed',32.5,50),
	('Avocado','Some came damaged',85.5,230),
	('Orange','Very sweet and juicy',12.4,400),
	('Pineapple','Not very good batch',50.5,20),
	('Cherry','Colorful and sweet',25.7,600),
	('Pomegranate','Not many',70.5,8),
	('Blueberry','Very sweet and plenty of them',36.3,200),
	('Cantaloupe','Large and no seed, also very sweet',29.3,36),
	('Coconut','Large and Full of water',43.5,136);

--Insert INTO Sales

INSERT INTO Sales(
	Quantity_Sold,
	Sale_Date,
	Sale_Price,
	TotalAmount,
	CustomerName,
	ProductId
)
VALUES(20,'2024-09-16 8:23:00 AM',15,300,'John Smith',1),
	  (2,'2024-09-20 4:23:00 PM',50.5,101,'Juan Manuel',5),
	  (10,'2024-09-5 9:23:00 AM',43.5,435,'Dany Michel',10),
	  (1,'2024-09-5 5:23:00 PM',70.5,70.5,'Sofia',7),
	  (40,'2024-09-10 11:11:00 AM',25.7,1028,'Lily',6),
	  (50,'2024-09-12 6:23:00 PM',25.7, 1285 ,'Zac',6),
	  (20,'2024-09-15 10:23:00 AM',25.7,514,'Jhonny',6),
	  (62,'2024-09-23 9:34:00 PM',36.3,2250.6,'Harold',9),
	  (82,'2024-09-15 8:23:00 PM',12.4,1016.8,'Sebastian',4),
	  (2,'2024-09-17 7:14:00 AM',12.4,24.8,'Alonso',4);

	  
--Insert INTO Purchases

INSERT INTO Purchases(
	Quantity_Purchased,
	Purchase_Price,
	Purchase_Date,
	Total_Cost,
	ProductId
)VALUES (100,10.7, '2024-09-1 7:00:00 PM', 107,1),
		(50,40.5, '2024-09-17 7:14:00 AM', 2025,3),
		(50,15.3, '2024-09-15 5:14:00 AM', 765,2),
		(200,6.5, '2024-09-25 10:14:00 AM', 1300,4),
		(10,25, '2024-09-01 5:25:00 AM', 250,5),
		(500,12.7, '2024-09-30 6:56:00 PM', 6350,6),
		(8,60.9, '2024-09-13 8:45:00 AM', 487.9,7),
		(200,48.5, '2024-09-17 7:14:00 AM', 9700,3),
		(100,27.2, '2024-09-06 8:36:00 AM', 272,10),
		(200,24.4, '2024-09-30 8:56:00 AM', 4880,8);

--Stored Procedures For CRUDS On Products Table

--Create
CREATE PROCEDURE AddProduct(
	@productName VARCHAR(200),
	@description TEXT,
	@price MONEY,
	@quantityInStock INT
)	
AS
BEGIN
	INSERT INTO Products(ProductName,Description,Price,QuantityInStock)
	VALUES(@productName,@description,@price,@quantityInStock);
END;

EXEC AddProduct @productName = 'Grapes',
				@description = 'Small but sweet',
				@price = 29.1,
				@quantityInStock = 72;
				
SELECT * FROM Products;

--Read 
CREATE PROCEDURE GetProduct(
	@product_id INT)	
AS
BEGIN
	SELECT *
	FROM Products P
	WHERE P.ProductId = @product_id;
END;

EXEC GetProduct @product_id = 5;

--Update
CREATE PROCEDURE UpdateProduct(
	@product_id INT,
	@productName VARCHAR(200),
	@description TEXT,
	@price MONEY,
	@quantityInStock INT
)	
AS
BEGIN
	UPDATE Products
	SET ProductName = COALESCE(@productName, ProductName),
		Description = COALESCE(@description, Description),
		Price = COALESCE(@price, Price),
		QuantityInStock = COALESCE(@quantityInStock, QuantityInStock)
	WHERE ProductId = @product_id;
END;

SELECT * FROM Products;
EXEC UpdateProduct @product_id = 11,
				@productName = 'Grapes',
				@description = NULL,
				@price = NULL,
				@quantityInStock = 30;

SELECT * FROM Products;

---Delete


CREATE PROCEDURE DeleteProduct(
	@product_id INT)	
AS
BEGIN
	DELETE FROM Products
	WHERE ProductId = @product_id;
END;

SELECT * FROM Products;
EXEC DeleteProduct @product_id = 11;
SELECT * FROM Products;

--View to see Sold and Purchased Products
CREATE VIEW SalesAndPurchases AS
	SELECT 
		'Sale' AS 'Transaction',
		S.SaleId AS 'Sale_Id',
		P.ProductName,
		S.Quantity_Sold AS 'Quantity',
		S.Sale_Date,
		S.Sale_Price AS 'Price',
		S.TotalAmount
	FROM Sales S JOIN Products P 
	ON S.ProductId = P.ProductId

	UNION ALL

	SELECT 
		'Purchase' AS 'Transaction',
		P.PurchaseId AS 'Purchase_Id',
		PR.ProductName,
		P.Quantity_Purchased AS 'Quantity',
		P.Purchase_Date,
		p.Purchase_Price AS 'Price',
		p.Total_Cost
	FROM Purchases P JOIN Products PR
	ON P.ProductId = PR.ProductId;		

SELECT * 
FROM SalesAndPurchases
ORDER BY ProductName;

--View Average of Each group
CREATE VIEW WeeklySales AS 
SELECT 
    P.ProductName,
    COALESCE(AVG(S.TotalAmount), 0) AS AverageSold,
    (SELECT MAX(S1.Sale_Price) FROM Sales S1) AS MostExpensiveProductPrice,
    (SELECT MIN(S1.Sale_Price) FROM Sales S1) AS CheapestProductPrice
FROM 
    Sales S JOIN Products P ON S.ProductId = P.ProductId
WHERE 
    S.Sale_Date >= DATEADD(DAY, -7, GETDATE())
GROUP BY 
    P.ProductName;

SELECT * FROM WeeklySales;

--Temporary Table

SELECT 
P.ProductId,
P.ProductName,
CASE
	WHEN SUM(S.Quantity_Sold) IS NULL THEN 'unsold product'
	ELSE CONCAT('number of Sales: ', SUM(S.Quantity_Sold))
END AS SalesStatus
INTO TempProductSales
FROM Products P LEFT JOIN Sales S ON P.ProductId = S.ProductId
GROUP BY P.ProductId, P.ProductName;

SELECT * FROM TempProductSales;

--Purchases from proucts that start with letter 'L'
SELECT 
	P.ProductId, P.ProductName,P.Description, P.Price,
	P.QuantityInStock,PR.PurchaseId,PR.Quantity_Purchased,
	PR.Purchase_Price,PR.Purchase_Date, PR.Total_Cost
FROM Products P JOIN Purchases PR ON P.ProductId = PR.ProductId
WHERE P.ProductName LIKE 'L%';

