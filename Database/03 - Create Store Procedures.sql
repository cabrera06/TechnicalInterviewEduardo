use TechnicalInterviewDb;
go

CREATE PROCEDURE sp_GetAccountInfo
	@AccountId NVARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT  AccountId,
			CustomerName,
			Balance,
			CreatedDate 
	FROM Accounts 
	WHERE AccountId=@AccountId;
	--
	SELECT TOP 10 TransactionId,
				  AccountId,
				  Type,
				  Amount,
				  Date,
				  Description,
				  Reference
	FROM Transactions 
	WHERE AccountId=@AccountId
	ORDER BY Date DESC;

END;
GO

--************************************************************************
CREATE PROCEDURE sp_CreateDeposit 
	@AccountId NVARCHAR(50),
	@Amount DECIMAL (18,2),
	@Description NVARCHAR(500)
AS

BEGIN
	SET NOCOUNT ON;
	
	BEGIN TRY
		DECLARE @Balance DECIMAL(18,2);
		DECLARE @Reference INT;
		--
		IF @Amount<0 THROW 51000, 'El monto del deposito debe ser mayor a 0', 1;
		--
		SELECT @Balance=Balance
		FROM  Accounts
		WHERE AccountId= @AccountId;

		IF @@ROWCOUNT = 0 THROW 51000, 'La cuenta indicada no existe', 2;

		SELECT @Reference = NEXT VALUE FOR SeqReference;

		BEGIN TRANSACTION;

		INSERT INTO Transactions(AccountId,
							Type,
							Amount ,
							Description,
							Reference)
		VALUES(@AccountId,
			'DEPOSIT',
			@Amount,
			@Description,
			@Reference);

		--To return nuew balance and update the Account
		SET @Balance+=@Amount
		--
		UPDATE Accounts SET Balance=@Balance
		WHERE AccountId=@AccountId; 
		--
		COMMIT TRANSACTION;

		SELECT	Success = 1,
				Message = 'Transaccion exitosa',
				NewBalance = @Balance,
				Reference = @Reference;
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
			SELECT	Success = 0,
					Message = COALESCE(ERROR_MESSAGE(),'Se produjo un error al realizar el deposito'),
					NewBalance = NULL,
					Reference = NULL;

	END CATCH

END;
GO
--************************************************************************

CREATE PROCEDURE sp_CreateWithdrawal  
	@AccountId NVARCHAR(50),
	@Amount DECIMAL (18,2),
	@Description NVARCHAR(500)
AS
BEGIN
	SET NOCOUNT ON;
	
	--
	BEGIN TRY
		DECLARE @Balance DECIMAL(18,2);
		DECLARE @Reference INT;
		--
		SELECT @Balance=Balance
		FROM  Accounts
		WHERE AccountId= @AccountId;

		IF @@ROWCOUNT = 0 THROW 51000, 'La cuenta indicada no existe', 1;
		IF @Balance<@Amount THROW 51001, 'La cuenta de no posee los fondos suficientes', 1;

		SELECT @Reference = NEXT VALUE FOR SeqReference;

		BEGIN TRANSACTION;

		INSERT INTO Transactions(AccountId,
							Type,
							Amount ,
							Description,
							Reference)
		VALUES(@AccountId,
			'WITHDRAWAL',
			@Amount,
			@Description,
			@Reference);

		--To return nuew balance and update the Account
		SET @Balance-=@Amount
		--
		UPDATE Accounts SET Balance=@Balance
		WHERE AccountId=@AccountId; 
	
		--
		COMMIT TRANSACTION;

		SELECT	Success = 1,
				Message = 'Transaccion exitosa',
				NewBalance = @Balance,
				Reference = @Reference;
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
        SELECT	Success = 0,
				Message = COALESCE(ERROR_MESSAGE(),'Se produjo un error al realizar el deposito'),
				NewBalance = NULL,
				Reference = NULL;
	END CATCH

END;
GO
--************************************************************************

CREATE PROCEDURE sp_ExecuteTransfer
	@FromAccountId NVARCHAR(50),
	@ToAccountId NVARCHAR(50),
	@Amount DECIMAL (18,2),
	@Description NVARCHAR(500)

AS
BEGIN
	--
	SET NOCOUNT ON;
	--
	BEGIN TRY
		DECLARE @Balance DECIMAL(18,2);
		DECLARE @Reference INT;
		--
		SELECT @Balance=Balance
		FROM  Accounts
		WHERE AccountId= @FromAccountId;

		IF @@ROWCOUNT = 0 THROW 51000, 'La cuenta de origen no existe', 1;
		IF @Balance<@Amount THROW 51001, 'La cuenta de origen no posee los fondos suficientes', 1;

		SET @Balance=null;

		SELECT @Balance=Balance
		FROM  Accounts
		WHERE AccountId= @ToAccountId;

		IF @@ROWCOUNT = 0 THROW 51000, 'La cuenta de destino no existe', 2;

		SELECT @Reference = NEXT VALUE FOR SeqReference;

		BEGIN TRANSACTION;

		--DEBIT
		INSERT INTO Transactions(AccountId,
								Type,
								Amount ,
								Description,
								Reference)
		VALUES(@FromAccountId,
				'TRANSFER_OUT',
				@Amount,
				@Description,
				@Reference);

		UPDATE Accounts SET Balance-=@Amount
		WHERE AccountId=@FromAccountId;

		--CREDIT
		INSERT INTO Transactions(AccountId,
								Type,
								Amount ,
								Description,
								Reference)
		VALUES(@ToAccountId,
				'TRANSFER_IN',
				@Amount,
				@Description,
				@Reference);

		UPDATE Accounts SET Balance+=@Amount
		WHERE AccountId=@ToAccountId;

		COMMIT TRANSACTION;

		SELECT	Success = 1,
				Message = 'Transaccion exitosa',
				Reference = @Reference
	END TRY
	--
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
		SELECT	Success = 0,
				Message = COALESCE(ERROR_MESSAGE(),'Se produjo un error al realizar la transferencia'),
				Reference=NULL
	END CATCH
END;
go
