
CREATE DATABASE [FruitVegestables]
GO

USE [FruitVegestables]
GO


CREATE TABLE [dbo].[FoodTypes] (
[FoodId]   INT           NOT NULL,
[TypeName] NVARCHAR (50) NOT NULL,
PRIMARY KEY CLUSTERED ([FoodId] ASC)
);
GO
CREATE TABLE [dbo].[Fruits] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [DanishName]       NVARCHAR (30)  NOT NULL,
    [FoodTypeId]       INT            NOT NULL,
    [ApiMapping]       NVARCHAR (256) NOT NULL,
    [SpoilDays]        TINYINT        NOT NULL,
    [SpoilHours]       TINYINT        NOT NULL,
    [IdealTemperature] FLOAT (53)     NOT NULL,
    [IdealHumidity]    FLOAT (53)     NOT NULL,
    [Q10Factor]        FLOAT        DEFAULT ((3)) NOT NULL,
    [MaxTemp]          FLOAT (53)     DEFAULT ((40)) NOT NULL,
    [MinTemp]          FLOAT (53)     DEFAULT ((-15)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([DanishName] ASC),
    CONSTRAINT [FK_Fruits_FoodTypes] FOREIGN KEY ([FoodTypeId]) REFERENCES [dbo].[FoodTypes] ([FoodId]),
);
GO

CREATE TABLE [dbo].[Measurements] (
    [Id]          INT        IDENTITY (1, 1) NOT NULL,
    [TimeStamp]   BIGINT     DEFAULT (datediff_big(millisecond,'1970-01-01 00:00:00',getdate())) NOT NULL,
    [Tempearture] FLOAT (53) NOT NULL,
    [Humidity]    FLOAT (53) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE TABLE [dbo].[Users] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (50)  NOT NULL,
    [Password]     NVARCHAR (256) NOT NULL,
    [SessionToken] NVARCHAR (256) DEFAULT (NULL) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Name] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [sessionToken_notnull]
    ON [dbo].[Users]([SessionToken] ASC) WHERE ([SessionToken] IS NOT NULL);
GO


CREATE PROCEDURE [dbo].[AddMeasurement]
	@temperature FLOAT,
	@humidity FLOAT
AS
	INSERT INTO Measurements (Tempearture, Humidity)
	OUTPUT inserted.Id, inserted.TimeStamp, inserted.Tempearture, inserted.Humidity
	VALUES (@temperature, @humidity)
	
GO


CREATE PROCEDURE [dbo].[AddUser]
	@name NVARCHAR(50),
	@password NVARCHAR(256),
	@sessionToken NVARCHAR(256) = NULL
AS
	INSERT INTO Users (Name, Password, SessionToken)
	OUTPUT inserted.*
	VALUES (@name, @password, @sessionToken)
	
GO


CREATE PROCEDURE [dbo].[GenerateSessionToken]
	@name NVARCHAR(50),
	@password NVARCHAR(256)
AS
	UPDATE Users SET SessionToken = NEWID() OUTPUT INSERTED.SessionToken WHERE Name = @name AND Password = @password
	
GO


CREATE PROCEDURE [dbo].[GetAllNames]
	@filterFruit BIT = 1,
	@filterVegetable BIT = 1
AS
	SELECT DanishName FROM Fruits
	WHERE ((Fruits.FoodTypeId = 1 AND @filterFruit = 1)) OR
	((Fruits.FoodTypeId = 2 AND @filterVegetable = 1))
	
GO


CREATE PROCEDURE [dbo].[GetFruitNames]
	@filterFruit BIT = 1,
	@filterVegetable BIT = 1
AS
	SELECT DanishName FROM Fruits
	WHERE ((Fruits.FoodTypeId = 1 AND @filterFruit = 1)) OR
	((Fruits.FoodTypeId = 2 AND @filterVegetable = 1))
	
GO



CREATE PROCEDURE [dbo].[GetFruits]
	@filterFruit BIT = 1,
	@filterVegetable BIT = 1
	AS
