USE master;

IF EXISTS (
	SELECT
		*
	FROM
		sys.databases
	WHERE
		name='PropertyRentalDB'
) BEGIN
DROP DATABASE PropertyRentalDB END;

CREATE DATABASE PropertyRentalDB;

USE PropertyRentalDB;

GO
CREATE TABLE
	Roles (
		RoleID INT NOT NULL,
		Description NVARCHAR (15) NOT NULL,
		CONSTRAINT PK_Role PRIMARY KEY (RoleID)
	);

CREATE TABLE
	Users (
		UserID INT IDENTITY (111111, 1) NOT NULL,
		FirstName NVARCHAR (50) NOT NULL,
		LastName NVARCHAR (50) NOT NULL,
		Email NVARCHAR (70) NOT NULL,
		Password NVARCHAR (50) NOT NULL,
		PhoneNo NVARCHAR (15) NOT NULL,
		RoleID INT NOT NULL,
		CONSTRAINT PK_User PRIMARY KEY (UserID),
		CONSTRAINT FK_User_Role FOREIGN KEY (RoleID) REFERENCES Roles (RoleID)
	);

CREATE TABLE
	Address (
		AddressID INT IDENTITY (10001, 1) NOT NULL,
		Street NVARCHAR (50) NOT NULL,
		Unit NVARCHAR (10),
		ZipCode NVARCHAR (7) NOT NULL,
		City NVARCHAR (50) NOT NULL,
		Province NVARCHAR (50) NOT NULL,
		CONSTRAINT PK_Address PRIMARY KEY (AddressID)
	);

CREATE TABLE
	Apartments (
		ApartmentID INT IDENTITY (1000001, 1) NOT NULL,
		ManagerID INT NOT NULL,
		AddressID INT NOT NULL,
		Extra NVARCHAR (MAX),
		Price DECIMAL(10, 2),
		Status NVARCHAR (15) NOT NULL,
		CONSTRAINT PK_Apartment PRIMARY KEY (ApartmentID),
		CONSTRAINT FK_Apartment_Owner FOREIGN KEY (ManagerID) REFERENCES Users (UserID),
		CONSTRAINT FK_Apartment_Address FOREIGN KEY (AddressID) REFERENCES Address (AddressID)
	);

CREATE TABLE
	Messages (
		MessageID INT IDENTITY (111111, 1) NOT NULL,
		ApartmentID INT,
		SenderID INT NOT NULL,
		RecieverID INT NOT NULL,
		Date DATETIME NOT NULL,
		Text NVARCHAR (MAX) NOT NULL,
		CONSTRAINT PK_Message PRIMARY KEY (MessageID),
		CONSTRAINT FK_Message_Apartment FOREIGN KEY (ApartmentID) REFERENCES Apartments (ApartmentID),
		CONSTRAINT FK_Message_Sender FOREIGN KEY (SenderID) REFERENCES Users (UserID),
		CONSTRAINT FK_Message_Reciever FOREIGN KEY (RecieverID) REFERENCES Users (UserID)
	);

CREATE TABLE
	Appointments (
		AppointmentID INT IDENTITY (1110001, 1) NOT NULL,
		ApartmentID INT NOT NULL,
		ManagerID INT NOT NULL,
		TenantID INT NOT NULL,
		Date DATETIME NOT NULL,
		Status NVARCHAR (15) NOT NULL,
		CONSTRAINT PK_Appointment PRIMARY KEY (AppointmentID),
		CONSTRAINT FK_Appointment_Apartment FOREIGN KEY (ApartmentID) REFERENCES Apartments (ApartmentID),
		CONSTRAINT FK_Appointment_Manager FOREIGN KEY (ManagerID) REFERENCES Users (UserID),
		CONSTRAINT FK_Appointment_Tenant FOREIGN KEY (TenantID) REFERENCES Users (UserID)
	);

INSERT INTO
	Roles (RoleID, Description)
VALUES
	(1, 'Tenant'),
	(2, 'Owner'),
	(3, 'Manager'),
	(4, 'Administrator');

