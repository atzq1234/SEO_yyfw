using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using HxSoft.Common;
using HxSoft.Model;
using HxSoft.ClassFactory;
using System.IO;
using System.Data.Common;

namespace HxSoft.Web.Admin.Download
{
    public partial class Download_Add : System.Web.UI.Page
    {
        /// <summary>
        ///下载管理
        /// 创建人:杨小明
        /// 日期:2011-2-24
        /// </summary>
        //定义全局变量
        public string DownloadID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["DownloadID"], 0).ToString();
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
                return Config.Request(Request["OrderKey"], "DownloadID");
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
        public string strDownName
        {
            get
            {
                return Config.Request(Request["txtDownName"], "");
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
                if (strDownName != "") TempSql.Append(" and DownName like @DownName");
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
                if (strDownName != "") listParams.Add(Config.Conn().CreateDbParameter("@DownName", "%" + strDownName + "%"));
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
                TempUrl.Append("txtDownName=" + Server.UrlEncode(strDownName) + "&");
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
                
                //下载分类
                //Factory.Class().ShowSelectTree("0", drpClassID, "", Config.SysDownloadMouldID);
                drpClassID.Items.Insert(0, new ListItem("请选择", "-1"));

                if (DownloadID == "0")
                {
                    GetData.LimitChkMsg("DownloadAdd");
                    lblTitle.Text = "添加";
                    string strListID = Factory.Download().GetListID();
                    txtListID.Text = strListID;
                    hidlistID.Value = strListID;
                }
                else
                {
                    GetData.LimitChkMsg("DownloadEdit");
                    lblTitle.Text = "修改";
                    ShowInfo();
                }
            }
        }
        //保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DownloadModel dowModel = new DownloadModel();
            string strOldListID = hidlistID.Value;
            dowModel.ClassID = drpClassID.SelectedValue;
            dowModel.DownName = txtDownName.Text.Trim();
            dowModel.DownPic = txtDownPic.Text.Trim();
            dowModel.DownUrl = txtDownUrl.Text.Trim();
            string strDownSize = txtDownSize.Text.Trim();
            double Size = 0;
            try
            {
                if (File.Exists(Server.MapPath(dowModel.DownUrl)))
                {
                    FileInfo fi = new FileInfo(Server.MapPath(dowModel.DownUrl));
                    Size = Math.Round(Convert.ToDouble(fi.Length) / 1024, 2);
                    dowModel.DownSize = Size.ToString();
                }
                else
                {
                    dowModel.DownSize = strDownSize;
                }
            }
            catch
            {
                dowModel.DownSize = strDownSize;
            }
            dowModel.Tags = txtTags.Text.Trim();
            dowModel.Keywords = txtKeywords.Text.Trim();
            dowModel.Description = Config.HTMLCls(txtDescription.Text.Trim());
            dowModel.Details = txtDetails.Value;
            dowModel.ClickNum = txtClickNum.Text;
            dowModel.ListID = txtListID.Text.Trim();
            if (chkIsRecommend.Checked)
            {
                dowModel.IsRecommend = "1";
            }
            else
            {
                dowModel.IsRecommend = "0";
            }
            dowModel.AdminID = Session["AdminID"].ToString();
            dowModel.AddTime = DateTime.Now.ToString();
            dowModel.IsClose = radIsClose.Text.Trim();
            if (DownloadID == "0")
            {
                Factory.Download().OrderInfo(dowModel.ListID, strOldListID); 
                Factory.Download().InsertInfo(dowModel);
                Factory.AdminLog().InsertLog("添加名称为\"" + dowModel.DownName + "\"的下载。", Session["AdminID"].ToString());
                Config.MsgGotoUrl("添加成功！", "Download.aspx");

            }
            else
            {
                DownloadModel dowModel_2 = new DownloadModel();
                dowModel_2 = Factory.Download().GetInfo(DownloadID);
                if (dowModel_2 != null)
                {
                    if (GetData.CheckAdminID(dowModel_2.AdminID, "DownloadAll"))//检查创建者
                    {
                        Factory.Download().OrderInfo(dowModel.ListID, strOldListID); 
                        Factory.Download().UpdateInfo(dowModel, DownloadID);
                        Factory.AdminLog().InsertLog("修改编号为" + DownloadID + "的下载。", Session["AdminID"].ToString());
                        Config.MsgGotoUrl("修改成功！", "Download.aspx?" + UrlOrderPara + UrlPara + "page=" + page);
                    }
                }
            }
        }
        //显示数据
        protected void ShowInfo()
        {
            DownloadModel dowModel = new DownloadModel();
            dowModel = Factory.Download().GetInfo(DownloadID);
            if (dowModel != null)
            {
                if (GetData.CheckAdminID(dowModel.AdminID, "DownloadAll"))//检查创建者
                {
                    ClassModel claModel = Factory.Class().GetInfo(dowModel.ClassID);
                    if (claModel != null)
                    {
                        Config.setDefaultSelected(drpConfigID, claModel.ConfigID);
                        Factory.Class().ShowSelectTree("0", drpClassID, " and ConfigID=" + claModel.ConfigID, Config.SysDownloadMouldID);
                    }
                    Config.setDefaultSelected(drpClassID, dowModel.ClassID);
                    txtDownName.Text = dowModel.DownName;
                    txtDownPic.Text = dowModel.DownPic;
                    txtDownUrl.Text = dowModel.DownUrl;
                    txtDownSize.Text = dowModel.DownSize;
                    txtTags.Text = dowModel.Tags;
                    txtKeywords.Text = dowModel.Keywords;
                    txtDescription.Text = Config.HTMLToTextarea(dowModel.Description);
                    txtDetails.Value = dowModel.Details;
                    txtClickNum.Text = dowModel.ClickNum;
                    txtListID.Text = dowModel.ListID;
                    hidlistID.Value = dowModel.ListID;
                    if (dowModel.IsRecommend == "1")
                    {
                        chkIsRecommend.Checked = true;
                    }
                    //txtAdminID.Text = dowModel.AdminID;
                    //txtAddTime.Text = dowModel.AddTime;

                    radIsClose.ClearSelection();
                    Config.setDefaultSelected(radIsClose, dowModel.IsClose);
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
            Factory.Class().ShowSelectTree("0", drpClassID, " and ConfigID=" + drpConfigID.SelectedValue, Config.SysDownloadMouldID);
            drpClassID.Items.Insert(0, new ListItem("请选择", "-1"));
        }

    }
}
