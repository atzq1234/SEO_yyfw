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
using System.Collections.Generic;
using System.Data.Common;

namespace HxSoft.Web.Admin._System
{
    public partial class ClassTemplate_Add : System.Web.UI.Page
    {
        /// <summary>
        ///栏目模板
        /// 创建人:杨小明
        /// 日期:2012-4-16
        /// </summary>
        //定义全局变量
        public string ClassTemplateID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["ClassTemplateID"], 0).ToString();
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
                return Config.Request(Request["OrderKey"], "ClassTemplateID");
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
        public string strClassPropertyID
        {
            get
            {
                return Config.Request(Request["drpClassPropertyID"], "-1");
            }
        }
        public string strTemplateName
        {
            get
            {
                return Config.Request(Request["txtTemplateName"], "");
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
                if (strClassPropertyID != "-1") TempSql.Append(" and ClassPropertyID =@ClassPropertyID");
                if (strTemplateName != "") TempSql.Append(" and TemplateName like @TemplateName");
                if (strIsClose != "-1") TempSql.Append(" and IsClose=@IsClose");
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
                if (strClassPropertyID != "-1") listParams.Add(Config.Conn().CreateDbParameter("@ClassPropertyID", strClassPropertyID));
                if (strTemplateName != "") listParams.Add(Config.Conn().CreateDbParameter("@TemplateName", "%" + strTemplateName + "%"));
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
                TempUrl.Append("drpClassPropertyID=" + Server.UrlEncode(strClassPropertyID) + "&");
                TempUrl.Append("txtTemplateName=" + Server.UrlEncode(strTemplateName) + "&");
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
                //栏目属性
                Factory.Acc().DataBind("select * from t_ClassProperty where IsClose=0 order by ListID asc", null,Config.DataBindObjTypeCollection.DropDownList.ToString(), drpClassPropertyID, "PropertyName", "ClassPropertyID");
                drpClassPropertyID.Items.Insert(0, new ListItem("请选择", "-1"));
                
                if (ClassTemplateID == "0")
                {
                    GetData.LimitChkMsg("ClassTemplateAdd");
                    lblTitle.Text = "添加";
                    string strListID = Factory.ClassTemplate().GetListID();
                    txtListID.Text = strListID;
                    hidlistID.Value = strListID;
                }
                else
                {
                    GetData.LimitChkMsg("ClassTemplateEdit");
                    lblTitle.Text = "修改";
                    ShowInfo();
                }
            }
        }
        //保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ClassTemplateModel claTempModel = new ClassTemplateModel();
            string strOldListID = hidlistID.Value;
            claTempModel.ClassPropertyID = drpClassPropertyID.SelectedValue;
            claTempModel.TemplateName = txtTemplateName.Text.Trim();
            claTempModel.TemplatePath = txtTemplatePath.Text.Trim();
            claTempModel.ListID = txtListID.Text.Trim();
            claTempModel.AdminID = Session["AdminID"].ToString();
            claTempModel.AddTime = DateTime.Now.ToString();
            claTempModel.IsClose = radIsClose.SelectedValue;
            if (ClassTemplateID == "0")
            {
                Factory.ClassTemplate().OrderInfo(claTempModel.ListID, strOldListID);
                Factory.ClassTemplate().InsertInfo(claTempModel);
                Factory.AdminLog().InsertLog("添加名称为" + claTempModel.TemplateName + "的栏目模板!", Session["AdminID"].ToString());
                Config.MsgGotoUrl("添加成功！", "ClassTemplate.aspx");
            }
            else
            {
                ClassTemplateModel claTempModel_2 = new ClassTemplateModel();
                claTempModel_2 = Factory.ClassTemplate().GetInfo(ClassTemplateID);
                if (claTempModel_2 != null)
                {
                    if (GetData.CheckAdminID(claTempModel_2.AdminID, "ClassTemplateAll"))//检查创建者
                    {
                        Factory.ClassTemplate().OrderInfo(claTempModel.ListID, strOldListID);
                        Factory.ClassTemplate().UpdateInfo(claTempModel, ClassTemplateID);
                        Factory.AdminLog().InsertLog("修改编号为" + ClassTemplateID + "的栏目模板!", Session["AdminID"].ToString());
                        Config.MsgGotoUrl("修改成功！", "ClassTemplate.aspx?" + UrlOrderPara + UrlPara + "page=" + page);
                    }
                }
            }
        }
        //显示数据
        protected void ShowInfo()
        {
            ClassTemplateModel claTempModel = new ClassTemplateModel();
            claTempModel = Factory.ClassTemplate().GetInfo(ClassTemplateID);
            if (claTempModel != null)
            {
                if (GetData.CheckAdminID(claTempModel.AdminID, "ClassTemplateAll"))//检查创建者
                {
                    Config.setDefaultSelected(drpClassPropertyID, claTempModel.ClassPropertyID);
                    txtTemplateName.Text = claTempModel.TemplateName;
                    txtTemplatePath.Text = claTempModel.TemplatePath;
                    txtListID.Text = claTempModel.ListID;
                    hidlistID.Value = claTempModel.ListID;
                    //txtAdminID.Text = claTempModel.AdminID;
                    //txtAddTime.Text = claTempModel.AddTime;
                    radIsClose.ClearSelection();
                    Config.setDefaultSelected(radIsClose, claTempModel.IsClose);
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
