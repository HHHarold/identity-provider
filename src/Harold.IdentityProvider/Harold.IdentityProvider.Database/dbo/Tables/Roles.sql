CREATE TABLE [dbo].[Roles] (
    [RoleId]      INT           IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (20) NOT NULL,
    [Description] TEXT          NULL,
    PRIMARY KEY CLUSTERED ([RoleId] ASC),
    CONSTRAINT [AK_Roles_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);

