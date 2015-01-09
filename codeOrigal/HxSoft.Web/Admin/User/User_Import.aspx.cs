using System;
using System.Data;
using System.Data.OleDb;
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
    public partial class User_Import : System.Web.UI.Page
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
            GetData.LimitChkMsg("UserImport");
            if (!Page.IsPostBack)
            {
                //��Ա����
                Factory.Acc().DataBind("select * from t_UserRank  order by ListID asc", null,Config.DataBindObjTypeCollection.DropDownList.ToString(), drpUserRankID, "UserRankName", "UserRankID");
                drpUserRankID.Items.Insert(0, new ListItem("��ѡ��", "-1"));
            }
        }
        //��������
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strImportFile = txtImportFile.Text;
            string strFilePath = Server.MapPath(strImportFile);
            string strFileExt = Path.GetExtension(strFilePath);
            if (strFileExt.ToLower() != ".xls" && strFileExt.ToLower() != ".mdb")
            {
                errMsg.Text = "��ѡ���׺Ϊ.xls�ļ���Excel�ļ����׺Ϊ.mdb��Access�ļ�!";
            }
            else
            {
                DataAccess dat;
                string strSql;
                if (strFileExt.ToLower() == ".xls")
                {
                    string strExcelConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath(strImportFile) + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
                    dat = new DataAccess(strExcelConnStr);
                    strSql = "select * from [sheet1$]";
                }
                else
                {
                    string strAccessConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath(strImportFile);
                    dat = new DataAccess(strAccessConnStr);
                    strSql = "select * from sheet1";
                }
                StringBuilder strErr = new StringBuilder();
                int i = 0;
                using (OleDbDataReader dr = dat.GetDataReader(CommandType.Text, strSql, null))
                {
                    while (dr.Read())
                    {
                        UserModel userModel = new UserModel();
                        userModel.UserName = dr["�û���"].ToString().Trim();
                        string strUserPass = dr["����"].ToString().Trim();
                        userModel.PassQuestion = "";
                        userModel.PassAnswer = "";
                        userModel.RealName = dr["����"].ToString().Trim();
                        userModel.Sex = dr["�Ա�"].ToString();
                        userModel.Email = dr["�ʼ���ַ"].ToString().Trim();
                        userModel.Mobile = dr["��ϵ�绰"].ToString().Trim();
                        userModel.Address = dr["��ϵ��ַ"].ToString().Trim();
                        userModel.Company = dr["��˾����"].ToString().Trim();
                        userModel.Point = dr["����"].ToString().Trim();
                        userModel.Comment = dr["��ע"].ToString().Trim();
                        userModel.UserRankID = drpUserRankID.SelectedValue;
                        userModel.IsAudit = "1";
                        userModel.LoginNum = "0";
                        userModel.LastLoginTime = "";
                        userModel.ThisLoginTime = "";
                        userModel.AddTime = DateTime.Now.ToString();
                        userModel.IsClose = "0";
                        if (!Factory.User().CheckInfo("UserName", userModel.UserName))
                        {
                            userModel.UserPass = Config.md5(strUserPass);
                            Factory.User().InsertInfo(userModel);
                            i++;
                        }
                        else
                        {
                            strErr.Append(userModel.UserName + "<br>");
                        }
                    }
                }
                Factory.AdminLog().InsertLog("��������" + i.ToString() + "����Ա��", Session["AdminID"].ToString());
                errReslut.Text = "�����ɹ�!����" + i.ToString() + "����Ա�����»�Աδ�ܵ���ɹ�,����ԭ�������ݿ����Ѵ�����ͬ��Ա!<br>";
                errReslut.Text += strErr.ToString();
            }
        }
    }
}
