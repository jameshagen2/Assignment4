CREATE TABLE [dbo].[NetUser] (
    [UserID]       INT       IDENTITY (1, 1)  NOT NULL,
    [UserName]     VARCHAR (15) NOT NULL,
    [UserPassword] VARCHAR (20) NOT NULL,
    [UserType]     VARCHAR (20) NOT NULL,
    PRIMARY KEY CLUSTERED ([UserID] ASC),
    UNIQUE NONCLUSTERED ([UserName] ASC)
);

