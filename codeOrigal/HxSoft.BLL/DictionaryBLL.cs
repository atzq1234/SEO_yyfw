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
    ///数据字典-业务逻辑类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>

    public class DictionaryBLL
    {

        private readonly DictionaryDAL dictDAL = new DictionaryDAL();

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue, string strParentID)
        {
            return dictDAL.CheckInfo(strFieldName, strFieldValue, strParentID);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strParentID, string strDictionaryID)
        {
            return dictDAL.CheckInfo(strFieldName, strFieldValue, strParentID, strDictionaryID);
        }
        #endregion

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public DictionaryModel GetInfo(string strDictionaryID)
        {
            return dictDAL.GetInfo(strDictionaryID);
        }
        /// <summary>
        /// 前台读取信息
        /// </summary>
        public DictionaryModel GetInfo2(string strDictionaryID)
        {
            return dictDAL.GetInfo2(strDictionaryID);
        }

        public IList<DictionaryModel> GetInfoListByParentID(string strParentID)
        {
            return dictDAL.GetInfoListByParentID(strParentID);
        }
        #endregion

        #region 从缓存读取信息
        /// <summary>
        /// 从缓存读取信息
        /// </summary>
        public DictionaryModel GetCacheInfo(string strDictionaryID)
        {
            string key = "Cache_Dictionary_Model_" + strDictionaryID;
            if (HttpRuntime.Cache[key] != null)
                return (DictionaryModel)HttpRuntime.Cache[key];
            else
            {
                DictionaryModel dictModel = dictDAL.GetInfo(strDictionaryID);
                CacheHelper.AddCache(key, dictModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return dictModel;
            }
        }
        #endregion

        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        public void InsertInfo(DictionaryModel dictModel)
        {
            dictDAL.InsertInfo(dictModel);
            //增加子级数
            dictDAL.AddChildNum(dictModel.ParentID);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(DictionaryModel dictModel, string strDictionaryID)
        {
            dictDAL.UpdateInfo(dictModel, strDictionaryID);
            string key = "Cache_Dictionary_Model_" + strDictionaryID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strDictionaryID)
        {
            //取ParentID
            DictionaryModel dictModel = GetInfo(strDictionaryID);
            if (dictModel != null)
            {
                //减少子级数
                dictDAL.CutChildNum(dictModel.ParentID);
            }
            string key = "Cache_Dictionary_Model_" + strDictionaryID;
            CacheHelper.RemoveCache(key);
            dictDAL.DeleteInfo(strDictionaryID);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strDictionaryID, string strIsClose)
        {
            dictDAL.UpdateCloseStatus(strDictionaryID, strIsClose);
        }
        #endregion

        #region 移动信息
        /// <summary>
        /// 移动信息
        /// </summary>
        public void MoveInfo(DictionaryModel dictModel, string strDictionaryID)
        {
            dictDAL.MoveInfo(dictModel, strDictionaryID);
        }
        #endregion

        #region 读取排序号
        /// <summary>
        /// 读取排序号
        /// </summary>
        public string GetListID(string strParentID)
        {
            return dictDAL.GetListID(strParentID);
        }
        #endregion

        #region 排序信息
        /// <summary>
        /// 排序信息
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strParentID, string strListID, string strOldListID)
        {
            dictDAL.OrderInfo(strParentID, strListID, strOldListID);
        }
        #endregion

        #region 显示下拉树形列表
        /// <summary>
        /// 显示下拉树形列表
        /// </summary>
        public void ShowSelectTree(string strParentID, DropDownList drp, string strSql)
        {
            dictDAL.ShowSelectTree(strParentID, 0, drp, strSql);
        }
        #endregion

        #region 移动时更新子级数
        /// <summary>
        /// 修改时更新子级数
        /// </summary>
        public void UpdateChildNum(string strParentID, string strOldParentID)
        {
            if (strParentID != strOldParentID)
            {
                dictDAL.AddChildNum(strParentID);
                dictDAL.CutChildNum(strOldParentID);
            }
        }
        #endregion

        #region 显示路径
        /// <summary>
        /// 显示路径
        /// </summary>
        public StringBuilder ShowPath(string strDictionaryID)
        {
            StringBuilder tempStr = new StringBuilder("根结点");
            DictionaryModel dictModel = new DictionaryModel();
            dictModel = dictDAL.GetInfo(strDictionaryID);
            if (dictModel == null)
            {
                return tempStr;
            }
            else
            {
                string strPath = dictDAL.GetPath(strDictionaryID).ToString();
                string[] arrPath = strPath.Split(new char[] { ',' });
                for (int i = 0; i < arrPath.Length; i++)
                {
                    DictionaryModel dictModel_2 = new DictionaryModel();
                    dictModel_2 = dictDAL.GetInfo(arrPath[i]);
                    if (dictModel_2 != null)
                    {
                        tempStr.Append(" > " + dictModel_2.DictionaryName);
                    }
                }
                return tempStr;
            }
        }
        #endregion

        #region 取第一个类别ID
        /// <summary>
        /// 取第一个类别ID
        /// </summary>
        public string GetFirstDictionaryID(string strParentID)
        {
            return dictDAL.GetFirstDictionaryID(strParentID);
        }
        #endregion

        #region 取子分类ID
        /// <summary>
        /// 取子分类ID
        /// </summary>
        public StringBuilder GetSubDictionaryID(string strParentID)
        {
            StringBuilder strTemp = new StringBuilder();
            strTemp.Append("-1,");
            strTemp.Append(dictDAL.GetSubDictionaryID(strParentID));
            strTemp.Append("-1");
            return strTemp;
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strDictionaryID)
        {
            return dictDAL.GetValueByField(strFieldName,strDictionaryID);
        }
        #endregion

        #region Ajax取类别
        /// <summary>
        /// Ajax取类别
        /// </summary>
        /// <param name="strParentID"></param>
        /// <param name="strType">select,checkbox,radio</param>
        /// <param name="strObjName"></param>
        /// <param name="intValType">0或1,0取ID,1取Val</param>
        /// <returns></returns>
        public StringBuilder AjaxGetDictionaryList(string strParentID, string strType, string strObjName, int intValType)
        {
            return dictDAL.AjaxGetDictionaryList(strParentID, strType, strObjName, intValType);
        }
        #endregion

        #region 返回子类别ID的SQL
        /// <summary>
        /// 返回子栏目ID的SQL
        /// </summary>
        public string GetSubDictionarySql(string strDictionaryID)
        {
            return GetSubDictionarySql(strDictionaryID);
        }
        #endregion

    }
}
