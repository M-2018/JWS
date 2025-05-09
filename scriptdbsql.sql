USE [master]
GO
/****** Object:  Database [JWSDb]    Script Date: 16/02/2025 1:17:49 p. m. ******/
CREATE DATABASE [JWSDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'JWSDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\JWSDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'JWSDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\JWSDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [JWSDb] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [JWSDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [JWSDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [JWSDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [JWSDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [JWSDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [JWSDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [JWSDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [JWSDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [JWSDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [JWSDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [JWSDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [JWSDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [JWSDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [JWSDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [JWSDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [JWSDb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [JWSDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [JWSDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [JWSDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [JWSDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [JWSDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [JWSDb] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [JWSDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [JWSDb] SET RECOVERY FULL 
GO
ALTER DATABASE [JWSDb] SET  MULTI_USER 
GO
ALTER DATABASE [JWSDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [JWSDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [JWSDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [JWSDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [JWSDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [JWSDb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'JWSDb', N'ON'
GO
ALTER DATABASE [JWSDb] SET QUERY_STORE = ON
GO
ALTER DATABASE [JWSDb] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [JWSDb]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 16/02/2025 1:17:49 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Asistencias]    Script Date: 16/02/2025 1:17:49 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Asistencias](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Fecha] [datetime2](7) NULL,
	[Presente] [bit] NULL,
	[EstudianteId] [bigint] NOT NULL,
	[MateriaId] [bigint] NOT NULL,
	[CicloId] [bigint] NOT NULL,
 CONSTRAINT [PK_Asistencias] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Calificaciones]    Script Date: 16/02/2025 1:17:49 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Calificaciones](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Tarea] [decimal](18, 2) NULL,
	[Trabajo] [decimal](18, 2) NULL,
	[ExamenFinal] [decimal](18, 2) NULL,
	[Exposicion] [decimal](18, 2) NULL,
	[Actitudinal] [decimal](18, 2) NULL,
	[Quiz1] [decimal](18, 2) NULL,
	[Definitiva] [decimal](18, 2) NULL,
	[Recuperacion] [bit] NULL,
	[Habilitacion] [bit] NULL,
	[EstudianteId] [bigint] NOT NULL,
	[CicloId] [bigint] NOT NULL,
	[MateriaId] [bigint] NOT NULL,
	[NotaHabilitacion] [decimal](18, 2) NULL,
	[NotaRecuperacion] [decimal](18, 2) NULL,
	[Quiz2] [decimal](18, 2) NULL,
	[Taller] [decimal](18, 2) NULL,
 CONSTRAINT [PK_Calificaciones] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CicloMaterias]    Script Date: 16/02/2025 1:17:49 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CicloMaterias](
	[CicloId] [bigint] NOT NULL,
	[MateriaId] [bigint] NOT NULL,
 CONSTRAINT [PK_CicloMaterias] PRIMARY KEY CLUSTERED 
(
	[CicloId] ASC,
	[MateriaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ciclos]    Script Date: 16/02/2025 1:17:49 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ciclos](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](max) NOT NULL,
	[Anio] [nvarchar](max) NOT NULL,
	[Semestre] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Ciclos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Estudiantes]    Script Date: 16/02/2025 1:17:49 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estudiantes](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Nombres] [nvarchar](max) NOT NULL,
	[Apellidos] [nvarchar](max) NOT NULL,
	[NroDocumento] [nvarchar](max) NOT NULL,
	[TipoDocumento] [nvarchar](max) NOT NULL,
	[FechaNacimiento] [datetime2](7) NOT NULL,
	[Direccion] [nvarchar](max) NOT NULL,
	[Telefono] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[FotoUrl] [nvarchar](max) NULL,
	[SemestrePagado] [bit] NULL,
	[CicloId] [bigint] NOT NULL,
 CONSTRAINT [PK_Estudiantes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Materias]    Script Date: 16/02/2025 1:17:49 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Materias](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](max) NOT NULL,
	[ProfesorId] [bigint] NOT NULL,
 CONSTRAINT [PK_Materias] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Matriculas]    Script Date: 16/02/2025 1:17:49 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Matriculas](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[EstudianteId] [bigint] NOT NULL,
	[CicloId] [bigint] NOT NULL,
	[AnioLectivo] [int] NOT NULL,
 CONSTRAINT [PK_Matriculas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonasResponsables]    Script Date: 16/02/2025 1:17:49 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonasResponsables](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Nombres] [nvarchar](max) NOT NULL,
	[Apellidos] [nvarchar](max) NOT NULL,
	[Relacion] [nvarchar](max) NOT NULL,
	[EstudianteId] [bigint] NOT NULL,
	[CorreoElectronico] [nvarchar](max) NOT NULL,
	[Telefono] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_PersonasResponsables] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Profesores]    Script Date: 16/02/2025 1:17:49 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profesores](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Nombres] [nvarchar](max) NOT NULL,
	[Apellidos] [nvarchar](max) NOT NULL,
	[NroDocumento] [nvarchar](max) NOT NULL,
	[TipoDocumento] [nvarchar](max) NOT NULL,
	[FechaNacimiento] [datetime2](7) NOT NULL,
	[Direccion] [nvarchar](max) NOT NULL,
	[Telefono] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Especialidad] [nvarchar](max) NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Profesores] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 16/02/2025 1:17:49 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Nombres] [nvarchar](max) NOT NULL,
	[Apellidos] [nvarchar](max) NOT NULL,
	[IsAdmin] [bit] NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_Asistencias_CicloId]    Script Date: 16/02/2025 1:17:49 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_Asistencias_CicloId] ON [dbo].[Asistencias]
(
	[CicloId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Asistencias_EstudianteId]    Script Date: 16/02/2025 1:17:49 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_Asistencias_EstudianteId] ON [dbo].[Asistencias]
(
	[EstudianteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Asistencias_MateriaId]    Script Date: 16/02/2025 1:17:49 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_Asistencias_MateriaId] ON [dbo].[Asistencias]
(
	[MateriaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Calificaciones_CicloId]    Script Date: 16/02/2025 1:17:49 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_Calificaciones_CicloId] ON [dbo].[Calificaciones]
(
	[CicloId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Calificaciones_MateriaId]    Script Date: 16/02/2025 1:17:49 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_Calificaciones_MateriaId] ON [dbo].[Calificaciones]
(
	[MateriaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CicloMaterias_MateriaId]    Script Date: 16/02/2025 1:17:49 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_CicloMaterias_MateriaId] ON [dbo].[CicloMaterias]
(
	[MateriaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Estudiantes_CicloId]    Script Date: 16/02/2025 1:17:49 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_Estudiantes_CicloId] ON [dbo].[Estudiantes]
(
	[CicloId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Materias_ProfesorId]    Script Date: 16/02/2025 1:17:49 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_Materias_ProfesorId] ON [dbo].[Materias]
(
	[ProfesorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Matriculas_CicloId]    Script Date: 16/02/2025 1:17:49 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_Matriculas_CicloId] ON [dbo].[Matriculas]
(
	[CicloId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Matriculas_EstudianteId]    Script Date: 16/02/2025 1:17:49 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_Matriculas_EstudianteId] ON [dbo].[Matriculas]
(
	[EstudianteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PersonasResponsables_EstudianteId]    Script Date: 16/02/2025 1:17:49 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_PersonasResponsables_EstudianteId] ON [dbo].[PersonasResponsables]
(
	[EstudianteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Asistencias]  WITH CHECK ADD  CONSTRAINT [FK_Asistencias_Ciclos_CicloId] FOREIGN KEY([CicloId])
REFERENCES [dbo].[Ciclos] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Asistencias] CHECK CONSTRAINT [FK_Asistencias_Ciclos_CicloId]
GO
ALTER TABLE [dbo].[Asistencias]  WITH CHECK ADD  CONSTRAINT [FK_Asistencias_Estudiantes_EstudianteId] FOREIGN KEY([EstudianteId])
REFERENCES [dbo].[Estudiantes] ([Id])
GO
ALTER TABLE [dbo].[Asistencias] CHECK CONSTRAINT [FK_Asistencias_Estudiantes_EstudianteId]
GO
ALTER TABLE [dbo].[Asistencias]  WITH CHECK ADD  CONSTRAINT [FK_Asistencias_Materias_MateriaId] FOREIGN KEY([MateriaId])
REFERENCES [dbo].[Materias] ([Id])
GO
ALTER TABLE [dbo].[Asistencias] CHECK CONSTRAINT [FK_Asistencias_Materias_MateriaId]
GO
ALTER TABLE [dbo].[Calificaciones]  WITH CHECK ADD  CONSTRAINT [FK_Calificaciones_Ciclos_CicloId] FOREIGN KEY([CicloId])
REFERENCES [dbo].[Ciclos] ([Id])
GO
ALTER TABLE [dbo].[Calificaciones] CHECK CONSTRAINT [FK_Calificaciones_Ciclos_CicloId]
GO
ALTER TABLE [dbo].[Calificaciones]  WITH CHECK ADD  CONSTRAINT [FK_Calificaciones_Estudiantes_EstudianteId] FOREIGN KEY([EstudianteId])
REFERENCES [dbo].[Estudiantes] ([Id])
GO
ALTER TABLE [dbo].[Calificaciones] CHECK CONSTRAINT [FK_Calificaciones_Estudiantes_EstudianteId]
GO
ALTER TABLE [dbo].[Calificaciones]  WITH CHECK ADD  CONSTRAINT [FK_Calificaciones_Materias_MateriaId] FOREIGN KEY([MateriaId])
REFERENCES [dbo].[Materias] ([Id])
GO
ALTER TABLE [dbo].[Calificaciones] CHECK CONSTRAINT [FK_Calificaciones_Materias_MateriaId]
GO
ALTER TABLE [dbo].[CicloMaterias]  WITH CHECK ADD  CONSTRAINT [FK_CicloMaterias_Ciclos_CicloId] FOREIGN KEY([CicloId])
REFERENCES [dbo].[Ciclos] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CicloMaterias] CHECK CONSTRAINT [FK_CicloMaterias_Ciclos_CicloId]
GO
ALTER TABLE [dbo].[CicloMaterias]  WITH CHECK ADD  CONSTRAINT [FK_CicloMaterias_Materias_MateriaId] FOREIGN KEY([MateriaId])
REFERENCES [dbo].[Materias] ([Id])
GO
ALTER TABLE [dbo].[CicloMaterias] CHECK CONSTRAINT [FK_CicloMaterias_Materias_MateriaId]
GO
ALTER TABLE [dbo].[Estudiantes]  WITH CHECK ADD  CONSTRAINT [FK_Estudiantes_Ciclos_CicloId] FOREIGN KEY([CicloId])
REFERENCES [dbo].[Ciclos] ([Id])
GO
ALTER TABLE [dbo].[Estudiantes] CHECK CONSTRAINT [FK_Estudiantes_Ciclos_CicloId]
GO
ALTER TABLE [dbo].[Materias]  WITH CHECK ADD  CONSTRAINT [FK_Materias_Profesores_ProfesorId] FOREIGN KEY([ProfesorId])
REFERENCES [dbo].[Profesores] ([Id])
GO
ALTER TABLE [dbo].[Materias] CHECK CONSTRAINT [FK_Materias_Profesores_ProfesorId]
GO
ALTER TABLE [dbo].[Matriculas]  WITH CHECK ADD  CONSTRAINT [FK_Matriculas_Ciclos_CicloId] FOREIGN KEY([CicloId])
REFERENCES [dbo].[Ciclos] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Matriculas] CHECK CONSTRAINT [FK_Matriculas_Ciclos_CicloId]
GO
ALTER TABLE [dbo].[Matriculas]  WITH CHECK ADD  CONSTRAINT [FK_Matriculas_Estudiantes_EstudianteId] FOREIGN KEY([EstudianteId])
REFERENCES [dbo].[Estudiantes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Matriculas] CHECK CONSTRAINT [FK_Matriculas_Estudiantes_EstudianteId]
GO
ALTER TABLE [dbo].[PersonasResponsables]  WITH CHECK ADD  CONSTRAINT [FK_PersonasResponsables_Estudiantes_EstudianteId] FOREIGN KEY([EstudianteId])
REFERENCES [dbo].[Estudiantes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PersonasResponsables] CHECK CONSTRAINT [FK_PersonasResponsables_Estudiantes_EstudianteId]
GO
USE [master]
GO
ALTER DATABASE [JWSDb] SET  READ_WRITE 
GO
