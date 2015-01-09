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

namespace HxSoft.Web.Admin.Message
{
    public partial class Message_View : System.Web.UI.Page
    {
        /// <summary>
        /// ������:��С��
        /// ����:2010-12-6
        /// </summary>
        //����ȫ�ֱ���
        public string MessageID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["MessageID"], 0).ToString();
            }
        }
        public int page
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["page"], 1);
            }
        }
        public bool IsCanReply
        {
            get 
            { 
                return GetData.LimitChk("MessageReply");
            }
        }
        public bool IsCanDel
        {
            get
            {
                return GetData.LimitChk("MessageDel");
            }
        }

        #region ****�������****
        public string strOrderKey
        {
            get
            {
                return Config.Request(Request["OrderKey"], "AddTime");
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
                return " order by IsReply asc," + strOrderKey + " " + strAscDesc1;
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
        public string strDictionaryID
        {
            get
            {
                return Config.Request(Request["DictionaryID"], "-1");
            }
        }
        public string strTitle
        {
            get
            {
                return Config.Request(Request["txtTitle"], "");
            }
        }
        public string strUserName
        {
            get
            {
                return Config.Request(Request["txtUserName"], "");
            }
        }
        public string strIsReply
        {
            get
            {
                return Config.Request(Request["radIsReply"], "-1");
            }
        }
        #endregion
        #region ****��ѯ���****
        public string SqlQuery
        {
            get
            {
                StringBuilder TempSql = new StringBuilder("");
                if (strDictionaryID != "-1") TempSql.Append(" and (DictionaryID=@DictionaryID or DictionaryID in (" + Factory.Dictionary().GetSubDictionarySql(strDictionaryID) + "))");
                if (strTitle != "") TempSql.Append(" and Title like @Title");
                if (strUserName != "") TempSql.Append(" and UserID in (select UserID from t_User where UserName like @UserName)");
                if (strIsReply != "-1") TempSql.Append(" and IsReply = @IsReply");
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
                if (strDictionaryID != "-1") listParams.Add(Config.Conn().CreateDbParameter("@DictionaryID", strDictionaryID));
                if (strTitle != "") listParams.Add(Config.Conn().CreateDbParameter("@Title", "%" + strTitle + "%"));
                if (strUserName != "") listParams.Add(Config.Conn().CreateDbParameter("@UserName", "%" + strUserName + "%"));
                if (strIsReply != "-1") listParams.Add(Config.Conn().CreateDbParameter("@IsReply", strIsReply));
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
                TempUrl.Append("DictionaryID=" + Server.UrlEncode(strDictionaryID) + "&");
                TempUrl.Append("txtTitle=" + Server.UrlEncode(strTitle) + "&");
                TempUrl.Append("txtUserName=" + Server.UrlEncode(strUserName) + "&");
                TempUrl.Append("radIsReply=" + Server.UrlEncode(strIsReply) + "&");
                return TempUrl.ToString();
            }
        }
        #endregion
        //ҳ���ʼ��
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            GetData.LimitChkMsg("Message");
            if (!Page.IsPostBack)
            {
                ShowInfo();
                Repeater_Bind(repList);
            }
        }
        //��������
        protected void btnSave_Click(object sender, EventArgs e)
        {
            MessageModel mesModel = new MessageModel();
            mesModel.DictionaryID = hidDictionaryID.Value;
            mesModel.UserID = "0";
            mesModel.Title = "Re:" + lblTitle.Text.Trim();
            mesModel.MessageContent = Config.HTMLCls(txtMessageContent.Text.Trim());
            mesModel.AdminID = Session["AdminID"].ToString();
            mesModel.ParentID = MessageID;
            mesModel.AddTime = DateTime.Now.ToString();
            mesModel.IsRead = "0";
            mesModel.IsReply = "0";
            mesModel.IsEnd = "0";
            Factory.Message().InsertInfo(mesModel);
            //�Ƿ����������
            if (chkIsEnd.Checked)
            {
                Factory.Message().UpdateEndStatus(MessageID, "1");//����
            }
            //��������Ϊδ��
            Factory.Message().UpdateReadStatus(MessageID, "0");
            //��������Ϊ�ѻظ�
            Factory.Message().UpdateReplyStatus(MessageID, "1");
            Factory.AdminLog().InsertLog("�ظ����Ϊ" + MessageID + "�����ԡ�", Session["AdminID"].ToString());
            Config.MsgGotoUrl("�ظ��ɹ���", "Message_View.aspx?MessageID=" + MessageID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
        }
        //��ʾ����
        protected void ShowInfo()
        {
            MessageModel mesModel = new MessageModel();
            mesModel = Factory.Message().GetInfo(MessageID);
            if (mesModel != null)
            {
                hidDictionaryID.Value = mesModel.DictionaryID;
                lblUserName.Text = GetData.GetUserName(mesModel.UserID);
                lblTitle.Text = mesModel.Title;
                lblMessageContent.Text = mesModel.MessageContent;
                lblAddTime.Text = mesModel.AddTime;
                if (mesModel.UserID == "-1")//�ο�
                {
                    tr1_1.Visible = false;
                    tr1_2.Visible = false;
                    tr1_3.Visible = false;
                    //
                    tr2_1.Visible = IsCanReply;
                    tr2_2.Visible = !IsCanReply;
                }
                else//��Ա
                {
                    if (mesModel.IsEnd == "1" && mesModel.IsReply == "1")
                    {
                        tr1_1.Visible = false;
                        tr1_2.Visible = false;
                        tr1_3.Visible = false;
                        //
                        tr2_1.Visible = false;
                        tr2_2.Visible = true;
                    }
                    else
                    {
                        tr1_1.Visible = IsCanReply;
                        tr1_2.Visible = IsCanReply;
                        tr1_3.Visible = IsCanReply;
                        //
                        tr2_1.Visible = false;
                        tr2_2.Visible = !IsCanReply;
                    }
                }
            }
            else
            {
                Config.ShowEnd("��û�в鿴����Ϣ��Ȩ�ޣ�");
            }
        }

        //������
        protected void Repeater_Bind(Repeater rep)
        {
            string sql = "select * from t_Message where 1=1 and ParentID=" + MessageID + " order by AddTime asc";
            Factory.Acc().DataBind(sql,null,Config.DataBindObjTypeCollection.Repeater.ToString(), rep);
        }

        //ɾ������
        protected void repList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                string strMessageID = e.CommandArgument.ToString();
                Factory.Message().DeleteInfo(strMessageID);
                Factory.AdminLog().InsertLog("ɾ�����Ϊ" + strMessageID.ToString() + "�����Իظ���", Session["AdminID"].ToString());
                Config.MsgGotoUrl("���Ϊ" + strMessageID.ToString() + "���Իظ�ɾ���ɹ�!", "Message_View.aspx?MessageID=" + MessageID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }

        //����Ϊ�ѻظ�
        protected void btnReply_Click(object sender, EventArgs e)
        {
            if (MessageID != "0")
            {
                Factory.Message().UpdateReplyStatus(MessageID, "1");
                Factory.AdminLog().InsertLog("���ñ��Ϊ" + MessageID + "������Ϊ�ѻظ���", Session["AdminID"].ToString());
                Config.MsgGotoUrl("���óɹ���", "Message_View.aspx?MessageID=" + MessageID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }
    }
}
