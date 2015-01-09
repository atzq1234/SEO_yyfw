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

namespace HxSoft.Web.Admin.Video
{
    public partial class Video_Add : System.Web.UI.Page
    {
        /// <summary>
        ///视频管理
        /// 创建人:
        /// 日期:2012-9-19
        /// </summary>
        //定义全局变量
        public string VideoID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["VideoID"], 0).ToString();
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
                return Config.Request(Request["OrderKey"], "VideoID");
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
        public string strTitle
        {
            get
            {
                return Config.Request(Request["txtTitle"], "");
            }
        }
        #endregion
        #region ****查询语句****
        public string SqlQuery
        {
            get
            {
                StringBuilder TempSql = new StringBuilder("");
                if (strClassID != "-1") TempSql.Append(" and ClassID = @ClassID");
                if (strTitle != "") TempSql.Append(" and Title like @Title");
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
                if (strClassID != "-1") listParams.Add(Config.Conn().CreateDbParameter("@ClassID", strClassID));
                if (strTitle != "") listParams.Add(Config.Conn().CreateDbParameter("@Title", "%" + strTitle + "%"));
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
                TempUrl.Append("txtTitle=" + Server.UrlEncode(strTitle) + "&");
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

                //栏目分类
                //Factory.Class().ShowSelectTree("0", drpClassID, "", Config.SysVideoMouldID);
                drpClassID.Items.Insert(0, new ListItem("请选择", "-1"));

                if (VideoID == "0")
                {
                    GetData.LimitChkMsg("VideoAdd");
                    lblTitle.Text = "添加";
                    string strListID = Factory.Video().GetListID();
                    txtListID.Text = strListID;
                    hidlistID.Value = strListID;
                }
                else
                {
                    GetData.LimitChkMsg("VideoEdit");
                    lblTitle.Text = "修改";
                    ShowInfo();
                }
            }
        }
        //保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            VideoModel vidModel = new VideoModel();
            string strOldListID = hidlistID.Value;
            vidModel.ClassID = drpClassID.SelectedValue;
            vidModel.Title = txtTitle.Text.Trim();
            vidModel.VideoPic = txtVideoPic.Text.Trim();
            vidModel.VideoPath = txtVideoPath.Text.Trim();
            vidModel.Description = Config.HTMLCls(txtDescription.Text.Trim());
            vidModel.ListID = txtListID.Text.Trim();
            vidModel.AdminID = Session["AdminID"].ToString();
            vidModel.IsClose = radIsClose.SelectedValue;
            vidModel.AddTime = DateTime.Now.ToString();
            if (VideoID == "0")
            {
                Factory.Video().OrderInfo(vidModel.ListID, strOldListID);
                Factory.Video().InsertInfo(vidModel);
                Factory.AdminLog().InsertLog("添加名称为" + vidModel.Title + "的视频!", Session["AdminID"].ToString());
                Config.MsgGotoUrl("添加成功！", "Video.aspx");
            }
            else
            {
                VideoModel vidModel_2 = new VideoModel();
                vidModel_2 = Factory.Video().GetInfo(VideoID);
                if (vidModel_2 != null)
                {
                    if (GetData.CheckAdminID(vidModel_2.AdminID, "VideoAll"))//检查创建者
                    {
                        Factory.Video().OrderInfo(vidModel.ListID, strOldListID);
                        Factory.Video().UpdateInfo(vidModel, VideoID);
                        Factory.AdminLog().InsertLog("修改编号为" + VideoID + "的视频!", Session["AdminID"].ToString());
                        Config.MsgGotoUrl("修改成功！", "Video.aspx?" + UrlOrderPara + UrlPara + "page=" + page);
                    }
                }
            }
        }
        //显示数据
        protected void ShowInfo()
        {
            VideoModel vidModel = new VideoModel();
            vidModel = Factory.Video().GetInfo(VideoID);
            if (vidModel != null)
            {
                if (GetData.CheckAdminID(vidModel.AdminID, "VideoAll"))//检查创建者
                {

                    ClassModel claModel = Factory.Class().GetInfo(vidModel.ClassID);
                    if (claModel != null)
                    {
                        Config.setDefaultSelected(drpConfigID, claModel.ConfigID);
                        Factory.Class().ShowSelectTree("0", drpClassID, " and ConfigID=" + claModel.ConfigID, Config.SysVideoMouldID);
                    }
                    Config.setDefaultSelected(drpClassID, vidModel.ClassID);
                    txtTitle.Text = vidModel.Title;
                    txtVideoPic.Text = vidModel.VideoPic;
                    txtVideoPath.Text = vidModel.VideoPath;
                    txtDescription.Text = Config.HTMLToTextarea(vidModel.Description);
                    txtListID.Text = vidModel.ListID;
                    hidlistID.Value = vidModel.ListID;
                    //txtAdminID.Text = artPicModel.AdminID;
                    //txtAddTime.Text = artPicModel.AddTime;
                    radIsClose.ClearSelection();
                    Config.setDefaultSelected(radIsClose, vidModel.IsClose);
                }
                else
                {
                    Config.ShowEnd("您没有查看此视频的权限！");
                }
            }
            else
            {
                Config.ShowEnd("您没有查看此视频的权限！");
            }
        }

        //栏目分类
        protected void drpConfigID_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpClassID.Items.Clear();
            Factory.Class().ShowSelectTree("0", drpClassID, " and ConfigID=" + drpConfigID.SelectedValue, Config.SysVideoMouldID);
            drpClassID.Items.Insert(0, new ListItem("请选择", "-1"));
        }

    }
}
