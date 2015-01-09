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
    ///栏目模板-业务逻辑类
    /// 创建人:杨小明
    /// 日期:2012-4-16
    /// </summary>

    public class ClassTemplateBLL
    {

        private readonly ClassTemplateDAL claTempDAL = new ClassTemplateDAL();

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return claTempDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strClassTemplateID)
        {
            return claTempDAL.CheckInfo(strFieldName, strFieldValue, strClassTemplateID);
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strClassTemplateID)
        {
            return claTempDAL.GetValueByField(strFieldName, strClassTemplateID);
        }
        #endregion

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public ClassTemplateModel GetInfo(string strClassTemplateID)
        {
            return claTempDAL.GetInfo(strClassTemplateID);
        }
        #endregion

        #region 从缓存读取信息
        /// <summary>
        /// 从缓存读取信息
        /// </summary>
        public ClassTemplateModel GetCacheInfo(string strClassTemplateID)
        {
            string key = "Cache_ClassTemplate_Model_" + strClassTemplateID;
            if (HttpRuntime.Cache[key] != null)
                return (ClassTemplateModel)HttpRuntime.Cache[key];
            else
            {
                ClassTemplateModel claTempModel = claTempDAL.GetInfo(strClassTemplateID);
                CacheHelper.AddCache(key, claTempModel, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                return claTempModel;
            }
        }
        #endregion

        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        public void InsertInfo(ClassTemplateModel claTempModel)
        {
            claTempDAL.InsertInfo(claTempModel);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(ClassTemplateModel claTempModel, string strClassTemplateID)
        {
            claTempDAL.UpdateInfo(claTempModel, strClassTemplateID);
            string key = "Cache_ClassTemplate_Model_" + strClassTemplateID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strClassTemplateID)
        {
            claTempDAL.DeleteInfo(strClassTemplateID);
            string key = "Cache_ClassTemplate_Model_" + strClassTemplateID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strClassTemplateID, string strIsClose)
        {
            claTempDAL.UpdateCloseStatus(strClassTemplateID, strIsClose);
            string key = "Cache_ClassTemplate_Model_" + strClassTemplateID;
            CacheHelper.RemoveCache(key);
        }
        #endregion

        #region 读取排序号
        /// <summary>
        /// 读取排序号
        /// </summary>
        public string GetListID()
        {
            return claTempDAL.GetListID();
        }
        #endregion

        #region 排序信息
        /// <summary>
        /// 排序信息
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strListID, string strOldListID)
        {
            claTempDAL.OrderInfo(strListID, strOldListID);
        }
        #endregion
    }
}
