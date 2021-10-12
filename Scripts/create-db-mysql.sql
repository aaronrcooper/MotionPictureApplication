CREATE DATABASE MotionPicture;

CREATE TABLE MotionPicture.MotionPictures
(
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    Description NVARCHAR(500),
    ReleaseYear INT NOT NULL
);