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

namespace HxSoft.Web.Admin._System
{
    public partial class Limit : System.Web.UI.Page
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
        public string ParentID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["ParentID"], 0).ToString();
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
            GetData.LimitChkMsg("Limit");
            lbtnAdd.Visible = GetData.LimitChk("LimitAdd");
            lbtnEdit.Visible = GetData.LimitChk("LimitEdit");
            lbtnMove.Visible = GetData.LimitChk("LimitMove");
            lbtnDel.Visible = GetData.LimitChk("LimitDel");
            lbtnOpen.Visible = GetData.LimitChk("LimitOpen");
            lbtnClose.Visible = GetData.LimitChk("LimitClose");
            if (!Page.IsPostBack)
            {
                lblNav.Text=Factory.Limit().ShowPath(ParentID).ToString();
                btnQuery.PostBackUrl = "Limit.aspx?ParentID=" + ParentID;
                lbtnAdd.OnClientClick = "javascript:return GoTo('Limit_Add.aspx?ParentID="+ParentID+"')";
                //�����ϼ�
                LimitModel limModel = new LimitModel();
                limModel = Factory.Limit().GetInfo(ParentID);
                if (limModel != null)
                {
                    lbtnGoBack.OnClientClick = "javascript:return GoTo('Limit.aspx?ParentID=" + limModel.ParentID + "')";
                }
                Repeater_Bind(repList);
            }
        }
        //������
        protected void Repeater_Bind(Repeater rep)
        {
            string sql = "select * from t_Limit where 1=1 and ParentID=" + ParentID + SqlQuery + SqlOrder;
            pager.InnerHtml = Factory.Acc().DataPageBind(sql, SqlParams, Config.DataBindObjTypeCollection.Repeater.ToString(), rep, 10, page, "?ParentID=" + ParentID + "&" + UrlOrderPara + UrlPara).ToString();
        }
        //�޸�
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string strLimitID = Config.Request(Request.Form["LimitID"], "0");
            if (strLimitID != "0")
            {
                Response.Redirect("Limit_Add.aspx?ParentID=" + ParentID + "&LimitID=" + strLimitID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }
        //�ƶ�
        protected void lbtnMove_Click(object sender, EventArgs e)
        {
            string strLimitID = Config.Request(Request.Form["LimitID"], "0");
            if (strLimitID != "0")
            {
                Response.Redirect("Limit_Move.aspx?ParentID=" + ParentID + "&LimitID=" + strLimitID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }

        //ɾ��
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string strLimitID = Config.Request(Request.Form["LimitID"], "0");
            if (strLimitID != "0")
            {
                string[] arrLimitID = strLimitID.Split(new char[] { ',' });
                StringBuilder strTempLimitID = new StringBuilder();
                LimitModel limModel = new LimitModel();
                int n = 0;
                for (int i = 0; i < arrLimitID.Length; i++)
                {
                    limModel = Factory.Limit().GetInfo(arrLimitID[i]);
                    if (limModel != null)
                    {
                        if (Convert.ToInt32(limModel.ChildNum)==0)
                        {
                            if (GetData.CheckAdminID(limModel.AdminID, "LimitAll"))//��鴴����
                            {
                                Factory.Limit().DeleteInfo(arrLimitID[i]);
                                strTempLimitID.Append(arrLimitID[i]);
                                if (i + 1 < arrLimitID.Length) strTempLimitID.Append(",");
                                n++;
                            }
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("ɾ�����Ϊ" + strTempLimitID.ToString() + "��Ȩ���ֶ�!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("���Ϊ" + strTempLimitID.ToString() + "Ȩ���ֶ�ɾ���ɹ�!", "Limit.aspx?ParentID=" + ParentID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
                }
                else
                {
                    Config.MsgGoBack("����ʧ�ܣ�����ɾ���Ӽ�!");
                }
            }
        }

        //��������
        protected void btnOpen_Click(object sender, EventArgs e)
        {
            string strLimitID = Config.Request(Request.Form["LimitID"], "0");
            if (strLimitID != "0")
            {
                string[] arrLimitID = strLimitID.Split(new char[] { ',' });
                StringBuilder strTempLimitID = new StringBuilder();
                LimitModel limModel = new LimitModel();
                int n = 0;
                for (int i = 0; i < arrLimitID.Length; i++)
                {
                    limModel = Factory.Limit().GetInfo(arrLimitID[i]);
                    if (limModel != null)
                    {
                        if (GetData.CheckAdminID(limModel.AdminID, "LimitAll"))//��鴴����
                        {
                            Factory.Limit().UpdateCloseStatus(arrLimitID[i], "0");
                            strTempLimitID.Append(arrLimitID[i]);
                            if (i + 1 < arrLimitID.Length) strTempLimitID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("���ű��Ϊ" + strTempLimitID.ToString() + "��Ȩ���ֶ�!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("���Ϊ" + strTempLimitID.ToString() + "Ȩ���ֶο��ųɹ�!", "Limit.aspx?ParentID=" + ParentID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strLimitID = Config.Request(Request.Form["LimitID"], "0");
            if (strLimitID != "0")
            {
                string[] arrLimitID = strLimitID.Split(new char[] { ',' });
                StringBuilder strTempLimitID = new StringBuilder();
                LimitModel limModel = new LimitModel();
                int n = 0;
                for (int i = 0; i < arrLimitID.Length; i++)
                {
                    limModel = Factory.Limit().GetInfo(arrLimitID[i]);
                    if (limModel != null)
                    {
                        if (GetData.CheckAdminID(limModel.AdminID, "LimitAll"))//��鴴����
                        {
                            Factory.Limit().UpdateCloseStatus(arrLimitID[i], "1");
                            strTempLimitID.Append(arrLimitID[i]);
                            if (i + 1 < arrLimitID.Length) strTempLimitID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("�رձ��Ϊ" + strTempLimitID.ToString() + "��Ȩ���ֶ�!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("���Ϊ" + strTempLimitID.ToString() + "Ȩ���ֶιرճɹ�!", "Limit.aspx?ParentID=" + ParentID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
