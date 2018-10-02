CREATE TABLE [dbo].[Roles] (
    [RoleId] INT           NOT NULL IDENTITY(1,1),
    [Name]  NVARCHAR (20) NOT NULL,
	[Description] TEXT,
    PRIMARY KEY CLUSTERED ([RoleId] ASC),
	CONSTRAINT [AK_Roles_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);

