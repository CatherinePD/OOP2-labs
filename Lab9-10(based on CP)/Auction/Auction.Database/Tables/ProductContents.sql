CREATE TABLE [dbo].[ProductContents]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Content] NVARCHAR(MAX) NOT NULL,
    [ProductId] INT NOT NULL, 
    CONSTRAINT [FK_ProductContents_Products] FOREIGN KEY ([ProductId]) REFERENCES [Products]([Id])
)
