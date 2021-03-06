﻿USE [master]
GO

/****** Object:  Database [Logistic]    Script Date: 11/23/2020 12:17:55 PM ******/
CREATE DATABASE [Logistic]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Logistic', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\Logistic.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Logistic_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\Logistic_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

ALTER DATABASE [Logistic] SET COMPATIBILITY_LEVEL = 130
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Logistic].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [Logistic] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [Logistic] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [Logistic] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [Logistic] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [Logistic] SET ARITHABORT OFF 
GO

ALTER DATABASE [Logistic] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [Logistic] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [Logistic] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [Logistic] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [Logistic] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [Logistic] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [Logistic] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [Logistic] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [Logistic] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [Logistic] SET  DISABLE_BROKER 
GO

ALTER DATABASE [Logistic] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [Logistic] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [Logistic] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [Logistic] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [Logistic] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [Logistic] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [Logistic] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [Logistic] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [Logistic] SET  MULTI_USER 
GO

ALTER DATABASE [Logistic] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [Logistic] SET DB_CHAINING OFF 
GO

ALTER DATABASE [Logistic] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [Logistic] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [Logistic] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [Logistic] SET QUERY_STORE = OFF
GO

USE [Logistic]
GO

ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO

ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO

ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO

ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO

ALTER DATABASE [Logistic] SET  READ_WRITE 
GO