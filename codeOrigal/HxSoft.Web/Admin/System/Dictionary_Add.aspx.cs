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
    public partial class Dictionary_Add : System.Web.UI.Page
    {
        /// <summary>
        /// ������:��С��
        /// ����:2010-12-6
        /// </summary>
        //����ȫ�ֱ���
        public string DictionaryID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["DictionaryID"], 0).ToString();
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
        public string strDictionaryName
        {
            get
            {
                return Config.Request(Request["txtDictionaryName"], "");
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
                if (strDictionaryName != "") TempSql.Append(" and DictionaryName like @DictionaryName");
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
                if (strDictionaryName != "") listParams.Add(Config.Conn().CreateDbParameter("@DictionaryName", "%" + strDictionaryName + "%"));
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
                TempUrl.Append("txtDictionaryName=" + Server.UrlEncode(strDictionaryName) + "&");
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
                if (DictionaryID == "0")
                {
                    GetData.LimitChkMsg("DictionaryAdd");
                    lblTitle.Text = "���";
                    lblParent.Text = Factory.Dictionary().ShowPath(ParentID).ToString();
                    string strListID = Factory.Dictionary().GetListID(ParentID);
                    txtListID.Text = strListID;
                    hidlistID.Value = strListID;
                }
                else
                {
                    GetData.LimitChkMsg("DictionaryEdit");
                    lblTitle.Text = "�޸�";
                    ShowInfo();
                }
            }
        }
        //��������
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DictionaryModel dictModel = new DictionaryModel();
            string strOldListID = hidlistID.Value;
            dictModel.DictionaryName = txtDictionaryName.Text.Trim();
            dictModel.DictionaryVal = txtDictionaryVal.Text.Trim();
            dictModel.ParentID = ParentID;
            dictModel.ChildNum = "0";
            dictModel.ListID = txtListID.Text.Trim();
            dictModel.AdminID = Session["AdminID"].ToString();
            dictModel.AddTime = DateTime.Now.ToString();
            dictModel.IsClose = radIsClose.SelectedValue;
            if (DictionaryID == "0")
            {
                if (!Factory.Dictionary().CheckInfo("DictionaryName", dictModel.DictionaryName, dictModel.ParentID))
                {
                    Factory.Dictionary().OrderInfo(dictModel.ParentID, dictModel.ListID, strOldListID);
                    Factory.Dictionary().InsertInfo(dictModel);
                    Factory.AdminLog().InsertLog("�������Ϊ\"" + dictModel.DictionaryName + "\"���ֵ䡣", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("��ӳɹ���", "Dictionary.aspx?ParentID=" + dictModel.ParentID);
                }
                else
                {
                    errMsg.Text = "�Ѵ�����ͬ�ֵ�!";
                }
            }
            else
            {
                DictionaryModel dictModel_2 = new DictionaryModel();
                dictModel_2 = Factory.Dictionary().GetInfo(DictionaryID);
                if (dictModel_2 != null)
                {
                    if (GetData.CheckAdminID(dictModel_2.AdminID, "DictionaryAll"))//��鴴����
                    {
                        if (!Factory.Dictionary().CheckInfo("DictionaryName", dictModel.DictionaryName, dictModel.ParentID, DictionaryID))
                        {
                            Factory.Dictionary().OrderInfo(dictModel.ParentID, dictModel.ListID, strOldListID);
                            Factory.Dictionary().UpdateInfo(dictModel, DictionaryID);
                            Factory.AdminLog().InsertLog("�޸ı��Ϊ" + DictionaryID + "���ֵ䡣", Session["AdminID"].ToString());
                            Config.MsgGotoUrl("�޸ĳɹ���", "Dictionary.aspx?ParentID=" + dictModel.ParentID + "&" + UrlOrderPara + UrlPara + "page=" + page);
                        }
                        else
                        {
                            errMsg.Text = "�Ѵ�����ͬ�ֵ�!";
                        }
                    }
                }
            }
        }
        //��ʾ����
        protected void ShowInfo()
        {
            DictionaryModel dictModel = new DictionaryModel();
            dictModel = Factory.Dictionary().GetInfo(DictionaryID);
            if (dictModel != null)
            {
                if (GetData.CheckAdminID(dictModel.AdminID, "DictionaryAll"))//��鴴����
                {
                    txtDictionaryName.Text = dictModel.DictionaryName;
                    txtDictionaryVal.Text = dictModel.DictionaryVal;
                    lblParent.Text = Factory.Dictionary().ShowPath(dictModel.ParentID).ToString();
                    //txtChildNum.Text = dictModel.ChildNum;
                    txtListID.Text = dictModel.ListID;
                    hidlistID.Value = dictModel.ListID;
                    //txtAddAdminID.Text = dictModel.AddAdminID;
                    //txtAddTime.Text = dictModel.AddTime;
                    radIsClose.ClearSelection();
                    Config.setDefaultSelected(radIsClose, dictModel.IsClose);
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
