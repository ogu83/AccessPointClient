USE [master]
GO
/****** Object:  Database [accessControlManagement]    Script Date: 10/04/2015 05:36:32 ******/
CREATE DATABASE [accessControlManagement] ON  PRIMARY 
( NAME = N'accessControlManagement', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLSERVER2008R2\MSSQL\DATA\accessControlManagement.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'accessControlManagement_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLSERVER2008R2\MSSQL\DATA\accessControlManagement_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [accessControlManagement] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [accessControlManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [accessControlManagement] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [accessControlManagement] SET ANSI_NULLS OFF
GO
ALTER DATABASE [accessControlManagement] SET ANSI_PADDING OFF
GO
ALTER DATABASE [accessControlManagement] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [accessControlManagement] SET ARITHABORT OFF
GO
ALTER DATABASE [accessControlManagement] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [accessControlManagement] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [accessControlManagement] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [accessControlManagement] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [accessControlManagement] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [accessControlManagement] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [accessControlManagement] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [accessControlManagement] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [accessControlManagement] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [accessControlManagement] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [accessControlManagement] SET  DISABLE_BROKER
GO
ALTER DATABASE [accessControlManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [accessControlManagement] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [accessControlManagement] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [accessControlManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [accessControlManagement] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [accessControlManagement] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [accessControlManagement] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [accessControlManagement] SET  READ_WRITE
GO
ALTER DATABASE [accessControlManagement] SET RECOVERY FULL
GO
ALTER DATABASE [accessControlManagement] SET  MULTI_USER
GO
ALTER DATABASE [accessControlManagement] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [accessControlManagement] SET DB_CHAINING OFF
GO
USE [accessControlManagement]
GO
/****** Object:  Table [dbo].[department]    Script Date: 10/04/2015 05:36:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[department](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](10) NULL,
 CONSTRAINT [PK_department] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[accessPoint]    Script Date: 10/04/2015 05:36:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[accessPoint](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[IPv4] [nvarchar](50) NOT NULL,
	[IPv6] [nvarchar](50) NOT NULL,
	[Location] [nvarchar](50) NOT NULL,
	[IsOn] [tinyint] NOT NULL,
 CONSTRAINT [PK_accessPoint] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[role]    Script Date: 10/04/2015 05:36:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[IsManager] [int] NOT NULL,
 CONSTRAINT [PK_role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[department_accessPoint]    Script Date: 10/04/2015 05:36:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[department_accessPoint](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccessPoint_Id] [int] NOT NULL,
	[Department_Id] [int] NOT NULL,
 CONSTRAINT [PK_department_accessPoint] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[userGroup]    Script Date: 10/04/2015 05:36:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[userGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](10) NOT NULL,
	[Role_Id] [int] NOT NULL,
 CONSTRAINT [PK_userGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[role_accessPoint]    Script Date: 10/04/2015 05:36:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[role_accessPoint](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Role_Id] [int] NOT NULL,
	[AccessPoint_Id] [int] NOT NULL,
 CONSTRAINT [PK_role_accessPoint] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[userGroup_accessPoint]    Script Date: 10/04/2015 05:36:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[userGroup_accessPoint](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccessPoint_Id] [int] NOT NULL,
	[Group_Id] [int] NOT NULL,
 CONSTRAINT [PK_userGroup_accessPoint] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user]    Script Date: 10/04/2015 05:36:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Role_Id] [int] NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[FullName] [nvarchar](50) NOT NULL,
	[Group_Id] [int] NOT NULL,
	[Phone] [bigint] NOT NULL,
	[WorkStartTime] [time](7) NOT NULL,
	[WorkEndTime] [time](7) NOT NULL,
	[Department_Id] [int] NOT NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[accessLog]    Script Date: 10/04/2015 05:36:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[accessLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Action_Id] [int] NOT NULL,
	[Role_Id] [int] NOT NULL,
	[User_Id] [int] NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[Time] [datetime] NOT NULL,
	[AccessPoint_Id] [int] NOT NULL,
 CONSTRAINT [PK_accessLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_department_accessPoint_accessPoint]    Script Date: 10/04/2015 05:36:36 ******/
ALTER TABLE [dbo].[department_accessPoint]  WITH CHECK ADD  CONSTRAINT [FK_department_accessPoint_accessPoint] FOREIGN KEY([AccessPoint_Id])
REFERENCES [dbo].[accessPoint] ([Id])
GO
ALTER TABLE [dbo].[department_accessPoint] CHECK CONSTRAINT [FK_department_accessPoint_accessPoint]
GO
/****** Object:  ForeignKey [FK_department_accessPoint_department]    Script Date: 10/04/2015 05:36:36 ******/
ALTER TABLE [dbo].[department_accessPoint]  WITH CHECK ADD  CONSTRAINT [FK_department_accessPoint_department] FOREIGN KEY([Department_Id])
REFERENCES [dbo].[department] ([Id])
GO
ALTER TABLE [dbo].[department_accessPoint] CHECK CONSTRAINT [FK_department_accessPoint_department]
GO
/****** Object:  ForeignKey [FK_userGroup_role]    Script Date: 10/04/2015 05:36:36 ******/
ALTER TABLE [dbo].[userGroup]  WITH CHECK ADD  CONSTRAINT [FK_userGroup_role] FOREIGN KEY([Id])
REFERENCES [dbo].[role] ([Id])
GO
ALTER TABLE [dbo].[userGroup] CHECK CONSTRAINT [FK_userGroup_role]
GO
/****** Object:  ForeignKey [FK_role_accessPoint_accessPoint]    Script Date: 10/04/2015 05:36:36 ******/
ALTER TABLE [dbo].[role_accessPoint]  WITH CHECK ADD  CONSTRAINT [FK_role_accessPoint_accessPoint] FOREIGN KEY([AccessPoint_Id])
REFERENCES [dbo].[accessPoint] ([Id])
GO
ALTER TABLE [dbo].[role_accessPoint] CHECK CONSTRAINT [FK_role_accessPoint_accessPoint]
GO
/****** Object:  ForeignKey [FK_role_accessPoint_role]    Script Date: 10/04/2015 05:36:36 ******/
ALTER TABLE [dbo].[role_accessPoint]  WITH CHECK ADD  CONSTRAINT [FK_role_accessPoint_role] FOREIGN KEY([Role_Id])
REFERENCES [dbo].[role] ([Id])
GO
ALTER TABLE [dbo].[role_accessPoint] CHECK CONSTRAINT [FK_role_accessPoint_role]
GO
/****** Object:  ForeignKey [FK_userGroup_accessPoint_accessPoint]    Script Date: 10/04/2015 05:36:36 ******/
ALTER TABLE [dbo].[userGroup_accessPoint]  WITH CHECK ADD  CONSTRAINT [FK_userGroup_accessPoint_accessPoint] FOREIGN KEY([AccessPoint_Id])
REFERENCES [dbo].[accessPoint] ([Id])
GO
ALTER TABLE [dbo].[userGroup_accessPoint] CHECK CONSTRAINT [FK_userGroup_accessPoint_accessPoint]
GO
/****** Object:  ForeignKey [FK_userGroup_accessPoint_userGroup]    Script Date: 10/04/2015 05:36:36 ******/
ALTER TABLE [dbo].[userGroup_accessPoint]  WITH CHECK ADD  CONSTRAINT [FK_userGroup_accessPoint_userGroup] FOREIGN KEY([Group_Id])
REFERENCES [dbo].[userGroup] ([Id])
GO
ALTER TABLE [dbo].[userGroup_accessPoint] CHECK CONSTRAINT [FK_userGroup_accessPoint_userGroup]
GO
/****** Object:  ForeignKey [FK_user_department]    Script Date: 10/04/2015 05:36:36 ******/
ALTER TABLE [dbo].[user]  WITH CHECK ADD  CONSTRAINT [FK_user_department] FOREIGN KEY([Department_Id])
REFERENCES [dbo].[department] ([Id])
GO
ALTER TABLE [dbo].[user] CHECK CONSTRAINT [FK_user_department]
GO
/****** Object:  ForeignKey [FK_user_role]    Script Date: 10/04/2015 05:36:36 ******/
ALTER TABLE [dbo].[user]  WITH CHECK ADD  CONSTRAINT [FK_user_role] FOREIGN KEY([Role_Id])
REFERENCES [dbo].[role] ([Id])
GO
ALTER TABLE [dbo].[user] CHECK CONSTRAINT [FK_user_role]
GO
/****** Object:  ForeignKey [FK_user_userGroup]    Script Date: 10/04/2015 05:36:36 ******/
ALTER TABLE [dbo].[user]  WITH CHECK ADD  CONSTRAINT [FK_user_userGroup] FOREIGN KEY([Group_Id])
REFERENCES [dbo].[userGroup] ([Id])
GO
ALTER TABLE [dbo].[user] CHECK CONSTRAINT [FK_user_userGroup]
GO
/****** Object:  ForeignKey [FK_accessLog_accessPoint]    Script Date: 10/04/2015 05:36:36 ******/
ALTER TABLE [dbo].[accessLog]  WITH CHECK ADD  CONSTRAINT [FK_accessLog_accessPoint] FOREIGN KEY([AccessPoint_Id])
REFERENCES [dbo].[accessPoint] ([Id])
GO
ALTER TABLE [dbo].[accessLog] CHECK CONSTRAINT [FK_accessLog_accessPoint]
GO
/****** Object:  ForeignKey [FK_accessLog_role]    Script Date: 10/04/2015 05:36:36 ******/
ALTER TABLE [dbo].[accessLog]  WITH CHECK ADD  CONSTRAINT [FK_accessLog_role] FOREIGN KEY([Role_Id])
REFERENCES [dbo].[role] ([Id])
GO
ALTER TABLE [dbo].[accessLog] CHECK CONSTRAINT [FK_accessLog_role]
GO
/****** Object:  ForeignKey [FK_accessLog_user]    Script Date: 10/04/2015 05:36:36 ******/
ALTER TABLE [dbo].[accessLog]  WITH CHECK ADD  CONSTRAINT [FK_accessLog_user] FOREIGN KEY([User_Id])
REFERENCES [dbo].[user] ([Id])
GO
ALTER TABLE [dbo].[accessLog] CHECK CONSTRAINT [FK_accessLog_user]
GO
