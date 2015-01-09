using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HxSoft.Model;
using HxSoft.Common;

namespace HxSoft.DAL
{
    /// <summary>
    ///管理员管理组管理-数据访问类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>
    public class AdminInGroupDAL
    {

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strAdminID,string strAdminGroupID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_AdminInGroup where AdminID=@AdminID and AdminGroupID=@AdminGroupID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AdminID",strAdminID),
            Config.Conn().CreateDbParameter("@AdminGroupID",strAdminGroupID)};
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.HasRows)
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

        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        public void InsertInfo(AdminInGroupModel admInGrModel)
        {
            StringBuilder sql = new StringBuilder("insert into");
            sql.Append(" t_AdminInGroup(AdminID,AdminGroupID)");
            sql.Append(" values(@AdminID,@AdminGroupID)");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AdminID",admInGrModel.AdminID),
            Config.Conn().CreateDbParameter("@AdminGroupID",admInGrModel.AdminGroupID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strAdminID, string strAdminGroupID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_AdminInGroup where AdminID=@AdminID and AdminGroupID=@AdminGroupID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AdminID",strAdminID),
            Config.Conn().CreateDbParameter("@AdminGroupID",strAdminGroupID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }

        /// <summary>
        /// 根据AdminID删除信息
        /// </summary>
        public void DeleteInfoByAdminID(string strAdminID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_AdminInGroup where AdminID=@AdminID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AdminID",strAdminID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }

        /// <summary>
        /// 根据AdminGroupID删除信息
        /// </summary>
        public void DeleteInfoByAdminGroupID(string strAdminGroupID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_AdminInGroup where AdminGroupID=@AdminGroupID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AdminGroupID",strAdminGroupID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 清除管理组中的管理员权限缓存
        /// <summary>
        /// 清除管理组中的管理员权限缓存
        /// </summary>
        /// <param name="strAdminGroupID"></param>
        /// <returns></returns>
        public void RemoveLimitCache(string strAdminGroupID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select AdminID from t_AdminInGroup where AdminGroupID=@AdminGroupID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AdminGroupID",strAdminGroupID)};
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                while (dr.Read())
                {
                    string key = "Cache_AdminGroup_LimitValues_" + dr[0].ToString();
                    CacheHelper.RemoveCache(key);
                }
            }
        }
        #endregion


    }
}
