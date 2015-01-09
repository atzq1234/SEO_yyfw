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
    ///文件管理-业务逻辑类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>

    public class PathBLL
    {

        private readonly PathDAL pathDAL = new PathDAL();

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public PathModel GetInfo(string strPath)
        {
            return pathDAL.GetInfo(strPath);
        }
        #endregion

        #region 从缓存读取信息
        /// <summary>
        /// 从缓存读取信息
        /// </summary>
        public PathModel GetCacheInfo(string strPath)
        {
            string key = "Cache_path_Model_" + strPath;
            if (HttpRuntime.Cache[key] != null)
                return (PathModel)HttpRuntime.Cache[key];
            else
            {
                PathModel pathModel = pathDAL.GetInfo(strPath);
                CacheHelper.AddCache(key, pathModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return pathModel;
            }
        }
        #endregion

        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        public void InsertInfo(PathModel pathModel)
        {
            pathDAL.InsertInfo(pathModel);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strPath)
        {
            pathDAL.DeleteInfo(strPath);
            string key = "Cache_path_Model_" + strPath;
            CacheHelper.RemoveCache(key);
        }
        #endregion
    }
}
