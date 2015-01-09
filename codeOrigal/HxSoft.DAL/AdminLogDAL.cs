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
    /// 管理员日志管理-数据访问类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>
    public class AdminLogDAL
    {

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_AdminLog where " + strFieldName + "=@" + strFieldName + "");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@"+strFieldName,strFieldValue)};
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

        public bool CheckInfo(string strFieldName, string strFieldValue, string strAdminLogID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_AdminLog where " + strFieldName + "=@" + strFieldName + " and AdminLogID<>@AdminLogID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@"+strFieldName,strFieldValue),
            Config.Conn().CreateDbParameter("@AdminLogID",strAdminLogID)};
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

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public AdminLogModel GetInfo(string strAdminLogID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_AdminLog where AdminLogID=@AdminLogID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AdminLogID",strAdminLogID)};
            AdminLogModel admlogModel = new AdminLogModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    admlogModel.LogContent = dr["LogContent"].ToString();
                    admlogModel.ScriptFile = dr["ScriptFile"].ToString();
                    admlogModel.IpAddress = dr["IpAddress"].ToString();
                    admlogModel.AdminID = dr["AdminID"].ToString();
                    admlogModel.AddTime = dr["AddTime"].ToString();
                    return admlogModel;
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion

        #region 插入信息
        /// <summary>
        /// 插入信息
        /// </summary>
        public void InsertInfo(AdminLogModel admlogModel)
        {
            StringBuilder sql = new StringBuilder("insert into");
            sql.Append(" t_AdminLog(LogContent,ScriptFile,IpAddress,AdminID,AddTime)");
            sql.Append(" values(@LogContent,@ScriptFile,@IpAddress,@AdminID,@AddTime)");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@LogContent",admlogModel.LogContent),
            Config.Conn().CreateDbParameter("@ScriptFile",admlogModel.ScriptFile),
            Config.Conn().CreateDbParameter("@IpAddress",admlogModel.IpAddress),
            Config.Conn().CreateDbParameter("@AdminID",admlogModel.AdminID),
            Config.Conn().CreateDbParameter("@AddTime",admlogModel.AddTime)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(AdminLogModel admlogModel, string strAdminLogID)
        {
            StringBuilder sql = new StringBuilder("update t_AdminLog set ");
            sql.Append(" LogContent=@LogContent,");
            sql.Append(" ScriptFile=@ScriptFile,");
            sql.Append(" IpAddress=@IpAddress,");
            sql.Append(" AdminID=@AdminID,");
            sql.Append(" AddTime=@AddTime");
            sql.Append(" where AdminLogID=@AdminLogID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@LogContent",admlogModel.LogContent),
            Config.Conn().CreateDbParameter("@ScriptFile",admlogModel.ScriptFile),
            Config.Conn().CreateDbParameter("@IpAddress",admlogModel.IpAddress),
            Config.Conn().CreateDbParameter("@AddTime",admlogModel.AddTime),
            Config.Conn().CreateDbParameter("@AdminID",admlogModel.AdminID),
            Config.Conn().CreateDbParameter("@AdminLogID",strAdminLogID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strAdminLogID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_AdminLog where AdminLogID=@AdminLogID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AdminLogID",strAdminLogID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

    }
}
