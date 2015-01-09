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
    ///友情链接-业务逻辑类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>

    public class LinkBLL
    {

        private readonly LinkDAL linkDAL = new LinkDAL();

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return linkDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strLinkID)
        {
            return linkDAL.CheckInfo(strFieldName, strFieldValue, strLinkID);
        }
        #endregion

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public LinkModel GetInfo(string strLinkID)
        {
            return linkDAL.GetInfo(strLinkID);
        }
        #endregion

        #region 从缓存读取信息
        /// <summary>
        /// 从缓存读取信息
        /// </summary>
        public LinkModel GetCacheInfo(string strLinkID)
        {
            string key = "Cache_Link_Model_" + strLinkID;
            if (HttpRuntime.Cache[key] != null)
                return (LinkModel)HttpRuntime.Cache[key];
            else
            {
                LinkModel linkModel = linkDAL.GetInfo(strLinkID);
                CacheHelper.AddCache(key, linkModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return linkModel;
            }
        }
        #endregion

        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        public void InsertInfo(LinkModel linkModel)
        {
            linkDAL.InsertInfo(linkModel);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(LinkModel linkModel, string strLinkID)
        {
            linkDAL.UpdateInfo(linkModel, strLinkID);
            string key = "Cache_Link_Model_" + strLinkID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strLinkID)
        {
            linkDAL.DeleteInfo(strLinkID);
            string key = "Cache_Link_Model_" + strLinkID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strLinkID, string strIsClose)
        {
            linkDAL.UpdateCloseStatus(strLinkID, strIsClose);
        }
        #endregion

        #region 转移信息
        /// <summary>
        /// 转移信息
        /// </summary>
        public void TransferInfo(string strLinkID, string strConfigID)
        {
            linkDAL.TransferInfo(strLinkID, strConfigID);
        }
        #endregion

        #region 读取排序号
        /// <summary>
        /// 读取排序号
        /// </summary>
        public string GetListID()
        {
            return linkDAL.GetListID();
        }
        #endregion

        #region 排序信息
        /// <summary>
        /// 排序信息
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strListID, string strOldListID)
        {
            linkDAL.OrderInfo(strListID, strOldListID);
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strLinkID)
        {
            return linkDAL.GetValueByField(strFieldName, strLinkID);
        }
        #endregion


    }
}
