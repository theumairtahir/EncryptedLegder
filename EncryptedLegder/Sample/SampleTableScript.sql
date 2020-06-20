CREATE TABLE [dbo].[Table] (
    [Id]                  BIGINT        IDENTITY (1, 1) NOT NULL,
    [TransactioneeId]     INT           NOT NULL,
    [Debit]               VARCHAR (MAX) NOT NULL,
    [Credit]              VARCHAR (MAX) NOT NULL,
    [Balance]             VARCHAR (MAX) NOT NULL,
    [TransactionDateTime] VARCHAR (MAX) NOT NULL,
    [Description]         VARCHAR (MAX) NOT NULL,
    [Comments]            VARCHAR (MAX) NOT NULL,
    [Tag1]                VARCHAR (MAX) NULL,
    [Tag2]                VARCHAR (MAX) NULL,
    [Tag3]                VARCHAR (MAX) NULL,
    [Signature] VARCHAR(MAX) NOT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);