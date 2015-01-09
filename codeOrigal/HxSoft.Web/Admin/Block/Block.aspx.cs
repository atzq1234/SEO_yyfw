using System;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
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

namespace HxSoft.Web.Admin.Block
{
    public partial class Block : System.Web.UI.Page
    {
        /// <summary>
        ///片段内容管理
        /// 创建人:Admin
        /// 日期:2012-10-17
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
                return Config.Request(Request["OrderKey"], "BlockID");
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
        public string strTitle
        {
            get
            {
                return Config.Request(Request["txtTitle"], "");
            }
        }
        public string strBlockContent
        {
            get
            {
                return Config.Request(Request["txtBlockContent"], "");
            }
        }
        #endregion
        #region ****查询语句****
        public string SqlQuery
        {
            get
            {
                StringBuilder TempSql = new StringBuilder("");
                if (strTitle != "") TempSql.Append(" and Title like @Title");
                if (strBlockContent != "") TempSql.Append(" and BlockContent like @BlockContent");
                return TempSql.ToString();
            }
        }
        #endregion
        #region ****DbParameter参数****
        public DbParameter[] SqlParams
        {
            get
            {
                List<DbParameter> listParams = new List<DbParameter>();
                if (strTitle != "") listParams.Add(Config.Conn().CreateDbParameter("@Title", "%"+strTitle+"%"));
                if (strBlockContent != "") listParams.Add(Config.Conn().CreateDbParameter("@BlockContent", "%" + strBlockContent + "%"));
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
                TempUrl.Append("txtTitle=" + Server.UrlEncode(strTitle) + "&");
                TempUrl.Append("txtBlockContent=" + Server.UrlEncode(strBlockContent) + "&");
                return TempUrl.ToString();
            }
        }
        #endregion
        //页面初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            GetData.LimitChkMsg("Block");
            lbtnAdd.Visible = GetData.LimitChk("BlockAdd");
            lbtnEdit.Visible = GetData.LimitChk("BlockEdit");
            lbtnDel.Visible = GetData.LimitChk("BlockDel");
            lbtnOpen.Visible = GetData.LimitChk("BlockOpen");
            lbtnClose.Visible = GetData.LimitChk("BlockClose");
            if (!Page.IsPostBack)
            {
                
                Repeater_Bind(repList);
            }
        }
        //绑定数据
        protected void Repeater_Bind(Repeater rep)
        {
            string sql = "select * from t_Block where 1=1 " + SqlQuery + SqlOrder;
            pager.InnerHtml = Factory.Acc().DataPageBind(sql, SqlParams, Config.DataBindObjTypeCollection.Repeater.ToString(), rep, 10, page, "?" + UrlOrderPara + UrlPara).ToString();
        }
        //修改
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string strBlockID = Config.Request(Request.Form["BlockID"], "0");
            if (strBlockID != "0")
            {
                Response.Redirect("Block_Add.aspx?BlockID=" + strBlockID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }
        //删除
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string strBlockID = Config.Request(Request.Form["BlockID"], "0");
            if (strBlockID != "0")
            {
                string[] arrBlockID = strBlockID.Split(new char[] { ',' });
                StringBuilder strTempBlockID = new StringBuilder();
                BlockModel bloModel = new BlockModel();
                int n = 0;
                for (int i = 0; i < arrBlockID.Length; i++)
                {
                    bloModel = Factory.Block().GetInfo(arrBlockID[i]);
                    if (bloModel != null)
                    {
                        if (GetData.CheckAdminID(bloModel.AdminID, "BlockAll"))//检查创建者
                        {
                            Factory.Block().DeleteInfo(arrBlockID[i]);
                            strTempBlockID.Append(arrBlockID[i]);
                            if (i + 1 < arrBlockID.Length) strTempBlockID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("删除编号为" + strTempBlockID.ToString() + "的片段内容!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempBlockID.ToString() + "的片段内容删除成功!", "Block.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strBlockID = Config.Request(Request.Form["BlockID"], "0");
            if (strBlockID != "0")
            {
                string[] arrBlockID = strBlockID.Split(new char[] { ',' });
                StringBuilder strTempBlockID = new StringBuilder();
                BlockModel bloModel = new BlockModel();
                int n = 0;
                for (int i = 0; i < arrBlockID.Length; i++)
                {
                    bloModel = Factory.Block().GetInfo(arrBlockID[i]);
                    if (bloModel != null)
                    {
                        if (GetData.CheckAdminID(bloModel.AdminID, "BlockAll"))//检查创建者
                        {
                            Factory.Block().UpdateCloseStatus(arrBlockID[i], "0");
                            strTempBlockID.Append(arrBlockID[i]);
                            if (i + 1 < arrBlockID.Length) strTempBlockID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("开放编号为" + strTempBlockID.ToString() + "的片段内容!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempBlockID.ToString() + "的片段内容开放成功!", "Block.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
                }
                else
                {
                    Config.MsgGoBack("操作失败!");
                }
            }
        }

        //关闭
        protected void btnClose_Click(object sender, EventArgs e)
        {
            string strBlockID = Config.Request(Request.Form["BlockID"], "0");
            if (strBlockID != "0")
            {
                string[] arrBlockID = strBlockID.Split(new char[] { ',' });
                StringBuilder strTempBlockID = new StringBuilder();
                BlockModel bloModel = new BlockModel();
                int n = 0;
                for (int i = 0; i < arrBlockID.Length; i++)
                {
                    bloModel = Factory.Block().GetInfo(arrBlockID[i]);
                    if (bloModel != null)
                    {
                        if (GetData.CheckAdminID(bloModel.AdminID, "BlockAll"))//检查创建者
                        {
                            Factory.Block().UpdateCloseStatus(arrBlockID[i], "1");
                            strTempBlockID.Append(arrBlockID[i]);
                            if (i + 1 < arrBlockID.Length) strTempBlockID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("关闭编号为" + strTempBlockID.ToString() + "的片段内容!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("编号为" + strTempBlockID.ToString() + "的片段内容关闭成功!", "Block.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
