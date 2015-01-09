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
    /// ���÷���
    /// </summary>
    public class Config
    {
        #region ϵͳģ��������
        /// <summary>
        /// ��ҳ��
        /// </summary>
        public static string SysSinglePageMouldID
        {
            get
            {
                return ConfigurationManager.AppSettings["SysSinglePageMouldID"].ToString();
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public static string SysArticleMouldID
        {
            get
            {
                return ConfigurationManager.AppSettings["SysArticleMouldID"].ToString();
            }
        }
        /// <summary>
        /// ��Ʒ
        /// </summary>
        public static string SysProductMouldID
        {
            get
            {
                return ConfigurationManager.AppSettings["SysProductMouldID"].ToString();
            }
        }
        /// <summary>
        /// ��Ƹ
        /// </summary>
        public static string SysJobMouldID
        {
            get
            {
                return ConfigurationManager.AppSettings["SysJobMouldID"].ToString();
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public static string SysDownloadMouldID
        {
            get
            {
                return ConfigurationManager.AppSettings["SysDownloadMouldID"].ToString();
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public static string SysSurveyMouldID
        {
            get
            {
                return ConfigurationManager.AppSettings["SysSurveyMouldID"].ToString();
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public static string SysLinkMouldID
        {
            get
            {
                return ConfigurationManager.AppSettings["SysLinkMouldID"].ToString();
            }
        }
        /// <summary>
        /// ��վ��ͼ
        /// </summary>
        public static string SysSitemapMouldID
        {
            get
            {
                return ConfigurationManager.AppSettings["SysSitemapMouldID"].ToString();
            }
        }
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        public static string SysFeedbackMouldID
        {
            get
            {
                return ConfigurationManager.AppSettings["SysFeedbackMouldID"].ToString();
            }
        }
        /// <summary>
        /// ���Ա�
        /// </summary>
        public static string SysGuestbookMouldID
        {
            get
            {
                return ConfigurationManager.AppSettings["SysGuestbookMouldID"].ToString();
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public static string SysOuterLinkMouldID
        {
            get
            {
                return ConfigurationManager.AppSettings["SysOuterLinkMouldID"].ToString();
            }
        }

        /// <summary>
        /// ��Ƶ����
        /// </summary>
        public static string SysVideoMouldID
        {
            get
            {
                return ConfigurationManager.AppSettings["SysVideoMouldID"].ToString();
            }
        }

        /// <summary>
        /// ������
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
        /// ���Է���
        /// </summary>
        public static string SysMessageDictionaryMouldID
        {
            get
            {
                return ConfigurationManager.AppSettings["SysMessageDictionaryMouldID"].ToString();
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public static string SysFeedbackDictionaryMouldID
        {
            get
            {
                return ConfigurationManager.AppSettings["SysFeedbackDictionaryMouldID"].ToString();
            }
        }
        /// <summary>
        /// ��ԱȨ���ֶ�
        /// </summary>
        public static string SysUserLimitMouldID
        {
            get
            {
                return ConfigurationManager.AppSettings["SysUserLimitMouldID"].ToString();
            }
        }
        #endregion
        #region ��Ŀ���
          public static string SysHelp
        {
            get
            {
                return ConfigurationManager.AppSettings["SysHelp"].ToString();
            }
        }
        #endregion
        #region �����ֵ�������
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        public static string FeedbackDictionaryMouldID
        {
            get
            {
                return ConfigurationManager.AppSettings["FeedbackDictionaryMouldID"].ToString();
            }
        }
        /// <summary>
        /// ������Ƹ
        /// </summary>
        public static string JobDictionaryMouldID
        {
            get
            {
                return ConfigurationManager.AppSettings["JobDictionaryMouldID"].ToString();
            }
        }
        /// <summary>
        /// ��Ʒ����
        /// </summary>
        public static string ProductDictionaryMouldID
        {
            get
            {
                return ConfigurationManager.AppSettings["ProductDictionaryMouldID"].ToString();
            }
        }
        /// <summary>
        /// ��Ʒ����
        /// </summary>
        public static string ProductTypeID
        {
            get
            {
                return ConfigurationManager.AppSettings["ProductTypeID"].ToString();
            }
        }
        #endregion
        #region ��վ����
        /// <summary>
        /// Ĭ��վ��Ŀ¼
        /// </summary>
        public static string DefaultSiteDir
        {
            get
            {
                return ConfigurationManager.AppSettings["DefaultSiteDir"].ToString();
            }
        }
        /// <summary>
        /// α��̬�ļ���׺
        /// </summary>
        public static string FileExt
        {
            get
            {
                return ConfigurationManager.AppSettings["FileExt"].ToString();
            }
        }
        /// <summary>
        /// ϵͳ����
        /// </summary>
        public static string SystemName
        {
            get
            {
                return ConfigurationManager.AppSettings["SystemName"].ToString();
            }
        }
        /// <summary>
        /// ��Ȩ
        /// </summary>
        public static string Authorized
        {
            get
            {
                return ConfigurationManager.AppSettings["Authorized"].ToString();
            }
        }
        /// <summary>
        /// ��Ȩ����
        /// </summary>
        public static string Copyright
        {
            get
            {
                return ConfigurationManager.AppSettings["Copyright"].ToString();
            }
        }
        #endregion
        #region ��̨����
        /// <summary>
        /// ��̨Ŀ¼·��
        /// </summary>
        public static string AdminPath
        {
            get
            {
                return ConfigurationManager.AppSettings["AdminPath"].ToString();
            }
        }
        /// <summary>
        /// ��������ԱID
        /// </summary>
        public static string SystemAdminID
        {
            get
            {
                return "1";
            }
        }
        /// <summary>
        /// �ļ��ϴ�Ŀ¼·��
        /// </summary>
        public static string FileUploadPath
        {
            get
            {
                return ConfigurationManager.AppSettings["FileUploadPath"].ToString();
            }
        }
        #endregion

        #region ���ݿ�����
        #region ���ݿ������ַ���
        /// <summary>
        /// ���ݿ������ַ��Ƿ������,���SQL���ݿ�,0-������ʽ,1-���ܷ�ʽ
        /// </summary>
        public static string IsEncrypt
        {
            get { return ConfigurationManager.AppSettings["IsEncrypt"].ToString(); }
        }
        /// <summary>
        /// ���ݿ�����,Sql(sql2000/2005/2008),OleDb(Access2000/2003/2007),MySql,Oracle
        /// </summary>
        public static string DatabaseType
        {
            get { return ConfigurationManager.AppSettings["DatabaseType"].ToString(); }
        }
        /// <summary>
        /// Sql���ݿ������ַ���
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
        /// Access���ݿ������ַ���
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
        /// Excel���ݿ������ַ���
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
        /// �������ݿ������ַ���
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
        /// MySql���ݿ������ַ���
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
        #region ���ݿ�ӿ�ʵ����
        /// <summary>
        /// ���ݿ�ӿ�ʵ����
        /// </summary>
        /// <returns></returns>
        private static DbHelper _dbhelper;
        /// <summary>
        /// ���ݿ�ӿ�ʵ����
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
        #region �������ͼ���,MySql, OleDb, Oracle, Sql
        /// <summary>
        /// �������ͼ���
        /// </summary>
        public enum DatabaseTypeCollection
        {
            MySql, OleDb, Oracle, Sql
        }
        #endregion
        #region ���ݰ󶨿ؼ����ͼ���
        /// <summary>
        /// ���ݰ󶨿ؼ����ͼ���,CheckBoxList, DropDownList, ListBox, RadioButtonList, Repeater, DataList, DataGrid, GridView, DetailsView, FormView
        /// </summary>
        public enum DataBindObjTypeCollection
        {
            CheckBoxList, DropDownList, ListBox, RadioButtonList, Repeater, DataList, DataGrid, GridView, DetailsView, FormView
        }
        #endregion
        #endregion

        #region ʹ��System.Net.Mail�����ʼ�
        /// <summary>
        /// ʹ��System.Net.Mail�����ʼ�
        /// </summary>
        /// <param name="SmtpServer">SMTP������</param>
        /// <param name="UserName">�û���</param>
        /// <param name="Password">����</param>
        /// <param name="ReceiveAddress">���ŵ�ַ</param>
        /// <param name="CcAddress">���͵�ַ</param>
        /// <param name="BccAddress">���͵�ַ</param>
        /// <param name="Subject">�ʼ�����</param>
        /// <param name="MailBody">�ʼ�����</param>
        /// <param name="Attachment">������ַ</param>
        /// <param name="IsHTML">�Ƿ���HTML�ʼ�</param>
        /// <param name="IsSSL">�Ƿ���Ҫ��������֤</param>
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
        #region ȥ��������
        /// <summary>
        /// ȥ��������
        /// </summary>
        /// <param name="str">Ҫ���˵��ַ���</param>
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
        #region ȥ��Html����
        /// <summary>
        /// ȥ��Html����
        /// </summary>
        /// <param name="str">Ҫ���˵��ַ���</param>
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
        #region ��ԭHtml����,�����ı���
        /// <summary>
        /// ��ԭHtml����,�����ı���
        /// </summary>
        /// <param name="str">Ҫ���˵��ַ���</param>
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
        #region ȥ��"<"��">"֮��HTML����
        /// <summary>
        /// ȥ��&quot;&lt;&quot;��&quot;&gt;&quot;֮��HTML����
        /// </summary>
        /// <param name="str">Ҫ���˵��ַ���</param>
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
        #region ȡ�����ַ�
        /// <summary>
        ///  ȡ�����ַ�,lenΪ0�򷵻�ԭ�ַ�
        /// </summary>
        /// <param name="str">Ҫ��ȡ���ַ���</param>
        /// <param name="len">��ȡ����</param>
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
        #region ������������ַ�
        /// <summary>
        /// ������������ַ�,strParA��strParB���,�򷵻�str1,���򷵻�str2
        /// </summary>
        /// <param name="strParA">Ҫ�ȽϵĲ���</param>
        /// <param name="strParB">Ҫ�ȽϵĲ���</param>
        /// <param name="str1">Ҫ���ص��ַ�</param>
        /// <param name="str2">Ҫ���ص��ַ�</param>
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
        #region ������Ϣ��ת��ָ����ַ
        /// <summary>
        /// ������Ϣ��ת��ָ����ַ
        /// </summary>
        /// <param name="Msg">Ҫ��������Ϣ</param>
        /// <param name="Link">Ҫ��ת�����ӵ�ַ</param>
        public static void MsgGotoUrl(string Msg, string Link)
        {
            http.Current.Response.Write("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\n");
            http.Current.Response.Write("<html xmlns=\"http://www.w3.org/1999/xhtml\">\n");
            http.Current.Response.Write("<head>\n");
            http.Current.Response.Write("<script src=\"/App_Themes/artDialog/artDialog-4.1.6.js?skin=default\" type=\"text/javascript\"></script>\n");
            http.Current.Response.Write("<script src=\"/App_Themes/artDialog/artDialog-4.1.6.iframe.plugins.js\" type=\"text/javascript\"></script>\n");
            http.Current.Response.Write("</head>\n");
            http.Current.Response.Write("<body>\n");
            http.Current.Response.Write("<script  type=\"text/javascript\">art.dialog({title:'��ʾ',icon:'succeed',content:'" + Msg + "',lock:true,ok:function(){location.href='" + Link + "';}});</script>\n");
            http.Current.Response.Write("</body>\n");
            http.Current.Response.Write("</html>\n");
            http.Current.Response.End();
        }
        #endregion
        #region ������Ϣˢ�¸���ҳ��
        /// <summary>
        /// ������Ϣˢ�¸���ҳ��
        /// </summary>
        /// <param name="Msg">Ҫ��������Ϣ</param>
        public static void MsgReload(string Msg)
        {
            http.Current.Response.Write("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\n");
            http.Current.Response.Write("<html xmlns=\"http://www.w3.org/1999/xhtml\">\n");
            http.Current.Response.Write("<head>\n");
            http.Current.Response.Write("<script src=\"/App_Themes/artDialog/artDialog-4.1.6.js?skin=default\" type=\"text/javascript\"></script>\n");
            http.Current.Response.Write("<script src=\"/App_Themes/artDialog/artDialog-4.1.6.iframe.plugins.js\" type=\"text/javascript\"></script>\n");
            http.Current.Response.Write("</head>\n");
            http.Current.Response.Write("<body>\n");
            http.Current.Response.Write("<script  type=\"text/javascript\">art.dialog({title:'��ʾ',icon:'succeed',content:'" + Msg + "',lock:true,ok:function(){parent.location.reload();}});</script>\n");
            http.Current.Response.Write("</body>\n");
            http.Current.Response.Write("</html>\n");
            http.Current.Response.End();
        }
        #endregion
        #region ������Ϣ��������һҳ
        /// <summary>
        /// ������Ϣ��������һҳ
        /// </summary>
        /// <param name="Msg">Ҫ��������Ϣ</param>
        public static void MsgGoBack(string Msg)
        {
            http.Current.Response.Write("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\n");
            http.Current.Response.Write("<html xmlns=\"http://www.w3.org/1999/xhtml\">\n");
            http.Current.Response.Write("<head>\n");
            http.Current.Response.Write("<script src=\"/App_Themes/artDialog/artDialog-4.1.6.js?skin=default\" type=\"text/javascript\"></script>\n");
            http.Current.Response.Write("<script src=\"/App_Themes/artDialog/artDialog-4.1.6.iframe.plugins.js\" type=\"text/javascript\"></script>\n");
            http.Current.Response.Write("</head>\n");
            http.Current.Response.Write("<body>\n");
            http.Current.Response.Write("<script  type=\"text/javascript\">art.dialog({title:'��ʾ',icon:'warning',content:'" + Msg + "',lock:true,ok:function(){history.go(-1);}});</script>\n");
            http.Current.Response.Write("</body>\n");
            http.Current.Response.Write("</html>\n");
            http.Current.Response.End();
        }
        #endregion
        #region ����Ƿ�Ϊ����
        /// <summary>
        /// ����Ƿ�Ϊ����
        /// </summary>
        /// <param name="str">Ҫ�����ַ���</param>
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
        #region ����Ƿ�Ϊ����
        /// <summary>
        /// ����Ƿ�Ϊ����
        /// </summary>
        /// <param name="str">�������ַ���</param>
        /// <returns>����true��false</returns>
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
        #region ����Ƿ�Ϊ�ʼ���ַ
        /// <summary>
        /// ����Ƿ�Ϊ�ʼ���ַ
        /// </summary>
        /// <param name="str">Ҫ�����ַ���</param>
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
        #region ����Ƿ�Ϊ��ϵ�绰
        /// <summary>
        /// ����Ƿ�Ϊ��ϵ�绰
        /// </summary>
        /// <param name="str">Ҫ�����ַ���</param>
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
        #region ����DropDownList��RadioButtonList��ListBox��CheckBoxListĬ��ѡ����
        /// <summary>
        /// ����DropDownListĬ��ѡ����
        /// </summary>
        /// <param name="drp">DropDownList������</param>
        /// <param name="val">Ҫ�Ƚϵ�ֵ</param>
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
        /// ����RadioButtonListĬ��ѡ����
        /// </summary>
        /// <param name="rad">RadioButtonList������</param>
        /// <param name="val">Ҫ�Ƚϵ�ֵ</param>
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
        /// ����ListBoxĬ��ѡ����
        /// </summary>
        /// <param name="lb">ListBox������</param>
        /// <param name="val">Ҫ�Ƚϵ�ֵ</param>
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
        /// ����CheckBoxListĬ��ѡ����
        /// </summary>
        /// <param name="chk">CheckBoxList������</param>
        /// <param name="val">Ҫ�Ƚϵ�ֵ</param>
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
        #region �����б����Ƿ����
        /// <summary>
        /// �����б����Ƿ����
        /// </summary>
        /// <param name="drp">DropDownList������</param>
        /// <param name="val">Ҫ�Ƚϵ�ֵ</param>
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
        #region ��ʾ��Ϣ����ֹ����
        /// <summary>
        /// ��ʾ��Ϣ����ֹ����
        /// </summary>
        /// <param name="str">��ʾ����Ϣ</param>
        public static void ShowEnd(string str)
        {

            http.Current.Response.Write(str);
            http.Current.Response.End();
        }
        #endregion
        #region md5����
        /// <summary>
        /// md5����
        /// </summary>
        /// <param name="str">Ҫ��������ַ���</param>
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
        #region SHA1����
        /// <summary>
        /// SHA1����
        /// </summary>
        /// <param name="str">Ҫ��������ַ���</param>
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
        #region ������
        /// <summary>
        /// ȡ������,����+�����
        /// </summary>
        /// <returns></returns>
        public static string GetOrderNo()
        {
            Random rad = new Random();
            int radno = rad.Next(1000, 9999);
            return DateTime.Now.ToString("yyyyMMddHHmmss") + radno.ToString();
        }
        #endregion
        #region С��10������ǰ����ַ�0
        /// <summary>
        /// С��10������ǰ����ַ�0
        /// </summary>
        /// <param name="n">Ҫ��������</param>
        /// <returns></returns>
        public static string ShowZero(int n)
        {
            if (n < 10)
                return "0" + n.ToString();
            else
                return n.ToString();
        }
        #endregion
        #region ��ʾflash��ͼƬ
        /// <summary>
        /// ��ʾflash��ͼƬ
        /// </summary>
        /// <param name="path">flash��ͼƬ��·��</param>
        /// <param name="width">���</param>
        /// <param name="height">�߶�</param>
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
        #region ֱ�������ļ�
        /// <summary>
        /// ֱ�������ļ�
        /// </summary>
        /// <param name="strFilePath">Ҫ���ص��ļ�·��</param>
        /// <param name="strFileName">�ͻ��˱�����ļ���</param>
        public static void DirectDownFile(string strFilePath, string strFileName)
        {
            FileInfo fileInfo = new FileInfo(strFilePath);

            //�������Ҫ��ͷ,�����п�������ӵĶ���!
            http.Current.Response.Clear();
            http.Current.Response.ClearContent();
            http.Current.Response.ClearHeaders();

            // HttpUtility.UrlEncode(filename) ��ʹ���ļ�����ȷ��ʾ
            http.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(strFileName));
            http.Current.Response.AddHeader("Content-Length", fileInfo.Length.ToString());

            //  http.Current.Response.ContentType �������кܶ�!
            http.Current.Response.ContentType = "application/octet-stream";
            http.Current.Response.WriteFile(fileInfo.FullName);

            //�����Ϳ����׳�������!
            http.Current.Response.Flush();
            http.Current.Response.End();
        }
        #endregion
        #region ���������ļ�
        /// <summary>
        /// ���������ļ�
        /// </summary>
        /// <param name="strFilePath">Ҫ���ص��ļ�·��</param>
        /// <param name="strFileName">�ͻ��˱�����ļ���</param>
        public static void FlushDownFile(string strFilePath, string strFileName)
        {
            FileInfo fileInfo = new FileInfo(strFilePath);
            if (fileInfo.Exists == true)
            {
                const long ChunkSize = 102400;//100K ÿ�ζ�ȡ�ļ���ֻ��ȡ100K���������Ի����������ѹ��
                byte[] buffer = new byte[ChunkSize];
                FileStream iStream = File.OpenRead(strFilePath);
                long dataLengthToRead = iStream.Length;//��ȡ���ص��ļ��ܴ�С

                //�⼸�仰�Ǳ���Ҫ��,���û��,�����п����з�������������Ӷ���!!!
                http.Current.Response.Clear();
                http.Current.Response.ClearContent();
                http.Current.Response.ClearHeaders();
                //ͬ��,����û�����ñ���,��Ϊ��ȡ���ļ���������gb2312,�����û�б�Ҫ��!

                // Response.ContentType �������кܶ�!
                http.Current.Response.ContentType = "application/octet-stream";
                // HttpUtility.UrlEncode(filename) ��ʹ���ļ�����ȷ��ʾ
                http.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(strFileName));

                while (dataLengthToRead > 0 && http.Current.Response.IsClientConnected)
                {
                    int lengthRead = iStream.Read(buffer, 0, Convert.ToInt32(ChunkSize));//��ȡ�Ĵ�С
                    http.Current.Response.OutputStream.Write(buffer, 0, lengthRead);
                    http.Current.Response.Flush();
                    dataLengthToRead = dataLengthToRead - lengthRead;
                }
                http.Current.Response.Close();
                http.Current.Response.End();
            }
        }
        #endregion
        #region �����ļ�
        /// <summary>
        /// ���Ӳ���ļ����ṩ���� ֧�ִ��ļ����������ٶ����ơ���Դռ��С
        /// </summary>
        /// <param name="http.Current.Request">Page.Request����</param>
        /// <param name="http.Current.Response">Page.Response����</param>
        /// <param name="strFileName">�����ļ���</param>
        /// <param name="strFilePath">���ļ�������·��</param>
        /// <param name="_speed">ÿ���������ص��ֽ���</param>
        /// <returns>�����Ƿ�ɹ�</returns>
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
                    //int sleep = 200;   //ÿ��5��   ��5*10K bytesÿ��
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
        #region ��������б�
        /// <summary>
        /// ��������б�
        /// </summary>
        /// <param name="drp">DropDownList������</param>
        /// <param name="intBase">��ʼ���</param>
        public static void DropDownList_Year(DropDownList drp, int intBase)
        {
            for (int i = intBase; i <= DateTime.Now.Year; i++)
            {
                drp.Items.Add(new ListItem(i.ToString() + "��", i.ToString()));
            }
        }
        #endregion
        #region �����·��б�
        /// <summary>
        /// �����·��б�
        /// </summary>
        /// <param name="drp">DropDownList������</param>
        public static void DropDownList_Month(DropDownList drp)
        {
            for (int i = 1; i <= 12; i++)
            {
                drp.Items.Add(new ListItem(i.ToString() + "��", i.ToString()));
            }
        }
        #endregion
        #region �������б�
        /// <summary>
        /// �������б�
        /// </summary>
        /// <param name="drp">DropDownList������</param>
        public static void DropDownList_Day(DropDownList drp)
        {
            for (int i = 1; i <= 31; i++)
            {
                drp.Items.Add(new ListItem(i.ToString() + "��", i.ToString()));
            }
        }
        #endregion
        #region ����Сʱ�б�
        /// <summary>
        /// ����Сʱ�б�
        /// </summary>
        /// <param name="drp">DropDownList������</param>
        public static void DropDownList_Hour(DropDownList drp)
        {
            for (int i = 0; i <= 23; i++)
            {
                drp.Items.Add(new ListItem(i.ToString() + "ʱ", i.ToString()));
            }
        }
        #endregion
        #region ���������б�
        /// <summary>
        /// ���������б�
        /// </summary>
        /// <param name="drp">DropDownList������</param>
        public static void DropDownList_Minute(DropDownList drp)
        {
            for (int i = 0; i <= 59; i++)
            {
                drp.Items.Add(new ListItem(ShowZero(i) + "��", ShowZero(i)));
            }
        }
        #endregion
        #region ���������б�
        /// <summary>
        /// ���������б�
        /// </summary>
        /// <param name="drp">DropDownList������</param>
        public static void DropDownList_Second(DropDownList drp)
        {
            for (int i = 0; i <= 59; i++)
            {
                drp.Items.Add(new ListItem(ShowZero(i) + "��", ShowZero(i)));
            }
        }
        #endregion
        #region �ַ�����
        /// <summary>
        /// �ַ�����
        /// </summary>
        /// <param name="str">Ҫ���ܵ��ַ�</param>
        /// <returns></returns>
        public static string Encrypt(string str)
        {
            return ConnectionInfo.EncryptDBConnectionString(str);
        }
        #endregion
        #region �ַ�����
        /// <summary>
        /// �ַ�����
        /// </summary>
        /// <param name="str">Ҫ���ܵ��ַ�</param>
        /// <returns></returns>
        public static string Decrypt(string str)
        {
            return ConnectionInfo.DecryptDBConnectionString(str);
        }
        #endregion
        #region �ļ����ļ��в���
        /// <summary>
        /// ����ļ��Ƿ����,�粻����,�򴴽��ļ���
        /// </summary>
        /// <param name="strFolderPath">�ļ���·��</param>
        public static void CheckFolder(string strFolderPath)
        {
            if (!Directory.Exists(strFolderPath))
            {
                Directory.CreateDirectory(strFolderPath);
            }
        }
        /// <summary>
        /// �滻�ļ������е������ַ�
        /// </summary>
        /// <param name="strFolderName">Ҫ�����ļ�����</param>
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
        /// �滻�ļ����е������ַ�
        /// </summary>
        /// <param name="strFileName">Ҫ�����ļ���</param>
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
        #region ͨ���ļ����ĺ�׺���ж��ļ��Ƿ���ͼƬ
        /// <summary>
        /// ͨ���ļ����ĺ�׺���ж��ļ��Ƿ���ͼƬ
        /// </summary>
        /// <param name="strFileName">Ҫ�����ļ���</param>
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
        /// ͨ���ļ����ĺ�׺���ж��ļ��Ƿ���ͼƬ
        /// </summary>
        /// <param name="strFileExt">Ҫ���ĺ�׺</param>
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
        /// ͨ���ļ����ĺ�׺���ж��ļ��Ƿ���ͼƬ(������gifͼƬ)
        /// </summary>
        /// <param name="strFileExt">Ҫ���ĺ�׺</param>
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
        #region �ļ������Ƿ����ļ��к��ļ�
        /// <summary>
        /// �ļ������Ƿ����ļ��к��ļ�
        /// </summary>
        /// <param name="strFolderPath">Ҫ�����ļ���</param>
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
        #region �ռ�������Ϣ
        /// <summary>
        /// �ռ�������Ϣ
        /// </summary>
        /// <param name="ex">Exception������</param>
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
                    sw.WriteLine("ʱ��:" + DateTime.Now.ToString());
                    sw.WriteLine("IP:" + http.Current.Request.UserHostAddress);
                    sw.WriteLine("��������:" + ex.Message);
                    sw.WriteLine("���������ʵ��:" + ex.InnerException);
                    sw.WriteLine("��ջ����:\n" + ex.StackTrace);
                    sw.WriteLine("����Դ:" + ex.Source);
                    sw.WriteLine("��������ķ���:" + ex.TargetSite);
                    sw.WriteLine("���������·��:" + http.Current.Request.Url.ToString());
                    sw.WriteLine("ҳ����Դ:" + http.Current.Request.UrlReferrer.ToString());
                    sw.WriteLine("---------------------------------------------------------------------------------------------------------------");
                }
            }
            catch
            { }
        }
        #endregion
        #region �ؼ��֡�����
        /// <summary>
        /// ��վ�ؼ���
        /// </summary>
        /// <param name="strKeywords">��ҳҪ���õĹؼ���</param>
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
        /// ��վ����
        /// </summary>
        /// <param name="strDescription">��ҳҪ���õ�����</param>
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
        #region ���߿ͷ�
        /// <summary>
        /// ���߿ͷ�
        /// </summary>
        /// <param name="strTypeID">���߿ͷ�����</param>
        /// <param name="strNickName">�ǳ�</param>
        /// <param name="strAccount">�ʺ�</param>
        /// <param name="strChatKey">keyֵ</param>
        /// <returns></returns>
        public static string ShowChatAccount(string strTypeID, string strNickName, string strAccount, string strChatKey)
        {
            switch (strTypeID)
            {
                case "1":
                    if (strChatKey == string.Empty)
                    {
                        return "<a target=\"_blank\" href=\"http://wpa.qq.com/msgrd?v=3&uin=" + strAccount + "&site=qq&menu=yes\"><img border=\"0\" src=\"http://wpa.qq.com/pa?p=2:" + strAccount + ":4\" alt=\"���������ҷ���Ϣ\" title=\"���������ҷ���Ϣ\">" + strNickName + "</a>";
                    }
                    else
                    {
                        return "<a target=\"_blank\" href=\"http://sighttp.qq.com/authd?IDKEY=" + strChatKey + "\"><img border=\"0\"  src=\"http://wpa.qq.com/imgd?IDKEY=" + strChatKey + "&pic=4\" alt=\"���������ҷ���Ϣ\" title=\"���������ҷ���Ϣ\">" + strNickName + "</a>";
                    }
                case "2":
                    if (strChatKey == string.Empty)
                    {
                        return "<a href=\"msnim:chat?contact=" + strAccount + "\"><img border=\"0\" src=\"/App_Themes/Images/im_msn.jpg\" alt=\"���������ҷ���Ϣ\" title=\"���������ҷ���Ϣ\">" + strNickName + "</a>";
                    }
                    else
                    {
                        return "<a target=\"_blank\" href=\"http://settings.messenger.live.com/Conversation/IMMe.aspx?invitee=" + strChatKey + "@apps.messenger.live.com&amp;mkt=zh-cn\"><img  border=\"0\" src=\"http://messenger.services.live.com/users/" + strChatKey + "@apps.messenger.live.com/presenceimage?mkt=zh-cn\" alt=\"���������ҷ���Ϣ\" title=\"���������ҷ���Ϣ\"/>" + strNickName + "</a>";
                    }
                case "3":
                    return "<a target=\"blank\" href=\"callto://" + strAccount + "/\"><img  border=\"0\" src=\"/App_Themes/Images/im_skype.jpg\"  alt=\"���������ҷ���Ϣ\" title=\"���������ҷ���Ϣ\"/>" + strNickName + "</a>";
                case "4":
                    return "<a target=\"blank\" href=\"http://amos1.taobao.com/msg.ww?v=2&amp;uid=" + strAccount + "&amp;s=2\"><img  border=\"0\" src=\"http://amos1.taobao.com/online.ww?v=2&amp;uid=" + strAccount + "&amp;s=2\"  alt=\"���������ҷ���Ϣ\" title=\"���������ҷ���Ϣ\"/>" + strNickName + "</a>";
                default:
                    return "";
            }
        }
        #endregion
        #region Request
        /// <summary>
        /// ����POST��GET��ʽ�ύ��ֵ
        /// </summary>
        /// <param name="obj">Request.Form��Request.QueryString����</param>
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
        /// ����POST��GET��ʽ�ύ��ֵ�Ƿ�����
        /// </summary>
        /// <param name="obj">Request.Form��Request.QueryString����</param>
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
        #region �ַ�����ת��
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
        #region ȡ����Ԥ��
        public static string GetWeather()
        {
            StringBuilder strReturn = new StringBuilder();
            try
            {
                //ȡIP
                string strIP = HttpContext.Current.Request.UserHostAddress.ToString();

                //����IPȡ����
                WebRequest web = WebRequest.Create("http://int.dpool.sina.com.cn/iplookup/iplookup.php?format=json&ip=" + strIP);
                web.Timeout = 20000;
                HttpWebResponse http = (HttpWebResponse)web.GetResponse();
                Stream s = http.GetResponseStream();
                StreamReader sr = new StreamReader(s, Encoding.Default);
                Dictionary<string, object> strCityJson = (Dictionary<string, object>)JsonConvert.DeserializeObject(sr.ReadToEnd(), typeof(Dictionary<string, object>));
                object val;
                strCityJson.TryGetValue("city", out val);
                string strTempCity = "����";
                if (val != null)
                {
                    strTempCity = val.ToString();
                }
                string strCity = GB2312ToUtf8(strTempCity.ToString());

                //���ݳ���ȡ����
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
                            strReturn.Append(" " + w1 + "��" + w2);
                        }
                        XmlNode nodeT1 = xml.SelectSingleNode("/Profiles/Weather/temperature1");
                        strReturn.Append(" " + nodeT1.InnerText + "��");
                        XmlNode nodeT2 = xml.SelectSingleNode("/Profiles/Weather/temperature2");
                        strReturn.Append("��" + nodeT2.InnerText + "��");
                    }
                }
            }
            catch
            { }
            return strReturn.ToString();
        }
        #endregion
        #region ��ת��
        /// <summary>
        /// ��ת��
        /// </summary>
        /// <param name="str"></param>
        /// <param name="convType">B2G:��ת�� G2B:��ת��</param>
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
        //---------------------------------------------------------------------����������
        #region ��ת��ѡ��
        /// <summary>
        /// ��ת��ѡ��
        /// </summary>
        /// <param name="str">B2G:��ת�� G2B:��ת��</param>
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

