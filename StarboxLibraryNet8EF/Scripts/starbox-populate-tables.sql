--populate drinks table
INSERT INTO [dbo].[Drinks]
           ([Name]
           ,[Price])
     VALUES
           ('Coffee'
           ,0.0)
GO

INSERT INTO [dbo].[Drinks]
           ([Name]
           ,[Price])
     VALUES
           ('Decaf Coffee'
           ,0.0)
GO

INSERT INTO [dbo].[Drinks]
           ([Name]
           ,[Price])
     VALUES
           ('Cappuccino'
           ,0.0)
GO

INSERT INTO [dbo].[Drinks]
           ([Name]
           ,[Price])
     VALUES
           ('Cafe Americano'
           ,0.0)
GO

INSERT INTO [dbo].[Drinks]
           ([Name]
           ,[Price])
     VALUES
           ('Cafe Latte'
           ,0.0)
GO

INSERT INTO [dbo].[Drinks]
           ([Name]
           ,[Price])
     VALUES
           ('Cafe Mocha'
           ,0.0)
GO

SELECT TOP (1000) [Id]
      ,[Name]
      ,[Price]
  FROM [dbo].[Drinks]


--populate ingredients
INSERT INTO [dbo].[Ingredients]
           ([Name]
           ,[UnitCost]
           ,[Amount])
     VALUES
           ('Coffee'
           ,0.75
           ,10)
GO

INSERT INTO [dbo].[Ingredients]
           ([Name]
           ,[UnitCost]
           ,[Amount])
     VALUES
           ('Cream'
           ,0.25
           ,10)

INSERT INTO [dbo].[Ingredients]
           ([Name]
           ,[UnitCost]
           ,[Amount])
     VALUES
           ('Sugar'
           ,0.25
           ,10)
GO

INSERT INTO [dbo].[Ingredients]
           ([Name]
           ,[UnitCost]
           ,[Amount])
     VALUES
           ('Decaf Coffee'
           ,0.75
           ,10)
GO

INSERT INTO [dbo].[Ingredients]
           ([Name]
           ,[UnitCost]
           ,[Amount])
     VALUES
           ('Cocoa'
           ,0.90
           ,10)
GO

INSERT INTO [dbo].[Ingredients]
           ([Name]
           ,[UnitCost]
           ,[Amount])
     VALUES
           ('Espresso'
           ,1.10
           ,10)
GO

INSERT INTO [dbo].[Ingredients]
           ([Name]
           ,[UnitCost]
           ,[Amount])
     VALUES
           ('Steamed Milk'
           ,0.35
           ,10)
GO

INSERT INTO [dbo].[Ingredients]
           ([Name]
           ,[UnitCost]
           ,[Amount])
     VALUES
           ('Foamed Milk'
           ,0.35
           ,10)
GO

INSERT INTO [dbo].[Ingredients]
           ([Name]
           ,[UnitCost]
           ,[Amount])
     VALUES
           ('Whipped Cream'
           ,1.00
           ,10)
GO

SELECT TOP (1000) [Id]
      ,[Name]
      ,[UnitCost]
      ,[Amount]
  FROM [dbo].[Ingredients]

--populate DrinkIngredients.
--Cafe Americano
INSERT INTO DrinkIngredients (DrinkId, IngredientId, IngredientQuantity)
SELECT 
    d.Id AS DrinkId,
    i.Id AS IngredientId,
    3 AS IngredientQuantity  
FROM Drinks d
JOIN Ingredients i
    ON i.Name = 'Espresso'  
WHERE d.Name = 'Cafe Americano';  

--Cafe Latte
INSERT INTO DrinkIngredients (DrinkId, IngredientId, IngredientQuantity)
SELECT 
    d.Id AS DrinkId,
    i.Id AS IngredientId,
    2 AS IngredientQuantity  
FROM Drinks d
JOIN Ingredients i
    ON i.Name = 'Espresso'  
WHERE d.Name = 'Cafe Latte';  

INSERT INTO DrinkIngredients (DrinkId, IngredientId, IngredientQuantity)
SELECT 
    d.Id AS DrinkId,
    i.Id AS IngredientId,
    1 AS IngredientQuantity  
FROM Drinks d
JOIN Ingredients i
    ON i.Name = 'Steamed Milk'  
WHERE d.Name = 'Cafe Latte';  

--Cafe Mocha
INSERT INTO DrinkIngredients (DrinkId, IngredientId, IngredientQuantity)
SELECT 
    d.Id AS DrinkId,
    i.Id AS IngredientId,
    1 AS IngredientQuantity  
FROM Drinks d
JOIN Ingredients i
    ON i.Name = 'Espresso'  
WHERE d.Name = 'Cafe Mocha';  

