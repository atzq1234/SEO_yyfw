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
    ///文章图片-数据访问类
    /// 创建人:杨小明
    /// 日期:2012-1-19
    /// </summary>
    public class ArticlePicDAL
    {
        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_ArticlePic where " + strFieldName + "=@" + strFieldName + "");
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

        public bool CheckInfo(string strFieldName, string strFieldValue, string strArticlePicID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_ArticlePic where " + strFieldName + "=@" + strFieldName + " and ArticlePicID<>@ArticlePicID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@"+strFieldName,strFieldValue),
Config.Conn().CreateDbParameter("@ArticlePicID",strArticlePicID)};
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
        public string GetValueByField(string strFieldName, string strArticlePicID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select " + strFieldName + " from t_ArticlePic where ArticlePicID=@ArticlePicID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ArticlePicID",strArticlePicID)};
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
        public ArticlePicModel GetInfo(string strArticlePicID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_ArticlePic where ArticlePicID=@ArticlePicID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ArticlePicID",strArticlePicID)};
            ArticlePicModel artPicModel = new ArticlePicModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    artPicModel.ArticleID = dr["ArticleID"].ToString();
                    artPicModel.Title = dr["Title"].ToString();
                    artPicModel.SmallPic = dr["SmallPic"].ToString();
                    artPicModel.BigPic = dr["BigPic"].ToString();
                    artPicModel.Description = dr["Description"].ToString();
                    artPicModel.ListID = dr["ListID"].ToString();
                    artPicModel.AdminID = dr["AdminID"].ToString();
                    artPicModel.AddTime = dr["AddTime"].ToString();
                    artPicModel.IsClose = dr["IsClose"].ToString();
                    return artPicModel;
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
        public void InsertInfo(ArticlePicModel artPicModel)
        {
            StringBuilder sql = new StringBuilder("insert into");
            sql.Append(" t_ArticlePic(ArticleID,Title,SmallPic,BigPic,Description,ListID,AdminID,AddTime,IsClose)");
            sql.Append(" values(@ArticleID,@Title,@SmallPic,@BigPic,@Description,@ListID,@AdminID,@AddTime,@IsClose)");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ArticleID",artPicModel.ArticleID),
Config.Conn().CreateDbParameter("@Title",artPicModel.Title),
Config.Conn().CreateDbParameter("@SmallPic",artPicModel.SmallPic),
Config.Conn().CreateDbParameter("@BigPic",artPicModel.BigPic),
Config.Conn().CreateDbParameter("@Description",artPicModel.Description),
Config.Conn().CreateDbParameter("@ListID",artPicModel.ListID),
Config.Conn().CreateDbParameter("@AdminID",artPicModel.AdminID),
Config.Conn().CreateDbParameter("@AddTime",artPicModel.AddTime),
Config.Conn().CreateDbParameter("@IsClose",artPicModel.IsClose)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(ArticlePicModel artPicModel, string strArticlePicID)
        {
            StringBuilder sql = new StringBuilder("update t_ArticlePic set ");
            sql.Append(" ArticleID=@ArticleID,");
            sql.Append(" Title=@Title,");
            sql.Append(" SmallPic=@SmallPic,");
            sql.Append(" BigPic=@BigPic,");
            sql.Append(" Description=@Description,");
            sql.Append(" ListID=@ListID,");
            //sql.Append(" AdminID=@AdminID,");
            //sql.Append(" AddTime=@AddTime,");
            sql.Append(" IsClose=@IsClose");
            sql.Append(" where  ArticlePicID=@ArticlePicID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ArticleID",artPicModel.ArticleID),
Config.Conn().CreateDbParameter("@Title",artPicModel.Title),
Config.Conn().CreateDbParameter("@SmallPic",artPicModel.SmallPic),
Config.Conn().CreateDbParameter("@BigPic",artPicModel.BigPic),
Config.Conn().CreateDbParameter("@Description",artPicModel.Description),
Config.Conn().CreateDbParameter("@ListID",artPicModel.ListID),
//Config.Conn().CreateDbParameter("@AdminID",artPicModel.AdminID),
//Config.Conn().CreateDbParameter("@AddTime",artPicModel.AddTime),
Config.Conn().CreateDbParameter("@IsClose",artPicModel.IsClose),
Config.Conn().CreateDbParameter("@ArticlePicID",strArticlePicID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strArticlePicID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_ArticlePic where ArticlePicID=@ArticlePicID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ArticlePicID",strArticlePicID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strArticlePicID, string strIsClose)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_ArticlePic set IsClose=@IsClose where ArticlePicID=@ArticlePicID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@IsClose",strIsClose),
Config.Conn().CreateDbParameter("@ArticlePicID",strArticlePicID)};
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
                sql.Append("select ListID from t_ArticlePic order by ListID desc limit 0,1");
            }
            else
            {
                sql.Append("select top 1 ListID from t_ArticlePic order by ListID desc");
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
                    sql.Append("create table tmp as select ArticlePicID from t_ArticlePic where ListID<=@ListID and ListID>@OldListID;");
                    sql.Append("update t_ArticlePic set ListID=ListID-1 where ArticlePicID in(select ArticlePicID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_ArticlePic set ListID=ListID-1 where ArticlePicID in(select ArticlePicID from t_ArticlePic where  ListID<=@ListID and ListID>@OldListID)");
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
                    sql.Append("create table tmp as select ArticlePicID from t_ArticlePic where ListID>=@ListID and ListID<@OldListID;");
                    sql.Append("update t_ArticlePic set ListID=ListID+1 where ArticlePicID in(select ArticlePicID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_ArticlePic set ListID=ListID+1 where ArticlePicID in(select ArticlePicID from t_ArticlePic where  ListID>=@ListID and ListID<@OldListID)");
                }
                DbParameter[] cmdParams = {
                Config.Conn().CreateDbParameter("@ListID",strListID),
                Config.Conn().CreateDbParameter("@OldListID",strOldListID)};
                Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
            }
        }
        #endregion

        #region 根据ArticleID删除信息
        /// <summary>
        /// 根据ArticleID删除信息
        /// </summary>
        public void DeleteInfoByArticleID(string strArticleID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_ArticlePic where ArticleID=@ArticleID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ArticleID",strArticleID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion
    }
}
