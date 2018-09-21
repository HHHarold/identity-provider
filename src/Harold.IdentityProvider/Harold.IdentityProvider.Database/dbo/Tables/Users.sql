CREATE TABLE [dbo].[Users] (
    [UserId]    INT           NOT NULL,
    [FirstName] NVARCHAR (60) NOT NULL,
    [LastName]  NVARCHAR (60) NOT NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC)
);

