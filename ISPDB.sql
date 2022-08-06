CREATE DATABASE ADODB
GO

/*Creating Objects*/

CREATE TABLE Registration
(
	id INT PRIMARY KEY IDENTITY,
	firstName VARCHAR(30) NOT NULL,
	lastName VARCHAR(30) NOT NULL,
	email VARCHAR(50) UNIQUE NOT NULL,
	contact VARCHAR(20) UNIQUE NOT NULL,
	address VARCHAR(100) NOT NULL,
	userName VARCHAR(100) UNIQUE NOT NULL,
	password VARCHAR(20) UNIQUE NOT NULL
)
GO

CREATE PROC spAddUser
	@firstName VARCHAR(30),
	@lastName VARCHAR(30),
	@email VARCHAR(50),
	@contact VARCHAR(20),
	@address VARCHAR(100),
	@userName VARCHAR(100),
	@password VARCHAR(20)
AS
INSERT INTO Registration VALUES(@firstName,@lastName,@email,@contact,@address,@userName,@password)
GO

CREATE TABLE packagePlan
(
	planId INT PRIMARY KEY IDENTITY,
	planName VARCHAR(30) NOT NULL,
	speed VARCHAR(20) NOT NULL,
	price MONEY NOT NULL
)
GO

CREATE TABLE paymentMethod
(
	paymentMethodID INT PRIMARY KEY IDENTITY,
	paymentMethodName VARCHAR(20) NOT NULL,
	methodImage IMAGE NOT NULL
)
GO

CREATE PROC spInsertPaymentMethod
	@paymentMethodName VARCHAR(20),
	@methodImage IMAGE
AS
INSERT INTO paymentMethod VALUES(@paymentMethodName, @methodImage)
GO

CREATE TABLE customer
(
	customerId INT PRIMARY KEY IDENTITY,
	customerFirstName VARCHAR(30) NOT NULL,
	customerLastName VARCHAR(30) NOT NULL,
	contactNumber INT UNIQUE NOT NULL,
	address VARCHAR(50) NOT NULL,
	postalCode INT DEFAULT 'N/A',
	email VARCHAR(50) UNIQUE NOT NULL,
	IPNumber INT UNIQUE NOT NULL,
	connectionStartDate DATETIME NOT NULL,
	planId INT REFERENCES packagePlan(planId),
	paymentMethodID INT REFERENCES paymentMethod(paymentMethodID)
)
GO

CREATE PROC spInsertCustomer
	@customerFirstName VARCHAR(30),
	@customerLastName VARCHAR(30),
	@contactNumber INT,
	@address VARCHAR(50),
	@postalCode INT,
	@email VARCHAR(50),
	@IPNumber INT,
	@connectionStartDate DATETIME,
	@planId INT,
	@paymentMethodID INT
AS
INSERT INTO customer VALUES(@customerFirstName, @customerLastName, @contactNumber, @address, @postalCode, @email, @IPNumber, @connectionStartDate, @planId, @paymentMethodID)
GO

CREATE PROC spUpdateCustomer
	@customerFirstName VARCHAR(30),
	@customerLastName VARCHAR(30),
	@contactNumber INT,
	@address VARCHAR(50),
	@postalCode INT,
	@email VARCHAR(50),
	@IPNumber INT,
	@connectionStartDate DATETIME,
	@planId INT,
	@paymentMethodID INT
AS
UPDATE customer SET customerFirstName=@customerFirstName, customerLastName=@customerLastName, contactNumber=@contactNumber,address=@address,
					postalCode=@postalCode,email=@email,IPNumber=@IPNumber,connectionStartDate=@connectionStartDate,planId=@planId,paymentMethodID=@paymentMethodID
					WHERE IPNumber=@IPNumber
GO

CREATE TABLE employeeType
(
	employeeTypeId INT PRIMARY KEY IDENTITY,
	employeeTypeName VARCHAR(30) NOT NULL
)
GO

CREATE TABLE employee
(
	employeeId INT PRIMARY KEY IDENTITY,
	employeeFirstName VARCHAR(30) NOT NULL,
	employeeLastName NVARCHAR(20) NOT NULL,
	gender VARCHAR(10) NOT NULL,
	contactNumber INT UNIQUE NOT NULL,
	email VARCHAR(60) UNIQUE NOT NULL,
	dateOfBirth DATETIME NOT NULL,
	NID INT UNIQUE NOT NULL,
	address VARCHAR(100) NOT NULL,
	postalCode INT DEFAULT 'N/A',
	employeeImage IMAGE NOT NULL,
	employeeTypeId INT REFERENCES employeeType(employeeTypeId)
)
GO

CREATE PROC spDeleteEmployee
	@employeeId INT
AS 
DELETE FROM employee WHERE employeeId=@employeeId
GO


/*Executing Objects*/

INSERT INTO employeeType VALUES('General')
INSERT INTO employeeType VALUES('Service Provider')
GO

SELECT * FROM employee




