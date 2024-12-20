USE [master]
GO
/****** Object:  Database [StoreManagementProject]    Script Date: 16/05/2024 3:06:06 PM ******/
CREATE DATABASE [StoreManagementProject]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'StoreManagementProject', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\StoreManagementProject.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'StoreManagementProject_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\StoreManagementProject_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [StoreManagementProject] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [StoreManagementProject].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [StoreManagementProject] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [StoreManagementProject] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [StoreManagementProject] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [StoreManagementProject] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [StoreManagementProject] SET ARITHABORT OFF 
GO
ALTER DATABASE [StoreManagementProject] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [StoreManagementProject] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [StoreManagementProject] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [StoreManagementProject] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [StoreManagementProject] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [StoreManagementProject] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [StoreManagementProject] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [StoreManagementProject] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [StoreManagementProject] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [StoreManagementProject] SET  ENABLE_BROKER 
GO
ALTER DATABASE [StoreManagementProject] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [StoreManagementProject] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [StoreManagementProject] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [StoreManagementProject] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [StoreManagementProject] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [StoreManagementProject] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [StoreManagementProject] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [StoreManagementProject] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [StoreManagementProject] SET  MULTI_USER 
GO
ALTER DATABASE [StoreManagementProject] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [StoreManagementProject] SET DB_CHAINING OFF 
GO
ALTER DATABASE [StoreManagementProject] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [StoreManagementProject] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [StoreManagementProject] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [StoreManagementProject] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [StoreManagementProject] SET QUERY_STORE = ON
GO
ALTER DATABASE [StoreManagementProject] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [StoreManagementProject]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 16/05/2024 3:06:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[AccountID] [int] NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[Username] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[Role] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 16/05/2024 3:06:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryID] [int] NOT NULL,
	[CategoryName] [nvarchar](100) NULL,
	[Description] [nvarchar](max) NULL,
	[Deleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 16/05/2024 3:06:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerID] [int] NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Email] [nvarchar](100) NULL,
	[Phone] [nvarchar](20) NULL,
	[Address] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 16/05/2024 3:06:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmployeeID] [int] NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Email] [nvarchar](100) NULL,
	[Phone] [nvarchar](20) NULL,
	[Address] [nvarchar](255) NULL,
	[Position] [nvarchar](50) NULL,
	[Salary] [decimal](10, 2) NULL,
	[HireDate] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 16/05/2024 3:06:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[OrderDetailID] [int] NOT NULL,
	[ShippingAddress] [nvarchar](255) NULL,
	[PaymentMethod] [nvarchar](50) NULL,
	[OrderID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderItems]    Script Date: 16/05/2024 3:06:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItems](
	[OrderItemID] [int] NOT NULL,
	[OrderID] [int] NULL,
	[ProductID] [int] NULL,
	[Quantity] [int] NULL,
	[UnitPrice] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 16/05/2024 3:06:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderID] [int] NOT NULL,
	[CustomerID] [int] NULL,
	[OrderDate] [datetime] NULL,
	[TotalAmount] [decimal](10, 2) NULL,
	[Status] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductDetails]    Script Date: 16/05/2024 3:06:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductDetails](
	[DetailID] [int] NOT NULL,
	[ProductID] [int] NULL,
	[Size] [nvarchar](50) NULL,
	[Color] [nvarchar](50) NULL,
	[Weight] [decimal](10, 2) NULL,
	[OtherDetails] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[DetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 16/05/2024 3:06:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductID] [int] NOT NULL,
	[ProductName] [nvarchar](100) NULL,
	[Description] [nvarchar](max) NULL,
	[CategoryID] [int] NULL,
	[ProviderID] [int] NULL,
	[UnitPrice] [decimal](10, 2) NULL,
	[StockQuantity] [int] NULL,
	[Deleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Providers]    Script Date: 16/05/2024 3:06:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Providers](
	[ProviderID] [int] NOT NULL,
	[ProviderName] [nvarchar](100) NULL,
	[ContactPerson] [nvarchar](100) NULL,
	[Email] [nvarchar](100) NULL,
	[Phone] [nvarchar](20) NULL,
	[Address] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ProviderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WarehouseDispatchItems]    Script Date: 16/05/2024 3:06:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WarehouseDispatchItems](
	[WarehouseDispatchItemID] [int] NOT NULL,
	[WarehouseDispatchID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Quantity] [int] NULL,
	[UnitPrice] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[WarehouseDispatchItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WarehouseDispatchs]    Script Date: 16/05/2024 3:06:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WarehouseDispatchs](
	[WarehouseDispatchID] [int] NOT NULL,
	[DispatchDate] [datetime] NULL,
	[TotalQuantity] [int] NULL,
	[TotalAmount] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[WarehouseDispatchID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WarehouseReceiptItems]    Script Date: 16/05/2024 3:06:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WarehouseReceiptItems](
	[WarehouseReceiptItemID] [int] NOT NULL,
	[WarehouseReceiptID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Quantity] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[WarehouseReceiptItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WarehouseReceipts]    Script Date: 16/05/2024 3:06:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WarehouseReceipts](
	[WarehouseReceiptID] [int] NOT NULL,
	[ReceiptDate] [datetime] NULL,
	[TotalQuantity] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[WarehouseReceiptID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Categories] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employees] ([EmployeeID])
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([OrderID])
GO
ALTER TABLE [dbo].[OrderItems]  WITH CHECK ADD FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([OrderID])
GO
ALTER TABLE [dbo].[OrderItems]  WITH CHECK ADD FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customers] ([CustomerID])
GO
ALTER TABLE [dbo].[ProductDetails]  WITH CHECK ADD FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Categories] ([CategoryID])
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD FOREIGN KEY([ProviderID])
REFERENCES [dbo].[Providers] ([ProviderID])
GO
ALTER TABLE [dbo].[WarehouseDispatchItems]  WITH CHECK ADD  CONSTRAINT [FK_WarehouseDispatchItem_Products] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[WarehouseDispatchItems] CHECK CONSTRAINT [FK_WarehouseDispatchItem_Products]
GO
ALTER TABLE [dbo].[WarehouseDispatchItems]  WITH CHECK ADD  CONSTRAINT [FK_WarehouseDispatchItem_WarehouseDispatchs] FOREIGN KEY([WarehouseDispatchID])
REFERENCES [dbo].[WarehouseDispatchs] ([WarehouseDispatchID])
GO
ALTER TABLE [dbo].[WarehouseDispatchItems] CHECK CONSTRAINT [FK_WarehouseDispatchItem_WarehouseDispatchs]
GO
ALTER TABLE [dbo].[WarehouseReceiptItems]  WITH CHECK ADD  CONSTRAINT [FK_WarehouseReceiptItem_Products] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[WarehouseReceiptItems] CHECK CONSTRAINT [FK_WarehouseReceiptItem_Products]
GO
ALTER TABLE [dbo].[WarehouseReceiptItems]  WITH CHECK ADD  CONSTRAINT [FK_WarehouseReceiptItem_WarehouseReceipt] FOREIGN KEY([WarehouseReceiptID])
REFERENCES [dbo].[WarehouseReceipts] ([WarehouseReceiptID])
GO
ALTER TABLE [dbo].[WarehouseReceiptItems] CHECK CONSTRAINT [FK_WarehouseReceiptItem_WarehouseReceipt]
GO
USE [master]
GO
ALTER DATABASE [StoreManagementProject] SET  READ_WRITE 
GO
