CREATE TABLE [dbo].[Products]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Name] NVARCHAR(75) NOT NULL,
    [Description] NVARCHAR(MAX) NULL,
    [CategoryId] INT NOT NULL,
    [OwnerId] INT NOT NULL, 
    CONSTRAINT [FK_Products_Categories] FOREIGN KEY ([CategoryId]) REFERENCES [Categories]([Id]), 
    CONSTRAINT [FK_Products_Users] FOREIGN KEY ([OwnerId]) REFERENCES [Users]([Id])
)
