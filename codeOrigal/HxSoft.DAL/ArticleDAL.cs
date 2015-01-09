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
    ///文章管理-数据访问类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>
    public class ArticleDAL
    {

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Article where " + strFieldName + "=@" + strFieldName + "");
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

        public bool CheckInfo(string strFieldName, string strFieldValue, string strArticleID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Article where " + strFieldName + "=@" + strFieldName + " and ArticleID<>@ArticleID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@"+strFieldName,strFieldValue),
            Config.Conn().CreateDbParameter("@ArticleID",strArticleID)};
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
        public ArticleModel GetInfo(string strArticleID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Article where ArticleID=@ArticleID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ArticleID",strArticleID)};
            ArticleModel artModel = new ArticleModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    artModel.ClassID = dr["ClassID"].ToString();
                    artModel.Title = dr["Title"].ToString();
                    artModel.Author = dr["Author"].ToString();
                    artModel.ComeFrom = dr["ComeFrom"].ToString();
                    artModel.Picture = dr["Picture"].ToString();
                    artModel.Video = dr["Video"].ToString();
                    artModel.Tags = dr["Tags"].ToString();
                    artModel.Keywords = dr["Keywords"].ToString();
                    artModel.Description = dr["Description"].ToString();
                    artModel.Details = dr["Details"].ToString();
                    artModel.IsRecommend = dr["IsRecommend"].ToString();
                    artModel.ListID = dr["ListID"].ToString();
                    artModel.ClickNum = dr["ClickNum"].ToString();
                    artModel.AdminID = dr["AdminID"].ToString();
                    artModel.AddTime = dr["AddTime"].ToString();
                    artModel.IsClose = dr["IsClose"].ToString();
                    return artModel;
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
        public ArticleModel GetInfo2(string strArticleID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Article where IsClose=0 and ArticleID=@ArticleID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ArticleID",strArticleID)};
            ArticleModel artModel = new ArticleModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    artModel.ClassID = dr["ClassID"].ToString();
                    artModel.Title = dr["Title"].ToString();
                    artModel.Author = dr["Author"].ToString();
                    artModel.ComeFrom = dr["ComeFrom"].ToString();
                    artModel.Picture = dr["Picture"].ToString();
                    artModel.Video = dr["Video"].ToString();
                    artModel.Tags = dr["Tags"].ToString();
                    artModel.Keywords = dr["Keywords"].ToString();
                    artModel.Description = dr["Description"].ToString();
                    artModel.Details = dr["Details"].ToString();
                    artModel.IsRecommend = dr["IsRecommend"].ToString();
                    artModel.ListID = dr["ListID"].ToString();
                    artModel.ClickNum = dr["ClickNum"].ToString();
                    artModel.AdminID = dr["AdminID"].ToString();
                    artModel.AddTime = dr["AddTime"].ToString();
                    artModel.IsClose = dr["IsClose"].ToString();
                    return artModel;
                }
                else
                {
                    return null;
                }
            }
        }

        public IList<ArticleModel> GetInfoList(string strClassID, int topnum)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select top(@topnum)* from t_Article where IsClose=0 and ClassID=@ClassID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ClassID",strClassID),
                                      Config.Conn().CreateDbParameter("@topnum",topnum)};
            IList<ArticleModel> artList = new List<ArticleModel>();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                while (dr.Read())
                {
                    ArticleModel artModel = new ArticleModel();
                    artModel.ArticleID = dr["ArticleID"].ToString();
                    artModel.ClassID = dr["ClassID"].ToString();
                    artModel.Title = dr["Title"].ToString();
                    artModel.Author = dr["Author"].ToString();
                    artModel.ComeFrom = dr["ComeFrom"].ToString();
                    artModel.Picture = dr["Picture"].ToString();
                    artModel.Video = dr["Video"].ToString();
                    artModel.Tags = dr["Tags"].ToString();
                    artModel.Keywords = dr["Keywords"].ToString();
                    artModel.Description = dr["Description"].ToString();
                    artModel.Details = dr["Details"].ToString();
                    artModel.IsRecommend = dr["IsRecommend"].ToString();
                    artModel.ListID = dr["ListID"].ToString();
                    artModel.ClickNum = dr["ClickNum"].ToString();
                    artModel.AdminID = dr["AdminID"].ToString();
                    artModel.AddTime = dr["AddTime"].ToString();
                    artModel.IsClose = dr["IsClose"].ToString();
                    artList.Add(artModel);
                }
                if (artList.Count > 0)
                {
                    return artList;
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
        public void InsertInfo(ArticleModel artModel)
        {
            StringBuilder sql = new StringBuilder("insert into");
            sql.Append(" t_Article(ClassID,Title,Author,ComeFrom,Picture,Video,Tags,Keywords,Description,Details,IsRecommend,ListID,ClickNum,AdminID,AddTime,IsClose)");
            sql.Append(" values(@ClassID,@Title,@Author,@ComeFrom,@Picture,@Video,@Tags,@Keywords,@Description,@Details,@IsRecommend,@ListID,@ClickNum,@AdminID,@AddTime,@IsClose)");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ClassID",artModel.ClassID),
            Config.Conn().CreateDbParameter("@Title",artModel.Title),
            Config.Conn().CreateDbParameter("@Author",artModel.Author),
            Config.Conn().CreateDbParameter("@ComeFrom",artModel.ComeFrom),
            Config.Conn().CreateDbParameter("@Picture",artModel.Picture),
            Config.Conn().CreateDbParameter("@Video",artModel.Video),
            Config.Conn().CreateDbParameter("@Tags",artModel.Tags),
            Config.Conn().CreateDbParameter("@Keywords",artModel.Keywords),
            Config.Conn().CreateDbParameter("@Description",artModel.Description),
            Config.Conn().CreateDbParameter("@Details",artModel.Details),
            Config.Conn().CreateDbParameter("@IsRecommend",artModel.IsRecommend),
            Config.Conn().CreateDbParameter("@ListID",artModel.ListID),
            Config.Conn().CreateDbParameter("@ClickNum",artModel.ClickNum),
            Config.Conn().CreateDbParameter("@AdminID",artModel.AdminID),
            Config.Conn().CreateDbParameter("@AddTime",artModel.AddTime),
            Config.Conn().CreateDbParameter("@IsClose",artModel.IsClose)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(ArticleModel artModel, string strArticleID)
        {
            StringBuilder sql = new StringBuilder("update t_Article set ");
            sql.Append(" ClassID=@ClassID,");
            sql.Append(" Title=@Title,");
            sql.Append(" Author=@Author,");
            sql.Append(" ComeFrom=@ComeFrom,");
            sql.Append(" Picture=@Picture,");
            sql.Append(" Video=@Video,");
            sql.Append(" Tags=@Tags,");
            sql.Append(" Keywords=@Keywords,");
            sql.Append(" Description=@Description,");
            sql.Append(" Details=@Details,");
            sql.Append(" IsRecommend=@IsRecommend,");
            sql.Append(" ListID=@ListID,");
            sql.Append(" ClickNum=@ClickNum,");
            //sql.Append(" AdminID=@AdminID,");
            sql.Append(" AddTime=@AddTime,");
            sql.Append(" IsClose=@IsClose");
            sql.Append(" where  ArticleID=@ArticleID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ClassID",artModel.ClassID),
            Config.Conn().CreateDbParameter("@Title",artModel.Title),
            Config.Conn().CreateDbParameter("@Author",artModel.Author),
            Config.Conn().CreateDbParameter("@ComeFrom",artModel.ComeFrom),
            Config.Conn().CreateDbParameter("@Picture",artModel.Picture),
            Config.Conn().CreateDbParameter("@Video",artModel.Video),
            Config.Conn().CreateDbParameter("@Tags",artModel.Tags),
            Config.Conn().CreateDbParameter("@Keywords",artModel.Keywords),
            Config.Conn().CreateDbParameter("@Description",artModel.Description),
            Config.Conn().CreateDbParameter("@Details",artModel.Details),
            Config.Conn().CreateDbParameter("@IsRecommend",artModel.IsRecommend),
            Config.Conn().CreateDbParameter("@ListID",artModel.ListID),
            Config.Conn().CreateDbParameter("@ClickNum",artModel.ClickNum),
            //Config.Conn().CreateDbParameter("@AdminID",artModel.AdminID),
            Config.Conn().CreateDbParameter("@AddTime",artModel.AddTime),
            Config.Conn().CreateDbParameter("@IsClose",artModel.IsClose),
            Config.Conn().CreateDbParameter("@ArticleID",strArticleID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strArticleID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_Article where ArticleID=@ArticleID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ArticleID",strArticleID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strArticleID, string strIsClose)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_Article set IsClose=@IsClose where ArticleID=@ArticleID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@IsClose",strIsClose),
            Config.Conn().CreateDbParameter("@ArticleID",strArticleID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 转移信息
        /// <summary>
        /// 转移信息
        /// </summary>
        public void TransferInfo(string strArticleID, string strClassID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_Article set ClassID=@ClassID where ArticleID=@ArticleID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ClassID",strClassID),
            Config.Conn().CreateDbParameter("@ArticleID",strArticleID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 取第一个信息ID
        /// <summary>
        /// 取第一个信息ID
        /// </summary>
        public string GetFirstID(string strClassID)
        {
            StringBuilder sql = new StringBuilder();
            if (Config.DatabaseType == Config.DatabaseTypeCollection.MySql.ToString())
            {
                sql.Append("select ArticleID from t_Article where IsClose=0 and ClassID=@ClassID order by ArticleID asc limit 0,1");
            }
            else
            {
                sql.Append("select top 1 ArticleID from t_Article where IsClose=0 and ClassID=@ClassID order by ArticleID asc");
            }
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ClassID",strClassID)};
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    return dr["ArticleID"].ToString();
                }
                else
                {
                    return "0";
                }
            }
        }
        #endregion

        #region 访问数加1
        /// <summary>
        /// 访问数加1
        /// </summary>
        public void Click(string strArticleID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_Article set ClickNum=ClickNum+1 where ArticleID=@ArticleID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ArticleID",strArticleID)};
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
                sql.Append("select ListID from t_Article order by ListID desc limit 0,1");
            }
            else
            {
                sql.Append("select top 1 ListID from t_Article order by ListID desc");
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
                    sql.Append("create table tmp as select ArticleID from t_Article where ListID<=@ListID and ListID>@OldListID;");
                    sql.Append("update t_Article set ListID=ListID-1 where ArticleID in(select ArticleID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_Article set ListID=ListID-1 where ArticleID in(select ArticleID from t_Article where  ListID<=@ListID and ListID>@OldListID)");
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
                    sql.Append("create table tmp as select ArticleID from t_Article where ListID>=@ListID and ListID<@OldListID;");
                    sql.Append("update t_Article set ListID=ListID+1 where ArticleID in(select ArticleID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_Article set ListID=ListID+1 where ArticleID in(select ArticleID from t_Article where  ListID>=@ListID and ListID<@OldListID)");
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
        public string GetValueByField(string strFieldName, string strArticleID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select " + strFieldName + " from t_Article where ArticleID=@ArticleID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ArticleID",strArticleID)};
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

        #region 取上一篇信息ID
        /// <summary>
        /// 取上一篇信息ID
        /// </summary>
        public string GetPrevID(string strClassID, string strArticleID)
        {
            StringBuilder sql = new StringBuilder();
            if (Config.DatabaseType == Config.DatabaseTypeCollection.MySql.ToString())
            {
                sql.Append("select ArticleID from t_Article where IsClose=0 and ClassID=@ClassID and ArticleID<@ArticleID order by ArticleID desc limit 0,1");
            }
            else
            {
                sql.Append("select top 1 ArticleID from t_Article where IsClose=0 and ClassID=@ClassID and ArticleID<@ArticleID order by ArticleID desc");
            }
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ClassID",strClassID),
            Config.Conn().CreateDbParameter("@ArticleID",strArticleID)};
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    return dr["ArticleID"].ToString();
                }
                else
                {
                    return "0";
                }
            }
        }
        #endregion

        #region 取下一篇信息ID
        /// <summary>
        /// 取下一篇信息ID
        /// </summary>
        public string GetNextID(string strClassID, string strArticleID)
        {
            StringBuilder sql = new StringBuilder();
            if (Config.DatabaseType == Config.DatabaseTypeCollection.MySql.ToString())
            {
                sql.Append("select ArticleID from t_Article where IsClose=0 and ClassID=@ClassID and ArticleID>@ArticleID order by ArticleID asc limit 0,1");
            }
            else
            {
                sql.Append("select top 1 ArticleID from t_Article where IsClose=0 and ClassID=@ClassID and ArticleID>@ArticleID order by ArticleID asc");
            }
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ClassID",strClassID),
            Config.Conn().CreateDbParameter("@ArticleID",strArticleID)};
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    return dr["ArticleID"].ToString();
                }
                else
                {
                    return "0";
                }
            }
        }
        #endregion

        #region RSS文件
        /// <summary>
        /// RSS文件
        /// </summary>
        /// <param name="strClassID"></param>
        /// <returns></returns>
        public StringBuilder RSS(string strClassID)
        {
            StringBuilder strXml = new StringBuilder();
            ClassModel claModel = (new ClassDAL()).GetInfo2(strClassID);
            if (claModel != null)
            {
                ConfigModel confModel = (new ConfigDAL()).GetInfo(claModel.ConfigID);
                if (confModel != null)
                {
                    strXml.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n");
                    strXml.Append("<rss version=\"2.0\">\n");
                    strXml.Append("<channel>\n");
                    strXml.Append("<title>" + claModel.ClassName + "</title>\n");
                    strXml.Append("<description>" + claModel.ClassName + "</description>\n");
                    strXml.Append("<link>http://" + HttpContext.Current.Request.ServerVariables["HTTP_HOST"].ToString() + confModel.WebsiteUrl + claModel.ClassEnName + Config.FileExt + "</link>\n");
                    strXml.Append("<copyright>Copyright " + DateTime.Now.Year.ToString() + " " + confModel.WebsiteName + " All Rights Reserved</copyright>\n");
                    strXml.Append("<generator>" + HttpContext.Current.Request.ServerVariables["HTTP_HOST"].ToString() + "</generator>\n");
                    string sql = "select * from t_Article where IsClose=0 and ClassID=" + strClassID + " order by AddTime desc";
                    using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), null))
                    {
                        while (dr.Read())
                        {
                            strXml.Append("<item>\n");
                            strXml.Append("<title><![CDATA[" + dr["Title"].ToString() + "]]></title>\n");
                            strXml.Append("<link>http://" + HttpContext.Current.Request.ServerVariables["HTTP_HOST"].ToString() + confModel.WebsiteUrl + "article-details-" + dr["ArticleID"].ToString() + Config.FileExt + "</link>\n");
                            strXml.Append("<description><![CDATA[" + dr["Details"].ToString() + "]]></description>\n");
                            strXml.Append("<pubDate>" + dr["AddTime"].ToString() + "</pubDate>\n");
                            strXml.Append("</item>\n");
                        }
                    }
                    strXml.Append("</channel>\n");
                    strXml.Append("</rss>\n");
                }
            }
            return strXml;
        }
        #endregion

    }
}
