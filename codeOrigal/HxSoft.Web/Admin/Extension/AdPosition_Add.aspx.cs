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
    public partial class AdPosition_Add : System.Web.UI.Page
    {
        /// <summary>
        /// ������:��С��
        /// ����:2010-12-6
        /// </summary>
        //����ȫ�ֱ���
        public string AdPositionID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["AdPositionID"], 0).ToString();
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
        public string strAdPositionName
        {
            get
            {
                return Config.Request(Request["txtAdPositionName"], "");
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
        #region ****��ѯ���****
        public string SqlQuery
        {
            get
            {
                StringBuilder TempSql = new StringBuilder("");
                if (strAdPositionName != "") TempSql.Append(" and AdPositionName like @AdPositionName");
                if (strTypeID != "-1") TempSql.Append(" and TypeID = @TypeID");
                if (strIsClose != "-1") TempSql.Append(" and IsClose = @IsClose");
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
                if (strAdPositionName != "") listParams.Add(Config.Conn().CreateDbParameter("@AdPositionName", "%" + strAdPositionName + "%"));
                if (strTypeID != "-1") listParams.Add(Config.Conn().CreateDbParameter("@TypeID", strTypeID));
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
                TempUrl.Append("txtAdPositionName=" + Server.UrlEncode(strAdPositionName) + "&");
                TempUrl.Append("drpTypeID=" + Server.UrlEncode(strTypeID) + "&");
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
                if (AdPositionID == "0")
                {
                    GetData.LimitChkMsg("AdPositionAdd");
                    lblTitle.Text = "���";
                    string strListID = Factory.AdPosition().GetListID();
                    txtListID.Text = strListID;
                    hidlistID.Value = strListID;
                }
                else
                {
                    GetData.LimitChkMsg("AdPositionEdit");
                    lblTitle.Text = "�޸�";
                    ShowInfo();
                }
            }
        }
        //��������
        protected void btnSave_Click(object sender, EventArgs e)
        {
            AdPositionModel adPosModel = new AdPositionModel();
            string strOldListID = hidlistID.Value;
            adPosModel.AdPositionName = txtAdPositionName.Text.Trim();
            adPosModel.AdPositionIntro = Config.HTMLCls(txtAdPositionIntro.Text.Trim());
            adPosModel.TypeID = drpTypeID.SelectedValue;
            adPosModel.Width = txtWidth.Text.Trim();
            adPosModel.Height = txtHeight.Text.Trim();
            adPosModel.Price = txtAdPrice.Text.Trim();
            adPosModel.ListID = txtListID.Text.Trim();
            adPosModel.AdminID = Session["AdminID"].ToString();
            adPosModel.AddTime = DateTime.Now.ToString();
            adPosModel.IsClose = radIsClose.SelectedValue;
            if (AdPositionID == "0")
            {
                Factory.AdPosition().OrderInfo(adPosModel.ListID, strOldListID);
                Factory.AdPosition().InsertInfo(adPosModel);
                Factory.AdminLog().InsertLog("�������Ϊ\"" + adPosModel.AdPositionName + "\"�Ĺ��λ��", Session["AdminID"].ToString());
                Config.MsgGotoUrl("��ӳɹ���", "AdPosition.aspx");
            }
            else
            {
                AdPositionModel adPosModel_2 = new AdPositionModel();
                adPosModel_2 = Factory.AdPosition().GetInfo(AdPositionID);
                if (adPosModel_2 != null)
                {
                    if (GetData.CheckAdminID(adPosModel_2.AdminID, "AdPositionAll"))//��鴴����
                    {
                        Factory.AdPosition().OrderInfo(adPosModel.ListID, strOldListID);
                        Factory.AdPosition().UpdateInfo(adPosModel, AdPositionID);
                        Factory.AdminLog().InsertLog("�޸ı��Ϊ" + AdPositionID + "�Ĺ��λ��", Session["AdminID"].ToString());
                        Config.MsgGotoUrl("�޸ĳɹ���", "AdPosition.aspx?" + UrlOrderPara + UrlPara + "page=" + page);
                    }
                }
            }
        }
        //��ʾ����
        protected void ShowInfo()
        {
            AdPositionModel adPosModel = new AdPositionModel();
            adPosModel = Factory.AdPosition().GetInfo(AdPositionID);
            if (adPosModel != null)
            {
                if (GetData.CheckAdminID(adPosModel.AdminID, "AdPositionAll"))//��鴴����
                {
                    txtAdPositionName.Text = adPosModel.AdPositionName;
                    txtAdPositionIntro.Text = Config.HTMLToTextarea(adPosModel.AdPositionIntro);
                    Config.setDefaultSelected(drpTypeID, adPosModel.TypeID);
                    txtWidth.Text = adPosModel.Width;
                    txtHeight.Text = adPosModel.Height;
                    txtAdPrice.Text = adPosModel.Price;
                    txtListID.Text = adPosModel.ListID;
                    hidlistID.Value = adPosModel.ListID;
                    //txtAdminID.Text = adPosModel.AdminID;
                    //txtAddTime.Text = adPosModel.AddTime;

                    radIsClose.ClearSelection();
                    Config.setDefaultSelected(radIsClose, adPosModel.IsClose);
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
