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
    public partial class Industry : System.Web.UI.Page
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
            GetData.LimitChkMsg("Industry");
            lbtnAdd.Visible = GetData.LimitChk("IndustryAdd");
            lbtnEdit.Visible = GetData.LimitChk("IndustryEdit");
            lbtnMove.Visible = GetData.LimitChk("IndustryMove");
            lbtnDel.Visible = GetData.LimitChk("IndustryDel");
            lbtnOpen.Visible = GetData.LimitChk("IndustryOpen");
            lbtnClose.Visible = GetData.LimitChk("IndustryClose");
            if (!Page.IsPostBack)
            {
                lblNav.Text=Factory.Industry().ShowPath(ParentID).ToString();
                btnQuery.PostBackUrl = "Industry.aspx?ParentID=" + ParentID;
                lbtnAdd.OnClientClick = "javascript:return GoTo('Industry_Add.aspx?ParentID="+ParentID+"')";
                //�����ϼ�
                IndustryModel indModel = new IndustryModel();
                indModel = Factory.Industry().GetInfo(ParentID);
                if (indModel != null)
                {
                    lbtnGoBack.OnClientClick = "javascript:return GoTo('Industry.aspx?ParentID=" + indModel.ParentID + "')";
                }
                Repeater_Bind(repList);
            }
        }
        //������
        protected void Repeater_Bind(Repeater rep)
        {
            string sql = "select * from t_Industry where 1=1 and ParentID=" + ParentID + SqlQuery + SqlOrder;
            pager.InnerHtml = Factory.Acc().DataPageBind(sql, SqlParams, Config.DataBindObjTypeCollection.Repeater.ToString(), rep, 10, page, "?ParentID=" + ParentID + "&" + UrlOrderPara + UrlPara).ToString();
        }
        //�޸�
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string strIndustryID = Config.Request(Request.Form["IndustryID"], "0");
            if (strIndustryID != "0")
            {
                Response.Redirect("Industry_Add.aspx?ParentID=" + ParentID + "&IndustryID=" + strIndustryID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }
        //�ƶ�
        protected void lbtnMove_Click(object sender, EventArgs e)
        {
            string strIndustryID = Config.Request(Request.Form["IndustryID"], "0");
            if (strIndustryID != "0")
            {
                Response.Redirect("Industry_Move.aspx?ParentID=" + ParentID + "&IndustryID=" + strIndustryID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
            }
        }

        //ɾ��
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string strIndustryID = Config.Request(Request.Form["IndustryID"], "0");
            if (strIndustryID != "0")
            {
                string[] arrIndustryID = strIndustryID.Split(new char[] { ',' });
                StringBuilder strTempIndustryID = new StringBuilder();
                IndustryModel indModel = new IndustryModel();
                int n = 0;
                for (int i = 0; i < arrIndustryID.Length; i++)
                {
                    indModel = Factory.Industry().GetInfo(arrIndustryID[i]);
                    if (indModel != null)
                    {
                        if (Convert.ToInt32(indModel.ChildNum)==0)
                        {
                            if (GetData.CheckAdminID(indModel.AdminID, "IndustryAll"))//��鴴����
                            {
                                Factory.Industry().DeleteInfo(arrIndustryID[i]);
                                strTempIndustryID.Append(arrIndustryID[i]);
                                if (i + 1 < arrIndustryID.Length) strTempIndustryID.Append(",");
                                n++;
                            }
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("ɾ�����Ϊ" + strTempIndustryID.ToString() + "����ҵ!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("���Ϊ" + strTempIndustryID.ToString() + "��ҵɾ���ɹ�!", "Industry.aspx?ParentID=" + ParentID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strIndustryID = Config.Request(Request.Form["IndustryID"], "0");
            if (strIndustryID != "0")
            {
                string[] arrIndustryID = strIndustryID.Split(new char[] { ',' });
                StringBuilder strTempIndustryID = new StringBuilder();
                IndustryModel indModel = new IndustryModel();
                int n = 0;
                for (int i = 0; i < arrIndustryID.Length; i++)
                {
                    indModel = Factory.Industry().GetInfo(arrIndustryID[i]);
                    if (indModel != null)
                    {
                        if (GetData.CheckAdminID(indModel.AdminID, "IndustryAll"))//��鴴����
                        {
                            Factory.Industry().UpdateCloseStatus(arrIndustryID[i], "0");
                            strTempIndustryID.Append(arrIndustryID[i]);
                            if (i + 1 < arrIndustryID.Length) strTempIndustryID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("���ű��Ϊ" + strTempIndustryID.ToString() + "����ҵ!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("���Ϊ" + strTempIndustryID.ToString() + "��ҵ���ųɹ�!", "Industry.aspx?ParentID=" + ParentID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
            string strIndustryID = Config.Request(Request.Form["IndustryID"], "0");
            if (strIndustryID != "0")
            {
                string[] arrIndustryID = strIndustryID.Split(new char[] { ',' });
                StringBuilder strTempIndustryID = new StringBuilder();
                IndustryModel indModel = new IndustryModel();
                int n = 0;
                for (int i = 0; i < arrIndustryID.Length; i++)
                {
                    indModel = Factory.Industry().GetInfo(arrIndustryID[i]);
                    if (indModel != null)
                    {
                        if (GetData.CheckAdminID(indModel.AdminID, "IndustryAll"))//��鴴����
                        {
                            Factory.Industry().UpdateCloseStatus(arrIndustryID[i], "1");
                            strTempIndustryID.Append(arrIndustryID[i]);
                            if (i + 1 < arrIndustryID.Length) strTempIndustryID.Append(",");
                            n++;
                        }
                    }
                }
                if (n > 0)
                {
                    Factory.AdminLog().InsertLog("�رձ��Ϊ" + strTempIndustryID.ToString() + "����ҵ!", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("���Ϊ" + strTempIndustryID.ToString() + "��ҵ�رճɹ�!", "Industry.aspx?ParentID=" + ParentID + "&" + UrlOrderPara + UrlPara + "page=" + page.ToString());
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
