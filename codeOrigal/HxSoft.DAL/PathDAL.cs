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
    ///文件管理-数据访问类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>
    public class PathDAL
    {

        #region 读取信息
        /// <summary>
        /// 读取信息
        /// </summary>
        public PathModel GetInfo(string strPath)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Path where Path=@Path");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@Path",strPath)};
            PathModel pathModel = new PathModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    pathModel.AdminID = dr["AdminID"].ToString();
                    return pathModel;
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
        public void InsertInfo(PathModel pathModel)
        {
            StringBuilder sql = new StringBuilder("insert into");
            sql.Append(" t_Path(Path,AdminID)");
            sql.Append(" values(@Path,@AdminID)");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@Path",pathModel.Path),
            Config.Conn().CreateDbParameter("@AdminID",pathModel.AdminID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strPath)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_Path where Path=@Path");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@Path",strPath)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

    }
}
