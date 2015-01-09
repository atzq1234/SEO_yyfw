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
    public partial class AdminGroup_SetLimit : System.Web.UI.Page
    {
        /// <summary>
        /// ������:��С��
        /// ����:2010-12-6
        /// </summary>
        //����ȫ�ֱ���
        public string AdminGroupID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["AdminGroupID"], 0).ToString();
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
        public string strAdminGroupName
        {
            get
            {
                return Config.Request(Request["txtAdminGroupName"], "");
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
                if (strAdminGroupName != "") TempSql.Append(" and AdminGroupName like @AdminGroupName");
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
                if (strAdminGroupName != "") listParams.Add(Config.Conn().CreateDbParameter("@AdminGroupName", "%" + strAdminGroupName + "%"));
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
                TempUrl.Append("txtAdminGroupName=" + Server.UrlEncode(strAdminGroupName) + "&");
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
                GetData.LimitChkMsg("AdminGroupSetLimit");
                if (AdminGroupID == "0")
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
            AdminGroupModel admGrModel = new AdminGroupModel();
            //***
            string strLimitValue = Config.Request(Request.Form["LimitValue"], "-1,-1");
            admGrModel.LimitValues = "-1," + strLimitValue + ",-1";
            //***

            AdminGroupModel admGrModel_2 = new AdminGroupModel();
            admGrModel_2 = Factory.AdminGroup().GetInfo(AdminGroupID);
            if (admGrModel_2 != null)
            {
                if (GetData.CheckAdminID(admGrModel_2.AdminID, "AdminGroupAll"))//��鴴����
                {
                    //���Ȩ�޻���
                    Factory.AdminInGroup().RemoveLimitCache(AdminGroupID);
                    
                    Factory.AdminGroup().SetLimit(admGrModel, AdminGroupID);
                    Factory.AdminLog().InsertLog("���ù�������Ϊ" + AdminGroupID + "�Ĳ���Ȩ�ޡ�", Session["AdminID"].ToString());
                    Config.MsgGotoUrl("����ɹ���", "AdminGroup.aspx?" + UrlOrderPara + UrlPara + "page=" + page);
                }
            }
        }
        //��ʾ����
        protected void ShowInfo()
        {
            //������
            AdminGroupModel admGrModel = new AdminGroupModel();
            admGrModel = Factory.AdminGroup().GetInfo(AdminGroupID);
            if (admGrModel != null)
            {
                if (GetData.CheckAdminID(admGrModel.AdminID, "AdminGroupAll"))//��鴴����
                {
                    lblAdminGroupName.Text = admGrModel.AdminGroupName;
                }
                else
                {
                    Config.ShowEnd("��û�в鿴����Ϣ��Ȩ�ޣ�");
                }
            }
            else
            {
                Config.ShowEnd("��û�в鿴����Ϣ��Ȩ�ޣ�");
            }

            //����Ȩ���б�
            DataList1.DataKeyField = "LimitID";
            string strSql;
            if (Session["AdminID"].ToString() != Config.SystemAdminID)
            {
                strSql = " and IsDist=1";
            }
            else
            {
                strSql = "";
            }
            Factory.Acc().DataBind("select * from t_Limit where ParentID=0 " + strSql + " and LimitID<>" + Config.SysUserLimitMouldID + " and IsClose=0 order by ListID asc", null,Config.DataBindObjTypeCollection.DataList.ToString(), DataList1);
        }

        //��Ȩ���б�
        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            string strParentID = DataList1.DataKeys[e.Item.ItemIndex].ToString();
            DataList DataList2 = (DataList)e.Item.FindControl("DataList2");
            Factory.Acc().DataBind("select * from t_Limit where ParentID=" + strParentID + " and IsClose=0 order by ListID asc", null,Config.DataBindObjTypeCollection.DataList.ToString(), DataList2);
        }
    }
}
