CREATE TABLE [dbo].[Users] (
    [UserId]    INT           NOT NULL IDENTITY(1,1),
    [FirstName] NVARCHAR (20) NOT NULL,
    [LastName]  NVARCHAR (20) NOT NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC)
);

