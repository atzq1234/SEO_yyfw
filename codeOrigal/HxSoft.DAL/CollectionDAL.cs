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
    ///产品收藏-数据访问类
    /// 创建人:杨小明
    /// 日期:2010-8-15
    /// </summary>
    public class CollectionDAL
    {

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Collection where " + strFieldName + "=@" + strFieldName + "");
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

        public bool CheckInfo(string strFieldName, string strFieldValue, string strCollectionID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Collection where " + strFieldName + "=@" + strFieldName + " and CollectionID<>@CollectionID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@"+strFieldName,strFieldValue),
            Config.Conn().CreateDbParameter("@CollectionID",strCollectionID)};
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
        public CollectionModel GetInfo(string strCollectionID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Collection where CollectionID=@CollectionID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@CollectionID",strCollectionID)};
            CollectionModel colModel = new CollectionModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    colModel.Title = dr["Title"].ToString();
                    colModel.Url = dr["Url"].ToString();
                    colModel.UserID = dr["UserID"].ToString();
                    colModel.AddTime = dr["AddTime"].ToString();
                    return colModel;
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
        public void InsertInfo(CollectionModel colModel)
        {
            StringBuilder sql = new StringBuilder("insert into");
            sql.Append(" t_Collection(Title,Url,UserID,AddTime)");
            sql.Append(" values(@Title,@Url,@UserID,@AddTime)");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@Title",colModel.Title),
Config.Conn().CreateDbParameter("@Url",colModel.Url),
Config.Conn().CreateDbParameter("@UserID",colModel.UserID),
Config.Conn().CreateDbParameter("@AddTime",colModel.AddTime)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(CollectionModel colModel, string strCollectionID)
        {
            StringBuilder sql = new StringBuilder("update t_Collection set ");
            sql.Append(" Title=@Title,");
            sql.Append(" Url=@Url,");
            sql.Append(" UserID=@UserID,");
            sql.Append(" AddTime=@AddTime");
            sql.Append(" where  CollectionID=@CollectionID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@Title",colModel.Title),
Config.Conn().CreateDbParameter("@Url",colModel.Url),
Config.Conn().CreateDbParameter("@UserID",colModel.UserID),
Config.Conn().CreateDbParameter("@AddTime",colModel.AddTime),
Config.Conn().CreateDbParameter("@CollectionID",strCollectionID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strCollectionID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_Collection where CollectionID=@CollectionID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@CollectionID",strCollectionID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 是否收藏
        /// <summary>
        /// 是否收藏
        /// </summary>
        public bool IsCollect(string strUrl, string strUserID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Collection where Url=@Url and UserID=@UserID");
            DbParameter[] cmdParams = { 
                Config.Conn().CreateDbParameter("@Url", strUrl), 
                Config.Conn().CreateDbParameter("@UserID", strUserID)};
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                return dr.HasRows;
            }
        }
        #endregion
    }
}
