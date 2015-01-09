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
    ///广告位管理-数据访问类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>
    public class AdPositionDAL
    {

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_AdPosition where " + strFieldName + "=@" + strFieldName + "");
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

        public bool CheckInfo(string strFieldName, string strFieldValue, string strAdPositionID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_AdPosition where " + strFieldName + "=@" + strFieldName + " and AdPositionID<>@AdPositionID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@"+strFieldName,strFieldValue),
            Config.Conn().CreateDbParameter("@AdPositionID",strAdPositionID)};
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
        public string GetValueByField(string strFieldName, string strAdPositionID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select " + strFieldName + " from t_AdPosition where AdPositionID=@AdPositionID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@AdPositionID",strAdPositionID)};
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
        public AdPositionModel GetInfo(string strAdPositionID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_AdPosition where AdPositionID=@AdPositionID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AdPositionID",strAdPositionID)};
            AdPositionModel adPosModel = new AdPositionModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    adPosModel.AdPositionName = dr["AdPositionName"].ToString();
                    adPosModel.AdPositionIntro = dr["AdPositionIntro"].ToString();
                    adPosModel.TypeID = dr["TypeID"].ToString();
                    adPosModel.Width = dr["Width"].ToString();
                    adPosModel.Height = dr["Height"].ToString();
                    adPosModel.Price = dr["Price"].ToString();
                    adPosModel.ListID = dr["ListID"].ToString();
                    adPosModel.AdminID = dr["AdminID"].ToString();
                    adPosModel.AddTime = dr["AddTime"].ToString();
                    adPosModel.IsClose = dr["IsClose"].ToString();
                    return adPosModel;
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 前台读取信息
        /// </summary>
        public AdPositionModel GetInfo2(string strAdPositionID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_AdPosition where IsClose=0 and AdPositionID=@AdPositionID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AdPositionID",strAdPositionID)};
            AdPositionModel adPosModel = new AdPositionModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    adPosModel.AdPositionName = dr["AdPositionName"].ToString();
                    adPosModel.AdPositionIntro = dr["AdPositionIntro"].ToString();
                    adPosModel.TypeID = dr["TypeID"].ToString();
                    adPosModel.Width = dr["Width"].ToString();
                    adPosModel.Height = dr["Height"].ToString();
                    adPosModel.Price = dr["Price"].ToString();
                    adPosModel.ListID = dr["ListID"].ToString();
                    adPosModel.AdminID = dr["AdminID"].ToString();
                    adPosModel.AddTime = dr["AddTime"].ToString();
                    adPosModel.IsClose = dr["IsClose"].ToString();
                    return adPosModel;
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
        public void InsertInfo(AdPositionModel adPosModel)
        {
            StringBuilder sql = new StringBuilder("insert into");
            sql.Append(" t_AdPosition(AdPositionName,AdPositionIntro,TypeID,Width,Height,Price,ListID,AdminID,AddTime,IsClose)");
            sql.Append(" values(@AdPositionName,@AdPositionIntro,@TypeID,@Width,@Height,@Price,@ListID,@AdminID,@AddTime,@IsClose)");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AdPositionName",adPosModel.AdPositionName),
            Config.Conn().CreateDbParameter("@AdPositionIntro",adPosModel.AdPositionIntro),
            Config.Conn().CreateDbParameter("@TypeID",adPosModel.TypeID),
            Config.Conn().CreateDbParameter("@Width",adPosModel.Width),
            Config.Conn().CreateDbParameter("@Height",adPosModel.Height),
            Config.Conn().CreateDbParameter("@Price",adPosModel.Price),
            Config.Conn().CreateDbParameter("@ListID",adPosModel.ListID),
            Config.Conn().CreateDbParameter("@AdminID",adPosModel.AdminID),
            Config.Conn().CreateDbParameter("@AddTime",adPosModel.AddTime),
            Config.Conn().CreateDbParameter("@IsClose",adPosModel.IsClose)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(AdPositionModel adPosModel, string strAdPositionID)
        {
            StringBuilder sql = new StringBuilder("update t_AdPosition set ");
            sql.Append(" AdPositionName=@AdPositionName,");
            sql.Append(" AdPositionIntro=@AdPositionIntro,");
            sql.Append(" TypeID=@TypeID,");
            sql.Append(" Width=@Width,");
            sql.Append(" Height=@Height,");
            sql.Append(" Price=@Price,");
            sql.Append(" ListID=@ListID,");
            //sql.Append(" AdminID=@AdminID,");
            //sql.Append(" AddTime=@AddTime,");
            sql.Append(" IsClose=@IsClose");
            sql.Append(" where AdPositionID=@AdPositionID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AdPositionName",adPosModel.AdPositionName),
            Config.Conn().CreateDbParameter("@AdPositionIntro",adPosModel.AdPositionIntro),
            Config.Conn().CreateDbParameter("@TypeID",adPosModel.TypeID),
            Config.Conn().CreateDbParameter("@Width",adPosModel.Width),
            Config.Conn().CreateDbParameter("@Height",adPosModel.Height),
            Config.Conn().CreateDbParameter("@Price",adPosModel.Price),
            Config.Conn().CreateDbParameter("@ListID",adPosModel.ListID),
            //Config.Conn().CreateDbParameter("@AdminID",adPosModel.AdminID),
            //Config.Conn().CreateDbParameter("@AddTime",adPosModel.AddTime),
            Config.Conn().CreateDbParameter("@IsClose",adPosModel.IsClose),
            Config.Conn().CreateDbParameter("@AdPositionID",strAdPositionID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strAdPositionID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_AdPosition where AdPositionID=@AdPositionID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AdPositionID",strAdPositionID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strAdPositionID, string strIsClose)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_AdPosition set IsClose=@IsClose where AdPositionID=@AdPositionID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@IsClose",strIsClose),
            Config.Conn().CreateDbParameter("@AdPositionID",strAdPositionID)};
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
                sql.Append("select ListID from t_AdPosition order by ListID desc limit 0,1");
            }
            else
            {
                sql.Append("select top 1 ListID from t_AdPosition order by ListID desc");
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
                    sql.Append("create table tmp as select AdPositionID from t_AdPosition where ListID<=@ListID and ListID>@OldListID;");
                    sql.Append("update t_AdPosition set ListID=ListID-1 where AdPositionID in(select AdPositionID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_AdPosition set ListID=ListID-1 where AdPositionID in(select AdPositionID from t_AdPosition where  ListID<=@ListID and ListID>@OldListID)");
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
                    sql.Append("create table tmp as select AdPositionID from t_AdPosition where ListID>=@ListID and ListID<@OldListID;");
                    sql.Append("update t_AdPosition set ListID=ListID+1 where AdPositionID in(select AdPositionID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_AdPosition set ListID=ListID+1 where AdPositionID in(select AdPositionID from t_AdPosition where  ListID>=@ListID and ListID<@OldListID)");
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
