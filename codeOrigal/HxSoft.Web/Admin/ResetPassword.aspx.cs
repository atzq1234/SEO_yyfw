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
    public partial class ResetPassword : System.Web.UI.Page
    {
        /// <summary>
        /// ������:��С��
        /// ����:2010-12-6
        /// </summary>
        //����ȫ�ֱ���
        //ҳ���ʼ��
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            if (!Page.IsPostBack)
            {
                ShowInfo();
            }
        }

        //��������
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strOldAdminPass = Config.md5(txtOldAdminPass.Text);
            string strNewAdminPass = Config.md5(txtNewAdminPass.Text);
            AdminModel admModel = new AdminModel();
            admModel = Factory.Admin().GetInfo(Session["AdminID"].ToString());
            if (admModel != null)
            {
                if (admModel.AdminPass == strOldAdminPass)
                {
                    Factory.Admin().ResetPassword(Session["AdminID"].ToString(), strNewAdminPass);
                    Factory.AdminLog().InsertLog("�޸����롣", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("�����޸ĳɹ�!��Ҫ���µ�¼!", "LogOut.aspx");
                }
                else
                {
                    errMsg.Text = "�����벻��ȷ!";
                }
            }
        }

        //��ʾ����
        protected void ShowInfo()
        {
            AdminModel admModel = new AdminModel();
            admModel = Factory.Admin().GetInfo(Session["AdminID"].ToString());
            if (admModel != null)
            {
                lblAdminName.Text = admModel.AdminName;
            }
            else
            {
                Config.ShowEnd("��û�в鿴����Ϣ��Ȩ�ޣ�");
            }
        }

    }
}
