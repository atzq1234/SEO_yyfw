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
    public partial class SurveyItem_Add : System.Web.UI.Page
    {
        /// <summary>
        ///调查选顶 
        /// 创建人:杨小明
        /// 日期:2011-12-26
        /// </summary>
        //定义全局变量
        public string SurveyItemID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["SurveyItemID"], 0).ToString();
            }
        }
        public int page
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["page"], 1);
            }
        }
        public string SurveyID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["SurveyID"], 0).ToString();
            }
        }
        public int ProductPage
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["ProductPage"], 1);
            }
        }
        #region ****排序参数****
        public string strOrderKey
        {
            get
            {
                return Config.Request(Request["OrderKey"], "SurveyItemID");
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
        public string strItemName
        {
            get
            {
                return Config.Request(Request["txtItemName"], "");
            }
        }
        public string strTypeID
        {
            get
            {
                return Config.Request(Request["drpTypeID"], "-1");
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
                if (strItemName != "") TempSql.Append(" and ItemName like @ItemName");
                if (strTypeID != "-1") TempSql.Append(" and TypeID =@TypeID");
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
                if (strItemName != "") listParams.Add(Config.Conn().CreateDbParameter("@ItemName", "%" + strItemName + "%"));
                if (strTypeID != "-1") listParams.Add(Config.Conn().CreateDbParameter("@TypeID", strTypeID));
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
                TempUrl.Append("txtItemName=" + Server.UrlEncode(strItemName) + "&");
                TempUrl.Append("drpTypeID=" + Server.UrlEncode(strTypeID) + "&");
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
                gvSurveyItemOption_Bind();
                if (SurveyItemID == "0")
                {
                    GetData.LimitChkMsg("SurveyItemAdd");
                    lblTitle.Text = "添加";
                    string strListID = Factory.SurveyItem().GetListID(SurveyID);
                    txtListID.Text = strListID;
                    hidlistID.Value = strListID;
                }
                else
                {
                    GetData.LimitChkMsg("SurveyItemEdit");
                    lblTitle.Text = "修改";
                    ShowInfo();
                }
            }
        }
        //保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SurveyItemModel surItModel = new SurveyItemModel();
            string strOldListID = hidlistID.Value;
            surItModel.ItemName = txtItemName.Text.Trim();
            surItModel.TypeID = drpTypeID.SelectedValue;
            surItModel.SurveyID = SurveyID;
            surItModel.ListID = txtListID.Text.Trim();
            surItModel.AdminID = Session["AdminID"].ToString();
            surItModel.AddTime = DateTime.Now.ToString();
            surItModel.IsClose = radIsClose.SelectedValue;
            if (SurveyItemID == "0")
            {
                Factory.SurveyItem().OrderInfo(SurveyID,surItModel.ListID, strOldListID);
                Factory.SurveyItem().InsertInfo(surItModel);
                string strSurveyItemID = Factory.SurveyItem().GetSurveyItemID();
                //更新ProductID
                Factory.SurveyItemOption().UpdateSurveyItemID(strSurveyItemID, Session["AdminID"].ToString());
                Factory.AdminLog().InsertLog("添加名称为" + surItModel.ItemName + "的调查选项!", Session["AdminID"].ToString());
                Config.MsgGotoUrl("添加成功！", "SurveyItem.aspx?SurveyID=" + SurveyID + "&ProductPage=" + ProductPage);
            }
            else
            {
                SurveyItemModel surItModel_2 = new SurveyItemModel();
                surItModel_2 = Factory.SurveyItem().GetInfo(SurveyItemID);
                if (surItModel_2 != null)
                {
                    if (GetData.CheckAdminID(surItModel_2.AdminID, "SurveyItemAll"))//检查创建者
                    {
                        Factory.SurveyItem().OrderInfo(SurveyID, surItModel.ListID, strOldListID);
                        Factory.SurveyItem().UpdateInfo(surItModel, SurveyItemID);
                        Factory.AdminLog().InsertLog("修改编号为" + SurveyItemID + "的调查选项!", Session["AdminID"].ToString());
                        Config.MsgGotoUrl("修改成功！", "SurveyItem.aspx?SurveyID=" + SurveyID + "&ProductPage=" + ProductPage + "&" + UrlOrderPara + UrlPara + "page=" + page);
                    }
                }
            }
        }
        //显示数据
        protected void ShowInfo()
        {
            SurveyItemModel surItModel = new SurveyItemModel();
            surItModel = Factory.SurveyItem().GetInfo(SurveyItemID);
            if (surItModel != null)
            {
                if (GetData.CheckAdminID(surItModel.AdminID, "SurveyItemAll"))//检查创建者
                {
                    txtItemName.Text = surItModel.ItemName;
                    Config.setDefaultSelected(drpTypeID, surItModel.TypeID);
                    //txtSurveyID.Text = surItModel.SurveyID;
                    txtListID.Text = surItModel.ListID;
                    hidlistID.Value = surItModel.ListID;
                    //txtAdminID.Text = surItModel.AdminID;
                    //txtAddTime.Text = surItModel.AddTime;
                    radIsClose.ClearSelection();
                    Config.setDefaultSelected(radIsClose, surItModel.IsClose);
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


        #region 选项集合
        //选项集合
        private void gvSurveyItemOption_Bind()
        {
            string strSql;
            if (SurveyItemID == "0")
            {
                strSql = " and SurveyItemID=0 and AdminID=" + Session["AdminID"].ToString();
            }
            else
            {
                strSql = " and SurveyItemID=" + SurveyItemID;
            }
            string sql = "select * from t_SurveyItemOption where 1=1" + strSql + " order by ListID asc";
            gvSurveyItemOption.DataKeyNames = new string[] { "SurveyItemOptionID" };
            Factory.Acc().DataBind(sql,null,Config.DataBindObjTypeCollection.GridView.ToString(),gvSurveyItemOption);
        }

        //编辑
        protected void gvSurveyItemOption_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvSurveyItemOption.EditIndex = e.NewEditIndex;
            gvSurveyItemOption_Bind();
        }
        //取消
        protected void gvSurveyItemOption_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvSurveyItemOption.EditIndex = -1;
            gvSurveyItemOption_Bind();
        }
        //更新
        protected void gvSurveyItemOption_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string SurveyItemOptionID = gvSurveyItemOption.DataKeys[e.RowIndex].Value.ToString();
            SurveyItemOptionModel surChItModel = new SurveyItemOptionModel();
            surChItModel.SurveyItemID = SurveyItemID;
            surChItModel.ItemOptionName = ((TextBox)gvSurveyItemOption.Rows[e.RowIndex].Cells[0].FindControl("ItemOptionName")).Text.Trim();
            surChItModel.ListID = ((TextBox)gvSurveyItemOption.Rows[e.RowIndex].Cells[1].FindControl("ListID")).Text.Trim();
            string strOldListID = ((HiddenField)gvSurveyItemOption.Rows[e.RowIndex].Cells[1].FindControl("OldListID")).Value.Trim();
            if (!Config.IsNumeric(surChItModel.ListID))
            {
                surChItModel.ListID = strOldListID;
            }
            surChItModel.AdminID = Session["AdminID"].ToString();
            Factory.SurveyItemOption().OrderInfo(SurveyItemID, surChItModel.ListID, strOldListID);
            Factory.SurveyItemOption().UpdateInfo(surChItModel, SurveyItemOptionID);
            gvSurveyItemOption.EditIndex = -1;
            gvSurveyItemOption_Bind();
        }
        //删除
        protected void gvSurveyItemOption_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string SurveyItemOptionID = gvSurveyItemOption.DataKeys[e.RowIndex].Value.ToString();
            Factory.SurveyItemOption().DeleteInfo(SurveyItemOptionID);
            gvSurveyItemOption.EditIndex = -1;
            gvSurveyItemOption_Bind();
        }

        //添加
        protected void btnAddSurveyItemOption_Click(object sender, EventArgs e)
        {
            SurveyItemOptionModel surChItModel = new SurveyItemOptionModel();
            surChItModel.SurveyItemID = SurveyItemID;
            surChItModel.ItemOptionName = txtItemOptionName.Text.Trim();
            surChItModel.ListID = Factory.SurveyItemOption().GetListID(SurveyItemID);
            surChItModel.AdminID = Session["AdminID"].ToString();
            if (surChItModel.ItemOptionName == string.Empty)
            {
                errSurveyItemOption.Text = "名称不能为空!";
            }
            else
            {
                txtItemOptionName.Text = "";
                errSurveyItemOption.Text = "";
                Factory.SurveyItemOption().InsertInfo(surChItModel);
                gvSurveyItemOption_Bind();
            }
        }
        #endregion

    }
}
