USE StarboxDB
GO

--Drinks table
DROP TABLE IF EXISTS Drinks

CREATE TABLE Drinks
(
	Id    int PRIMARY KEY IDENTITY(1,1),
	Name    varchar(100) NOT NULL,
	Price money NOT NULL,
    RowVersion rowversion
)

--add unique constraint
ALTER TABLE Drinks
ADD CONSTRAINT UQ_DrinkName UNIQUE (Name);

--Ingredients table
DROP TABLE IF EXISTS Ingredients

CREATE TABLE Ingredients
(
	Id    int PRIMARY KEY IDENTITY(1,1),
	Name    varchar(100) NOT NULL,
	UnitCost money NOT NULL,
	Amount int not null,
    RowVersion rowversion
)

ALTER TABLE Ingredients
ADD CONSTRAINT UQ_IngredientName UNIQUE (Name);

--DrinkIngredients join table
DROP TABLE IF EXISTS DrinkIngredients

CREATE TABLE DrinkIngredients
(	
	DrinkId int NOT NULL,
	IngredientId int NOT NULL,	
	IngredientQuantity int NOT NULL,
	PRIMARY KEY (DrinkID, IngredientID),
	CONSTRAINT FK_Drink FOREIGN KEY (DrinkId)  REFERENCES Drinks(Id),
	CONSTRAINT FK_Ingredient FOREIGN KEY (IngredientId)  REFERENCES Ingredients(Id)
)



