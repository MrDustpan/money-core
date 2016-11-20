CREATE TABLE [Account]
(
  Id int IDENTITY(1, 1) PRIMARY KEY,
  UserId int NOT NULL,
  Name varchar(100) NOT NULL,
  Balance decimal(11, 2) NOT NULL,
  CONSTRAINT FK_Account_UserId FOREIGN KEY (UserId) REFERENCES [User] (Id)
);