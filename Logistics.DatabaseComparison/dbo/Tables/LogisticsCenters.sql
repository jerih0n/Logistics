CREATE TABLE [dbo].[LogisticsCenters] (
    [Id]          INT      NOT NULL,
    [CityId]      INT      NOT NULL,
    [DateUpdated] DATETIME NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LogisticsCenters_Cities] FOREIGN KEY ([CityId]) REFERENCES [dbo].[Cities] ([Id])
);