INSERT INTO DrinkIngredients (DrinkId, IngredientId, IngredientQuantity)
SELECT 
    d.Id AS DrinkId,
    i.Id AS IngredientId,
    1 AS IngredientQuantity  
FROM Drinks d
JOIN Ingredients i
    ON i.Name = 'Cocoa'  
WHERE d.Name = 'Cafe Mocha';  

INSERT INTO DrinkIngredients (DrinkId, IngredientId, IngredientQuantity)
SELECT 
    d.Id AS DrinkId,
    i.Id AS IngredientId,
    1 AS IngredientQuantity  
FROM Drinks d
JOIN Ingredients i
    ON i.Name = 'Steamed Milk'  
WHERE d.Name = 'Cafe Mocha';  

INSERT INTO DrinkIngredients (DrinkId, IngredientId, IngredientQuantity)
SELECT 
    d.Id AS DrinkId,
    i.Id AS IngredientId,
    1 AS IngredientQuantity  
FROM Drinks d
JOIN Ingredients i
    ON i.Name = 'Whipped Cream'  
WHERE d.Name = 'Cafe Mocha';  

--Cappuccino
INSERT INTO DrinkIngredients (DrinkId, IngredientId, IngredientQuantity)
SELECT 
    d.Id AS DrinkId,
    i.Id AS IngredientId,
    2 AS IngredientQuantity  
FROM Drinks d
JOIN Ingredients i
    ON i.Name = 'Espresso'  
WHERE d.Name = 'Cappuccino';  

INSERT INTO DrinkIngredients (DrinkId, IngredientId, IngredientQuantity)
SELECT 
    d.Id AS DrinkId,
    i.Id AS IngredientId,
    1 AS IngredientQuantity  
FROM Drinks d
JOIN Ingredients i
    ON i.Name = 'Steamed Milk'  
WHERE d.Name = 'Cappuccino';  

INSERT INTO DrinkIngredients (DrinkId, IngredientId, IngredientQuantity)
SELECT 
    d.Id AS DrinkId,
    i.Id AS IngredientId,
    1 AS IngredientQuantity  
FROM Drinks d
JOIN Ingredients i
    ON i.Name = 'Foamed Milk'  
WHERE d.Name = 'Cappuccino';  

--Coffee
INSERT INTO DrinkIngredients (DrinkId, IngredientId, IngredientQuantity)
SELECT 
    d.Id AS DrinkId,
    i.Id AS IngredientId,
    3 AS IngredientQuantity  
FROM Drinks d
JOIN Ingredients i
    ON i.Name = 'Coffee'  
WHERE d.Name = 'Coffee';  

INSERT INTO DrinkIngredients (DrinkId, IngredientId, IngredientQuantity)
SELECT 
    d.Id AS DrinkId,
    i.Id AS IngredientId,
    1 AS IngredientQuantity  
FROM Drinks d
JOIN Ingredients i
    ON i.Name = 'Sugar'  
WHERE d.Name = 'Coffee';

INSERT INTO DrinkIngredients (DrinkId, IngredientId, IngredientQuantity)
SELECT 
    d.Id AS DrinkId,
    i.Id AS IngredientId,
    1 AS IngredientQuantity  
FROM Drinks d
JOIN Ingredients i
    ON i.Name = 'Cream'  
WHERE d.Name = 'Coffee';

--Decaf Coffee
INSERT INTO DrinkIngredients (DrinkId, IngredientId, IngredientQuantity)
SELECT 
    d.Id AS DrinkId,
    i.Id AS IngredientId,
    3 AS IngredientQuantity  
FROM Drinks d
JOIN Ingredients i
    ON i.Name = 'Coffee'  
WHERE d.Name = 'Decaf Coffee';  

INSERT INTO DrinkIngredients (DrinkId, IngredientId, IngredientQuantity)
SELECT 
    d.Id AS DrinkId,
    i.Id AS IngredientId,
    1 AS IngredientQuantity  
FROM Drinks d
JOIN Ingredients i
    ON i.Name = 'Sugar'  
WHERE d.Name = 'Decaf Coffee';

INSERT INTO DrinkIngredients (DrinkId, IngredientId, IngredientQuantity)
SELECT 
    d.Id AS DrinkId,
    i.Id AS IngredientId,
    1 AS IngredientQuantity  
FROM Drinks d
JOIN Ingredients i
    ON i.Name = 'Cream'  
WHERE d.Name = 'Decaf Coffee';

//-----------------------------------//
  SELECT 
       d.Id as drinkId
      ,d.Name as drinkName
      ,d.Price
	  ,i.Name as ingredientName
	  ,di.IngredientQuantity
  FROM [dbo].[Drinks] d
  inner join [dbo].[DrinkIngredients] di
  on di.DrinkId = d.Id
  inner join  [dbo].[Ingredients] i
  on i.Id = di.IngredientId