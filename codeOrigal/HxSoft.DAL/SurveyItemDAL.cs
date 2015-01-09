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
    ///调查选顶 -数据访问类
    /// 创建人:杨小明
    /// 日期:2011-12-26
    /// </summary>
    public class SurveyItemDAL
    {
        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_SurveyItem where " + strFieldName + "=@" + strFieldName + "");
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

        public bool CheckInfo(string strFieldName, string strFieldValue, string strSurveyItemID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_SurveyItem where " + strFieldName + "=@" + strFieldName + " and SurveyItemID<>@SurveyItemID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@"+strFieldName,strFieldValue),
Config.Conn().CreateDbParameter("@SurveyItemID",strSurveyItemID)};
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

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strSurveyItemID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select " + strFieldName + " from t_SurveyItem where SurveyItemID=@SurveyItemID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@SurveyItemID",strSurveyItemID)};
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

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public SurveyItemModel GetInfo(string strSurveyItemID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_SurveyItem where SurveyItemID=@SurveyItemID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@SurveyItemID",strSurveyItemID)};
            SurveyItemModel surItModel = new SurveyItemModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    surItModel.ItemName = dr["ItemName"].ToString();
                    surItModel.TypeID = dr["TypeID"].ToString();
                    surItModel.SurveyID = dr["SurveyID"].ToString();
                    surItModel.ListID = dr["ListID"].ToString();
                    surItModel.AdminID = dr["AdminID"].ToString();
                    surItModel.AddTime = dr["AddTime"].ToString();
                    surItModel.IsClose = dr["IsClose"].ToString();
                    return surItModel;
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
        public void InsertInfo(SurveyItemModel surItModel)
        {
            StringBuilder sql = new StringBuilder("insert into");
            sql.Append(" t_SurveyItem(ItemName,TypeID,SurveyID,ListID,AdminID,AddTime,IsClose)");
            sql.Append(" values(@ItemName,@TypeID,@SurveyID,@ListID,@AdminID,@AddTime,@IsClose)");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ItemName",surItModel.ItemName),
Config.Conn().CreateDbParameter("@TypeID",surItModel.TypeID),
Config.Conn().CreateDbParameter("@SurveyID",surItModel.SurveyID),
Config.Conn().CreateDbParameter("@ListID",surItModel.ListID),
Config.Conn().CreateDbParameter("@AdminID",surItModel.AdminID),
Config.Conn().CreateDbParameter("@AddTime",surItModel.AddTime),
Config.Conn().CreateDbParameter("@IsClose",surItModel.IsClose)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(SurveyItemModel surItModel, string strSurveyItemID)
        {
            StringBuilder sql = new StringBuilder("update t_SurveyItem set ");
            sql.Append(" ItemName=@ItemName,");
            sql.Append(" TypeID=@TypeID,");
            //sql.Append(" SurveyID=@SurveyID,");
            sql.Append(" ListID=@ListID,");
           //sql.Append(" AdminID=@AdminID,");
            //sql.Append(" AddTime=@AddTime,");
            sql.Append(" IsClose=@IsClose");
            sql.Append(" where  SurveyItemID=@SurveyItemID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ItemName",surItModel.ItemName),
Config.Conn().CreateDbParameter("@TypeID",surItModel.TypeID),
//Config.Conn().CreateDbParameter("@SurveyID",surItModel.SurveyID),
Config.Conn().CreateDbParameter("@ListID",surItModel.ListID),
//Config.Conn().CreateDbParameter("@AdminID",surItModel.AdminID),
//Config.Conn().CreateDbParameter("@AddTime",surItModel.AddTime),
Config.Conn().CreateDbParameter("@IsClose",surItModel.IsClose),
Config.Conn().CreateDbParameter("@SurveyItemID",strSurveyItemID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strSurveyItemID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_SurveyItem where SurveyItemID=@SurveyItemID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@SurveyItemID",strSurveyItemID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strSurveyItemID, string strIsClose)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_SurveyItem set IsClose=@IsClose where SurveyItemID=@SurveyItemID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@IsClose",strIsClose),
            Config.Conn().CreateDbParameter("@SurveyItemID",strSurveyItemID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 读取排序号
        /// <summary>
        /// 读取排序号
        /// </summary>
        public string GetListID(string strSurveyID)
        {
            StringBuilder sql = new StringBuilder();
            if (Config.DatabaseType == Config.DatabaseTypeCollection.MySql.ToString())
            {
                sql.Append("select ListID from t_SurveyItem where SurveyID=@SurveyID order by ListID desc limit 0,1");
            }
            else
            {
                sql.Append("select top 1 ListID from t_SurveyItem where SurveyID=@SurveyID order by ListID desc");
            }
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@SurveyID",strSurveyID)};
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
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
        public void OrderInfo(string strSurveyID,string strListID, string strOldListID)
        {
            if (Convert.ToInt32(strListID) > Convert.ToInt32(strOldListID))
            {
                StringBuilder sql = new StringBuilder();
                if (Config.DatabaseType == Config.DatabaseTypeCollection.MySql.ToString())
                {
                    sql.Append("create table tmp as select SurveyItemID from t_SurveyItem where SurveyID=@SurveyID and ListID<=@ListID and ListID>@OldListID;");
                    sql.Append("update t_SurveyItem set ListID=ListID-1 where SurveyItemID in(select SurveyItemID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_SurveyItem set ListID=ListID-1 where SurveyItemID in(select SurveyItemID from t_SurveyItem where SurveyID=@SurveyID and ListID<=@ListID and ListID>@OldListID)");
                }
                DbParameter[] cmdParams = {
                Config.Conn().CreateDbParameter("@SurveyID",strSurveyID),
                Config.Conn().CreateDbParameter("@ListID",strListID),
                Config.Conn().CreateDbParameter("@OldListID",strOldListID)};
                Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
            }
            else if (Convert.ToInt32(strListID) < Convert.ToInt32(strOldListID))
            {
                StringBuilder sql = new StringBuilder();
                if (Config.DatabaseType == Config.DatabaseTypeCollection.MySql.ToString())
                {
                    sql.Append("create table tmp as select SurveyItemID from t_SurveyItem where SurveyID=@SurveyID and ListID>=@ListID and ListID<@OldListID;");
                    sql.Append("update t_SurveyItem set ListID=ListID+1 where SurveyItemID in(select SurveyItemID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_SurveyItem set ListID=ListID+1 where SurveyItemID in(select SurveyItemID from t_SurveyItem where SurveyID=@SurveyID and ListID>=@ListID and ListID<@OldListID)");
                }
                DbParameter[] cmdParams = {
                Config.Conn().CreateDbParameter("@SurveyID",strSurveyID),
                Config.Conn().CreateDbParameter("@ListID",strListID),
                Config.Conn().CreateDbParameter("@OldListID",strOldListID)};
                Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
            }
        }
        #endregion

        #region 取SurveyItemID
        /// <summary>
        /// 取SurveyItemID
        /// </summary>
        public string GetSurveyItemID()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select max(SurveyItemID) from t_SurveyItem");
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), null))
            {
                if (dr.Read())
                {
                    return dr[0].ToString();
                }
                else
                {
                    return "0";
                }
            }
        }
        #endregion

        #region 根据SurveyID删除信息
        /// <summary>
        /// 根据SurveyID删除信息
        /// </summary>
        public void DeleteInfoBySurveyID(string strSurveyID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_SurveyItem where SurveyID=@SurveyID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@SurveyID",strSurveyID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion
    }
}
