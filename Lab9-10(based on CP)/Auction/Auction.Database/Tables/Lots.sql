CREATE TABLE [dbo].[Lots]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Title] NVARCHAR(75) NOT NULL,
    [DateCreated] DATETIME NOT NULL,
    [DateToExpire] DATETIME NOT NULL,
    [DateFinished] DATETIME NULL,
    [StartBid] DECIMAL(9,3) NOT NULL,
    [CurrentBid] DECIMAL (9,3) NOT NULL,
    [MinBidStep] DECIMAL (9,3) NOT NULL,
    [ProductId] INT NOT NULL,
    [IsClosed] BIT NOT NULL DEFAULT(0),
    [IsExpired] BIT NOT NULL DEFAULT(0),
    CONSTRAINT [FK_Lots_Products] FOREIGN KEY ([ProductId]) REFERENCES [Products]([Id])
)
