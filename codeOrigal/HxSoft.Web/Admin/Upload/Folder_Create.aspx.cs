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
using System.IO;
using System.Text;
using HxSoft.Common;
using HxSoft.Model;
using HxSoft.ClassFactory;

namespace HxSoft.Web.Admin.Upload
{
    public partial class Folder_Create : System.Web.UI.Page
    {
        /// <summary>
        /// ������:��С��
        /// ����:2010-12-6
        /// </summary>
        //����ȫ�ֱ���
        public string strFolderPath
        {
            get
            {
                return Config.FolderNameReplace(Config.Request(Request["FolderPath"], Config.FileUploadPath));
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            GetData.LimitChkMsg("FolderCreate");
            if (!Page.IsPostBack)
            {
            }
        }

        //�����ļ���
        protected void btnCreateFolder_Click(object sender, EventArgs e)
        {
            string strFolderName = Config.FolderNameReplace(txtFolderName.Text);
            if (strFolderName != string.Empty)
            {
                string strNewFolderPath = strFolderPath + strFolderName + "/";
                if (!Directory.Exists(Server.MapPath(strNewFolderPath)))
                {
                    Directory.CreateDirectory(Server.MapPath(strNewFolderPath));
                    //�������ļ��е�·�����浽���ݿ���
                    PathModel pathModel = new PathModel();
                    pathModel.Path = strNewFolderPath.ToLower();
                    pathModel.AdminID = Session["AdminID"].ToString();
                    Factory.Path().InsertInfo(pathModel);
                    Factory.AdminLog().InsertLog("�����ļ���\"" + strNewFolderPath + "\"��", Session["AdminID"].ToString());
                    //
                }
                StringBuilder strJs = new StringBuilder();
                strJs.Append("<script>");
                strJs.Append("parent.window.location.reload();");
                strJs.Append("</script>");
                Config.ShowEnd(strJs.ToString());
            }
        }
    }
}
