using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Caching;
using HxSoft.Common;
using HxSoft.Model;
using HxSoft.DAL;

namespace HxSoft.BLL
{
    /// <summary>
    ///��Ŀ����-ҵ���߼���
    /// ������:��С��
    /// ����:2010-12-6
    /// </summary>

    public class ClassBLL
    {

        private readonly ClassDAL claDAL = new ClassDAL();

        #region �����Ϣ,����ĳ�ֶε�Ψһ��
        /// <summary>
        /// �����Ϣ,����ĳ�ֶε�Ψһ��
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue, string strConfigID)
        {
            return claDAL.CheckInfo(strFieldName, strFieldValue, strConfigID);
        }
        public bool CheckInfo(string strFieldName, string strFieldValue, string strConfigID, string strClassID)
        {
            return claDAL.CheckInfo(strFieldName, strFieldValue, strConfigID, strClassID);
        }
        public bool CheckInfo2(string strFieldName, string strFieldValue, string strParentID)
        {
            return claDAL.CheckInfo2(strFieldName, strFieldValue, strParentID);
        }
        public bool CheckInfo2(string strFieldName, string strFieldValue, string strParentID, string strClassID)
        {
            return claDAL.CheckInfo2(strFieldName, strFieldValue, strParentID, strClassID);
        }
        #endregion

        #region ��ȡ��Ϣ
        /// <summary>
        /// ��ȡ��Ϣ
        /// </summary>
        public ClassModel GetInfo(string strClassID)
        {
            return claDAL.GetInfo(strClassID);
        }
        /// <summary>
        /// ǰ̨��ȡ��Ϣ
        /// </summary>
        public ClassModel GetInfo2(string strClassID)
        {
            return claDAL.GetInfo2(strClassID);
        }

        public IList<ClassModel> GetInfoListByParentID(string strParentID)
        {
            return claDAL.GetInfoListByParentID(strParentID);
        }
        #endregion

