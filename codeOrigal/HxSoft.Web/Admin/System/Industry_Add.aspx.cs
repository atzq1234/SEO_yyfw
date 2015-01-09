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
    public partial class Industry_Add : System.Web.UI.Page
    {
        /// <summary>
        /// ������:��С��
        /// ����:2010-12-6
        /// </summary>
        //����ȫ�ֱ���
        public string IndustryID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["IndustryID"], 0).ToString();
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
        public string strIndustryName
        {
            get
            {
                return Config.Request(Request["txtIndustryName"], "");
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
                if (strIndustryName != "") TempSql.Append(" and IndustryName like @IndustryName");
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
                if (strIndustryName != "") listParams.Add(Config.Conn().CreateDbParameter("@IndustryName", "%" + strIndustryName + "%"));
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
                TempUrl.Append("txtIndustryName=" + Server.UrlEncode(strIndustryName) + "&");
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
                if (IndustryID == "0")
                {
                    GetData.LimitChkMsg("IndustryAdd");
                    lblTitle.Text = "���";
                    lblParent.Text = Factory.Industry().ShowPath(ParentID).ToString();
                    string strListID = Factory.Industry().GetListID(ParentID);
                    txtListID.Text = strListID;
                    hidlistID.Value = strListID;
                }
                else
                {
                    GetData.LimitChkMsg("IndustryEdit");
                    lblTitle.Text = "�޸�";
                    ShowInfo();
                }
            }
        }
        //��������
        protected void btnSave_Click(object sender, EventArgs e)
        {
            IndustryModel indModel = new IndustryModel();
            string strOldListID = hidlistID.Value;
            indModel.IndustryName = txtIndustryName.Text.Trim();
            indModel.ParentID = ParentID;
            indModel.ChildNum = "0";
            indModel.ListID = txtListID.Text.Trim();
            indModel.AdminID = Session["AdminID"].ToString();
            indModel.AddTime = DateTime.Now.ToString();
            indModel.IsClose = radIsClose.SelectedValue;
            if (IndustryID == "0")
            {
                if (!Factory.Industry().CheckInfo("IndustryName", indModel.IndustryName, indModel.ParentID))
                {
                    Factory.Industry().OrderInfo(indModel.ParentID, indModel.ListID, strOldListID);
                    Factory.Industry().InsertInfo(indModel);
                    Factory.AdminLog().InsertLog("�������Ϊ\"" + indModel.IndustryName + "\"����ҵ��", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("��ӳɹ���", "Industry.aspx?ParentID=" + indModel.ParentID);
                }
                else
                {
                    errMsg.Text = "�Ѵ�����ͬ��ҵ!";
                }
            }
            else
            {
                IndustryModel indModel_2 = new IndustryModel();
                indModel_2 = Factory.Industry().GetInfo(IndustryID);
                if (indModel_2 != null)
                {
                    if (GetData.CheckAdminID(indModel_2.AdminID, "IndustryAll"))//��鴴����
                    {
                        if (!Factory.Industry().CheckInfo("IndustryName", indModel.IndustryName, indModel.ParentID, IndustryID))
                        {
                            Factory.Industry().OrderInfo(indModel.ParentID, indModel.ListID, strOldListID);
                            Factory.Industry().UpdateInfo(indModel, IndustryID);
                            Factory.AdminLog().InsertLog("�޸ı��Ϊ" + IndustryID + "����ҵ��", Session["AdminID"].ToString());
                            Config.MsgGotoUrl("�޸ĳɹ���", "Industry.aspx?ParentID=" + indModel.ParentID + "&" + UrlOrderPara + UrlPara + "page=" + page);
                        }
                        else
                        {
                            errMsg.Text = "�Ѵ�����ͬ��ҵ!";
                        }
                    }
                }
            }
        }
        //��ʾ����
        protected void ShowInfo()
        {
            IndustryModel indModel = new IndustryModel();
            indModel = Factory.Industry().GetInfo(IndustryID);
            if (indModel != null)
            {
                if (GetData.CheckAdminID(indModel.AdminID, "IndustryAll"))//��鴴����
                {
                    txtIndustryName.Text = indModel.IndustryName;
                    lblParent.Text = Factory.Industry().ShowPath(indModel.ParentID).ToString();
                    //txtChildNum.Text = indModel.ChildNum;
                    txtListID.Text = indModel.ListID;
                    hidlistID.Value = indModel.ListID;
                    //txtAddAdminID.Text = indModel.AddAdminID;
                    //txtAddTime.Text = indModel.AddTime;
                    radIsClose.ClearSelection();
                    Config.setDefaultSelected(radIsClose, indModel.IsClose);
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