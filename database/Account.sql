CREATE TABLE [Account]
(
  Id int IDENTITY(1, 1) PRIMARY KEY,
  Name varchar(100) NOT NULL,
  Balance decimal(11, 2) NOT NULL
);