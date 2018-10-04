CREATE TABLE [dbo].[Users] (
    [UserId]       INT           IDENTITY (1, 1) NOT NULL,
    [FirstName]    NVARCHAR (20) NOT NULL,
    [LastName]     NVARCHAR (20) NOT NULL,
    [RoleId]       INT           NOT NULL,
    [Username]     VARCHAR (20)  NOT NULL,
    [PasswordSalt] BINARY (128)   NOT NULL,
    [PasswordHash] BINARY (64)   NOT NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [FK_Users_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([RoleId]),
    CONSTRAINT [AK_Users_Username] UNIQUE NONCLUSTERED ([Username] ASC)
);

