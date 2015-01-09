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
    ///友情链接-数据访问类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>
    public class LinkDAL
    {

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Link where " + strFieldName + "=@" + strFieldName + "");
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

        public bool CheckInfo(string strFieldName, string strFieldValue, string strLinkID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Link where " + strFieldName + "=@" + strFieldName + " and LinkID<>@LinkID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@"+strFieldName,strFieldValue),
Config.Conn().CreateDbParameter("@LinkID",strLinkID)};
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
        public LinkModel GetInfo(string strLinkID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Link where LinkID=@LinkID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@LinkID",strLinkID)};
            LinkModel linkModel = new LinkModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    linkModel.ConfigID = dr["ConfigID"].ToString();
                    linkModel.TypeID = dr["TypeID"].ToString();
                    linkModel.SiteName = dr["SiteName"].ToString();
                    linkModel.SiteUrl = dr["SiteUrl"].ToString();
                    linkModel.LogoUrl = dr["LogoUrl"].ToString();
                    linkModel.ListID = dr["ListID"].ToString();
                    linkModel.AdminID = dr["AdminID"].ToString();
                    linkModel.AddTime = dr["AddTime"].ToString();
                    linkModel.IsClose = dr["IsClose"].ToString();
                    return linkModel;
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
        public void InsertInfo(LinkModel linkModel)
        {
            StringBuilder sql = new StringBuilder("insert into");
            sql.Append(" t_Link(ConfigID,TypeID,SiteName,SiteUrl,LogoUrl,ListID,AdminID,AddTime,IsClose)");
            sql.Append(" values(@ConfigID,@TypeID,@SiteName,@SiteUrl,@LogoUrl,@ListID,@AdminID,@AddTime,@IsClose)");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ConfigID",linkModel.ConfigID),
Config.Conn().CreateDbParameter("@TypeID",linkModel.TypeID),
Config.Conn().CreateDbParameter("@SiteName",linkModel.SiteName),
Config.Conn().CreateDbParameter("@SiteUrl",linkModel.SiteUrl),
Config.Conn().CreateDbParameter("@LogoUrl",linkModel.LogoUrl),
Config.Conn().CreateDbParameter("@ListID",linkModel.ListID),
Config.Conn().CreateDbParameter("@AdminID",linkModel.AdminID),
Config.Conn().CreateDbParameter("@AddTime",linkModel.AddTime),
Config.Conn().CreateDbParameter("@IsClose",linkModel.IsClose)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(LinkModel linkModel, string strLinkID)
        {
            StringBuilder sql = new StringBuilder("update t_Link set ");
            sql.Append(" ConfigID=@ConfigID,");
            sql.Append(" TypeID=@TypeID,");
            sql.Append(" SiteName=@SiteName,");
            sql.Append(" SiteUrl=@SiteUrl,");
            sql.Append(" LogoUrl=@LogoUrl,");
            sql.Append(" ListID=@ListID,");
            //sql.Append(" AdminID=@AdminID,");
            //sql.Append(" AddTime=@AddTime,");
            sql.Append(" IsClose=@IsClose");
            sql.Append(" where  LinkID=@LinkID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ConfigID",linkModel.ConfigID),
Config.Conn().CreateDbParameter("@TypeID",linkModel.TypeID),
Config.Conn().CreateDbParameter("@SiteName",linkModel.SiteName),
Config.Conn().CreateDbParameter("@SiteUrl",linkModel.SiteUrl),
Config.Conn().CreateDbParameter("@LogoUrl",linkModel.LogoUrl),
Config.Conn().CreateDbParameter("@ListID",linkModel.ListID),
//Config.Conn().CreateDbParameter("@AdminID",linkModel.AdminID),
//Config.Conn().CreateDbParameter("@AddTime",linkModel.AddTime),
Config.Conn().CreateDbParameter("@IsClose",linkModel.IsClose),
Config.Conn().CreateDbParameter("@LinkID",strLinkID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strLinkID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_Link where LinkID=@LinkID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@LinkID",strLinkID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strLinkID, string strIsClose)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_Link set IsClose=@IsClose where LinkID=@LinkID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@IsClose",strIsClose),
            Config.Conn().CreateDbParameter("@LinkID",strLinkID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 转移信息
        /// <summary>
        /// 转移信息
        /// </summary>
        public void TransferInfo(string strLinkID, string strConfigID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_Link set ConfigID=@ConfigID where LinkID=@LinkID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ConfigID",strConfigID),
            Config.Conn().CreateDbParameter("@LinkID",strLinkID)};
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
                sql.Append("select ListID from t_Link order by ListID desc limit 0,1");
            }
            else
            {
                sql.Append("select top 1 ListID from t_Link order by ListID desc");
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
                    sql.Append("create table tmp as select LinkID from t_Link where ListID<=@ListID and ListID>@OldListID;");
                    sql.Append("update t_Link set ListID=ListID-1 where LinkID in(select LinkID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_Link set ListID=ListID-1 where LinkID in(select LinkID from t_Link where  ListID<=@ListID and ListID>@OldListID)");
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
                    sql.Append("create table tmp as select LinkID from t_Link where ListID>=@ListID and ListID<@OldListID;");
                    sql.Append("update t_Link set ListID=ListID+1 where LinkID in(select LinkID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_Link set ListID=ListID+1 where LinkID in(select LinkID from t_Link where  ListID>=@ListID and ListID<@OldListID)");
                }
                DbParameter[] cmdParams = {
                Config.Conn().CreateDbParameter("@ListID",strListID),
                Config.Conn().CreateDbParameter("@OldListID",strOldListID)};
                Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
            }
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strLinkID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select " + strFieldName + " from t_Link where LinkID=@LinkID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@LinkID",strLinkID)};
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
