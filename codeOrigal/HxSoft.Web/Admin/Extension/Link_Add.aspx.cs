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

namespace HxSoft.Web.Admin.Extension
{
    public partial class Link_Add : System.Web.UI.Page
    {
        /// <summary>
        /// ������:��С��
        /// ����:2010-12-6
        /// </summary>
        //����ȫ�ֱ���
        public string LinkID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["LinkID"], 0).ToString();
            }
        }
        public int page
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["page"], 1);
            }
        }
        #region ****�������****
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
        #region ****�������****
        public string SqlOrder
        {
            get
            {
                return " order by " + strOrderKey + " " + strAscDesc1;
            }
        }
        #endregion
        #region ****Url�������****
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
        #region ****��ѯ����****
        public string strTypeID
        {
            get
            {
                return Config.Request(Request["drpTypeID"], "-1");
            }
        }
        public string strSiteName
        {
            get
            {
                return Config.Request(Request["txtSiteName"], "");
            }
        }
        public string strConfigID
        {
            get
            {
                return Config.Request(Request["drpConfigID"], "-1");
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
        #region ****��ѯ���****
        public string SqlQuery
        {
            get
            {
                StringBuilder TempSql = new StringBuilder("");
                if (strConfigID != "-1") TempSql.Append(" and ConfigID=@ConfigID");
                if (strTypeID != "-1") TempSql.Append(" and TypeID =@TypeID");
                if (strSiteName != "") TempSql.Append(" and SiteName like @SiteName");
                if (strIsClose != "-1") TempSql.Append(" and IsClose =@IsClose");
                return TempSql.ToString();
            }
        }
        #endregion
        #region****DbParameter����****
        public DbParameter[] SqlParams
        {
            get
            {
                List<DbParameter> listParams = new List<DbParameter>();
                if (strConfigID != "-1") listParams.Add(Config.Conn().CreateDbParameter("@ConfigID", strConfigID));
                if (strTypeID != "-1") listParams.Add(Config.Conn().CreateDbParameter("@TypeID", strTypeID));
                if (strSiteName != "") listParams.Add(Config.Conn().CreateDbParameter("@SiteName", "%" + strSiteName + "%"));
                if (strIsClose != "-1") listParams.Add(Config.Conn().CreateDbParameter("@IsClose", strIsClose));
                return listParams.ToArray();
            }
        }
        #endregion
        #region ****Url����****
        public string UrlPara
        {
            get
            {
                StringBuilder TempUrl = new StringBuilder("");
                TempUrl.Append("drpConfigID=" + Server.UrlEncode(strConfigID) + "&");
                TempUrl.Append("drpTypeID=" + Server.UrlEncode(strTypeID) + "&");
                TempUrl.Append("txtSiteName=" + Server.UrlEncode(strSiteName) + "&");
                TempUrl.Append("radIsClose=" + Server.UrlEncode(strIsClose) + "&");
                return TempUrl.ToString();
            }
        }
        #endregion
        //ҳ���ʼ��
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            if (!Page.IsPostBack)
            {
                //վ���б�
                GetData.GetConfigList(drpConfigID, "LanguageVer", "ConfigID");
                this.drpConfigID.Items.Insert(0, new ListItem("��ѡ��", "-1"));

                if (LinkID == "0")
                {
                    GetData.LimitChkMsg("LinkAdd");
                    lblTitle.Text = "���";
                    string strListID = Factory.Link().GetListID();
                    txtListID.Text = strListID;
                    hidlistID.Value = strListID;
                }
                else
                {
                    GetData.LimitChkMsg("LinkEdit");
                    lblTitle.Text = "�޸�";
                    ShowInfo();
                }
                RequiredFieldValidator4.Enabled = false;
            }
        }
        //��������
        protected void btnSave_Click(object sender, EventArgs e)
        {
            LinkModel linkModel = new LinkModel();
            string strOldListID = hidlistID.Value;
            linkModel.ConfigID = drpConfigID.SelectedValue;
            linkModel.TypeID = radTypeID.SelectedValue;
            linkModel.SiteName = txtSiteName.Text.Trim();
            linkModel.SiteUrl = txtSiteUrl.Text.Trim();
            linkModel.LogoUrl = txtLogoUrl.Text.Trim();
            linkModel.ListID = txtListID.Text.Trim();
            linkModel.AdminID = Session["AdminID"].ToString();
            linkModel.AddTime = DateTime.Now.ToString();
            linkModel.IsClose = radIsClose.SelectedValue;
            if (LinkID == "0")
            {
                Factory.Link().OrderInfo(linkModel.ListID, strOldListID);
                Factory.Link().InsertInfo(linkModel);
                Factory.AdminLog().InsertLog("�������Ϊ\"" + linkModel.SiteName + "\"���������ӡ�", Session["AdminID"].ToString());
                Config.MsgGotoUrl("��ӳɹ���", "Link.aspx");
            }
            else
            {
                LinkModel linkModel_2 = new LinkModel();
                linkModel_2 = Factory.Link().GetInfo(LinkID);
                if (linkModel_2 != null)
                {
                    if (GetData.CheckAdminID(linkModel_2.AdminID, "LinkAll"))//��鴴����
                    {
                        Factory.Link().OrderInfo(linkModel.ListID, strOldListID);
                        Factory.Link().UpdateInfo(linkModel, LinkID);
                        Factory.AdminLog().InsertLog("�޸ı��Ϊ" + LinkID + "���������ӡ�", Session["AdminID"].ToString());
                        Config.MsgGotoUrl("�޸ĳɹ���", "Link.aspx?" + UrlOrderPara + UrlPara + "page=" + page);
                    }
                }
            }
        }
        //��ʾ����
        protected void ShowInfo()
        {
            LinkModel linkModel = new LinkModel();
            linkModel = Factory.Link().GetInfo(LinkID);
            if (linkModel != null)
            {
                if (GetData.CheckAdminID(linkModel.AdminID, "LinkAll"))//��鴴����
                {
                    ClassModel claModel = Factory.Class().GetInfo(linkModel.ConfigID);
                    if (claModel != null)
                    {
                        Config.setDefaultSelected(drpConfigID, claModel.ConfigID);
                        Config.setDefaultSelected(radTypeID, linkModel.TypeID);
                        txtSiteName.Text = linkModel.SiteName;
                        txtSiteUrl.Text = linkModel.SiteUrl;
                        txtLogoUrl.Text = linkModel.LogoUrl;
                        txtListID.Text = linkModel.ListID;
                        hidlistID.Value = linkModel.ListID;
                        //txtAdminID.Text = linkModel.AdminID;
                        //txtAddTime.Text = linkModel.AddTime;
                        radIsClose.ClearSelection();
                        Config.setDefaultSelected(radIsClose, linkModel.IsClose);
                    }
                    else
                    {
                        Config.ShowEnd("��û�в鿴����Ϣ��Ȩ�ޣ�");
                    }
                }
                else
                {
                    Config.ShowEnd("��û�в鿴����Ϣ��Ȩ�ޣ�");
                }
            }
        }

        //����ѡ��
        protected void radTypeID_SelectedIndexChanged(object sender, EventArgs e)
        {
            RequiredFieldValidator4.Enabled = true;
        }

    }
}
