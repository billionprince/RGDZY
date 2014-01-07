CREATE TABLE [dbo].[Calendar] (
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [type]       INT              NOT NULL,
    [owner]      VARCHAR (50)     NOT NULL,
    [title]      VARCHAR (50)     NOT NULL,
    [start_time] VARCHAR (50)     NULL,
    [end_time]   VARCHAR (50)     NULL,
    [allday]     INT              NULL,
    [creator]    VARCHAR (50)     NULL,
    [url]        VARCHAR (100)    NULL,
    [detail]     VARCHAR (100)    NULL,
    [sendemail]  VARCHAR (50)     NULL,
    [group]      VARCHAR (50)     NULL,
    CONSTRAINT [PK__Calendar__05E49D2446E78A0C] PRIMARY KEY CLUSTERED ([owner] ASC, [Id] ASC, [type] ASC)
);


