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
);

SELECT * FROM Users;

DROP TABLE Users;

-- ******** Basic queries review ***********
SELECT Id, Email, Username FROM Users
        WHERE Users.Role LIKE 'admin'
        ORDER BY Username DESC;

-- IsNull
SELECT Email, ISNULL(Fullname,'None') + (', ') + ISNULL(Role,'n/a') As NameAndRole FROM Users;

-- NULLIF
SELECT Users.Email, NULLIF(Username, 'username7') AS NoUsername7 FROM Users;

-- Coalesce
SELECT Id, Email, COALESCE(Fullname, Username) AS FullNameOrUsername FROM Users;

-- Search Case
SELECT Email,
        CASE
            WHEN Fullname IS NULL THEN 'NO'
            ELSE 'YES'
        END AS HasFullname
    FROM Users;

SELECT Id, Email, Fullname FROM Users WHERE Username LIKE '%admin%';

SELECT Id, Email, Fullname FROM Users WHERE Fullname IS NOT NULL;

SELECT Id, Email, Fullname FROM Users WHERE Username LIKE 'username[0-4]';

SELECT Id, Email, Fullname FROM Users WHERE Username <> 'username1';

SELECT DISTINCT ISNULL(Fullname, 'None') As Fullname FROM Users ORDER BY Fullname

SELECT * FROM Users ORDER BY EMAIL OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY

SELECT * FROM Users ORDER BY EMAIL OFFSET 5 ROWS FETCH NEXT 5 ROWS ONLY







