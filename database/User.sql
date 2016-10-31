CREATE TABLE [User]
(
  Id int IDENTITY(1, 1) PRIMARY KEY,
  Email varchar(100) NOT NULL,
  Password varchar(200) NOT NULL,
  ConfirmationId varchar(50) NOT NULL,
  Status tinyint NOT NULL
);