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
    ///片段内容管理-数据访问类
    /// 创建人:Admin
    /// 日期:2012-10-17
    /// </summary>
    public class BlockDAL
    {
        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Block where " + strFieldName + "=@" + strFieldName + "");
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

        public bool CheckInfo(string strFieldName, string strFieldValue, string strBlockID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Block where " + strFieldName + "=@" + strFieldName + " and BlockID<>@BlockID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@"+strFieldName,strFieldValue),
Config.Conn().CreateDbParameter("@BlockID",strBlockID)};
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
        public string GetValueByField(string strFieldName, string strBlockID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select " + strFieldName + " from t_Block where BlockID=@BlockID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@BlockID",strBlockID)};
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
        public BlockModel GetInfo(string strBlockID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Block where BlockID=@BlockID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@BlockID",strBlockID)};
            BlockModel bloModel = new BlockModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    bloModel.Title = dr["Title"].ToString();
                    bloModel.BlockContent = dr["BlockContent"].ToString();
                    bloModel.AdminID = dr["AdminID"].ToString();
                    bloModel.AddTime = dr["AddTime"].ToString();
                    bloModel.IsClose = dr["IsClose"].ToString();
                    return bloModel;
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
        public BlockModel GetInfo2(string strBlockID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Block where IsClose=0 and BlockID=@BlockID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@BlockID",strBlockID)};
            BlockModel bloModel = new BlockModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    bloModel.Title = dr["Title"].ToString();
                    bloModel.BlockContent = dr["BlockContent"].ToString();
                    bloModel.AdminID = dr["AdminID"].ToString();
                    bloModel.AddTime = dr["AddTime"].ToString();
                    bloModel.IsClose = dr["IsClose"].ToString();
                    return bloModel;
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
        public void InsertInfo(BlockModel bloModel)
        {
            StringBuilder sql = new StringBuilder("insert into");
            sql.Append(" t_Block(Title,BlockContent,AdminID,AddTime,IsClose)");
            sql.Append(" values(@Title,@BlockContent,@AdminID,@AddTime,@IsClose)");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@Title",bloModel.Title),
Config.Conn().CreateDbParameter("@BlockContent",bloModel.BlockContent),
Config.Conn().CreateDbParameter("@AdminID",bloModel.AdminID),
Config.Conn().CreateDbParameter("@AddTime",bloModel.AddTime),
Config.Conn().CreateDbParameter("@IsClose",bloModel.IsClose)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(BlockModel bloModel, string strBlockID)
        {
            StringBuilder sql = new StringBuilder("update t_Block set ");
            sql.Append(" Title=@Title,");
            sql.Append(" BlockContent=@BlockContent,");
            //sql.Append(" AdminID=@AdminID,");
            //sql.Append(" AddTime=@AddTime,");
            sql.Append(" IsClose=@IsClose");
            sql.Append(" where  BlockID=@BlockID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@Title",bloModel.Title),
Config.Conn().CreateDbParameter("@BlockContent",bloModel.BlockContent),
//Config.Conn().CreateDbParameter("@AdminID",bloModel.AdminID),
//Config.Conn().CreateDbParameter("@AddTime",bloModel.AddTime),
Config.Conn().CreateDbParameter("@IsClose",bloModel.IsClose),
Config.Conn().CreateDbParameter("@BlockID",strBlockID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strBlockID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_Block where BlockID=@BlockID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@BlockID",strBlockID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strBlockID, string strIsClose)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_Block set IsClose=@IsClose where BlockID=@BlockID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@IsClose",strIsClose),
Config.Conn().CreateDbParameter("@BlockID",strBlockID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

    }
}
