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
    ///权限字段-业务逻辑类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>

    public class LimitBLL
    {

        private readonly LimitDAL limDAL = new LimitDAL();

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return limDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strLimitID)
        {
            return limDAL.CheckInfo(strFieldName, strFieldValue, strLimitID);
        }
        #endregion

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public LimitModel GetInfo(string strLimitID)
        {
            return limDAL.GetInfo(strLimitID);
        }
        /// <summary>
        /// 根据权限值读取信息
        /// </summary>
        public LimitModel GetInfoByLimitValue(string strLimitValue)
        {
            return limDAL.GetInfoByLimitValue(strLimitValue);
        }
        #endregion

        #region 从缓存读取信息
        /// <summary>
        /// 从缓存读取信息
        /// </summary>
        public LimitModel GetCacheInfo(string strLimitID)
        {
            string key = "Cache_Limit_Model_" + strLimitID;
            if (HttpRuntime.Cache[key] != null)
                return (LimitModel)HttpRuntime.Cache[key];
            else
            {
                LimitModel limModel = limDAL.GetInfo(strLimitID);
                CacheHelper.AddCache(key, limModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return limModel;
            }
        }
        #endregion

        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        public void InsertInfo(LimitModel limModel)
        {
            limDAL.InsertInfo(limModel);
            //增加子级数
            limDAL.AddChildNum(limModel.ParentID);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strLimitID)
        {
            //取ParentID
            LimitModel limModel = GetInfo(strLimitID);
            if (limModel != null)
            {
                //减少子级数
                limDAL.CutChildNum(limModel.ParentID);
            }
            string key = "Cache_Limit_Model_" + strLimitID;
            CacheHelper.RemoveCache(key);
            limDAL.DeleteInfo(strLimitID);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strLimitID, string strIsClose)
        {
            limDAL.UpdateCloseStatus(strLimitID, strIsClose);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(LimitModel limModel, string strLimitID)
        {
            limDAL.UpdateInfo(limModel, strLimitID);
            string key = "Cache_Limit_Model_" + strLimitID;
            CacheHelper.RemoveCache(key);
        }
         #endregion

        #region 移动信息
        /// <summary>
        /// 移动信息
        /// </summary>
        public void MoveInfo(LimitModel limModel, string strLimitID)
        {
            limDAL.MoveInfo(limModel, strLimitID);
        }
        #endregion

        #region 读取排序号
        /// <summary>
        /// 读取排序号
        /// </summary>
        public string GetListID(string strParentID)
        {
            return limDAL.GetListID(strParentID);
        }
        #endregion

        #region 排序信息
        /// <summary>
        /// 排序信息
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strParentID, string strListID, string strOldListID)
        {
            limDAL.OrderInfo(strParentID,strListID,strOldListID);
        }
        #endregion

        #region 显示下拉树形列表
        /// <summary>
        /// 显示下拉树形列表
        /// </summary>
        public void ShowSelectTree(string strParentID, DropDownList drp, string strSql)
        {
            limDAL.ShowSelectTree(strParentID, 0, drp, strSql);
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
                limDAL.AddChildNum(strParentID);
                limDAL.CutChildNum(strOldParentID);
            }
        }
        #endregion

        #region 显示路径
        /// <summary>
        /// 显示路径
        /// </summary>
        public StringBuilder ShowPath(string strLimitID)
        {
            StringBuilder tempStr = new StringBuilder("根结点");
            LimitModel limModel = new LimitModel();
            limModel = limDAL.GetInfo(strLimitID);
            if (limModel == null)
            {
                return tempStr;
            }
            else
            {
                string strPath = limDAL.GetPath(strLimitID).ToString();
                string[] arrPath = strPath.Split(new char[] { ',' });
                for (int i = 0; i < arrPath.Length; i++)
                {
                    LimitModel limModel_2 = new LimitModel();
                    limModel_2 = limDAL.GetInfo(arrPath[i]);
                    if (limModel_2 != null)
                    {
                        tempStr.Append(" > " + limModel_2.LimitField);
                    }
                }
                return tempStr;
            }
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strLimitID)
        {
            return limDAL.GetValueByField(strFieldName, strLimitID);
        }
        #endregion

        #region 根据权限值取字段值
        /// <summary>
        /// 根据权限值取字段值
        /// </summary>
        public string GetValueByLimitValue(string strFieldName, string strLimitValue)
        {
            return limDAL.GetValueByLimitValue(strFieldName, strLimitValue);
        }
        #endregion

    }
}
