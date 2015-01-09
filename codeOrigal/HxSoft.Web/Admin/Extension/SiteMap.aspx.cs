using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Data.OleDb;
using HxSoft.Common;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using HxSoft.Model;
using HxSoft.ClassFactory;
using System.Data.Common;

namespace HxSoft.Web.Admin.Extension
{
    public partial class SiteMap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
            }
        }

        //生成Google Xml文件格式
        public string GoogleSiteMapString()
        {
            DataSql dat = new DataSql(Config.SqlConnStr);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?> ");
            sb.AppendLine("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\"> ");

            string sql_config = "select ConfigID,WebsiteUrl from t_Config where IsClose=0 order by ListID asc";
            DataTable dt = Factory.Acc().GetDataTable(sql_config, null);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string sql_name = strSql(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString());
                using (SqlDataReader dr = dat.GetDataReader(CommandType.Text, sql_name, null))
                {
                    while (dr.Read())
                    {
                        sb.AppendLine("<url>");
                        sb.AppendLine(string.Format("<loc>{0}</loc> ", dr[0].ToString()));
                        sb.AppendLine(string.Format("<lastmod>{0}</lastmod> ", DateTime.Now.ToString("yyyy-MM-dd")));
                        sb.AppendLine(string.Format("<changefreq>{0}</changefreq> ", "monthly"));
                        sb.AppendLine(string.Format("<priority>{0}</priority> ", "0.3"));
                        sb.AppendLine("</url>");
                    }

                }
            }
            sb.AppendLine("</urlset>");
            return sb.ToString();
        }

        //生成Baidu Xml文件格式
        public string BaiduSiteMapString()
        {
            DataSql dat = new DataSql(Config.SqlConnStr);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?> ");
            sb.AppendLine("<urlset> ");

            string sql_config = "select ConfigID,WebsiteUrl from t_Config where IsClose=0 order by ListID asc";
            DataTable dt = Factory.Acc().GetDataTable(sql_config, null);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string sql_name = strSql(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString());
                using (SqlDataReader dr = dat.GetDataReader(CommandType.Text, sql_name, null))
                {
                    while (dr.Read())
                    {
                        sb.AppendLine("<url>");
                        sb.AppendLine(string.Format("<loc>{0}</loc> ", dr[0].ToString()));
                        sb.AppendLine(string.Format("<lastmod>{0}</lastmod> ", DateTime.Now.ToString("yyyy-MM-dd")));
                        sb.AppendLine(string.Format("<changefreq>{0}</changefreq> ", "monthly"));
                        sb.AppendLine(string.Format("<priority>{0}</priority> ", "0.3"));
                        sb.AppendLine("</url>");
                    }

                }
            }
            sb.AppendLine("</urlset>");
            return sb.ToString();
        }

        //获得Url地址
        public string strSql(string strConfigID, string strWebsiteUrl)
        {
            string DataLink = "http://" + Request.ServerVariables["HTTP_HOST"] + strWebsiteUrl;
            string strTemp = " and ClassID in(select ClassID from t_Class where ConfigID=" + strConfigID + ")";
            StringBuilder strSql = new StringBuilder();
            if (Config.DatabaseType == Config.DatabaseTypeCollection.MySql.ToString())
            {
                strSql.Append("select concat('" + DataLink + "',ClassEnName,'" + Config.FileExt + "') as sLinkUrl from t_Class where IsClose=0 " + strTemp);
                strSql.Append(" union all select concat('" + DataLink + "article-details-',ArticleID,'" + Config.FileExt + "') as sLinkUrl from t_Article where IsClose=0 " + strTemp);
                strSql.Append(" union all select concat('" + DataLink + "product-details-',ProductID,'" + Config.FileExt + "') as sLinkUrl from t_Product where IsClose=0 " + strTemp);
                strSql.Append(" union all select concat('" + DataLink + "job-details-',JobID,'" + Config.FileExt + "') as sLinkUrl from t_Job where IsClose=0 " + strTemp);
                strSql.Append(" union all select concat('" + DataLink + "download-details-',DownloadID,'" + Config.FileExt + "') as sLinkUrl from t_Download where IsClose=0 " + strTemp);
                strSql.Append(" union all select concat('" + DataLink + "survey-details-',SurveyID,'" + Config.FileExt + "') as sLinkUrl from t_Survey where IsClose=0 " + strTemp);
                strSql.Append(" union all select concat('" + DataLink + "video-details-',SurveyID,'" + Config.FileExt + "') as sLinkUrl from t_Video where IsClose=0 " + strTemp);
            }
            else
            {
                strSql.Append("select ('" + DataLink + "'+ClassEnName+'" + Config.FileExt + "') as sLinkUrl from t_Class where IsClose=0 " + strTemp);
                strSql.Append(" union all select ('" + DataLink + "article-details-'+ltrim(str(ArticleID))+'" + Config.FileExt + "') as sLinkUrl from t_Article where IsClose=0 " + strTemp);
                strSql.Append(" union all select ('" + DataLink + "product-details-'+ltrim(str(ProductID))+'" + Config.FileExt + "') as sLinkUrl from t_Product where IsClose=0 " + strTemp);
                strSql.Append(" union all select ('" + DataLink + "job-details-'+ltrim(str(JobID))+'" + Config.FileExt + "') as sLinkUrl from t_Job where IsClose=0 " + strTemp);
                strSql.Append(" union all select ('" + DataLink + "download-details-'+ltrim(str(DownloadID))+'" + Config.FileExt + "') as sLinkUrl from t_Download where IsClose=0 " + strTemp);
                strSql.Append(" union all select ('" + DataLink + "survey-details-'+ltrim(str(SurveyID))+'" + Config.FileExt + "') as sLinkUrl from t_Survey where IsClose=0 " + strTemp);
                strSql.Append(" union all select ('" + DataLink + "video-details-'+ltrim(str(VideoID))+'" + Config.FileExt + "') as sLinkUrl from t_Video where IsClose=0 " + strTemp);
            }
            return strSql.ToString();
        }


        //保存sitemap.xml文件至根目录
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            File.WriteAllText(Server.MapPath("/sitemap.xml"), GoogleSiteMapString());
            errMsg1.Text = "生成成功,请提交文件";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!File.Exists(Server.MapPath("/sitemap.xml")))
            {
                errMsg2.Text = "请先生成文件，再提交";
            }
            else if (checkType.Items.Count == 0)
            {
                errMsg2.Text = "请选择提交的入口";
            }
            else
            {
                string locurl = "http://" + Request.ServerVariables["HTTP_HOST"] + "/sitemap.xml";
                string param = "";
                StringBuilder strErr = new StringBuilder();

                MSXML2.XMLHTTP xmlhttp = new MSXML2.XMLHTTP();
                for (int i = 0; i < checkType.Items.Count; i++)
                {
                    if (checkType.Items[i].Selected)
                    {
                        if (i == 0)
                        {
                            param = "sitemap={0}";
                            param = string.Format(param, HttpUtility.UrlEncode(locurl));
                            xmlhttp.open("POST", "http://www.google.com/webmasters/tools/ping?", false);
                            xmlhttp.setRequestHeader("CONTENT-TYPE", "application/x-www-form-urlencoded");//POST方法必须设置此选项，GET方法则不需要设置
                            xmlhttp.setRequestHeader("Content-Length", param.Length.ToString());
                            xmlhttp.send(param);
                            if (xmlhttp.readyState == 4)
                            {
                                if (xmlhttp.status == 200)
                                {
                                    strErr.Append("成功提交到Google / ");
                                }
                                else
                                {
                                    strErr.Append("Google提交失败(" + xmlhttp.status + ") / ");
                                }
                            }
                        }
                        else if (i == 1)
                        {
                            param = "p={0}";
                            param = string.Format(param, HttpUtility.UrlEncode(locurl));
                            xmlhttp.open("POST", "http://tool.cnzz.com/yahoo/ti.php?", false);
                            xmlhttp.setRequestHeader("CONTENT-TYPE", "application/x-www-form-urlencoded");//POST方法必须设置此选项，GET方法则不需要设置
                            xmlhttp.setRequestHeader("Content-Length", param.Length.ToString());
                            xmlhttp.send(param);
                            if (xmlhttp.readyState == 4)
                            {
                                if (xmlhttp.status == 200)
                                {
                                    strErr.Append("成功提交到Yahoo / ");
                                }
                                else
                                {
                                    strErr.Append("Yahoo提交失败(" + xmlhttp.status + ") / ");
                                }
                            }
                        }
                        else if (i == 2)
                        {
                            param = "siteMap={0}";
                            param = string.Format(param, HttpUtility.UrlEncode(locurl));
                            xmlhttp.open("POST", "http://www.bing.com/webmaster/ping.aspx?" + param, false);
                            xmlhttp.setRequestHeader("CONTENT-TYPE", "application/x-www-form-urlencoded");//POST方法必须设置此选项，GET方法则不需要设置
                            xmlhttp.setRequestHeader("Content-Length", param.Length.ToString());
                            xmlhttp.send();
                            if (xmlhttp.readyState == 4)
                            {
                                if (xmlhttp.status == 200)
                                {
                                    strErr.Append("成功提交到Bing");
                                }
                                else
                                {
                                    strErr.Append("Bing提交失败(" + xmlhttp.status + ")");
                                }
                            }
                        }
                    }
                }
                errMsg2.Text = strErr.ToString();
            }
        }

    }
}