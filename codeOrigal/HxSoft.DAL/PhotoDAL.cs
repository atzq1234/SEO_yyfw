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
    ///相册管理-数据访问类
    /// 创建人:
    /// 日期:2012-9-20
    /// </summary>
    public class PhotoDAL
    {
        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Photo where " + strFieldName + "=@" + strFieldName + "");
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

        public bool CheckInfo(string strFieldName, string strFieldValue, string strPhotoID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Photo where " + strFieldName + "=@" + strFieldName + " and PhotoID<>@PhotoID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@"+strFieldName,strFieldValue),
Config.Conn().CreateDbParameter("@PhotoID",strPhotoID)};
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
        public string GetValueByField(string strFieldName, string strPhotoID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select " + strFieldName + " from t_Photo where PhotoID=@PhotoID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@PhotoID",strPhotoID)};
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
        public PhotoModel GetInfo(string strPhotoID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Photo where PhotoID=@PhotoID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@PhotoID",strPhotoID)};
            PhotoModel phoModel = new PhotoModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    phoModel.ClassID = dr["ClassID"].ToString();
                    phoModel.Title = dr["Title"].ToString();
                    phoModel.SmallPic = dr["SmallPic"].ToString();
                    phoModel.BigPic = dr["BigPic"].ToString();
                    phoModel.Description = dr["Description"].ToString();
                    phoModel.ListID = dr["ListID"].ToString();
                    phoModel.AdminID = dr["AdminID"].ToString();
                    phoModel.AddTime = dr["AddTime"].ToString();
                    phoModel.IsClose = dr["IsClose"].ToString();
                    return phoModel;
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
        public void InsertInfo(PhotoModel phoModel)
        {
            StringBuilder sql = new StringBuilder("insert into");
            sql.Append(" t_Photo(ClassID,Title,SmallPic,BigPic,Description,ListID,AdminID,AddTime,IsClose)");
            sql.Append(" values(@ClassID,@Title,@SmallPic,@BigPic,@Description,@ListID,@AdminID,@AddTime,@IsClose)");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ClassID",phoModel.ClassID),
Config.Conn().CreateDbParameter("@Title",phoModel.Title),
Config.Conn().CreateDbParameter("@SmallPic",phoModel.SmallPic),
Config.Conn().CreateDbParameter("@BigPic",phoModel.BigPic),
Config.Conn().CreateDbParameter("@Description",phoModel.Description),
Config.Conn().CreateDbParameter("@ListID",phoModel.ListID),
Config.Conn().CreateDbParameter("@AdminID",phoModel.AdminID),
Config.Conn().CreateDbParameter("@AddTime",phoModel.AddTime),
Config.Conn().CreateDbParameter("@IsClose",phoModel.IsClose)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(PhotoModel phoModel, string strPhotoID)
        {
            StringBuilder sql = new StringBuilder("update t_Photo set ");
            sql.Append(" ClassID=@ClassID,");
            sql.Append(" Title=@Title,");
            sql.Append(" SmallPic=@SmallPic,");
            sql.Append(" BigPic=@BigPic,");
            sql.Append(" Description=@Description,");
            sql.Append(" ListID=@ListID,");
            //sql.Append(" AdminID=@AdminID,");
            //sql.Append(" AddTime=@AddTime,");
            sql.Append(" IsClose=@IsClose");
            sql.Append(" where  PhotoID=@PhotoID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ClassID",phoModel.ClassID),
Config.Conn().CreateDbParameter("@Title",phoModel.Title),
Config.Conn().CreateDbParameter("@SmallPic",phoModel.SmallPic),
Config.Conn().CreateDbParameter("@BigPic",phoModel.BigPic),
Config.Conn().CreateDbParameter("@Description",phoModel.Description),
Config.Conn().CreateDbParameter("@ListID",phoModel.ListID),
//Config.Conn().CreateDbParameter("@AdminID",phoModel.AdminID),
//Config.Conn().CreateDbParameter("@AddTime",phoModel.AddTime),
Config.Conn().CreateDbParameter("@IsClose",phoModel.IsClose),
Config.Conn().CreateDbParameter("@PhotoID",strPhotoID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strPhotoID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_Photo where PhotoID=@PhotoID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@PhotoID",strPhotoID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strPhotoID, string strIsClose)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_Photo set IsClose=@IsClose where PhotoID=@PhotoID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@IsClose",strIsClose),
Config.Conn().CreateDbParameter("@PhotoID",strPhotoID)};
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
                sql.Append("select ListID from t_Photo order by ListID desc limit 0,1");
            }
            else
            {
                sql.Append("select top 1 ListID from t_Photo order by ListID desc");
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
                    sql.Append("create table tmp as select PhotoID from t_Photo where ListID<=@ListID and ListID>@OldListID;");
                    sql.Append("update t_Photo set ListID=ListID-1 where PhotoID in(select PhotoID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_Photo set ListID=ListID-1 where PhotoID in(select PhotoID from t_Photo where  ListID<=@ListID and ListID>@OldListID)");
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
                    sql.Append("create table tmp as select PhotoID from t_Photo where ListID>=@ListID and ListID<@OldListID;");
                    sql.Append("update t_Photo set ListID=ListID+1 where PhotoID in(select PhotoID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_Photo set ListID=ListID+1 where PhotoID in(select PhotoID from t_Photo where  ListID>=@ListID and ListID<@OldListID)");
                }
                DbParameter[] cmdParams = {
                Config.Conn().CreateDbParameter("@ListID",strListID),
                Config.Conn().CreateDbParameter("@OldListID",strOldListID)};
                Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
            }
        }
        #endregion
    }
}
