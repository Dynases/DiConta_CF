USE BDDiconDinoEco
GO

/****** Object:  Table [dbo].[TC001]    Script Date: 30/09/2019 5:26:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Sucursal](
	[canumi] [int] NOT NULL,
	[cadesc] [nvarchar](200) NULL,
	[caconcep1] [nvarchar](200) NULL,
	[caconcep2] [nvarchar](200) NULL,
	[caconcep3] [nvarchar](200) NULL,
	[caconcep4] [nvarchar](200) NULL,
	[caip] [nvarchar](20) NULL,
	[canprac] [int] NULL,
	[canrefor] [int] NULL,
	[cafact] [date] NULL,
	[cahact] [nvarchar](5) NULL,
	[cauact] [nvarchar](10) NULL,
 CONSTRAINT [PK_TC001_1] PRIMARY KEY CLUSTERED 
(
	[canumi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


