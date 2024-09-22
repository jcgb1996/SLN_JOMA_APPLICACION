USE [JM_COMUNICATE]
GO

/****** Object:  Table [JM].[MenuXRol]    Script Date: 22/9/2024 17:37:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [JM].[MenuXRol](
	[IdRol] [int] NOT NULL,
	[MenuId] [int] NOT NULL,
	[UsuarioCreacion] [varchar](50) NOT NULL,
	[FechaCreacion] [datetime] NULL,
	[UsuarioModificacion] [varchar](50) NULL,
	[FechaModificacion] [datetime] NULL,
	[Estado] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC,
	[MenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


