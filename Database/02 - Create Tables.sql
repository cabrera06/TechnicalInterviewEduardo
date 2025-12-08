use TechnicalInterviewDb;
go
CREATE TABLE Accounts (
    AccountId NVARCHAR(50) PRIMARY KEY,
    CustomerName NVARCHAR(200) NOT NULL,
    Balance DECIMAL(18,2) NOT NULL DEFAULT 0,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT CK_Accounts_Balance CHECK (Balance >= 0)
);
go
--************************************************************************
CREATE TABLE Transactions (
    TransactionId INT IDENTITY(1,1) PRIMARY KEY,
    AccountId NVARCHAR(50) NOT NULL,
    Type NVARCHAR(20) NOT NULL, -- DEPOSIT, WITHDRAWAL, TRANSFER_IN, TRANSFER_OUT
    Amount DECIMAL(18,2) NOT NULL,
    Date DATETIME NOT NULL DEFAULT GETDATE(),
    Description NVARCHAR(500),
    Reference INT,
    FOREIGN KEY (AccountId) REFERENCES Accounts(AccountId)
);
go
--************************************************************************
CREATE TABLE InterestHistory (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    AccountId NVARCHAR(50) NOT NULL,
    InterestRate DECIMAL(5,2) NOT NULL,
    CalculatedInterest DECIMAL(18,2) NOT NULL,
    CalculationDate DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (AccountId) REFERENCES Accounts(AccountId)
);
go

--************************************************************************
CREATE INDEX IX_Transactions_AccountId ON Transactions(AccountId);
CREATE INDEX IX_Transactions_Date ON Transactions(Date DESC);
go