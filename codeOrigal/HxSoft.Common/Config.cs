using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
//
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
//
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using http = System.Web.HttpContext;
using InsideSystem.Utility;
using System.Data;
using System.Threading;
using Newtonsoft.Json;
using Microsoft.VisualBasic;


namespace HxSoft.Common
{
    /// <summary>
    /// 常用方法
    /// </summary>
    public class Config
    {
        #region 系统模块编号配置
        /// <summary>
        /// 单页面
        /// </summary>
        public static string SysSinglePageMouldID
        {
            get
            {
                return ConfigurationManager.AppSettings["SysSinglePageMouldID"].ToString();
            }
        }
        /// <summary>
        /// 文章
        /// </summary>
        public static string SysArticleMouldID
        {
            get
            {
                return ConfigurationManager.AppSettings["SysArticleMouldID"].ToString();
            }
        }
        /// <summary>
        /// 产品
        /// </summary>
        public static string SysProductMouldID
        {
            get
            {
                return ConfigurationManager.AppSettings["SysProductMouldID"].ToString();
            }
        }
        /// <summary>
        /// 招聘
        /// </summary>
        public static string SysJobMouldID
        {
            get
            {
                return ConfigurationManager.AppSettings["SysJobMouldID"].ToString();
            }
        }
        /// <summary>
        /// 下载
        /// </summary>
        public static string SysDownloadMouldID
        {
            get
            {
                return ConfigurationManager.AppSettings["SysDownloadMouldID"].ToString();
            }
        }
        /// <summary>
        /// 调查
        /// </summary>
        public static string SysSurveyMouldID
        {
            get
            {
                return ConfigurationManager.AppSettings["SysSurveyMouldID"].ToString();
            }
        }
        /// <summary>
        /// 友情链接
        /// </summary>
        public static string SysLinkMouldID
        {
            get
            {
                return ConfigurationManager.AppSettings["SysLinkMouldID"].ToString();
            }
        }
        /// <summary>
        /// 网站地图
        /// </summary>
        public static string SysSitemapMouldID
        {
            get
            {
                return ConfigurationManager.AppSettings["SysSitemapMouldID"].ToString();
            }
        }
        /// <summary>
        /// 信息反馈
        /// </summary>
        public static string SysFeedbackMouldID
        {
            get
            {
                return ConfigurationManager.AppSettings["SysFeedbackMouldID"].ToString();
            }
        }
        /// <summary>
        /// 留言本
        /// </summary>
        public static string SysGuestbookMouldID
        {
            get
            {
                return ConfigurationManager.AppSettings["SysGuestbookMouldID"].ToString();
            }
        }
        /// <summary>
        /// 外链
        /// </summary>
        public static string SysOuterLinkMouldID
        {
            get
            {
                return ConfigurationManager.AppSettings["SysOuterLinkMouldID"].ToString();
            }
        }

        /// <summary>
        /// 视频管理
        /// </summary>
        public static string SysVideoMouldID
        {
            get
            {
                return ConfigurationManager.AppSettings["SysVideoMouldID"].ToString();
            }
        }

        /// <summary>
        /// 相册管理
        /// </summary>
        public static string SysPhotoMouldID
        {
            get
            {
                return ConfigurationManager.AppSettings["SysPhotoMouldID"].ToString();
            }
        }

        //---------------------------------------------------------------------------------------
        /// <summary>
        /// 留言分类
        /// </summary>
        public static string SysMessageDictionaryMouldID
        {
            get
            {
                return ConfigurationManager.AppSettings["SysMessageDictionaryMouldID"].ToString();
            }
        }
        /// <summary>
        /// 反馈分类
        /// </summary>
        public static string SysFeedbackDictionaryMouldID
        {
            get
            {
                return ConfigurationManager.AppSettings["SysFeedbackDictionaryMouldID"].ToString();
            }
        }
        /// <summary>
        /// 会员权限字段
        /// </summary>
        public static string SysUserLimitMouldID
        {
            get
            {
                return ConfigurationManager.AppSettings["SysUserLimitMouldID"].ToString();
            }
        }
        #endregion
        #region 栏目编号
          public static string SysHelp
        {
            get
            {
                return ConfigurationManager.AppSettings["SysHelp"].ToString();
            }
        }
        #endregion
        #region 数据字典编号设置
        /// <summary>
        /// 信息反馈
        /// </summary>
        public static string FeedbackDictionaryMouldID
        {
            get
            {
                return ConfigurationManager.AppSettings["FeedbackDictionaryMouldID"].ToString();
            }
        }
        /// <summary>
        /// 在线招聘
        /// </summary>
        public static string JobDictionaryMouldID
        {
            get
            {
                return ConfigurationManager.AppSettings["JobDictionaryMouldID"].ToString();
            }
        }
        /// <summary>
        /// 产品订购
        /// </summary>
        public static string ProductDictionaryMouldID
        {
            get
            {
                return ConfigurationManager.AppSettings["ProductDictionaryMouldID"].ToString();
            }
        }
        /// <summary>
        /// 产品类型
        /// </summary>
        public static string ProductTypeID
        {
            get
            {
                return ConfigurationManager.AppSettings["ProductTypeID"].ToString();
            }
        }
        #endregion
        #region 网站配置
        /// <summary>
        /// 默认站点目录
        /// </summary>
        public static string DefaultSiteDir
        {
            get
            {
                return ConfigurationManager.AppSettings["DefaultSiteDir"].ToString();
            }
        }
        /// <summary>
        /// 伪静态文件后缀
        /// </summary>
        public static string FileExt
        {
            get
            {
                return ConfigurationManager.AppSettings["FileExt"].ToString();
            }
        }
        /// <summary>
        /// 系统名称
        /// </summary>
        public static string SystemName
        {
            get
            {
                return ConfigurationManager.AppSettings["SystemName"].ToString();
            }
        }
        /// <summary>
        /// 授权
        /// </summary>
        public static string Authorized
        {
            get
            {
                return ConfigurationManager.AppSettings["Authorized"].ToString();
            }
        }
        /// <summary>
        /// 版权所有
        /// </summary>
        public static string Copyright
        {
            get
            {
                return ConfigurationManager.AppSettings["Copyright"].ToString();
            }
        }
        #endregion
        #region 后台配置
        /// <summary>
        /// 后台目录路径
        /// </summary>
        public static string AdminPath
        {
            get
            {
                return ConfigurationManager.AppSettings["AdminPath"].ToString();
            }
        }
        /// <summary>
        /// 超级管理员ID
        /// </summary>
        public static string SystemAdminID
        {
            get
            {
                return "1";
            }
        }
        /// <summary>
        /// 文件上传目录路径
        /// </summary>
        public static string FileUploadPath
        {
            get
            {
                return ConfigurationManager.AppSettings["FileUploadPath"].ToString();
            }
        }
        #endregion

