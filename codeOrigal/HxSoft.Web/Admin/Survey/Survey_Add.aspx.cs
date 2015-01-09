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

namespace HxSoft.Web.Admin.Survey
{
    public partial class Survey_Add : System.Web.UI.Page
    {
        /// <summary>
        ///在线调查
        /// 创建人:杨小明
        /// 日期:2011-12-26
        /// </summary>
        //定义全局变量
        public string SurveyID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["SurveyID"], 0).ToString();
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
                return Config.Request(Request["OrderKey"], "SurveyID");
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
        public string strClassID
        {
            get
            {
                return Config.Request(Request["drpClassID"], "-1");
            }
        }
        public string strSubject
        {
            get
            {
                return Config.Request(Request["txtSubject"], "");
            }
        }
        public string strIsRecommend
        {
            get
            {
                return Config.Request(Request["radIsRecommend"], "-1");
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
                if (strClassID != "-1") TempSql.Append(" and (ClassID=@ClassID or ClassID in (" + Factory.Class().GetSubClassSql(strClassID) + "))");
                if (strSubject != "") TempSql.Append(" and Subject like @Subject");
                if (strIsRecommend != "-1") TempSql.Append(" and IsRecommend =@IsRecommend");
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
                if (strClassID != "-1") listParams.Add(Config.Conn().CreateDbParameter("@ClassID", strClassID));
                if (strSubject != "") listParams.Add(Config.Conn().CreateDbParameter("@Subject", "%" + strSubject + "%"));
                if (strIsRecommend != "-1") listParams.Add(Config.Conn().CreateDbParameter("@IsRecommend", strIsRecommend));
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
                TempUrl.Append("drpClassID=" + Server.UrlEncode(strClassID) + "&");
                TempUrl.Append("txtSubject=" + Server.UrlEncode(strSubject) + "&");
                TempUrl.Append("radIsRecommend=" + Server.UrlEncode(strIsRecommend) + "&");
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
                //站点列表
                GetData.GetConfigList(drpConfigID, "LanguageVer", "ConfigID");
                this.drpConfigID.Items.Insert(0, new ListItem("请选择", "-1"));
                
                //调查分类
                //Factory.Class().ShowSelectTree("0", drpClassID, "", Config.SysSurveyMouldID);
                drpClassID.Items.Insert(0, new ListItem("请选择", "-1"));
                
                if (SurveyID == "0")
                {
                    GetData.LimitChkMsg("SurveyAdd");
                    lblTitle.Text = "添加";
                    string strListID = Factory.Survey().GetListID();
                    txtListID.Text = strListID;
                    hidlistID.Value = strListID;
                }
                else
                {
                    GetData.LimitChkMsg("SurveyEdit");
                    lblTitle.Text = "修改";
                    ShowInfo();
                }
            }
        }
        //保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SurveyModel surModel = new SurveyModel();
            string strOldListID = hidlistID.Value;
            surModel.ClassID = drpClassID.SelectedValue;
            surModel.Subject = txtSubject.Text.Trim();
            surModel.IntrContent = Config.HTMLCls(txtIntrContent.Text.Trim());
            if (chkIsRecommend.Checked)
            {
                surModel.IsRecommend = "1";
            }
            else
            {
                surModel.IsRecommend = "0";
            }
            surModel.ListID = txtListID.Text.Trim();
            surModel.ClickNum = txtClickNum.Text.Trim();
            surModel.AdminID = Session["AdminID"].ToString();
            surModel.AddTime = DateTime.Now.ToString();
            surModel.IsClose = radIsClose.SelectedValue;
            if (SurveyID == "0")
            {
                Factory.Survey().OrderInfo(surModel.ListID, strOldListID);
                Factory.Survey().InsertInfo(surModel);
                Factory.AdminLog().InsertLog("添加名称为" + surModel.Subject + "的在线调查!", Session["AdminID"].ToString());
                Config.MsgGotoUrl("添加成功！", "Survey.aspx");
            }
            else
            {
                SurveyModel surModel_2 = new SurveyModel();
                surModel_2 = Factory.Survey().GetInfo(SurveyID);
                if (surModel_2 != null)
                {
                    if (GetData.CheckAdminID(surModel_2.AdminID, "SurveyAll"))//检查创建者
                    {
                        Factory.Survey().OrderInfo(surModel.ListID, strOldListID);
                        Factory.Survey().UpdateInfo(surModel, SurveyID);
                        Factory.AdminLog().InsertLog("修改编号为" + SurveyID + "的在线调查!", Session["AdminID"].ToString());
                        Config.MsgGotoUrl("修改成功！", "Survey.aspx?" + UrlOrderPara + UrlPara + "page=" + page);
                    }
                }
            }
        }
        //显示数据
        protected void ShowInfo()
        {
            SurveyModel surModel = new SurveyModel();
            surModel = Factory.Survey().GetInfo(SurveyID);
            if (surModel != null)
            {
                if (GetData.CheckAdminID(surModel.AdminID, "SurveyAll"))//检查创建者
                {
                    ClassModel claModel = Factory.Class().GetInfo(surModel.ClassID);
                    if (claModel != null)
                    {
                        Config.setDefaultSelected(drpConfigID, claModel.ConfigID);
                        Factory.Class().ShowSelectTree("0", drpClassID, " and ConfigID=" + claModel.ConfigID, Config.SysSurveyMouldID);
                    }
                    Config.setDefaultSelected(drpClassID, surModel.ClassID);
                    txtSubject.Text = surModel.Subject;
                    txtIntrContent.Text = Config.HTMLToTextarea(surModel.IntrContent);
                    if (surModel.IsRecommend == "1")
                    {
                        chkIsRecommend.Checked = true;
                    }
                    txtClickNum.Text = surModel.ClickNum;
                    txtListID.Text = surModel.ListID;
                    hidlistID.Value = surModel.ListID;
                    //txtAdminID.Text = surModel.AdminID;
                    //txtAddTime.Text = surModel.AddTime;
                    radIsClose.ClearSelection();
                    Config.setDefaultSelected(radIsClose, surModel.IsClose);
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

        //栏目分类
        protected void drpConfigID_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpClassID.Items.Clear();
            Factory.Class().ShowSelectTree("0", drpClassID, " and ConfigID=" + drpConfigID.SelectedValue, Config.SysSurveyMouldID);
            drpClassID.Items.Insert(0, new ListItem("请选择", "-1"));
        }

    }
}
