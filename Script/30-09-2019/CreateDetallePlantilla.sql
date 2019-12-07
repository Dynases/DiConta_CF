USE [BDDiconDinoEco]
GO

ALTER TABLE [dbo].[DetallePlantilla] DROP CONSTRAINT [FK__DetallePl__Plant__1CE72E9F]
GO

/****** Object:  Table [dbo].[DetallePlantilla]    Script Date: 01/10/2019 5:12:09 ******/
DROP TABLE [dbo].[DetallePlantilla]
GO

/****** Object:  Table [dbo].[DetallePlantilla]    Script Date: 01/10/2019 5:12:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DetallePlantilla](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PlantillaId] [int] NULL,
	[CuentaId] [int] NULL,
	[Porcentaje] [decimal](18, 2) NULL,
	[Debe] [int] NULL,
	[Haber] [int] NULL,
	[Glosa] [nvarchar](200) NULL,
 CONSTRAINT [PK_DetallePlantilla] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[DetallePlantilla]  WITH CHECK ADD  CONSTRAINT [FK__DetallePl__Plant__1CE72E9F] FOREIGN KEY([PlantillaId])
REFERENCES [dbo].[Plantilla] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[DetallePlantilla] CHECK CONSTRAINT [FK__DetallePl__Plant__1CE72E9F]
GO