        #region 数据库连接
        #region 数据库连接字符串
        /// <summary>
        /// 数据库连接字符是否加密码,针对SQL数据库,0-公开方式,1-加密方式
        /// </summary>
        public static string IsEncrypt
        {
            get { return ConfigurationManager.AppSettings["IsEncrypt"].ToString(); }
        }
        /// <summary>
        /// 数据库类型,Sql(sql2000/2005/2008),OleDb(Access2000/2003/2007),MySql,Oracle
        /// </summary>
        public static string DatabaseType
        {
            get { return ConfigurationManager.AppSettings["DatabaseType"].ToString(); }
        }
        /// <summary>
        /// Sql数据库连接字符串
        /// </summary>
        public static string SqlConnStr
        {
            get
            {
                if (IsEncrypt == "1")
                {
                    return ConnectionInfo.DecryptDBConnectionString(ConfigurationManager.AppSettings["SqlConnStr"].ToString());
                }
                else
                {
                    return ConfigurationManager.AppSettings["SqlConnStr"].ToString();
                }
            }
        }
        /// <summary>
        /// Access数据库连接字符串
        /// </summary>
        public static string AccessConnStr
        {
            get
            {
                string strAccessPath = http.Current.Server.MapPath(ConfigurationManager.AppSettings["AccessPath"].ToString());
                return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strAccessPath;
                //return "Provider=Microsoft.Ace.OleDb.12.0;Data Source=" + strAccessPath;
            }
        }

        public static string AccessStr
        {
            get
            {
                return ConfigurationManager.AppSettings["AccessPath"].ToString();
            }
        }
        /// <summary>
        /// Excel数据库连接字符串
        /// </summary>
        public static string ExcelConnStr
        {
            get
            {
                string strExcelPath = http.Current.Server.MapPath(ConfigurationManager.AppSettings["ExcelPath"].ToString());
                return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strExcelPath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
                //return "Provider=Microsoft.Ace.OleDb.12.0;Data Source=" + strExcelPath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\"";
            }
        }
        /// <summary>
        /// 地区数据库连接字符串
        /// </summary>
        public static string AreaConnStr
        {
            get
            {
                string strAreaPath = http.Current.Server.MapPath("/App_Data/area.mdb");
                return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strAreaPath;
            }
        }
        /// <summary>
        /// MySql_Server
        /// </summary>
        public static string MySql_Server
        {
            get
            {
                return ConfigurationManager.AppSettings["MySql_Server"].ToString();
            }
        }
        /// <summary>
        /// MySql_Port
        /// </summary>
        public static string MySql_Port
        {
            get
            {
                return ConfigurationManager.AppSettings["MySql_Port"].ToString();
            }
        }
        /// <summary>
        /// MySql_Database
        /// </summary>
        public static string MySql_Database
        {
            get
            {
                return ConfigurationManager.AppSettings["MySql_Database"].ToString();
            }
        }
        /// <summary>
        /// MySql_Uid
        /// </summary>
        public static string MySql_Uid
        {
            get
            {
                return ConfigurationManager.AppSettings["MySql_Uid"].ToString();
            }
        }
        /// <summary>
        /// MySql_Pwd
        /// </summary>
        public static string MySql_Pwd
        {
            get
            {
                return ConfigurationManager.AppSettings["MySql_Pwd"].ToString();
            }
        }
        /// <summary>
        /// MySql_Charset
        /// </summary>
        public static string MySql_Charset
        {
            get
            {
                return ConfigurationManager.AppSettings["MySql_Charset"].ToString();
            }
        }
        /// <summary>
        /// MySql数据库连接字符串
        /// </summary>
        public static string MySqlConnStr
        {
            get
            {
                string strTemp = "Server=" + MySql_Server + ";Port=" + MySql_Port + ";Database=" + MySql_Database + ";Uid=" + MySql_Uid + ";Pwd=" + MySql_Pwd + ";Charset=" + MySql_Charset + "";
                if (IsEncrypt == "1")
                {
                    return ConnectionInfo.DecryptDBConnectionString(strTemp);
                }
                else
                {
                    return strTemp;
                }
            }
        }
        #endregion
        #region 数据库接口实例化
        /// <summary>
        /// 数据库接口实例化
        /// </summary>
        /// <returns></returns>
        private static DbHelper _dbhelper;
        /// <summary>
        /// 数据库接口实例化
        /// </summary>
        /// <returns></returns>
        public static DbHelper Conn()
        {
            if (_dbhelper == null)
            {
                _dbhelper = new DbHelper();
            }
            return _dbhelper;
        }
        #endregion
        #region 数据类型集合,MySql, OleDb, Oracle, Sql
        /// <summary>
        /// 数据类型集合
        /// </summary>
        public enum DatabaseTypeCollection
        {
            MySql, OleDb, Oracle, Sql
        }
        #endregion
        #region 数据绑定控件类型集合
        /// <summary>
        /// 数据绑定控件类型集合,CheckBoxList, DropDownList, ListBox, RadioButtonList, Repeater, DataList, DataGrid, GridView, DetailsView, FormView
        /// </summary>
        public enum DataBindObjTypeCollection
        {
            CheckBoxList, DropDownList, ListBox, RadioButtonList, Repeater, DataList, DataGrid, GridView, DetailsView, FormView
        }
        #endregion
        #endregion

