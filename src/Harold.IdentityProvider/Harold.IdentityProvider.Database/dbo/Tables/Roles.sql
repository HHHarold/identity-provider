CREATE TABLE [dbo].[Roles] (
    [RolId] INT           NOT NULL IDENTITY(1,1),
    [Name]  NVARCHAR (20) NOT NULL,
	[Description] TEXT,
    PRIMARY KEY CLUSTERED ([RolId] ASC),
	CONSTRAINT [AK_Roles_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);

