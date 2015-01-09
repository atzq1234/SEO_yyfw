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
    ///系统配置管理-数据访问类
    /// 创建人:Admin
    /// 日期:2012-10-13
    /// </summary>
    public class SetDAL
    {
        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Set where " + strFieldName + "=@" + strFieldName + "");
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

        public bool CheckInfo(string strFieldName, string strFieldValue, string strSetID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Set where " + strFieldName + "=@" + strFieldName + " and SetID<>@SetID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@"+strFieldName,strFieldValue),
Config.Conn().CreateDbParameter("@SetID",strSetID)};
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
        public string GetValueByField(string strFieldName, string strSetID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select " + strFieldName + " from t_Set where SetID=@SetID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@SetID",strSetID)};
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
        public SetModel GetInfo()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Set ");
            SetModel seModel = new SetModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), null))
            {
                if (dr.Read())
                {
                    seModel.SetID = dr["SetID"].ToString();
                    seModel.WaterTypeID = dr["WaterTypeID"].ToString();
                    seModel.WaterText = dr["WaterText"].ToString();
                    seModel.Font = dr["Font"].ToString();
                    seModel.FontSize =dr["FontSize"].ToString();
                    seModel.FontColor = dr["FontColor"].ToString();
                    seModel.WaterPic = dr["WaterPic"].ToString();
                    seModel.WaterPosition = dr["WaterPosition"].ToString();
                    seModel.IsArticleThumb = dr["IsArticleThumb"].ToString();
                    seModel.ArticleThumbWidth = dr["ArticleThumbWidth"].ToString();
                    seModel.ArticleThumbHeight = dr["ArticleThumbHeight"].ToString();
                    seModel.IsProductThumb = dr["IsProductThumb"].ToString();
                    seModel.ProductThumbWidth = dr["ProductThumbWidth"].ToString();
                    seModel.ProductThumbHeight = dr["ProductThumbHeight"].ToString();
                    seModel.IsPhotoThumb = dr["IsPhotoThumb"].ToString();
                    seModel.PhotoThumbWidth = dr["PhotoThumbWidth"].ToString();
                    seModel.PhotoThumbHeight = dr["PhotoThumbHeight"].ToString();
                    return seModel;
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
        public void InsertInfo(SetModel seModel)
        {
            StringBuilder sql = new StringBuilder("insert into");
            sql.Append(" t_Set(WaterTypeID,WaterText,Font,FontSize,FontColor,WaterPic,WaterPosition,IsArticleThumb,ArticleThumbWidth,ArticleThumbHeight,IsProductThumb,ProductThumbWidth,ProductThumbHeight,IsPhotoThumb,PhotoThumbWidth,PhotoThumbHeight)");
            sql.Append(" values(@WaterTypeID,@WaterText,@Font,@FontSize,@FontColor,@WaterPic,@WaterPosition,@IsArticleThumb,@ArticleThumbWidth,@ArticleThumbHeight,@IsProductThumb,@ProductThumbWidth,@ProductThumbHeight,@IsPhotoThumb,@PhotoThumbWidth,@PhotoThumbHeight)");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@WaterTypeID",seModel.WaterTypeID),
Config.Conn().CreateDbParameter("@WaterText",seModel.WaterText),
Config.Conn().CreateDbParameter("@Font",seModel.Font),
Config.Conn().CreateDbParameter("@FontSize",seModel.FontSize),
Config.Conn().CreateDbParameter("@FontColor",seModel.FontColor),
Config.Conn().CreateDbParameter("@WaterPic",seModel.WaterPic),
Config.Conn().CreateDbParameter("@WaterPosition",seModel.WaterPosition),
Config.Conn().CreateDbParameter("@IsArticleThumb",seModel.IsArticleThumb),
Config.Conn().CreateDbParameter("@ArticleThumbWidth",seModel.ArticleThumbWidth),
Config.Conn().CreateDbParameter("@ArticleThumbHeight",seModel.ArticleThumbHeight),
Config.Conn().CreateDbParameter("@IsProductThumb",seModel.IsProductThumb),
Config.Conn().CreateDbParameter("@ProductThumbWidth",seModel.ProductThumbWidth),
Config.Conn().CreateDbParameter("@ProductThumbHeight",seModel.ProductThumbHeight),
Config.Conn().CreateDbParameter("@IsPhotoThumb",seModel.IsPhotoThumb),
Config.Conn().CreateDbParameter("@PhotoThumbWidth",seModel.PhotoThumbWidth),
Config.Conn().CreateDbParameter("@PhotoThumbHeight",seModel.PhotoThumbHeight)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(SetModel seModel)
        {
            StringBuilder sql = new StringBuilder("update t_Set set ");
            sql.Append(" WaterTypeID=@WaterTypeID,");
            sql.Append(" WaterText=@WaterText,");
            sql.Append(" Font=@Font,");
            sql.Append(" FontSize=@FontSize,");
            sql.Append(" FontColor=@FontColor,");
            sql.Append(" WaterPic=@WaterPic,");
            sql.Append(" WaterPosition=@WaterPosition,");
            sql.Append(" IsArticleThumb=@IsArticleThumb,");
            sql.Append(" ArticleThumbWidth=@ArticleThumbWidth,");
            sql.Append(" ArticleThumbHeight=@ArticleThumbHeight,");
            sql.Append(" IsProductThumb=@IsProductThumb,");
            sql.Append(" ProductThumbWidth=@ProductThumbWidth,");
            sql.Append(" ProductThumbHeight=@ProductThumbHeight,");
            sql.Append(" IsPhotoThumb=@IsPhotoThumb,");
            sql.Append(" PhotoThumbWidth=@PhotoThumbWidth,");
            sql.Append(" PhotoThumbHeight=@PhotoThumbHeight");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@WaterTypeID",seModel.WaterTypeID),
Config.Conn().CreateDbParameter("@WaterText",seModel.WaterText),
Config.Conn().CreateDbParameter("@Font",seModel.Font),
Config.Conn().CreateDbParameter("@FontSize",seModel.FontSize),
Config.Conn().CreateDbParameter("@FontColor",seModel.FontColor),
Config.Conn().CreateDbParameter("@WaterPic",seModel.WaterPic),
Config.Conn().CreateDbParameter("@WaterPosition",seModel.WaterPosition),
Config.Conn().CreateDbParameter("@IsArticleThumb",seModel.IsArticleThumb),
Config.Conn().CreateDbParameter("@ArticleThumbWidth",seModel.ArticleThumbWidth),
Config.Conn().CreateDbParameter("@ArticleThumbHeight",seModel.ArticleThumbHeight),
Config.Conn().CreateDbParameter("@IsProductThumb",seModel.IsProductThumb),
Config.Conn().CreateDbParameter("@ProductThumbWidth",seModel.ProductThumbWidth),
Config.Conn().CreateDbParameter("@ProductThumbHeight",seModel.ProductThumbHeight),
Config.Conn().CreateDbParameter("@IsPhotoThumb",seModel.IsPhotoThumb),
Config.Conn().CreateDbParameter("@PhotoThumbWidth",seModel.PhotoThumbWidth),
Config.Conn().CreateDbParameter("@PhotoThumbHeight",seModel.PhotoThumbHeight)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion
    }
}
