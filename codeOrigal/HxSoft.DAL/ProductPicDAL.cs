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
    ///产品图片-数据访问类
    /// 创建人:杨小明
    /// 日期:2012-1-19
    /// </summary>
    public class ProductPicDAL
    {
        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_ProductPic where " + strFieldName + "=@" + strFieldName + "");
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

        public bool CheckInfo(string strFieldName, string strFieldValue, string strProductPicID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_ProductPic where " + strFieldName + "=@" + strFieldName + " and ProductPicID<>@ProductPicID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@"+strFieldName,strFieldValue),
Config.Conn().CreateDbParameter("@ProductPicID",strProductPicID)};
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
        public string GetValueByField(string strFieldName, string strProductPicID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select " + strFieldName + " from t_ProductPic where ProductPicID=@ProductPicID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ProductPicID",strProductPicID)};
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
        public ProductPicModel GetInfo(string strProductPicID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_ProductPic where ProductPicID=@ProductPicID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ProductPicID",strProductPicID)};
            ProductPicModel proPicModel = new ProductPicModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    proPicModel.ProductID = dr["ProductID"].ToString();
                    proPicModel.Title = dr["Title"].ToString();
                    proPicModel.SmallPic = dr["SmallPic"].ToString();
                    proPicModel.BigPic = dr["BigPic"].ToString();
                    proPicModel.Description = dr["Description"].ToString();
                    proPicModel.ListID = dr["ListID"].ToString();
                    proPicModel.AdminID = dr["AdminID"].ToString();
                    proPicModel.AddTime = dr["AddTime"].ToString();
                    proPicModel.IsClose = dr["IsClose"].ToString();
                    return proPicModel;
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
        public void InsertInfo(ProductPicModel proPicModel)
        {
            StringBuilder sql = new StringBuilder("insert into");
            sql.Append(" t_ProductPic(ProductID,Title,SmallPic,BigPic,Description,ListID,AdminID,AddTime,IsClose)");
            sql.Append(" values(@ProductID,@Title,@SmallPic,@BigPic,@Description,@ListID,@AdminID,@AddTime,@IsClose)");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ProductID",proPicModel.ProductID),
Config.Conn().CreateDbParameter("@Title",proPicModel.Title),
Config.Conn().CreateDbParameter("@SmallPic",proPicModel.SmallPic),
Config.Conn().CreateDbParameter("@BigPic",proPicModel.BigPic),
Config.Conn().CreateDbParameter("@Description",proPicModel.Description),
Config.Conn().CreateDbParameter("@ListID",proPicModel.ListID),
Config.Conn().CreateDbParameter("@AdminID",proPicModel.AdminID),
Config.Conn().CreateDbParameter("@AddTime",proPicModel.AddTime),
Config.Conn().CreateDbParameter("@IsClose",proPicModel.IsClose)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(ProductPicModel proPicModel, string strProductPicID)
        {
            StringBuilder sql = new StringBuilder("update t_ProductPic set ");
            sql.Append(" ProductID=@ProductID,");
            sql.Append(" Title=@Title,");
            sql.Append(" SmallPic=@SmallPic,");
            sql.Append(" BigPic=@BigPic,");
            sql.Append(" Description=@Description,");
            sql.Append(" ListID=@ListID,");
            //sql.Append(" AdminID=@AdminID,");
            //sql.Append(" AddTime=@AddTime,");
            sql.Append(" IsClose=@IsClose");
            sql.Append(" where  ProductPicID=@ProductPicID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ProductID",proPicModel.ProductID),
Config.Conn().CreateDbParameter("@Title",proPicModel.Title),
Config.Conn().CreateDbParameter("@SmallPic",proPicModel.SmallPic),
Config.Conn().CreateDbParameter("@BigPic",proPicModel.BigPic),
Config.Conn().CreateDbParameter("@Description",proPicModel.Description),
Config.Conn().CreateDbParameter("@ListID",proPicModel.ListID),
//Config.Conn().CreateDbParameter("@AdminID",proPicModel.AdminID),
//Config.Conn().CreateDbParameter("@AddTime",proPicModel.AddTime),
Config.Conn().CreateDbParameter("@IsClose",proPicModel.IsClose),
Config.Conn().CreateDbParameter("@ProductPicID",strProductPicID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strProductPicID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_ProductPic where ProductPicID=@ProductPicID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ProductPicID",strProductPicID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strProductPicID, string strIsClose)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_ProductPic set IsClose=@IsClose where ProductPicID=@ProductPicID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@IsClose",strIsClose),
Config.Conn().CreateDbParameter("@ProductPicID",strProductPicID)};
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
                sql.Append("select ListID from t_ProductPic order by ListID desc limit 0,1");
            }
            else
            {
                sql.Append("select top 1 ListID from t_ProductPic order by ListID desc");
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
                    sql.Append("create table tmp as select ProductPicID from t_ProductPic where ListID<=@ListID and ListID>@OldListID;");
                    sql.Append("update t_ProductPic set ListID=ListID-1 where ProductPicID in(select ProductPicID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_ProductPic set ListID=ListID-1 where ProductPicID in(select ProductPicID from t_ProductPic where  ListID<=@ListID and ListID>@OldListID)");
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
                    sql.Append("create table tmp as select ProductPicID from t_ProductPic where ListID>=@ListID and ListID<@OldListID;");
                    sql.Append("update t_ProductPic set ListID=ListID+1 where ProductPicID in(select ProductPicID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_ProductPic set ListID=ListID+1 where ProductPicID in(select ProductPicID from t_ProductPic where  ListID>=@ListID and ListID<@OldListID)");
                }
                DbParameter[] cmdParams = {
                Config.Conn().CreateDbParameter("@ListID",strListID),
                Config.Conn().CreateDbParameter("@OldListID",strOldListID)};
                Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
            }
        }
        #endregion

        #region 根据ProductID删除信息
        /// <summary>
        /// 根据ProductID删除信息
        /// </summary>
        public void DeleteInfoByProductID(string strProductID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_ProductPic where ProductID=@ProductID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ProductID",strProductID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

    }
}
