CREATE DATABASE THETEACHER

USE THETEACHER

CREATE TABLE Users (
	Id uniqueidentifier primary key not null,
    Email nvarchar(100) not null,
    Username nvarchar(100) not null,
    Password nvarchar(200) not null,
    Salt nvarchar(200) not null,
    Fullname nvarchar(100),
    Role nvarchar(10),
    UpdatedAt datetime not null,
    CreatedAt  datetime not null
)

SELECT * FROM USERS