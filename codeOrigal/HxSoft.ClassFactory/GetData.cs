using System;
using System.Web;
using HxSoft.Common;
using HxSoft.Model;
using HxSoft.BLL;
using System.Web.UI.WebControls;

namespace HxSoft.ClassFactory
{
    public class GetData
    {
        #region �����ʶ(add by yang)
        /// <summary>
        /// �����ʶ
        /// </summary>
        /// <param name="strOrderKey"></param>
        /// <param name="strOrderField"></param>
        /// <param name="strAscDesc1"></param>
        /// <returns></returns>
        public static string GetOrderSign(string strOrderKey, string strOrderField, string strAscDesc1)
        {
            if (strOrderKey == strOrderField)
            {
                if (strAscDesc1 == "asc")
                    return "<img src=\"../Admin_Themes/Images/up.gif\"/>";
                else
                    return "<img src=\"../Admin_Themes/Images/down.gif\"/>";
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region �ر�״̬(add by yang)
        /// <summary>
        /// �ر�״̬
        /// </summary>
        /// <param name="strIsClose"></param>
        /// <returns></returns>
        public static string ShowCloseStatus(string strIsClose)
        {
            return strIsClose == "0" ? "����" : "<span style=\"color:red\">�ر�</span>";
        }
        #endregion

        #region ���״̬(add by yang)
        /// <summary>
        /// ���״̬
        /// </summary>
        /// <param name="strIsAudit"></param>
        /// <returns></returns>
        public static string ShowAuditStatus(string strIsAudit)
        {
            return strIsAudit == "1" ? "�����" : "<span style=\"color:red\">δ���</span>";
        }
        #endregion

        #region �Ķ�״̬(add by yang)
        /// <summary>
        /// �Ķ�״̬
        /// </summary>
        /// <param name="strIsRead"></param>
        /// <returns></returns>
        public static string ShowReadStatus(string strIsRead)
        {
            if (strIsRead == "1")
                return "�Ѷ�";
            else if (strIsRead == "0")
                return "<span style=\"color:red\">δ��</span>";
            else
                return "-";
        }
        #endregion

        #region �ظ�״̬(add by yang)
        /// <summary>
        /// �ظ�״̬
        /// </summary>
        /// <param name="strIsReply"></param>
        /// <returns></returns>
        public static string ShowReplyStatus(string strIsReply)
        {
            return strIsReply == "1" ? "�ѻظ�" : "<span style=\"color:red\">δ�ظ�</span>";
        }
        #endregion

        #region ����״̬(add by yang)
        /// <summary>
        /// ����״̬
        /// </summary>
        /// <param name="strIsEnd"></param>
        /// <returns></returns>
        public static string ShowEndStatus(string strIsEnd)
        {
            return strIsEnd == "1" ? " | �ѽ���" : "";
        }
        #endregion

        #region �Ƽ�״̬(add by yang)
        /// <summary>
        /// �Ƽ�״̬
        /// </summary>
        /// <param name="strIsRecommend"></param>
        /// <returns></returns>
        public static string ShowRecommendStatus(string strIsRecommend)
        {
            return strIsRecommend == "1" ? "<span style=\"color:red\"> |��</span>" : "";
        }
        #endregion

        #region ����״̬(add by yang)
        /// <summary>
        /// ����״̬
        /// </summary>
        /// <param name="strIsDist"></param>
        /// <returns></returns>
        public static string ShowDistStatus(string strIsDist)
        {
            return strIsDist == "1" ? "<span style=\"color:red\"> |��</span>" : "";
        }
        #endregion

        #region ����״̬(add by yang)
        /// <summary>
        /// ����״̬
        /// </summary>
        /// <param name="strIsDeal"></param>
        /// <returns></returns>
        public static string ShowDealStatus(string strIsDeal)
        {
            return strIsDeal == "1" ? "�Ѵ���" : "<span style=\"color:red\">δ����</span>";
        }
        #endregion

        #region �ʼ�����״̬(add by yang)
        /// <summary>
        /// �ʼ�����״̬
        /// </summary>
        /// <param name="strIsRec"></param>
        /// <returns></returns>
        public static string ShowRecStatus(string strIsRec)
        {
            return strIsRec == "1" ? "����" : "<span style=\"color:red\">�˶�</span>";
        }
        #endregion

        #region �Ƿ���ʾ�ڵ���(add by yang)
        /// <summary>
        /// �Ƿ���ʾ�ڵ���
        /// </summary>
        /// <param name="strIsShowNav"></param>
        /// <returns></returns>
        public static string ShowNavStatus(string strIsShowNav)
        {
            return strIsShowNav == "1" ? "<span style=\"color:red\"> |��</span>" : "";
        }
        #endregion
        //====================
        #region ��ʾ���λ����(add by yang)
        /// <summary>
        /// ��ʾ���λ����
        /// </summary>
        /// <param name="strTypeID"></param>
        /// <returns></returns>
        public static string ShowAdPositionType(string strTypeID)
        {
            switch (strTypeID)
            {
                case "1":
                    return "��ͨ��ʾ���";
                case "2":
                    return "Flash�õ�Ƭ���";
                case "3":
                    return "�������";
                case "4":
                    return "�������";
                default:
                    return "";
            }
        }
        #endregion

        #region ��ʾ������������(add by yang)
        /// <summary>
        /// ��ʾ������������
        /// </summary>
        /// <param name="strTypeID"></param>
        /// <returns></returns>
        public static string ShowLinkType(string strTypeID)
        {
            switch (strTypeID)
            {
                case "1":
                    return "��������";
                case "2":
                    return "ͼƬ����";
                default:
                    return "";
            }
        }
        #endregion

        #region ��ʾ�����ʺ�����(add by yang)
        /// <summary>
        /// ��ʾ�����ʺ�����
        /// </summary>
        /// <param name="strTypeID"></param>
        /// <returns></returns>
        public static string ShowChatType(string strTypeID)
        {
            switch (strTypeID)
            {
                case "1":
                    return "QQ";
                case "2":
                    return "MSN";
                case "3":
                    return "Skype";
                case "4":
                    return "��������";
                default:
                    return "";
            }
        }
        #endregion

        #region ��ʾ����ѡ������(add by yang)
        /// <summary>
        /// ��ʾ����ѡ������
        /// </summary>
        /// <param name="strTypeID"></param>
        /// <returns></returns>
        public static string ShowSurveyItemType(string strTypeID)
        {
            switch (strTypeID)
            {
                case "1":
                    return "��ѡ";
                case "2":
                    return "��ѡ";
                case "3":
                    return "�ı�";
                default:
                    return "";
            }
        }
        #endregion

        #region �б��¼��ѡ���Ƿ��ѡ(add by yang)
        /// <summary>
        /// �б��¼��ѡ���Ƿ��ѡ
        /// </summary>
        /// <param name="strAdminID"></param>
        /// <param name="strLimitValue"></param>
        /// <returns></returns>
        public static string ShowDisabledStatus(string strAdminID, string strLimitValue)
        {
            if (CheckAdminID(strAdminID, strLimitValue))
            {
                return "";
            }
            else
            {
                return "disabled=\"disabled\"";
            }
        }
        #endregion

        #region �ļ�(��)��ѡ���Ƿ��ѡ(add by yang)
        /// <summary>
        /// �ļ�(��)��ѡ���Ƿ��ѡ
        /// </summary>
        /// <param name="strPath"></param>
        /// <param name="strLimitValue"></param>
        /// <returns></returns>
        public static string ShowPathDisabledStatus(string strPath, string strLimitValue)
        {
            PathModel pathModel = new PathModel();
            pathModel = Factory.Path().GetInfo(strPath.ToLower());
            if (pathModel != null)
            {
                return ShowDisabledStatus(pathModel.AdminID, strLimitValue);
            }
            else
            {
                return "disabled=\"disabled\"";
            }
        }
        #endregion
        //====================
        #region �������(add by yang)
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="strDate"></param>
        /// <returns></returns>
        public static string CheckDate(string strDate)
        {
            if (Convert.ToDateTime(strDate).Year == 1900)
            {
                return "-";
            }
            else
            {
                return strDate;
            }
        }
        #endregion

        #region ��������Ȩ��(add by yang)
        /// <summary>
        /// ��������Ȩ��
        /// </summary>
        /// <param name="strAdminGroupID"></param>
        /// <param name="strLimitValue"></param>
        /// <returns></returns>
        public static string CheckAdminGroupLimitValue(string strAdminGroupID, string strLimitValue)
        {
            if (Factory.AdminGroup().GetValueByField("LimitValues", strAdminGroupID).IndexOf("," + strLimitValue + ",") > 0)
            {
                return "checked=\"checked\"";
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region ����Ա����Ȩ��(add by yang)
        /// <summary>
        /// ����Ա����Ȩ��
        /// </summary>
        /// <param name="strUserRankID"></param>
        /// <param name="strLimitValue"></param>
        /// <returns></returns>
        public static string CheckUserRankLimitValue(string strUserRankID, string strLimitValue)
        {
            if (Factory.UserRank().GetValueByField("LimitValues", strUserRankID).IndexOf("," + strLimitValue + ",") > 0)
            {
                return "checked=\"checked\"";
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region ��鵱ǰ��¼����Ա��Ȩ��(add by yang)
        /// <summary>
        /// ��鵱ǰ��¼����Ա��Ȩ��
        /// </summary>
        /// <param name="strLimitValue"></param>
        /// <returns></returns>
        public static bool LimitChk(string strLimitValue)
        {
            if (HttpContext.Current.Session["AdminID"].ToString() == Config.SystemAdminID)//��������Ա����Ȩ�޿���
            {
                return true;
            }
            else
            {
                if (Factory.Limit().GetValueByLimitValue("IsClose", strLimitValue) == "0")
                {
                    string strTempLimit = Factory.AdminGroup().GetLimitValues(HttpContext.Current.Session["AdminID"].ToString()).ToString();
                    if (strTempLimit.IndexOf("," + strLimitValue + ",") > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// ��鵱ǰ��¼����Ա��Ȩ��
        /// </summary>
        /// <param name="strLimitValue"></param>
        public static void LimitChkMsg(string strLimitValue)
        {
            if (LimitChk(strLimitValue) == false)
            {
                Config.ShowEnd("�Բ���,��û�д˲���Ȩ��!");
            }
        }
        #endregion

        #region �����Ϣ�������Ƿ�Ϊ��ǰ����Ա����ǰ����Ա�Ƿ��й���������Ϣ��Ȩ��(add by yang)
        /// <summary>
        /// �����Ϣ�������Ƿ�Ϊ��ǰ����Ա����ǰ����Ա�Ƿ��й���������Ϣ��Ȩ��
        /// </summary>
        /// <param name="strAdminID"></param>
        /// <param name="strLimitValue"></param>
        /// <returns></returns>
        public static bool CheckAdminID(string strAdminID, string strLimitValue)
        {
            if (LimitChk(strLimitValue))
            {
                return true;
            }
            else
            {
                if (strAdminID == HttpContext.Current.Session["AdminID"].ToString())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion

        #region ��鵱ǰ��¼��Ա��Ȩ��(add by wj)
        /// <summary>
        /// ��鵱ǰ��¼��Ա��Ȩ��
        /// </summary>
        /// <param name="strLimitValue"></param>
        /// <returns></returns>
        public static bool LimitChkUser(string strLimitValue)
        {
            if (Factory.Limit().GetValueByLimitValue("IsClose", strLimitValue) == "0")
            {
                string strTempLimit = Factory.UserRank().GetLimitValues(HttpContext.Current.Session["UserID"].ToString()).ToString();
                if (strTempLimit.IndexOf("," + strLimitValue + ",") > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion
        //====================
        #region ȡ�ļ�(��)������(add by yang)
        /// <summary>
        /// ȡ�ļ�(��)������
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static string GetPathAdmin(string strPath)
        {
            PathModel pathModel = new PathModel();
            pathModel = Factory.Path().GetInfo(strPath.ToLower());
            if (pathModel != null)
            {
                return Factory.Admin().GetValueByField("AdminName", pathModel.AdminID);
            }
            else
            {
                return "ϵͳ�ļ�";
            }
        }
        #endregion

        #region ȡ��Ա�û���(add by yang)
        /// <summary>
        /// ȡ��Ա�û���
        /// </summary>
        /// <param name="strUserID"></param>
        /// <returns></returns>
        public static string GetUserName(string strUserID)
        {
            if (strUserID == "-1")
            {
                return "�ο�";
            }
            else
            {
                return Factory.User().GetValueByField("UserName",strUserID);
            }
        }
        #endregion

        //====================
        #region ȡվ���б�
        /// <summary>
        /// ȡվ���б�
        /// </summary>
        /// <returns></returns>
        public static void GetConfigList(Repeater rep)
        {
            string sql = "select * from t_Config order by ListID asc";
            Factory.Acc().DataBind(sql, null, Config.DataBindObjTypeCollection.Repeater.ToString(), rep);
        }
        public static void GetConfigList(DropDownList drp, string TF, string VF)
        {
            string sql = "select " + TF + "," + VF + " from t_Config order by ListID asc";
            Factory.Acc().DataBind(sql, null, Config.DataBindObjTypeCollection.DropDownList.ToString(), drp, TF, VF);
        }
        #endregion

        #region ȡ�����ֵ��б�
        /// <summary>
        /// ȡ�����ֵ��б�
        /// </summary>
        /// <param name="strParentID"></param>
        /// <returns></returns>
        public static void GetDictionaryList(string strParentID, Repeater rep)
        {
            string sql = "select * from t_Dictionary where IsClose=0 and ParentID=" + strParentID + " order by ListID asc";
            Factory.Acc().DataBind(sql, null, Config.DataBindObjTypeCollection.Repeater.ToString(), rep);
        }
        public static void GetDictionaryList(string strParentID, DropDownList drp, string TF, string VF)
        {
            string sql = "select " + TF + "," + VF + " from t_Dictionary where IsClose=0 and ParentID=" + strParentID + " order by ListID asc";
            Factory.Acc().DataBind(sql, null, Config.DataBindObjTypeCollection.DropDownList.ToString(), drp, TF, VF);
        }
        #endregion

        #region ȡ�����б�
        /// <summary>
        /// ȡ�����б�
        /// </summary>
        /// <param name="strParentID"></param>
        /// <returns></returns>
        public static void GetAreaList(string strParentID, Repeater rep)
        {
            string sql = "select * from t_Area where IsClose=0 and ParentID=" + strParentID + " order by ListID asc";
            Factory.Acc().DataBind(sql, null, Config.DataBindObjTypeCollection.Repeater.ToString(), rep);
        }
        public static void GetAreaList(string strParentID, DropDownList drp, string TF, string VF)
        {
            string sql = "select " + TF + "," + VF + " from t_Area where IsClose=0 and ParentID=" + strParentID + " order by ListID asc";
            Factory.Acc().DataBind(sql, null, Config.DataBindObjTypeCollection.DropDownList.ToString(), drp, TF, VF);
        }
        #endregion
    }
}
