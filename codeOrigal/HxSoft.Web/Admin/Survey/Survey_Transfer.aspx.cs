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
using System.Data.Common;
using System.Collections.Generic;

namespace HxSoft.Web.Admin.Survey
{
    public partial class Survey_Transfer : System.Web.UI.Page
    {
        /// <summary>
        /// ������:��С��
        /// ����:2010-12-6
        /// </summary>
        //����ȫ�ֱ���
        public string SurveyID
        {
            get
            {
                return Config.Request(Request.QueryString["SurveyID"], "0");
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
                return Config.Request(Request["OrderKey"], "SurveyID");
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
        public string strClassID
        {
            get
            {
                return Config.Request(Request["drpClassID"], "-1");
            }
        }
        public string strSubject
        {
            get
            {
                return Config.Request(Request["txtSubject"], "");
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
        #region ****��ѯ���****
        public string SqlQuery
        {
            get
            {
                StringBuilder TempSql = new StringBuilder("");
                if (strClassID != "-1") TempSql.Append(" and (ClassID=@ClassID or ClassID in (" + Factory.Class().GetSubClassSql(strClassID) + "))");
                if (strSubject != "") TempSql.Append(" and Subject like @Subject");
                if (strIsRecommend != "-1") TempSql.Append(" and IsRecommend =@IsRecommend");
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
                if (strClassID != "-1") listParams.Add(Config.Conn().CreateDbParameter("@ClassID", strClassID));
                if (strSubject != "") listParams.Add(Config.Conn().CreateDbParameter("@Subject", "%" + strSubject + "%"));
                if (strIsRecommend != "-1") listParams.Add(Config.Conn().CreateDbParameter("@IsRecommend", strIsRecommend));
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
                TempUrl.Append("drpClassID=" + Server.UrlEncode(strClassID) + "&");
                TempUrl.Append("txtSubject=" + Server.UrlEncode(strSubject) + "&");
                TempUrl.Append("radIsRecommend=" + Server.UrlEncode(strIsRecommend) + "&");
                TempUrl.Append("radIsClose=" + Server.UrlEncode(strIsClose) + "&");
                return TempUrl.ToString();
            }
        }
        #endregion
        //ҳ���ʼ��
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            GetData.LimitChkMsg("SurveyTransfer");
            if (!Page.IsPostBack)
            {
                //�������
                Factory.Class().ShowSelectTree("0", drpClassID, "", Config.SysSurveyMouldID);
                drpClassID.Items.Insert(0, new ListItem("��ѡ��", "0"));
            }
        }
        //��������
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strClassID = drpClassID.SelectedValue;
            string[] arrSurveyID = SurveyID.Split(new char[] { ',' });
            StringBuilder strTempSurveyID = new StringBuilder();
            SurveyModel surModel = new SurveyModel();
            int n = 0;
            for (int i = 0; i < arrSurveyID.Length; i++)
            {
                surModel = Factory.Survey().GetInfo(arrSurveyID[i]);
                if (surModel != null)
                {
                    if (GetData.CheckAdminID(surModel.AdminID, "SurveyAll"))//��鴴����
                    {
                        Factory.Survey().TransferInfo(arrSurveyID[i],strClassID);
                        strTempSurveyID.Append(arrSurveyID[i]);
                        if (i + 1 < arrSurveyID.Length) strTempSurveyID.Append(",");
                        n++;
                    }
                }
            }
            if (n > 0)
            {
                Factory.AdminLog().InsertLog("ת�Ʊ��Ϊ" + strTempSurveyID.ToString() + "�ĵ���!", Session["AdminID"].ToString());
                Config.MsgGotoUrl("���Ϊ" + strTempSurveyID.ToString() + "����ת�Ƴɹ�!", "Survey.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
            else
            {
                Config.MsgGoBack("����ʧ��!");
            }
        }
    }
}
