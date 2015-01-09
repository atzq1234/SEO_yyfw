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
    public partial class Ad_Add : System.Web.UI.Page
    {
        /// <summary>
        /// ������:��С��
        /// ����:2010-12-6
        /// </summary>
        //����ȫ�ֱ���
        public string AdID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["AdID"], 0).ToString();
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
        public string strAdName
        {
            get
            {
                return Config.Request(Request["txtAdName"], "");
            }
        }
        public string strAdPositionID
        {
            get
            {
                return Config.Request(Request["drpAdPositionID"], "-1");
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
                if (strAdName != "") TempSql.Append(" and AdName like @AdName");
                if (strAdPositionID != "-1") TempSql.Append(" and AdPositionID = @AdPositionID");
                if (strIsClose != "-1") TempSql.Append(" and IsClose =" + strIsClose);
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
                if (strAdName != "") listParams.Add(Config.Conn().CreateDbParameter("@AdName", "%" + strAdName + "%"));
                if (strAdPositionID != "-1") listParams.Add(Config.Conn().CreateDbParameter("@AdPositionID", strAdPositionID));
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
                TempUrl.Append("txtAdName=" + Server.UrlEncode(strAdName) + "&");
                TempUrl.Append("drpAdPositionID=" + Server.UrlEncode(strAdPositionID) + "&");
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
                //���λ
                Factory.Acc().DataBind("select * from t_AdPosition order by ListID asc",  null,Config.DataBindObjTypeCollection.DropDownList.ToString(), drpAdPositionID, "AdPositionName", "AdPositionID");
                drpAdPositionID.Items.Insert(0, new ListItem("��ѡ��", "-1"));
                
                if (AdID == "0")
                {
                    GetData.LimitChkMsg("AdAdd");
                    lblTitle.Text = "���";
                    string strListID = Factory.Ad().GetListID();
                    txtListID.Text = strListID;
                    hidlistID.Value = strListID;
                }
                else
                {
                    GetData.LimitChkMsg("AdEdit");
                    lblTitle.Text = "�޸�";
                    ShowInfo();
                }
            }
        }
        //��������
        protected void btnSave_Click(object sender, EventArgs e)
        {
            AdModel adModel = new AdModel();
            string strOldListID = hidlistID.Value;
            adModel.AdName = txtAdName.Text.Trim();
            adModel.AdIntro = Config.HTMLCls(txtAdIntro.Text.Trim());
            adModel.AdPositionID = drpAdPositionID.SelectedValue;
            adModel.AdSmallPic = txtAdSmallPic.Text.Trim();
            adModel.AdPath = txtAdPath.Text.Trim();
            adModel.AdLink = txtAdLink.Text.Trim();
            adModel.ClickNum = txtClickNum.Text.Trim();
            adModel.ListID = txtListID.Text.Trim();
            adModel.AdminID = Session["AdminID"].ToString();
            adModel.AddTime = DateTime.Now.ToString();
            adModel.IsClose = radIsClose.SelectedValue;
            if (AdID == "0")
            {
                Factory.Ad().OrderInfo(adModel.ListID, strOldListID);
                Factory.Ad().InsertInfo(adModel);
                Factory.AdminLog().InsertLog("�������Ϊ\"" + adModel.AdName + "\"�Ĺ�档", Session["AdminID"].ToString());
                Config.MsgGotoUrl("��ӳɹ���", "Ad.aspx");
            }
            else
            {
                AdModel adModel_2 = new AdModel();
                adModel_2 = Factory.Ad().GetInfo(AdID);
                if (adModel_2 != null)
                {
                    if (GetData.CheckAdminID(adModel_2.AdminID, "AdAll"))//��鴴����
                    {
                        Factory.Ad().OrderInfo(adModel.ListID, strOldListID);
                        Factory.Ad().UpdateInfo(adModel, AdID);
                        Factory.AdminLog().InsertLog("�޸ı��Ϊ" + AdID + "�Ĺ�档", Session["AdminID"].ToString());
                        Config.MsgGotoUrl("�޸ĳɹ���", "Ad.aspx?" + UrlOrderPara + UrlPara + "page=" + page);
                    }
                }
            }
        }
        //��ʾ����
        protected void ShowInfo()
        {
            AdModel adModel = new AdModel();
            adModel = Factory.Ad().GetInfo(AdID);
            if (adModel != null)
            {
                if (GetData.CheckAdminID(adModel.AdminID, "AdAll"))//��鴴����
                {
                    txtAdName.Text = adModel.AdName;
                    txtAdIntro.Text = Config.HTMLToTextarea(adModel.AdIntro);
                    Config.setDefaultSelected(drpAdPositionID, adModel.AdPositionID);
                    txtAdSmallPic.Text = adModel.AdSmallPic;
                    txtAdPath.Text = adModel.AdPath;
                    txtAdLink.Text = adModel.AdLink;
                    txtClickNum.Text = adModel.ClickNum;
                    txtListID.Text = adModel.ListID;
                    hidlistID.Value = adModel.ListID;
                    //txtAdminID.Text = adModel.AdminID;
                    //txtAddTime.Text = adModel.AddTime;

                    radIsClose.ClearSelection();
                    Config.setDefaultSelected(radIsClose, adModel.IsClose);
                }
                else
                {
                    Config.ShowEnd("��û�в鿴����Ϣ��Ȩ��"); 
                }
            }
            else
            {
                Config.ShowEnd("��û�в鿴����Ϣ��Ȩ�ޣ�");
            }
        }
    }
}
