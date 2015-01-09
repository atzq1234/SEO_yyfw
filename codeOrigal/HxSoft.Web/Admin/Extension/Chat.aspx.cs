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

namespace HxSoft.Web.Admin.Extension
{
    public partial class Chat : System.Web.UI.Page
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
        public string strNickName
        {
            get
            {
                return Config.Request(Request["txtNickName"], "");
            }
        }
        public string strAccount
        {
            get
            {
                return Config.Request(Request["txtAccount"], "");
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
                if (strTypeID != "-1") TempSql.Append(" and TypeID =@TypeID");
                if (strNickName != "") TempSql.Append(" and NickName like @NickName");
                if (strAccount != "") TempSql.Append(" and Account like @Account");
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
                if (strTypeID != "-1") listParams.Add(Config.Conn().CreateDbParameter("@TypeID", strTypeID));
                if (strNickName != "") listParams.Add(Config.Conn().CreateDbParameter("@NickName", "%" + strNickName + "%"));
                if (strAccount != "") listParams.Add(Config.Conn().CreateDbParameter("@Account", "%" + strAccount + "%"));
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
                TempUrl.Append("drpTypeID=" + Server.UrlEncode(strTypeID) + "&");
                TempUrl.Append("txtNickName=" + Server.UrlEncode(strNickName) + "&");
                TempUrl.Append("txtAccount=" + Server.UrlEncode(strAccount) + "&");
                TempUrl.Append("radIsClose=" + Server.UrlEncode(strIsClose) + "&");
                return TempUrl.ToString();
            }
        }
        #endregion
        //ҳ���ʼ��
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            GetData.LimitChkMsg("Chat");
            lbtnAdd.Visible = GetData.LimitChk("ChatAdd");
            lbtnEdit.Visible = GetData.LimitChk("ChatEdit");
            lbtnDel.Visible = GetData.LimitChk("ChatDel");
            lbtnOpen.Visible = GetData.LimitChk("ChatOpen");
            lbtnClose.Visible = GetData.LimitChk("ChatClose");
            if (!Page.IsPostBack)
            {
                Repeater_Bind(repList);
            }
        }
        //������
        protected void Repeater_Bind(Repeater rep)
        {
            string sql = "select * from t_Chat where 1=1 " + SqlQuery + SqlOrder;
            pager.InnerHtml = Factory.Acc().DataPageBind(sql, SqlParams, Config.DataBindObjTypeCollection.Repeater.ToString(), rep, 10, page, "?" + UrlOrderPara + UrlPara).ToString();
        }
        //�޸�
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string strChatID = Config.Request(Request.Form["ChatID"], "0");
            if (strChatID != "0")
            {
                Response.Redirect("Chat_Add.aspx?ChatID=" + strChatID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }
        //ɾ��
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string strChatID = Config.Request(Request.Form["ChatID"], "0");
            if (strChatID != "0")
            {
                string[] arrChatID = strChatID.Split(new char[] { ',' });
                StringBuilder strTempChatID = new StringBuilder();
                ChatModel chaModel = new ChatModel();
                int n = 0;
                for (int i = 0; i < arrChatID.Length; i++)
                {
                    chaModel = Factory.Chat().GetInfo(arrChatID[i]);
                    if (chaModel != null)
                    {
                        if (GetData.CheckAdminID(chaModel.AdminID, "ChatAll"))//��鴴����
                        {
                            Factory.Chat().DeleteInfo(arrChatID[i]);
                            strTempChatID.Append(arrChatID[i]);
                            if (i + 1 < arrChatID.Length) strTempChatID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("ɾ�����Ϊ" + strTempChatID.ToString() + "�������ʺ�!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("���Ϊ" + strTempChatID.ToString() + "�����ʺ�ɾ���ɹ�!", "Chat.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strChatID = Config.Request(Request.Form["ChatID"], "0");
            if (strChatID != "0")
            {
                string[] arrChatID = strChatID.Split(new char[] { ',' });
                StringBuilder strTempChatID = new StringBuilder();
                ChatModel chaModel = new ChatModel();
                int n = 0;
                for (int i = 0; i < arrChatID.Length; i++)
                {
                    chaModel = Factory.Chat().GetInfo(arrChatID[i]);
                    if (chaModel != null)
                    {
                        if (GetData.CheckAdminID(chaModel.AdminID, "ChatAll"))//��鴴����
                        {
                            Factory.Chat().UpdateCloseStatus(arrChatID[i], "0");
                            strTempChatID.Append(arrChatID[i]);
                            if (i + 1 < arrChatID.Length) strTempChatID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("���ű��Ϊ" + strTempChatID.ToString() + "�������ʺ�!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("���Ϊ" + strTempChatID.ToString() + "�����ʺſ��ųɹ�!", "Chat.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strChatID = Config.Request(Request.Form["ChatID"], "0");
            if (strChatID != "0")
            {
                string[] arrChatID = strChatID.Split(new char[] { ',' });
                StringBuilder strTempChatID = new StringBuilder();
                ChatModel chaModel = new ChatModel();
                int n = 0;
                for (int i = 0; i < arrChatID.Length; i++)
                {
                    chaModel = Factory.Chat().GetInfo(arrChatID[i]);
                    if (chaModel != null)
                    {
                        if (GetData.CheckAdminID(chaModel.AdminID, "ChatAll"))//��鴴����
                        {
                            Factory.Chat().UpdateCloseStatus(arrChatID[i], "1");
                            strTempChatID.Append(arrChatID[i]);
                            if (i + 1 < arrChatID.Length) strTempChatID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("�رձ��Ϊ" + strTempChatID.ToString() + "�������ʺ�!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("���Ϊ" + strTempChatID.ToString() + "�����ʺŹرճɹ�!", "Chat.aspx?" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
