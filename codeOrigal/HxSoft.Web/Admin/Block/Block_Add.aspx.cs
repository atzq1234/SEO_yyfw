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
    public partial class Block_Add : System.Web.UI.Page
    {
        /// <summary>
        ///片段内容管理
        /// 创建人:Admin
        /// 日期:2012-10-17
        /// </summary>
        //定义全局变量
        public string BlockID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["BlockID"], 0).ToString();
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
                if (strTitle != "") listParams.Add(Config.Conn().CreateDbParameter("@Title", "%" + strTitle + "%"));
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
            if (!Page.IsPostBack)
            {
               
                if (BlockID == "0")
                {
                    GetData.LimitChkMsg("BlockAdd");
                    lblTitle.Text = "添加";
                }
                else
                {
                    GetData.LimitChkMsg("BlockEdit");
                    lblTitle.Text = "修改";
                    ShowInfo();
                }
            }
        }
        //保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            BlockModel bloModel = new BlockModel();
            bloModel.Title = txtTitle.Text.Trim();
            bloModel.BlockContent = txtBlockContent.Value;
            bloModel.AdminID = Session["AdminID"].ToString();
            bloModel.IsClose = radIsClose.SelectedValue;
            bloModel.AddTime = DateTime.Now.ToString();
            if (BlockID == "0")
            {
                Factory.Block().InsertInfo(bloModel);
                Factory.AdminLog().InsertLog("添加名称为" + bloModel.Title + "的片段内容!", Session["AdminID"].ToString());
                Config.MsgGotoUrl("添加成功！", "Block.aspx");
            }
            else
            {
                BlockModel bloModel_2 = new BlockModel();
                bloModel_2 = Factory.Block().GetInfo(BlockID);
                if (bloModel_2 != null)
                {
                    if (GetData.CheckAdminID(bloModel_2.AdminID, "BlockAll"))//检查创建者
                    {
                        Factory.Block().UpdateInfo(bloModel, BlockID);
                        Factory.AdminLog().InsertLog("修改编号为" + BlockID + "的片段内容!", Session["AdminID"].ToString());
                        Config.MsgGotoUrl("修改成功！", "Block.aspx?" + UrlOrderPara + UrlPara + "page=" + page);
                    }
                }
            }
        }
        //显示数据
        protected void ShowInfo()
        {
            BlockModel bloModel = new BlockModel();
            bloModel = Factory.Block().GetInfo(BlockID);
            if (bloModel != null)
            {
                if (GetData.CheckAdminID(bloModel.AdminID, "BlockAll"))//检查创建者
                {
                    txtTitle.Text = bloModel.Title;
                    txtBlockContent.Value = bloModel.BlockContent;
                    Config.setDefaultSelected(radIsClose, bloModel.IsClose);
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
