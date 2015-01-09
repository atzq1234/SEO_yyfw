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
    public partial class Limit_Move : System.Web.UI.Page
    {
        /// <summary>
        /// ������:��С��
        /// ����:2010-12-6
        /// </summary>
        //����ȫ�ֱ���
        public string LimitID
        {
            get
            {
                return Config.Request(Request.QueryString["LimitID"], "0");
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
        public string strLimitField
        {
            get
            {
                return Config.Request(Request["txtLimitField"], "");
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
                if (strLimitField != "") TempSql.Append(" and LimitField like @LimitField");
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
                if (strLimitField != "") listParams.Add(Config.Conn().CreateDbParameter("@LimitField", "%" + strLimitField + "%"));
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
                TempUrl.Append("txtLimitField=" + Server.UrlEncode(strLimitField) + "&");
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
                GetData.LimitChkMsg("LimitMove");
                if (LimitID == "0")
                {
                    Config.ShowEnd("��ѡ��Ҫ�����ļ�¼!");
                }
                else
                {
                    ShowInfo();
                }
            }
        }
        //��������
        protected void btnSave_Click(object sender, EventArgs e)
        {
            StringBuilder strTempLimitID = new StringBuilder();
            LimitModel limModel = new LimitModel();
            limModel.ParentID = drpParentID.SelectedValue;
            string[] arrLimitID = hidLimitID.Value.Split(new char[] { ',' });
            int n = 0;
            for (int i = 0; i < arrLimitID.Length; i++)
            {
                LimitModel limModel_2 = new LimitModel();
                limModel_2 = Factory.Limit().GetInfo(arrLimitID[i]);
                if (limModel_2 != null)
                {
                    if (GetData.CheckAdminID(limModel_2.AdminID, "LimitAll"))//��鴴����
                    {
                        //������һ��,ȡ�¸�������
                        if (limModel.ParentID != limModel_2.ParentID)
                        {
                            limModel.ListID = Factory.Limit().GetListID(limModel.ParentID);
                        }
                        else
                        {
                            limModel.ListID = limModel_2.ListID;
                        }
                        Factory.Limit().MoveInfo(limModel, arrLimitID[i]);
                        Factory.Limit().UpdateChildNum(limModel.ParentID, limModel_2.ParentID);
                        strTempLimitID.Append(arrLimitID[i]);
                        if (i + 1 < arrLimitID.Length) strTempLimitID.Append(",");
                        n++;
                    }
                }
            }
            if (n > 0)
            {
                Factory.AdminLog().InsertLog("�ƶ����Ϊ" + strTempLimitID.ToString() + "��Ȩ���ֶ�!", Session["AdminID"].ToString());
                Config.MsgGotoUrl("���Ϊ" + strTempLimitID.ToString() + "Ȩ���ֶ��ƶ��ɹ�!", "Limit.aspx?ParentID=" + limModel.ParentID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
            else
            {
                Config.MsgGoBack("����ʧ��!");
            }
        }


        //��ʾ����
        protected void ShowInfo()
        {
            LimitModel limModel = new LimitModel();
            string[] arrLimitID = LimitID.Split(new char[] { ','});
            for (int i = 0; i < arrLimitID.Length; i++)
            {
                limModel = Factory.Limit().GetInfo(arrLimitID[i]);
                if (limModel != null)
                {
                    if (GetData.CheckAdminID(limModel.AdminID, "LimitAll"))//��鴴����
                    {
                        lblLimitField.Text = lblLimitField.Text + limModel.LimitField;
                        hidLimitID.Value=hidLimitID.Value+arrLimitID[i];
                        if (i + 1 < arrLimitID.Length)
                        {
                            lblLimitField.Text = lblLimitField.Text + "��";
                            hidLimitID.Value = hidLimitID.Value + ",";
                        }
                    }
                }
            }
            Factory.Limit().ShowSelectTree("0", drpParentID, " and ParentID not in(" + LimitID + ") and LimitID not in(" + LimitID + ")");
            drpParentID.Items.Insert(0, new ListItem("�����", "0"));
            drpParentID.Attributes.Add("size", "20");
            Config.setDefaultSelected(drpParentID, ParentID);
        }
    }
}
