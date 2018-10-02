CREATE TABLE [dbo].[Users] (
    [UserId]    INT           NOT NULL IDENTITY(1,1),
    [FirstName] NVARCHAR (20) NOT NULL,
    [LastName]  NVARCHAR (20) NOT NULL,
	[RoleId]       INT          NOT NULL,
    [Username]     VARCHAR (20) NOT NULL,
    [PasswordSalt] BINARY (64)  NOT NULL,
    [PasswordHash] BINARY (32)  NOT NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC),
	CONSTRAINT [AK_Users_Username] UNIQUE NONCLUSTERED ([Username] ASC), 
    CONSTRAINT [FK_Users_Roles] FOREIGN KEY ([RoleId]) REFERENCES [Roles]([RoleId])
);

