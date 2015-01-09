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
    ///系统配置管理-业务逻辑类
    /// 创建人:Admin
    /// 日期:2012-10-13
    /// </summary>

    public class SetBLL
    {

        private readonly SetDAL seDAL = new SetDAL();

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            return seDAL.CheckInfo(strFieldName, strFieldValue);
        }

        public bool CheckInfo(string strFieldName, string strFieldValue, string strSetID)
        {
            return seDAL.CheckInfo(strFieldName, strFieldValue, strSetID);
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strSetID)
        {
            return seDAL.GetValueByField(strFieldName, strSetID);
        }
        #endregion

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public SetModel GetInfo()
        {
            return seDAL.GetInfo();
        }
        #endregion

        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        public void InsertInfo(SetModel seModel)
        {
            seDAL.InsertInfo(seModel);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(SetModel seModel)
        {
            seDAL.UpdateInfo(seModel);
        }
        #endregion

    }
}
