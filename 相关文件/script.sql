USE [SurveyDB]
GO
/****** Object:  Table [dbo].[ActiveJobs]    Script Date: 2019/11/21 20:01:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActiveJobs](
	[JobID] [int] IDENTITY(1,1) NOT NULL,
	[JobName] [nvarchar](50) NULL,
	[JobType] [nvarchar](20) NULL,
	[JobDesc] [nvarchar](2000) NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[SurveyID] [int] NULL,
	[PositionID] [int] NULL,
	[Count] [int] NULL,
	[AvageMoney] [decimal](18, 2) NULL,
 CONSTRAINT [PK_ACTIVEJOBS] PRIMARY KEY CLUSTERED 
(
	[JobID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Company]    Script Date: 2019/11/21 20:01:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Company](
	[CompanyID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] [nvarchar](50) NULL,
	[CompanyType] [nvarchar](20) NULL,
	[CompanyDesc] [nvarchar](4000) NULL,
	[Status] [int] NULL,
	[DepartName] [nvarchar](50) NULL,
	[City] [nvarchar](10) NULL,
	[CompanyNo] [varchar](50) NULL,
 CONSTRAINT [PK_COMPANY] PRIMARY KEY CLUSTERED 
(
	[CompanyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Department]    Script Date: 2019/11/21 20:01:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[ID] [int] NULL,
	[DepartName] [nvarchar](20) NULL,
	[DepartDesc] [nvarchar](2000) NULL,
	[CreateTime] [datetime] NULL,
	[Status] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Dictionary]    Script Date: 2019/11/21 20:01:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dictionary](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NULL,
	[Value] [nvarchar](100) NULL,
	[Type] [nvarchar](20) NULL,
	[Status] [int] NULL,
	[ParentID] [int] NULL,
 CONSTRAINT [PK_DICTIONARY] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Menus]    Script Date: 2019/11/21 20:01:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menus](
	[MenuID] [int] IDENTITY(1,1) NOT NULL,
	[MenuName] [nvarchar](20) NULL,
	[MenuType] [nvarchar](20) NULL,
	[Status] [int] NULL,
	[ParentID] [int] NULL,
	[Order] [int] NULL,
 CONSTRAINT [PK_MENUS] PRIMARY KEY CLUSTERED 
(
	[MenuID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Position]    Script Date: 2019/11/21 20:01:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Position](
	[PositionID] [int] IDENTITY(1,1) NOT NULL,
	[SurveyID] [int] NULL,
	[PositionName] [nvarchar](20) NULL,
	[PositionType] [nvarchar](20) NULL,
	[PositionDesc] [nvarchar](2000) NULL,
	[Status] [int] NULL,
	[CompanyID] [int] NULL,
	[Source] [nvarchar](50) NULL,
	[Years] [nvarchar](50) NULL,
 CONSTRAINT [PK_POSITION] PRIMARY KEY CLUSTERED 
(
	[PositionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Project]    Script Date: 2019/11/21 20:01:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Project](
	[ProjectID] [int] IDENTITY(1,1) NOT NULL,
	[SurveyID] [int] NULL,
	[ProjectName] [varchar](100) NULL,
	[ProjectType] [varchar](10) NULL,
	[ProjectTypeID] [int] NULL,
	[ProjectDesc] [varchar](3000) NULL,
	[BeginTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
 CONSTRAINT [PK_PROJECT] PRIMARY KEY CLUSTERED 
(
	[ProjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RoleMenus]    Script Date: 2019/11/21 20:01:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleMenus](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MenuID] [int] NULL,
	[RoleID] [int] NULL,
 CONSTRAINT [PK_ROLEMENUS] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Roles]    Script Date: 2019/11/21 20:01:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](20) NULL,
	[RoleDesc] [nvarchar](100) NULL,
	[Status] [int] NULL,
	[CreateTime] [datetime] NULL,
	[AlterTime] [datetime] NULL,
 CONSTRAINT [PK_ROLES] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SkillTag]    Script Date: 2019/11/21 20:01:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SkillTag](
	[TagID] [int] IDENTITY(1,1) NOT NULL,
	[TagName] [nvarchar](20) NULL,
	[TagType] [nvarchar](20) NULL,
	[CourseName] [nvarchar](20) NULL,
	[CourseID] [int] NULL,
	[Direction] [nvarchar](20) NULL,
	[DirectionID] [int] NULL,
	[Creatime] [datetime] NULL,
 CONSTRAINT [PK_SKILLTAG] PRIMARY KEY CLUSTERED 
(
	[TagID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SkillTags]    Script Date: 2019/11/21 20:01:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SkillTags](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TagID] [int] NULL,
	[PositionID] [int] NULL,
 CONSTRAINT [PK_SKILLTAGS] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Student]    Script Date: 2019/11/21 20:01:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Student](
	[StuNo] [int] IDENTITY(1,1) NOT NULL,
	[StuName] [nvarchar](50) NULL,
	[ClassNo] [varchar](50) NULL,
	[City] [nvarchar](10) NULL,
	[Phone] [varchar](13) NULL,
	[Address] [nvarchar](200) NULL,
	[WagesOfPeriod] [decimal](18, 0) NULL,
	[WagesOfFull] [decimal](18, 0) NULL,
	[WagesOfReal] [decimal](18, 0) NULL,
	[Status] [int] NULL,
	[CreateTime] [datetime] NULL,
	[AlterTime] [datetime] NULL,
	[CurrentCompanyNo] [varchar](50) NULL,
	[Year] [int] NULL,
	[Batch] [varchar](20) NULL,
 CONSTRAINT [PK_STUDENT] PRIMARY KEY CLUSTERED 
(
	[StuNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[StudentTags]    Script Date: 2019/11/21 20:01:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentTags](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StuNo] [int] NULL,
	[TagID] [int] NULL,
	[SelfPoint] [int] NULL,
	[TeacherPoint] [int] NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_STUDENTTAGS] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SurveyModel]    Script Date: 2019/11/21 20:01:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SurveyModel](
	[SurveyID] [int] IDENTITY(1,1) NOT NULL,
	[SurveyTickNumber] [varchar](50) NULL,
	[StuNo] [int] NULL,
	[CompanyID] [int] NULL,
	[StuName] [nvarchar](10) NULL,
	[CompanyNo] [nvarchar](64) NULL,
	[WagesOfPeriod] [decimal](18, 0) NULL,
	[WagesOfFull] [decimal](18, 0) NULL,
	[WagesOfReal] [decimal](18, 0) NULL,
	[Status] [int] NULL,
	[CreateTime] [datetime] NULL,
	[Year] [int] NULL,
	[Batch] [nvarchar](50) NULL,
	[Direction] [nvarchar](10) NULL,
	[ProjectName] [nvarchar](50) NULL,
	[Phone] [nchar](14) NULL,
	[CorworkPhone] [nchar](14) NULL,
	[PositionName] [nvarchar](50) NULL,
	[AuditName] [nvarchar](50) NULL,
	[AuditStatus] [int] NULL,
	[School] [nvarchar](50) NULL,
	[DepartName] [nvarchar](50) NULL,
	[Type] [int] NULL,
 CONSTRAINT [PK_SURVEY] PRIMARY KEY CLUSTERED 
(
	[SurveyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 2019/11/21 20:01:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleID] [int] NULL,
	[UserID] [int] NULL,
 CONSTRAINT [PK_USERROLES] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 2019/11/21 20:01:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](20) NULL,
	[NickName] [nvarchar](20) NULL,
	[ImageUrl] [nvarchar](100) NULL,
	[Status] [int] NULL,
	[CreateTime] [datetime] NULL,
	[DepartID] [int] NULL,
 CONSTRAINT [PK_USERS] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[ActiveJobs] ON 

INSERT [dbo].[ActiveJobs] ([JobID], [JobName], [JobType], [JobDesc], [StartTime], [EndTime], [SurveyID], [PositionID], [Count], [AvageMoney]) VALUES (1, N'工程师', N'NET', N'net 工程师', CAST(N'2019-11-14 19:46:32.470' AS DateTime), CAST(N'2019-11-14 19:46:32.470' AS DateTime), 3, NULL, NULL, NULL)
INSERT [dbo].[ActiveJobs] ([JobID], [JobName], [JobType], [JobDesc], [StartTime], [EndTime], [SurveyID], [PositionID], [Count], [AvageMoney]) VALUES (2, N'工程师', N'NET', N'net 工程师', CAST(N'2019-11-14 19:52:49.563' AS DateTime), CAST(N'2019-11-14 19:52:49.563' AS DateTime), 4, NULL, NULL, NULL)
INSERT [dbo].[ActiveJobs] ([JobID], [JobName], [JobType], [JobDesc], [StartTime], [EndTime], [SurveyID], [PositionID], [Count], [AvageMoney]) VALUES (3, N'工程师', N'NET', N'net 工程师', CAST(N'2019-11-14 19:55:46.133' AS DateTime), CAST(N'2019-11-14 19:55:46.133' AS DateTime), 5, NULL, NULL, NULL)
INSERT [dbo].[ActiveJobs] ([JobID], [JobName], [JobType], [JobDesc], [StartTime], [EndTime], [SurveyID], [PositionID], [Count], [AvageMoney]) VALUES (4, N'工程师', N'NET', N'net 工程师', CAST(N'2019-11-14 20:02:00.917' AS DateTime), CAST(N'2019-11-14 20:02:00.917' AS DateTime), 6, NULL, NULL, NULL)
INSERT [dbo].[ActiveJobs] ([JobID], [JobName], [JobType], [JobDesc], [StartTime], [EndTime], [SurveyID], [PositionID], [Count], [AvageMoney]) VALUES (5, N'2工程师', N'2NET', N'2net 工程师', CAST(N'2019-11-15 10:38:16.917' AS DateTime), CAST(N'2019-11-15 10:38:16.917' AS DateTime), 7, NULL, NULL, NULL)
INSERT [dbo].[ActiveJobs] ([JobID], [JobName], [JobType], [JobDesc], [StartTime], [EndTime], [SurveyID], [PositionID], [Count], [AvageMoney]) VALUES (6, N'工程师', N'NET', N'net 工程师', CAST(N'2019-11-15 10:38:16.917' AS DateTime), CAST(N'2019-11-15 10:38:16.917' AS DateTime), 7, NULL, NULL, NULL)
INSERT [dbo].[ActiveJobs] ([JobID], [JobName], [JobType], [JobDesc], [StartTime], [EndTime], [SurveyID], [PositionID], [Count], [AvageMoney]) VALUES (7, N'2工程师', N'2NET', N'2net 工程师', CAST(N'2019-11-15 10:48:06.423' AS DateTime), CAST(N'2019-11-15 10:48:06.423' AS DateTime), 8, NULL, NULL, NULL)
INSERT [dbo].[ActiveJobs] ([JobID], [JobName], [JobType], [JobDesc], [StartTime], [EndTime], [SurveyID], [PositionID], [Count], [AvageMoney]) VALUES (8, N'工程师', N'NET', N'net 工程师', CAST(N'2019-11-15 10:48:06.423' AS DateTime), CAST(N'2019-11-15 10:48:06.423' AS DateTime), 8, NULL, NULL, NULL)
INSERT [dbo].[ActiveJobs] ([JobID], [JobName], [JobType], [JobDesc], [StartTime], [EndTime], [SurveyID], [PositionID], [Count], [AvageMoney]) VALUES (9, N'2工程师', N'2NET', N'2net 工程师', CAST(N'2019-11-15 15:12:36.587' AS DateTime), CAST(N'2019-11-15 15:12:36.587' AS DateTime), 9, NULL, NULL, NULL)
INSERT [dbo].[ActiveJobs] ([JobID], [JobName], [JobType], [JobDesc], [StartTime], [EndTime], [SurveyID], [PositionID], [Count], [AvageMoney]) VALUES (10, N'工程师', N'NET', N'net 工程师', CAST(N'2019-11-15 15:12:36.587' AS DateTime), CAST(N'2019-11-15 15:12:36.587' AS DateTime), 9, NULL, NULL, NULL)
INSERT [dbo].[ActiveJobs] ([JobID], [JobName], [JobType], [JobDesc], [StartTime], [EndTime], [SurveyID], [PositionID], [Count], [AvageMoney]) VALUES (11, N'2工程师', N'2NET', N'2net 工程师', CAST(N'2019-11-15 15:14:38.420' AS DateTime), CAST(N'2019-11-15 15:14:38.420' AS DateTime), 10, NULL, NULL, NULL)
INSERT [dbo].[ActiveJobs] ([JobID], [JobName], [JobType], [JobDesc], [StartTime], [EndTime], [SurveyID], [PositionID], [Count], [AvageMoney]) VALUES (12, N'工程师', N'NET', N'net 工程师', CAST(N'2019-11-15 15:14:38.420' AS DateTime), CAST(N'2019-11-15 15:14:38.420' AS DateTime), 10, NULL, NULL, NULL)
INSERT [dbo].[ActiveJobs] ([JobID], [JobName], [JobType], [JobDesc], [StartTime], [EndTime], [SurveyID], [PositionID], [Count], [AvageMoney]) VALUES (13, N'2工程师', N'2NET', N'2net 工程师', CAST(N'2019-11-15 15:24:44.933' AS DateTime), CAST(N'2019-11-15 15:24:44.933' AS DateTime), 11, NULL, NULL, NULL)
INSERT [dbo].[ActiveJobs] ([JobID], [JobName], [JobType], [JobDesc], [StartTime], [EndTime], [SurveyID], [PositionID], [Count], [AvageMoney]) VALUES (14, N'工程师', N'NET', N'net 工程师', CAST(N'2019-11-15 15:24:44.933' AS DateTime), CAST(N'2019-11-15 15:24:44.933' AS DateTime), 11, NULL, NULL, NULL)
INSERT [dbo].[ActiveJobs] ([JobID], [JobName], [JobType], [JobDesc], [StartTime], [EndTime], [SurveyID], [PositionID], [Count], [AvageMoney]) VALUES (15, N'NET工程师', N'NET', N'asp.net', NULL, NULL, 27, NULL, 1, CAST(4000.00 AS Decimal(18, 2)))
INSERT [dbo].[ActiveJobs] ([JobID], [JobName], [JobType], [JobDesc], [StartTime], [EndTime], [SurveyID], [PositionID], [Count], [AvageMoney]) VALUES (16, N'JAVA工程师', N'NET', N'java', NULL, NULL, 27, NULL, 1, CAST(4000.00 AS Decimal(18, 2)))
INSERT [dbo].[ActiveJobs] ([JobID], [JobName], [JobType], [JobDesc], [StartTime], [EndTime], [SurveyID], [PositionID], [Count], [AvageMoney]) VALUES (17, N'java', N'NET', N'd ', CAST(N'2019-05-11 00:00:00.000' AS DateTime), NULL, 29, NULL, 1, CAST(4000.00 AS Decimal(18, 2)))
INSERT [dbo].[ActiveJobs] ([JobID], [JobName], [JobType], [JobDesc], [StartTime], [EndTime], [SurveyID], [PositionID], [Count], [AvageMoney]) VALUES (18, N'java', N'NET', N'd ', CAST(N'2019-05-11 00:00:00.000' AS DateTime), NULL, 30, NULL, 1, CAST(4000.00 AS Decimal(18, 2)))
INSERT [dbo].[ActiveJobs] ([JobID], [JobName], [JobType], [JobDesc], [StartTime], [EndTime], [SurveyID], [PositionID], [Count], [AvageMoney]) VALUES (19, N'发送', N'NET', N'地方 ', CAST(N'2019-11-06 00:00:00.000' AS DateTime), CAST(N'2019-11-14 00:00:00.000' AS DateTime), 31, NULL, 4, CAST(3000.00 AS Decimal(18, 2)))
INSERT [dbo].[ActiveJobs] ([JobID], [JobName], [JobType], [JobDesc], [StartTime], [EndTime], [SurveyID], [PositionID], [Count], [AvageMoney]) VALUES (20, N'发送2', N'NET', N'地方 ', CAST(N'2019-11-06 00:00:00.000' AS DateTime), CAST(N'2019-11-14 00:00:00.000' AS DateTime), 31, NULL, 4, CAST(3000.00 AS Decimal(18, 2)))
INSERT [dbo].[ActiveJobs] ([JobID], [JobName], [JobType], [JobDesc], [StartTime], [EndTime], [SurveyID], [PositionID], [Count], [AvageMoney]) VALUES (21, N'Net', N'NET', N'dfsdd', CAST(N'2019-11-13 00:00:00.000' AS DateTime), CAST(N'2019-11-21 00:00:00.000' AS DateTime), 33, NULL, 1, CAST(3232.00 AS Decimal(18, 2)))
INSERT [dbo].[ActiveJobs] ([JobID], [JobName], [JobType], [JobDesc], [StartTime], [EndTime], [SurveyID], [PositionID], [Count], [AvageMoney]) VALUES (22, N'Net2', N'NET', N'dfsdd', CAST(N'2019-11-13 00:00:00.000' AS DateTime), CAST(N'2019-11-21 00:00:00.000' AS DateTime), 33, NULL, 1, CAST(3232.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[ActiveJobs] OFF
SET IDENTITY_INSERT [dbo].[Company] ON 

INSERT [dbo].[Company] ([CompanyID], [CompanyName], [CompanyType], [CompanyDesc], [Status], [DepartName], [City], [CompanyNo]) VALUES (1, N'河南厚溥有限公司', N'互联网信息服务', N'NET190901', NULL, NULL, N'郑州', N'2019')
INSERT [dbo].[Company] ([CompanyID], [CompanyName], [CompanyType], [CompanyDesc], [Status], [DepartName], [City], [CompanyNo]) VALUES (2, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Company] ([CompanyID], [CompanyName], [CompanyType], [CompanyDesc], [Status], [DepartName], [City], [CompanyNo]) VALUES (3, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Company] ([CompanyID], [CompanyName], [CompanyType], [CompanyDesc], [Status], [DepartName], [City], [CompanyNo]) VALUES (4, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Company] ([CompanyID], [CompanyName], [CompanyType], [CompanyDesc], [Status], [DepartName], [City], [CompanyNo]) VALUES (5, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Company] ([CompanyID], [CompanyName], [CompanyType], [CompanyDesc], [Status], [DepartName], [City], [CompanyNo]) VALUES (6, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Company] ([CompanyID], [CompanyName], [CompanyType], [CompanyDesc], [Status], [DepartName], [City], [CompanyNo]) VALUES (7, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Company] ([CompanyID], [CompanyName], [CompanyType], [CompanyDesc], [Status], [DepartName], [City], [CompanyNo]) VALUES (8, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Company] ([CompanyID], [CompanyName], [CompanyType], [CompanyDesc], [Status], [DepartName], [City], [CompanyNo]) VALUES (9, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Company] ([CompanyID], [CompanyName], [CompanyType], [CompanyDesc], [Status], [DepartName], [City], [CompanyNo]) VALUES (10, N'联想科技', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Company] ([CompanyID], [CompanyName], [CompanyType], [CompanyDesc], [Status], [DepartName], [City], [CompanyNo]) VALUES (11, N'联想科技', N'IT服务', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Company] ([CompanyID], [CompanyName], [CompanyType], [CompanyDesc], [Status], [DepartName], [City], [CompanyNo]) VALUES (12, N'联想科技', N'IT服务', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Company] ([CompanyID], [CompanyName], [CompanyType], [CompanyDesc], [Status], [DepartName], [City], [CompanyNo]) VALUES (13, N'联想科技', N'IT服务', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Company] ([CompanyID], [CompanyName], [CompanyType], [CompanyDesc], [Status], [DepartName], [City], [CompanyNo]) VALUES (14, N'联想科技', N'iefu', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Company] ([CompanyID], [CompanyName], [CompanyType], [CompanyDesc], [Status], [DepartName], [City], [CompanyNo]) VALUES (15, N'df', N'df', N'dfd', NULL, NULL, NULL, NULL)
INSERT [dbo].[Company] ([CompanyID], [CompanyName], [CompanyType], [CompanyDesc], [Status], [DepartName], [City], [CompanyNo]) VALUES (16, N'联想科技', N'IT服务', N'产品研发，项目开发', NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Company] OFF
SET IDENTITY_INSERT [dbo].[Dictionary] ON 

INSERT [dbo].[Dictionary] ([ID], [Name], [Value], [Type], [Status], [ParentID]) VALUES (1, N'NET', N'NET', N'方向', NULL, NULL)
INSERT [dbo].[Dictionary] ([ID], [Name], [Value], [Type], [Status], [ParentID]) VALUES (2, N'Java', N'Java', N'方向', NULL, NULL)
INSERT [dbo].[Dictionary] ([ID], [Name], [Value], [Type], [Status], [ParentID]) VALUES (3, N'网销', N'网销', N'方向', NULL, NULL)
INSERT [dbo].[Dictionary] ([ID], [Name], [Value], [Type], [Status], [ParentID]) VALUES (4, N'大数据', N'大数据', N'方向', NULL, NULL)
INSERT [dbo].[Dictionary] ([ID], [Name], [Value], [Type], [Status], [ParentID]) VALUES (5, N'企信', N'企信', N'方向', NULL, NULL)
INSERT [dbo].[Dictionary] ([ID], [Name], [Value], [Type], [Status], [ParentID]) VALUES (6, N'前端', N'前端', N'方向', NULL, NULL)
INSERT [dbo].[Dictionary] ([ID], [Name], [Value], [Type], [Status], [ParentID]) VALUES (7, N'北京', N'北京', N'城市', NULL, NULL)
INSERT [dbo].[Dictionary] ([ID], [Name], [Value], [Type], [Status], [ParentID]) VALUES (8, N'上海', N'上海', N'城市', NULL, NULL)
INSERT [dbo].[Dictionary] ([ID], [Name], [Value], [Type], [Status], [ParentID]) VALUES (9, N'广州', N'广州', N'城市', NULL, NULL)
INSERT [dbo].[Dictionary] ([ID], [Name], [Value], [Type], [Status], [ParentID]) VALUES (10, N'郑州', N'郑州', N'城市', NULL, NULL)
INSERT [dbo].[Dictionary] ([ID], [Name], [Value], [Type], [Status], [ParentID]) VALUES (11, N'西安', N'西安', N'城市', NULL, NULL)
INSERT [dbo].[Dictionary] ([ID], [Name], [Value], [Type], [Status], [ParentID]) VALUES (12, N'成都', N'成都', N'城市', NULL, NULL)
INSERT [dbo].[Dictionary] ([ID], [Name], [Value], [Type], [Status], [ParentID]) VALUES (13, N'OA办公', N'OA', N'项目类型', NULL, NULL)
INSERT [dbo].[Dictionary] ([ID], [Name], [Value], [Type], [Status], [ParentID]) VALUES (14, N'CMS', N'CMS', N'项目类型', NULL, NULL)
INSERT [dbo].[Dictionary] ([ID], [Name], [Value], [Type], [Status], [ParentID]) VALUES (15, N'ERP', N'ERP', N'项目类型', NULL, NULL)
INSERT [dbo].[Dictionary] ([ID], [Name], [Value], [Type], [Status], [ParentID]) VALUES (16, N'数据分析系统', N'DAS', N'项目类型', NULL, NULL)
INSERT [dbo].[Dictionary] ([ID], [Name], [Value], [Type], [Status], [ParentID]) VALUES (17, N'智联招聘', N'智联招聘', N'来源', NULL, NULL)
INSERT [dbo].[Dictionary] ([ID], [Name], [Value], [Type], [Status], [ParentID]) VALUES (18, N'前程无忧', N'前程无忧', N'来源', NULL, NULL)
INSERT [dbo].[Dictionary] ([ID], [Name], [Value], [Type], [Status], [ParentID]) VALUES (19, N'猎聘网', N'猎聘网', N'来源', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Dictionary] OFF
SET IDENTITY_INSERT [dbo].[Position] ON 

INSERT [dbo].[Position] ([PositionID], [SurveyID], [PositionName], [PositionType], [PositionDesc], [Status], [CompanyID], [Source], [Years]) VALUES (1, 2, N'NET工程师', N'NET', N'.NET工程师', NULL, NULL, NULL, NULL)
INSERT [dbo].[Position] ([PositionID], [SurveyID], [PositionName], [PositionType], [PositionDesc], [Status], [CompanyID], [Source], [Years]) VALUES (2, 3, N'NET工程师', N'NET', N'.NET工程师', NULL, NULL, NULL, NULL)
INSERT [dbo].[Position] ([PositionID], [SurveyID], [PositionName], [PositionType], [PositionDesc], [Status], [CompanyID], [Source], [Years]) VALUES (3, 4, N'NET工程师', N'NET', N'.NET工程师', NULL, NULL, NULL, NULL)
INSERT [dbo].[Position] ([PositionID], [SurveyID], [PositionName], [PositionType], [PositionDesc], [Status], [CompanyID], [Source], [Years]) VALUES (4, 5, N'NET工程师', N'NET', N'.NET工程师', NULL, NULL, NULL, NULL)
INSERT [dbo].[Position] ([PositionID], [SurveyID], [PositionName], [PositionType], [PositionDesc], [Status], [CompanyID], [Source], [Years]) VALUES (5, 6, N'NET工程师', N'NET', N'.NET工程师', NULL, NULL, NULL, NULL)
INSERT [dbo].[Position] ([PositionID], [SurveyID], [PositionName], [PositionType], [PositionDesc], [Status], [CompanyID], [Source], [Years]) VALUES (6, 7, N'NET工程师', N'NET', N'.NET工程师', NULL, NULL, NULL, NULL)
INSERT [dbo].[Position] ([PositionID], [SurveyID], [PositionName], [PositionType], [PositionDesc], [Status], [CompanyID], [Source], [Years]) VALUES (7, 7, N'NET工程师2', N'NET2', N'.NET工程师2', NULL, NULL, NULL, NULL)
INSERT [dbo].[Position] ([PositionID], [SurveyID], [PositionName], [PositionType], [PositionDesc], [Status], [CompanyID], [Source], [Years]) VALUES (8, 8, N'NET工程师', N'NET', N'.NET工程师', NULL, NULL, NULL, NULL)
INSERT [dbo].[Position] ([PositionID], [SurveyID], [PositionName], [PositionType], [PositionDesc], [Status], [CompanyID], [Source], [Years]) VALUES (9, 8, N'NET工程师2', N'NET2', N'.NET工程师2', NULL, NULL, NULL, NULL)
INSERT [dbo].[Position] ([PositionID], [SurveyID], [PositionName], [PositionType], [PositionDesc], [Status], [CompanyID], [Source], [Years]) VALUES (10, 9, N'NET工程师', N'NET', N'.NET工程师', NULL, NULL, NULL, NULL)
INSERT [dbo].[Position] ([PositionID], [SurveyID], [PositionName], [PositionType], [PositionDesc], [Status], [CompanyID], [Source], [Years]) VALUES (11, 9, N'NET工程师2', N'NET2', N'.NET工程师2', NULL, NULL, NULL, NULL)
INSERT [dbo].[Position] ([PositionID], [SurveyID], [PositionName], [PositionType], [PositionDesc], [Status], [CompanyID], [Source], [Years]) VALUES (12, 10, N'NET工程师', N'NET', N'.NET工程师', NULL, NULL, NULL, NULL)
INSERT [dbo].[Position] ([PositionID], [SurveyID], [PositionName], [PositionType], [PositionDesc], [Status], [CompanyID], [Source], [Years]) VALUES (13, 10, N'NET工程师2', N'NET2', N'.NET工程师2', NULL, NULL, NULL, NULL)
INSERT [dbo].[Position] ([PositionID], [SurveyID], [PositionName], [PositionType], [PositionDesc], [Status], [CompanyID], [Source], [Years]) VALUES (14, 11, N'NET工程师', N'NET', N'.NET工程师', NULL, NULL, NULL, NULL)
INSERT [dbo].[Position] ([PositionID], [SurveyID], [PositionName], [PositionType], [PositionDesc], [Status], [CompanyID], [Source], [Years]) VALUES (15, 11, N'NET工程师2', N'NET2', N'.NET工程师2', NULL, NULL, NULL, NULL)
INSERT [dbo].[Position] ([PositionID], [SurveyID], [PositionName], [PositionType], [PositionDesc], [Status], [CompanyID], [Source], [Years]) VALUES (16, 27, N'NET工程师', N'NET', N'asp.net', NULL, NULL, N'智联招聘', N'2')
INSERT [dbo].[Position] ([PositionID], [SurveyID], [PositionName], [PositionType], [PositionDesc], [Status], [CompanyID], [Source], [Years]) VALUES (17, 27, N'JAVA工程师', N'NET', N'java', NULL, NULL, N'智联招聘', N'2')
INSERT [dbo].[Position] ([PositionID], [SurveyID], [PositionName], [PositionType], [PositionDesc], [Status], [CompanyID], [Source], [Years]) VALUES (18, 29, N'java', N'NET', N'd ', NULL, NULL, N'智联招聘', N'3')
INSERT [dbo].[Position] ([PositionID], [SurveyID], [PositionName], [PositionType], [PositionDesc], [Status], [CompanyID], [Source], [Years]) VALUES (19, 30, N'java', N'NET', N'd ', NULL, NULL, N'智联招聘', N'3')
INSERT [dbo].[Position] ([PositionID], [SurveyID], [PositionName], [PositionType], [PositionDesc], [Status], [CompanyID], [Source], [Years]) VALUES (20, 31, N'发送', N'NET', N'地方 ', NULL, NULL, N'智联招聘', N'3')
INSERT [dbo].[Position] ([PositionID], [SurveyID], [PositionName], [PositionType], [PositionDesc], [Status], [CompanyID], [Source], [Years]) VALUES (21, 31, N'发送2', N'NET', N'地方 ', NULL, NULL, N'智联招聘', N'3')
INSERT [dbo].[Position] ([PositionID], [SurveyID], [PositionName], [PositionType], [PositionDesc], [Status], [CompanyID], [Source], [Years]) VALUES (22, 31, N'发送3', N'NET', N'地方 ', NULL, NULL, N'智联招聘', N'3')
INSERT [dbo].[Position] ([PositionID], [SurveyID], [PositionName], [PositionType], [PositionDesc], [Status], [CompanyID], [Source], [Years]) VALUES (23, 33, N'Net', N'NET', N'dfsdd', NULL, NULL, N'智联招聘', N'3')
INSERT [dbo].[Position] ([PositionID], [SurveyID], [PositionName], [PositionType], [PositionDesc], [Status], [CompanyID], [Source], [Years]) VALUES (24, 33, N'Net2', N'NET', N'dfsdd', NULL, NULL, N'智联招聘', N'3')
SET IDENTITY_INSERT [dbo].[Position] OFF
SET IDENTITY_INSERT [dbo].[Project] ON 

INSERT [dbo].[Project] ([ProjectID], [SurveyID], [ProjectName], [ProjectType], [ProjectTypeID], [ProjectDesc], [BeginTime], [EndTime]) VALUES (1, 3, N'物流管理系统', N'物流', 1, N'第一个项目', CAST(N'2019-11-14 19:46:32.470' AS DateTime), CAST(N'2019-11-14 19:46:32.470' AS DateTime))
INSERT [dbo].[Project] ([ProjectID], [SurveyID], [ProjectName], [ProjectType], [ProjectTypeID], [ProjectDesc], [BeginTime], [EndTime]) VALUES (2, 4, N'物流管理系统', N'物流', 1, N'第一个项目', CAST(N'2019-11-14 19:52:49.563' AS DateTime), CAST(N'2019-11-14 19:52:49.563' AS DateTime))
INSERT [dbo].[Project] ([ProjectID], [SurveyID], [ProjectName], [ProjectType], [ProjectTypeID], [ProjectDesc], [BeginTime], [EndTime]) VALUES (3, 5, N'物流管理系统', N'物流', 1, N'第一个项目', CAST(N'2019-11-14 19:55:46.133' AS DateTime), CAST(N'2019-11-14 19:55:46.133' AS DateTime))
INSERT [dbo].[Project] ([ProjectID], [SurveyID], [ProjectName], [ProjectType], [ProjectTypeID], [ProjectDesc], [BeginTime], [EndTime]) VALUES (4, 6, N'物流管理系统', N'物流', 1, N'第一个项目', CAST(N'2019-11-14 20:02:00.917' AS DateTime), CAST(N'2019-11-14 20:02:00.917' AS DateTime))
INSERT [dbo].[Project] ([ProjectID], [SurveyID], [ProjectName], [ProjectType], [ProjectTypeID], [ProjectDesc], [BeginTime], [EndTime]) VALUES (5, 7, N'物流管理系统', N'物流', 1, N'第一个项目', CAST(N'2019-11-15 10:38:16.917' AS DateTime), CAST(N'2019-11-15 10:38:16.920' AS DateTime))
INSERT [dbo].[Project] ([ProjectID], [SurveyID], [ProjectName], [ProjectType], [ProjectTypeID], [ProjectDesc], [BeginTime], [EndTime]) VALUES (6, 7, N'物流管理系统2', N'物流2', 1, N'2第一个项目', CAST(N'2019-11-15 10:38:16.920' AS DateTime), CAST(N'2019-11-15 10:38:16.920' AS DateTime))
INSERT [dbo].[Project] ([ProjectID], [SurveyID], [ProjectName], [ProjectType], [ProjectTypeID], [ProjectDesc], [BeginTime], [EndTime]) VALUES (7, 8, N'物流管理系统', N'物流', 1, N'第一个项目', CAST(N'2019-11-15 10:48:06.423' AS DateTime), CAST(N'2019-11-15 10:48:06.423' AS DateTime))
INSERT [dbo].[Project] ([ProjectID], [SurveyID], [ProjectName], [ProjectType], [ProjectTypeID], [ProjectDesc], [BeginTime], [EndTime]) VALUES (8, 8, N'物流管理系统2', N'物流2', 1, N'2第一个项目', CAST(N'2019-11-15 10:48:06.423' AS DateTime), CAST(N'2019-11-15 10:48:06.423' AS DateTime))
INSERT [dbo].[Project] ([ProjectID], [SurveyID], [ProjectName], [ProjectType], [ProjectTypeID], [ProjectDesc], [BeginTime], [EndTime]) VALUES (9, 9, N'物流管理系统', N'物流', 1, N'第一个项目', CAST(N'2019-11-15 15:12:36.590' AS DateTime), CAST(N'2019-11-15 15:12:36.590' AS DateTime))
INSERT [dbo].[Project] ([ProjectID], [SurveyID], [ProjectName], [ProjectType], [ProjectTypeID], [ProjectDesc], [BeginTime], [EndTime]) VALUES (10, 9, N'物流管理系统2', N'物流2', 1, N'2第一个项目', CAST(N'2019-11-15 15:12:36.590' AS DateTime), CAST(N'2019-11-15 15:12:36.590' AS DateTime))
INSERT [dbo].[Project] ([ProjectID], [SurveyID], [ProjectName], [ProjectType], [ProjectTypeID], [ProjectDesc], [BeginTime], [EndTime]) VALUES (11, 10, N'物流管理系统', N'物流', 1, N'第一个项目', CAST(N'2019-11-15 15:14:38.420' AS DateTime), CAST(N'2019-11-15 15:14:38.420' AS DateTime))
INSERT [dbo].[Project] ([ProjectID], [SurveyID], [ProjectName], [ProjectType], [ProjectTypeID], [ProjectDesc], [BeginTime], [EndTime]) VALUES (12, 10, N'物流管理系统2', N'物流2', 1, N'2第一个项目', CAST(N'2019-11-15 15:14:38.420' AS DateTime), CAST(N'2019-11-15 15:14:38.420' AS DateTime))
INSERT [dbo].[Project] ([ProjectID], [SurveyID], [ProjectName], [ProjectType], [ProjectTypeID], [ProjectDesc], [BeginTime], [EndTime]) VALUES (16, 11, N'update', N'ee', NULL, NULL, CAST(N'2019-11-19 15:55:30.637' AS DateTime), CAST(N'2019-11-19 15:55:30.637' AS DateTime))
INSERT [dbo].[Project] ([ProjectID], [SurveyID], [ProjectName], [ProjectType], [ProjectTypeID], [ProjectDesc], [BeginTime], [EndTime]) VALUES (17, 16, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Project] ([ProjectID], [SurveyID], [ProjectName], [ProjectType], [ProjectTypeID], [ProjectDesc], [BeginTime], [EndTime]) VALUES (18, 16, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Project] ([ProjectID], [SurveyID], [ProjectName], [ProjectType], [ProjectTypeID], [ProjectDesc], [BeginTime], [EndTime]) VALUES (19, 17, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Project] ([ProjectID], [SurveyID], [ProjectName], [ProjectType], [ProjectTypeID], [ProjectDesc], [BeginTime], [EndTime]) VALUES (20, 17, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Project] ([ProjectID], [SurveyID], [ProjectName], [ProjectType], [ProjectTypeID], [ProjectDesc], [BeginTime], [EndTime]) VALUES (21, 18, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Project] ([ProjectID], [SurveyID], [ProjectName], [ProjectType], [ProjectTypeID], [ProjectDesc], [BeginTime], [EndTime]) VALUES (22, 18, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Project] ([ProjectID], [SurveyID], [ProjectName], [ProjectType], [ProjectTypeID], [ProjectDesc], [BeginTime], [EndTime]) VALUES (23, 23, N'sdfd', N'CMS', NULL, N'sdfs', NULL, NULL)
INSERT [dbo].[Project] ([ProjectID], [SurveyID], [ProjectName], [ProjectType], [ProjectTypeID], [ProjectDesc], [BeginTime], [EndTime]) VALUES (24, 24, N'sdfd', N'CMS', NULL, N'sdfs', NULL, NULL)
INSERT [dbo].[Project] ([ProjectID], [SurveyID], [ProjectName], [ProjectType], [ProjectTypeID], [ProjectDesc], [BeginTime], [EndTime]) VALUES (25, 24, N'sdfdss', N'CMS', NULL, N'sdfs', NULL, NULL)
INSERT [dbo].[Project] ([ProjectID], [SurveyID], [ProjectName], [ProjectType], [ProjectTypeID], [ProjectDesc], [BeginTime], [EndTime]) VALUES (26, 24, N'rtrtr', N'CMS', NULL, N'sdfs', NULL, NULL)
INSERT [dbo].[Project] ([ProjectID], [SurveyID], [ProjectName], [ProjectType], [ProjectTypeID], [ProjectDesc], [BeginTime], [EndTime]) VALUES (27, 27, N'财务', N'OA', NULL, N'财务系统', NULL, NULL)
INSERT [dbo].[Project] ([ProjectID], [SurveyID], [ProjectName], [ProjectType], [ProjectTypeID], [ProjectDesc], [BeginTime], [EndTime]) VALUES (28, 27, N'场馆', N'OA', NULL, N'场馆系统', NULL, NULL)
INSERT [dbo].[Project] ([ProjectID], [SurveyID], [ProjectName], [ProjectType], [ProjectTypeID], [ProjectDesc], [BeginTime], [EndTime]) VALUES (29, 31, N'士大夫', N'OA', NULL, N'地方', NULL, NULL)
INSERT [dbo].[Project] ([ProjectID], [SurveyID], [ProjectName], [ProjectType], [ProjectTypeID], [ProjectDesc], [BeginTime], [EndTime]) VALUES (30, 31, N'士大夫2', N'OA', NULL, N'地方', NULL, NULL)
INSERT [dbo].[Project] ([ProjectID], [SurveyID], [ProjectName], [ProjectType], [ProjectTypeID], [ProjectDesc], [BeginTime], [EndTime]) VALUES (31, 31, N'士大夫3', N'OA', NULL, N'地方', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Project] OFF
SET IDENTITY_INSERT [dbo].[Student] ON 

INSERT [dbo].[Student] ([StuNo], [StuName], [ClassNo], [City], [Phone], [Address], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [AlterTime], [CurrentCompanyNo], [Year], [Batch]) VALUES (1, N'王瑞祥', NULL, N'北京', N'17700611332', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2019, N'NET190901')
INSERT [dbo].[Student] ([StuNo], [StuName], [ClassNo], [City], [Phone], [Address], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [AlterTime], [CurrentCompanyNo], [Year], [Batch]) VALUES (2, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Student] ([StuNo], [StuName], [ClassNo], [City], [Phone], [Address], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [AlterTime], [CurrentCompanyNo], [Year], [Batch]) VALUES (3, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Student] ([StuNo], [StuName], [ClassNo], [City], [Phone], [Address], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [AlterTime], [CurrentCompanyNo], [Year], [Batch]) VALUES (4, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Student] ([StuNo], [StuName], [ClassNo], [City], [Phone], [Address], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [AlterTime], [CurrentCompanyNo], [Year], [Batch]) VALUES (5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Student] ([StuNo], [StuName], [ClassNo], [City], [Phone], [Address], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [AlterTime], [CurrentCompanyNo], [Year], [Batch]) VALUES (6, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Student] ([StuNo], [StuName], [ClassNo], [City], [Phone], [Address], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [AlterTime], [CurrentCompanyNo], [Year], [Batch]) VALUES (7, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Student] ([StuNo], [StuName], [ClassNo], [City], [Phone], [Address], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [AlterTime], [CurrentCompanyNo], [Year], [Batch]) VALUES (8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Student] ([StuNo], [StuName], [ClassNo], [City], [Phone], [Address], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [AlterTime], [CurrentCompanyNo], [Year], [Batch]) VALUES (9, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Student] ([StuNo], [StuName], [ClassNo], [City], [Phone], [Address], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [AlterTime], [CurrentCompanyNo], [Year], [Batch]) VALUES (10, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Student] ([StuNo], [StuName], [ClassNo], [City], [Phone], [Address], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [AlterTime], [CurrentCompanyNo], [Year], [Batch]) VALUES (11, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Student] ([StuNo], [StuName], [ClassNo], [City], [Phone], [Address], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [AlterTime], [CurrentCompanyNo], [Year], [Batch]) VALUES (12, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Student] ([StuNo], [StuName], [ClassNo], [City], [Phone], [Address], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [AlterTime], [CurrentCompanyNo], [Year], [Batch]) VALUES (13, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Student] ([StuNo], [StuName], [ClassNo], [City], [Phone], [Address], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [AlterTime], [CurrentCompanyNo], [Year], [Batch]) VALUES (14, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Student] ([StuNo], [StuName], [ClassNo], [City], [Phone], [Address], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [AlterTime], [CurrentCompanyNo], [Year], [Batch]) VALUES (15, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Student] ([StuNo], [StuName], [ClassNo], [City], [Phone], [Address], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [AlterTime], [CurrentCompanyNo], [Year], [Batch]) VALUES (16, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Student] ([StuNo], [StuName], [ClassNo], [City], [Phone], [Address], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [AlterTime], [CurrentCompanyNo], [Year], [Batch]) VALUES (17, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Student] ([StuNo], [StuName], [ClassNo], [City], [Phone], [Address], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [AlterTime], [CurrentCompanyNo], [Year], [Batch]) VALUES (18, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Student] ([StuNo], [StuName], [ClassNo], [City], [Phone], [Address], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [AlterTime], [CurrentCompanyNo], [Year], [Batch]) VALUES (19, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Student] ([StuNo], [StuName], [ClassNo], [City], [Phone], [Address], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [AlterTime], [CurrentCompanyNo], [Year], [Batch]) VALUES (20, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Student] ([StuNo], [StuName], [ClassNo], [City], [Phone], [Address], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [AlterTime], [CurrentCompanyNo], [Year], [Batch]) VALUES (21, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Student] ([StuNo], [StuName], [ClassNo], [City], [Phone], [Address], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [AlterTime], [CurrentCompanyNo], [Year], [Batch]) VALUES (22, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Student] ([StuNo], [StuName], [ClassNo], [City], [Phone], [Address], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [AlterTime], [CurrentCompanyNo], [Year], [Batch]) VALUES (23, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Student] ([StuNo], [StuName], [ClassNo], [City], [Phone], [Address], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [AlterTime], [CurrentCompanyNo], [Year], [Batch]) VALUES (24, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Student] ([StuNo], [StuName], [ClassNo], [City], [Phone], [Address], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [AlterTime], [CurrentCompanyNo], [Year], [Batch]) VALUES (25, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Student] ([StuNo], [StuName], [ClassNo], [City], [Phone], [Address], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [AlterTime], [CurrentCompanyNo], [Year], [Batch]) VALUES (26, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Student] OFF
SET IDENTITY_INSERT [dbo].[SurveyModel] ON 

INSERT [dbo].[SurveyModel] ([SurveyID], [SurveyTickNumber], [StuNo], [CompanyID], [StuName], [CompanyNo], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [Year], [Batch], [Direction], [ProjectName], [Phone], [CorworkPhone], [PositionName], [AuditName], [AuditStatus], [School], [DepartName], [Type]) VALUES (1, N'123456', 1, 1, N'张三', NULL, CAST(321 AS Decimal(18, 0)), CAST(123 AS Decimal(18, 0)), CAST(333 AS Decimal(18, 0)), NULL, NULL, 2019, N'Net190901', N'Java', N'Java', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[SurveyModel] ([SurveyID], [SurveyTickNumber], [StuNo], [CompanyID], [StuName], [CompanyNo], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [Year], [Batch], [Direction], [ProjectName], [Phone], [CorworkPhone], [PositionName], [AuditName], [AuditStatus], [School], [DepartName], [Type]) VALUES (2, N'123456', 1, 1, N'李四', NULL, CAST(321 AS Decimal(18, 0)), CAST(123 AS Decimal(18, 0)), CAST(333 AS Decimal(18, 0)), NULL, NULL, 2019, N'Net190901', N'Java', N'Java', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[SurveyModel] ([SurveyID], [SurveyTickNumber], [StuNo], [CompanyID], [StuName], [CompanyNo], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [Year], [Batch], [Direction], [ProjectName], [Phone], [CorworkPhone], [PositionName], [AuditName], [AuditStatus], [School], [DepartName], [Type]) VALUES (3, N'123456', 1, 1, N'王五', NULL, CAST(321 AS Decimal(18, 0)), CAST(123 AS Decimal(18, 0)), CAST(333 AS Decimal(18, 0)), NULL, NULL, 2019, N'Net190901', N'Java', N'Java', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[SurveyModel] ([SurveyID], [SurveyTickNumber], [StuNo], [CompanyID], [StuName], [CompanyNo], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [Year], [Batch], [Direction], [ProjectName], [Phone], [CorworkPhone], [PositionName], [AuditName], [AuditStatus], [School], [DepartName], [Type]) VALUES (4, N'123456', 1, 1, N'王五', NULL, CAST(321 AS Decimal(18, 0)), CAST(123 AS Decimal(18, 0)), CAST(333 AS Decimal(18, 0)), NULL, NULL, 2019, N'Net190901', N'Java', N'Java', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[SurveyModel] ([SurveyID], [SurveyTickNumber], [StuNo], [CompanyID], [StuName], [CompanyNo], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [Year], [Batch], [Direction], [ProjectName], [Phone], [CorworkPhone], [PositionName], [AuditName], [AuditStatus], [School], [DepartName], [Type]) VALUES (5, N'123456', 1, 1, N'王五', NULL, CAST(321 AS Decimal(18, 0)), CAST(123 AS Decimal(18, 0)), CAST(333 AS Decimal(18, 0)), NULL, NULL, 2019, N'Net190901', N'Java', N'Java', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[SurveyModel] ([SurveyID], [SurveyTickNumber], [StuNo], [CompanyID], [StuName], [CompanyNo], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [Year], [Batch], [Direction], [ProjectName], [Phone], [CorworkPhone], [PositionName], [AuditName], [AuditStatus], [School], [DepartName], [Type]) VALUES (6, N'123456', 1, 1, N'王五', NULL, CAST(321 AS Decimal(18, 0)), CAST(123 AS Decimal(18, 0)), CAST(333 AS Decimal(18, 0)), NULL, NULL, 2019, N'Net190901', N'Java', N'Java', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[SurveyModel] ([SurveyID], [SurveyTickNumber], [StuNo], [CompanyID], [StuName], [CompanyNo], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [Year], [Batch], [Direction], [ProjectName], [Phone], [CorworkPhone], [PositionName], [AuditName], [AuditStatus], [School], [DepartName], [Type]) VALUES (7, N'123456', 1, 1, N'张三', N'Neeee', CAST(321 AS Decimal(18, 0)), CAST(123 AS Decimal(18, 0)), CAST(333 AS Decimal(18, 0)), NULL, NULL, 2019, N'Net190901', N'Java', N'Java', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[SurveyModel] ([SurveyID], [SurveyTickNumber], [StuNo], [CompanyID], [StuName], [CompanyNo], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [Year], [Batch], [Direction], [ProjectName], [Phone], [CorworkPhone], [PositionName], [AuditName], [AuditStatus], [School], [DepartName], [Type]) VALUES (8, N'123456', 1, 1, N'调查学生1', N'Neeee', CAST(321 AS Decimal(18, 0)), CAST(123 AS Decimal(18, 0)), CAST(333 AS Decimal(18, 0)), 0, NULL, 2019, N'Net190901', N'Java', N'Java', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[SurveyModel] ([SurveyID], [SurveyTickNumber], [StuNo], [CompanyID], [StuName], [CompanyNo], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [Year], [Batch], [Direction], [ProjectName], [Phone], [CorworkPhone], [PositionName], [AuditName], [AuditStatus], [School], [DepartName], [Type]) VALUES (9, N'123456', 1, 1, N'调查学生2', N'Neeee', CAST(321 AS Decimal(18, 0)), CAST(123 AS Decimal(18, 0)), CAST(333 AS Decimal(18, 0)), 0, NULL, 2019, N'Net190901', N'Net', N'Net项目部', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[SurveyModel] ([SurveyID], [SurveyTickNumber], [StuNo], [CompanyID], [StuName], [CompanyNo], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [Year], [Batch], [Direction], [ProjectName], [Phone], [CorworkPhone], [PositionName], [AuditName], [AuditStatus], [School], [DepartName], [Type]) VALUES (10, N'123456', 1, 1, N'调查学生3', N'Neeee', CAST(321 AS Decimal(18, 0)), CAST(123 AS Decimal(18, 0)), CAST(333 AS Decimal(18, 0)), 0, CAST(N'2019-11-15 15:14:38.420' AS DateTime), 2019, N'Net190901', N'Net', N'Net项目部', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[SurveyModel] ([SurveyID], [SurveyTickNumber], [StuNo], [CompanyID], [StuName], [CompanyNo], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [Year], [Batch], [Direction], [ProjectName], [Phone], [CorworkPhone], [PositionName], [AuditName], [AuditStatus], [School], [DepartName], [Type]) VALUES (11, N'123456', 8, 1, N'调查学生3', N'Neeee', CAST(321 AS Decimal(18, 0)), CAST(123 AS Decimal(18, 0)), CAST(333 AS Decimal(18, 0)), 0, CAST(N'2019-11-15 15:24:44.933' AS DateTime), 2019, N'Net190901', N'大数据', N'修改', N'17700611332   ', N'17700923232   ', N'NET工程师', N'王瑞祥', 1, N'河南大学', N'研发部', NULL)
INSERT [dbo].[SurveyModel] ([SurveyID], [SurveyTickNumber], [StuNo], [CompanyID], [StuName], [CompanyNo], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [Year], [Batch], [Direction], [ProjectName], [Phone], [CorworkPhone], [PositionName], [AuditName], [AuditStatus], [School], [DepartName], [Type]) VALUES (12, NULL, 2, 2, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2019, N'Net190901', N'Java', N'Java', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[SurveyModel] ([SurveyID], [SurveyTickNumber], [StuNo], [CompanyID], [StuName], [CompanyNo], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [Year], [Batch], [Direction], [ProjectName], [Phone], [CorworkPhone], [PositionName], [AuditName], [AuditStatus], [School], [DepartName], [Type]) VALUES (13, NULL, 3, 3, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2019, N'Net190901', N'Java', N'Java', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[SurveyModel] ([SurveyID], [SurveyTickNumber], [StuNo], [CompanyID], [StuName], [CompanyNo], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [Year], [Batch], [Direction], [ProjectName], [Phone], [CorworkPhone], [PositionName], [AuditName], [AuditStatus], [School], [DepartName], [Type]) VALUES (14, NULL, 4, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2019, N'2019', N'2019', N'Java', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[SurveyModel] ([SurveyID], [SurveyTickNumber], [StuNo], [CompanyID], [StuName], [CompanyNo], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [Year], [Batch], [Direction], [ProjectName], [Phone], [CorworkPhone], [PositionName], [AuditName], [AuditStatus], [School], [DepartName], [Type]) VALUES (15, NULL, 5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2019, N'2019', N'2019', N'Java', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[SurveyModel] ([SurveyID], [SurveyTickNumber], [StuNo], [CompanyID], [StuName], [CompanyNo], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [Year], [Batch], [Direction], [ProjectName], [Phone], [CorworkPhone], [PositionName], [AuditName], [AuditStatus], [School], [DepartName], [Type]) VALUES (16, NULL, 9, 4, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2019, N'Net190901', N'Java', N'Java', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[SurveyModel] ([SurveyID], [SurveyTickNumber], [StuNo], [CompanyID], [StuName], [CompanyNo], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [Year], [Batch], [Direction], [ProjectName], [Phone], [CorworkPhone], [PositionName], [AuditName], [AuditStatus], [School], [DepartName], [Type]) VALUES (17, NULL, 10, 5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2019, N'Net190901', N'Java', N'Java', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[SurveyModel] ([SurveyID], [SurveyTickNumber], [StuNo], [CompanyID], [StuName], [CompanyNo], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [Year], [Batch], [Direction], [ProjectName], [Phone], [CorworkPhone], [PositionName], [AuditName], [AuditStatus], [School], [DepartName], [Type]) VALUES (18, NULL, 11, 6, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2019, N'Net190901', N'Java', N'Java', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[SurveyModel] ([SurveyID], [SurveyTickNumber], [StuNo], [CompanyID], [StuName], [CompanyNo], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [Year], [Batch], [Direction], [ProjectName], [Phone], [CorworkPhone], [PositionName], [AuditName], [AuditStatus], [School], [DepartName], [Type]) VALUES (19, NULL, 12, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2019, N'Net190901', N'Java', N'Java', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[SurveyModel] ([SurveyID], [SurveyTickNumber], [StuNo], [CompanyID], [StuName], [CompanyNo], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [Year], [Batch], [Direction], [ProjectName], [Phone], [CorworkPhone], [PositionName], [AuditName], [AuditStatus], [School], [DepartName], [Type]) VALUES (20, NULL, 13, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2019, N'Net190901', N'Java', N'Java', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[SurveyModel] ([SurveyID], [SurveyTickNumber], [StuNo], [CompanyID], [StuName], [CompanyNo], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [Year], [Batch], [Direction], [ProjectName], [Phone], [CorworkPhone], [PositionName], [AuditName], [AuditStatus], [School], [DepartName], [Type]) VALUES (21, NULL, 14, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2019, N'Net190901', N'Java', N'Java', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[SurveyModel] ([SurveyID], [SurveyTickNumber], [StuNo], [CompanyID], [StuName], [CompanyNo], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [Year], [Batch], [Direction], [ProjectName], [Phone], [CorworkPhone], [PositionName], [AuditName], [AuditStatus], [School], [DepartName], [Type]) VALUES (22, NULL, 15, 7, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2019, N'Net190901', N'Java', N'Java', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[SurveyModel] ([SurveyID], [SurveyTickNumber], [StuNo], [CompanyID], [StuName], [CompanyNo], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [Year], [Batch], [Direction], [ProjectName], [Phone], [CorworkPhone], [PositionName], [AuditName], [AuditStatus], [School], [DepartName], [Type]) VALUES (23, NULL, 16, 8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2019, N'Net190901', N'Java', N'Java', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[SurveyModel] ([SurveyID], [SurveyTickNumber], [StuNo], [CompanyID], [StuName], [CompanyNo], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [Year], [Batch], [Direction], [ProjectName], [Phone], [CorworkPhone], [PositionName], [AuditName], [AuditStatus], [School], [DepartName], [Type]) VALUES (24, NULL, 17, 9, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2019, N'Net190901', N'Java', N'Java', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[SurveyModel] ([SurveyID], [SurveyTickNumber], [StuNo], [CompanyID], [StuName], [CompanyNo], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [Year], [Batch], [Direction], [ProjectName], [Phone], [CorworkPhone], [PositionName], [AuditName], [AuditStatus], [School], [DepartName], [Type]) VALUES (25, NULL, 18, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2019, N'Net190901', N'Java', N'Java', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[SurveyModel] ([SurveyID], [SurveyTickNumber], [StuNo], [CompanyID], [StuName], [CompanyNo], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [Year], [Batch], [Direction], [ProjectName], [Phone], [CorworkPhone], [PositionName], [AuditName], [AuditStatus], [School], [DepartName], [Type]) VALUES (26, NULL, 19, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2019, N'Net190901', N'Java', N'Java', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[SurveyModel] ([SurveyID], [SurveyTickNumber], [StuNo], [CompanyID], [StuName], [CompanyNo], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [Year], [Batch], [Direction], [ProjectName], [Phone], [CorworkPhone], [PositionName], [AuditName], [AuditStatus], [School], [DepartName], [Type]) VALUES (27, NULL, 20, 10, N'王瑞祥', N'1111', CAST(4000 AS Decimal(18, 0)), CAST(5000 AS Decimal(18, 0)), CAST(4500 AS Decimal(18, 0)), NULL, NULL, 2019, N'1909', N'Java', N'NET190901', N'17700611332   ', NULL, NULL, NULL, NULL, N'河南大学', NULL, NULL)
INSERT [dbo].[SurveyModel] ([SurveyID], [SurveyTickNumber], [StuNo], [CompanyID], [StuName], [CompanyNo], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [Year], [Batch], [Direction], [ProjectName], [Phone], [CorworkPhone], [PositionName], [AuditName], [AuditStatus], [School], [DepartName], [Type]) VALUES (28, NULL, 21, 11, NULL, N'0001', NULL, NULL, NULL, NULL, NULL, 2019, N'Net190901', N'Java', N'Java', NULL, NULL, N'地方', NULL, NULL, NULL, N'士大夫', NULL)
INSERT [dbo].[SurveyModel] ([SurveyID], [SurveyTickNumber], [StuNo], [CompanyID], [StuName], [CompanyNo], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [Year], [Batch], [Direction], [ProjectName], [Phone], [CorworkPhone], [PositionName], [AuditName], [AuditStatus], [School], [DepartName], [Type]) VALUES (29, NULL, 22, 12, NULL, N'0001', NULL, NULL, NULL, NULL, NULL, 2019, N'Net190901', N'Java', N'Java', NULL, NULL, N'地方', NULL, NULL, NULL, N'士大夫', NULL)
INSERT [dbo].[SurveyModel] ([SurveyID], [SurveyTickNumber], [StuNo], [CompanyID], [StuName], [CompanyNo], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [Year], [Batch], [Direction], [ProjectName], [Phone], [CorworkPhone], [PositionName], [AuditName], [AuditStatus], [School], [DepartName], [Type]) VALUES (30, NULL, 23, 13, NULL, N'0001', NULL, NULL, NULL, NULL, NULL, 2019, N'Net190901', N'Java', N'Java', NULL, NULL, N'地方', NULL, NULL, NULL, N'士大夫', NULL)
INSERT [dbo].[SurveyModel] ([SurveyID], [SurveyTickNumber], [StuNo], [CompanyID], [StuName], [CompanyNo], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [Year], [Batch], [Direction], [ProjectName], [Phone], [CorworkPhone], [PositionName], [AuditName], [AuditStatus], [School], [DepartName], [Type]) VALUES (31, NULL, 24, 14, N'wang', N'0001', CAST(3000 AS Decimal(18, 0)), CAST(4000 AS Decimal(18, 0)), CAST(5000 AS Decimal(18, 0)), NULL, NULL, 2019, N'1909', N'Java', N'190901', N'17700611332   ', NULL, N'财务工程师', NULL, NULL, N'河南大学', N'财务部', NULL)
INSERT [dbo].[SurveyModel] ([SurveyID], [SurveyTickNumber], [StuNo], [CompanyID], [StuName], [CompanyNo], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [Year], [Batch], [Direction], [ProjectName], [Phone], [CorworkPhone], [PositionName], [AuditName], [AuditStatus], [School], [DepartName], [Type]) VALUES (32, NULL, 25, 15, NULL, N'dfd', NULL, NULL, NULL, NULL, NULL, 2019, N'Net190901', N'Java', N'Java', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[SurveyModel] ([SurveyID], [SurveyTickNumber], [StuNo], [CompanyID], [StuName], [CompanyNo], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [Year], [Batch], [Direction], [ProjectName], [Phone], [CorworkPhone], [PositionName], [AuditName], [AuditStatus], [School], [DepartName], [Type]) VALUES (33, NULL, 26, 16, NULL, N'12323', NULL, NULL, NULL, NULL, NULL, 2019, N'Net190901', N'Java', N'Java', NULL, NULL, N'开发工程师', NULL, NULL, NULL, N'项目部', NULL)
INSERT [dbo].[SurveyModel] ([SurveyID], [SurveyTickNumber], [StuNo], [CompanyID], [StuName], [CompanyNo], [WagesOfPeriod], [WagesOfFull], [WagesOfReal], [Status], [CreateTime], [Year], [Batch], [Direction], [ProjectName], [Phone], [CorworkPhone], [PositionName], [AuditName], [AuditStatus], [School], [DepartName], [Type]) VALUES (34, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2019, N'Net190901', N'Java', N'Java', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[SurveyModel] OFF
ALTER TABLE [dbo].[ActiveJobs]  WITH CHECK ADD  CONSTRAINT [FK_ACTIVEJO_REFERENCE_SURVEY] FOREIGN KEY([SurveyID])
REFERENCES [dbo].[SurveyModel] ([SurveyID])
GO
ALTER TABLE [dbo].[ActiveJobs] CHECK CONSTRAINT [FK_ACTIVEJO_REFERENCE_SURVEY]
GO
ALTER TABLE [dbo].[Department]  WITH CHECK ADD  CONSTRAINT [FK_DEPARTME_REFERENCE_USERS] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Department] CHECK CONSTRAINT [FK_DEPARTME_REFERENCE_USERS]
GO
ALTER TABLE [dbo].[Position]  WITH CHECK ADD  CONSTRAINT [FK_POSITION_REFERENCE_SURVEY] FOREIGN KEY([SurveyID])
REFERENCES [dbo].[SurveyModel] ([SurveyID])
GO
ALTER TABLE [dbo].[Position] CHECK CONSTRAINT [FK_POSITION_REFERENCE_SURVEY]
GO
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_PROJECT_REFERENCE_SURVEY] FOREIGN KEY([SurveyID])
REFERENCES [dbo].[SurveyModel] ([SurveyID])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_PROJECT_REFERENCE_SURVEY]
GO
ALTER TABLE [dbo].[RoleMenus]  WITH CHECK ADD  CONSTRAINT [FK_ROLEMENU_REFERENCE_MENUS] FOREIGN KEY([MenuID])
REFERENCES [dbo].[Menus] ([MenuID])
GO
ALTER TABLE [dbo].[RoleMenus] CHECK CONSTRAINT [FK_ROLEMENU_REFERENCE_MENUS]
GO
ALTER TABLE [dbo].[RoleMenus]  WITH CHECK ADD  CONSTRAINT [FK_ROLEMENU_REFERENCE_ROLES] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([RoleID])
GO
ALTER TABLE [dbo].[RoleMenus] CHECK CONSTRAINT [FK_ROLEMENU_REFERENCE_ROLES]
GO
ALTER TABLE [dbo].[SkillTags]  WITH CHECK ADD  CONSTRAINT [FK_SKILLTAG_REFERENCE_POSITION] FOREIGN KEY([PositionID])
REFERENCES [dbo].[Position] ([PositionID])
GO
ALTER TABLE [dbo].[SkillTags] CHECK CONSTRAINT [FK_SKILLTAG_REFERENCE_POSITION]
GO
ALTER TABLE [dbo].[SkillTags]  WITH CHECK ADD  CONSTRAINT [FK_SKILLTAG_REFERENCE_SKILLTAG] FOREIGN KEY([TagID])
REFERENCES [dbo].[SkillTag] ([TagID])
GO
ALTER TABLE [dbo].[SkillTags] CHECK CONSTRAINT [FK_SKILLTAG_REFERENCE_SKILLTAG]
GO
ALTER TABLE [dbo].[StudentTags]  WITH CHECK ADD  CONSTRAINT [FK_STUDENTT_REFERENCE_SKILLTAG] FOREIGN KEY([TagID])
REFERENCES [dbo].[SkillTag] ([TagID])
GO
ALTER TABLE [dbo].[StudentTags] CHECK CONSTRAINT [FK_STUDENTT_REFERENCE_SKILLTAG]
GO
ALTER TABLE [dbo].[StudentTags]  WITH CHECK ADD  CONSTRAINT [FK_STUDENTT_REFERENCE_STUDENT] FOREIGN KEY([StuNo])
REFERENCES [dbo].[Student] ([StuNo])
GO
ALTER TABLE [dbo].[StudentTags] CHECK CONSTRAINT [FK_STUDENTT_REFERENCE_STUDENT]
GO
ALTER TABLE [dbo].[SurveyModel]  WITH CHECK ADD  CONSTRAINT [FK_SURVEY_REFERENCE_COMPANY] FOREIGN KEY([CompanyID])
REFERENCES [dbo].[Company] ([CompanyID])
GO
ALTER TABLE [dbo].[SurveyModel] CHECK CONSTRAINT [FK_SURVEY_REFERENCE_COMPANY]
GO
ALTER TABLE [dbo].[SurveyModel]  WITH CHECK ADD  CONSTRAINT [FK_SURVEY_REFERENCE_STUDENT] FOREIGN KEY([StuNo])
REFERENCES [dbo].[Student] ([StuNo])
GO
ALTER TABLE [dbo].[SurveyModel] CHECK CONSTRAINT [FK_SURVEY_REFERENCE_STUDENT]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_USERROLE_REFERENCE_ROLES] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([RoleID])
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_USERROLE_REFERENCE_ROLES]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_USERROLE_REFERENCE_USERS] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_USERROLE_REFERENCE_USERS]
GO
