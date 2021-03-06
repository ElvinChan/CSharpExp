USE [master]
GO
/****** Object:  Database [CitysOfShanxi]    Script Date: 12/12/2014 14:09:00 ******/
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'CitysOfShanxi')
BEGIN
CREATE DATABASE [CitysOfShanxi] ON  PRIMARY 
( NAME = N'CitysOfShanxi', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\CitysOfShanxi.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'CitysOfShanxi_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\CitysOfShanxi_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
END
GO
ALTER DATABASE [CitysOfShanxi] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CitysOfShanxi].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CitysOfShanxi] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [CitysOfShanxi] SET ANSI_NULLS OFF
GO
ALTER DATABASE [CitysOfShanxi] SET ANSI_PADDING OFF
GO
ALTER DATABASE [CitysOfShanxi] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [CitysOfShanxi] SET ARITHABORT OFF
GO
ALTER DATABASE [CitysOfShanxi] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [CitysOfShanxi] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [CitysOfShanxi] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [CitysOfShanxi] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [CitysOfShanxi] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [CitysOfShanxi] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [CitysOfShanxi] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [CitysOfShanxi] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [CitysOfShanxi] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [CitysOfShanxi] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [CitysOfShanxi] SET  DISABLE_BROKER
GO
ALTER DATABASE [CitysOfShanxi] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [CitysOfShanxi] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [CitysOfShanxi] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [CitysOfShanxi] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [CitysOfShanxi] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [CitysOfShanxi] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [CitysOfShanxi] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [CitysOfShanxi] SET  READ_WRITE
GO
ALTER DATABASE [CitysOfShanxi] SET RECOVERY FULL
GO
ALTER DATABASE [CitysOfShanxi] SET  MULTI_USER
GO
ALTER DATABASE [CitysOfShanxi] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [CitysOfShanxi] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'CitysOfShanxi', N'ON'
GO
USE [CitysOfShanxi]
GO
/****** Object:  Table [dbo].[City]    Script Date: 12/12/2014 14:09:01 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[City]') AND type in (N'U'))
DROP TABLE [dbo].[City]
GO
/****** Object:  Table [dbo].[City]    Script Date: 12/12/2014 14:09:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[City]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[City](
	[id] [int] NULL,
	[name] [nvarchar](50) NULL,
	[fk] [int] NULL
) ON [PRIMARY]
END
GO
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (1, N'太原市', 0)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (2, N'大同市', 0)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (3, N'阳泉市', 0)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (4, N'长治市', 0)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (5, N'晋城市', 0)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (6, N'朔州市', 0)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (7, N'晋中市', 0)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (8, N'运城市', 0)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (9, N'忻州市', 0)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (10, N'临汾市', 0)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (11, N'吕梁市', 0)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (12, N'杏花岭区', 1)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (13, N'小店区', 1)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (14, N'迎泽区', 1)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (15, N'尖草坪区', 1)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (16, N'万柏林区', 1)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (17, N'晋源区（晋源街道）', 1)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (18, N'古交市  ', 1)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (19, N'阳曲县（黄寨镇）', 1)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (20, N'清徐县（清源镇）', 1)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (21, N'娄烦县（娄烦镇）', 1)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (22, N'城　区', 2)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (23, N'矿　区', 2)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (24, N'南郊区（口泉乡）', 2)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (25, N'新荣区（新荣镇）', 2)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (26, N'大同县（西坪镇）', 2)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (27, N'天镇县（玉泉镇）', 2)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (28, N'灵丘县（武灵镇）', 2)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (29, N'阳高县（龙泉镇）', 2)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (30, N'左云县（云兴镇）', 2)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (31, N'广灵县（壶泉镇）', 2)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (32, N'浑源县（永安镇）', 2)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (33, N'城　区', 3)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (34, N'矿　区', 3)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (35, N'郊　区（荫营镇）', 3)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (36, N'平定县（冠山镇）', 3)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (37, N'盂　县（秀水镇）', 3)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (38, N'城　区', 4)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (39, N'郊　区', 4)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (40, N'潞城市（潞华街道）', 4)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (41, N'长治县（韩店镇）', 4)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (42, N'长子县（丹朱镇）', 4)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (43, N'平顺县（青羊镇）', 4)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (44, N'襄垣县（古韩镇）', 4)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (45, N'沁源县（沁河镇）', 4)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (46, N'屯留县（麒绛镇）', 4)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (47, N'黎城县（黎侯镇）', 4)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (48, N'武乡县（丰州镇）', 4)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (49, N'沁　县（定昌镇）', 4)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (50, N'壶关县（龙泉镇）', 4)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (51, N'城　区', 5)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (52, N'高平市（东城街道）', 5)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (53, N'泽州县（南村镇）', 5)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (54, N'陵川县（崇文镇）', 5)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (55, N'阳城县（凤城镇）', 5)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (56, N'沁水县（龙港镇）', 5)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (57, N'朔城区', 6)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (58, N'平鲁区（井坪镇）', 6)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (59, N'山阴县（岱岳镇）', 6)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (60, N'右玉县（新城镇）', 6)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (61, N'应　县（金城镇）', 6)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (62, N'怀仁县（云中镇）', 6)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (63, N'榆次区', 7)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (64, N'介休市', 7)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (65, N'昔阳县（乐平镇）', 7)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (66, N'灵石县（翠峰镇）', 7)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (67, N'祁　县（昭余镇）', 7)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (68, N'左权县（辽阳镇）', 7)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (69, N'寿阳县（朝阳镇）', 7)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (70, N'太谷县（明星镇）', 7)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (71, N'和顺县（义兴镇）', 7)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (72, N'平遥县（古陶镇）', 7)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (73, N'榆社县（箕城镇）', 7)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (74, N'盐湖区', 8)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (75, N'河津市（城区街道）', 8)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (76, N'永济市（城东街道）', 8)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (77, N'闻喜县（桐城镇）', 8)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (78, N'新绛县（龙兴镇）', 8)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (79, N'平陆县（圣人涧镇）', 8)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (80, N'垣曲县（新城镇）', 8)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (81, N'绛　县（古绛镇）', 8)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (82, N'稷山县（稷峰镇）', 8)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (83, N'芮城县（古魏镇）', 8)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (84, N'夏　县（瑶峰镇）', 8)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (85, N'万荣县（解店镇）', 8)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (86, N'临猗县（猗氏镇）', 8)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (87, N'忻府区', 9)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (88, N'原平市（南城街道）', 9)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (89, N'代　县（上馆镇）', 9)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (90, N'神池县（龙泉镇）', 9)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (91, N'五寨县（砚城镇）', 9)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (92, N'五台县（台城镇', 9)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (93, N'偏关县（新关镇）', 9)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (94, N'宁武县（凤凰镇）', 9)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (95, N'静乐县（鹅城镇）', 9)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (96, N'繁峙县（繁城镇）', 9)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (97, N'河曲县（文笔镇）', 9)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (98, N'保德县（东关镇）', 9)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (99, N'定襄县（晋昌镇）', 9)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (100, N'岢岚县（岚漪镇） ', 9)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (101, N'尧都区', 10)
GO
print 'Processed 100 total records'
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (102, N'侯马市（路西街道）', 10)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (103, N'霍州市（鼓楼街道）', 10)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (104, N'汾西县（永安镇）', 10)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (105, N'吉　县（吉昌镇）', 10)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (106, N'安泽县（府城镇）', 10)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (107, N'大宁县（昕水镇）', 10)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (108, N'浮山县（天坛镇）', 10)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (109, N'古　县（岳阳镇）', 10)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (110, N'隰　县（龙泉镇）', 10)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (111, N'襄汾县（城关镇）', 10)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (112, N'翼城县（唐兴镇）', 10)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (113, N'永和县（芝河镇）', 10)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (114, N'乡宁县（昌宁镇）', 10)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (115, N'曲沃县（乐昌镇）', 10)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (116, N'洪洞县（大槐树镇）', 10)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (117, N'蒲　县（蒲城镇）', 10)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (118, N'离石区', 11)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (119, N'孝义市', 11)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (120, N'汾阳市', 11)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (121, N'文水县（凤城镇）', 11)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (122, N'中阳县（宁乡镇）', 11)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (123, N'兴　县（蔚汾镇）', 11)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (124, N'临　县（临泉镇）', 11)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (125, N'方山县（圪洞镇）', 11)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (126, N'柳林县（柳林镇）', 11)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (127, N'岚　县（东村镇）', 11)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (128, N'交口县（水头镇）', 11)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (129, N'交城县（天宁镇）', 11)
INSERT [dbo].[City] ([id], [name], [fk]) VALUES (130, N'石楼县（灵泉镇）', 11)
