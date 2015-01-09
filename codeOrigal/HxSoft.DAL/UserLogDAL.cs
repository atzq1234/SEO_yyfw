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
    /// 会员日志管理-数据访问类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>
    public class UserLogDAL
    {

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_UserLog where " + strFieldName + "=@" + strFieldName + "");
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

        public bool CheckInfo(string strFieldName, string strFieldValue, string strUserLogID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_UserLog where " + strFieldName + "=@" + strFieldName + " and UserLogID<>@UserLogID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@"+strFieldName,strFieldValue),
            Config.Conn().CreateDbParameter("@UserLogID",strUserLogID)};
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
        public UserLogModel GetInfo(string strUserLogID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_UserLog where UserLogID=@UserLogID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@UserLogID",strUserLogID)};
            UserLogModel userlogModel = new UserLogModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    userlogModel.LogContent = dr["LogContent"].ToString();
                    userlogModel.ScriptFile = dr["ScriptFile"].ToString();
                    userlogModel.IpAddress = dr["IpAddress"].ToString();
                    userlogModel.UserID = dr["UserID"].ToString();
                    userlogModel.AddTime = dr["AddTime"].ToString();
                    return userlogModel;
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
        public void InsertInfo(UserLogModel userlogModel)
        {
            StringBuilder sql = new StringBuilder("insert into");
            sql.Append(" t_UserLog(LogContent,ScriptFile,IpAddress,UserID,AddTime)");
            sql.Append(" values(@LogContent,@ScriptFile,@IpAddress,@UserID,@AddTime)");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@LogContent",userlogModel.LogContent),
            Config.Conn().CreateDbParameter("@ScriptFile",userlogModel.ScriptFile),
            Config.Conn().CreateDbParameter("@IpAddress",userlogModel.IpAddress),
            Config.Conn().CreateDbParameter("@UserID",userlogModel.UserID),
            Config.Conn().CreateDbParameter("@AddTime",userlogModel.AddTime)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(UserLogModel userlogModel, string strUserLogID)
        {
            StringBuilder sql = new StringBuilder("update t_UserLog set ");
            sql.Append(" LogContent=@LogContent,");
            sql.Append(" ScriptFile=@ScriptFile,");
            sql.Append(" IpAddress=@IpAddress,");
            sql.Append(" UserID=@UserID,");
            sql.Append(" AddTime=@AddTime");
            sql.Append(" where UserLogID=@UserLogID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@LogContent",userlogModel.LogContent),
            Config.Conn().CreateDbParameter("@ScriptFile",userlogModel.ScriptFile),
            Config.Conn().CreateDbParameter("@IpAddress",userlogModel.IpAddress),
            Config.Conn().CreateDbParameter("@AddTime",userlogModel.AddTime),
            Config.Conn().CreateDbParameter("@UserID",userlogModel.UserID),
            Config.Conn().CreateDbParameter("@UserLogID",strUserLogID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strUserLogID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_UserLog where UserLogID=@UserLogID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@UserLogID",strUserLogID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

    }
}
