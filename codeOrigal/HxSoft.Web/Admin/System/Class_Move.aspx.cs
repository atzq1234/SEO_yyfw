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
    public partial class Class_Move : System.Web.UI.Page
    {
        /// <summary>
        /// 创建人:杨小明
        /// 日期:2010-12-6
        /// </summary>
        //定义全局变量
        public string ClassID
        {
            get
            {
                return Config.Request(Request.QueryString["ClassID"], "0");
            }
        }
        public int page
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["page"], 1);
            }
        }
        public string ParentID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["ParentID"], 0).ToString();
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
        public string strClassName
        {
            get
            {
                return Config.Request(Request["txtClassName"], "");
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
                if (strClassName != "") TempSql.Append(" and ClassName like @ClassName");
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
                if (strClassName != "") listParams.Add(Config.Conn().CreateDbParameter("@ClassName", "%" + strClassName + "%"));
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
                TempUrl.Append("txtClassName=" + Server.UrlEncode(strClassName) + "&");
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
                GetData.LimitChkMsg("ClassMove");
                if (ClassID == "0")
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
            StringBuilder strTempClassID = new StringBuilder();
            ClassModel claModel = new ClassModel();
            claModel.ParentID = drpParentID.SelectedValue;
            string[] arrClassID = hidClassID.Value.Split(new char[] { ',' });
            int n = 0;
            for (int i = 0; i < arrClassID.Length; i++)
            {
                ClassModel claModel_2 = new ClassModel();
                claModel_2 = Factory.Class().GetInfo(arrClassID[i]);
                if (claModel_2 != null)
                {
                    if (GetData.CheckAdminID(claModel_2.AdminID, "ClassAll"))//检查创建者
                    {
                        //父级不一样,取新父级排序
                        if (claModel.ParentID != claModel_2.ParentID)
                        {
                            claModel.ListID = Factory.Class().GetListID(claModel.ParentID);
                        }
                        else
                        {
                            claModel.ListID = claModel_2.ListID;
                        }
                        Factory.Class().MoveInfo(claModel, arrClassID[i]);
                        Factory.Class().UpdateChildNum(claModel.ParentID, claModel_2.ParentID);
                        strTempClassID.Append(arrClassID[i]);
                        if (i + 1 < arrClassID.Length) strTempClassID.Append(",");
                        n++;
                    }
                }
            }
            if (n > 0)
            {
                Factory.AdminLog().InsertLog("移动编号为" + strTempClassID.ToString() + "的栏目!", Session["AdminID"].ToString());
                Config.MsgGotoUrl("编号为" + strTempClassID.ToString() + "栏目移动成功!", "Class.aspx?ParentID=" + claModel.ParentID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
            else
            {
                Config.MsgGoBack("操作失败!");
            }
        }


        //显示数据
        protected void ShowInfo()
        {
            ClassModel claModel = new ClassModel();
            string[] arrClassID = ClassID.Split(new char[] { ','});
            for (int i = 0; i < arrClassID.Length; i++)
            {
                claModel = Factory.Class().GetInfo(arrClassID[i]);
                if (claModel != null)
                {
                    if (GetData.CheckAdminID(claModel.AdminID, "ClassAll"))//检查创建者
                    {
                        lblClassName.Text = lblClassName.Text + claModel.ClassName;
                        hidClassID.Value=hidClassID.Value+arrClassID[i];
                        if (i + 1 < arrClassID.Length)
                        {
                            lblClassName.Text = lblClassName.Text + "，";
                            hidClassID.Value = hidClassID.Value + ",";
                        }
                    }
                }
            }
            Factory.Class().ShowSelectTree("0",drpParentID, " and ParentID not in(" + ClassID + ") and ClassID not in(" + ClassID + ")","-1");
            drpParentID.Items.Insert(0,new ListItem("根结点","0"));
            drpParentID.Attributes.Add("size","20");
            Config.setDefaultSelected(drpParentID, ParentID);
        }
    }
}
