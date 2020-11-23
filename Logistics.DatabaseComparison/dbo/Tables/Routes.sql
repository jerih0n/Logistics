CREATE TABLE [dbo].[Routes] (
    [Id]          INT             NOT NULL,
    [StartCityId] INT             NOT NULL,
    [EndCityId]   INT             NOT NULL,
    [DateUpdated] DATETIME        NOT NULL,
    [Distance]    DECIMAL (18, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Routes_Cities_End] FOREIGN KEY ([EndCityId]) REFERENCES [dbo].[Cities] ([Id]),
    CONSTRAINT [FK_Routes_Cities_Start] FOREIGN KEY ([StartCityId]) REFERENCES [dbo].[Cities] ([Id])
);

