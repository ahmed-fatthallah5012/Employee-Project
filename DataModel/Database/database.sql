USE [master]
GO
/****** Object:  Database [EmployeeDb]    Script Date: 23-Mar-21 6:46:03 PM ******/
CREATE DATABASE [EmployeeDb]
GO
USE [EmployeeDb]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 23-Mar-21 6:46:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Phone] [nvarchar](11) NULL,
	[Photo] [image] NULL,
	[ParentId] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
