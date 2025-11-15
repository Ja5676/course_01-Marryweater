create database LotFlowDB
go

use LotFlowDB
go 

CREATE TABLE Users
(
    Id INT IDENTITY(1,1) PRIMARY KEY,       -- автоматичне інкрементування
    Username NVARCHAR(50) NOT NULL,         -- ім'я користувача
    Email NVARCHAR(100) NOT NULL UNIQUE,    -- унікальна електронна пошта
    DateRegistered DATETIME NOT NULL, -- дата реєстрації
    Salt NVARCHAR(50) NOT NULL,             -- соль для хешу пароля
    PasswordHash NVARCHAR(256) NOT NULL     -- хеш пароля
);

CREATE TABLE Categories (
    Id int primary key identity(1,1),
    Name NVARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE Ads (
    Id int primary key identity(1,1),
    Title NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500),
    Price DECIMAL(10,2) NOT NULL,
    CategoryId INTEGER NOT NULL,
    UserId INTEGER NOT NULL,
    ImagePath VARBINARY(MAX),
    DatePosted DATETIME not null,
    FOREIGN KEY (CategoryId) REFERENCES Categories(Id),
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);