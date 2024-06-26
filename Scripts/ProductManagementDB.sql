USE [ProductManagementDB]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 6/25/2024 8:29:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](30) NULL,
	[Description] [nvarchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 6/25/2024 8:29:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](12) NOT NULL,
	[Email] [nvarchar](30) NULL,
	[Active] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomersItems]    Script Date: 6/25/2024 8:29:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomersItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[ItemId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Items]    Script Date: 6/25/2024 8:29:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Items](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Number] [int] NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[Active] [bit] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Price] [decimal](19, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [Name], [Description]) VALUES (1, N'Sports & Outdoors', N'Weights and fitness material')
INSERT [dbo].[Categories] ([Id], [Name], [Description]) VALUES (3, N'Electronics', NULL)
INSERT [dbo].[Categories] ([Id], [Name], [Description]) VALUES (4, N'Home Appliances', NULL)
INSERT [dbo].[Categories] ([Id], [Name], [Description]) VALUES (5, N'Clothing', NULL)
INSERT [dbo].[Categories] ([Id], [Name], [Description]) VALUES (6, N'Books', NULL)
INSERT [dbo].[Categories] ([Id], [Name], [Description]) VALUES (7, N'Beauty & Personal Care', NULL)
INSERT [dbo].[Categories] ([Id], [Name], [Description]) VALUES (8, N'Toys & Games', NULL)
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Customers] ON 

INSERT [dbo].[Customers] ([Id], [Name], [Phone], [Email], [Active]) VALUES (1, N'Angel Bello', N'8295747432', N'bello@gmail.com', 1)
INSERT [dbo].[Customers] ([Id], [Name], [Phone], [Email], [Active]) VALUES (2, N'Rosa Colón', N'45345324', N'rosa@gmail.com', 1)
INSERT [dbo].[Customers] ([Id], [Name], [Phone], [Email], [Active]) VALUES (3, N'Saiter', N'3453456324', N'saiterbellomateo@gmail.com', 1)
INSERT [dbo].[Customers] ([Id], [Name], [Phone], [Email], [Active]) VALUES (4, N'Anthony Santos', N'22618165', N'asantos@gmail.com', 1)
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO
SET IDENTITY_INSERT [dbo].[CustomersItems] ON 

INSERT [dbo].[CustomersItems] ([Id], [CustomerId], [ItemId]) VALUES (9, 1, 6)
INSERT [dbo].[CustomersItems] ([Id], [CustomerId], [ItemId]) VALUES (10, 3, 6)
INSERT [dbo].[CustomersItems] ([Id], [CustomerId], [ItemId]) VALUES (12, 2, 4)
INSERT [dbo].[CustomersItems] ([Id], [CustomerId], [ItemId]) VALUES (13, 3, 8)
INSERT [dbo].[CustomersItems] ([Id], [CustomerId], [ItemId]) VALUES (14, 3, 7)
INSERT [dbo].[CustomersItems] ([Id], [CustomerId], [ItemId]) VALUES (15, 3, 5)
INSERT [dbo].[CustomersItems] ([Id], [CustomerId], [ItemId]) VALUES (16, 1, 4)
INSERT [dbo].[CustomersItems] ([Id], [CustomerId], [ItemId]) VALUES (17, 1, 5)
INSERT [dbo].[CustomersItems] ([Id], [CustomerId], [ItemId]) VALUES (18, 1, 8)
INSERT [dbo].[CustomersItems] ([Id], [CustomerId], [ItemId]) VALUES (19, 4, 8)
SET IDENTITY_INSERT [dbo].[CustomersItems] OFF
GO
SET IDENTITY_INSERT [dbo].[Items] ON 

INSERT [dbo].[Items] ([Id], [Name], [Number], [Description], [Active], [CategoryId], [Price]) VALUES (4, N'The Bible', 34, N'Holy ', 1, 7, CAST(300.00 AS Decimal(19, 2)))
INSERT [dbo].[Items] ([Id], [Name], [Number], [Description], [Active], [CategoryId], [Price]) VALUES (5, N'Iphone 15', 234, N'Brand new Iphone', 1, 3, CAST(560.00 AS Decimal(19, 2)))
INSERT [dbo].[Items] ([Id], [Name], [Number], [Description], [Active], [CategoryId], [Price]) VALUES (6, N'Speaker', 23, N'HQ Speakers', 1, 3, CAST(560.00 AS Decimal(19, 2)))
INSERT [dbo].[Items] ([Id], [Name], [Number], [Description], [Active], [CategoryId], [Price]) VALUES (7, N'Cap', 46, N'Goat cap', 1, 5, CAST(12.00 AS Decimal(19, 2)))
INSERT [dbo].[Items] ([Id], [Name], [Number], [Description], [Active], [CategoryId], [Price]) VALUES (8, N'Keychain', 78, N'Great keychain', 1, 1, CAST(17.00 AS Decimal(19, 2)))
SET IDENTITY_INSERT [dbo].[Items] OFF
GO
/****** Object:  Index [UQ__Item__78A1A19D16969BCC]    Script Date: 6/25/2024 8:29:42 PM ******/
ALTER TABLE [dbo].[Items] ADD UNIQUE NONCLUSTERED 
(
	[Number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CustomersItems]  WITH CHECK ADD FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
GO
ALTER TABLE [dbo].[CustomersItems]  WITH CHECK ADD FOREIGN KEY([ItemId])
REFERENCES [dbo].[Items] ([Id])
GO
ALTER TABLE [dbo].[Items]  WITH CHECK ADD FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
GO
