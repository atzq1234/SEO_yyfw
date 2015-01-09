using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using HxSoft.Common;
using HxSoft.Model;
using HxSoft.ClassFactory;
using System.IO;

namespace HxSoft.Web.Admin.User
{
    public partial class Mail_Export : System.Web.UI.Page
    {
        /// <summary>
        /// 创建人:杨小明
        /// 日期:2010-12-6
        /// </summary>

        //页面初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            GetData.LimitChkMsg("MailExport");
            string strFileName = "mail_" + Session["AdminID"].ToString() + ".txt";
            string strFilePath = Server.MapPath(strFileName);
            string sql = "select * from t_Mail where IsRec=1 order by MailID asc";
            Factory.Mail().EmailExport(sql, strFilePath, strFileName);
        }
    }
}
