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
    public partial class Ad : System.Web.UI.Page
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
            GetData.LimitChkMsg("Ad");
            lbtnAdd.Visible = GetData.LimitChk("AdAdd");
            lbtnEdit.Visible = GetData.LimitChk("AdEdit");
            lbtnDel.Visible = GetData.LimitChk("AdDel");
            lbtnOpen.Visible = GetData.LimitChk("AdOpen");
            lbtnClose.Visible = GetData.LimitChk("AdClose");
            if (!Page.IsPostBack)
            {
                //���λ
                Factory.Acc().DataBind("select * from t_AdPosition order by ListID asc", null,Config.DataBindObjTypeCollection.DropDownList.ToString(), drpAdPositionID, "AdPositionName", "AdPositionID");
                drpAdPositionID.Items.Insert(0,new ListItem("����","-1"));

                Repeater_Bind(repList);
            }
        }
        //������
        protected void Repeater_Bind(Repeater rep)
        {
            string sql_f_1 = "(select TypeID from t_AdPosition as b where b.AdPositionID=a.AdPositionID ) as AdPositionTypeID";
            string sql = "select *," + sql_f_1 + " from t_Ad as a where 1=1 " + SqlQuery + SqlOrder;
            pager.InnerHtml = Factory.Acc().DataPageBind(sql, SqlParams, Config.DataBindObjTypeCollection.Repeater.ToString(), rep, 10, page, "?" + UrlOrderPara + UrlPara).ToString();
        }
        //�޸�
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string strAdID = Config.Request(Request.Form["AdID"], "0");
            if (strAdID != "0")
            {
                Response.Redirect("Ad_Add.aspx?AdID=" + strAdID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }
        //ɾ��
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string strAdID = Config.Request(Request.Form["AdID"], "0");
            if (strAdID != "0")
            {
                string[] arrAdID = strAdID.Split(new char[] { ',' });
                StringBuilder strTempAdID = new StringBuilder();
                AdModel adModel = new AdModel();
                int n = 0;
                for (int i = 0; i < arrAdID.Length; i++)
                {
                    adModel = Factory.Ad().GetInfo(arrAdID[i]);
                    if (adModel != null)
                    {
                        if (GetData.CheckAdminID(adModel.AdminID, "AdAll"))//��鴴����
                        {
                            Factory.Ad().DeleteInfo(arrAdID[i]);
                            strTempAdID.Append(arrAdID[i]);
                            if (i + 1 < arrAdID.Length) strTempAdID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("ɾ�����Ϊ" + strTempAdID.ToString() + "�Ĺ��!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("���Ϊ" + strTempAdID.ToString() + "���ɾ���ɹ�!", "Ad.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strAdID = Config.Request(Request.Form["AdID"], "0");
            if (strAdID != "0")
            {
                string[] arrAdID = strAdID.Split(new char[] { ',' });
                StringBuilder strTempAdID = new StringBuilder();
                AdModel adModel = new AdModel();
                int n = 0;
                for (int i = 0; i < arrAdID.Length; i++)
                {
                    adModel = Factory.Ad().GetInfo(arrAdID[i]);
                    if (adModel != null)
                    {
                        if (GetData.CheckAdminID(adModel.AdminID, "AdAll"))//��鴴����
                        {
                            Factory.Ad().UpdateCloseStatus(arrAdID[i], "0");
                            strTempAdID.Append(arrAdID[i]);
                            if (i + 1 < arrAdID.Length) strTempAdID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("���ű��Ϊ" + strTempAdID.ToString() + "�Ĺ��!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("���Ϊ" + strTempAdID.ToString() + "��濪�ųɹ�!", "Ad.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strAdID = Config.Request(Request.Form["AdID"], "0");
            if (strAdID != "0")
            {
                string[] arrAdID = strAdID.Split(new char[] { ',' });
                StringBuilder strTempAdID = new StringBuilder();
                AdModel adModel = new AdModel();
                int n = 0;
                for (int i = 0; i < arrAdID.Length; i++)
                {
                    adModel = Factory.Ad().GetInfo(arrAdID[i]);
                    if (adModel != null)
                    {
                        if (GetData.CheckAdminID(adModel.AdminID, "AdAll"))//��鴴����
                        {
                            Factory.Ad().UpdateCloseStatus(arrAdID[i], "1");
                            strTempAdID.Append(arrAdID[i]);
                            if (i + 1 < arrAdID.Length) strTempAdID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("�رձ��Ϊ" + strTempAdID.ToString() + "�Ĺ��!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("���Ϊ" + strTempAdID.ToString() + "���رճɹ�!", "Ad.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
