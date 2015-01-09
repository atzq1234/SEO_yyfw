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
using System.Data.Common;
using System.Collections.Generic;

namespace HxSoft.Web.Admin._System
{
    public partial class ClassProperty_Add : System.Web.UI.Page
    {
        /// <summary>
        ///栏目属性
        /// 创建人:杨小明
        /// 日期:2012-4-16
        /// </summary>
        //定义全局变量
        public string ClassPropertyID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["ClassPropertyID"], 0).ToString();
            }
        }
        public int page
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["page"], 1);
            }
        }
        #region ****排序参数****
        public string strOrderKey
        {
            get
            {
                return Config.Request(Request["OrderKey"], "ClassPropertyID");
            }
        }
        public string strAscDesc1
        {
            get
            {
                return Config.Request(Request["AscDesc"], "asc");
            }
        }
        public string strAscDesc2
        {
            get
            {
                if (strAscDesc1 == "asc")
                    return "desc";
                else
                    return "asc";
            }
        }
        #endregion
        #region ****排序语句****
        public string SqlOrder
        {
            get
            {
                return " order by " + strOrderKey + " " + strAscDesc1;
            }
        }
        #endregion
        #region ****Url排序参数****
        public string UrlOrderPara
        {
            get
            {
                StringBuilder TempUrl = new StringBuilder("");
                TempUrl.Append("OrderKey=" + Server.UrlEncode(strOrderKey) + "&");
                TempUrl.Append("AscDesc=" + Server.UrlEncode(strAscDesc1) + "&");
                return TempUrl.ToString();
            }
        }
        #endregion
        #region ****查询参数****
        public string strPropertyName
        {
            get
            {
                return Config.Request(Request["txtPropertyName"], "");
            }
        }
        public string strIsClose
        {
            get
            {
                return Config.Request(Request["radIsClose"], "-1");
            }
        }
        #endregion
        #region ****查询语句****
        public string SqlQuery
        {
            get
            {
                StringBuilder TempSql = new StringBuilder("");
                if (strPropertyName != "") TempSql.Append(" and PropertyName like @PropertyName");
                if (strIsClose != "-1") TempSql.Append(" and IsClose =@IsClose");
                return TempSql.ToString();
            }
        }
        #endregion
        #region****DbParameter参数****
        public DbParameter[] SqlParams
        {
            get
            {
                List<DbParameter> listParams = new List<DbParameter>();
                if (strPropertyName != "") listParams.Add(Config.Conn().CreateDbParameter("@PropertyName", "%" + strPropertyName + "%"));
                if (strIsClose != "-1") listParams.Add(Config.Conn().CreateDbParameter("@IsClose", strIsClose));
                return listParams.ToArray();
            }
        }
        #endregion
        #region ****Url参数****
        public string UrlPara
        {
            get
            {
                StringBuilder TempUrl = new StringBuilder("");
                TempUrl.Append("txtPropertyName=" + Server.UrlEncode(strPropertyName) + "&");
                TempUrl.Append("radIsClose=" + Server.UrlEncode(strIsClose) + "&");
                return TempUrl.ToString();
            }
        }
        #endregion
        //页面初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            if (!Page.IsPostBack)
            {
                if (ClassPropertyID == "0")
                {
                    GetData.LimitChkMsg("ClassPropertyAdd");
                    lblTitle.Text = "添加";
                    string strListID = Factory.ClassProperty().GetListID();
                    txtListID.Text = strListID;
                    hidlistID.Value = strListID;
                }
                else
                {
                    GetData.LimitChkMsg("ClassPropertyEdit");
                    lblTitle.Text = "修改";
                    ShowInfo();
                }
            }
        }
        //保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ClassPropertyModel claProModel = new ClassPropertyModel();
            string strOldListID = hidlistID.Value;
            claProModel.PropertyName = txtPropertyName.Text.Trim();
            claProModel.ListID = txtListID.Text.Trim();
            claProModel.AdminID = Session["AdminID"].ToString();
            claProModel.AddTime = DateTime.Now.ToString();
            claProModel.IsClose = radIsClose.SelectedValue;
            if (ClassPropertyID == "0")
            {
                Factory.ClassProperty().OrderInfo(claProModel.ListID, strOldListID);
                Factory.ClassProperty().InsertInfo(claProModel);
                Factory.AdminLog().InsertLog("添加名称为" + claProModel.PropertyName + "的栏目属性!", Session["AdminID"].ToString());
                Config.MsgGotoUrl("添加成功！", "ClassProperty.aspx");
            }
            else
            {
                ClassPropertyModel claProModel_2 = new ClassPropertyModel();
                claProModel_2 = Factory.ClassProperty().GetInfo(ClassPropertyID);
                if (claProModel_2 != null)
                {
                    if (GetData.CheckAdminID(claProModel_2.AdminID, "ClassPropertyAll"))//检查创建者
                    {
                        Factory.ClassProperty().OrderInfo(claProModel.ListID, strOldListID);
                        Factory.ClassProperty().UpdateInfo(claProModel, ClassPropertyID);
                        Factory.AdminLog().InsertLog("修改编号为" + ClassPropertyID + "的栏目属性!", Session["AdminID"].ToString());
                        Config.MsgGotoUrl("修改成功！", "ClassProperty.aspx?" + UrlOrderPara + UrlPara + "page=" + page);
                    }
                }
            }
        }
        //显示数据
        protected void ShowInfo()
        {
            ClassPropertyModel claProModel = new ClassPropertyModel();
            claProModel = Factory.ClassProperty().GetInfo(ClassPropertyID);
            if (claProModel != null)
            {
                if (GetData.CheckAdminID(claProModel.AdminID, "ClassPropertyAll"))//检查创建者
                {
                    txtPropertyName.Text = claProModel.PropertyName;
                    txtListID.Text = claProModel.ListID;
                    hidlistID.Value = claProModel.ListID;
                    //txtAdminID.Text = claProModel.AdminID;
                    //txtAddTime.Text = claProModel.AddTime;
                    radIsClose.ClearSelection();
                    Config.setDefaultSelected(radIsClose, claProModel.IsClose);
                }
                else
                {
                    Config.ShowEnd("您没有查看此信息的权限！");
                }
            }
            else
            {
                Config.ShowEnd("您没有查看此信息的权限！");
            }
        }

    }
}
