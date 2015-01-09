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
    public partial class Link : System.Web.UI.Page
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
            GetData.LimitChkMsg("Link");
            lbtnAdd.Visible = GetData.LimitChk("LinkAdd");
            lbtnEdit.Visible = GetData.LimitChk("LinkEdit");
            lbtnDel.Visible = GetData.LimitChk("LinkDel");
            lbtnOpen.Visible = GetData.LimitChk("LinkOpen");
            lbtnClose.Visible = GetData.LimitChk("LinkClose");
            lbtnTransfer.Visible = GetData.LimitChk("LinkTransfer");
            if (!Page.IsPostBack)
            {
                //վ���б�
                GetData.GetConfigList(drpConfigID, "LanguageVer", "ConfigID");
                this.drpConfigID.Items.Insert(0, new ListItem("��ѡ��", "-1"));

                Repeater_Bind(repList);
            }
        }
        //������
        protected void Repeater_Bind(Repeater rep)
        {
            string sql = "select * from t_Link where 1=1 " + SqlQuery + SqlOrder;
            pager.InnerHtml = Factory.Acc().DataPageBind(sql, SqlParams, Config.DataBindObjTypeCollection.Repeater.ToString(), rep, 10, page, "?" + UrlOrderPara + UrlPara).ToString();
        }
        //�޸�
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string strLinkID = Config.Request(Request.Form["LinkID"], "0");
            if (strLinkID != "0")
            {
                Response.Redirect("Link_Add.aspx?LinkID=" + strLinkID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }
        //ɾ��
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string strLinkID = Config.Request(Request.Form["LinkID"], "0");
            if (strLinkID != "0")
            {
                string[] arrLinkID = strLinkID.Split(new char[] { ',' });
                StringBuilder strTempLinkID = new StringBuilder();
                LinkModel linkModel = new LinkModel();
                int n = 0;
                for (int i = 0; i < arrLinkID.Length; i++)
                {
                    linkModel = Factory.Link().GetInfo(arrLinkID[i]);
                    if (linkModel != null)
                    {
                        if (GetData.CheckAdminID(linkModel.AdminID, "LinkAll"))//��鴴����
                        {
                            Factory.Link().DeleteInfo(arrLinkID[i]);
                            strTempLinkID.Append(arrLinkID[i]);
                            if (i + 1 < arrLinkID.Length) strTempLinkID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("ɾ�����Ϊ" + strTempLinkID.ToString() + "����������!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("���Ϊ" + strTempLinkID.ToString() + "��������ɾ���ɹ�!", "Link.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strLinkID = Config.Request(Request.Form["LinkID"], "0");
            if (strLinkID != "0")
            {
                string[] arrLinkID = strLinkID.Split(new char[] { ',' });
                StringBuilder strTempLinkID = new StringBuilder();
                LinkModel linkModel = new LinkModel();
                int n = 0;
                for (int i = 0; i < arrLinkID.Length; i++)
                {
                    linkModel = Factory.Link().GetInfo(arrLinkID[i]);
                    if (linkModel != null)
                    {
                        if (GetData.CheckAdminID(linkModel.AdminID, "LinkAll"))//��鴴����
                        {
                            Factory.Link().UpdateCloseStatus(arrLinkID[i], "0");
                            strTempLinkID.Append(arrLinkID[i]);
                            if (i + 1 < arrLinkID.Length) strTempLinkID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("���ű��Ϊ" + strTempLinkID.ToString() + "����������!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("���Ϊ" + strTempLinkID.ToString() + "�������ӿ��ųɹ�!", "Link.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strLinkID = Config.Request(Request.Form["LinkID"], "0");
            if (strLinkID != "0")
            {
                string[] arrLinkID = strLinkID.Split(new char[] { ',' });
                StringBuilder strTempLinkID = new StringBuilder();
                LinkModel linkModel = new LinkModel();
                int n = 0;
                for (int i = 0; i < arrLinkID.Length; i++)
                {
                    linkModel = Factory.Link().GetInfo(arrLinkID[i]);
                    if (linkModel != null)
                    {
                        if (GetData.CheckAdminID(linkModel.AdminID, "LinkAll"))//��鴴����
                        {
                            Factory.Link().UpdateCloseStatus(arrLinkID[i], "1");
                            strTempLinkID.Append(arrLinkID[i]);
                            if (i + 1 < arrLinkID.Length) strTempLinkID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("�رձ��Ϊ" + strTempLinkID.ToString() + "����������!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("���Ϊ" + strTempLinkID.ToString() + "�������ӹرճɹ�!", "Link.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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

        //ת����������
        protected void btnTransfer_Click(object sender, EventArgs e)
        {
            string strLinkID = Config.Request(Request.Form["LinkID"], "0");
            if (strLinkID != "0")
            {
                Response.Redirect("Link_Transfer.aspx?LinkID=" + strLinkID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }

    }
}
