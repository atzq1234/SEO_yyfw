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
    ///管理组管理-数据访问类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>
    public class AdminGroupDAL
    {

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_AdminGroup where " + strFieldName + "=@" + strFieldName + "");
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

        public bool CheckInfo(string strFieldName, string strFieldValue, string strAdminGroupID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_AdminGroup where " + strFieldName + "=@" + strFieldName + " and AdminGroupID<>@AdminGroupID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@"+strFieldName,strFieldValue),
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

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public AdminGroupModel GetInfo(string strAdminGroupID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_AdminGroup where AdminGroupID=@AdminGroupID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AdminGroupID",strAdminGroupID)};
            AdminGroupModel admGrModel = new AdminGroupModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    admGrModel.AdminGroupName = dr["AdminGroupName"].ToString();
                    admGrModel.LimitValues = dr["LimitValues"].ToString();
                    admGrModel.ListID = dr["ListID"].ToString();
                    admGrModel.AdminID = dr["AdminID"].ToString();
                    admGrModel.AddTime = dr["AddTime"].ToString();
                    admGrModel.IsClose = dr["IsClose"].ToString();
                    return admGrModel;
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
        public void InsertInfo(AdminGroupModel admGrModel)
        {
            StringBuilder sql = new StringBuilder("insert into");
            sql.Append(" t_AdminGroup(AdminGroupName,LimitValues,ListID,AdminID,AddTime,IsClose)");
            sql.Append(" values(@AdminGroupName,@LimitValues,@ListID,@AdminID,@AddTime,@IsClose)");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AdminGroupName",admGrModel.AdminGroupName),
            Config.Conn().CreateDbParameter("@LimitValues",admGrModel.LimitValues),
            Config.Conn().CreateDbParameter("@ListID",admGrModel.ListID),
            Config.Conn().CreateDbParameter("@AdminID",admGrModel.AdminID),
            Config.Conn().CreateDbParameter("@AddTime",admGrModel.AddTime),
            Config.Conn().CreateDbParameter("@IsClose",admGrModel.IsClose)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(AdminGroupModel admGrModel, string strAdminGroupID)
        {
            StringBuilder sql = new StringBuilder("update t_AdminGroup set ");
            sql.Append(" AdminGroupName=@AdminGroupName,");
            //sql.Append(" LimitValues=@LimitValues,");
            sql.Append(" ListID=@ListID,");
            //sql.Append(" AdminID=@AdminID,");
            //sql.Append(" AddTime=@AddTime,");
            sql.Append(" IsClose=@IsClose");
            sql.Append(" where AdminGroupID=@AdminGroupID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AdminGroupName",admGrModel.AdminGroupName),
            //Config.Conn().CreateDbParameter("@LimitValues",admGrModel.LimitValues),
            Config.Conn().CreateDbParameter("@ListID",admGrModel.ListID),
            //Config.Conn().CreateDbParameter("@AdminID",admGrModel.AdminID),
            //Config.Conn().CreateDbParameter("@AddTime",admGrModel.AddTime),
            Config.Conn().CreateDbParameter("@IsClose",admGrModel.IsClose),
            Config.Conn().CreateDbParameter("@AdminGroupID",strAdminGroupID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strAdminGroupID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_AdminGroup where AdminGroupID=@AdminGroupID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AdminGroupID",strAdminGroupID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strAdminGroupID, string strIsClose)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_AdminGroup set IsClose=@IsClose where AdminGroupID=@AdminGroupID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@IsClose",strIsClose),
            Config.Conn().CreateDbParameter("@AdminGroupID",strAdminGroupID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 设置操作权限
        /// <summary>
        /// 设置操作权限
        /// </summary>
        public void SetLimit(AdminGroupModel admGrModel, string strAdminGroupID)
        {
            StringBuilder sql = new StringBuilder("update t_AdminGroup set ");
            sql.Append(" LimitValues=@LimitValues");
            sql.Append(" where AdminGroupID=@AdminGroupID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@LimitValues",admGrModel.LimitValues),
            Config.Conn().CreateDbParameter("@AdminGroupID",strAdminGroupID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 读取排序号
        /// <summary>
        /// 读取排序号
        /// </summary>
        public string GetListID()
        {
            StringBuilder sql = new StringBuilder();
            if (Config.DatabaseType == Config.DatabaseTypeCollection.MySql.ToString())
            {
                sql.Append("select ListID from t_AdminGroup order by ListID desc limit 0,1");
            }
            else
            {
                sql.Append("select top 1 ListID from t_AdminGroup order by ListID desc");
            }
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), null))
            {
                if (dr.Read())
                {
                    string strListID = Convert.ToString((int)dr["ListID"] + 1);
                    return strListID;
                }
                else
                {
                    return "1";
                }
            }
        }
        #endregion

        #region 排序信息
        /// <summary>
        /// 排序信息
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strListID, string strOldListID)
        {
            if (Convert.ToInt32(strListID) > Convert.ToInt32(strOldListID))
            {
                StringBuilder sql = new StringBuilder();
                if (Config.DatabaseType == Config.DatabaseTypeCollection.MySql.ToString())
                {
                    sql.Append("create table tmp as select AdminGroupID from t_AdminGroup where ListID<=@ListID and ListID>@OldListID;");
                    sql.Append("update t_AdminGroup set ListID=ListID-1 where AdminGroupID in(select AdminGroupID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_AdminGroup set ListID=ListID-1 where AdminGroupID in(select AdminGroupID from t_AdminGroup where  ListID<=@ListID and ListID>@OldListID)");
                }
                DbParameter[] cmdParams = {
                Config.Conn().CreateDbParameter("@ListID",strListID),
                Config.Conn().CreateDbParameter("@OldListID",strOldListID)};
                Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
            }
            else if (Convert.ToInt32(strListID) < Convert.ToInt32(strOldListID))
            {
                StringBuilder sql = new StringBuilder();
                if (Config.DatabaseType == Config.DatabaseTypeCollection.MySql.ToString())
                {
                    sql.Append("create table tmp as select AdminGroupID from t_AdminGroup where ListID>=@ListID and ListID<@OldListID;");
                    sql.Append("update t_AdminGroup set ListID=ListID+1 where AdminGroupID in(select AdminGroupID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_AdminGroup set ListID=ListID+1 where AdminGroupID in(select AdminGroupID from t_AdminGroup where  ListID>=@ListID and ListID<@OldListID)");
                }
                DbParameter[] cmdParams = {
                Config.Conn().CreateDbParameter("@ListID",strListID),
                Config.Conn().CreateDbParameter("@OldListID",strOldListID)};
                Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
            }
        }
        #endregion

        #region 取权限字段值
        public StringBuilder GetLimitValues(string strAdminID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select LimitValues from t_AdminGroup where AdminGroupID in(select AdminGroupID from t_AdminInGroup where AdminID=@AdminID) and IsClose=0");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AdminID",strAdminID)};
            StringBuilder strTemp = new StringBuilder();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                while (dr.Read())
                {
                    strTemp.Append(dr["LimitValues"].ToString());
                }
            }
            return strTemp;
        }
        #endregion

        #region 取管理员组
        public StringBuilder GetAdminGroupNames(string strAdminID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select AdminGroupName from t_AdminGroup where AdminGroupID in(select AdminGroupID from t_AdminInGroup where AdminID=@AdminID) and IsClose=0");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AdminID",strAdminID)};
            StringBuilder strTemp = new StringBuilder();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                while (dr.Read())
                {
                    strTemp.Append(dr["AdminGroupName"].ToString()+" ");
                }
            }
            return strTemp;
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strAdminGroupID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select " + strFieldName + " from t_AdminGroup where AdminGroupID=@AdminGroupID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@AdminGroupID",strAdminGroupID)};
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    return dr[strFieldName].ToString();
                }
                else
                {
                    return "";
                }
            }
        }
        #endregion

    }
}
