USE [IAGenerativaDB]
GO

/****** Object:  Table [dbo].[Ambito]    Script Date: 10/6/2025 08:58:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****------------------------Ambito-------------------------------------****/

CREATE TABLE [dbo].[Ambito](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Ambito] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****------------------------Clasificacion-------------------------------------****/
CREATE TABLE [dbo].[Clasificacion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Clasificacion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****------------------------TipoEstadoAnimo-------------------------------------****/

CREATE TABLE [dbo].[TipoEstadoAnimo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](255) NOT NULL,
 CONSTRAINT [PK_TipoEstadoAnimo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****------------------------EstadosAnimo-------------------------------------****/
CREATE TABLE [dbo].[EstadosAnimo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](55) NOT NULL,
	[TipoId] [int] NOT NULL,
 CONSTRAINT [PK_EstadosAnimo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[EstadosAnimo]  WITH CHECK ADD  CONSTRAINT [FK_EstadosAnimo_TipoEstadosAnimo] FOREIGN KEY([TipoId])
REFERENCES [dbo].[TipoEstadoAnimo] ([Id])
GO

ALTER TABLE [dbo].[EstadosAnimo] CHECK CONSTRAINT [FK_EstadosAnimo_TipoEstadosAnimo]
GO

/****------------------------ResultadoAnalizadorDeTexto-------------------------------------****/
CREATE TABLE [dbo].[ResultadoAnalizadorDeTexto](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TextoOriginal] [nvarchar](max) NOT NULL,
	[ClasificacionId] [int] NULL,
	[AmbitoId] [int] NULL,
	[TipoEstadoAnimoId] [int] NULL,
	[FechaProcesamiento] [datetime] NOT NULL,
	[PorcentajeFormal] [float] NOT NULL,
	[PorcentajeInformal] [float] NOT NULL,
 CONSTRAINT [PK_ResultadoClasificacion_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[ResultadoAnalizadorDeTexto]  WITH CHECK ADD  CONSTRAINT [FK_ResultadoAnalizadorDeTexto_Ambito] FOREIGN KEY([AmbitoId])
REFERENCES [dbo].[Ambito] ([Id])
GO

ALTER TABLE [dbo].[ResultadoAnalizadorDeTexto] CHECK CONSTRAINT [FK_ResultadoAnalizadorDeTexto_Ambito]
GO

ALTER TABLE [dbo].[ResultadoAnalizadorDeTexto]  WITH CHECK ADD  CONSTRAINT [FK_ResultadoAnalizadorDeTexto_Clasificacion] FOREIGN KEY([ClasificacionId])
REFERENCES [dbo].[Clasificacion] ([Id])
GO

ALTER TABLE [dbo].[ResultadoAnalizadorDeTexto] CHECK CONSTRAINT [FK_ResultadoAnalizadorDeTexto_Clasificacion]
GO

ALTER TABLE [dbo].[ResultadoAnalizadorDeTexto]  WITH CHECK ADD  CONSTRAINT [FK_ResultadoAnalizadorDeTexto_TipoEstadosAnimo] FOREIGN KEY([TipoEstadoAnimoId])
REFERENCES [dbo].[TipoEstadoAnimo] ([Id])
GO

ALTER TABLE [dbo].[ResultadoAnalizadorDeTexto] CHECK CONSTRAINT [FK_ResultadoAnalizadorDeTexto_TipoEstadosAnimo]
GO

/****------------------------ResultadoAnalizadorEstadoAnimo-------------------------------------****/
CREATE TABLE [dbo].[ResultadoAnalizadorEstadoAnimo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TextoOriginal] [varchar](max) NOT NULL,
	[TipoEstadoAnimoId] [int] NOT NULL,
	[FechaProcesamiento] [datetime] NOT NULL,
 CONSTRAINT [PK_ResultadoAnalizadorEstadoAnimo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[ResultadoAnalizadorEstadoAnimo]  WITH CHECK ADD  CONSTRAINT [FK_ResultadoAnalizadorEstadoAnimo_TipoEstadoAnimo] FOREIGN KEY([TipoEstadoAnimoId])
REFERENCES [dbo].[TipoEstadoAnimo] ([Id])
GO

ALTER TABLE [dbo].[ResultadoAnalizadorEstadoAnimo] CHECK CONSTRAINT [FK_ResultadoAnalizadorEstadoAnimo_TipoEstadoAnimo]
GO

/****------------------------ResultadoAnalizadorOraciones-------------------------------------****/
CREATE TABLE [dbo].[ResultadoAnalizadorOraciones](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TextoOriginal] [varchar](max) NOT NULL,
	[ClasificacionId] [int] NOT NULL,
	[FechaProcesamiento] [datetime] NOT NULL,
 CONSTRAINT [PK_ResultadoAnalizadorOraciones] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[ResultadoAnalizadorOraciones]  WITH CHECK ADD  CONSTRAINT [FK_ResultadoAnalizadorOraciones_Clasificacion] FOREIGN KEY([ClasificacionId])
REFERENCES [dbo].[Clasificacion] ([Id])
GO

ALTER TABLE [dbo].[ResultadoAnalizadorOraciones] CHECK CONSTRAINT [FK_ResultadoAnalizadorOraciones_Clasificacion]
GO

/****------------------------ResultadoTransformadorDeTexto-------------------------------------****/
CREATE TABLE [dbo].[ResultadoTransformadorDeTexto](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TextoOriginal] [varchar](max) NOT NULL,
	[TextoTransformado] [varchar](max) NULL,
	[AmbitoId] [int] NOT NULL,
	[FechaProcesamiento] [datetime] NOT NULL,
 CONSTRAINT [PK_ResultadoTransformadorDeTexto] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[ResultadoTransformadorDeTexto]  WITH CHECK ADD  CONSTRAINT [FK_ResultadoTransformadorDeTexto_Ambito] FOREIGN KEY([AmbitoId])
REFERENCES [dbo].[Ambito] ([Id])
GO

ALTER TABLE [dbo].[ResultadoTransformadorDeTexto] CHECK CONSTRAINT [FK_ResultadoTransformadorDeTexto_Ambito]
GO
