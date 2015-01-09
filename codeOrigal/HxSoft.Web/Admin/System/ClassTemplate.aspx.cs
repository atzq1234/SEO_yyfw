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
    public partial class ClassTemplate : System.Web.UI.Page
    {
        /// <summary>
        ///栏目模板
        /// 创建人:杨小明
        /// 日期:2012-4-16
        /// </summary>
        //定义全局变量
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
            GetData.LimitChkMsg("ClassTemplate");
            lbtnAdd.Visible = GetData.LimitChk("ClassTemplateAdd");
            lbtnEdit.Visible = GetData.LimitChk("ClassTemplateEdit");
            lbtnDel.Visible = GetData.LimitChk("ClassTemplateDel");
            lbtnOpen.Visible = GetData.LimitChk("ClassTemplateOpen");
            lbtnClose.Visible = GetData.LimitChk("ClassTemplateClose");
            if (!Page.IsPostBack)
            {
                //栏目属性
                Factory.Acc().DataBind("select * from t_ClassProperty where IsClose=0 order by ListID asc", null,Config.DataBindObjTypeCollection.DropDownList.ToString(), drpClassPropertyID, "PropertyName", "ClassPropertyID");
                drpClassPropertyID.Items.Insert(0,new ListItem("不限","-1"));

                Repeater_Bind(repList);
            }
        }
        //绑定数据
        protected void Repeater_Bind(Repeater rep)
        {
            string sql = "select * from t_ClassTemplate where 1=1  and ClassPropertyID in(select ClassPropertyID from t_ClassProperty where IsClose=0)" + SqlQuery + SqlOrder;
            pager.InnerHtml = Factory.Acc().DataPageBind(sql, SqlParams, Config.DataBindObjTypeCollection.Repeater.ToString(), rep, 10, page, "?" + UrlOrderPara + UrlPara).ToString();
        }
        //修改
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string strClassTemplateID = Config.Request(Request.Form["ClassTemplateID"], "0");
            if (strClassTemplateID != "0")
            {
                Response.Redirect("ClassTemplate_Add.aspx?ClassTemplateID=" + strClassTemplateID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }
        //删除
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string strClassTemplateID = Config.Request(Request.Form["ClassTemplateID"], "0");
            if (strClassTemplateID != "0")
            {
                string[] arrClassTemplateID = strClassTemplateID.Split(new char[] { ',' });
                StringBuilder strTempClassTemplateID = new StringBuilder();
                ClassTemplateModel claTempModel = new ClassTemplateModel();
                int n = 0;
                for (int i = 0; i < arrClassTemplateID.Length; i++)
                {
                    claTempModel = Factory.ClassTemplate().GetInfo(arrClassTemplateID[i]);
                    if (claTempModel != null)
                    {
                        if (GetData.CheckAdminID(claTempModel.AdminID, "ClassTemplateAll"))//检查创建者
                        {
                            Factory.ClassTemplate().DeleteInfo(arrClassTemplateID[i]);
                            strTempClassTemplateID.Append(arrClassTemplateID[i]);
                            if (i + 1 < arrClassTemplateID.Length) strTempClassTemplateID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("删除编号为" + strTempClassTemplateID.ToString() + "的栏目模板!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempClassTemplateID.ToString() + "的栏目模板删除成功!", "ClassTemplate.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
                }
                else
                {
                    Config.MsgGoBack("操作失败!");
                }
            }
        }

        //开放
        protected void btnOpen_Click(object sender, EventArgs e)
        {
            string strClassTemplateID = Config.Request(Request.Form["ClassTemplateID"], "0");
            if (strClassTemplateID != "0")
            {
                string[] arrClassTemplateID = strClassTemplateID.Split(new char[] { ',' });
                StringBuilder strTempClassTemplateID = new StringBuilder();
                ClassTemplateModel claTempModel = new ClassTemplateModel();
                int n = 0;
                for (int i = 0; i < arrClassTemplateID.Length; i++)
                {
                    claTempModel = Factory.ClassTemplate().GetInfo(arrClassTemplateID[i]);
                    if (claTempModel != null)
                    {
                        if (GetData.CheckAdminID(claTempModel.AdminID, "ClassTemplateAll"))//检查创建者
                        {
                            Factory.ClassTemplate().UpdateCloseStatus(arrClassTemplateID[i], "0");
                            strTempClassTemplateID.Append(arrClassTemplateID[i]);
                            if (i + 1 < arrClassTemplateID.Length) strTempClassTemplateID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("开放编号为" + strTempClassTemplateID.ToString() + "的栏目模板!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempClassTemplateID.ToString() + "的栏目模板开放成功!", "ClassTemplate.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
                }
                else
                {
                    Config.MsgGoBack("操作失败!");
                }
            }
        }

        //删除
        protected void btnClose_Click(object sender, EventArgs e)
        {
            string strClassTemplateID = Config.Request(Request.Form["ClassTemplateID"], "0");
            if (strClassTemplateID != "0")
            {
                string[] arrClassTemplateID = strClassTemplateID.Split(new char[] { ',' });
                StringBuilder strTempClassTemplateID = new StringBuilder();
                ClassTemplateModel claTempModel = new ClassTemplateModel();
                int n = 0;
                for (int i = 0; i < arrClassTemplateID.Length; i++)
                {
                    claTempModel = Factory.ClassTemplate().GetInfo(arrClassTemplateID[i]);
                    if (claTempModel != null)
                    {
                        if (GetData.CheckAdminID(claTempModel.AdminID, "ClassTemplateAll"))//检查创建者
                        {
                            Factory.ClassTemplate().UpdateCloseStatus(arrClassTemplateID[i], "1");
                            strTempClassTemplateID.Append(arrClassTemplateID[i]);
                            if (i + 1 < arrClassTemplateID.Length) strTempClassTemplateID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("关闭编号为" + strTempClassTemplateID.ToString() + "的栏目模板!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempClassTemplateID.ToString() + "的栏目模板关闭成功!", "ClassTemplate.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
                }
                else
                {
                    Config.MsgGoBack("操作失败!");
                }
            }
        }

        //查询
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Repeater_Bind(repList);
        }

    }
}
