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

namespace HxSoft.Web.Admin
{
    public partial class Login : System.Web.UI.Page
    {
        /// <summary>
        /// ������:��С��
        /// ����:2010-12-6
        /// </summary>
        //����ȫ�ֱ���
        public string Url//���ص�ַ
        {
            get
            {
                string strUrl = Config.Request(Request.QueryString["Url"], "");
                return "Index_Index.aspx?Url=" + strUrl;
            }
        }
        //��ʼ��ҳ��
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }


        //��½
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string strAdminName = txtAdminName.Text.Trim();
            string strAdminPass = txtAdminPass.Text.Trim();
            strAdminPass = Config.md5(strAdminPass);
            string strVerifyCode = txtVerifyCode.Text.Trim();

            if (Session["VerifyCode"] == null)
            {
                errMsg.Text = "��֤������!";
            }
            else
            {
                if (strVerifyCode != Session["VerifyCode"].ToString())
                {
                    errMsg.Text = "��֤������!";
                }
                else
                {
                    if (Factory.Admin().Login(strAdminName, strAdminPass))
                    {
                        Factory.AdminLog().InsertLog("��¼ϵͳ��", Session["AdminID"].ToString());
                        Response.Redirect(Url);
                        //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "", "alert('��¼�ɹ�!');location.href='" + Url + "'", true);
                    }
                    else
                    {
                        errMsg.Text = "�û�������������!";
                    }
                }
            }
        }

    }
}
