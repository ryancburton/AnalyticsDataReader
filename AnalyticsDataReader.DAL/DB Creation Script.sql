CREATE TABLE [dbo].[AnalyticalData](
	ID bigint NOT NULL IDENTITY(1,1),
	[DateTime] [DATETIME] NOT NULL,
	Point NUMERIC(20,8) NOT NULL
 CONSTRAINT [PK_AnalyticalData] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,
    CONSTRAINT [UQ_Datecode] UNIQUE NONCLUSTERED
    (
        [DateTime]
    )
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[AnalyticalMetaData](
	ID bigint NOT NULL IDENTITY(1,1),
	[DateOfUpload] [DATETIME] NOT NULL,
	MinForSeries NUMERIC(20,8) NOT NULL,
	MaxForSeries NUMERIC(20,8) NOT NULL,
	AverageForSeries NUMERIC(20,8) NOT NULL,
	StartOfMostExpensiveHour Date NOT NULL,
	SeriesStartID bigint NOT NULL,
	SeriesEndID bigint NOT NULL
 CONSTRAINT [PK_AnalyticalMetaData] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

--Insert into [AnalyticalData] values(NEWID(), GETDATE(), 37.45999908);
--Insert into [AnalyticalData] values(NEWID(), GETDATE(), 100.45999908);
--Insert into [AnalyticalData] values(NEWID(), GETDATE(), 55.45999908);

--select * from [AnalyticalData]

--select * from [AnalyticalMetaData]

--drop table [AnalyticalData]

--drop table [AnalyticalMetaData]