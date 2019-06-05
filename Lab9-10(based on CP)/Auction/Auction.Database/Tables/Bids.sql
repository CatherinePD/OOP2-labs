CREATE TABLE [dbo].[Bids]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Amount] DECIMAL(9,3) NOT NULL,
    [LotId] INT NOT NULL,
    [DateCreated] DATETIME NOT NULL,
    [UserId] INT NOT NULL,
    [IsObserved] BIT NOT NULL DEFAULT(1), 
    CONSTRAINT [FK_Bids_Users] FOREIGN KEY ([UserId]) REFERENCES [Users]([Id]),
    CONSTRAINT [FK_Bids_Lots] FOREIGN KEY ([LotId]) REFERENCES [Lots]([Id])
)