        #region 使用System.Net.Mail发送邮件
        /// <summary>
        /// 使用System.Net.Mail发送邮件
        /// </summary>
        /// <param name="SmtpServer">SMTP服务器</param>
        /// <param name="UserName">用户名</param>
        /// <param name="Password">密码</param>
        /// <param name="ReceiveAddress">收信地址</param>
        /// <param name="CcAddress">抄送地址</param>
        /// <param name="BccAddress">暗送地址</param>
        /// <param name="Subject">邮件主题</param>
        /// <param name="MailBody">邮件内容</param>
        /// <param name="Attachment">附件地址</param>
        /// <param name="IsHTML">是否发送HTML邮件</param>
        /// <param name="IsSSL">是否需要服务器验证</param>
        public static void SendMailMessage(string SmtpServer, string UserName, string Password, string ReceiveAddress, string CcAddress, string BccAddress, string Subject, string MailBody, string Attachment, bool IsHTML, bool IsSSL)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(UserName);
            mail.To.Add(ReceiveAddress);
            if (CcAddress != string.Empty) mail.CC.Add(CcAddress);
            if (BccAddress != string.Empty) mail.Bcc.Add(BccAddress);
            if (File.Exists(Attachment) == true) mail.Attachments.Add(new Attachment(Attachment));
            mail.Subject = Subject;
            mail.IsBodyHtml = IsHTML;
            mail.Body = MailBody;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = SmtpServer;
            smtp.Credentials = new NetworkCredential(UserName, Password);
            smtp.EnableSsl = IsSSL;
            smtp.Send(mail);

        }
        #endregion
        #region 去掉单引号
        /// <summary>
        /// 去掉单引号
        /// </summary>
        /// <param name="str">要过滤的字符串</param>
        /// <returns></returns>
        public static string HTMLClear(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Replace("'", "");
            }
            return str;
        }
        #endregion
        #region 去掉Html代码
        /// <summary>
        /// 去掉Html代码
        /// </summary>
        /// <param name="str">要过滤的字符串</param>
        /// <returns></returns>
        public static string HTMLCls(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Replace("'", "");
                str = str.Replace("\"", "&quot;");
                str = str.Replace("%3C", "&lt;");
                str = str.Replace("%3E", "&gt;");
                str = str.Replace("<", "&lt;");
                str = str.Replace(">", "&gt;");
                str = str.Replace("\n", "<br>");
            }
            return str;
        }
        #endregion
        #region 还原Html代码,用于文本框
        /// <summary>
        /// 还原Html代码,用于文本框
        /// </summary>
        /// <param name="str">要过滤的字符串</param>
        /// <returns></returns>
        public static string HTMLToTextarea(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Replace("<br>", "\n");
                str = str.Replace("&nbsp;", " ");
            }
            return str;
        }
        #endregion
        #region 去掉"<"和">"之间HTML代码
        /// <summary>
        /// 去掉&quot;&lt;&quot;和&quot;&gt;&quot;之间HTML代码
        /// </summary>
        /// <param name="str">要过滤的字符串</param>
        /// <returns></returns>
        public static string HTMLRemove(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                Regex re = new Regex(@"<[^>]*>");
                return re.Replace(str, "");
            }
            else
            {
                return "";
            }
        }
        #endregion
        #region 取部分字符
        /// <summary>
        ///  取部分字符,len为0则返回原字符
        /// </summary>
        /// <param name="str">要截取的字符串</param>
        /// <param name="len">截取长度</param>
        /// <returns></returns>
        public static string ShowPartStr(string str, int len)
        {
            if (len == 0)
            {
                return str;
            }
            else
            {
                if (!string.IsNullOrEmpty(str))
                {
                    int t = 0;
                    int n = 0;
                    string strTemp = "";
                    foreach (char c in str)
                    {
                        n++;
                        if (Convert.ToInt32(c) < 0 || Convert.ToInt32(c) > 255)
                        {
                            t = t + 2;
                        }
                        else
                        {
                            t = t + 1;
                        }
                        if (t > len)
                        {
                            strTemp = str.Substring(0, n - 1) + "...";
                            break;
                        }
                        else
                        {
                            strTemp = str;
                        }
                    }
                    return strTemp;
                }
                else
                {
                    return "";
                }
            }
        }
        #endregion
        #region 根据条件输出字符
        /// <summary>
        /// 根据条件输出字符,strParA与strParB相等,则返回str1,否则返回str2
        /// </summary>
        /// <param name="strParA">要比较的参数</param>
        /// <param name="strParB">要比较的参数</param>
        /// <param name="str1">要返回的字符</param>
        /// <param name="str2">要返回的字符</param>
        /// <returns></returns>
        public static string ShowStr(string strParA, string strParB, string str1, string str2)
        {
            if (strParA == strParB)
            {
                return str1;
            }
            else
            {
                return str2;
            }
        }
        #endregion
        #region 弹出信息跳转到指定地址
        /// <summary>
        /// 弹出信息跳转到指定地址
        /// </summary>
        /// <param name="Msg">要弹出的信息</param>
        /// <param name="Link">要跳转的链接地址</param>
        public static void MsgGotoUrl(string Msg, string Link)
        {
            http.Current.Response.Write("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\n");
            http.Current.Response.Write("<html xmlns=\"http://www.w3.org/1999/xhtml\">\n");
            http.Current.Response.Write("<head>\n");
            http.Current.Response.Write("<script src=\"/App_Themes/artDialog/artDialog-4.1.6.js?skin=default\" type=\"text/javascript\"></script>\n");
            http.Current.Response.Write("<script src=\"/App_Themes/artDialog/artDialog-4.1.6.iframe.plugins.js\" type=\"text/javascript\"></script>\n");
            http.Current.Response.Write("</head>\n");
            http.Current.Response.Write("<body>\n");
            http.Current.Response.Write("<script  type=\"text/javascript\">art.dialog({title:'提示',icon:'succeed',content:'" + Msg + "',lock:true,ok:function(){location.href='" + Link + "';}});</script>\n");
            http.Current.Response.Write("</body>\n");
            http.Current.Response.Write("</html>\n");
            http.Current.Response.End();
        }
        #endregion
        #region 弹出信息刷新父级页面
        /// <summary>
        /// 弹出信息刷新父级页面
        /// </summary>
        /// <param name="Msg">要弹出的信息</param>
        public static void MsgReload(string Msg)
        {
            http.Current.Response.Write("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\n");
            http.Current.Response.Write("<html xmlns=\"http://www.w3.org/1999/xhtml\">\n");
            http.Current.Response.Write("<head>\n");
            http.Current.Response.Write("<script src=\"/App_Themes/artDialog/artDialog-4.1.6.js?skin=default\" type=\"text/javascript\"></script>\n");
            http.Current.Response.Write("<script src=\"/App_Themes/artDialog/artDialog-4.1.6.iframe.plugins.js\" type=\"text/javascript\"></script>\n");
            http.Current.Response.Write("</head>\n");
            http.Current.Response.Write("<body>\n");
            http.Current.Response.Write("<script  type=\"text/javascript\">art.dialog({title:'提示',icon:'succeed',content:'" + Msg + "',lock:true,ok:function(){parent.location.reload();}});</script>\n");
            http.Current.Response.Write("</body>\n");
            http.Current.Response.Write("</html>\n");
            http.Current.Response.End();
        }
        #endregion
        #region 弹出信息并返回上一页
        /// <summary>
        /// 弹出信息并返回上一页
        /// </summary>
        /// <param name="Msg">要弹出的信息</param>
        public static void MsgGoBack(string Msg)
        {
            http.Current.Response.Write("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\n");
            http.Current.Response.Write("<html xmlns=\"http://www.w3.org/1999/xhtml\">\n");
            http.Current.Response.Write("<head>\n");
            http.Current.Response.Write("<script src=\"/App_Themes/artDialog/artDialog-4.1.6.js?skin=default\" type=\"text/javascript\"></script>\n");
            http.Current.Response.Write("<script src=\"/App_Themes/artDialog/artDialog-4.1.6.iframe.plugins.js\" type=\"text/javascript\"></script>\n");
            http.Current.Response.Write("</head>\n");
            http.Current.Response.Write("<body>\n");
            http.Current.Response.Write("<script  type=\"text/javascript\">art.dialog({title:'提示',icon:'warning',content:'" + Msg + "',lock:true,ok:function(){history.go(-1);}});</script>\n");
            http.Current.Response.Write("</body>\n");
            http.Current.Response.Write("</html>\n");
            http.Current.Response.End();
        }
        #endregion
        #region 检查是否为日期
        /// <summary>
        /// 检查是否为日期
        /// </summary>
        /// <param name="str">要检查的字符串</param>
        /// <returns></returns>
        public static bool IsDate(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                Regex re = new Regex(@"^((\d{2}(([02468][048])|([13579][26]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|([1-2][0-9])))))|(\d{2}(([02468][1235679])|([13579][01345789]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|(1[0-9])|(2[0-8]))))))(\s(((0?[0-9])|([1-2][0-3]))\:([0-5]?[0-9])((\s)|(\:([0-5]?[0-9])))))?$");

                if (!re.IsMatch(str))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region 检查是否为数字
        /// <summary>
        /// 检查是否为数字
        /// </summary>
        /// <param name="str">被检查的字符串</param>
        /// <returns>返回true或false</returns>
        public static bool IsNumeric(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                Regex re = new Regex(@"^(-?\d+)(\.\d+)?$");
                if (!re.IsMatch(str))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region 检查是否为邮件地址
        /// <summary>
        /// 检查是否为邮件地址
        /// </summary>
        /// <param name="str">要检查的字符串</param>
        /// <returns></returns>
        public static bool IsMail(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                Regex re = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");

                if (!re.IsMatch(str))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region 检查是否为联系电话
        /// <summary>
        /// 检查是否为联系电话
        /// </summary>
        /// <param name="str">要检查的字符串</param>
        /// <returns></returns>
        public static bool IsTel(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                Regex re = new Regex(@"((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)");

                if (!re.IsMatch(str))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region 设置DropDownList或RadioButtonList或ListBox或CheckBoxList默认选定项
        /// <summary>
        /// 设置DropDownList默认选定项
        /// </summary>
        /// <param name="drp">DropDownList对象名</param>
        /// <param name="val">要比较的值</param>
        public static void setDefaultSelected(DropDownList drp, string val)
        {
            foreach (ListItem lt in drp.Items)
            {
                if (lt.Value == val)
                {
                    lt.Selected = true;
                }
                else
                {
                    lt.Selected = false;
                }
            }
        }
        /// <summary>
        /// 设置RadioButtonList默认选定项
        /// </summary>
        /// <param name="rad">RadioButtonList对象名</param>
        /// <param name="val">要比较的值</param>
        public static void setDefaultSelected(RadioButtonList rad, string val)
        {
            foreach (ListItem lt in rad.Items)
            {
                if (lt.Value == val)
                {
                    lt.Selected = true;
                    break;
                }
            }
        }
        /// <summary>
        /// 设置ListBox默认选定项
        /// </summary>
        /// <param name="lb">ListBox对象名</param>
        /// <param name="val">要比较的值</param>
        public static void setDefaultSelected(ListBox lb, string val)
        {
            foreach (ListItem lt in lb.Items)
            {
                if (lt.Value == val)
                {
                    lt.Selected = true;
                }
            }
        }
        /// <summary>
        /// 设置CheckBoxList默认选定项
        /// </summary>
        /// <param name="chk">CheckBoxList对象名</param>
        /// <param name="val">要比较的值</param>
        public static void setDefaultSelected(CheckBoxList chk, string val)
        {
            foreach (ListItem lt in chk.Items)
            {
                if (lt.Value == val)
                {
                    lt.Selected = true;
                }
            }
        }
        #endregion
        #region 下拉列表中是否存在
        /// <summary>
        /// 下拉列表中是否存在
        /// </summary>
        /// <param name="drp">DropDownList对象名</param>
        /// <param name="val">要比较的值</param>
        /// <returns></returns>
        public static bool DropdownListIsExistItem(DropDownList drp, string val)
        {
            bool tempBool = false;
            foreach (ListItem lt in drp.Items)
            {
                if (lt.Value == val)
                {
                    tempBool = true;
                    break;
                }
            }
            return tempBool;
        }
        #endregion
        #region 显示信息并终止程序
        /// <summary>
        /// 显示信息并终止程序
        /// </summary>
        /// <param name="str">显示的信息</param>
        public static void ShowEnd(string str)
        {

            http.Current.Response.Write(str);
            http.Current.Response.End();
        }
        #endregion
        #region md5加密
        /// <summary>
        /// md5加密
        /// </summary>
        /// <param name="str">要加密码的字符串</param>
        /// <returns></returns>
        public static string md5(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "md5");
            }
            else
            {
                return "";
            }
        }
        #endregion
        #region SHA1加密
        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <param name="str">要加密码的字符串</param>
        /// <returns></returns>
        public static string sha1(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "sha1");
            }
            else
            {
                return "";
            }
        }
        #endregion
        #region 订单号
        /// <summary>
        /// 取订单号,日期+随机码
        /// </summary>
        /// <returns></returns>
        public static string GetOrderNo()
        {
            Random rad = new Random();
            int radno = rad.Next(1000, 9999);
            return DateTime.Now.ToString("yyyyMMddHHmmss") + radno.ToString();
        }
        #endregion
        #region 小于10的数字前面加字符0
        /// <summary>
        /// 小于10的数字前面加字符0
        /// </summary>
        /// <param name="n">要检查的数字</param>
        /// <returns></returns>
        public static string ShowZero(int n)
        {
            if (n < 10)
                return "0" + n.ToString();
            else
                return n.ToString();
        }
        #endregion
        #region 显示flash或图片
        /// <summary>
        /// 显示flash或图片
        /// </summary>
        /// <param name="path">flash或图片的路径</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns></returns>
        public static string Flash(string path, string width, string height)
        {
            StringBuilder fstr = new StringBuilder("");
            string FileExt = path.Substring((path.Length - 3), 3);
            if (FileExt.ToLower() == "swf")
            {
                fstr.Append("<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0\" width=\"" + width + "\" height=\"" + height + "\">\n");
                fstr.Append("<param name=\"movie\" value=\"" + path + "\">\n");
                fstr.Append("<param name=\"quality\" value=\"high\">\n");
                fstr.Append("<param name=\"wmode\" value=\"transparent\">\n");
                fstr.Append("<embed src=\"" + path + "\" wmode=\"transparent\" quality=\"high\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" type=\"application/x-shockwave-flash\" width=\"" + width + "\" height=\"" + height + "\"></embed>\n");
                fstr.Append("</object>\n");
            }
            else
            {
                fstr.Append("<img src=\"" + path + "\" style=\"border;0px;\" width=\"" + width + "\" height=\"" + height + "\"/>");
            }
            return fstr.ToString();
        }
        #endregion
        #region 直接下载文件
        /// <summary>
        /// 直接下载文件
        /// </summary>
        /// <param name="strFilePath">要下载的文件路径</param>
        /// <param name="strFileName">客户端保存的文件名</param>
        public static void DirectDownFile(string strFilePath, string strFileName)
        {
            FileInfo fileInfo = new FileInfo(strFilePath);

            //清除不必要的头,基类中可能乱添加的东西!
            http.Current.Response.Clear();
            http.Current.Response.ClearContent();
            http.Current.Response.ClearHeaders();

            // HttpUtility.UrlEncode(filename) 是使得文件名正确显示
            http.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(strFileName));
            http.Current.Response.AddHeader("Content-Length", fileInfo.Length.ToString());

            //  http.Current.Response.ContentType 的种类有很多!
            http.Current.Response.ContentType = "application/octet-stream";
            http.Current.Response.WriteFile(fileInfo.FullName);

            //这样就可以抛出下载了!
            http.Current.Response.Flush();
            http.Current.Response.End();
        }
        #endregion
        #region 缓冲下载文件
        /// <summary>
        /// 缓冲下载文件
        /// </summary>
        /// <param name="strFilePath">要下载的文件路径</param>
        /// <param name="strFileName">客户端保存的文件名</param>
        public static void FlushDownFile(string strFilePath, string strFileName)
        {
            FileInfo fileInfo = new FileInfo(strFilePath);
            if (fileInfo.Exists == true)
            {
                const long ChunkSize = 102400;//100K 每次读取文件，只读取100K，这样可以缓解服务器的压力
                byte[] buffer = new byte[ChunkSize];
                FileStream iStream = File.OpenRead(strFilePath);
                long dataLengthToRead = iStream.Length;//获取下载的文件总大小

                //这几句话是必须要的,如果没有,基类中可能有方法向里面乱添加东西!!!
                http.Current.Response.Clear();
                http.Current.Response.ClearContent();
                http.Current.Response.ClearHeaders();
                //同样,这里没有设置编码,因为读取的文件中设置了gb2312,这里就没有必要了!

                // Response.ContentType 的种类有很多!
                http.Current.Response.ContentType = "application/octet-stream";
                // HttpUtility.UrlEncode(filename) 是使得文件名正确显示
                http.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(strFileName));

                while (dataLengthToRead > 0 && http.Current.Response.IsClientConnected)
                {
                    int lengthRead = iStream.Read(buffer, 0, Convert.ToInt32(ChunkSize));//读取的大小
                    http.Current.Response.OutputStream.Write(buffer, 0, lengthRead);
                    http.Current.Response.Flush();
                    dataLengthToRead = dataLengthToRead - lengthRead;
                }
                http.Current.Response.Close();
                http.Current.Response.End();
            }
        }
        #endregion
        #region 下载文件
        /// <summary>
        /// 输出硬盘文件，提供下载 支持大文件、续传、速度限制、资源占用小
        /// </summary>
        /// <param name="http.Current.Request">Page.Request对象</param>
        /// <param name="http.Current.Response">Page.Response对象</param>
        /// <param name="strFileName">下载文件名</param>
        /// <param name="strFilePath">带文件名下载路径</param>
        /// <param name="_speed">每秒允许下载的字节数</param>
        /// <returns>返回是否成功</returns>
        public static void ResponseFile(string strFilePath, string strFileName)
        {
            try
            {
                FileStream myFile = new FileStream(strFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(myFile);
                try
                {
                    http.Current.Response.AddHeader("Accept-Ranges", "bytes");
                    http.Current.Response.Buffer = false;
                    long fileLength = myFile.Length;
                    long startBytes = 0;

                    int pack = 10240; //10K bytes
                    //int sleep = 200;   //每秒5次   即5*10K bytes每秒
                    int sleep = (int)Math.Floor((decimal)1000 * pack / 102400) + 1;
                    if (http.Current.Request.Headers["Range"] != null)
                    {
                        http.Current.Response.StatusCode = 206;
                        string[] range = http.Current.Request.Headers["Range"].Split(new char[] { '=', '-' });
                        startBytes = Convert.ToInt64(range[1]);
                    }
                    http.Current.Response.AddHeader("Content-Length", (fileLength - startBytes).ToString());
                    if (startBytes != 0)
                    {
                        http.Current.Response.AddHeader("Content-Range", string.Format(" bytes {0}-{1}/{2}", startBytes, fileLength - 1, fileLength));
                    }
                    http.Current.Response.AddHeader("Connection", "Keep-Alive");
                    http.Current.Response.ContentType = "application/octet-stream";
                    http.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(strFileName, Encoding.UTF8));

                    br.BaseStream.Seek(startBytes, SeekOrigin.Begin);
                    int maxCount = (int)Math.Floor((decimal)(fileLength - startBytes) / pack) + 1;

                    for (int i = 0; i < maxCount; i++)
                    {
                        if (http.Current.Response.IsClientConnected)
                        {
                            http.Current.Response.BinaryWrite(br.ReadBytes(pack));
                            Thread.Sleep(sleep);
                        }
                        else
                        {
                            i = maxCount;
                        }
                    }
                }
                catch
                {
                }
                finally
                {
                    br.Close();
                    myFile.Close();
                }
            }
            catch
            {
            }
        }

        #endregion
        #region 下拉年份列表
        /// <summary>
        /// 下拉年份列表
        /// </summary>
        /// <param name="drp">DropDownList对象名</param>
        /// <param name="intBase">开始年份</param>
        public static void DropDownList_Year(DropDownList drp, int intBase)
        {
            for (int i = intBase; i <= DateTime.Now.Year; i++)
            {
                drp.Items.Add(new ListItem(i.ToString() + "年", i.ToString()));
            }
        }
        #endregion
        #region 下拉月份列表
        /// <summary>
        /// 下拉月份列表
        /// </summary>
        /// <param name="drp">DropDownList对象名</param>
        public static void DropDownList_Month(DropDownList drp)
        {
            for (int i = 1; i <= 12; i++)
            {
                drp.Items.Add(new ListItem(i.ToString() + "月", i.ToString()));
            }
        }
        #endregion
        #region 下拉日列表
        /// <summary>
        /// 下拉日列表
        /// </summary>
        /// <param name="drp">DropDownList对象名</param>
        public static void DropDownList_Day(DropDownList drp)
        {
            for (int i = 1; i <= 31; i++)
            {
                drp.Items.Add(new ListItem(i.ToString() + "日", i.ToString()));
            }
        }
        #endregion
        #region 下拉小时列表
        /// <summary>
        /// 下拉小时列表
        /// </summary>
        /// <param name="drp">DropDownList对象名</param>
        public static void DropDownList_Hour(DropDownList drp)
        {
            for (int i = 0; i <= 23; i++)
            {
                drp.Items.Add(new ListItem(i.ToString() + "时", i.ToString()));
            }
        }
        #endregion
        #region 下拉分钟列表
        /// <summary>
        /// 下拉分钟列表
        /// </summary>
        /// <param name="drp">DropDownList对象名</param>
        public static void DropDownList_Minute(DropDownList drp)
        {
            for (int i = 0; i <= 59; i++)
            {
                drp.Items.Add(new ListItem(ShowZero(i) + "分", ShowZero(i)));
            }
        }
        #endregion
        #region 下拉秒钟列表
        /// <summary>
        /// 下拉秒钟列表
        /// </summary>
        /// <param name="drp">DropDownList对象名</param>
        public static void DropDownList_Second(DropDownList drp)
        {
            for (int i = 0; i <= 59; i++)
            {
                drp.Items.Add(new ListItem(ShowZero(i) + "秒", ShowZero(i)));
            }
        }
        #endregion
        #region 字符加密
        /// <summary>
        /// 字符加密
        /// </summary>
        /// <param name="str">要加密的字符</param>
        /// <returns></returns>
        public static string Encrypt(string str)
        {
            return ConnectionInfo.EncryptDBConnectionString(str);
        }
        #endregion
        #region 字符解密
        /// <summary>
        /// 字符解密
        /// </summary>
        /// <param name="str">要解密的字符</param>
        /// <returns></returns>
        public static string Decrypt(string str)
        {
            return ConnectionInfo.DecryptDBConnectionString(str);
        }
        #endregion
        #region 文件及文件夹操作
        /// <summary>
        /// 检查文件是否存在,如不存在,则创建文件夹
        /// </summary>
        /// <param name="strFolderPath">文件夹路径</param>
        public static void CheckFolder(string strFolderPath)
        {
            if (!Directory.Exists(strFolderPath))
            {
                Directory.CreateDirectory(strFolderPath);
            }
        }
        /// <summary>
        /// 替换文件夹名中的特殊字符
        /// </summary>
        /// <param name="strFolderName">要检查的文件夹名</param>
        public static string FolderNameReplace(string strFolderName)
        {
            if (strFolderName != string.Empty)
            {
                strFolderName = strFolderName.Replace(",", "_");
                strFolderName = strFolderName.Replace(";", "_");
                strFolderName = strFolderName.Replace(".", "_");
                strFolderName = strFolderName.Replace("//", "/");
            }
            return strFolderName;
        }
        /// <summary>
        /// 替换文件名中的特殊字符
        /// </summary>
        /// <param name="strFileName">要检查的文件名</param>
        public static string FileNameReplace(string strFileName)
        {
            if (strFileName != string.Empty)
            {
                strFileName = strFileName.Replace(",", "_");
                strFileName = strFileName.Replace(";", "_");
            }
            return strFileName;
        }
        #endregion
        #region 通过文件名的后缀来判断文件是否是图片
        /// <summary>
        /// 通过文件名的后缀来判断文件是否是图片
        /// </summary>
        /// <param name="strFileName">要检查的文件名</param>
        /// <returns></returns>
        public static bool IsPicture(string strFileName)
        {
            if (strFileName.IndexOf(".jpg") > -1 || strFileName.IndexOf(".gif") > -1 || strFileName.IndexOf(".bmp") > -1 || strFileName.IndexOf(".jpeg") > -1 || strFileName.IndexOf(".png") > -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 通过文件名的后缀来判断文件是否是图片
        /// </summary>
        /// <param name="strFileExt">要检查的后缀</param>
        /// <returns></returns>
        public static bool IsPicture2(string strFileExt)
        {
            if (strFileExt == ".jpg" || strFileExt == ".gif" || strFileExt == ".bmp" || strFileExt == ".jpeg" || strFileExt == ".png")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 通过文件名的后缀来判断文件是否是图片(不包括gif图片)
        /// </summary>
        /// <param name="strFileExt">要检查的后缀</param>
        /// <returns></returns>
        public static bool IsPicture3(string strFileExt)
        {
            if (strFileExt == ".jpg" || strFileExt == ".bmp" || strFileExt == ".jpeg" || strFileExt == ".png")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region 文件夹里是否有文件夹和文件
        /// <summary>
        /// 文件夹里是否有文件夹和文件
        /// </summary>
        /// <param name="strFolderPath">要检查的文件夹</param>
        /// <returns></returns>
        public static int FolderAndFileTotal(string strFolderPath)
        {
            if (Directory.Exists(strFolderPath))
            {
                DirectoryInfo dir = new DirectoryInfo(strFolderPath);
                FileSystemInfo[] arrDir = dir.GetFileSystemInfos();
                return arrDir.Length;
            }
            else
            {
                return 0;
            }
        }
        #endregion
        #region 收集错误信息
        /// <summary>
        /// 收集错误信息
        /// </summary>
        /// <param name="ex">Exception对象名</param>
        public static void Err(Exception ex)
        {
            try
            {
                CheckFolder(http.Current.Server.MapPath("/error/"));
                string path = http.Current.Server.MapPath("/error/err_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
                if (!File.Exists(path))
                {
                    FileStream f = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
                    f.Close();
                }
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine("时间:" + DateTime.Now.ToString());
                    sw.WriteLine("IP:" + http.Current.Request.UserHostAddress);
                    sw.WriteLine("错误描述:" + ex.Message);
                    sw.WriteLine("引发错误的实例:" + ex.InnerException);
                    sw.WriteLine("堆栈描述:\n" + ex.StackTrace);
                    sw.WriteLine("错误源:" + ex.Source);
                    sw.WriteLine("引发错误的方法:" + ex.TargetSite);
                    sw.WriteLine("引发错误的路径:" + http.Current.Request.Url.ToString());
                    sw.WriteLine("页面来源:" + http.Current.Request.UrlReferrer.ToString());
                    sw.WriteLine("---------------------------------------------------------------------------------------------------------------");
                }
            }
            catch
            { }
        }
        #endregion
        #region 关键字、描述
        /// <summary>
        /// 网站关键字
        /// </summary>
        /// <param name="strKeywords">网页要设置的关键字</param>
        /// <returns></returns>
        public static HtmlMeta SetKeywords(string strKeywords)
        {
            HtmlMeta metaKey = new HtmlMeta();
            metaKey.ID = "keywords";
            metaKey.Name = "keywords";
            metaKey.Content = strKeywords;
            return metaKey;
        }
        /// <summary>
        /// 网站描述
        /// </summary>
        /// <param name="strDescription">网页要设置的描述</param>
        /// <returns></returns>
        public static HtmlMeta SetDescription(string strDescription)
        {
            HtmlMeta metaDes = new HtmlMeta();
            metaDes.ID = "description";
            metaDes.Name = "description";
            metaDes.Content = strDescription;
            return metaDes;
        }
        #endregion
        #region 在线客服
        /// <summary>
        /// 在线客服
        /// </summary>
        /// <param name="strTypeID">在线客服类型</param>
        /// <param name="strNickName">昵称</param>
        /// <param name="strAccount">帐号</param>
        /// <param name="strChatKey">key值</param>
        /// <returns></returns>
        public static string ShowChatAccount(string strTypeID, string strNickName, string strAccount, string strChatKey)
        {
            switch (strTypeID)
            {
                case "1":
                    if (strChatKey == string.Empty)
                    {
                        return "<a target=\"_blank\" href=\"http://wpa.qq.com/msgrd?v=3&uin=" + strAccount + "&site=qq&menu=yes\"><img border=\"0\" src=\"http://wpa.qq.com/pa?p=2:" + strAccount + ":4\" alt=\"点击这里给我发消息\" title=\"点击这里给我发消息\">" + strNickName + "</a>";
                    }
                    else
                    {
                        return "<a target=\"_blank\" href=\"http://sighttp.qq.com/authd?IDKEY=" + strChatKey + "\"><img border=\"0\"  src=\"http://wpa.qq.com/imgd?IDKEY=" + strChatKey + "&pic=4\" alt=\"点击这里给我发消息\" title=\"点击这里给我发消息\">" + strNickName + "</a>";
                    }
                case "2":
                    if (strChatKey == string.Empty)
                    {
                        return "<a href=\"msnim:chat?contact=" + strAccount + "\"><img border=\"0\" src=\"/App_Themes/Images/im_msn.jpg\" alt=\"点击这里给我发消息\" title=\"点击这里给我发消息\">" + strNickName + "</a>";
                    }
                    else
                    {
                        return "<a target=\"_blank\" href=\"http://settings.messenger.live.com/Conversation/IMMe.aspx?invitee=" + strChatKey + "@apps.messenger.live.com&amp;mkt=zh-cn\"><img  border=\"0\" src=\"http://messenger.services.live.com/users/" + strChatKey + "@apps.messenger.live.com/presenceimage?mkt=zh-cn\" alt=\"点击这里给我发消息\" title=\"点击这里给我发消息\"/>" + strNickName + "</a>";
                    }
                case "3":
                    return "<a target=\"blank\" href=\"callto://" + strAccount + "/\"><img  border=\"0\" src=\"/App_Themes/Images/im_skype.jpg\"  alt=\"点击这里给我发消息\" title=\"点击这里给我发消息\"/>" + strNickName + "</a>";
                case "4":
                    return "<a target=\"blank\" href=\"http://amos1.taobao.com/msg.ww?v=2&amp;uid=" + strAccount + "&amp;s=2\"><img  border=\"0\" src=\"http://amos1.taobao.com/online.ww?v=2&amp;uid=" + strAccount + "&amp;s=2\"  alt=\"点击这里给我发消息\" title=\"点击这里给我发消息\"/>" + strNickName + "</a>";
                default:
                    return "";
            }
        }
        #endregion
        #region Request
        /// <summary>
        /// 过滤POST或GET方式提交的值
        /// </summary>
        /// <param name="obj">Request.Form或Request.QueryString对象</param>
        /// <returns></returns>
        public static string Request(object obj, string strDefaultVal)
        {
            if (obj == null)
            {
                return strDefaultVal;
            }
            else
            {
                string strTemp = obj.ToString();
                if (String.IsNullOrEmpty(strTemp))
                {
                    strTemp = strDefaultVal;
                }
                return strTemp;
            }
        }
        #endregion
        #region Request Numeric
        /// <summary>
        /// 过滤POST或GET方式提交的值是否数字
        /// </summary>
        /// <param name="obj">Request.Form或Request.QueryString对象</param>
        public static int RequestNumeric(object obj, int intDefaultVal)
        {
            if (obj == null)
            {
                return intDefaultVal;
            }
            else
            {
                string strTemp = obj.ToString();
                if (!IsNumeric(strTemp))
                {
                    return intDefaultVal;
                }
                else
                {
                    return int.Parse(strTemp);
                }
            }
        }
        #endregion
        #region 字符编码转换
        public static string GB2312ToUtf8(string gb2312String)
        {
            Encoding fromEncoding = Encoding.GetEncoding("gb2312");
            Encoding toEncoding = Encoding.UTF8;
            return EncodingConvert(gb2312String, fromEncoding, toEncoding);
        }

        public static string Utf8ToGB2312(string utf8String)
        {
            Encoding fromEncoding = Encoding.UTF8;
            Encoding toEncoding = Encoding.GetEncoding("gb2312");
            return EncodingConvert(utf8String, fromEncoding, toEncoding);
        }

        public static string EncodingConvert(string fromString, Encoding fromEncoding, Encoding toEncoding)
        {
            byte[] fromBytes = fromEncoding.GetBytes(fromString);
            byte[] toBytes = Encoding.Convert(fromEncoding, toEncoding, fromBytes);

            string toString = toEncoding.GetString(toBytes);
            return toString;
        }
        #endregion
        #region 取天气预报
        public static string GetWeather()
        {
            StringBuilder strReturn = new StringBuilder();
            try
            {
                //取IP
                string strIP = HttpContext.Current.Request.UserHostAddress.ToString();

                //根据IP取城市
                WebRequest web = WebRequest.Create("http://int.dpool.sina.com.cn/iplookup/iplookup.php?format=json&ip=" + strIP);
                web.Timeout = 20000;
                HttpWebResponse http = (HttpWebResponse)web.GetResponse();
                Stream s = http.GetResponseStream();
                StreamReader sr = new StreamReader(s, Encoding.Default);
                Dictionary<string, object> strCityJson = (Dictionary<string, object>)JsonConvert.DeserializeObject(sr.ReadToEnd(), typeof(Dictionary<string, object>));
                object val;
                strCityJson.TryGetValue("city", out val);
                string strTempCity = "深圳";
                if (val != null)
                {
                    strTempCity = val.ToString();
                }
                string strCity = GB2312ToUtf8(strTempCity.ToString());

                //根据城市取天气
                MSXML2.XMLHTTP xmlhttp = new MSXML2.XMLHTTP();
                xmlhttp.open("GET", "http://php.weather.sina.com.cn/xml.php?city=" + strCity + "&password=DJOYnieT8234jlsK&day=0", false);
                xmlhttp.send();

                if (xmlhttp.readyState == 4)
                {
                    if (xmlhttp.status == 200)
                    {
                        byte[] buff = (byte[])xmlhttp.responseBody;
                        MemoryStream stream = new MemoryStream(buff);
                        XmlDocument xml = new XmlDocument();
                        xml.Load(stream);
                        XmlNode nodeDate = xml.SelectSingleNode("/Profiles/Weather/savedate_weather");
                        strReturn.Append(" " + nodeDate.InnerText);
                        XmlNode nodeCity = xml.SelectSingleNode("/Profiles/Weather/city");
                        strReturn.Append(" " + nodeCity.InnerText);
                        XmlNode nodeW1 = xml.SelectSingleNode("/Profiles/Weather/status1");
                        XmlNode nodeW2 = xml.SelectSingleNode("/Profiles/Weather/status2");
                        string w1 = nodeW1.InnerText;
                        string w2 = nodeW2.InnerText;
                        if (w1 == w2)
                        {
                            strReturn.Append(" " + w1);
                        }
                        else
                        {
                            strReturn.Append(" " + w1 + "～" + w2);
                        }
                        XmlNode nodeT1 = xml.SelectSingleNode("/Profiles/Weather/temperature1");
                        strReturn.Append(" " + nodeT1.InnerText + "℃");
                        XmlNode nodeT2 = xml.SelectSingleNode("/Profiles/Weather/temperature2");
                        strReturn.Append("～" + nodeT2.InnerText + "℃");
                    }
                }
            }
            catch
            { }
            return strReturn.ToString();
        }
        #endregion
        #region 简繁转换
        /// <summary>
        /// 简繁转换
        /// </summary>
        /// <param name="str"></param>
        /// <param name="convType">B2G:繁转简 G2B:简转繁</param>
        /// <returns></returns>
        public static string StringConvert(string str, string convType)
        {
            if (!string.IsNullOrEmpty(str))
            {
                if (convType == "B2G")
                {
                    return Strings.StrConv(str, VbStrConv.SimplifiedChinese, 0);
                }
                else if (convType == "G2B")
                {
                    return Strings.StrConv(str, VbStrConv.TraditionalChinese, 0);
                }
                else
                {
                    return str;
                }
            }
            else
            {
                return "";
            }

        }
        #endregion
        //---------------------------------------------------------------------调用其他类
        #region 简繁转换选择
        /// <summary>
        /// 简繁转换选择
        /// </summary>
        /// <param name="str">B2G:繁转简 G2B:简转繁</param>
        public static void SelectLanguageVer(string str)
        {
            switch (str)
            {
                case "B2G":
                    http.Current.Response.Filter = new CB2GFilter(http.Current.Response.Filter);
                    break;
                case "G2B":
                    http.Current.Response.Filter = new CG2BFilter(http.Current.Response.Filter);
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}

