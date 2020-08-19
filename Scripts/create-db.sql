CREATE DATABASE MotionPicture
GO

USE MotionPicture
GO

CREATE TABLE MotionPicture.dbo.MotionPictures (
    Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    Description NVARCHAR(500),
    ReleaseYear INT NOT NULL
);