        #region �ӻ����ȡ��Ϣ
        /// <summary>
        /// �ӻ����ȡ��Ϣ
        /// </summary>
        public ClassModel GetCacheInfo(string strClassID)
        {
            string key = "Cache_Class_Model_" + strClassID;
            if (HttpRuntime.Cache[key] != null)
                return (ClassModel)HttpRuntime.Cache[key];
            else
            {
                ClassModel claModel = claDAL.GetInfo(strClassID);
                CacheHelper.AddCache(key, claModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return claModel;
            }
        }
        /// <summary>
        /// ǰ̨�ӻ����ȡ��Ϣ
        /// </summary>
        public ClassModel GetCacheInfo2(string strConfigID)
        {
            string key = "Cache_Class_Model_" + strConfigID;
            if (HttpRuntime.Cache[key] != null)
                return (ClassModel)HttpRuntime.Cache[key];
            else
            {
                ClassModel claModel = claDAL.GetInfo2(strConfigID);
                CacheHelper.AddCache(key, claModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return claModel;
            }
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void InsertInfo(ClassModel claModel)
        {
            claDAL.InsertInfo(claModel);
            //�����Ӽ���
            claDAL.AddChildNum(claModel.ParentID);
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void UpdateInfo(ClassModel claModel, string strClassID)
        {
            claDAL.UpdateInfo(claModel, strClassID);
            string key = "Cache_Class_Model_" + strClassID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region ɾ����Ϣ
        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        public void DeleteInfo(string strClassID)
        {
            //ȡParentID
            ClassModel claModel = GetInfo(strClassID);
            if (claModel != null)
            {
                //�����Ӽ���
                claDAL.CutChildNum(claModel.ParentID);
            }
            string key = "Cache_Class_Model_" + strClassID;
            CacheHelper.RemoveCache(key);
            claDAL.DeleteInfo(strClassID);
        }
        #endregion

        #region ����״̬
        /// <summary>
        /// ����״̬
        /// </summary>
        public void UpdateCloseStatus(string strClassID, string strIsClose)
        {
            claDAL.UpdateCloseStatus(strClassID, strIsClose);
            string key = "Cache_Class_Model_" + strClassID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region �ƶ���Ϣ
        /// <summary>
        /// �ƶ���Ϣ
        /// </summary>
        public void MoveInfo(ClassModel claModel, string strClassID)
        {
            claDAL.MoveInfo(claModel, strClassID);
        }
        #endregion

        #region ��ȡ�����
        /// <summary>
        /// ��ȡ�����
        /// </summary>
        public string GetListID(string strParentID)
        {
            return claDAL.GetListID(strParentID);
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strParentID, string strListID, string strOldListID)
        {
            claDAL.OrderInfo(strParentID,strListID,strOldListID);
        }
        #endregion

        #region �ƶ�ʱ�����Ӽ���
        /// <summary>
        /// �޸�ʱ�����Ӽ���
        /// </summary>
        public void UpdateChildNum(string strParentID, string strOldParentID)
        {
            if (strParentID != strOldParentID)
            {
                claDAL.AddChildNum(strParentID);
                claDAL.CutChildNum(strOldParentID);
            }
        }
        #endregion

        #region ��ʾ���������б�
        /// <summary>
        /// ��ʾ���������б�
        /// </summary>
        public void ShowSelectTree(string strParentID, DropDownList drp, string strSql, string strClassPropertyID)
        {
            claDAL.ShowSelectTree(strParentID, 0, drp, strSql,strClassPropertyID);
        }
        #endregion

        #region ȡ��һ�����ID
        /// <summary>
        /// ȡ��һ�����ID
        /// </summary>
        public string GetFirstClassID(string strParentID)
        {
            return claDAL.GetFirstClassID(strParentID);
        }
        #endregion

        #region ȡ����ĿID
        /// <summary>
        /// ȡ����ĿID
        /// </summary>
        public StringBuilder GetSubClassID(string strParentID)
        {
            StringBuilder strTemp = new StringBuilder();
            strTemp.Append("-1,");
            strTemp.Append(claDAL.GetSubClassID(strParentID));
            strTemp.Append("-1");
            return strTemp;
        }
        #endregion

        #region ȡ·��
        /// <summary>
        /// ȡ·��
        /// </summary>
        public StringBuilder GetPath(string strClassID)
        {
            return claDAL.GetPath(strClassID);
        }
        #endregion

        #region ȡ��ǰ��Ŀ
        /// <summary>
        /// ȡ��ǰ��Ŀ
        /// </summary>
        public StringBuilder GetClassBlock(string strClassPath, int intDepth)
        {
            return claDAL.GetClassBlock(strClassPath, intDepth);
        }
        #endregion

        #region ȡ��Ŀ�б�
        /// <summary>
        /// ȡ��Ŀ�б�(a)
        /// </summary>
        public StringBuilder GetClassList(string ParentID, string strSeparator, int intShowLen, string strLinkKey)
        {
            return claDAL.GetClassList(ParentID, strSeparator, intShowLen, strLinkKey);
        }
        /// <summary>
        /// ȡ��Ŀ�б�(li)
        /// </summary>
        public StringBuilder GetClassList(string ParentID, string strStyleClass, string ClassID, int intShowLen, string strLinkKey)
        {
            return claDAL.GetClassList(ParentID, strStyleClass, ClassID, intShowLen, strLinkKey);
        }
        /// <summary>
        /// ȡ��Ŀ�б�(�ݹ�)
        /// </summary>
        public StringBuilder GetClassList(string ParentID, string strStyleClass, string strClassPath, int i, int intShowLen, string strLinkKey)
        {
            return claDAL.GetClassList(ParentID, strStyleClass, strClassPath, i, intShowLen, strLinkKey);
        }
        #endregion

        #region ��ʾ·��
        /// <summary>
        /// ��ʾ·��
        /// </summary>
        public StringBuilder ShowPath(string strClassID)
        {
            StringBuilder tempStr = new StringBuilder("�����");
            ClassModel claModel = new ClassModel();
            claModel = claDAL.GetInfo(strClassID);
            if (claModel == null)
            {
                return tempStr;
            }
            else
            {
                string strPath = claDAL.GetPath(strClassID).ToString();
                string[] arrPath = strPath.Split(new char[] { ',' });
                for (int i = 0; i < arrPath.Length; i++)
                {
                    ClassModel claModel_2 = new ClassModel();
                    claModel_2 = claDAL.GetInfo(arrPath[i]);
                    if (claModel_2 != null)
                    {
                        tempStr.Append(" > " + claModel_2.ClassName);
                    }
                }
                return tempStr;
            }
        }
        #endregion

        #region ȡλ�õ���
        /// <summary>
        /// ȡλ�õ���
        /// </summary>
        public StringBuilder GetClassNav(string strClassPath, int intDepth,  string strNavStr)
        {
            StringBuilder strClassNav = new StringBuilder();
            string[] ArrClassPath = strClassPath.Split(new char[] { ',' });
            for (int i = intDepth; i < ArrClassPath.Length; i++)
            {
                ClassModel claModel = new ClassModel();
                claModel = claDAL.GetInfo(ArrClassPath[i]);
                if (claModel!=null)
                {
                    string strTempLinkUrl,strTempTarget;
                    if (claModel.LinkUrl != string.Empty)
                    {
                        strTempLinkUrl = claModel.LinkUrl;
                        strTempTarget = "target=\"" + claModel.Target + "\"";
                    }
                    else
                    {
                        strTempLinkUrl = claModel.ClassEnName + Config.FileExt;
                        strTempTarget = "";
                    }
                    strClassNav.Append("<a href=\"" + strTempLinkUrl + "\" " + strTempTarget + ">" + claModel.ClassName + "</a>");
                    if (i != ArrClassPath.Length - 1) strClassNav.Append(strNavStr);
                }
            }
            return strClassNav;
        }
        #endregion

        #region ������ĿӢ����ȡ��ĿID
        /// <summary>
        /// ������ĿӢ����ȡ��ĿID
        /// </summary>
        public string GetClassIDByClassEnName(string strConfigID, string strClassEnName)
        {
            return claDAL.GetClassIDByClassEnName(strConfigID,strClassEnName);
        }
        #endregion

        #region ������ĿIDȡ��ĿӢ����
        /// <summary>
        /// ������ĿIDȡ��ĿӢ����
        /// </summary>
        public string GetClassEnNameByClassID(string strClassID)
        {
            return claDAL.GetClassEnNameByClassID(strClassID);
        }
        #endregion

        #region ȡ�������ID
        /// <summary>
        /// ȡ�������ID
        /// </summary>
        public string GetTopClassID(string strClassID)
        {
            return claDAL.GetTopClassID(strClassID);
        }
        #endregion

        #region ȡ�ֶ�ֵ
        /// <summary>
        /// ȡ�ֶ�ֵ
        /// </summary>
        public string GetValueByField(string strFieldName, string strClassID)
        {
            return claDAL.GetValueByField(strFieldName, strClassID);
        }
        #endregion

        #region Ajaxȡ���
        /// <summary>
        /// Ajaxȡ���
        /// </summary>
        /// <param name="strParentID"></param>
        /// <param name="strType">select,checkbox,radio</param>
        /// <param name="strObjName"></param>
        /// <returns></returns>
        public StringBuilder AjaxGetClassList(string strParentID, string strType, string strObjName)
        {
            return claDAL.AjaxGetClassList(strParentID, strType, strObjName);
        }
        #endregion

        #region ��������ĿID��SQL
        /// <summary>
        /// ��������ĿID��SQL
        /// </summary>
        public string GetSubClassSql(string strClassID)
        {
            return claDAL.GetSubClassSql(strClassID);
        }
        #endregion


        #region ͬ����������Ŀ����
        /// <summary>
        /// ͬ����������Ŀ����
        /// </summary>
        public void UpdateSubClassConfig(string strClassID, string strClassConfig, string strClassPropertyID)
        {
            claDAL.UpdateSubClassConfig(strClassID, strClassConfig, strClassPropertyID);
        }
        #endregion


        #region ������Ŀ·��ȡClassID
        /// <summary>
        /// ������Ŀ·��ȡClassID
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public string GetClassIDByPath(string strClassPath, int index, string strClassID)
        {
            return claDAL.GetClassIDByPath(strClassPath, index, strClassID);
        }
        #endregion

    }
}