SELECT * FROM Fruits
WHERE ((FoodTypeId = 1 AND @filterFruit = 1)) OR
	((FoodTypeId = 2 AND @filterVegetable = 1))
	
GO


CREATE PROCEDURE [dbo].[GetFruitsJOIN]
	@filterFruit BIT = 1,
	@filterVegetable BIT = 1,
	@filterName NVARCHAR(30) = NULL,
	@lowInterval INT = 0,
	@highInterval INT = 0x7ffffffe -- max int value minus 1, so it wont overflow
	AS
SELECT Fruits.*, FoodTypes.TypeName FROM Fruits
JOIN FoodTypes ON Fruits.FoodTypeId = FoodTypes.FoodId
WHERE ((Fruits.FoodTypeId = 1 AND @filterFruit = 1) OR
	(Fruits.FoodTypeId = 2 AND @filterVegetable = 1) )
	
	AND
	
	(@filterName IS NULL OR DanishName LIKE '%' + @filterName + '%')
ORDER BY DanishName OFFSET @lowInterval ROWS 
FETCH NEXT (@highInterval) ROWS ONLY

GO


CREATE PROCEDURE GetMeasurements
	@lowInterval INT = 0,
	@highInterval INT = 0x7ffffffe, -- max int value minus 1, so it wont overflow
	@sortProp NVARCHAR(30) = NULL,
	@descending BIT = 1

AS

IF @descending = 0
	BEGIN
    SELECT * FROM Measurements
	ORDER BY 
	CASE
		WHEN @sortProp = 'Id' THEN CAST(Id as sql_variant)
		WHEN @sortProp = 'TimeStamp' THEN TimeStamp
		WHEN @sortProp = 'Temperature' THEN Tempearture
		WHEN @sortProp = 'Humidity' THEN Humidity
		ELSE TimeStamp END
	ASC OFFSET @lowInterval ROWS 
	FETCH NEXT (@highInterval) ROWS ONLY
	END
ELSE
	BEGIN
    SELECT * FROM Measurements
	ORDER BY
	CASE
		WHEN @sortProp = 'Id' THEN CAST(Id as sql_variant)
		WHEN @sortProp = 'TimeStamp' THEN TimeStamp
		WHEN @sortProp = 'Temperature' THEN Tempearture
		WHEN @sortProp = 'Humidity' THEN Humidity
		ELSE TimeStamp END
	DESC OFFSET @lowInterval ROWS 
	FETCH NEXT (@highInterval) ROWS ONLY
	END;
	
GO




CREATE PROCEDURE [dbo].[GetUserByNameAndPassword]
	@name NVARCHAR(50),
	@password NVARCHAR(256)
AS
	SELECT * FROM Users
	WHERE Users.Name = @name AND Users.Password = @password
	
GO



CREATE PROCEDURE [dbo].[LogOut]
	@sessiontoken NVARCHAR(256)
AS
	UPDATE Users SET SessionToken = null
	OUTPUT inserted.SessionToken
	WHERE SessionToken = @sessiontoken
	
GO



CREATE PROCEDURE [dbo].[ValidateSessionToken]
	@sessionToken NVARCHAR(256)
AS
	SELECT * FROM Users WHERE SessionToken = @sessionToken
	
GO



-- The procedures below are ONLY for test DATABASE



CREATE PROCEDURE [dbo].[NukeFruits]

AS
	DELETE FROM Fruits
	DBCC CHECKIDENT ('Fruits', RESEED, 0);
	
GO



CREATE PROCEDURE [dbo].[NukeMeasurements]

AS
	DELETE FROM Measurements
	DBCC CHECKIDENT ('Measurements', RESEED, 0);
	
GO


CREATE PROCEDURE [dbo].[NukeUsers]

AS
	DELETE FROM Users
	DBCC CHECKIDENT ('Users', RESEED, 0);
	
GO










