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
    public partial class Dictionary_Move : System.Web.UI.Page
    {
        /// <summary>
        /// 创建人:杨小明
        /// 日期:2010-12-6
        /// </summary>
        //定义全局变量
        public string DictionaryID
        {
            get
            {
                return Config.Request(Request.QueryString["DictionaryID"], "0");
            }
        }
        public string ParentID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["ParentID"], 0).ToString();
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
                return Config.Request(Request["OrderKey"], "ListID");
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
        public string strDictionaryName
        {
            get
            {
                return Config.Request(Request["txtDictionaryName"], "");
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
                if (strDictionaryName != "") TempSql.Append(" and DictionaryName like @DictionaryName");
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
                if (strDictionaryName != "") listParams.Add(Config.Conn().CreateDbParameter("@DictionaryName", "%" + strDictionaryName + "%"));
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
                TempUrl.Append("txtDictionaryName=" + Server.UrlEncode(strDictionaryName) + "&");
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
                GetData.LimitChkMsg("DictionaryMove");
                if (DictionaryID == "0")
                {
                    Config.ShowEnd("请选择要操作的记录!");
                }
                else
                {
                    ShowInfo();
                }
            }
        }
        //保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            StringBuilder strTempDictionaryID = new StringBuilder();
            DictionaryModel dictModel = new DictionaryModel();
            dictModel.ParentID = drpParentID.SelectedValue;
            string[] arrDictionaryID = hidDictionaryID.Value.Split(new char[] { ',' });
            int n = 0;
            for (int i = 0; i < arrDictionaryID.Length; i++)
            {
                DictionaryModel dictModel_2 = new DictionaryModel();
                dictModel_2 = Factory.Dictionary().GetInfo(arrDictionaryID[i]);
                if (dictModel_2 != null)
                {
                    if (GetData.CheckAdminID(dictModel_2.AdminID, "DictionaryAll"))//检查创建者
                    {
                        //父级不一样,取新父级排序
                        if (dictModel.ParentID != dictModel_2.ParentID)
                        {
                            dictModel.ListID = Factory.Dictionary().GetListID(dictModel.ParentID);
                        }
                        else
                        {
                            dictModel.ListID = dictModel_2.ListID;
                        }
                        Factory.Dictionary().MoveInfo(dictModel, arrDictionaryID[i]);
                        Factory.Dictionary().UpdateChildNum(dictModel.ParentID, dictModel_2.ParentID);
                        strTempDictionaryID.Append(arrDictionaryID[i]);
                        if (i + 1 < arrDictionaryID.Length) strTempDictionaryID.Append(",");
                        n++;
                    }
                }
            }
            if (n > 0)
            {
                Factory.AdminLog().InsertLog("移动编号为" + strTempDictionaryID.ToString() + "的字典!", Session["AdminID"].ToString());
                Config.MsgGotoUrl("编号为" + strTempDictionaryID.ToString() + "字典移动成功!", "Dictionary.aspx?ParentID=" + dictModel.ParentID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
            else
            {
                Config.MsgGoBack("操作失败!");
            }
        }


        //显示数据
        protected void ShowInfo()
        {
            DictionaryModel dictModel = new DictionaryModel();
            string[] arrDictionaryID = DictionaryID.Split(new char[] { ','});
            for (int i = 0; i < arrDictionaryID.Length; i++)
            {
                dictModel = Factory.Dictionary().GetInfo(arrDictionaryID[i]);
                if (dictModel != null)
                {
                    if (GetData.CheckAdminID(dictModel.AdminID, "DictionaryAll"))//检查创建者
                    {
                        lblDictionaryName.Text = lblDictionaryName.Text + dictModel.DictionaryName;
                        hidDictionaryID.Value=hidDictionaryID.Value+arrDictionaryID[i];
                        if (i + 1 < arrDictionaryID.Length)
                        {
                            lblDictionaryName.Text = lblDictionaryName.Text + "，";
                            hidDictionaryID.Value = hidDictionaryID.Value + ",";
                        }
                    }
                }
            }
            Factory.Dictionary().ShowSelectTree("0", drpParentID, " and ParentID not in(" + DictionaryID + ") and DictionaryID not in(" + DictionaryID + ")");
            drpParentID.Items.Insert(0, new ListItem("根结点", "0"));
            drpParentID.Attributes.Add("size", "20");
            Config.setDefaultSelected(drpParentID, ParentID);
        }
    }
}
