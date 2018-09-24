CREATE TABLE [dbo].[Logins] (
    [LoginId]      INT          NOT NULL IDENTITY(1,1),
    [UserId]       INT          NOT NULL,
    [RoleId]       INT          NOT NULL,
    [UserName]     VARCHAR (20) NOT NULL,
    [PasswordSalt] BINARY (64)  NOT NULL,
    [PasswordHash] BINARY (32)  NOT NULL,
    PRIMARY KEY CLUSTERED ([LoginId] ASC),
    CONSTRAINT [FK_Logins_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([RolId]),
    CONSTRAINT [FK_Logins_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [AK_Logins_UserName] UNIQUE NONCLUSTERED ([UserName] ASC)
);

