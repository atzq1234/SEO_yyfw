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
        /// 创建人:杨小明
        /// 日期:2010-12-6
        /// </summary>
        //定义全局变量
        //页面初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            GetData.LimitChkMsg("UserImport");
            if (!Page.IsPostBack)
            {
                //会员级别
                Factory.Acc().DataBind("select * from t_UserRank  order by ListID asc", null,Config.DataBindObjTypeCollection.DropDownList.ToString(), drpUserRankID, "UserRankName", "UserRankID");
                drpUserRankID.Items.Insert(0, new ListItem("请选择", "-1"));
            }
        }
        //保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strImportFile = txtImportFile.Text;
            string strFilePath = Server.MapPath(strImportFile);
            string strFileExt = Path.GetExtension(strFilePath);
            if (strFileExt.ToLower() != ".xls" && strFileExt.ToLower() != ".mdb")
            {
                errMsg.Text = "请选择后缀为.xls文件的Excel文件或后缀为.mdb的Access文件!";
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
                        userModel.UserName = dr["用户名"].ToString().Trim();
                        string strUserPass = dr["密码"].ToString().Trim();
                        userModel.PassQuestion = "";
                        userModel.PassAnswer = "";
                        userModel.RealName = dr["姓名"].ToString().Trim();
                        userModel.Sex = dr["性别"].ToString();
                        userModel.Email = dr["邮件地址"].ToString().Trim();
                        userModel.Mobile = dr["联系电话"].ToString().Trim();
                        userModel.Address = dr["联系地址"].ToString().Trim();
                        userModel.Company = dr["公司名称"].ToString().Trim();
                        userModel.Point = dr["积分"].ToString().Trim();
                        userModel.Comment = dr["备注"].ToString().Trim();
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
                Factory.AdminLog().InsertLog("批量导入" + i.ToString() + "个会员。", Session["AdminID"].ToString());
                errReslut.Text = "操作成功!导入" + i.ToString() + "个会员。以下会员未能导入成功,可能原因是数据库中已存在相同会员!<br>";
                errReslut.Text += strErr.ToString();
            }
        }
    }
}
