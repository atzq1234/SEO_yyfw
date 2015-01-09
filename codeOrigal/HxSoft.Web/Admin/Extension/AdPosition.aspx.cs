using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using HxSoft.ClassFactory;
using HxSoft.Common;
using HxSoft.Model;

namespace HxSoft.Web.Admin.Extension
{
    public partial class AdPosition : System.Web.UI.Page
    {
        /// <summary>
        /// ������:��С��
        /// ����:2010-12-6
        /// </summary>
        //����ȫ�ֱ���
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
            GetData.LimitChkMsg("AdPosition");
            lbtnAdd.Visible = GetData.LimitChk("AdPositionAdd");
            lbtnEdit.Visible = GetData.LimitChk("AdPositionEdit");
            lbtnDel.Visible = GetData.LimitChk("AdPositionDel");
            lbtnOpen.Visible = GetData.LimitChk("AdPositionOpen");
            lbtnClose.Visible = GetData.LimitChk("AdPositionClose");
            if (!Page.IsPostBack)
            {
                Repeater_Bind(repList);
            }
        }
        //������
        protected void Repeater_Bind(Repeater rep)
        {
            string sql = "select * from t_AdPosition where 1=1 " + SqlQuery + SqlOrder;
            pager.InnerHtml = Factory.Acc().DataPageBind(sql, SqlParams, Config.DataBindObjTypeCollection.Repeater.ToString(), rep, 10, page, "?" + UrlOrderPara + UrlPara).ToString();
        }
        //�޸�
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string strAdPositionID = Config.Request(Request.Form["AdPositionID"], "0");
            if (strAdPositionID != "0")
            {
                Response.Redirect("AdPosition_Add.aspx?AdPositionID=" + strAdPositionID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }
        //ɾ��
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string strAdPositionID = Config.Request(Request.Form["AdPositionID"], "0");
            if (strAdPositionID != "0")
            {
                string[] arrAdPositionID = strAdPositionID.Split(new char[] { ',' });
                StringBuilder strTempAdPositionID = new StringBuilder();
                AdPositionModel adPosModel = new AdPositionModel();
                int n = 0;
                for (int i = 0; i < arrAdPositionID.Length; i++)
                {
                    adPosModel = Factory.AdPosition().GetInfo(arrAdPositionID[i]);
                    if (adPosModel != null)
                    {
                        if (GetData.CheckAdminID(adPosModel.AdminID, "AdPositionAll"))//��鴴����
                        {
                            Factory.AdPosition().DeleteInfo(arrAdPositionID[i]);
                            strTempAdPositionID.Append(arrAdPositionID[i]);
                            if (i + 1 < arrAdPositionID.Length) strTempAdPositionID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("ɾ�����Ϊ" + strTempAdPositionID.ToString() + "�Ĺ��λ!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("���Ϊ" + strTempAdPositionID.ToString() + "���λɾ���ɹ�!", "AdPosition.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
                }
                else
                {
                    Config.MsgGoBack("����ʧ��!");
                }
            }
        }


        //��������
        protected void btnOpen_Click(object sender, EventArgs e)
        {
            string strAdPositionID = Config.Request(Request.Form["AdPositionID"], "0");
            if (strAdPositionID != "0")
            {
                string[] arrAdPositionID = strAdPositionID.Split(new char[] { ',' });
                StringBuilder strTempAdPositionID = new StringBuilder();
                AdPositionModel adPosModel = new AdPositionModel();
                int n = 0;
                for (int i = 0; i < arrAdPositionID.Length; i++)
                {
                    adPosModel = Factory.AdPosition().GetInfo(arrAdPositionID[i]);
                    if (adPosModel != null)
                    {
                        if (GetData.CheckAdminID(adPosModel.AdminID, "AdPositionAll"))//��鴴����
                        {
                            Factory.AdPosition().UpdateCloseStatus(arrAdPositionID[i], "0");
                            strTempAdPositionID.Append(arrAdPositionID[i]);
                            if (i + 1 < arrAdPositionID.Length) strTempAdPositionID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("���ű��Ϊ" + strTempAdPositionID.ToString() + "�Ĺ��λ!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("���Ϊ" + strTempAdPositionID.ToString() + "���λ���ųɹ�!", "AdPosition.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
                }
                else
                {
                    Config.MsgGoBack("����ʧ��!");
                }
            }
        }

        //�����ر�
        protected void btnClose_Click(object sender, EventArgs e)
        {
            string strAdPositionID = Config.Request(Request.Form["AdPositionID"], "0");
            if (strAdPositionID != "0")
            {
                string[] arrAdPositionID = strAdPositionID.Split(new char[] { ',' });
                StringBuilder strTempAdPositionID = new StringBuilder();
                AdPositionModel adPosModel = new AdPositionModel();
                int n = 0;
                for (int i = 0; i < arrAdPositionID.Length; i++)
                {
                    adPosModel = Factory.AdPosition().GetInfo(arrAdPositionID[i]);
                    if (adPosModel != null)
                    {
                        if (GetData.CheckAdminID(adPosModel.AdminID, "AdPositionAll"))//��鴴����
                        {
                            Factory.AdPosition().UpdateCloseStatus(arrAdPositionID[i], "1");
                            strTempAdPositionID.Append(arrAdPositionID[i]);
                            if (i + 1 < arrAdPositionID.Length) strTempAdPositionID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("�رձ��Ϊ" + strTempAdPositionID.ToString() + "�Ĺ��λ!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("���Ϊ" + strTempAdPositionID.ToString() + "���λ�رճɹ�!", "AdPosition.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
                }
                else
                {
                    Config.MsgGoBack("����ʧ��!");
                }
            }
        }

        //��ѯ
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Repeater_Bind(repList);
        }
    }
}
