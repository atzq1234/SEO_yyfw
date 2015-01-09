-- phpMyAdmin SQL Dump
-- version 3.3.7
-- http://www.phpmyadmin.net
--
-- 主机: localhost
-- 生成日期: 2012 年 10 月 19 日 10:59
-- 服务器版本: 5.0.90
-- PHP 版本: 5.2.14

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- 数据库: `db_tong2211`
--

-- --------------------------------------------------------

--
-- 表的结构 `t_ad`
--

CREATE TABLE IF NOT EXISTS `t_ad` (
  `AdID` int(11) NOT NULL auto_increment,
  `AdName` varchar(200) character set utf8 default NULL,
  `AdIntro` longtext character set utf8,
  `AdPositionID` int(11) NOT NULL,
  `AdSmallPic` varchar(200) character set utf8 default NULL,
  `AdPath` varchar(200) character set utf8 default NULL,
  `AdLink` varchar(200) character set utf8 default NULL,
  `ClickNum` int(11) NOT NULL,
  `ListID` int(11) NOT NULL,
  `AdminID` int(11) NOT NULL,
  `AddTime` datetime default NULL,
  `IsClose` int(11) NOT NULL,
  PRIMARY KEY  (`AdID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=3 ;

--
-- 转存表中的数据 `t_ad`
--

INSERT INTO `t_ad` (`AdID`, `AdName`, `AdIntro`, `AdPositionID`, `AdSmallPic`, `AdPath`, `AdLink`, `ClickNum`, `ListID`, `AdminID`, `AddTime`, `IsClose`) VALUES
(1, '1', '', 1, '', '/Files/Ad/1.jpg', 'index.aspx', 6, 1, 1, '2011-04-28 10:24:43', 0),
(2, '2', '', 1, '', '/Files/Ad/2.jpg', 'index.aspx', 2, 2, 1, '2011-04-28 10:26:25', 0);

-- --------------------------------------------------------

--
-- 表的结构 `t_admin`
--

CREATE TABLE IF NOT EXISTS `t_admin` (
  `AdminID` int(11) NOT NULL auto_increment,
  `AdminName` varchar(200) character set utf8 default NULL,
  `AdminPass` varchar(200) character set utf8 default NULL,
  `RealName` varchar(200) character set utf8 default NULL,
  `Email` varchar(200) character set utf8 default NULL,
  `Department` varchar(200) character set utf8 default NULL,
  `Comment` longtext character set utf8,
  `LoginNum` int(11) NOT NULL,
  `LastLoginTime` datetime default NULL,
  `ThisLoginTime` datetime default NULL,
  `ManageAdminID` int(11) NOT NULL,
  `AddTime` datetime default NULL,
  `IsClose` int(11) NOT NULL,
  PRIMARY KEY  (`AdminID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=6 ;

--
-- 转存表中的数据 `t_admin`
--

INSERT INTO `t_admin` (`AdminID`, `AdminName`, `AdminPass`, `RealName`, `Email`, `Department`, `Comment`, `LoginNum`, `LastLoginTime`, `ThisLoginTime`, `ManageAdminID`, `AddTime`, `IsClose`) VALUES
(1, 'hxadmin', 'C732AFD633B55CDF99A8C6A39FEA6889', '杨小明', 'bd-sky@qq.com', '技术部', '', 659, '2012-10-19 09:43:54', '2012-10-19 14:30:18', 1, '2010-05-19 17:41:07', 0),
(5, 'webadmin', '41EBC027AA3D22432D1C413E9B71B55B', 'webadmin', 'admin@web.com', 'tech', '', 5, '2011-08-18 09:56:10', '2011-11-28 00:05:27', 1, '2011-04-28 12:13:40', 0);

-- --------------------------------------------------------

--
-- 表的结构 `t_admingroup`
--

CREATE TABLE IF NOT EXISTS `t_admingroup` (
  `AdminGroupID` int(11) NOT NULL auto_increment,
  `AdminGroupName` varchar(200) character set utf8 default NULL,
  `LimitValues` longtext character set utf8,
  `ListID` int(11) NOT NULL,
  `AdminID` int(11) NOT NULL,
  `AddTime` datetime default NULL,
  `IsClose` int(11) NOT NULL,
  PRIMARY KEY  (`AdminGroupID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

--
-- 转存表中的数据 `t_admingroup`
--

INSERT INTO `t_admingroup` (`AdminGroupID`, `AdminGroupName`, `LimitValues`, `ListID`, `AdminID`, `AddTime`, `IsClose`) VALUES
(1, '系统管理组', '-1,Config,ConfigAdd,ConfigEdit,ConfigDel,ConfigAll,File,FileUpload,FileDel,FileAll,Class,ClassAdd,ClassEdit,ClassMove,ClassDel,ClassOpen,ClassAll,Article,ArticleAdd,ArticleEdit,ArticleDel,ArticleOpen,ArticleClose,ArticleAll,AdminGroup,AdminGroupAdd,AdminGroupEdit,AdminGroupDel,AdminGroupOpen,AdminGroupClose,AdminGroupSetAdmin,AdminGroupSetLimit,AdminGroupAll,Product,ProductAdd,ProductEdit,ProductDel,ProductOpen,ProductClose,ProductAll,Admin,AdminAdd,AdminEdit,AdminDel,AdminOpen,AdminClose,AdminSetAdminGroup,AdminAll,Feedback,FeedbackDel,FeedbackDeal,AdminLog,AdminLogDel,AdminLogAll,AdPosition,AdPositionAdd,AdPositionEdit,AdPositionDel,AdPositionOpen,AdPositionClose,AdPositionAll,Folder,FolderCreate,FolderDel,FolderAll,Ad,AdAdd,AdEdit,AdDel,AdOpen,AdClose,AdAll,-1', 1, 1, '2010-05-21 11:29:51', 0);

-- --------------------------------------------------------

--
-- 表的结构 `t_adminingroup`
--

CREATE TABLE IF NOT EXISTS `t_adminingroup` (
  `AdminID` int(11) NOT NULL,
  `AdminGroupID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- 转存表中的数据 `t_adminingroup`
--

INSERT INTO `t_adminingroup` (`AdminID`, `AdminGroupID`) VALUES
(5, 1);

-- --------------------------------------------------------

--
-- 表的结构 `t_adminlog`
--

CREATE TABLE IF NOT EXISTS `t_adminlog` (
  `AdminLogID` int(11) NOT NULL auto_increment,
  `LogContent` longtext character set utf8,
  `ScriptFile` varchar(200) character set utf8 default NULL,
  `IPAddress` varchar(200) character set utf8 default NULL,
  `AdminID` int(11) NOT NULL,
  `AddTime` datetime default NULL,
  PRIMARY KEY  (`AdminLogID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=1781 ;

--
-- 转存表中的数据 `t_adminlog`
--

INSERT INTO `t_adminlog` (`AdminLogID`, `LogContent`, `ScriptFile`, `IPAddress`, `AdminID`, `AddTime`) VALUES
(722, '删除编号为721的管理员日志!', '/admin/System/AdminLog.aspx', '127.0.0.1', 1, '2012-04-21 00:13:20'),
(723, '登录系统！', '/admin/login.aspx', '127.0.0.1', 1, '2012-04-21 23:08:29'),
(724, '登录系统！', '/admin/login.aspx', '127.0.0.1', 1, '2012-05-10 23:28:35'),
(725, '登录系统！', '/admin/login.aspx', '127.0.0.1', 1, '2012-05-10 23:28:36'),
(726, '上传文件"/Files/Ad/1.jpg"。', '/admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-05-10 23:29:30'),
(727, '上传文件"/Files/Class/1_8458.jpg"。', '/admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-05-10 23:30:33'),
(728, '上传文件"/Files/Class/1_1492.jpg"。', '/admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-05-10 23:30:44'),
(729, '登录系统！', '/admin/login.aspx', '127.0.0.1', 1, '2012-05-20 01:45:57'),
(730, '移动编号为2,3,4,5的栏目!', '/admin/System/Class_Move.aspx', '127.0.0.1', 1, '2012-05-20 01:46:11'),
(731, '移动编号为3,4的栏目!', '/admin/System/Class_Move.aspx', '127.0.0.1', 1, '2012-05-20 02:03:23'),
(732, '移动编号为3,4的栏目!', '/admin/System/Class_Move.aspx', '127.0.0.1', 1, '2012-05-20 02:08:44'),
(733, '移动编号为2,5的栏目!', '/admin/System/Class_Move.aspx', '127.0.0.1', 1, '2012-05-20 02:08:54'),
(734, '修改编号为2的栏目。', '/admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-05-20 02:09:11'),
(735, '登录系统！', '/admin/login.aspx', '127.0.0.1', 1, '2012-05-20 15:13:13'),
(736, '修改编号为4的栏目。', '/admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-05-20 15:13:50'),
(737, '修改编号为1的栏目。', '/admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-05-20 15:14:00'),
(738, '修改编号为2的栏目。', '/admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-05-20 15:14:23'),
(739, '修改编号为2的栏目。', '/admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-05-20 15:14:30'),
(740, '修改编号为3的栏目。', '/admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-05-20 15:15:12'),
(741, '修改编号为4的栏目。', '/admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-05-20 15:15:36'),
(742, '修改编号为5的栏目。', '/admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-05-20 15:16:02'),
(743, '添加名称为"在线调查"的栏目。', '/admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-05-20 15:17:01'),
(744, '添加名称为"联系我们"的栏目。', '/admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-05-20 15:17:43'),
(745, '修改编号为2的栏目。', '/admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-05-20 15:18:09'),
(746, '修改编号为7的栏目。', '/admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-05-20 15:18:59'),
(747, '修改编号为6的栏目。', '/admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-05-20 15:19:18'),
(748, '修改编号为7的栏目。', '/admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-05-20 15:19:52'),
(749, '添加名称为"在线调查"的栏目。', '/admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-05-20 15:20:42'),
(750, '添加名称为"友情链接"的栏目。', '/admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-05-20 15:21:04'),
(751, '添加名称为"网站地图"的栏目。', '/admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-05-20 15:21:28'),
(752, '添加名称为"信息反馈"的栏目。', '/admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-05-20 15:21:50'),
(753, '添加名称为"在线留言"的栏目。', '/admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-05-20 15:22:19'),
(754, '修改编号为4的栏目模板!', '/admin/System/ClassTemplate_Add.aspx', '127.0.0.1', 1, '2012-05-20 15:23:33'),
(755, '添加名称为"测试新闻"的文章。', '/admin/Article/Article_Add.aspx', '127.0.0.1', 1, '2012-05-20 17:44:39'),
(756, '修改编号为9的文章。', '/admin/Article/Article_Add.aspx', '127.0.0.1', 1, '2012-05-20 17:51:02'),
(757, '修改编号为9的文章。', '/admin/Article/Article_Add.aspx', '127.0.0.1', 1, '2012-05-20 17:54:20'),
(758, '删除文件"/Files/Class/1_1492.jpg"。', '/admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-05-20 18:18:00'),
(759, '删除文件"/Files/Class/1_8458.jpg"。', '/admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-05-20 18:18:00'),
(760, '添加名称为"产品测试"的产品。', '/admin/Product/Product_Add.aspx', '127.0.0.1', 1, '2012-05-20 18:18:22'),
(761, '添加名称为"招聘测试"的招聘。', '/admin/Job/Job_Add.aspx', '127.0.0.1', 1, '2012-05-20 18:52:41'),
(762, '修改编号为1的招聘。', '/admin/Job/Job_Add.aspx', '127.0.0.1', 1, '2012-05-20 19:04:34'),
(763, '添加名称为"下载测试"的下载。', '/admin/Download/Download_Add.aspx', '127.0.0.1', 1, '2012-05-20 19:32:43'),
(764, '修改编号为1的在线调查!', '/admin/Survey/Survey_Add.aspx', '127.0.0.1', 1, '2012-05-20 19:35:54'),
(765, '删除编号为1,2,3的调查结果!', '/admin/Survey/SurveyResult.aspx', '127.0.0.1', 1, '2012-05-20 19:36:24'),
(766, '修改编号为1的友情链接。', '/admin/Extension/Link_Add.aspx', '127.0.0.1', 1, '2012-05-20 20:06:26'),
(767, '修改编号为1的友情链接。', '/admin/Extension/Link_Add.aspx', '127.0.0.1', 1, '2012-05-20 20:26:42'),
(768, '修改编号为2的留言!', '/admin/Message/Guestbook_Add.aspx', '127.0.0.1', 1, '2012-05-20 21:06:48'),
(769, '修改编号为1的广告位。', '/admin/Extension/AdPosition_Add.aspx', '127.0.0.1', 1, '2012-05-20 21:52:51'),
(770, '修改编号为1的广告位。', '/admin/Extension/AdPosition_Add.aspx', '127.0.0.1', 1, '2012-05-20 21:55:08'),
(771, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-12 15:37:59'),
(772, '上传文件"/Files/Article/shu_qi.jpg"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-09-12 15:38:25'),
(773, '上传文件"/Files/Article/shu_qi_3713.jpg"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-09-12 15:38:31'),
(774, '添加名称为的图片/视频!', '/Admin/Article/ArticlePic_Add.aspx', '127.0.0.1', 1, '2012-09-12 15:38:33'),
(775, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-12 15:42:39'),
(776, '删除文件"/Files/Article/shu_qi.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-09-12 15:44:17'),
(777, '删除文件"/Files/Article/shu_qi_3713.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-09-12 15:44:17'),
(778, '上传文件"/Files/Article/shu_qi.jpg"。', '/Admin/Upload/File_BatchUpload_Dialog.aspx', '127.0.0.1', 1, '2012-09-12 15:48:26'),
(779, '上传文件"/Files/Article/xu_xi_lei.jpg"。', '/Admin/Upload/File_BatchUpload_Dialog.aspx', '127.0.0.1', 1, '2012-09-12 15:48:26'),
(780, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-12 16:13:23'),
(781, '删除文件"/Files/Article/shu_qi.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-09-12 16:13:53'),
(782, '删除文件"/Files/Article/xu_xi_lei.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-09-12 16:13:53'),
(783, '上传文件"/Files/Article/chen_hui_lin.jpg"。', '/Admin/Upload/File_BatchUpload_Dialog.aspx', '127.0.0.1', 1, '2012-09-12 16:32:02'),
(784, '上传文件"/Files/Article/xu_ruo_xuan.jpg"。', '/Admin/Upload/File_BatchUpload_Dialog.aspx', '127.0.0.1', 1, '2012-09-12 16:32:02'),
(785, '上传文件"/Files/Article/water lilies.jpg"。', '/Admin/Upload/File_BatchUpload_Dialog.aspx', '127.0.0.1', 1, '2012-09-12 16:32:02'),
(786, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-12 17:22:05'),
(787, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-12 17:38:37'),
(788, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-13 11:01:33'),
(789, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-13 11:36:19'),
(790, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-13 11:57:01'),
(791, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-13 12:50:53'),
(792, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-13 12:55:57'),
(793, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-13 13:41:37'),
(794, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-13 13:50:22'),
(795, '推荐编号为2的产品!', '/Admin/Product/Product.aspx', '127.0.0.1', 1, '2012-09-13 14:10:54'),
(796, '推荐编号为2的产品!', '/Admin/Product/Product.aspx', '127.0.0.1', 1, '2012-09-13 14:19:33'),
(797, '取消编号为2的产品推荐!', '/Admin/Product/Product.aspx', '127.0.0.1', 1, '2012-09-13 14:19:47'),
(798, '取消编号为2的产品推荐!', '/Admin/Product/Product.aspx', '127.0.0.1', 1, '2012-09-13 14:19:49'),
(799, '推荐编号为2的产品!', '/Admin/Product/Product.aspx', '127.0.0.1', 1, '2012-09-13 14:21:08'),
(800, '推荐编号为2的产品!', '/Admin/Product/Product.aspx', '127.0.0.1', 1, '2012-09-13 14:21:48'),
(801, '取消编号为2的产品推荐!', '/Admin/Product/Product.aspx', '127.0.0.1', 1, '2012-09-13 14:21:51'),
(802, '推荐编号为2的产品!', '/Admin/Product/Product.aspx', '127.0.0.1', 1, '2012-09-13 14:21:55'),
(803, '推荐编号为2的产品!', '/Admin/Product/Product.aspx', '127.0.0.1', 1, '2012-09-13 14:21:57'),
(804, '添加名称为"产品展示"的栏目。', '/Admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-09-13 14:25:38'),
(805, '添加名称为"立人电脑"的产品。', '/Admin/Product/Product_Add.aspx', '127.0.0.1', 1, '2012-09-13 14:27:21'),
(806, '开放编号为2的产品!', '/Admin/Product/Product.aspx', '127.0.0.1', 1, '2012-09-13 14:27:28'),
(807, '转移编号为2,3的产品!', '/Admin/Product/Product_Transfer.aspx', '127.0.0.1', 1, '2012-09-13 14:27:38'),
(808, '转移编号为2,3的产品!', '/Admin/Product/Product_Transfer.aspx', '127.0.0.1', 1, '2012-09-13 14:29:32'),
(809, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-13 14:43:04'),
(810, '复制编号为2,3的产品!', '/Admin/Product/Product_Copy.aspx', '127.0.0.1', 1, '2012-09-13 14:43:26'),
(811, '删除编号为4,5的产品!', '/Admin/Product/Product.aspx', '127.0.0.1', 1, '2012-09-13 14:48:26'),
(812, '复制编号为2,3的产品!', '/Admin/Product/Product_Copy.aspx', '127.0.0.1', 1, '2012-09-13 14:48:37'),
(813, '删除编号为6,7的产品!', '/Admin/Product/Product.aspx', '127.0.0.1', 1, '2012-09-13 14:48:59'),
(814, '复制编号为2,3的产品!', '/Admin/Product/Product_Copy.aspx', '127.0.0.1', 1, '2012-09-13 14:55:42'),
(815, '转移编号为8,9的产品!', '/Admin/Product/Product_Transfer.aspx', '127.0.0.1', 1, '2012-09-13 14:56:09'),
(816, '关闭编号为2,3,8,9的产品!', '/Admin/Product/Product.aspx', '127.0.0.1', 1, '2012-09-13 15:03:07'),
(817, '推荐编号为2,3,8,9的产品!', '/Admin/Product/Product.aspx', '127.0.0.1', 1, '2012-09-13 15:03:32'),
(818, '取消编号为2,3,8,9的产品推荐!', '/Admin/Product/Product.aspx', '127.0.0.1', 1, '2012-09-13 15:03:38'),
(819, '开放编号为2,3,8,9的产品!', '/Admin/Product/Product.aspx', '127.0.0.1', 1, '2012-09-13 15:05:41'),
(820, '关闭编号为2,3,8,9的产品!', '/Admin/Product/Product.aspx', '127.0.0.1', 1, '2012-09-13 15:06:08'),
(821, '关闭编号为2,3,8,9的产品!', '/Admin/Product/Product.aspx', '127.0.0.1', 1, '2012-09-13 15:06:18'),
(822, '开放编号为2,3,8,9的产品!', '/Admin/Product/Product.aspx', '127.0.0.1', 1, '2012-09-13 15:06:23'),
(823, '推荐编号为2,3,8,9的产品!', '/Admin/Product/Product.aspx', '127.0.0.1', 1, '2012-09-13 15:06:29'),
(824, '取消编号为2,3,8,9的产品推荐!', '/Admin/Product/Product.aspx', '127.0.0.1', 1, '2012-09-13 15:06:34'),
(825, '转移编号为2,3,8,9的产品!', '/Admin/Product/Product_Transfer.aspx', '127.0.0.1', 1, '2012-09-13 15:06:47'),
(826, '复制编号为2,3,8,9的产品!', '/Admin/Product/Product_Copy.aspx', '127.0.0.1', 1, '2012-09-13 15:06:54'),
(827, '关闭编号为2,3,8,9,10,11,12,13的产品!', '/Admin/Product/Product.aspx', '127.0.0.1', 1, '2012-09-13 15:08:19'),
(828, '开放编号为2,3,8,9,10,11,12,13的产品!', '/Admin/Product/Product.aspx', '127.0.0.1', 1, '2012-09-13 15:08:28'),
(829, '开放编号为8,9的产品!', '/Admin/Product/Product.aspx', '127.0.0.1', 1, '2012-09-13 15:26:12'),
(830, '关闭编号为8,9的产品!', '/Admin/Product/Product.aspx', '127.0.0.1', 1, '2012-09-13 15:26:17'),
(831, '复制编号为2,3,8,9,10,11,12,13的产品!', '/Admin/Product/Product_Copy.aspx', '127.0.0.1', 1, '2012-09-13 15:26:31'),
(832, '删除编号为3,8,9,10,11,12,13,14,15的产品!', '/Admin/Product/Product.aspx', '127.0.0.1', 1, '2012-09-13 15:29:56'),
(833, '删除编号为16,17,18,19,20,21的产品!', '/Admin/Product/Product.aspx', '127.0.0.1', 1, '2012-09-13 15:30:07'),
(834, '开放编号为2的产品!', '/Admin/Product/Product.aspx', '127.0.0.1', 1, '2012-09-13 15:30:22'),
(835, '复制编号为2的产品!', '/Admin/Product/Product_Copy.aspx', '127.0.0.1', 1, '2012-09-13 15:38:43'),
(836, '关闭编号为2,22的产品!', '/Admin/Product/Product.aspx', '127.0.0.1', 1, '2012-09-13 15:51:18'),
(837, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-13 16:22:17'),
(838, '开放编号为2,22的产品!', '/Admin/Product/Product.aspx', '127.0.0.1', 1, '2012-09-13 16:51:09'),
(839, '取消编号为2,22的产品推荐!', '/Admin/Product/Product.aspx', '127.0.0.1', 1, '2012-09-13 16:52:23'),
(840, '推荐编号为2,22的产品!', '/Admin/Product/Product.aspx', '127.0.0.1', 1, '2012-09-13 16:52:52'),
(841, '开放编号为2,22的产品!', '/Admin/Product/Product.aspx', '127.0.0.1', 1, '2012-09-13 16:52:59'),
(842, '推荐编号为2,22的产品!', '/Admin/Product/Product.aspx', '127.0.0.1', 1, '2012-09-13 16:54:41'),
(843, '关闭编号为2,22的产品!', '/Admin/Product/Product.aspx', '127.0.0.1', 1, '2012-09-13 16:56:52'),
(844, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-13 17:26:04'),
(845, '开放编号为2的产品!', '/Admin/Product/Product.aspx', '127.0.0.1', 1, '2012-09-13 17:43:01'),
(846, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-13 17:46:27'),
(847, '取消编号为2,22的产品推荐!', '/Admin/Product/Product.aspx', '127.0.0.1', 1, '2012-09-13 17:48:19'),
(848, '复制编号为2,22的产品!', '/Admin/Product/Product_Copy.aspx', '127.0.0.1', 1, '2012-09-13 17:48:26'),
(849, '转移编号为2,22,23,24的产品!', '/Admin/Product/Product_Transfer.aspx', '127.0.0.1', 1, '2012-09-13 17:48:33'),
(850, '开放编号为2,22,23,24的产品!', '/Admin/Product/Product.aspx', '127.0.0.1', 1, '2012-09-13 17:48:43'),
(851, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-14 09:34:31'),
(852, '删除编号为2的图片/视频!', '/Admin/Article/ArticlePic.aspx', '127.0.0.1', 1, '2012-09-14 10:38:17'),
(853, '上传文件"/Files/Article/123.jpg"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-09-14 10:38:38'),
(854, '上传文件"/Files/Article/123_7167.jpg"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-09-14 10:38:45'),
(855, '添加名称为的图片/视频!', '/Admin/Article/ArticlePic_Add.aspx', '127.0.0.1', 1, '2012-09-14 10:38:46'),
(856, '上传文件"/Files/Article/456.jpg"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-09-14 10:38:54'),
(857, '上传文件"/Files/Article/456_2714.jpg"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-09-14 10:39:00'),
(858, '添加名称为的图片/视频!', '/Admin/Article/ArticlePic_Add.aspx', '127.0.0.1', 1, '2012-09-14 10:39:02'),
(859, '上传文件"/Files/Article/123124435.jpg"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-09-14 10:39:10'),
(860, '上传文件"/Files/Article/123124435_2402.jpg"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-09-14 10:39:17'),
(861, '添加名称为的图片/视频!', '/Admin/Article/ArticlePic_Add.aspx', '127.0.0.1', 1, '2012-09-14 10:39:18'),
(862, '上传文件"/Files/Article/asdasd1.jpg"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-09-14 10:39:26'),
(863, '上传文件"/Files/Article/asdasd1_3503.jpg"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-09-14 10:39:34'),
(864, '添加名称为的图片/视频!', '/Admin/Article/ArticlePic_Add.aspx', '127.0.0.1', 1, '2012-09-14 10:39:35'),
(865, '上传文件"/Files/Article/sdfsf.jpg"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-09-14 10:39:42'),
(866, '上传文件"/Files/Article/sdfsf_8461.jpg"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-09-14 10:39:50'),
(867, '添加名称为的图片/视频!', '/Admin/Article/ArticlePic_Add.aspx', '127.0.0.1', 1, '2012-09-14 10:39:51'),
(868, '修改编号为9的文章。', '/Admin/Article/Article_Add.aspx', '127.0.0.1', 1, '2012-09-14 10:41:10'),
(869, '修改编号为9的文章。', '/Admin/Article/Article_Add.aspx', '127.0.0.1', 1, '2012-09-14 10:43:38'),
(870, '添加名称为"法国是的发个速度噶斯蒂芬"的文章。', '/Admin/Article/Article_Add.aspx', '127.0.0.1', 1, '2012-09-14 10:51:37'),
(871, '修改编号为2的产品。', '/Admin/Product/Product_Add.aspx', '127.0.0.1', 1, '2012-09-14 11:18:56'),
(872, '添加名称为"数风流人物还看今朝"的文章。', '/Admin/Article/Article_Add.aspx', '127.0.0.1', 1, '2012-09-14 11:35:41'),
(873, '添加名称为"莫使金蹲空对月"的文章。', '/Admin/Article/Article_Add.aspx', '127.0.0.1', 1, '2012-09-14 11:36:04'),
(874, '添加名称为"千金散尽还复来"的文章。', '/Admin/Article/Article_Add.aspx', '127.0.0.1', 1, '2012-09-14 11:36:22'),
(875, '添加名称为"你问我爱你有多深"的文章。', '/Admin/Article/Article_Add.aspx', '127.0.0.1', 1, '2012-09-14 11:36:38'),
(876, '修改编号为1的招聘。', '/Admin/Job/Job_Add.aspx', '127.0.0.1', 1, '2012-09-14 11:49:24'),
(877, '添加名称为调查钓鱼岛事件的在线调查!', '/Admin/Survey/Survey_Add.aspx', '127.0.0.1', 1, '2012-09-14 13:39:13'),
(878, '添加名称为调查黄岩岛事件的在线调查!', '/Admin/Survey/Survey_Add.aspx', '127.0.0.1', 1, '2012-09-14 13:39:30'),
(879, '添加名称为调查南海事件的在线调查!', '/Admin/Survey/Survey_Add.aspx', '127.0.0.1', 1, '2012-09-14 13:39:52'),
(880, '添加名称为"招聘开发"的招聘。', '/Admin/Job/Job_Add.aspx', '127.0.0.1', 1, '2012-09-14 13:42:30'),
(881, '添加名称为"招聘维护"的招聘。', '/Admin/Job/Job_Add.aspx', '127.0.0.1', 1, '2012-09-14 13:42:51'),
(882, '添加名称为"招聘项目经理"的招聘。', '/Admin/Job/Job_Add.aspx', '127.0.0.1', 1, '2012-09-14 13:43:19'),
(883, '添加名称为"招聘总经理"的招聘。', '/Admin/Job/Job_Add.aspx', '127.0.0.1', 1, '2012-09-14 13:43:41'),
(884, '修改编号为3的招聘。', '/Admin/Job/Job_Add.aspx', '127.0.0.1', 1, '2012-09-14 13:44:03'),
(885, '修改编号为4的招聘。', '/Admin/Job/Job_Add.aspx', '127.0.0.1', 1, '2012-09-14 13:44:11'),
(886, '修改编号为5的招聘。', '/Admin/Job/Job_Add.aspx', '127.0.0.1', 1, '2012-09-14 13:44:16'),
(887, '添加名称为"哈利波特全集"的下载。', '/Admin/Download/Download_Add.aspx', '127.0.0.1', 1, '2012-09-14 13:45:07'),
(888, '添加名称为"北京青年全集"的下载。', '/Admin/Download/Download_Add.aspx', '127.0.0.1', 1, '2012-09-14 13:45:25'),
(889, '添加名称为"轩辕剑全集"的下载。', '/Admin/Download/Download_Add.aspx', '127.0.0.1', 1, '2012-09-14 13:45:39'),
(890, '添加名称为"轩辕剑全集"的下载。', '/Admin/Download/Download_Add.aspx', '127.0.0.1', 1, '2012-09-14 13:45:40'),
(891, '删除编号为2,3,4,5的招聘信息!', '/Admin/Job/Job.aspx', '127.0.0.1', 1, '2012-09-14 14:01:08'),
(892, '复制编号为24的产品!', '/Admin/Product/Product_Copy.aspx', '127.0.0.1', 1, '2012-09-14 15:06:50'),
(893, '修改编号为1的的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-14 16:35:02'),
(894, '修改编号为1的的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-14 16:36:01'),
(895, '修改编号为1的的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-14 16:39:41'),
(896, '修改编号为1的的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-14 16:49:57'),
(897, '修改编号为1的的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-14 16:56:03'),
(898, '修改编号为1的的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-14 16:56:12'),
(899, '添加名称为阿萨德的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-14 16:58:33'),
(900, '删除编号为2的网站配置!', '/Admin/System/Config.aspx', '127.0.0.1', 1, '2012-09-14 16:59:01'),
(901, '修改编号为1的的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-14 17:00:57'),
(902, '修改编号为1的的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-14 17:01:03'),
(903, '修改编号为1的的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-14 17:08:25'),
(904, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-14 17:52:36'),
(905, '修改编号为1的的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-14 17:55:08'),
(906, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-15 09:13:47'),
(907, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-15 09:32:54'),
(908, '修改编号为1的的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-15 09:40:43'),
(909, '修改编号为1的的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-15 10:03:04'),
(910, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-15 10:17:35'),
(911, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-15 10:20:28'),
(912, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-15 10:22:20'),
(913, '修改编号为1的的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-15 10:22:42'),
(914, '修改编号为1的的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-15 10:22:58'),
(915, '修改编号为1的的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-15 10:23:26'),
(916, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-15 10:27:19'),
(917, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-15 10:30:02'),
(918, '添加名称为阿萨德的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-15 10:34:28'),
(919, '添加名称为阿萨德的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-15 10:35:09'),
(920, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-15 10:48:22'),
(921, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-15 10:48:23'),
(922, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-15 10:56:05'),
(923, '删除编号为4的网站配置!', '/Admin/System/Config.aspx', '127.0.0.1', 1, '2012-09-15 10:56:23'),
(924, '修改编号为1的的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-15 10:56:35'),
(925, '修改编号为1的的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-15 10:56:43'),
(926, '修改编号为1的的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-15 10:57:23'),
(927, '修改编号为1的的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-15 10:57:57'),
(928, '修改编号为1的的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-15 11:03:04'),
(929, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-15 11:14:27'),
(930, '修改编号为1的的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-15 11:18:23'),
(931, '修改编号为1的的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-15 11:21:38'),
(932, '修改编号为1的的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-15 11:22:44'),
(933, '修改编号为1的的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-15 11:22:57'),
(934, '修改编号为1的的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-15 11:23:46'),
(935, '修改编号为1的的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-15 11:24:58'),
(936, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-17 09:22:11'),
(937, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-17 10:00:06'),
(938, '修改编号为1的的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-17 10:00:48'),
(939, '修改编号为1的的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-17 10:06:10'),
(940, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-17 10:22:15'),
(941, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-17 11:15:43'),
(942, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-17 13:35:35'),
(943, '修改编号为1的的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-17 14:00:06'),
(944, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-17 15:36:23'),
(945, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-17 16:17:53'),
(946, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-17 16:20:18'),
(947, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-17 16:37:50'),
(948, '修改编号为1的的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-17 17:18:21'),
(949, '修改编号为1的的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-17 17:30:14'),
(950, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-17 17:49:26'),
(951, '修改编号为1的的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-17 17:51:16'),
(952, '修改编号为1的的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-17 18:01:16'),
(953, '修改编号为1的的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-17 18:34:03'),
(954, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-17 18:36:44'),
(955, '修改编号为1的的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-17 18:37:00'),
(956, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-17 19:17:50'),
(957, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-17 19:21:13'),
(958, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-17 19:23:29'),
(959, '修改编号为1的的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-17 19:26:26'),
(960, '修改编号为1的的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-17 19:52:11'),
(961, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-18 09:33:47'),
(962, '修改编号为1的的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-18 10:26:58'),
(963, '修改编号为1的的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-18 10:35:34'),
(964, '上传文件"/Files/Article/blue hills_5769.jpg"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-09-18 10:48:32'),
(965, '添加名称为的图片/视频!', '/Admin/Article/ArticlePic_Add.aspx', '127.0.0.1', 1, '2012-09-18 10:48:33'),
(966, '删除编号为1的图片/视频!', '/Admin/Article/ArticlePic.aspx', '127.0.0.1', 1, '2012-09-18 10:48:41'),
(967, '上传文件"/Files/Article/blue hills_5870.jpg"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-09-18 10:48:50'),
(968, '添加名称为的图片/视频!', '/Admin/Article/ArticlePic_Add.aspx', '127.0.0.1', 1, '2012-09-18 10:48:51'),
(969, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-18 11:57:31'),
(970, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-18 13:47:33'),
(971, '登录系统！', '/Admin/login.aspx', '127.0.0.1', 1, '2012-09-18 14:18:47'),
(972, '修改编号为1的的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-18 14:35:53'),
(973, '修改编号为1的的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-18 14:36:17'),
(974, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-18 15:08:44'),
(975, '添加名称为简体英文版的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-18 15:46:17'),
(976, '添加名称为"asdasd"的友情链接。', '/Admin/Extension/Link_Add.aspx', '127.0.0.1', 1, '2012-09-18 15:47:59'),
(977, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-18 16:20:38'),
(978, '修改编号为1的栏目。', '/Admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-09-18 16:30:09'),
(979, '删除编号为2的图片/视频!', '/Admin/Article/ArticlePic.aspx', '127.0.0.1', 1, '2012-09-18 16:38:22'),
(980, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-19 08:59:48'),
(981, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-19 10:09:40'),
(982, '删除编号为5的网站配置!', '/Admin/System/Config.aspx', '127.0.0.1', 1, '2012-09-19 10:38:39'),
(983, '删除编号为2的友情链接!', '/Admin/Extension/Link.aspx', '127.0.0.1', 1, '2012-09-19 10:38:53'),
(984, '修改编号为1的友情链接。', '/Admin/Extension/Link_Add.aspx', '127.0.0.1', 1, '2012-09-19 10:39:08'),
(985, '修改编号为1的友情链接。', '/Admin/Extension/Link_Add.aspx', '127.0.0.1', 1, '2012-09-19 10:55:03'),
(986, '修改编号为1的友情链接。', '/Admin/Extension/Link_Add.aspx', '127.0.0.1', 1, '2012-09-19 10:55:09'),
(987, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-19 11:08:54'),
(988, '修改编号为1的友情链接。', '/Admin/Extension/Link_Add.aspx', '127.0.0.1', 1, '2012-09-19 11:10:40'),
(989, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-19 11:24:15'),
(990, '修改编号为1的友情链接。', '/Admin/Extension/Link_Add.aspx', '127.0.0.1', 1, '2012-09-19 11:32:54'),
(991, '添加名称为"新浪"的友情链接。', '/Admin/Extension/Link_Add.aspx', '127.0.0.1', 1, '2012-09-19 14:02:59'),
(992, '添加名称为"腾讯"的友情链接。', '/Admin/Extension/Link_Add.aspx', '127.0.0.1', 1, '2012-09-19 14:03:58'),
(993, '修改编号为4的友情链接。', '/Admin/Extension/Link_Add.aspx', '127.0.0.1', 1, '2012-09-19 14:05:28'),
(994, '修改编号为4的友情链接。', '/Admin/Extension/Link_Add.aspx', '127.0.0.1', 1, '2012-09-19 14:05:57'),
(995, '添加名称为英文网站的网站配置!', '/Admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-09-19 14:07:49'),
(996, '添加名称为"博客园"的友情链接。', '/Admin/Extension/Link_Add.aspx', '127.0.0.1', 1, '2012-09-19 14:08:21'),
(997, '转移编号为1的友情链接!', '/Admin/Extension/Link_Transfer.aspx', '127.0.0.1', 1, '2012-09-19 14:25:42'),
(998, '转移编号为1的友情链接!', '/Admin/Extension/Link_Transfer.aspx', '127.0.0.1', 1, '2012-09-19 14:30:21'),
(999, '转移编号为1的友情链接!', '/Admin/Extension/Link_Transfer.aspx', '127.0.0.1', 1, '2012-09-19 14:30:40'),
(1000, '转移编号为1的友情链接!', '/Admin/Extension/Link_Transfer.aspx', '127.0.0.1', 1, '2012-09-19 14:32:41'),
(1001, '转移编号为1的友情链接!', '/Admin/Extension/Link_Transfer.aspx', '127.0.0.1', 1, '2012-09-19 14:34:19'),
(1002, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-19 15:29:47'),
(1003, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-19 16:14:18'),
(1004, '添加名称为视频的栏目属性!', '/Admin/System/ClassProperty_Add.aspx', '127.0.0.1', 1, '2012-09-19 16:14:44'),
(1005, '添加名称为默认模版的栏目模板!', '/Admin/System/ClassTemplate_Add.aspx', '127.0.0.1', 1, '2012-09-19 16:48:34'),
(1006, '添加名称为"视频查看"的栏目。', '/Admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-09-19 16:49:22'),
(1007, '添加名称为好视频的Video!', '/Admin/Video/Video_Add.aspx', '127.0.0.1', 1, '2012-09-19 16:49:54'),
(1008, '修改编号为1的Video!', '/Admin/Video/Video_Add.aspx', '127.0.0.1', 1, '2012-09-19 16:56:37'),
(1009, '修改编号为1的Video!', '/Admin/Video/Video_Add.aspx', '127.0.0.1', 1, '2012-09-19 16:56:47'),
(1010, '修改编号为1的Video!', '/Admin/Video/Video_Add.aspx', '127.0.0.1', 1, '2012-09-19 16:56:53'),
(1011, '关闭编号为1的Video!', '/Admin/Video/Video.aspx', '127.0.0.1', 1, '2012-09-19 17:03:23'),
(1012, '关闭编号为1的视频!', '/Admin/Video/Video.aspx', '127.0.0.1', 1, '2012-09-19 17:09:19'),
(1013, '开放编号为1的视频!', '/Admin/Video/Video.aspx', '127.0.0.1', 1, '2012-09-19 17:09:38'),
(1014, '关闭编号为10的文章!', '/Admin/Article/Article.aspx', '127.0.0.1', 1, '2012-09-19 17:09:54'),
(1015, '开放编号为10的文章!', '/Admin/Article/Article.aspx', '127.0.0.1', 1, '2012-09-19 17:09:59'),
(1016, '关闭编号为1的视频!', '/Admin/Video/Video.aspx', '127.0.0.1', 1, '2012-09-19 17:10:16'),
(1017, '开放编号为1的视频!', '/Admin/Video/Video.aspx', '127.0.0.1', 1, '2012-09-19 17:13:09'),
(1018, '开放编号为1的视频!', '/Admin/Video/Video.aspx', '127.0.0.1', 1, '2012-09-19 17:15:58'),
(1019, '开放编号为1的视频!', '/Admin/Video/Video.aspx', '127.0.0.1', 1, '2012-09-19 17:25:22'),
(1020, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-19 17:28:41'),
(1021, '关闭编号为1的视频!', '/Admin/Video/Video.aspx', '127.0.0.1', 1, '2012-09-19 17:30:44'),
(1022, '开放编号为1的视频!', '/Admin/Video/Video.aspx', '127.0.0.1', 1, '2012-09-19 17:33:31'),
(1023, '关闭编号为1的视频!', '/Admin/Video/Video.aspx', '127.0.0.1', 1, '2012-09-19 17:33:34'),
(1024, '开放编号为1的视频!', '/Admin/Video/Video.aspx', '127.0.0.1', 1, '2012-09-19 17:33:41'),
(1025, '开放编号为1的视频!', '/Admin/Video/Video.aspx', '127.0.0.1', 1, '2012-09-19 17:33:54'),
(1026, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-19 18:21:50'),
(1027, '开放编号为1的视频!', '/Admin/Video/Video.aspx', '127.0.0.1', 1, '2012-09-19 18:22:18'),
(1028, '关闭编号为1的视频!', '/Admin/Video/Video.aspx', '127.0.0.1', 1, '2012-09-19 18:22:22'),
(1029, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-20 09:01:58'),
(1030, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-20 09:27:31'),
(1031, '上传文件"/Files/Article/chen_yi_han.jpg"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-09-20 09:27:50'),
(1032, '添加名称为"阿斯斯蒂芬"的文章。', '/Admin/Article/Article_Add.aspx', '127.0.0.1', 1, '2012-09-20 09:28:06'),
(1033, '上传文件"/Files/Video/lin_zhi_ling.jpg"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-09-20 09:38:34'),
(1034, '修改编号为1的Video!', '/Admin/Video/Video_Add.aspx', '127.0.0.1', 1, '2012-09-20 09:38:36'),
(1035, '上传文件"/Files/Video/blue hills.jpg"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-09-20 09:40:01'),
(1036, '上传文件"/Files/Video/winter.jpg"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-09-20 09:40:11'),
(1037, '上传文件"/Files/Video/chen_yi_han.jpg"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-09-20 09:42:42'),
(1038, '修改编号为1的Video!', '/Admin/Video/Video_Add.aspx', '127.0.0.1', 1, '2012-09-20 09:43:38'),
(1039, '上传文件"/Files/Video/VideoPath/windows xp 启动.wav"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-09-20 09:50:25'),
(1040, '修改编号为1的Video!', '/Admin/Video/Video_Add.aspx', '127.0.0.1', 1, '2012-09-20 09:50:27'),
(1041, '上传文件"/Files/Video/VideoPic/123.jpg"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-09-20 09:51:28'),
(1042, '修改编号为1的Video!', '/Admin/Video/Video_Add.aspx', '127.0.0.1', 1, '2012-09-20 09:51:30'),
(1043, '修改编号为1的Video!', '/Admin/Video/Video_Add.aspx', '127.0.0.1', 1, '2012-09-20 09:53:20'),
(1044, '上传文件"/Files/Video/VideoPath/soundtest.wav"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-09-20 09:53:56'),
(1045, '开放编号为1的视频!', '/Admin/Video/Video.aspx', '127.0.0.1', 1, '2012-09-20 09:55:46'),
(1046, '修改编号为1的Video!', '/Admin/Video/Video_Add.aspx', '127.0.0.1', 1, '2012-09-20 09:56:14'),
(1047, '修改编号为1的Video!', '/Admin/Video/Video_Add.aspx', '127.0.0.1', 1, '2012-09-20 09:56:27'),
(1048, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-25 13:40:13'),
(1049, '添加名称为相册查看的栏目属性!', '/Admin/System/ClassProperty_Add.aspx', '127.0.0.1', 1, '2012-09-25 13:42:17'),
(1050, '修改编号为13的栏目属性!', '/Admin/System/ClassProperty_Add.aspx', '127.0.0.1', 1, '2012-09-25 13:43:39'),
(1051, '添加名称为相册的栏目模板!', '/Admin/System/ClassTemplate_Add.aspx', '127.0.0.1', 1, '2012-09-25 13:44:33'),
(1052, '添加名称为"相册查看"的栏目。', '/Admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-09-25 13:45:10'),
(1053, '修改编号为444的权限字段。', '/Admin/System/Limit_Add.aspx', '127.0.0.1', 1, '2012-09-25 14:12:49'),
(1054, '修改编号为445的权限字段。', '/Admin/System/Limit_Add.aspx', '127.0.0.1', 1, '2012-09-25 14:12:53'),
(1055, '添加名称为"视频管理"的权限字段。', '/Admin/System/Limit_Add.aspx', '127.0.0.1', 1, '2012-09-25 14:14:44'),
(1056, '添加名称为"添加"的权限字段。', '/Admin/System/Limit_Add.aspx', '127.0.0.1', 1, '2012-09-25 14:15:48'),
(1057, '添加名称为"修改"的权限字段。', '/Admin/System/Limit_Add.aspx', '127.0.0.1', 1, '2012-09-25 14:16:56'),
(1058, '添加名称为"删除"的权限字段。', '/Admin/System/Limit_Add.aspx', '127.0.0.1', 1, '2012-09-25 14:17:25'),
(1059, '添加名称为"批量开放"的权限字段。', '/Admin/System/Limit_Add.aspx', '127.0.0.1', 1, '2012-09-25 14:17:42'),
(1060, '添加名称为"批量关闭"的权限字段。', '/Admin/System/Limit_Add.aspx', '127.0.0.1', 1, '2012-09-25 14:17:52'),
(1061, '添加名称为"相册管理"的权限字段。', '/Admin/System/Limit_Add.aspx', '127.0.0.1', 1, '2012-09-25 14:18:09'),
(1062, '添加名称为"添加"的权限字段。', '/Admin/System/Limit_Add.aspx', '127.0.0.1', 1, '2012-09-25 14:18:36'),
(1063, '添加名称为"修改"的权限字段。', '/Admin/System/Limit_Add.aspx', '127.0.0.1', 1, '2012-09-25 14:18:45'),
(1064, '添加名称为"删除"的权限字段。', '/Admin/System/Limit_Add.aspx', '127.0.0.1', 1, '2012-09-25 14:18:53'),
(1065, '添加名称为"批量开放"的权限字段。', '/Admin/System/Limit_Add.aspx', '127.0.0.1', 1, '2012-09-25 14:19:08'),
(1066, '添加名称为"批量关闭"的权限字段。', '/Admin/System/Limit_Add.aspx', '127.0.0.1', 1, '2012-09-25 14:19:22'),
(1067, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-25 14:20:24'),
(1068, '上传文件"/Files/Photo/SmallPicPic/lin_zhi_ling.jpg"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-09-25 14:41:55'),
(1069, '上传文件"/Files/Photo/BigPic/lin_zhi_ling.jpg"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-09-25 14:42:00'),
(1070, '上传文件"/Files/Photo/SmallPic/lin_zhi_ling.jpg"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-09-25 14:42:57'),
(1071, '上传文件"/Files/Photo/BigPic/lin_zhi_ling_1344.jpg"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-09-25 14:43:04'),
(1072, '添加名称为一家人的Photo!', '/Admin/Photo/Photo_Add.aspx', '127.0.0.1', 1, '2012-09-25 14:43:13'),
(1073, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-25 15:01:39'),
(1074, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-25 15:11:40'),
(1075, '添加名称为阿萨德的Photo!', '/Admin/Photo/Photo_Add.aspx', '127.0.0.1', 1, '2012-09-25 15:32:51'),
(1076, '修改编号为2的Photo!', '/Admin/Photo/Photo_Add.aspx', '127.0.0.1', 1, '2012-09-25 15:38:04'),
(1077, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-09-25 16:06:01'),
(1078, '修改编号为12的栏目模板!', '/Admin/System/ClassTemplate_Add.aspx', '127.0.0.1', 1, '2012-09-25 16:10:10'),
(1079, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-09 09:29:08'),
(1080, '删除编号为4的友情链接!', '/Admin/Extension/Link.aspx', '127.0.0.1', 1, '2012-10-09 09:36:20'),
(1081, '登录系统！', '/Admin/login.aspx', '127.0.0.1', 1, '2012-10-09 13:56:40'),
(1082, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-10 09:45:31'),
(1083, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-10 09:50:14'),
(1084, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-10 10:03:48'),
(1085, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-10 10:54:24'),
(1086, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-10 10:54:25'),
(1087, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-10 11:52:36'),
(1088, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-10 14:08:45'),
(1089, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-10 14:13:05'),
(1090, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-10 14:14:19'),
(1091, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-10 14:15:41'),
(1092, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-10 17:31:19'),
(1093, '登录系统！', '/admin/login.aspx', '127.0.0.1', 1, '2012-10-12 16:18:56'),
(1094, '修改编号为1的的网站配置!', '/admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-10-12 16:28:36'),
(1095, '修改编号为6的的网站配置!', '/admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-10-12 16:29:45'),
(1096, '修改编号为1的的网站配置!', '/admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-10-12 16:29:50'),
(1097, '推荐编号为2,22的产品!', '/admin/Product/Product.aspx', '127.0.0.1', 1, '2012-10-12 16:55:23'),
(1098, '取消编号为2,22的产品推荐!', '/admin/Product/Product.aspx', '127.0.0.1', 1, '2012-10-12 16:55:28'),
(1099, '复制编号为2,22的产品!', '/admin/Product/Product_Copy.aspx', '127.0.0.1', 1, '2012-10-12 16:55:37'),
(1100, '添加名称为"复制"的权限字段。', '/admin/System/Limit_Add.aspx', '127.0.0.1', 1, '2012-10-12 17:40:27'),
(1101, '修改编号为458的权限字段。', '/admin/System/Limit_Add.aspx', '127.0.0.1', 1, '2012-10-12 17:40:36'),
(1102, '复制编号为27的产品!', '/admin/Product/Product_Copy.aspx', '127.0.0.1', 1, '2012-10-12 17:41:46'),
(1103, '修改编号为1的的网站配置!', '/admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-10-12 19:07:33'),
(1104, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-13 09:03:15'),
(1105, '登录系统！', '/admin/login.aspx', '127.0.0.1', 1, '2012-10-13 09:06:15'),
(1106, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-13 09:27:30'),
(1107, '上传文件"/Files/Product/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-13 09:31:58'),
(1108, '上传文件"/Files/Product/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-13 09:31:58'),
(1109, '上传文件"/Files/Product/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-13 09:31:58'),
(1110, '删除文件"/Files/Product/123.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-13 09:32:18'),
(1111, '删除文件"/Files/Product/sl_123.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-13 09:32:18'),
(1112, '删除文件"/Files/Product/wzsy_123.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-13 09:32:18'),
(1113, '上传文件"/Files/Product/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-13 09:32:30'),
(1114, '上传文件"/Files/Product/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-13 09:32:30'),
(1115, '上传文件"/Files/Product/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-13 09:32:30'),
(1116, '删除文件"/Files/Product/123.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-13 09:32:46'),
(1117, '删除文件"/Files/Product/sl_123.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-13 09:32:46'),
(1118, '删除文件"/Files/Product/wzsy_123.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-13 09:32:46'),
(1119, '上传文件"/Files/Product/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-13 09:32:53'),
(1120, '上传文件"/Files/Product/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-13 09:32:53'),
(1121, '上传文件"/Files/Product/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-13 09:32:53'),
(1122, '删除文件"/Files/Product/123.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-13 09:32:58'),
(1123, '删除文件"/Files/Product/sl_123.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-13 09:32:58'),
(1124, '删除文件"/Files/Product/wzsy_123.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-13 09:32:58'),
(1125, '上传文件"/Files/Product/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-13 09:33:16'),
(1126, '上传文件"/Files/Product/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-13 09:33:16'),
(1127, '上传文件"/Files/Product/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-13 09:33:16'),
(1128, '上传文件"/Files/Product/Blue hills.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-13 09:33:48'),
(1129, '上传文件"/Files/Product/Blue hills.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-13 09:33:48'),
(1130, '上传文件"/Files/Product/Blue hills.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-13 09:33:48'),
(1131, '上传文件"/Files/Product/456.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-13 09:33:48'),
(1132, '上传文件"/Files/Product/456.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-13 09:33:48'),
(1133, '上传文件"/Files/Product/456.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-13 09:33:48'),
(1134, '上传文件"/Files/Product/123124435.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-13 09:33:48'),
(1135, '上传文件"/Files/Product/123124435.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-13 09:33:48'),
(1136, '上传文件"/Files/Product/123124435.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-13 09:33:48'),
(1137, '上传文件"/Files/Product/asdasd1.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-13 09:33:48'),
(1138, '上传文件"/Files/Product/asdasd1.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-13 09:33:48'),
(1139, '上传文件"/Files/Product/asdasd1.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-13 09:33:48'),
(1140, '删除文件"/Files/Product/123.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-13 09:33:57'),
(1141, '删除文件"/Files/Product/123124435.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-13 09:33:57'),
(1142, '删除文件"/Files/Product/456.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-13 09:33:57'),
(1143, '删除文件"/Files/Product/asdasd1.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-13 09:33:57'),
(1144, '删除文件"/Files/Product/Blue hills.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-13 09:33:57'),
(1145, '删除文件"/Files/Product/sl_123.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-13 09:33:57'),
(1146, '删除文件"/Files/Product/sl_123124435.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-13 09:33:57'),
(1147, '删除文件"/Files/Product/sl_456.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-13 09:33:57'),
(1148, '删除文件"/Files/Product/sl_asdasd1.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-13 09:33:57'),
(1149, '删除文件"/Files/Product/sl_Blue hills.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-13 09:33:57'),
(1150, '删除文件"/Files/Product/wzsy_123.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-13 09:33:57'),
(1151, '删除文件"/Files/Product/wzsy_123124435.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-13 09:34:02'),
(1152, '删除文件"/Files/Product/wzsy_456.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-13 09:34:02'),
(1153, '删除文件"/Files/Product/wzsy_asdasd1.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-13 09:34:02'),
(1154, '删除文件"/Files/Product/wzsy_Blue hills.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-13 09:34:02'),
(1155, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-13 09:47:24'),
(1156, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-13 09:48:50'),
(1157, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-13 10:08:58'),
(1158, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-13 11:39:28'),
(1159, '添加名称为的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-13 11:41:44'),
(1160, '添加名称为的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-13 11:45:11'),
(1161, '添加名称为的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-13 11:45:19'),
(1162, '添加名称为的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-13 11:46:57'),
(1163, '添加名称为的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-13 11:47:56'),
(1164, '添加名称为的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-13 11:48:34'),
(1165, '添加名称为的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-13 11:49:13'),
(1166, '添加名称为的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-13 12:04:14');
INSERT INTO `t_adminlog` (`AdminLogID`, `LogContent`, `ScriptFile`, `IPAddress`, `AdminID`, `AddTime`) VALUES
(1167, '添加名称为的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-13 12:09:13'),
(1168, '添加名称为的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-13 12:10:25'),
(1169, '添加名称为的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-13 12:11:24'),
(1170, '添加名称为的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-13 12:12:35'),
(1171, '添加名称为的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-13 12:13:19'),
(1172, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-15 09:19:17'),
(1173, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-15 09:55:45'),
(1174, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-15 10:31:15'),
(1175, '登录系统！', '/admin/login.aspx', '127.0.0.1', 1, '2012-10-15 10:37:52'),
(1176, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-15 10:58:02'),
(1177, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-15 11:39:04'),
(1178, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-15 11:43:42'),
(1179, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-15 11:48:10'),
(1180, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-15 13:41:41'),
(1181, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-15 14:02:04'),
(1182, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-15 14:12:32'),
(1183, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-15 14:14:27'),
(1184, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-15 14:18:23'),
(1185, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-15 14:23:11'),
(1186, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-15 14:27:24'),
(1187, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-15 14:32:10'),
(1188, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-15 14:48:03'),
(1189, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-15 14:49:53'),
(1190, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-15 14:57:01'),
(1191, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-15 14:58:48'),
(1192, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-15 15:01:41'),
(1193, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-15 15:04:07'),
(1194, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-15 15:06:19'),
(1195, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-15 15:07:46'),
(1196, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-15 15:29:32'),
(1197, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-15 15:37:07'),
(1198, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-15 16:01:31'),
(1199, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-15 16:42:54'),
(1200, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-15 16:45:04'),
(1201, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-15 16:45:04'),
(1202, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-15 17:36:14'),
(1203, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-15 17:44:55'),
(1204, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-15 18:11:09'),
(1205, '登录系统！', '/admin/login.aspx', '127.0.0.1', 1, '2012-10-15 18:39:50'),
(1206, '上传文件"/Files/Article/20115710219384.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-15 18:40:22'),
(1207, '上传文件"/Files/Article/20115710219384.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-15 18:40:22'),
(1208, '上传文件"/Files/Article/201142020318542.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-15 18:40:22'),
(1209, '上传文件"/Files/Article/201142020318542.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-15 18:40:22'),
(1210, '上传文件"/Files/Article/201142511411903.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-15 18:40:22'),
(1211, '上传文件"/Files/Article/201142511411903.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-15 18:40:22'),
(1212, '上传文件"/Files/Article/201142983352489.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-15 18:40:22'),
(1213, '上传文件"/Files/Article/201142983352489.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-15 18:40:22'),
(1214, '上传文件"/Files/Article/201157101436325.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-15 18:40:22'),
(1215, '上传文件"/Files/Article/201157101436325.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-15 18:40:22'),
(1216, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-16 08:47:33'),
(1217, '登录系统！', '/admin/login.aspx', '127.0.0.1', 1, '2012-10-16 09:25:22'),
(1218, '登录系统！', '/admin/login.aspx', '127.0.0.1', 1, '2012-10-16 09:25:23'),
(1219, '修改编号为1的的网站配置!', '/admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-10-16 09:43:41'),
(1220, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-16 09:54:57'),
(1221, '修改编号为1的的网站配置!', '/admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-10-16 10:14:50'),
(1222, '修改编号为1的的网站配置!', '/admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-10-16 10:14:55'),
(1223, '修改编号为1的的网站配置!', '/admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-10-16 10:15:00'),
(1224, '修改编号为1的的网站配置!', '/admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-10-16 10:15:18'),
(1225, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-16 10:15:16'),
(1226, '修改编号为6的的网站配置!', '/admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-10-16 10:22:37'),
(1227, '修改编号为6的的网站配置!', '/admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-10-16 10:31:39'),
(1228, '修改编号为6的的网站配置!', '/admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-10-16 10:31:57'),
(1229, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-16 10:33:28'),
(1230, '修改编号为6的的网站配置!', '/admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-10-16 10:39:46'),
(1231, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-16 10:52:28'),
(1232, '删除编号为422的权限字段!', '/admin/System/Limit.aspx', '127.0.0.1', 1, '2012-10-16 10:59:17'),
(1233, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-16 10:57:41'),
(1234, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-16 11:02:49'),
(1235, '添加名称为"转移"的权限字段。', '/admin/System/Limit_Add.aspx', '127.0.0.1', 1, '2012-10-16 11:06:52'),
(1236, '修改编号为459的权限字段。', '/admin/System/Limit_Add.aspx', '127.0.0.1', 1, '2012-10-16 11:07:00'),
(1237, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-16 11:18:41'),
(1238, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-16 11:30:28'),
(1239, '修改编号为1的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-16 11:31:20'),
(1240, '修改编号为1的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-16 11:33:24'),
(1241, '修改编号为2的栏目。', '/admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-10-16 11:36:44'),
(1242, '修改编号为3的栏目。', '/admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-10-16 11:36:49'),
(1243, '修改编号为4的栏目。', '/admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-10-16 11:36:53'),
(1244, '修改编号为5的栏目。', '/admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-10-16 11:36:56'),
(1245, '修改编号为6的栏目。', '/admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-10-16 11:37:00'),
(1246, '修改编号为7的栏目。', '/admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-10-16 11:37:04'),
(1247, '修改编号为13的栏目。', '/admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-10-16 11:37:12'),
(1248, '修改编号为8的栏目。', '/admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-10-16 11:37:19'),
(1249, '修改编号为9的栏目。', '/admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-10-16 11:37:22'),
(1250, '修改编号为10的栏目。', '/admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-10-16 11:37:25'),
(1251, '修改编号为11的栏目。', '/admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-10-16 11:37:28'),
(1252, '修改编号为12的栏目。', '/admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-10-16 11:37:31'),
(1253, '上传文件"/Files/Product/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 11:42:19'),
(1254, '添加名称为"测试图片"的产品。', '/Admin/Product/Product_Add.aspx', '127.0.0.1', 1, '2012-10-16 11:44:15'),
(1255, '修改编号为29的产品。', '/Admin/Product/Product_Add.aspx', '127.0.0.1', 1, '2012-10-16 11:46:50'),
(1256, '修改编号为29的产品。', '/Admin/Product/Product_Add.aspx', '127.0.0.1', 1, '2012-10-16 11:49:19'),
(1257, '修改编号为29的产品。', '/Admin/Product/Product_Add.aspx', '127.0.0.1', 1, '2012-10-16 11:50:34'),
(1258, '修改编号为29的产品。', '/Admin/Product/Product_Add.aspx', '127.0.0.1', 1, '2012-10-16 11:51:49'),
(1259, '修改编号为29的产品。', '/Admin/Product/Product_Add.aspx', '127.0.0.1', 1, '2012-10-16 11:52:45'),
(1260, '修改编号为29的产品。', '/Admin/Product/Product_Add.aspx', '127.0.0.1', 1, '2012-10-16 11:53:35'),
(1261, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-16 13:38:46'),
(1262, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-16 13:41:35'),
(1263, '上传文件"/Files/Product/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 13:45:52'),
(1264, '修改编号为29的产品。', '/Admin/Product/Product_Add.aspx', '127.0.0.1', 1, '2012-10-16 13:45:58'),
(1265, '上传文件"/Files/Product/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 13:47:58'),
(1266, '修改编号为29的产品。', '/Admin/Product/Product_Add.aspx', '127.0.0.1', 1, '2012-10-16 13:48:02'),
(1267, '修改编号为1的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-16 13:52:15'),
(1268, '修改编号为1的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-16 14:03:30'),
(1269, '上传文件"/Files/Video/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 14:04:25'),
(1270, '修改编号为9的文章。', '/Admin/Article/Article_Add.aspx', '127.0.0.1', 1, '2012-10-16 14:04:39'),
(1271, '修改编号为1的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-16 14:05:55'),
(1272, '修改编号为1的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-16 14:09:40'),
(1273, '上传文件"/Files/Product/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 14:12:32'),
(1274, '修改编号为29的产品。', '/Admin/Product/Product_Add.aspx', '127.0.0.1', 1, '2012-10-16 14:12:40'),
(1275, '修改编号为1的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-16 14:13:54'),
(1276, '修改编号为29的产品。', '/Admin/Product/Product_Add.aspx', '127.0.0.1', 1, '2012-10-16 14:14:08'),
(1277, '修改编号为1的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-16 14:14:28'),
(1278, '修改编号为9的文章。', '/Admin/Article/Article_Add.aspx', '127.0.0.1', 1, '2012-10-16 14:15:11'),
(1279, '修改编号为9的文章。', '/Admin/Article/Article_Add.aspx', '127.0.0.1', 1, '2012-10-16 14:18:01'),
(1280, '修改编号为1的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-16 14:18:37'),
(1281, '上传文件"/Files/Photo/BigPic/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 14:19:55'),
(1282, '修改编号为1的Photo!', '/Admin/Photo/Photo_Add.aspx', '127.0.0.1', 1, '2012-10-16 14:19:59'),
(1283, '修改编号为1的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-16 14:20:28'),
(1284, '修改编号为1的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-16 14:27:52'),
(1285, '上传文件"/Files/Article/BigPic/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 14:28:47'),
(1286, '添加名称为"没有什么好说的"的文章。', '/Admin/Article/Article_Add.aspx', '127.0.0.1', 1, '2012-10-16 14:28:53'),
(1287, '修改编号为1的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-16 14:29:01'),
(1288, '上传文件"/Files/Product/BigPic/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 14:31:16'),
(1289, '添加名称为"水印测试"的产品。', '/Admin/Product/Product_Add.aspx', '127.0.0.1', 1, '2012-10-16 14:31:20'),
(1290, '修改编号为1的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-16 14:31:25'),
(1291, '上传文件"/Files/Photo/BigPic/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 14:32:28'),
(1292, '添加名称为测试相册的Photo!', '/Admin/Photo/Photo_Add.aspx', '127.0.0.1', 1, '2012-10-16 14:32:31'),
(1293, '修改编号为1的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-16 14:32:37'),
(1294, '修改编号为1的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-16 14:47:52'),
(1295, '修改编号为1的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-16 14:47:58'),
(1296, '修改编号为1的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-16 15:19:15'),
(1297, '修改编号为1的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-16 15:19:26'),
(1298, '修改编号为1的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-16 15:19:32'),
(1299, '修改编号为1的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-16 15:19:45'),
(1300, '上传文件"/Files/Product/BigPic/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 15:59:19'),
(1301, '修改编号为1的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-16 16:00:12'),
(1302, '修改编号为1的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-16 16:00:45'),
(1303, '上传文件"/Files/Product/BigPic/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 16:01:26'),
(1304, '上传文件"/Files/Product/BigPic/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 16:01:26'),
(1305, '上传文件"/Files/Product/BigPic/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 16:04:46'),
(1306, '上传文件"/Files/Product/BigPic/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 16:04:46'),
(1307, '修改编号为1的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-16 16:05:13'),
(1308, '上传文件"/Files/Product/BigPic/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 16:05:51'),
(1309, '上传文件"/Files/Product/BigPic/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 16:05:51'),
(1310, '修改编号为1的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-16 16:06:28'),
(1311, '上传文件"/Files/Product/BigPic/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 16:06:45'),
(1312, '上传文件"/Files/Product/BigPic/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 16:06:45'),
(1313, '上传文件"/Files/Product/BigPic/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 16:35:26'),
(1314, '上传文件"/Files/Product/BigPic/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 16:35:26'),
(1315, '删除文件"/Files/Product/BigPic/123.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-16 16:35:46'),
(1316, '删除文件"/Files/Product/BigPic/wzsy_123.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-16 16:35:46'),
(1317, '修改编号为1的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-16 16:49:47'),
(1318, '修改编号为1的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-16 16:50:05'),
(1319, '上传文件"/Files/Product/BigPic/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 16:50:41'),
(1320, '上传文件"/Files/Product/BigPic/tpsy_123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 16:50:41'),
(1321, '上传文件"/Files/Product/BigPic/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 16:54:21'),
(1322, '上传文件"/Files/Product/BigPic/tpsy_123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 16:54:21'),
(1323, '上传文件"/Files/Product/BigPic/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 16:59:13'),
(1324, '上传文件"/Files/Product/BigPic/tpsy_123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 16:59:13'),
(1325, '修改编号为1的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-16 17:06:12'),
(1326, '修改编号为1的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-16 17:15:51'),
(1327, '上传文件"/Files/Product/BigPic/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 17:19:09'),
(1328, '上传文件"/Files/Product/BigPic/tpsy_123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 17:19:09'),
(1329, '上传文件"/Files/Product/BigPic/456.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 17:20:40'),
(1330, '上传文件"/Files/Product/BigPic/tpsy_456.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 17:20:46'),
(1331, '修改编号为1的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-16 17:23:52'),
(1332, '上传文件"/Files/Product/BigPic/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 17:24:52'),
(1333, '上传文件"/Files/Product/BigPic/tpsy_123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 17:24:52'),
(1334, '修改编号为1的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-16 17:25:32'),
(1335, '上传文件"/Files/Product/BigPic/456.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 17:25:47'),
(1336, '上传文件"/Files/Product/BigPic/tpsy_456.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 17:25:48'),
(1337, '修改编号为1的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-16 17:25:59'),
(1338, '上传文件"/Files/Product/BigPic/123124435.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 17:26:20'),
(1339, '上传文件"/Files/Product/BigPic/tpsy_123124435.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 17:26:20'),
(1340, '修改编号为1的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-16 17:27:14'),
(1341, '上传文件"/Files/Product/BigPic/asdasd1.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 17:27:29'),
(1342, '上传文件"/Files/Product/BigPic/tpsy_asdasd1.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 17:27:29'),
(1343, '修改编号为1的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-16 17:27:51'),
(1344, '上传文件"/Files/Product/BigPic/Blue hills.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 17:28:13'),
(1345, '上传文件"/Files/Product/BigPic/tpsy_Blue hills.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 17:28:13'),
(1346, '删除文件"/Files/Product/BigPic/123.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-16 17:29:00'),
(1347, '删除文件"/Files/Product/BigPic/123124435.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-16 17:29:00'),
(1348, '删除文件"/Files/Product/BigPic/456.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-16 17:29:00'),
(1349, '删除文件"/Files/Product/BigPic/asdasd1.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-16 17:29:00'),
(1350, '删除文件"/Files/Product/BigPic/Blue hills.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-16 17:29:00'),
(1351, '删除文件"/Files/Product/BigPic/tpsy_123.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-16 17:29:00'),
(1352, '删除文件"/Files/Product/BigPic/tpsy_123124435.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-16 17:29:00'),
(1353, '删除文件"/Files/Product/BigPic/tpsy_456.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-16 17:29:00'),
(1354, '删除文件"/Files/Product/BigPic/tpsy_asdasd1.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-16 17:29:00'),
(1355, '删除文件"/Files/Product/BigPic/tpsy_Blue hills.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-16 17:29:00'),
(1356, '上传文件"/Files/Product/BigPic/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 17:30:03'),
(1357, '上传文件"/Files/Product/BigPic/tpsy_123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-16 17:31:30'),
(1358, '删除文件"/Files/Product/BigPic/123.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-16 17:31:59'),
(1359, '删除文件"/Files/Product/BigPic/tpsy_123.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-16 17:31:59'),
(1360, '修改编号为1的Set!', '/admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-16 18:02:04'),
(1361, '修改编号为1的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-16 17:59:29'),
(1362, '修改编号为1的Set!', '/admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-16 18:02:47'),
(1363, '修改编号为1的Set!', '/admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-16 18:21:41'),
(1364, '修改编号为1的的网站配置!', '/admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-10-16 18:46:21'),
(1365, '修改编号为1的的网站配置!', '/admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-10-16 18:46:35'),
(1366, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-17 09:22:49'),
(1367, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-17 09:30:59'),
(1368, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-17 09:37:03'),
(1369, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-17 09:47:02'),
(1370, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-17 09:50:41'),
(1371, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-17 09:52:46'),
(1372, '修改编号为1的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-17 10:06:32'),
(1373, '修改编号为1的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-17 10:06:38'),
(1374, '修改编号为1的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-17 10:06:44'),
(1375, '修改编号为1的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-17 10:09:40'),
(1376, '修改编号为1的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-17 10:20:22'),
(1377, '上传文件"/Files/WaterPic/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-17 10:20:46'),
(1378, '上传文件"/Files/WaterPic/tpsy_123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-17 10:20:46'),
(1379, '修改编号为1的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-17 10:21:08'),
(1380, '删除文件"/Files/WaterPic/123.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-17 10:21:23'),
(1381, '删除文件"/Files/WaterPic/tpsy_123.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-17 10:21:23'),
(1382, '修改编号为1的Set!', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-17 10:21:47'),
(1383, '上传文件"/Files/Article/SmallPic/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-17 10:23:34'),
(1384, '上传文件"/Files/Article/SmallPic/tpsy_123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-17 10:23:34'),
(1385, '添加名称为"asd"的文章。', '/Admin/Article/Article_Add.aspx', '127.0.0.1', 1, '2012-10-17 10:23:40'),
(1386, '删除文件"/Files/Article/SmallPic/123.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-17 10:23:53'),
(1387, '删除文件"/Files/Article/SmallPic/tpsy_123.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-17 10:23:53'),
(1388, '修改编号为1的文章。', '/Admin/Article/Article_Add.aspx', '127.0.0.1', 1, '2012-10-17 10:23:57'),
(1389, '修改编号为1的文章。', '/Admin/Article/Article_Add.aspx', '127.0.0.1', 1, '2012-10-17 10:24:05'),
(1390, '删除编号为1的文章!', '/Admin/Article/Article.aspx', '127.0.0.1', 1, '2012-10-17 10:26:17'),
(1391, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-17 10:41:43'),
(1392, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-17 10:45:13'),
(1393, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-17 10:49:13'),
(1394, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-17 10:59:05'),
(1395, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-17 11:02:03'),
(1396, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-17 11:09:38'),
(1397, '添加编号为""的水印配置。', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-17 11:10:11'),
(1398, '修改编号为1的水印配置。', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-17 11:10:35'),
(1399, '修改编号为1的水印配置。', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-17 11:11:19'),
(1400, '添加编号为""的水印配置。', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-17 11:11:56'),
(1401, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-17 11:29:53'),
(1402, '修改编号为1的水印配置。', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-17 11:30:15'),
(1403, '上传文件"/Files/Product/BigPic/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-17 11:31:05'),
(1404, '添加名称为"asd"的产品。', '/Admin/Product/Product_Add.aspx', '127.0.0.1', 1, '2012-10-17 11:31:43'),
(1405, '修改编号为1的水印配置。', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-17 11:31:55'),
(1406, '修改编号为1的产品。', '/Admin/Product/Product_Add.aspx', '127.0.0.1', 1, '2012-10-17 11:32:32'),
(1407, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-17 11:34:00'),
(1408, '添加编号为""的水印配置。', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-17 11:34:37'),
(1409, '上传文件"/Files/Product/BigPic/456.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-17 11:35:20'),
(1410, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-17 11:48:34'),
(1411, '添加编号为""的水印配置。', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-17 11:48:43'),
(1412, '添加编号为""的水印配置。', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-17 11:49:17'),
(1413, '修改编号为1的水印配置。', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-17 11:49:20'),
(1414, '添加编号为""的水印配置。', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-17 11:51:42'),
(1415, '修改编号为1的水印配置。', '/Admin/Set/Set_Add.aspx', '127.0.0.1', 1, '2012-10-17 11:52:33'),
(1416, '上传文件"/Files/Article/BigPic/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-17 11:52:57'),
(1417, '添加名称为"asd"的文章。', '/Admin/Article/Article_Add.aspx', '127.0.0.1', 1, '2012-10-17 11:53:14'),
(1418, '上传文件"/Files/Photo/BigPic/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-17 11:53:41'),
(1419, '添加名称为asd的相册!', '/Admin/Photo/Photo_Add.aspx', '127.0.0.1', 1, '2012-10-17 11:53:57'),
(1420, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-17 13:46:10'),
(1421, '添加名称为"水印配置"的权限字段。', '/Admin/System/Limit_Add.aspx', '127.0.0.1', 1, '2012-10-17 13:46:56'),
(1422, '添加名称为"片段内容管理"的权限字段。', '/Admin/System/Limit_Add.aspx', '127.0.0.1', 1, '2012-10-17 13:49:26'),
(1423, '添加名称为"添加"的权限字段。', '/Admin/System/Limit_Add.aspx', '127.0.0.1', 1, '2012-10-17 13:50:22'),
(1424, '添加名称为"修改"的权限字段。', '/Admin/System/Limit_Add.aspx', '127.0.0.1', 1, '2012-10-17 13:50:32'),
(1425, '添加名称为"删除"的权限字段。', '/Admin/System/Limit_Add.aspx', '127.0.0.1', 1, '2012-10-17 13:50:41'),
(1426, '添加名称为"批量开放"的权限字段。', '/Admin/System/Limit_Add.aspx', '127.0.0.1', 1, '2012-10-17 13:51:01'),
(1427, '添加名称为"批量关闭"的权限字段。', '/Admin/System/Limit_Add.aspx', '127.0.0.1', 1, '2012-10-17 13:51:11'),
(1428, '修改编号为445的权限字段。', '/Admin/System/Limit_Add.aspx', '127.0.0.1', 1, '2012-10-17 13:52:48'),
(1429, '登录系统！', '/admin/login.aspx', '127.0.0.1', 1, '2012-10-17 13:55:34'),
(1430, '添加名称为片段的栏目属性!', '/Admin/System/ClassProperty_Add.aspx', '127.0.0.1', 1, '2012-10-17 13:54:17'),
(1431, '修改编号为14的栏目属性!', '/Admin/System/ClassProperty_Add.aspx', '127.0.0.1', 1, '2012-10-17 13:54:27'),
(1432, '删除编号为1的相册!', '/Admin/Photo/Photo.aspx', '127.0.0.1', 1, '2012-10-17 14:12:55'),
(1433, '添加名称为默认模版的栏目模板!', '/Admin/System/ClassTemplate_Add.aspx', '127.0.0.1', 1, '2012-10-17 14:18:47'),
(1434, '添加名称为"片段内容"的栏目。', '/Admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-10-17 14:19:06'),
(1435, '添加名称为asd的片段内容!', '/Admin/Block/Block_Add.aspx', '127.0.0.1', 1, '2012-10-17 14:24:51'),
(1436, '修改编号为2的片段内容!', '/Admin/Block/Block_Add.aspx', '127.0.0.1', 1, '2012-10-17 14:26:26'),
(1437, '删除编号为2的片段内容!', '/Admin/Block/Block.aspx', '127.0.0.1', 1, '2012-10-17 14:29:40'),
(1438, '添加编号为""的水印配置。', '/Admin/System/Set_Add.aspx', '127.0.0.1', 1, '2012-10-17 14:35:29'),
(1439, '修改编号为1的水印配置。', '/Admin/System/Set_Add.aspx', '127.0.0.1', 1, '2012-10-17 14:35:32'),
(1440, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-17 14:42:27'),
(1441, '添加名称为asdasd的片段内容!', '/Admin/Block/Block_Add.aspx', '127.0.0.1', 1, '2012-10-17 14:42:11'),
(1442, '修改编号为3的片段内容!', '/Admin/Block/Block_Add.aspx', '127.0.0.1', 1, '2012-10-17 14:42:24'),
(1443, '删除编号为3的片段内容!', '/Admin/Block/Block.aspx', '127.0.0.1', 1, '2012-10-17 14:42:28'),
(1444, '登录系统！', '/Admin/login.aspx', '127.0.0.1', 1, '2012-10-17 15:29:08'),
(1445, '添加编号为""的水印配置。', '/Admin/System/Set_Add.aspx', '127.0.0.1', 1, '2012-10-17 15:31:25'),
(1446, '上传文件"/Files/Video/VideoPic/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-17 15:32:00'),
(1447, '上传文件"/Files/Video/VideoPath/263yunintro.flv"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-17 15:35:45'),
(1448, '添加名称为轩辕剑第一集的视频!', '/Admin/Video/Video_Add.aspx', '127.0.0.1', 1, '2012-10-17 15:35:52'),
(1449, '删除编号为1的视频!', '/Admin/Video/Video.aspx', '127.0.0.1', 1, '2012-10-17 15:37:10'),
(1450, '上传文件"/Files/Video/VideoPath/263yunintro.flv"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-17 15:37:53'),
(1451, '添加名称为轩辕剑的视频!', '/Admin/Video/Video_Add.aspx', '127.0.0.1', 1, '2012-10-17 15:37:58'),
(1452, '修改编号为5的栏目。', '/Admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-10-17 15:45:54'),
(1453, '修改编号为15的栏目。', '/Admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-10-17 15:46:29'),
(1454, '删除编号为2的视频!', '/Admin/Video/Video.aspx', '127.0.0.1', 1, '2012-10-17 15:53:12'),
(1455, '上传文件"/Files/Video/VideoPath/263yunintro_9829.flv"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-17 15:54:02'),
(1456, '添加名称为轩辕剑的视频!', '/Admin/Video/Video_Add.aspx', '127.0.0.1', 1, '2012-10-17 15:54:09'),
(1457, '修改编号为3的视频!', '/Admin/Video/Video_Add.aspx', '127.0.0.1', 1, '2012-10-17 15:54:19'),
(1458, '修改编号为14的栏目。', '/Admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-10-17 15:55:15'),
(1459, '添加编号为""的水印配置。', '/Admin/System/Set_Add.aspx', '127.0.0.1', 1, '2012-10-17 16:01:21'),
(1460, '上传文件"/Files/Video/VideoPic/456.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-17 16:01:48'),
(1461, '添加名称为轩辕剑第二集的视频!', '/Admin/Video/Video_Add.aspx', '127.0.0.1', 1, '2012-10-17 16:02:10'),
(1462, '上传文件"/Files/Video/VideoPic/123124435.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-17 16:02:31'),
(1463, '添加名称为轩辕剑第三集的视频!', '/Admin/Video/Video_Add.aspx', '127.0.0.1', 1, '2012-10-17 16:02:42'),
(1464, '上传文件"/Files/Product/BigPic/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-17 16:11:20'),
(1465, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-17 16:13:15'),
(1466, '上传文件"/Files/Product/BigPic/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-17 16:13:56'),
(1467, '上传文件"/Files/Product/BigPic/123124435.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-17 16:14:07'),
(1468, '添加编号为""的水印配置。', '/Admin/System/Set_Add.aspx', '127.0.0.1', 1, '2012-10-17 16:14:32'),
(1469, '上传文件"/Files/Product/BigPic/asdasd1.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-17 16:15:00'),
(1470, '上传文件"/Files/Product/BigPic/chen_hui_lin.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-17 16:16:04'),
(1471, '上传文件"/Files/Product/BigPic/lin_zhi_ling.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-17 16:16:34'),
(1472, '删除编号为14的栏目属性!', '/Admin/System/ClassProperty.aspx', '127.0.0.1', 1, '2012-10-17 16:30:02'),
(1473, '删除编号为16的栏目!', '/Admin/System/Class.aspx', '127.0.0.1', 1, '2012-10-17 16:30:08'),
(1474, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-17 16:52:57'),
(1475, '添加编号为""的水印配置。', '/Admin/System/Set_Add.aspx', '127.0.0.1', 1, '2012-10-17 16:53:45'),
(1476, '上传文件"/Files/Photo/BigPic/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-17 16:54:15'),
(1477, '添加名称为娃哈哈的相册!', '/Admin/Photo/Photo_Add.aspx', '127.0.0.1', 1, '2012-10-17 16:54:23'),
(1478, '上传文件"/Files/Photo/BigPic/456.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-17 16:54:46'),
(1479, '添加名称为没得救的相册!', '/Admin/Photo/Photo_Add.aspx', '127.0.0.1', 1, '2012-10-17 16:55:29'),
(1480, '上传文件"/Files/Photo/BigPic/123124435.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-17 16:55:43'),
(1481, '添加名称为赚钱了的相册!', '/Admin/Photo/Photo_Add.aspx', '127.0.0.1', 1, '2012-10-17 16:55:50'),
(1482, '修改系统配置。', '/admin/System/Set_Add.aspx', '127.0.0.1', 1, '2012-10-17 17:14:20'),
(1483, '修改编号为460的权限字段。', '/admin/System/Limit_Add.aspx', '127.0.0.1', 1, '2012-10-17 17:21:12'),
(1484, '添加名称为"管理所有信息"的权限字段。', '/Admin/System/Limit_Add.aspx', '127.0.0.1', 1, '2012-10-17 17:30:22'),
(1485, '添加名称为"管理所有信息"的权限字段。', '/Admin/System/Limit_Add.aspx', '127.0.0.1', 1, '2012-10-17 17:30:53'),
(1486, '添加名称为"管理所有信息"的权限字段。', '/Admin/System/Limit_Add.aspx', '127.0.0.1', 1, '2012-10-17 17:31:15'),
(1487, '移动编号为14,15的栏目!', '/admin/System/Class_Move.aspx', '127.0.0.1', 1, '2012-10-17 17:36:45'),
(1488, '修改编号为14的栏目。', '/admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-10-17 17:37:17'),
(1489, '修改编号为15的栏目。', '/admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-10-17 17:37:44'),
(1490, '修改编号为14的栏目。', '/admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-10-17 17:37:50'),
(1491, '修改编号为391的权限字段。', '/admin/System/Limit_Add.aspx', '127.0.0.1', 1, '2012-10-17 17:38:53'),
(1492, '修改编号为461的权限字段。', '/admin/System/Limit_Add.aspx', '127.0.0.1', 1, '2012-10-17 17:39:16'),
(1493, '添加名称为sdfd的片段内容!', '/admin/Block/Block_Add.aspx', '127.0.0.1', 1, '2012-10-17 17:40:15'),
(1494, '修改编号为446的权限字段。', '/admin/System/Limit_Add.aspx', '127.0.0.1', 1, '2012-10-17 17:43:48'),
(1495, '修改编号为452的权限字段。', '/admin/System/Limit_Add.aspx', '127.0.0.1', 1, '2012-10-17 17:43:55'),
(1496, '修改编号为461的权限字段。', '/admin/System/Limit_Add.aspx', '127.0.0.1', 1, '2012-10-17 17:44:01'),
(1497, '修改编号为444的权限字段。', '/admin/System/Limit_Add.aspx', '127.0.0.1', 1, '2012-10-17 17:44:11'),
(1498, '修改编号为445的权限字段。', '/admin/System/Limit_Add.aspx', '127.0.0.1', 1, '2012-10-17 17:44:16'),
(1499, '修改编号为460的权限字段。', '/admin/System/Limit_Add.aspx', '127.0.0.1', 1, '2012-10-17 17:44:45'),
(1500, '修改编号为11的栏目属性!', '/admin/System/ClassProperty_Add.aspx', '127.0.0.1', 1, '2012-10-17 17:45:08'),
(1501, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-17 17:43:24'),
(1502, '上传文件"/Files/Photo/BigPic/联城优购.jpg"。', '/admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-17 17:48:21'),
(1503, '添加名称为11的相册!', '/admin/Photo/Photo_Add.aspx', '127.0.0.1', 1, '2012-10-17 17:48:30'),
(1504, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-17 18:46:03'),
(1505, '修改编号为4的相册!', '/Admin/Photo/Photo_Add.aspx', '127.0.0.1', 1, '2012-10-17 18:46:20'),
(1506, '修改编号为4的相册!', '/Admin/Photo/Photo_Add.aspx', '127.0.0.1', 1, '2012-10-17 18:46:25'),
(1507, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-18 09:42:58'),
(1508, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-18 09:48:38'),
(1509, '登录系统！', '/admin/login.aspx', '127.0.0.1', 1, '2012-10-18 10:19:11'),
(1510, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-18 10:50:02'),
(1511, '上传文件"/Files/Video/video.jpg"。', '/admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 11:36:47'),
(1512, '上传文件"/Files/Video/huixin.flv"。', '/admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 11:37:08'),
(1513, '添加名称为视频测试的视频!', '/admin/Video/Video_Add.aspx', '127.0.0.1', 1, '2012-10-18 11:37:11'),
(1514, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-18 11:45:25'),
(1515, '修改系统配置。', '/admin/System/Set_Add.aspx', '127.0.0.1', 1, '2012-10-18 12:00:21'),
(1516, '上传文件"/Files/Photo/联城优购.jpg"。', '/admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 12:02:33'),
(1517, '添加名称为相册测试的相册!', '/admin/Photo/Photo_Add.aspx', '127.0.0.1', 1, '2012-10-18 12:02:35'),
(1518, '删除文件"/Files/Photo/联城优购.jpg"。', '/admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 12:04:31'),
(1519, '删除文件"/Files/Photo/thumb/联城优购.jpg"。', '/admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 12:04:34'),
(1520, '修改编号为1的相册!', '/admin/Photo/Photo_Add.aspx', '127.0.0.1', 1, '2012-10-18 12:05:04'),
(1521, '修改编号为1的相册!', '/admin/Photo/Photo_Add.aspx', '127.0.0.1', 1, '2012-10-18 12:05:06'),
(1522, '添加名称为相册测试的相册!', '/admin/Photo/Photo_Add.aspx', '127.0.0.1', 1, '2012-10-18 12:05:41'),
(1523, '修改编号为1的相册!', '/admin/Photo/Photo_Add.aspx', '127.0.0.1', 1, '2012-10-18 12:06:15'),
(1524, '修改编号为1的片段内容!', '/admin/Block/Block_Add.aspx', '127.0.0.1', 1, '2012-10-18 13:57:21'),
(1525, '修改编号为1的片段内容!', '/admin/Block/Block_Add.aspx', '127.0.0.1', 1, '2012-10-18 13:59:58'),
(1526, '修改编号为1的片段内容!', '/admin/Block/Block_Add.aspx', '127.0.0.1', 1, '2012-10-18 14:08:51'),
(1527, '修改编号为1的相册!', '/admin/Photo/Photo_Add.aspx', '127.0.0.1', 1, '2012-10-18 14:09:58'),
(1528, '上传文件"/Files/Photo/video.jpg"。', '/admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 14:10:24'),
(1529, '修改编号为1的相册!', '/admin/Photo/Photo_Add.aspx', '127.0.0.1', 1, '2012-10-18 14:10:26'),
(1530, '删除文件"/Files/Photo/video.jpg"。', '/admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 14:10:36'),
(1531, '修改编号为1的相册!', '/admin/Photo/Photo_Add.aspx', '127.0.0.1', 1, '2012-10-18 14:10:42'),
(1532, '上传文件"/Files/Photo/video.jpg"。', '/admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 14:12:26'),
(1533, '修改编号为1的相册!', '/admin/Photo/Photo_Add.aspx', '127.0.0.1', 1, '2012-10-18 14:12:30'),
(1534, '修改编号为1的片段内容!', '/admin/Block/Block_Add.aspx', '127.0.0.1', 1, '2012-10-18 14:17:30'),
(1535, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-18 14:19:55'),
(1536, '上传文件"/Files/WaterPic/water.jpg"。', '/admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 14:24:07'),
(1537, '修改系统配置。', '/admin/System/Set_Add.aspx', '127.0.0.1', 1, '2012-10-18 14:24:19'),
(1538, '上传文件"/Files/WaterPic/video.jpg"。', '/admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 14:24:37'),
(1539, '删除文件"/Files/WaterPic/video.jpg"。', '/admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 14:25:08'),
(1540, '修改系统配置。', '/admin/System/Set_Add.aspx', '127.0.0.1', 1, '2012-10-18 14:26:18'),
(1541, '修改编号为1的相册!', '/admin/Photo/Photo_Add.aspx', '127.0.0.1', 1, '2012-10-18 14:26:23'),
(1542, '修改编号为2的相册!', '/admin/Photo/Photo_Add.aspx', '127.0.0.1', 1, '2012-10-18 14:26:27'),
(1543, '上传文件"/Files/Photo/video.jpg"。', '/admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 14:26:57'),
(1544, '修改编号为1的相册!', '/admin/Photo/Photo_Add.aspx', '127.0.0.1', 1, '2012-10-18 14:27:01'),
(1545, '上传文件"/Files/Photo/video.jpg"。', '/admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 14:27:33'),
(1546, '上传文件"/Files/Photo/联城优购.jpg"。', '/admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 14:28:16'),
(1547, '上传文件"/Files/WaterPic/q.jpg"。', '/admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 14:29:01'),
(1548, '修改系统配置。', '/admin/System/Set_Add.aspx', '127.0.0.1', 1, '2012-10-18 14:29:03'),
(1549, '上传文件"/Files/Photo/联城优购.jpg"。', '/admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 14:29:32'),
(1550, '删除文件"/Files/Photo/联城优购.jpg"。', '/admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 14:30:48'),
(1551, '上传文件"/Files/Photo/联城优购.jpg"。', '/admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 14:30:54'),
(1552, '上传文件"/Files/Photo/tpsy_联城优购.jpg"。', '/admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 14:30:55'),
(1553, '上传文件"/Files/WaterPic/abc.jpg"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 14:32:55'),
(1554, '修改系统配置。', '/Admin/System/Set_Add.aspx', '127.0.0.1', 1, '2012-10-18 14:32:57'),
(1555, '上传文件"/Files/Photo/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 14:33:24'),
(1556, '上传文件"/Files/Photo/tpsy_123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 14:33:24'),
(1557, '删除文件"/Files/WaterPic/q.jpg"。', '/admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 14:36:46'),
(1558, '上传文件"/Files/Photo/Blue hills.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 14:34:11'),
(1559, '上传文件"/Files/Photo/tpsy_Blue hills.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 14:34:12'),
(1560, '删除文件"/Files/Photo/tpsy_联城优购.jpg"。', '/admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 14:37:04'),
(1561, '删除文件"/Files/Photo/联城优购.jpg"。', '/admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 14:37:04'),
(1562, '修改系统配置。', '/admin/System/Set_Add.aspx', '127.0.0.1', 1, '2012-10-18 14:37:54'),
(1563, '删除文件"/Files/Photo/123.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 14:35:19'),
(1564, '删除文件"/Files/Photo/Blue hills.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 14:35:19'),
(1565, '删除文件"/Files/Photo/tpsy_123.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 14:35:19'),
(1566, '删除文件"/Files/Photo/tpsy_Blue hills.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 14:35:19'),
(1567, '修改系统配置。', '/admin/System/Set_Add.aspx', '127.0.0.1', 1, '2012-10-18 14:38:21'),
(1568, '修改系统配置。', '/admin/System/Set_Add.aspx', '127.0.0.1', 1, '2012-10-18 14:39:03'),
(1569, '上传文件"/Files/Photo/video.jpg"。', '/admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 14:39:19'),
(1570, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-18 14:38:12'),
(1571, '上传文件"/Files/Photo/Blue hills.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 14:38:52'),
(1572, '上传文件"/Files/Photo/Blue hills.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 14:42:00'),
(1573, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-18 14:42:45'),
(1574, '修改系统配置。', '/Admin/System/Set_Add.aspx', '127.0.0.1', 1, '2012-10-18 14:53:03'),
(1575, '上传文件"/Files/Photo/Blue hills.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 14:53:27'),
(1576, '删除文件"/Files/Photo/Blue hills.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 14:54:22'),
(1577, '上传文件"/Files/Photo/Blue hills.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 14:54:40'),
(1578, '上传文件"/Files/Photo/Blue hills.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 14:56:40'),
(1579, '上传文件"/Files/Photo/tpsy_Blue hills.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 14:56:40'),
(1580, '删除文件"/Files/Photo/Blue hills.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 14:56:56'),
(1581, '删除文件"/Files/Photo/tpsy_Blue hills.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 14:56:56'),
(1582, '上传文件"/Files/Photo/Blue hills.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 14:57:24'),
(1583, '上传文件"/Files/Photo/tpsy_Blue hills.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 14:57:56'),
(1584, '删除文件"/Files/Photo/Blue hills.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 14:58:03'),
(1585, '删除文件"/Files/Photo/tpsy_Blue hills.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 14:58:03'),
(1586, '上传文件"/Files/WaterPic/abc_5261.jpg"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 14:58:30'),
(1587, '修改系统配置。', '/Admin/System/Set_Add.aspx', '127.0.0.1', 1, '2012-10-18 14:58:32'),
(1588, '上传文件"/Files/Photo/Blue hills.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 14:58:48'),
(1589, '上传文件"/Files/Photo/tpsy_Blue hills.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 14:58:48'),
(1590, '修改系统配置。', '/Admin/System/Set_Add.aspx', '127.0.0.1', 1, '2012-10-18 15:04:02'),
(1591, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-18 15:06:38'),
(1592, '修改编号为6的的网站配置!', '/admin/System/Config_Add.aspx', '127.0.0.1', 1, '2012-10-18 15:25:24'),
(1593, '修改编号为1的栏目。', '/admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-10-18 15:25:57'),
(1594, '修改编号为1的栏目。', '/admin/System/Class_Add.aspx', '127.0.0.1', 1, '2012-10-18 15:28:06'),
(1595, '添加名称为"ss"的文章。', '/admin/Article/Article_Add.aspx', '127.0.0.1', 1, '2012-10-18 15:28:18'),
(1596, '添加名称为"ss"的产品。', '/admin/Product/Product_Add.aspx', '127.0.0.1', 1, '2012-10-18 15:28:28'),
(1597, '修改编号为1的相册!', '/Admin/Photo/Photo_Add.aspx', '127.0.0.1', 1, '2012-10-18 15:34:03'),
(1598, '上传文件"/Files/WaterPic/a.png"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 15:53:27'),
(1599, '修改系统配置。', '/Admin/System/Set_Add.aspx', '127.0.0.1', 1, '2012-10-18 15:53:32'),
(1600, '修改系统配置。', '/Admin/System/Set_Add.aspx', '127.0.0.1', 1, '2012-10-18 15:53:44'),
(1601, '上传文件"/Files/Photo/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 15:54:06'),
(1602, '上传图片水印"/Files/Photo/tpsy_123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 15:54:06'),
(1603, '上传文件"/Files/Photo/456.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 15:55:52'),
(1604, '上传图片水印"/Files/Photo/tpsy_456.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 15:55:59'),
(1605, '删除文件"/Files/Photo/123.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 15:56:17'),
(1606, '删除文件"/Files/Photo/456.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 15:56:17');
INSERT INTO `t_adminlog` (`AdminLogID`, `LogContent`, `ScriptFile`, `IPAddress`, `AdminID`, `AddTime`) VALUES
(1607, '删除文件"/Files/Photo/tpsy_123.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 15:56:17'),
(1608, '删除文件"/Files/Photo/tpsy_456.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 15:56:17'),
(1609, '上传文件"/Files/Photo/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 15:56:35'),
(1610, '上传图片水印"/Files/Photo/tpsy_123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 15:56:35'),
(1611, '删除文件"/Files/Photo/123.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 15:57:04'),
(1612, '删除文件"/Files/Photo/tpsy_123.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 15:57:04'),
(1613, '上传文件"/Files/Photo/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 15:58:14'),
(1614, '上传图片水印"/Files/Photo/tpsy_123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 15:58:15'),
(1615, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-18 16:11:06'),
(1616, '上传文件"/Files/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 16:11:51'),
(1617, '上传图片水印"/Files/Photo/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 16:11:51'),
(1618, '修改系统配置。', '/Admin/System/Set_Add.aspx', '127.0.0.1', 1, '2012-10-18 16:12:11'),
(1619, '上传文件"/Files/456.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 16:13:20'),
(1620, '上传文字水印"/Files/Photo/456.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 16:13:20'),
(1621, '删除文件"/Files/Photo/123.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 16:15:10'),
(1622, '删除文件"/Files/Photo/456.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 16:15:10'),
(1623, '修改编号为1的相册!', '/admin/Photo/Photo_Add.aspx', '127.0.0.1', 1, '2012-10-18 16:23:39'),
(1624, '修改编号为1的相册!', '/admin/Photo/Photo_Add.aspx', '127.0.0.1', 1, '2012-10-18 16:23:45'),
(1625, '修改编号为1的相册!', '/admin/Photo/Photo_Add.aspx', '127.0.0.1', 1, '2012-10-18 16:23:55'),
(1626, '修改系统配置。', '/Admin/System/Set_Add.aspx', '127.0.0.1', 1, '2012-10-18 16:21:14'),
(1627, '上传原始图片"/Files/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 16:31:44'),
(1628, '上传原始图片"/Files/Photo/456.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 16:34:47'),
(1629, '修改系统配置。', '/Admin/System/Set_Add.aspx', '127.0.0.1', 1, '2012-10-18 16:35:09'),
(1630, '上传文字水印"/Files/Photo/123124435.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 16:35:23'),
(1631, '修改系统配置。', '/Admin/System/Set_Add.aspx', '127.0.0.1', 1, '2012-10-18 16:35:39'),
(1632, '上传图片水印"/Files/Photo/asdasd1.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 16:35:53'),
(1633, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-18 16:51:30'),
(1634, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-18 17:01:23'),
(1635, '修改系统配置。', '/Admin/System/Set_Add.aspx', '127.0.0.1', 1, '2012-10-18 17:07:38'),
(1636, '修改系统配置。', '/admin/System/Set_Add.aspx', '127.0.0.1', 1, '2012-10-18 17:51:17'),
(1637, '上传文件"/Files/Photo/video_1752.jpg"。', '/admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 17:52:22'),
(1638, '上传图片水印"/Files/Photo/video1.jpg"。', '/admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 17:53:12'),
(1639, '删除文件"/Files/Photo/video1.jpg"。', '/admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 17:53:51'),
(1640, '删除文件"/Files/Photo/video_1752.jpg"。', '/admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 17:53:51'),
(1641, '修改系统配置。', '/admin/System/Set_Add.aspx', '127.0.0.1', 1, '2012-10-18 17:54:04'),
(1642, '上传图片水印"/Files/Photo/video1.jpg"。', '/admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 17:54:08'),
(1643, '删除文件"/Files/Photo/video1.jpg"。', '/admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 17:54:21'),
(1644, '修改系统配置。', '/admin/System/Set_Add.aspx', '127.0.0.1', 1, '2012-10-18 17:54:29'),
(1645, '修改系统配置。', '/admin/System/Set_Add.aspx', '127.0.0.1', 1, '2012-10-18 17:54:34'),
(1646, '上传文字水印"/Files/Photo/video1.jpg"。', '/admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 17:54:42'),
(1647, '删除文件"/Files/Photo/video1.jpg"。', '/admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 17:55:07'),
(1648, '上传文字水印"/Files/Photo/video1.jpg"。', '/admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 17:55:13'),
(1649, '上传文字水印"/Files/Photo/water.jpg"。', '/admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 17:56:17'),
(1650, '删除文件"/Files/Photo/video1.jpg"。', '/admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 17:56:32'),
(1651, '删除文件"/Files/Photo/water.jpg"。', '/admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 17:56:32'),
(1652, '上传文字水印"/Files/Photo/video1.jpg"。', '/admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 17:56:39'),
(1653, '删除文件"/Files/Photo/video1.jpg"。', '/admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 17:56:50'),
(1654, '上传文字水印"/Files/Photo/联城优购.jpg"。', '/admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 17:56:56'),
(1655, '删除文件"/Files/Photo/联城优购.jpg"。', '/admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 17:57:25'),
(1656, '上传文字水印"/Files/Photo/QQ截图20121018175747.jpg"。', '/admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 17:57:54'),
(1657, '上传文字水印"/Files/Photo/20115710219384.jpg"。', '/admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 17:58:43'),
(1658, '上传文字水印"/Files/Photo/201142020318542.jpg"。', '/admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 17:58:43'),
(1659, '上传文字水印"/Files/Photo/201142511411903.jpg"。', '/admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 17:58:43'),
(1660, '上传文字水印"/Files/Photo/201142983352489.jpg"。', '/admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 17:58:44'),
(1661, '上传文字水印"/Files/Photo/201157101436325.jpg"。', '/admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 17:58:44'),
(1662, '上传文件"/Files/Photo/qq截图20121018175747_7929.jpg"。', '/admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 18:00:33'),
(1663, '登录系统！', '/admin/login.aspx', '192.168.1.225', 1, '2012-10-18 18:35:54'),
(1664, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-18 19:02:39'),
(1665, '上传文字水印"/Files/Photo/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 19:11:39'),
(1666, '上传文字水印"/Files/Photo/123124435.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 19:11:50'),
(1667, '上传文字水印"/Files/Photo/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 19:16:31'),
(1668, '上传文字水印"/Files/Photo/123124435.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 19:16:40'),
(1669, '上传文字水印"/Files/Photo/_7096123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 19:16:40'),
(1670, '上传文字水印"/Files/Photo/456.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 19:16:40'),
(1671, '删除文件"/Files/Photo/123.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 19:17:21'),
(1672, '删除文件"/Files/Photo/123124435.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 19:17:21'),
(1673, '删除文件"/Files/Photo/456.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 19:17:21'),
(1674, '删除文件"/Files/Photo/_7096123.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 19:17:21'),
(1675, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-18 19:46:37'),
(1676, '上传文字水印"/Files/Photo/123124435.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 19:47:08'),
(1677, '上传文字水印"/Files/Photo/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 19:47:08'),
(1678, '上传文字水印"/Files/Photo/_1945123124435.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 19:49:16'),
(1679, '修改系统配置。', '/Admin/System/Set_Add.aspx', '127.0.0.1', 1, '2012-10-18 19:56:35'),
(1680, '上传原始图片"/Files/Photo/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 19:56:54'),
(1681, '上传原始图片"/Files/Photo/123124435.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 19:57:18'),
(1682, '上传原始图片"/Files/Photo/_1041123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 19:57:18'),
(1683, '上传原始图片"/Files/Photo/456.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 19:57:18'),
(1684, '删除文件"/Files/Photo/123.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 19:57:33'),
(1685, '删除文件"/Files/Photo/123124435.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 19:57:33'),
(1686, '删除文件"/Files/Photo/456.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 19:57:33'),
(1687, '删除文件"/Files/Photo/_1041123.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 19:57:33'),
(1688, '修改系统配置。', '/Admin/System/Set_Add.aspx', '127.0.0.1', 1, '2012-10-18 19:58:07'),
(1689, '上传文字水印"/Files/Photo/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 19:58:21'),
(1690, '上传文字水印"/Files/Photo/_6410123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 19:58:39'),
(1691, '上传文字水印"/Files/Photo/456.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 19:58:39'),
(1692, '上传文字水印"/Files/Photo/123124435.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 19:58:39'),
(1693, '删除文件"/Files/Photo/123.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 19:58:52'),
(1694, '删除文件"/Files/Photo/123124435.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 19:58:52'),
(1695, '删除文件"/Files/Photo/456.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 19:58:52'),
(1696, '删除文件"/Files/Photo/_6410123.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 19:58:52'),
(1697, '修改系统配置。', '/Admin/System/Set_Add.aspx', '127.0.0.1', 1, '2012-10-18 19:58:59'),
(1698, '上传图片水印"/Files/Photo/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 19:59:18'),
(1699, '上传图片水印"/Files/Photo/123124435.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 19:59:35'),
(1700, '上传图片水印"/Files/Photo/_7310123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 19:59:35'),
(1701, '上传图片水印"/Files/Photo/456.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 19:59:35'),
(1702, '删除文件"/Files/Photo/123.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 19:59:50'),
(1703, '删除文件"/Files/Photo/123124435.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 19:59:50'),
(1704, '删除文件"/Files/Photo/456.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 19:59:50'),
(1705, '删除文件"/Files/Photo/_7310123.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 19:59:50'),
(1706, '修改系统配置。', '/Admin/System/Set_Add.aspx', '127.0.0.1', 1, '2012-10-18 19:59:58'),
(1707, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-18 20:04:51'),
(1708, '上传图片水印"/Files/Photo/2.jpg"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 20:20:16'),
(1709, '上传图片水印"/Files/Photo/2.jpg"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 20:22:33'),
(1710, '上传图片水印"/Files/Photo/2.jpg"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 20:23:04'),
(1711, '上传图片水印"/Files/Photo/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 20:27:48'),
(1712, '上传图片水印"/Files/Photo/123124435.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 20:28:18'),
(1713, '上传图片水印"/Files/Photo/_6786123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 20:28:18'),
(1714, '上传图片水印"/Files/Photo/456.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 20:28:18'),
(1715, '修改系统配置。', '/Admin/System/Set_Add.aspx', '127.0.0.1', 1, '2012-10-18 20:29:05'),
(1716, '上传文字水印"/Files/Photo/123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 20:29:22'),
(1717, '上传文字水印"/Files/Photo/123124435.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 20:29:31'),
(1718, '上传文字水印"/Files/Photo/_8346123.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 20:29:31'),
(1719, '上传文字水印"/Files/Photo/456.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 20:29:31'),
(1720, '删除文件"/Files/Photo/123.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 20:29:45'),
(1721, '删除文件"/Files/Photo/123124435.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 20:29:45'),
(1722, '删除文件"/Files/Photo/456.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 20:29:45'),
(1723, '删除文件"/Files/Photo/_8346123.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 20:29:45'),
(1724, '修改系统配置。', '/Admin/System/Set_Add.aspx', '127.0.0.1', 1, '2012-10-18 20:29:51'),
(1725, '上传原始图片"/Files/Photo/Blue hills.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 20:30:06'),
(1726, '上传原始图片"/Files/Photo/123124435.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 20:30:16'),
(1727, '上传原始图片"/Files/Photo/asdasd1.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 20:30:16'),
(1728, '上传原始图片"/Files/Photo/_1728Blue hills.jpg"。', '/Admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-18 20:30:16'),
(1729, '删除文件"/Files/Photo/123124435.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 20:30:29'),
(1730, '删除文件"/Files/Photo/asdasd1.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 20:30:29'),
(1731, '删除文件"/Files/Photo/Blue hills.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 20:30:29'),
(1732, '删除文件"/Files/Photo/_1728Blue hills.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 20:30:29'),
(1733, '上传原始图片"/Files/Photo/2.jpg"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 20:30:36'),
(1734, '删除文件"/Files/Photo/2.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 20:30:47'),
(1735, '上传原始图片"/Files/Photo/2.jpg"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 20:30:54'),
(1736, '删除文件"/Files/Photo/2.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 20:31:03'),
(1737, '修改系统配置。', '/Admin/System/Set_Add.aspx', '127.0.0.1', 1, '2012-10-18 20:31:11'),
(1738, '上传文字水印"/Files/Photo/3.jpg"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 20:31:36'),
(1739, '删除文件"/Files/Photo/3.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 20:31:45'),
(1740, '上传文字水印"/Files/Photo/3.jpg"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 20:31:51'),
(1741, '删除文件"/Files/Photo/3.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 20:32:02'),
(1742, '修改系统配置。', '/Admin/System/Set_Add.aspx', '127.0.0.1', 1, '2012-10-18 20:32:08'),
(1743, '上传图片水印"/Files/Photo/wahaha.jpg"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 20:32:20'),
(1744, '删除文件"/Files/Photo/wahaha.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 20:32:28'),
(1745, '上传图片水印"/Files/Photo/wahaha.jpg"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 20:32:34'),
(1746, '删除文件"/Files/Photo/wahaha.jpg"。', '/Admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-18 20:32:41'),
(1747, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-19 08:50:16'),
(1748, '添加系统配置。', '/Admin/System/Set_Add.aspx', '127.0.0.1', 1, '2012-10-19 08:50:58'),
(1749, '上传文字水印"/Files/WaterPic/2.jpg"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-19 08:51:12'),
(1750, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-19 08:54:26'),
(1751, '上传文字水印"/Files/WaterPic/2.jpg"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-19 08:55:14'),
(1752, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-19 08:56:18'),
(1753, '上传原始图片"/Files/WaterPic/2.jpg"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-19 08:56:37'),
(1754, '上传文字水印"/Files/WaterPic/2.jpg"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-19 08:56:37'),
(1755, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-19 09:05:01'),
(1756, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-19 09:05:01'),
(1757, '上传原始图片"/Files/WaterPic/2.jpg"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-19 09:05:18'),
(1758, '上传文字水印"/Files/WaterPic/2.jpg"。', '/Admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-19 09:05:18'),
(1759, '登录系统！', '/Admin/Login.aspx', '127.0.0.1', 1, '2012-10-19 09:43:54'),
(1760, '登录系统！', '/admin/login.aspx', '127.0.0.1', 1, '2012-10-19 14:30:18'),
(1761, '添加系统配置。', '/admin/System/Set_Add.aspx', '127.0.0.1', 1, '2012-10-19 14:36:52'),
(1762, '上传图片水印"/Files/Article/video1.jpg"。', '/admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-19 14:37:07'),
(1763, '上传图片水印"/Files/Article/_1515video1.jpg"。', '/admin/Ajax/Ajax_FileHandle.ashx', '127.0.0.1', 1, '2012-10-19 14:37:48'),
(1764, '上传图片水印"/Files/Article/video1.jpg"。', '/admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-19 14:39:24'),
(1765, '上传图片水印"/Files/Article/video1_4109.jpg"。', '/admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-19 14:39:32'),
(1766, '上传文件"/Files/Article/联城物流规则.txt"。', '/admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-19 15:05:40'),
(1767, '删除文件"/Files/Article/联城物流规则.txt"。', '/admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-19 15:05:45'),
(1768, '上传文件"/Files/Article/gif.gif"。', '/admin/Upload/File_Upload_Dialog.aspx', '127.0.0.1', 1, '2012-10-19 16:02:14'),
(1769, '删除文件"/Files/Article/gif.gif"。', '/admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-19 16:02:23'),
(1770, '删除文件"/Files/Article/video1.jpg"。', '/admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-19 16:02:23'),
(1771, '删除文件"/Files/Article/video1_4109.jpg"。', '/admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-19 16:02:23'),
(1772, '删除文件"/Files/Article/_1515video1.jpg"。', '/admin/Upload/File_Select_Dialog.aspx', '127.0.0.1', 1, '2012-10-19 16:02:23'),
(1773, '删除编号为5的友情链接!', '/admin/Extension/Link.aspx', '127.0.0.1', 1, '2012-10-19 16:33:33'),
(1774, '删除编号为5的调查结果!', '/admin/Survey/SurveyResult.aspx', '127.0.0.1', 1, '2012-10-19 16:34:22'),
(1775, '删除编号为4的调查结果!', '/admin/Survey/SurveyResult.aspx', '127.0.0.1', 1, '2012-10-19 16:34:30'),
(1776, '删除编号为2,3,4,5,6的下载!', '/admin/Download/Download.aspx', '127.0.0.1', 1, '2012-10-19 16:35:05'),
(1777, '删除编号为1的招聘信息!', '/admin/Job/Job.aspx', '127.0.0.1', 1, '2012-10-19 16:35:11'),
(1778, '删除编号为1的产品!', '/admin/Product/Product.aspx', '127.0.0.1', 1, '2012-10-19 16:35:17'),
(1779, '删除编号为1的文章!', '/admin/Article/Article.aspx', '127.0.0.1', 1, '2012-10-19 16:35:22'),
(1780, '删除编号为2的在线调查!', '/admin/Survey/Survey.aspx', '127.0.0.1', 1, '2012-10-19 16:43:24');

-- --------------------------------------------------------

--
-- 表的结构 `t_adposition`
--

CREATE TABLE IF NOT EXISTS `t_adposition` (
  `AdPositionID` int(11) NOT NULL auto_increment,
  `AdPositionName` varchar(200) character set utf8 default NULL,
  `AdPositionIntro` longtext character set utf8,
  `TypeID` int(11) NOT NULL,
  `Width` int(11) NOT NULL,
  `Height` int(11) NOT NULL,
  `Price` double default NULL,
  `ListID` int(11) NOT NULL,
  `AdminID` int(11) NOT NULL,
  `AddTime` datetime default NULL,
  `IsClose` int(11) NOT NULL,
  PRIMARY KEY  (`AdPositionID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=3 ;

--
-- 转存表中的数据 `t_adposition`
--

INSERT INTO `t_adposition` (`AdPositionID`, `AdPositionName`, `AdPositionIntro`, `TypeID`, `Width`, `Height`, `Price`, `ListID`, `AdminID`, `AddTime`, `IsClose`) VALUES
(1, '首页图片切换', '', 2, 700, 300, 0, 1, 1, '2011-04-28 10:24:10', 0),
(2, '首页Banner图片', '', 1, 700, 300, 0, 2, 1, '2012-02-18 22:18:16', 0);

-- --------------------------------------------------------

--
-- 表的结构 `t_area`
--

CREATE TABLE IF NOT EXISTS `t_area` (
  `AreaID` int(11) NOT NULL auto_increment,
  `AreaName` varchar(200) character set utf8 default NULL,
  `ParentID` int(11) NOT NULL,
  `ChildNum` int(11) NOT NULL,
  `ListID` int(11) NOT NULL,
  `AdminID` int(11) NOT NULL,
  `AddTime` datetime default NULL,
  `IsClose` int(11) NOT NULL,
  PRIMARY KEY  (`AreaID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=5 ;

--
-- 转存表中的数据 `t_area`
--

INSERT INTO `t_area` (`AreaID`, `AreaName`, `ParentID`, `ChildNum`, `ListID`, `AdminID`, `AddTime`, `IsClose`) VALUES
(1, '广东省', 0, 2, 1, 1, '2010-11-02 10:25:54', 0),
(2, '深圳市', 1, 0, 2, 1, '2010-11-02 10:26:03', 0),
(3, '湖南省', 0, 0, 2, 1, '2010-11-02 10:44:26', 0),
(4, '广州市', 1, 0, 1, 1, '2010-11-02 10:44:42', 0);

-- --------------------------------------------------------

--
-- 表的结构 `t_article`
--

CREATE TABLE IF NOT EXISTS `t_article` (
  `ArticleID` int(11) NOT NULL auto_increment,
  `ClassID` int(11) NOT NULL,
  `Title` varchar(200) character set utf8 default NULL,
  `Author` varchar(200) character set utf8 default NULL,
  `ComeFrom` varchar(200) character set utf8 default NULL,
  `Picture` varchar(200) character set utf8 default NULL,
  `Video` varchar(200) character set utf8 default NULL,
  `Tags` varchar(200) character set utf8 default NULL,
  `Keywords` varchar(200) character set utf8 default NULL,
  `Description` longtext character set utf8,
  `Details` longtext character set utf8,
  `IsRecommend` int(11) NOT NULL,
  `ClickNum` int(11) NOT NULL,
  `ListID` int(11) NOT NULL,
  `AdminID` int(11) NOT NULL,
  `AddTime` datetime default NULL,
  `IsClose` int(11) NOT NULL,
  PRIMARY KEY  (`ArticleID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

--
-- 转存表中的数据 `t_article`
--


-- --------------------------------------------------------

--
-- 表的结构 `t_articlepic`
--

CREATE TABLE IF NOT EXISTS `t_articlepic` (
  `ArticlePicID` int(11) NOT NULL auto_increment,
  `ArticleID` int(11) NOT NULL,
  `Title` varchar(200) character set utf8 default NULL,
  `SmallPic` varchar(200) character set utf8 default NULL,
  `BigPic` varchar(200) character set utf8 default NULL,
  `Description` longtext character set utf8,
  `ListID` int(11) NOT NULL,
  `AdminID` int(11) NOT NULL,
  `AddTime` datetime default NULL,
  `IsClose` int(11) NOT NULL,
  PRIMARY KEY  (`ArticlePicID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

--
-- 转存表中的数据 `t_articlepic`
--


-- --------------------------------------------------------

--
-- 表的结构 `t_block`
--

CREATE TABLE IF NOT EXISTS `t_block` (
  `BlockID` int(11) NOT NULL auto_increment,
  `Title` varchar(200) character set utf8 default NULL,
  `BlockContent` longtext character set utf8,
  `AdminID` int(11) NOT NULL,
  `AddTime` datetime default NULL,
  `IsClose` int(11) NOT NULL,
  PRIMARY KEY  (`BlockID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

--
-- 转存表中的数据 `t_block`
--

INSERT INTO `t_block` (`BlockID`, `Title`, `BlockContent`, `AdminID`, `AddTime`, `IsClose`) VALUES
(1, 'E8产品介绍', '<p style="text-indent:2em;">E8网站管理系统是基于微软.NET Framework 2.0，采用Microsoft Access/SQL Server/MySQL数据库进行多层架构开发的网站管理系统。其功能设计主要面向中小型企业、各个行业、事业单位以及政府机关等复杂功能站点。系统已建立栏目系统、文章系统、产品系统、招聘系统、下载系统、调查系统、相册系统、视频系统、权限系统、会员系统、留言系统、广告系统、友情链接系统、文件系统、客服系统。使用自定义模型、自定义模板、可视化内容编辑、多语言版网站等功能，您还可以轻松、灵活的建立适合自身需求的任何系统功能，最大化满足每个用户任何时候的不同需求。</p>', 1, '2012-10-17 17:40:15', 0);

-- --------------------------------------------------------

--
-- 表的结构 `t_chat`
--

CREATE TABLE IF NOT EXISTS `t_chat` (
  `ChatID` int(11) NOT NULL auto_increment,
  `ConfigID` int(11) NOT NULL,
  `TypeID` int(11) NOT NULL,
  `NickName` varchar(200) character set utf8 default NULL,
  `Account` varchar(200) character set utf8 default NULL,
  `ChatKey` varchar(200) character set utf8 default NULL,
  `ListID` int(11) NOT NULL,
  `AdminID` int(11) NOT NULL,
  `AddTime` datetime default NULL,
  `IsClose` int(11) NOT NULL,
  PRIMARY KEY  (`ChatID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

--
-- 转存表中的数据 `t_chat`
--

INSERT INTO `t_chat` (`ChatID`, `ConfigID`, `TypeID`, `NickName`, `Account`, `ChatKey`, `ListID`, `AdminID`, `AddTime`, `IsClose`) VALUES
(1, 1, 1, '在线QQ', '84338993', '692fe998a1777eb7b13b7211347c0da59ebb5cfa65d4a68c', 1, 1, '2010-11-03 14:23:12', 0);

-- --------------------------------------------------------

--
-- 表的结构 `t_class`
--

CREATE TABLE IF NOT EXISTS `t_class` (
  `ClassID` int(11) NOT NULL auto_increment,
  `ConfigID` int(11) NOT NULL,
  `ClassName` varchar(200) character set utf8 default NULL,
  `ClassEnName` varchar(200) character set utf8 default NULL,
  `ClassPropertyID` int(11) NOT NULL,
  `ClassTemplateID` int(11) NOT NULL,
  `ClassPic` varchar(200) character set utf8 default NULL,
  `LinkUrl` varchar(200) character set utf8 default NULL,
  `Target` varchar(200) character set utf8 default NULL,
  `IsGoToFirst` int(11) NOT NULL,
  `IsShowNav` int(11) default NULL,
  `Keywords` longtext character set utf8,
  `Description` longtext character set utf8,
  `ClassContent` longtext character set utf8,
  `ClassConfig` longtext character set utf8,
  `ParentID` int(11) NOT NULL,
  `ChildNum` int(11) NOT NULL,
  `ListID` int(11) NOT NULL,
  `AdminID` int(11) NOT NULL,
  `AddTime` datetime default NULL,
  `IsClose` int(11) NOT NULL,
  PRIMARY KEY  (`ClassID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=16 ;

--
-- 转存表中的数据 `t_class`
--

INSERT INTO `t_class` (`ClassID`, `ConfigID`, `ClassName`, `ClassEnName`, `ClassPropertyID`, `ClassTemplateID`, `ClassPic`, `LinkUrl`, `Target`, `IsGoToFirst`, `IsShowNav`, `Keywords`, `Description`, `ClassContent`, `ClassConfig`, `ParentID`, `ChildNum`, `ListID`, `AdminID`, `AddTime`, `IsClose`) VALUES
(1, 1, '关于我们', 'about', 1, 1, '', '', '_self', 1, 1, '', '', '<p style="text-indent: 2em">关于我们</p>', '{"PageSize":12,"TopNum":0,"IsShowSub":false,"IsOnlyRecommend":false,"OrderField":"AddTime","OrderKey":"desc","TitleNum":0,"DescNum":0,"DataLink":"about","StyleClass":""}', 0, 2, 1, 1, '2011-07-22 16:04:33', 0),
(2, 1, '新闻中心', 'news', 2, 2, '', '', '_self', 1, 1, '', '新闻中心', '', '{"PageSize":12,"TopNum":0,"IsShowSub":false,"IsOnlyRecommend":false,"OrderField":"AddTime","OrderKey":"desc","TitleNum":0,"DescNum":0,"DataLink":"article-details-","StyleClass":""}', 0, 0, 2, 1, '2011-07-22 16:04:53', 0),
(3, 1, '产品中心', 'product', 3, 3, '', '', '_self', 1, 1, '', '', '', '{"PageSize":12,"TopNum":0,"IsShowSub":false,"IsOnlyRecommend":false,"OrderField":"AddTime","OrderKey":"desc","TitleNum":0,"DescNum":0,"DataLink":"product-details-","StyleClass":""}', 0, 1, 3, 1, '2011-08-25 17:19:44', 0),
(4, 1, '人才招聘', 'job', 4, 4, '', '', '_self', 1, 1, '', '', '', '{"PageSize":12,"TopNum":0,"IsShowSub":false,"IsOnlyRecommend":false,"OrderField":"AddTime","OrderKey":"desc","TitleNum":0,"DescNum":0,"DataLink":"job-details-","StyleClass":""}', 0, 0, 4, 1, '2011-11-27 01:42:20', 0),
(5, 1, '下载中心', 'download', 5, 5, '', '', '_self', 1, 1, '', '', '', '{"PageSize":12,"TopNum":0,"IsShowSub":false,"IsOnlyRecommend":false,"OrderField":"AddTime","OrderKey":"desc","TitleNum":0,"DescNum":0,"DataLink":"download-details-","StyleClass":""}', 0, 0, 5, 1, '2012-02-19 23:04:16', 0),
(6, 1, '联系我们', 'contact', 1, 1, '', '', '_self', 1, 1, '', '', '', '{"PageSize":12,"TopNum":0,"IsShowSub":false,"IsOnlyRecommend":false,"OrderField":"AddTime","OrderKey":"desc","TitleNum":0,"DescNum":0,"DataLink":"contact","StyleClass":""}', 0, 0, 6, 1, '2012-05-20 15:17:01', 0),
(7, 1, '快速链接', 'quick', 1, 1, '', '', '_self', 1, 0, '', '', '', '{"PageSize":12,"TopNum":0,"IsShowSub":false,"IsOnlyRecommend":false,"OrderField":"AddTime","OrderKey":"desc","TitleNum":0,"DescNum":0,"DataLink":"contact","StyleClass":""}', 0, 5, 7, 1, '2012-05-20 15:17:43', 0),
(8, 1, '在线调查', 'survey', 6, 6, '', '', '_self', 1, 1, '', '', '', '{"PageSize":12,"TopNum":0,"IsShowSub":false,"IsOnlyRecommend":false,"OrderField":"AddTime","OrderKey":"desc","TitleNum":0,"DescNum":0,"DataLink":"survey-details-","StyleClass":""}', 7, 0, 1, 1, '2012-05-20 15:20:42', 0),
(9, 1, '友情链接', 'link', 7, 7, '', '', '_self', 1, 1, '', '', '', '{"PageSize":12,"TopNum":0,"IsShowSub":false,"IsOnlyRecommend":false,"OrderField":"AddTime","OrderKey":"desc","TitleNum":0,"DescNum":0,"DataLink":"link","StyleClass":""}', 7, 0, 2, 1, '2012-05-20 15:21:04', 0),
(10, 1, '网站地图', 'sitemap', 8, 8, '', '', '_self', 1, 1, '', '', '', '{"PageSize":12,"TopNum":0,"IsShowSub":false,"IsOnlyRecommend":false,"OrderField":"AddTime","OrderKey":"desc","TitleNum":0,"DescNum":0,"DataLink":"sitemap","StyleClass":""}', 7, 0, 3, 1, '2012-05-20 15:21:28', 0),
(11, 1, '信息反馈', 'feedback', 9, 9, '', '', '_self', 1, 1, '', '', '', '{"PageSize":12,"TopNum":0,"IsShowSub":false,"IsOnlyRecommend":false,"OrderField":"AddTime","OrderKey":"desc","TitleNum":0,"DescNum":0,"DataLink":"feedback","StyleClass":""}', 7, 0, 4, 1, '2012-05-20 15:21:50', 0),
(12, 1, '在线留言', 'guestbook', 10, 10, '', '', '_self', 1, 1, '', '', '', '{"PageSize":12,"TopNum":0,"IsShowSub":false,"IsOnlyRecommend":false,"OrderField":"AddTime","OrderKey":"desc","TitleNum":0,"DescNum":0,"DataLink":"guestbook","StyleClass":""}', 7, 0, 5, 1, '2012-05-20 15:22:19', 0),
(13, 1, '产品展示', 'chanpinzhanshi', 3, 3, '', '', '_self', 1, 1, '', '', '', '{"PageSize":12,"TopNum":0,"IsShowSub":false,"IsOnlyRecommend":false,"OrderField":"AddTime","OrderKey":"desc","TitleNum":0,"DescNum":0,"DataLink":"product-details-","StyleClass":""}', 3, 0, 1, 1, '2012-09-13 14:25:38', 0),
(14, 1, '视频演示', 'shipinyanshi', 12, 11, '', '', '_self', 1, 1, '', '', '', '{"PageSize":12,"TopNum":0,"IsShowSub":false,"IsOnlyRecommend":false,"OrderField":"AddTime","OrderKey":"desc","TitleNum":0,"DescNum":0,"DataLink":"video-details-","StyleClass":""}', 1, 0, 1, 1, '2012-09-19 16:49:22', 0),
(15, 1, '工厂相册', 'gongchangxiangce', 13, 12, '', '', '_self', 1, 1, '', '', '', '{"PageSize":12,"TopNum":0,"IsShowSub":false,"IsOnlyRecommend":false,"OrderField":"AddTime","OrderKey":"desc","TitleNum":0,"DescNum":0,"DataLink":"xiangcechakan","StyleClass":""}', 1, 0, 2, 1, '2012-09-25 13:45:10', 0);

-- --------------------------------------------------------

--
-- 表的结构 `t_classproperty`
--

CREATE TABLE IF NOT EXISTS `t_classproperty` (
  `ClassPropertyID` int(11) NOT NULL auto_increment,
  `PropertyName` varchar(200) character set utf8 default NULL,
  `ListID` int(11) NOT NULL,
  `AdminID` int(11) NOT NULL,
  `AddTime` datetime default NULL,
  `IsClose` int(11) NOT NULL,
  PRIMARY KEY  (`ClassPropertyID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=14 ;

--
-- 转存表中的数据 `t_classproperty`
--

INSERT INTO `t_classproperty` (`ClassPropertyID`, `PropertyName`, `ListID`, `AdminID`, `AddTime`, `IsClose`) VALUES
(1, '单页面', 1, 1, '2012-04-16 22:20:48', 0),
(2, '文章', 2, 1, '2012-04-16 22:18:13', 0),
(3, '产品', 3, 1, '2012-04-16 22:18:23', 0),
(4, '招聘', 4, 1, '2012-04-16 22:18:33', 0),
(5, '下载', 5, 1, '2012-04-16 22:18:41', 0),
(6, '调查', 6, 1, '2012-04-16 22:18:49', 0),
(7, '友情链接', 7, 1, '2012-04-16 22:19:05', 0),
(8, '网站地图', 8, 1, '2012-04-16 22:19:17', 0),
(9, '信息反馈', 9, 1, '2012-04-16 22:19:25', 0),
(10, '留言本', 10, 1, '2012-04-16 22:19:33', 0),
(11, '外链', 13, 1, '2012-04-16 22:19:41', 0),
(12, '视频', 11, 1, '2012-09-19 16:14:44', 0),
(13, '相册', 12, 1, '2012-09-25 13:42:17', 0);

-- --------------------------------------------------------

--
-- 表的结构 `t_classtemplate`
--

CREATE TABLE IF NOT EXISTS `t_classtemplate` (
  `ClassTemplateID` int(11) NOT NULL auto_increment,
  `ClassPropertyID` int(11) NOT NULL,
  `TemplateName` varchar(200) character set utf8 default NULL,
  `TemplatePath` varchar(200) character set utf8 default NULL,
  `ListID` int(11) NOT NULL,
  `AdminID` int(11) NOT NULL,
  `AddTime` datetime default NULL,
  `IsClose` int(11) NOT NULL,
  PRIMARY KEY  (`ClassTemplateID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=14 ;

--
-- 转存表中的数据 `t_classtemplate`
--

INSERT INTO `t_classtemplate` (`ClassTemplateID`, `ClassPropertyID`, `TemplateName`, `TemplatePath`, `ListID`, `AdminID`, `AddTime`, `IsClose`) VALUES
(1, 1, '默认模板', 'UserControl/WUC_Class_Content.ascx', 1, 1, '2012-04-17 00:19:03', 0),
(2, 2, '默认模板', 'UserControl/WUC_Article_List.ascx', 2, 1, '2012-04-20 18:29:12', 0),
(3, 3, '默认模板', 'UserControl/WUC_Product_List.ascx', 3, 1, '2012-04-20 18:29:31', 0),
(4, 4, '默认模板', 'UserControl/WUC_Job_List.ascx', 4, 1, '2012-04-20 18:29:49', 0),
(5, 5, '默认模板', 'UserControl/WUC_Download_List.ascx', 5, 1, '2012-04-20 18:30:30', 0),
(6, 6, '默认模板', 'UserControl/WUC_Survey_List.ascx', 6, 1, '2012-04-20 18:30:58', 0),
(7, 7, '默认模板', 'UserControl/WUC_Link.ascx', 7, 1, '2012-04-20 18:31:21', 0),
(8, 8, '默认模板', 'UserControl/WUC_Sitemap.ascx', 8, 1, '2012-04-20 18:31:42', 0),
(9, 9, '默认模板', 'UserControl/WUC_Feedback.ascx', 9, 1, '2012-04-20 18:32:08', 0),
(10, 10, '默认模板', 'UserControl/WUC_Guestbook.ascx', 10, 1, '2012-04-20 18:32:35', 0),
(11, 12, '默认模版', 'UserControl/WUC_Video_List.ascx', 11, 1, '2012-09-19 16:48:34', 0),
(12, 13, '默认模版', 'UserControl/WUC_Photo_List.ascx', 12, 1, '2012-09-25 13:44:33', 0),
(13, 14, '默认模版', 'UserControl/WUC_Block_List.ascx', 13, 1, '2012-10-17 14:18:47', 0);

-- --------------------------------------------------------

--
-- 表的结构 `t_collection`
--

CREATE TABLE IF NOT EXISTS `t_collection` (
  `CollectionID` int(11) NOT NULL auto_increment,
  `Title` varchar(200) character set utf8 default NULL,
  `Url` varchar(200) character set utf8 default NULL,
  `UserID` int(11) NOT NULL,
  `AddTime` datetime default NULL,
  PRIMARY KEY  (`CollectionID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

--
-- 转存表中的数据 `t_collection`
--


-- --------------------------------------------------------

--
-- 表的结构 `t_config`
--

CREATE TABLE IF NOT EXISTS `t_config` (
  `ConfigID` int(11) NOT NULL auto_increment,
  `LanguageVer` varchar(200) character set utf8 default NULL,
  `WebsiteName` varchar(200) character set utf8 default NULL,
  `WebsiteUrl` varchar(200) character set utf8 default NULL,
  `WebsiteKeywords` longtext character set utf8,
  `WebsiteDescription` longtext character set utf8,
  `MailReceiveAddress` varchar(200) character set utf8 default NULL,
  `MailSmtpServer` varchar(200) character set utf8 default NULL,
  `MailUserName` varchar(200) character set utf8 default NULL,
  `MailPassword` longtext character set utf8,
  `FooterInfo` longtext character set utf8,
  `ListID` int(11) NOT NULL,
  `AdminID` int(11) NOT NULL,
  `AddTime` datetime default NULL,
  `IsClose` int(11) NOT NULL,
  PRIMARY KEY  (`ConfigID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=7 ;

--
-- 转存表中的数据 `t_config`
--

INSERT INTO `t_config` (`ConfigID`, `LanguageVer`, `WebsiteName`, `WebsiteUrl`, `WebsiteKeywords`, `WebsiteDescription`, `MailReceiveAddress`, `MailSmtpServer`, `MailUserName`, `MailPassword`, `FooterInfo`, `ListID`, `AdminID`, `AddTime`, `IsClose`) VALUES
(1, '简体中文版', 'E8企业网站管理系统 v2.2', '/cn/', 'E8企业网站管理系统 v2.2', 'E8企业网站管理系统 v2.2', '8088@160it.com', 'smtp.corpease.net', '8088@160it.com', 'AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAACOqV5hTLaEayBN94xSN3aAQAAAACAAAAAAADZgAAqAAAABAAAABfaeCvkNcPYTX1ppgI4M3lAAAAAASAAACgAAAAEAAAAKUzoqqCyaIo3pPPPy3JdTIQAAAA8ApR5aa4t9lF5n6q2yw4PhQAAAAze2te9uBEeFwxKsBbddfrzTaGjg==', '<p>网站底部信息</p>', 1, 1, '2011-08-25 17:31:23', 0),
(6, 'English', '英文网站', '/en/', '', '', 'asd', 'asd', 'asdas@qq.com', 'AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAL6QumJ1N8UOlpMXd9mhssQQAAAACAAAAAAADZgAAqAAAABAAAACnaCWaXzAgsp63+nDwb3MQAAAAAASAAACgAAAAEAAAADQ0kXK1vMu4Jz/bgOh7mSUIAAAAQxoMSXacDF8UAAAAlWtC1cC3xPhXX28lktEWLdKPbpk=', '', 2, 1, '2012-09-19 14:07:49', 0);

-- --------------------------------------------------------

--
-- 表的结构 `t_dictionary`
--

CREATE TABLE IF NOT EXISTS `t_dictionary` (
  `DictionaryID` int(11) NOT NULL auto_increment,
  `DictionaryName` varchar(200) character set utf8 default NULL,
  `DictionaryVal` varchar(200) character set utf8 default NULL,
  `ParentID` int(11) NOT NULL,
  `ChildNum` int(11) NOT NULL,
  `ListID` int(11) NOT NULL,
  `AdminID` int(11) NOT NULL,
  `AddTime` datetime default NULL,
  `IsClose` int(11) NOT NULL,
  PRIMARY KEY  (`DictionaryID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=8 ;

--
-- 转存表中的数据 `t_dictionary`
--

INSERT INTO `t_dictionary` (`DictionaryID`, `DictionaryName`, `DictionaryVal`, `ParentID`, `ChildNum`, `ListID`, `AdminID`, `AddTime`, `IsClose`) VALUES
(1, '会员留言', NULL, 0, 2, 1, 1, '2011-02-25 16:05:07', 0),
(2, '产品', NULL, 1, 0, 1, 1, '2011-02-25 16:05:20', 0),
(3, '服务', NULL, 1, 0, 2, 1, '2011-02-25 16:05:26', 0),
(4, '反馈类型', NULL, 0, 3, 2, 1, '2011-02-28 16:17:54', 0),
(5, '信息反馈', NULL, 4, 0, 1, 1, '2011-02-28 16:18:56', 0),
(6, '在线招聘', NULL, 4, 0, 2, 1, '2011-02-28 16:19:05', 0),
(7, '产品订购', NULL, 4, 0, 3, 1, '2011-02-28 16:19:11', 0);

-- --------------------------------------------------------

--
-- 表的结构 `t_download`
--

CREATE TABLE IF NOT EXISTS `t_download` (
  `DownloadID` int(11) NOT NULL auto_increment,
  `ClassID` int(11) NOT NULL,
  `DownName` varchar(200) character set utf8 default NULL,
  `DownPic` varchar(200) character set utf8 default NULL,
  `DownUrl` varchar(200) character set utf8 default NULL,
  `DownSize` double NOT NULL,
  `Tags` varchar(200) character set utf8 default NULL,
  `Keywords` varchar(200) character set utf8 default NULL,
  `Description` longtext character set utf8,
  `Details` longtext character set utf8,
  `ClickNum` int(11) NOT NULL,
  `ListID` int(11) NOT NULL,
  `IsRecommend` int(11) NOT NULL,
  `AdminID` int(11) NOT NULL,
  `AddTime` datetime default NULL,
  `IsClose` int(11) NOT NULL,
  PRIMARY KEY  (`DownloadID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

--
-- 转存表中的数据 `t_download`
--


-- --------------------------------------------------------

--
-- 表的结构 `t_feedback`
--

CREATE TABLE IF NOT EXISTS `t_feedback` (
  `FeedbackID` int(11) NOT NULL auto_increment,
  `DictionaryID` int(11) NOT NULL,
  `Title` varchar(200) character set utf8 default NULL,
  `FeedbackContent` longtext character set utf8,
  `IpAddress` varchar(200) character set utf8 default NULL,
  `AddTime` datetime default NULL,
  `IsDeal` int(11) NOT NULL,
  `DealMeno` longtext character set utf8,
  PRIMARY KEY  (`FeedbackID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

--
-- 转存表中的数据 `t_feedback`
--


-- --------------------------------------------------------

--
-- 表的结构 `t_guestbook`
--

CREATE TABLE IF NOT EXISTS `t_guestbook` (
  `GuestbookID` int(11) NOT NULL auto_increment,
  `NickName` varchar(200) character set utf8 default NULL,
  `BookContent` longtext character set utf8,
  `IpAddress` varchar(200) character set utf8 default NULL,
  `AddTime` datetime default NULL,
  `IsReply` int(11) NOT NULL,
  `ReplyContent` longtext character set utf8,
  `ReplyTime` datetime default NULL,
  `AdminID` int(11) NOT NULL,
  `IsClose` int(11) NOT NULL,
  PRIMARY KEY  (`GuestbookID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=3 ;

--
-- 转存表中的数据 `t_guestbook`
--

INSERT INTO `t_guestbook` (`GuestbookID`, `NickName`, `BookContent`, `IpAddress`, `AddTime`, `IsReply`, `ReplyContent`, `ReplyTime`, `AdminID`, `IsClose`) VALUES
(2, '留言测试', '留言测试', '127.0.0.1', '2012-05-20 21:05:52', 1, '留言回复测试', '2012-05-20 21:06:48', 1, 0);

-- --------------------------------------------------------

--
-- 表的结构 `t_industry`
--

CREATE TABLE IF NOT EXISTS `t_industry` (
  `IndustryID` int(11) NOT NULL auto_increment,
  `IndustryName` varchar(200) character set utf8 default NULL,
  `ParentID` int(11) NOT NULL,
  `ChildNum` int(11) NOT NULL,
  `ListID` int(11) NOT NULL,
  `AdminID` int(11) NOT NULL,
  `AddTime` datetime default NULL,
  `IsClose` int(11) NOT NULL,
  PRIMARY KEY  (`IndustryID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=3 ;

--
-- 转存表中的数据 `t_industry`
--

INSERT INTO `t_industry` (`IndustryID`, `IndustryName`, `ParentID`, `ChildNum`, `ListID`, `AdminID`, `AddTime`, `IsClose`) VALUES
(1, '半导体', 0, 1, 1, 1, '2010-11-02 11:01:21', 0),
(2, '集成电路', 1, 0, 1, 1, '2010-11-02 11:01:29', 0);

-- --------------------------------------------------------

--
-- 表的结构 `t_job`
--

CREATE TABLE IF NOT EXISTS `t_job` (
  `JobID` int(11) NOT NULL auto_increment,
  `ClassID` int(11) NOT NULL,
  `JobName` varchar(200) character set utf8 default NULL,
  `Department` varchar(200) character set utf8 default NULL,
  `JobNum` varchar(200) character set utf8 default NULL,
  `Salary` varchar(200) character set utf8 default NULL,
  `WorkPlace` varchar(200) character set utf8 default NULL,
  `EndTime` datetime default NULL,
  `Tags` varchar(200) character set utf8 default NULL,
  `Keywords` varchar(200) character set utf8 default NULL,
  `Description` longtext character set utf8,
  `Demand` longtext character set utf8,
  `IsRecommend` int(11) NOT NULL,
  `ClickNum` int(11) NOT NULL,
  `ListID` int(11) NOT NULL,
  `AdminID` int(11) NOT NULL,
  `AddTime` datetime default NULL,
  `IsClose` int(11) NOT NULL,
  PRIMARY KEY  (`JobID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

--
-- 转存表中的数据 `t_job`
--


-- --------------------------------------------------------

--
-- 表的结构 `t_limit`
--

CREATE TABLE IF NOT EXISTS `t_limit` (
  `LimitID` int(11) NOT NULL auto_increment,
  `LimitField` varchar(200) character set utf8 default NULL,
  `LimitValue` varchar(200) character set utf8 default NULL,
  `ParentID` int(11) NOT NULL,
  `ChildNum` int(11) NOT NULL,
  `ListID` int(11) NOT NULL,
  `IsDist` int(11) NOT NULL,
  `AdminID` int(11) NOT NULL,
  `AddTime` datetime default NULL,
  `IsClose` int(11) NOT NULL,
  PRIMARY KEY  (`LimitID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=470 ;

--
-- 转存表中的数据 `t_limit`
--

INSERT INTO `t_limit` (`LimitID`, `LimitField`, `LimitValue`, `ParentID`, `ChildNum`, `ListID`, `IsDist`, `AdminID`, `AddTime`, `IsClose`) VALUES
(1, '权限字段', 'Limit', 0, 7, 9, 0, 1, '2010-05-19 13:41:15', 0),
(2, '添加', 'LimitAdd', 1, 0, 1, 0, 1, '2010-05-28 14:51:40', 0),
(3, '修改', 'LimitEdit', 1, 0, 2, 0, 1, '2010-05-28 14:52:10', 0),
(4, '移动', 'LimitMove', 1, 0, 3, 0, 1, '2010-05-28 14:52:46', 0),
(5, '删除', 'LimitDel', 1, 0, 4, 0, 1, '2010-05-28 14:53:04', 0),
(6, '批量开放', 'LimitOpen', 1, 0, 5, 0, 1, '2010-05-28 14:53:18', 0),
(7, '批量关闭', 'LimitClose', 1, 0, 6, 0, 1, '2010-05-28 14:54:16', 0),
(8, '管理组管理', 'AdminGroup', 0, 8, 10, 1, 1, '2010-05-28 15:16:01', 0),
(9, '管理所有信息', 'LimitAll', 1, 0, 7, 0, 1, '2010-05-28 16:03:08', 0),
(10, '管理员管理', 'Admin', 0, 7, 11, 1, 1, '2010-05-28 16:10:13', 0),
(11, '管理员日志', 'AdminLog', 0, 2, 12, 1, 1, '2010-05-28 16:10:38', 0),
(12, '栏目管理', 'Class', 0, 7, 3, 1, 1, '2010-05-28 16:17:08', 0),
(17, '文章管理', 'Article', 0, 7, 15, 1, 1, '2010-05-28 16:19:00', 0),
(19, '广告位管理', 'AdPosition', 0, 6, 45, 1, 1, '2010-05-28 16:19:31', 0),
(20, '广告管理', 'Ad', 0, 6, 46, 1, 1, '2010-05-28 16:19:42', 0),
(21, '会员管理', 'User', 0, 11, 37, 0, 1, '2010-05-28 16:20:08', 0),
(22, '会员日志', 'UserLog', 0, 1, 38, 0, 1, '2010-05-28 16:20:18', 0),
(23, '添加', 'AdminGroupAdd', 8, 0, 1, 0, 1, '2010-05-28 16:23:11', 0),
(24, '修改', 'AdminGroupEdit', 8, 0, 2, 0, 1, '2010-05-28 16:23:30', 0),
(25, '删除', 'AdminGroupDel', 8, 0, 3, 0, 1, '2010-05-28 16:23:40', 0),
(26, '批量开放', 'AdminGroupOpen', 8, 0, 4, 0, 1, '2010-05-28 16:23:59', 0),
(27, '批量关闭', 'AdminGroupClose', 8, 0, 5, 0, 1, '2010-05-28 16:24:12', 0),
(28, '分配管理员', 'AdminGroupSetAdmin', 8, 0, 6, 0, 1, '2010-05-28 16:24:38', 0),
(29, '分配操作权限', 'AdminGroupSetLimit', 8, 0, 7, 0, 1, '2010-05-28 16:27:00', 0),
(30, '管理所有信息', 'AdminGroupAll', 8, 0, 8, 0, 1, '2010-05-28 16:27:44', 0),
(31, '添加', 'AdminAdd', 10, 0, 1, 0, 1, '2010-05-28 16:36:54', 0),
(32, '修改', 'AdminEdit', 10, 0, 2, 0, 1, '2010-05-28 16:37:07', 0),
(33, '删除', 'AdminDel', 10, 0, 3, 0, 1, '2010-05-28 16:37:24', 0),
(34, '批量开放', 'AdminOpen', 10, 0, 4, 0, 1, '2010-05-28 16:37:47', 0),
(35, '批量关闭', 'AdminClose', 10, 0, 5, 0, 1, '2010-05-28 16:37:57', 0),
(36, '分配管理组', 'AdminSetAdminGroup', 10, 0, 6, 0, 1, '2010-05-28 16:38:27', 0),
(37, '管理所有信息', 'AdminAll', 10, 0, 7, 0, 1, '2010-05-28 16:38:52', 0),
(38, '删除', 'AdminLogDel', 11, 0, 1, 0, 1, '2010-05-28 16:41:43', 0),
(39, '添加', 'ClassAdd', 12, 0, 1, 0, 1, '2010-05-28 16:44:03', 0),
(40, '修改', 'ClassEdit', 12, 0, 2, 0, 1, '2010-05-28 16:44:14', 0),
(41, '移动', 'ClassMove', 12, 0, 3, 0, 1, '2010-05-28 16:44:36', 0),
(42, '删除', 'ClassDel', 12, 0, 4, 0, 1, '2010-05-28 16:45:39', 0),
(43, '批量开放', 'ClassOpen', 12, 0, 5, 0, 1, '2010-05-28 16:45:58', 0),
(44, '批量关闭', 'ClassClose', 12, 0, 6, 0, 1, '2010-05-28 16:46:11', 1),
(45, '管理所有信息', 'ClassAll', 12, 0, 7, 0, 1, '2010-05-28 16:46:23', 0),
(74, '添加', 'ArticleAdd', 17, 0, 1, 0, 1, '2010-05-28 17:35:39', 0),
(75, '修改', 'ArticleEdit', 17, 0, 2, 0, 1, '2010-05-28 17:35:49', 0),
(76, '删除', 'ArticleDel', 17, 0, 3, 0, 1, '2010-05-28 17:35:56', 0),
(77, '批量开放', 'ArticleOpen', 17, 0, 5, 0, 1, '2010-05-28 17:36:03', 0),
(78, '批量关闭', 'ArticleClose', 17, 0, 6, 0, 1, '2010-05-28 17:36:19', 0),
(79, '管理所有信息', 'ArticleAll', 17, 0, 7, 0, 1, '2010-05-28 17:36:50', 0),
(86, '添加', 'AdPositionAdd', 19, 0, 1, 0, 1, '2010-05-28 17:54:53', 0),
(87, '修改', 'AdPositionEdit', 19, 0, 2, 0, 1, '2010-05-28 17:55:00', 0),
(88, '删除', 'AdPositionDel', 19, 0, 3, 0, 1, '2010-05-28 17:55:09', 0),
(89, '批量开放', 'AdPositionOpen', 19, 0, 4, 0, 1, '2010-05-28 17:55:23', 0),
(90, '批量关闭', 'AdPositionClose', 19, 0, 5, 0, 1, '2010-05-28 17:55:34', 0),
(91, '管理所有信息', 'AdPositionAll', 19, 0, 6, 0, 1, '2010-05-28 17:55:45', 0),
(92, '添加', 'AdAdd', 20, 0, 1, 0, 1, '2010-05-28 17:57:25', 0),
(93, '修改', 'AdEdit', 20, 0, 2, 0, 1, '2010-05-28 17:57:39', 0),
(94, '删除', 'AdDel', 20, 0, 3, 0, 1, '2010-05-28 17:57:49', 0),
(95, '批量开放', 'AdOpen', 20, 0, 4, 0, 1, '2010-05-28 17:58:13', 0),
(96, '批量关闭', 'AdClose', 20, 0, 5, 0, 1, '2010-05-28 17:58:24', 0),
(97, '管理所有信息', 'AdAll', 20, 0, 6, 0, 1, '2010-05-28 17:58:49', 0),
(98, '添加', 'UserAdd', 21, 0, 1, 0, 1, '2010-05-28 18:03:24', 0),
(99, '修改', 'UserEdit', 21, 0, 2, 0, 1, '2010-05-28 18:03:49', 0),
(100, '删除', 'UserDel', 21, 0, 3, 0, 1, '2010-05-28 18:03:57', 0),
(101, '批量开放', 'UserOpen', 21, 0, 4, 0, 1, '2010-05-28 18:04:08', 0),
(102, '批量关闭', 'UserClose', 21, 0, 5, 0, 1, '2010-05-28 18:04:18', 0),
(103, '管理所有信息', 'UserAll', 21, 0, 12, 0, 1, '2010-05-28 18:04:30', 0),
(104, '删除', 'UserLogDel', 22, 0, 1, 0, 1, '2010-05-28 18:05:22', 0),
(105, '审核', 'UserAudit', 21, 0, 6, 0, 1, '2010-05-29 16:40:49', 0),
(106, '取消审核', 'UserNoAudit', 21, 0, 7, 0, 1, '2010-05-29 16:41:02', 0),
(109, '文件夹管理', 'Folder', 0, 3, 13, 1, 1, '2010-06-12 18:58:43', 0),
(110, '文件管理', 'File', 0, 3, 14, 1, 1, '2010-06-12 18:59:37', 0),
(111, '创建文件夹', 'FolderCreate', 109, 0, 1, 0, 1, '2010-06-12 19:00:20', 0),
(112, '删除文件夹', 'FolderDel', 109, 0, 2, 0, 1, '2010-06-12 19:00:54', 0),
(113, '管理所有文件夹', 'FolderAll', 109, 0, 3, 0, 1, '2010-06-12 19:23:36', 0),
(114, '上传文件', 'FileUpload', 110, 0, 1, 0, 1, '2010-06-12 19:24:03', 0),
(115, '删除文件', 'FileDel', 110, 0, 2, 0, 1, '2010-06-12 19:24:20', 0),
(116, '管理所有文件', 'FileAll', 110, 0, 3, 0, 1, '2010-06-12 19:24:37', 0),
(124, '友情链接', 'Link', 0, 7, 47, 0, 1, '2010-07-29 22:35:30', 0),
(125, '添加', 'LinkAdd', 124, 0, 1, 0, 1, '2010-07-29 22:35:58', 0),
(126, '修改', 'LinkEdit', 124, 0, 2, 0, 1, '2010-07-29 22:36:29', 0),
(127, '删除', 'LinkDel', 124, 0, 3, 0, 1, '2010-07-29 22:36:39', 0),
(128, '管理所有信息', 'LinkAll', 124, 0, 8, 0, 1, '2010-07-29 22:37:05', 0),
(129, '网站配置', 'Config', 0, 4, 2, 1, 1, '2010-07-31 09:14:50', 0),
(130, '添加', 'ConfigAdd', 129, 0, 1, 0, 1, '2010-07-31 09:16:40', 0),
(131, '修改', 'ConfigEdit', 129, 0, 2, 0, 1, '2010-07-31 09:16:59', 0),
(134, '管理所有信息', 'ConfigAll', 129, 0, 6, 0, 1, '2010-07-31 09:22:48', 0),
(181, '会员留言', 'Message', 0, 3, 42, 0, 1, '2010-11-02 14:11:47', 0),
(182, '回复', 'MessageReply', 181, 0, 1, 0, 1, '2010-11-02 14:12:13', 0),
(183, '删除', 'MessageDel', 181, 0, 2, 0, 1, '2010-11-02 14:12:24', 0),
(184, '管理所有信息', 'MessageAll', 181, 0, 3, 0, 1, '2010-11-02 14:13:05', 0),
(283, '数据字典', 'Dictionary', 0, 7, 8, 0, 1, '2010-12-02 14:19:57', 0),
(284, '添加', 'DictionaryAdd', 283, 0, 1, 0, 1, '2010-12-02 14:20:25', 0),
(285, '修改', 'DictionaryEdit', 283, 0, 2, 0, 1, '2010-12-02 14:20:45', 0),
(286, '删除', 'DictionaryDel', 283, 0, 3, 0, 1, '2010-12-02 14:20:58', 0),
(287, '移动', 'DictionaryMove', 283, 0, 4, 0, 1, '2010-12-02 14:21:18', 0),
(288, '批量开放', 'DictionaryOpen', 283, 0, 5, 0, 1, '2010-12-02 14:21:33', 0),
(289, '批量关闭', 'DictionaryClose', 283, 0, 6, 0, 1, '2010-12-02 14:21:51', 0),
(290, '管理所有信息', 'DictionaryAll', 283, 0, 7, 0, 1, '2010-12-02 14:22:11', 0),
(291, '行业管理', 'Industry', 0, 7, 7, 0, 1, '2010-12-02 14:22:56', 0),
(292, '添加', 'IndustryAdd', 291, 0, 1, 0, 1, '2010-12-02 14:23:15', 0),
(293, '修改', 'IndustryEdit', 291, 0, 2, 0, 1, '2010-12-02 14:23:24', 0),
(294, '删除', 'IndustryDel', 291, 0, 3, 0, 1, '2010-12-02 14:23:34', 0),
(295, '移动', 'IndustryMove', 291, 0, 4, 0, 1, '2010-12-02 14:23:50', 0),
(296, '批量开放', 'IndustryOpen', 291, 0, 5, 0, 1, '2010-12-02 14:24:04', 0),
(297, '批量关闭', 'IndustryClose', 291, 0, 6, 0, 1, '2010-12-02 14:24:18', 0),
(298, '管理所有信息', 'IndustryAll', 291, 0, 7, 0, 1, '2010-12-02 14:24:28', 0),
(299, '地区管理', 'Area', 0, 7, 6, 0, 1, '2010-12-02 14:25:34', 0),
(300, '添加', 'AreaAdd', 299, 0, 1, 0, 1, '2010-12-02 14:25:51', 0),
(301, '修改', 'AreaEdit', 299, 0, 2, 0, 1, '2010-12-02 14:26:01', 0),
(302, '删除', 'AreaDel', 299, 0, 3, 0, 1, '2010-12-02 14:26:11', 0),
(303, '移动', 'AreaMove', 299, 0, 4, 0, 1, '2010-12-02 14:26:22', 0),
(304, '批量开放', 'AreaOpen', 299, 0, 5, 0, 1, '2010-12-02 14:26:34', 0),
(305, '批量关闭', 'AreaClose', 299, 0, 6, 0, 1, '2010-12-02 14:26:45', 0),
(306, '管理所有信息', 'AreaAll', 299, 0, 7, 0, 1, '2010-12-02 14:26:58', 0),
(315, '聊天账号', 'Chat', 0, 6, 48, 0, 1, '2010-12-02 14:33:31', 0),
(316, '添加', 'ChatAdd', 315, 0, 1, 0, 1, '2010-12-02 14:33:43', 0),
(317, '修改', 'ChatEdit', 315, 0, 2, 0, 1, '2010-12-02 14:33:52', 0),
(318, '删除', 'ChatDel', 315, 0, 3, 0, 1, '2010-12-02 14:34:02', 0),
(319, '批量开放', 'ChatOpen', 315, 0, 4, 0, 1, '2010-12-02 14:34:36', 0),
(320, '批量关闭', 'ChatClose', 315, 0, 5, 0, 1, '2010-12-02 14:34:49', 0),
(321, '管理所有信息', 'ChatAll', 315, 0, 6, 0, 1, '2010-12-02 14:34:59', 0),
(343, '批量开放', 'LinkOpen', 124, 0, 6, 0, 1, '2010-12-07 03:03:37', 0),
(344, '批量关闭', 'LinkClose', 124, 0, 7, 0, 1, '2010-12-07 03:03:51', 0),
(345, '会员权限', '====', 0, 1, 52, 0, 1, '2010-12-07 18:22:08', 0),
(346, '帐号管理', 'Account', 345, 0, 1, 0, 1, '2010-12-07 18:23:28', 0),
(347, '管理所有信息', 'AdminLogAll', 11, 0, 2, 0, 1, '2010-12-17 01:40:05', 0),
(348, '邮件订阅', 'Mail', 0, 2, 49, 0, 1, '2011-04-27 23:14:37', 0),
(349, '删除', 'MailDel', 348, 0, 1, 0, 1, '2011-04-27 23:15:28', 0),
(350, '招聘管理', 'Job', 0, 7, 19, 0, 1, '2011-04-27 23:21:51', 0),
(351, '下载管理', 'Download', 0, 7, 20, 0, 1, '2011-04-27 23:22:35', 0),
(352, '产品管理', 'Product', 0, 8, 17, 1, 1, '2011-04-27 23:23:15', 0),
(353, '会员级别', 'UserRank', 0, 7, 35, 0, 1, '2011-04-27 23:23:54', 0),
(354, '信息反馈', 'Feedback', 0, 2, 44, 1, 1, '2011-04-27 23:24:19', 0),
(355, '添加', 'JobAdd', 350, 0, 1, 0, 1, '2011-04-27 23:26:09', 0),
(356, '修改', 'JobEdit', 350, 0, 2, 0, 1, '2011-04-27 23:26:22', 0),
(357, '删除', 'JobDel', 350, 0, 3, 0, 1, '2011-04-27 23:26:30', 0),
(358, '批量开放', 'JobOpen', 350, 0, 5, 0, 1, '2011-04-27 23:26:42', 0),
(359, '批量关闭', 'JobClose', 350, 0, 6, 0, 1, '2011-04-27 23:26:53', 0),
(360, '管理所有信息', 'JobAll', 350, 0, 7, 0, 1, '2011-04-27 23:27:03', 0),
(361, '添加', 'DownloadAdd', 351, 0, 1, 0, 1, '2011-04-27 23:27:30', 0),
(362, '修改', 'DownloadEdit', 351, 0, 2, 0, 1, '2011-04-27 23:27:38', 0),
(363, '删除', 'DownloadDel', 351, 0, 3, 0, 1, '2011-04-27 23:27:45', 0),
(364, '批量开放', 'DownloadOpen', 351, 0, 5, 0, 1, '2011-04-27 23:27:56', 0),
(365, '批量关闭', 'DownloadClose', 351, 0, 6, 0, 1, '2011-04-27 23:28:04', 0),
(366, '管理所有信息', 'DownloadAll', 351, 0, 7, 0, 1, '2011-04-27 23:28:17', 0),
(367, '添加', 'ProductAdd', 352, 0, 1, 0, 1, '2011-04-27 23:28:43', 0),
(368, '修改', 'ProductEdit', 352, 0, 2, 0, 1, '2011-04-27 23:28:50', 0),
(369, '删除', 'ProductDel', 352, 0, 3, 0, 1, '2011-04-27 23:28:56', 0),
(370, '批量开放', 'ProductOpen', 352, 0, 6, 0, 1, '2011-04-27 23:29:05', 0),
(371, '批量关闭', 'ProductClose', 352, 0, 7, 0, 1, '2011-04-27 23:29:14', 0),
(372, '管理所有信息', 'ProductAll', 352, 0, 8, 0, 1, '2011-04-27 23:29:22', 0),
(373, '添加', 'UserRankAdd', 353, 0, 1, 0, 1, '2011-04-27 23:29:46', 0),
(374, '修改', 'UserRankEdit', 353, 0, 2, 0, 1, '2011-04-27 23:29:54', 0),
(375, '删除', 'UserRankDel', 353, 0, 3, 0, 1, '2011-04-27 23:30:05', 0),
(376, '批量开放', 'UserRankOpen', 353, 0, 4, 0, 1, '2011-04-27 23:30:15', 0),
(377, '批量关闭', 'UserRankClose', 353, 0, 5, 0, 1, '2011-04-27 23:30:29', 0),
(378, '管理所有信息', 'UserRankAll', 353, 0, 7, 0, 1, '2011-04-27 23:30:36', 0),
(379, '删除', 'FeedbackDel', 354, 0, 1, 0, 1, '2011-04-27 23:31:39', 0),
(380, '处理反馈', 'FeedbackDeal', 354, 0, 2, 0, 1, '2011-04-27 23:33:33', 0),
(381, '分配操作权限', 'UserRankSetLimit', 353, 0, 6, 0, 1, '2011-05-01 11:20:11', 0),
(382, '删除', 'ConfigDel', 129, 0, 4, 0, 1, '2011-08-25 17:45:34', 0),
(383, '留言管理', 'Guestbook', 0, 5, 43, 0, 1, '2011-09-25 01:52:07', 0),
(384, '开放留言', 'GuestbookOpen', 383, 0, 2, 0, 1, '2011-09-25 01:53:08', 0),
(385, '关闭留言', 'GuestbookClose', 383, 0, 3, 0, 1, '2011-09-25 01:53:23', 0),
(386, '删除留言', 'GuestbookDel', 383, 0, 4, 0, 1, '2011-09-25 01:53:46', 0),
(387, '回复留言', 'GuestbookReply', 383, 0, 5, 0, 1, '2011-09-25 01:54:12', 0),
(388, '添加留言', 'GuestbookAdd', 383, 0, 1, 0, 1, '2011-09-25 01:56:17', 0),
(389, '批量导入', 'UserImport', 21, 0, 9, 0, 1, '2011-11-11 00:01:26', 0),
(390, '批量导出', 'UserExport', 21, 0, 10, 0, 1, '2011-11-28 00:01:45', 0),
(391, '产品图片', 'ProductPic', 0, 6, 18, 0, 1, '2012-01-19 23:53:03', 0),
(392, '添加', 'ProductPicAdd', 391, 0, 1, 0, 1, '2012-01-19 23:54:17', 0),
(393, '修改', 'ProductPicEdit', 391, 0, 2, 0, 1, '2012-01-19 23:54:27', 0),
(394, '删除', 'ProductPicDel', 391, 0, 3, 0, 1, '2012-01-19 23:54:36', 0),
(395, '批量开放', 'ProductPicOpen', 391, 0, 4, 0, 1, '2012-01-19 23:54:45', 0),
(396, '批量关闭', 'ProductPicClose', 391, 0, 5, 0, 1, '2012-01-19 23:54:57', 0),
(397, '管理所有信息', 'ProductPicAll', 391, 0, 6, 0, 1, '2012-01-19 23:55:06', 0),
(398, '导出E-mail', 'UserEmailExport', 21, 0, 11, 0, 1, '2012-02-19 03:10:17', 0),
(399, '导出', 'MailExport', 348, 0, 2, 0, 1, '2012-02-19 03:16:29', 0),
(400, '调查管理', 'Survey', 0, 7, 21, 0, 1, '2012-02-20 01:01:20', 0),
(401, '调查项管理', 'SurveyItem', 0, 6, 22, 0, 1, '2012-02-20 01:03:14', 0),
(402, '调查结果管理', 'SurveyResult', 0, 2, 23, 0, 1, '2012-02-20 01:03:41', 0),
(403, '添加', 'SurveyAdd', 400, 0, 1, 0, 1, '2012-02-20 01:10:29', 0),
(404, '修改', 'SurveyEdit', 400, 0, 2, 0, 1, '2012-02-20 01:10:45', 0),
(405, '删除', 'SurveyDel', 400, 0, 3, 0, 1, '2012-02-20 01:10:55', 0),
(406, '批量开放', 'SurveyOpen', 400, 0, 5, 0, 1, '2012-02-20 01:11:14', 0),
(407, '批量关闭', 'SurveyClose', 400, 0, 6, 0, 1, '2012-02-20 01:11:28', 0),
(408, '管理所有信息', 'SurveyAll', 400, 0, 7, 0, 1, '2012-02-20 01:11:47', 0),
(409, '添加', 'SurveyItemAdd', 401, 0, 1, 0, 1, '2012-02-20 01:12:27', 0),
(410, '修改', 'SurveyItemEdit', 401, 0, 2, 0, 1, '2012-02-20 01:12:39', 0),
(411, '删除', 'SurveyItemDel', 401, 0, 3, 0, 1, '2012-02-20 01:12:48', 0),
(412, '批量开放', 'SurveyItemOpen', 401, 0, 4, 0, 1, '2012-02-20 01:13:01', 0),
(413, '批量关闭', 'SurveyItemClose', 401, 0, 5, 0, 1, '2012-02-20 01:13:14', 0),
(414, '管理所有信息', 'SurveyItemAll', 401, 0, 6, 0, 1, '2012-02-20 01:13:24', 0),
(415, '删除', 'SurveyResultDel', 402, 0, 1, 0, 1, '2012-02-20 01:13:56', 0),
(416, '查看', 'SurveyResultView', 402, 0, 2, 0, 1, '2012-02-20 01:14:09', 0),
(417, '转移', 'ArticleTransfer', 17, 0, 4, 0, 1, '2012-02-20 23:56:16', 0),
(418, '转移', 'ProductTransfer', 352, 0, 5, 0, 1, '2012-02-20 23:56:59', 0),
(419, '转移', 'JobTransfer', 350, 0, 4, 0, 1, '2012-02-20 23:57:25', 0),
(420, '转移', 'DownloadTransfer', 351, 0, 4, 0, 1, '2012-02-20 23:57:55', 0),
(421, '转移', 'SurveyTransfer', 400, 0, 4, 0, 1, '2012-02-20 23:58:16', 0),
(423, '栏目属性', 'ClassProperty', 0, 6, 4, 0, 1, '2012-04-20 22:33:26', 0),
(424, '栏目模板', 'ClassTemplate', 0, 6, 5, 0, 1, '2012-04-20 22:34:09', 0),
(425, '文章图片', 'ArticlePic', 0, 6, 16, 0, 1, '2012-04-20 22:35:18', 0),
(426, '添加', 'ClassPropertyAdd', 423, 0, 1, 0, 1, '2012-04-20 22:36:25', 0),
(427, '修改', 'ClassPropertyEdit', 423, 0, 2, 0, 1, '2012-04-20 22:36:39', 0),
(428, '删除', 'ClassPropertyDel', 423, 0, 3, 0, 1, '2012-04-20 22:36:51', 0),
(429, '批量开放', 'ClassPropertyOpen', 423, 0, 4, 0, 1, '2012-04-20 22:37:21', 0),
(430, '批量关闭', 'ClassPropertyClose', 423, 0, 5, 0, 1, '2012-04-20 22:37:33', 0),
(431, '管理所有信息', 'ClassPropertyAll', 423, 0, 6, 0, 1, '2012-04-20 22:37:47', 0),
(432, '添加', 'ClassTemplateAdd', 424, 0, 1, 0, 1, '2012-04-20 23:31:35', 0),
(433, '修改', 'ClassTemplateEdit', 424, 0, 2, 0, 1, '2012-04-20 23:31:45', 0),
(434, '删除', 'ClassTemplateDel', 424, 0, 3, 0, 1, '2012-04-20 23:31:55', 0),
(435, '批量开放', 'ClassTemplateOpen', 424, 0, 4, 0, 1, '2012-04-20 23:32:04', 0),
(436, '批量关闭', 'ClassTemplateClose', 424, 0, 5, 0, 1, '2012-04-20 23:32:13', 0),
(437, '管理所有信息', 'ClassTemplateAll', 424, 0, 6, 0, 1, '2012-04-20 23:32:22', 0),
(438, '添加', 'ArticlePicAdd', 425, 0, 1, 0, 1, '2012-04-20 23:32:43', 0),
(439, '修改', 'ArticlePicEdit', 425, 0, 2, 0, 1, '2012-04-20 23:32:52', 0),
(440, '删除', 'ArticlePicDel', 425, 0, 3, 0, 1, '2012-04-20 23:33:04', 0),
(441, '批量开放', 'ArticlePicOpen', 425, 0, 4, 0, 1, '2012-04-20 23:33:14', 0),
(442, '批量关闭', 'ArticlePicClose', 425, 0, 5, 0, 1, '2012-04-20 23:33:27', 0),
(443, '管理所有信息', 'ArticlePicAll', 425, 0, 6, 0, 1, '2012-04-20 23:33:43', 0),
(444, '备份数据库', 'BackUp', 0, 0, 50, 0, 1, '2012-09-25 00:00:00', 0),
(445, '站点地图', 'SiteMap', 0, 0, 51, 0, 1, '2012-09-25 00:00:00', 0),
(446, '视频管理', 'Video', 0, 6, 24, 0, 1, '2012-09-25 14:14:44', 0),
(447, '添加', 'VideoAdd', 446, 0, 1, 0, 1, '2012-09-25 14:15:48', 0),
(448, '修改', 'VideoEdit', 446, 0, 2, 0, 1, '2012-09-25 14:16:56', 0),
(449, '删除', 'VideoDel', 446, 0, 3, 0, 1, '2012-09-25 14:17:25', 0),
(450, '批量开放', 'VideoOpen', 446, 0, 4, 0, 1, '2012-09-25 14:17:42', 0),
(451, '批量关闭', 'VideoClose', 446, 0, 5, 0, 1, '2012-09-25 14:17:52', 0),
(452, '相册管理', 'Photo', 0, 6, 25, 0, 1, '2012-09-25 14:18:09', 0),
(453, '添加', 'PhotoAdd', 452, 0, 1, 0, 1, '2012-09-25 14:18:36', 0),
(454, '修改', 'PhotoEdit', 452, 0, 2, 0, 1, '2012-09-25 14:18:45', 0),
(455, '删除', 'PhotoDel', 452, 0, 3, 0, 1, '2012-09-25 14:18:53', 0),
(456, '批量开放', 'PhotoOpen', 452, 0, 4, 0, 1, '2012-09-25 14:19:08', 0),
(457, '批量关闭', 'PhotoClose', 452, 0, 5, 0, 1, '2012-09-25 14:19:21', 0),
(458, '复制', 'ProductCopy', 352, 0, 4, 0, 1, '2012-10-12 17:40:27', 0),
(459, '转移', 'LinkTransfer', 124, 0, 4, 0, 1, '2012-10-16 11:06:52', 0),
(460, '系统配置', 'Set', 0, 0, 1, 1, 1, '2012-10-17 13:46:56', 0),
(461, '片段内容', 'Block', 0, 6, 26, 0, 1, '2012-10-17 13:49:26', 0),
(462, '添加', 'BlockAdd', 461, 0, 1, 0, 1, '2012-10-17 13:50:22', 0),
(463, '修改', 'BlockEdit', 461, 0, 2, 0, 1, '2012-10-17 13:50:32', 0),
(464, '删除', 'BlockDel', 461, 0, 3, 0, 1, '2012-10-17 13:50:41', 0),
(465, '批量开放', 'BlockOpen', 461, 0, 4, 0, 1, '2012-10-17 13:51:01', 0),
(466, '批量关闭', 'BlockClose', 461, 0, 5, 0, 1, '2012-10-17 13:51:11', 0),
(467, '管理所有信息', 'VideoAll', 446, 0, 6, 0, 1, '2012-10-17 17:30:22', 0),
(468, '管理所有信息', 'PhotoAll', 452, 0, 6, 0, 1, '2012-10-17 17:30:53', 0),
(469, '管理所有信息', 'BlockAll', 461, 0, 6, 0, 1, '2012-10-17 17:31:15', 0);

-- --------------------------------------------------------

--
-- 表的结构 `t_link`
--

CREATE TABLE IF NOT EXISTS `t_link` (
  `LinkID` int(11) NOT NULL auto_increment,
  `ConfigID` int(11) NOT NULL,
  `TypeID` int(11) NOT NULL,
  `SiteName` varchar(200) character set utf8 default NULL,
  `SiteUrl` varchar(200) character set utf8 default NULL,
  `LogoUrl` varchar(200) character set utf8 default NULL,
  `ListID` int(11) NOT NULL,
  `AdminID` int(11) NOT NULL,
  `AddTime` datetime default NULL,
  `IsClose` int(11) NOT NULL,
  PRIMARY KEY  (`LinkID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=4 ;

--
-- 转存表中的数据 `t_link`
--

INSERT INTO `t_link` (`LinkID`, `ConfigID`, `TypeID`, `SiteName`, `SiteUrl`, `LogoUrl`, `ListID`, `AdminID`, `AddTime`, `IsClose`) VALUES
(1, 1, 2, '百度', 'http://www.baidu.com', 'http://www.baidu.com/img/baidu_jgylogo3.gif', 1, 1, '2011-04-28 11:42:37', 0),
(3, 1, 1, '新浪', 'http://www.sina.cn', '', 2, 1, '2012-09-19 14:02:59', 0);

-- --------------------------------------------------------

--
-- 表的结构 `t_mail`
--

CREATE TABLE IF NOT EXISTS `t_mail` (
  `MailID` int(11) NOT NULL auto_increment,
  `MailAddress` varchar(200) character set utf8 default NULL,
  `IsRec` int(11) NOT NULL,
  `AddTime` datetime default NULL,
  PRIMARY KEY  (`MailID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

--
-- 转存表中的数据 `t_mail`
--

INSERT INTO `t_mail` (`MailID`, `MailAddress`, `IsRec`, `AddTime`) VALUES
(1, 'bd-sky@qq.com', 1, '2011-04-28 05:25:20');

-- --------------------------------------------------------

--
-- 表的结构 `t_message`
--

CREATE TABLE IF NOT EXISTS `t_message` (
  `MessageID` int(11) NOT NULL auto_increment,
  `DictionaryID` int(11) NOT NULL,
  `UserID` int(11) NOT NULL,
  `Title` varchar(200) character set utf8 default NULL,
  `MessageContent` longtext character set utf8,
  `AdminID` int(11) NOT NULL,
  `ParentID` int(11) NOT NULL,
  `AddTime` datetime default NULL,
  `IsRead` int(11) NOT NULL,
  `IsReply` int(11) NOT NULL,
  `IsEnd` int(11) NOT NULL,
  PRIMARY KEY  (`MessageID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

--
-- 转存表中的数据 `t_message`
--


-- --------------------------------------------------------

--
-- 表的结构 `t_path`
--

CREATE TABLE IF NOT EXISTS `t_path` (
  `Path` varchar(200) character set utf8 default NULL,
  `AdminID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- 转存表中的数据 `t_path`
--

INSERT INTO `t_path` (`Path`, `AdminID`) VALUES
('/Files/WaterPic/2.jpg', 1),
('/Files/WaterPic/2.jpg', 1);

-- --------------------------------------------------------

--
-- 表的结构 `t_photo`
--

CREATE TABLE IF NOT EXISTS `t_photo` (
  `PhotoID` int(11) NOT NULL auto_increment,
  `ClassID` int(11) NOT NULL,
  `Title` varchar(200) character set utf8 default NULL,
  `SmallPic` varchar(200) character set utf8 default NULL,
  `BigPic` varchar(200) character set utf8 default NULL,
  `Description` longtext character set utf8,
  `ListID` int(11) NOT NULL,
  `AdminID` int(11) NOT NULL,
  `AddTime` datetime default NULL,
  `IsClose` int(11) NOT NULL,
  PRIMARY KEY  (`PhotoID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

--
-- 转存表中的数据 `t_photo`
--


-- --------------------------------------------------------

--
-- 表的结构 `t_product`
--

CREATE TABLE IF NOT EXISTS `t_product` (
  `ProductID` int(11) NOT NULL auto_increment,
  `ClassID` int(11) NOT NULL,
  `ProductName` varchar(200) character set utf8 default NULL,
  `SmallPic` varchar(200) character set utf8 default NULL,
  `BigPic` varchar(200) character set utf8 default NULL,
  `Tags` varchar(200) character set utf8 default NULL,
  `Keywords` varchar(200) character set utf8 default NULL,
  `Description` longtext character set utf8,
  `Details` longtext character set utf8,
  `ClickNum` int(11) NOT NULL,
  `ListID` int(11) NOT NULL,
  `IsRecommend` int(11) NOT NULL,
  `AdminID` int(11) NOT NULL,
  `AddTime` datetime default NULL,
  `IsClose` int(11) NOT NULL,
  PRIMARY KEY  (`ProductID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

--
-- 转存表中的数据 `t_product`
--


-- --------------------------------------------------------

--
-- 表的结构 `t_productpic`
--

CREATE TABLE IF NOT EXISTS `t_productpic` (
  `ProductPicID` int(11) NOT NULL auto_increment,
  `ProductID` int(11) NOT NULL,
  `Title` varchar(200) character set utf8 default NULL,
  `SmallPic` varchar(200) character set utf8 default NULL,
  `BigPic` varchar(200) character set utf8 default NULL,
  `Description` longtext character set utf8,
  `ListID` int(11) NOT NULL,
  `AdminID` int(11) NOT NULL,
  `AddTime` datetime default NULL,
  `IsClose` int(11) NOT NULL,
  PRIMARY KEY  (`ProductPicID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

--
-- 转存表中的数据 `t_productpic`
--


-- --------------------------------------------------------

--
-- 表的结构 `t_set`
--

CREATE TABLE IF NOT EXISTS `t_set` (
  `SetID` int(11) NOT NULL auto_increment,
  `WaterTypeID` int(11) default NULL,
  `WaterText` varchar(50) character set utf8 default NULL,
  `Font` varchar(50) character set utf8 default NULL,
  `FontSize` int(11) default NULL,
  `FontColor` varchar(50) character set utf8 default NULL,
  `WaterPic` varchar(200) character set utf8 default NULL,
  `WaterPosition` varchar(50) character set utf8 default NULL,
  `IsArticleThumb` int(11) default NULL,
  `ArticleThumbWidth` int(11) default NULL,
  `ArticleThumbHeight` int(11) default NULL,
  `IsProductThumb` int(11) default NULL,
  `ProductThumbWidth` int(11) default NULL,
  `ProductThumbHeight` int(11) default NULL,
  `IsPhotoThumb` int(11) default NULL,
  `PhotoThumbWidth` int(11) default NULL,
  `PhotoThumbHeight` int(11) default NULL,
  PRIMARY KEY  (`SetID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

--
-- 转存表中的数据 `t_set`
--

INSERT INTO `t_set` (`SetID`, `WaterTypeID`, `WaterText`, `Font`, `FontSize`, `FontColor`, `WaterPic`, `WaterPosition`, `IsArticleThumb`, `ArticleThumbWidth`, `ArticleThumbHeight`, `IsProductThumb`, `ProductThumbWidth`, `ProductThumbHeight`, `IsPhotoThumb`, `PhotoThumbWidth`, `PhotoThumbHeight`) VALUES
(1, 2, '汇鑫科技', 'Regular', 12, 'White', '/Files/WaterPic/water.jpg', 'RightB', 0, 100, 100, 0, 100, 100, 0, 100, 100);

-- --------------------------------------------------------

--
-- 表的结构 `t_survey`
--

CREATE TABLE IF NOT EXISTS `t_survey` (
  `SurveyID` int(11) NOT NULL auto_increment,
  `ClassID` int(11) NOT NULL,
  `Subject` varchar(200) character set utf8 default NULL,
  `IntrContent` longtext character set utf8,
  `IsRecommend` int(11) NOT NULL,
  `ClickNum` int(11) NOT NULL,
  `ListID` int(11) NOT NULL,
  `AdminID` int(11) NOT NULL,
  `AddTime` datetime default NULL,
  `IsClose` int(11) NOT NULL,
  PRIMARY KEY  (`SurveyID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

--
-- 转存表中的数据 `t_survey`
--


-- --------------------------------------------------------

--
-- 表的结构 `t_surveyitem`
--

CREATE TABLE IF NOT EXISTS `t_surveyitem` (
  `SurveyItemID` int(11) NOT NULL auto_increment,
  `ItemName` varchar(200) character set utf8 default NULL,
  `TypeID` int(11) NOT NULL,
  `SurveyID` int(11) NOT NULL,
  `ListID` int(11) NOT NULL,
  `AdminID` int(11) NOT NULL,
  `AddTime` datetime default NULL,
  `IsClose` int(11) NOT NULL,
  PRIMARY KEY  (`SurveyItemID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

--
-- 转存表中的数据 `t_surveyitem`
--


-- --------------------------------------------------------

--
-- 表的结构 `t_surveyitemoption`
--

CREATE TABLE IF NOT EXISTS `t_surveyitemoption` (
  `SurveyItemOptionID` int(11) NOT NULL auto_increment,
  `ItemOptionName` varchar(200) character set utf8 default NULL,
  `SurveyItemID` int(11) NOT NULL,
  `ListID` int(11) NOT NULL,
  `AdminID` int(11) NOT NULL,
  PRIMARY KEY  (`SurveyItemOptionID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

--
-- 转存表中的数据 `t_surveyitemoption`
--


-- --------------------------------------------------------

--
-- 表的结构 `t_surveyresult`
--

CREATE TABLE IF NOT EXISTS `t_surveyresult` (
  `SurveyResultID` int(11) NOT NULL auto_increment,
  `SurveyContent` longtext character set utf8,
  `IP` varchar(200) character set utf8 default NULL,
  `SurveyID` int(11) NOT NULL,
  `AddTime` datetime default NULL,
  PRIMARY KEY  (`SurveyResultID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

--
-- 转存表中的数据 `t_surveyresult`
--


-- --------------------------------------------------------

--
-- 表的结构 `t_user`
--

CREATE TABLE IF NOT EXISTS `t_user` (
  `UserID` int(11) NOT NULL auto_increment,
  `UserName` varchar(200) character set utf8 default NULL,
  `UserPass` varchar(200) character set utf8 default NULL,
  `PassQuestion` varchar(200) character set utf8 default NULL,
  `PassAnswer` varchar(200) character set utf8 default NULL,
  `RealName` varchar(200) character set utf8 default NULL,
  `Sex` varchar(200) character set utf8 default NULL,
  `Email` varchar(200) character set utf8 default NULL,
  `Mobile` varchar(200) character set utf8 default NULL,
  `Address` varchar(200) character set utf8 default NULL,
  `Company` varchar(200) character set utf8 default NULL,
  `Comment` longtext character set utf8,
  `UserRankID` int(11) NOT NULL,
  `IsAudit` int(11) NOT NULL,
  `Point` int(11) NOT NULL,
  `LoginNum` int(11) NOT NULL,
  `LastLoginTime` datetime default NULL,
  `ThisLoginTime` datetime default NULL,
  `AddTime` datetime default NULL,
  `IsClose` int(11) NOT NULL,
  PRIMARY KEY  (`UserID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

--
-- 转存表中的数据 `t_user`
--

INSERT INTO `t_user` (`UserID`, `UserName`, `UserPass`, `PassQuestion`, `PassAnswer`, `RealName`, `Sex`, `Email`, `Mobile`, `Address`, `Company`, `Comment`, `UserRankID`, `IsAudit`, `Point`, `LoginNum`, `LastLoginTime`, `ThisLoginTime`, `AddTime`, `IsClose`) VALUES
(1, 'test', '098F6BCD4621D373CADE4E832627B4F6', 'test', 'test', 'test', '男', 'test@test.com', '123456', '123456', 'test@test.com', 'test', 1, 1, 100, 14, '2012-02-27 23:09:32', '2012-05-20 21:53:21', '2010-12-07 12:01:27', 0);

-- --------------------------------------------------------

--
-- 表的结构 `t_userlog`
--

CREATE TABLE IF NOT EXISTS `t_userlog` (
  `UserLogID` int(11) NOT NULL auto_increment,
  `LogContent` longtext character set utf8,
  `ScriptFile` varchar(200) character set utf8 default NULL,
  `IPAddress` varchar(200) character set utf8 default NULL,
  `UserID` int(11) NOT NULL,
  `AddTime` datetime default NULL,
  PRIMARY KEY  (`UserLogID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=40 ;

--
-- 转存表中的数据 `t_userlog`
--

INSERT INTO `t_userlog` (`UserLogID`, `LogContent`, `ScriptFile`, `IPAddress`, `UserID`, `AddTime`) VALUES
(39, '登录系统！', '/user/Login.aspx', '127.0.0.1', 1, '2012-05-20 21:53:21');

-- --------------------------------------------------------

--
-- 表的结构 `t_userrank`
--

CREATE TABLE IF NOT EXISTS `t_userrank` (
  `UserRankID` int(11) NOT NULL auto_increment,
  `UserRankName` varchar(200) character set utf8 default NULL,
  `LimitValues` longtext character set utf8,
  `ListID` int(11) NOT NULL,
  `AdminID` int(11) NOT NULL,
  `AddTime` datetime default NULL,
  `IsClose` int(11) NOT NULL,
  PRIMARY KEY  (`UserRankID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

--
-- 转存表中的数据 `t_userrank`
--

INSERT INTO `t_userrank` (`UserRankID`, `UserRankName`, `LimitValues`, `ListID`, `AdminID`, `AddTime`, `IsClose`) VALUES
(1, '普通会员', '-1,Account,-1', 1, 1, '2010-12-07 17:07:46', 0);

-- --------------------------------------------------------

--
-- 表的结构 `t_video`
--

CREATE TABLE IF NOT EXISTS `t_video` (
  `VideoID` int(11) NOT NULL auto_increment,
  `ClassID` int(11) NOT NULL,
  `Title` varchar(200) character set utf8 default NULL,
  `VideoPic` varchar(200) character set utf8 default NULL,
  `VideoPath` varchar(200) character set utf8 default NULL,
  `Description` longtext character set utf8,
  `ListID` int(11) NOT NULL,
  `AdminID` int(11) NOT NULL,
  `AddTime` datetime default NULL,
  `IsClose` int(11) NOT NULL,
  PRIMARY KEY  (`VideoID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

--
-- 转存表中的数据 `t_video`
--

