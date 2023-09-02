USE [Inventory]
GO

/****** Object:  Table [dbo].[Products]    Script Date: 8/31/2023 8:03:13 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Products]') AND type in (N'U'))
DROP TABLE [dbo].[Products]
GO

/****** Object:  Table [dbo].[Products]    Script Date: 8/31/2023 8:03:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Products](
	[ProductId] uniqueIdentifier default (newId()) primary key,
	[Category] [nvarchar](20) NULL,
	[Subcategory] [nvarchar](20) NULL,
	[ProductCode] [nvarchar](20) NULL,
	[ProductName] [nvarchar](100) NULL,
	[Quantity] [int] NULL,
	[Price] [decimal](10, 2) NULL,
	[ProductDescription] [nvarchar](max) NULL,
	[ProductImage] [nvarchar](200) NULL)  
GO