INSERT INTO
	Users (
		FirstName,
		LastName,
		Email,
		Password,
		PhoneNo,
		RoleID
	)
VALUES
	(
		'Mary',
		'Smith',
		'mary_smith@hotmail.com',
		'12345',
		'(416) 123-4567',
		1
	),
	(
		'Richard',
		'Jones',
		'richard_jones@gmail.com',
		'12345',
		'(647) 123-4567',
		1
	),
	(
		'Michael',
		'Johnson',
		'michael_johnson@yahoo.com',
		'12345',
		'(647) 321-4567',
		2
	),
	(
		'Alexander',
		'White',
		'alexander_white@gmail.com',
		'12345',
		'(514) 123-4567',
		2
	),
	(
		'Jennifer',
		'Williams',
		'jennifer_williams@aol.de',
		'12345',
		'(514) 123-7654',
		3
	),
	(
		'Julia',
		'Brown',
		'julia_brown@rambler.ru',
		'12345',
		'(514) 235-7654',
		3
	),
	(
		'Margo',
		'Fontaine',
		'margo_fontaine@gmail.com',
		'12345',
		'(514) 999-3333',
		4
	);

INSERT INTO
	Address (Street, Unit, ZipCode, City, Province)
VALUES
	(
		'123 Main St',
		'Apt 12',
		'M1M 1M1',
		'Toronto',
		'ON'
	),
	(
		'456 Avenue Fleet Weed',
		'Apt 32',
		'M1G 6S1',
		'Toronto',
		'ON'
	),
	(
		'1001 Sherbrook',
		'Apt 123',
		'H8M 5Q1',
		'Montreal',
		'QC'
	),
	(
		'2345 Avenue Georges-Baril',
		'',
		'H2C 2N3',
		'Montreal',
		'QC'
	),
	(
		'1001 Sherbrook',
		'Apt 223',
		'H8M 5Q1',
		'Montreal',
		'QC'
	);

INSERT INTO
	Apartments (
		ManagerID,
		AddressID,
		Extra,
		Price,
		Status
	)
VALUES
	(
		111115,
		10001,
		'No pets allowed',
		1500,
		'Available'
	),
	(111115, 10002, 'Balcony', 1800, 'Rented'),
	(
		111116,
		10003,
		'Wash Machine, Drier',
		1230,
		'Available'
	),
	(
		111116,
		10004,
		'Swiming Pool',
		2000,
		'Available'
	),
	(
		111115,
		10005,
		'No pets, children allowed',
		2500,
		'Rented'
	);

INSERT INTO
	Messages (
		ApartmentID,
		SenderID,
		RecieverID,
		Date,
		Text
	)
VALUES
	(
		1000001,
		111111,
		111115,
		'2018-01-12 12:23:00',
		'Hello, I am interested in your apartment.'
	),
	(
		1000002,
		111112,
		111115,
		'2022-04-02 9:14:00',
		'Hello, I would like to see the appartment.'
	),
	(
		1000003,
		111111,
		111116,
		'2023-07-03 11:34:00',
		'Hello, Appartment is stil available?'
	),
	(
		1000004,
		111111,
		111116,
		'2023-09-04 3:02:00',
		'Hello, I am interested in your apartment.'
	),
	(
		1000005,
		111112,
		111115,
		'2023-01-05 10:22:00',
		'Hello, Are pets alowed?'
	);

INSERT INTO
	Appointments (
		ApartmentID,
		ManagerID,
		TenantID,
		Date,
		Status
	)
VALUES
	(
		1000001,
		111115,
		111111,
		'2023-10-12 10:30:00',
		'Pending'
	),
	(
		1000001,
		111115,
		111111,
		'2023-10-15 11:30:00',
		'Confirmed'
	),
	(
		1000003,
		111116,
		111112,
		'2023-10-20 13:30:00',
		'Pending'
	),
	(
		1000004,
		111115,
		111112,
		'2023-10-25 14:30:00',
		'Confirmed'
	),
	(
		1000005,
		111116,
		111111,
		'2023-10-30 15:30:00',
		'Pending'
	);

GO
