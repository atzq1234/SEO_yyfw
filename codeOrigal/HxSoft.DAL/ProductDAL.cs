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
    ///产品管理-数据访问类
    /// 创建人:杨小明
    /// 日期:2011-2-24
    /// </summary>
    public class ProductDAL
    {

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Product where " + strFieldName + "=@" + strFieldName + "");
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

        public bool CheckInfo(string strFieldName, string strFieldValue, string strProductID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Product where " + strFieldName + "=@" + strFieldName + " and ProductID<>@ProductID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@"+strFieldName,strFieldValue),
Config.Conn().CreateDbParameter("@ProductID",strProductID)};
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
        public ProductModel GetInfo(string strProductID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Product where ProductID=@ProductID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ProductID",strProductID)};
            ProductModel proModel = new ProductModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    proModel.ClassID = dr["ClassID"].ToString();
                    proModel.ProductName = dr["ProductName"].ToString();
                    proModel.SmallPic = dr["SmallPic"].ToString();
                    proModel.BigPic = dr["BigPic"].ToString();
                    proModel.Tags = dr["Tags"].ToString();
                    proModel.Keywords = dr["Keywords"].ToString();
                    proModel.Description = dr["Description"].ToString();
                    proModel.Details = dr["Details"].ToString();
                    proModel.ClickNum = dr["ClickNum"].ToString();
                    proModel.ListID = dr["ListID"].ToString();
                    proModel.IsRecommend = dr["IsRecommend"].ToString();
                    proModel.AdminID = dr["AdminID"].ToString();
                    proModel.AddTime = dr["AddTime"].ToString();
                    proModel.IsClose = dr["IsClose"].ToString();
                    return proModel;
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
        public ProductModel GetInfo2(string strProductID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Product where IsClose=0 and ProductID=@ProductID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ProductID",strProductID)};
            ProductModel proModel = new ProductModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    proModel.ClassID = dr["ClassID"].ToString();
                    proModel.ProductName = dr["ProductName"].ToString();
                    proModel.SmallPic = dr["SmallPic"].ToString();
                    proModel.BigPic = dr["BigPic"].ToString();
                    proModel.Tags = dr["Tags"].ToString();
                    proModel.Keywords = dr["Keywords"].ToString();
                    proModel.Description = dr["Description"].ToString();
                    proModel.Details = dr["Details"].ToString();
                    proModel.ClickNum = dr["ClickNum"].ToString();
                    proModel.ListID = dr["ListID"].ToString();
                    proModel.IsRecommend = dr["IsRecommend"].ToString();
                    proModel.AdminID = dr["AdminID"].ToString();
                    proModel.AddTime = dr["AddTime"].ToString();
                    proModel.IsClose = dr["IsClose"].ToString();
                    return proModel;
                }
                else
                {
                    return null;
                }
            }
        }


        public IList<ProductModel> GetInfoTopList(string strIsRecommand, int topnum)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select top(@TopNum)* from t_Product where IsClose=0 and IsRecommend=@IsRecommend");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@IsRecommend",strIsRecommand),
                                      Config.Conn().CreateDbParameter("@TopNum",topnum)};
            IList<ProductModel> proList = new List<ProductModel>();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                while (dr.Read())
                {
                    ProductModel proModel=new ProductModel();
                    proModel.ProductID = dr["ProductID"].ToString();
                    proModel.ClassID = dr["ClassID"].ToString();
                    proModel.ProductName = dr["ProductName"].ToString();
                    proModel.SmallPic = dr["SmallPic"].ToString();
                    proModel.BigPic = dr["BigPic"].ToString();
                    proModel.Tags = dr["Tags"].ToString();
                    proModel.Keywords = dr["Keywords"].ToString();
                    proModel.Description = dr["Description"].ToString();
                    proModel.Details = dr["Details"].ToString();
                    proModel.ClickNum = dr["ClickNum"].ToString();
                    proModel.ListID = dr["ListID"].ToString();
                    proModel.IsRecommend = dr["IsRecommend"].ToString();
                    proModel.AdminID = dr["AdminID"].ToString();
                    proModel.AddTime = dr["AddTime"].ToString();
                    proModel.IsClose = dr["IsClose"].ToString();
                    proList.Add(proModel);
                }
                if (proList.Count > 0)
                {
                    return proList;
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
        public void InsertInfo(ProductModel proModel)
        {
            StringBuilder sql = new StringBuilder("insert into");
            sql.Append(" t_Product(ClassID,ProductName,SmallPic,BigPic,Tags,Keywords,Description,Details,ClickNum,ListID,IsRecommend,AdminID,AddTime,IsClose)");
            sql.Append(" values(@ClassID,@ProductName,@SmallPic,@BigPic,@Tags,@Keywords,@Description,@Details,@ClickNum,@ListID,@IsRecommend,@AdminID,@AddTime,@IsClose)");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ClassID",proModel.ClassID),
Config.Conn().CreateDbParameter("@ProductName",proModel.ProductName),
Config.Conn().CreateDbParameter("@SmallPic",proModel.SmallPic),
Config.Conn().CreateDbParameter("@BigPic",proModel.BigPic),
Config.Conn().CreateDbParameter("@Tags",proModel.Tags),
Config.Conn().CreateDbParameter("@Keywords",proModel.Keywords),
Config.Conn().CreateDbParameter("@Description",proModel.Description),
Config.Conn().CreateDbParameter("@Details",proModel.Details),
Config.Conn().CreateDbParameter("@ClickNum",proModel.ClickNum),
Config.Conn().CreateDbParameter("@ListID",proModel.ListID),
Config.Conn().CreateDbParameter("@IsRecommend",proModel.IsRecommend),
Config.Conn().CreateDbParameter("@AdminID",proModel.AdminID),
Config.Conn().CreateDbParameter("@AddTime",proModel.AddTime),
Config.Conn().CreateDbParameter("@IsClose",proModel.IsClose)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(ProductModel proModel, string strProductID)
        {
            StringBuilder sql = new StringBuilder("update t_Product set ");
            sql.Append(" ClassID=@ClassID,");
            sql.Append(" ProductName=@ProductName,");
            sql.Append(" SmallPic=@SmallPic,");
            sql.Append(" BigPic=@BigPic,");
            sql.Append(" Tags=@Tags,");
            sql.Append(" Keywords=@Keywords,");
            sql.Append(" Description=@Description,");
            sql.Append(" Details=@Details,");
            sql.Append(" ClickNum=@ClickNum,");
            sql.Append(" ListID=@ListID,");
            sql.Append(" IsRecommend=@IsRecommend,");
            //sql.Append(" AdminID=@AdminID,");
            //sql.Append(" AddTime=@AddTime,");
            sql.Append(" IsClose=@IsClose");
            sql.Append(" where  ProductID=@ProductID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ClassID",proModel.ClassID),
Config.Conn().CreateDbParameter("@ProductName",proModel.ProductName),
Config.Conn().CreateDbParameter("@SmallPic",proModel.SmallPic),
Config.Conn().CreateDbParameter("@BigPic",proModel.BigPic),
Config.Conn().CreateDbParameter("@Tags",proModel.Tags),
Config.Conn().CreateDbParameter("@Keywords",proModel.Keywords),
Config.Conn().CreateDbParameter("@Description",proModel.Description),
Config.Conn().CreateDbParameter("@Details",proModel.Details),
Config.Conn().CreateDbParameter("@ClickNum",proModel.ClickNum),
Config.Conn().CreateDbParameter("@ListID",proModel.ListID),
Config.Conn().CreateDbParameter("@IsRecommend",proModel.IsRecommend),
//Config.Conn().CreateDbParameter("@AdminID",proModel.AdminID),
//Config.Conn().CreateDbParameter("@AddTime",proModel.AddTime),
Config.Conn().CreateDbParameter("@IsClose",proModel.IsClose),
Config.Conn().CreateDbParameter("@ProductID",strProductID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strProductID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_Product where ProductID=@ProductID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ProductID",strProductID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新关闭状态
        /// <summary>
        /// 更新关闭状态
        /// </summary>
        public void UpdateCloseStatus(string strProductID, string strIsClose)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_Product set IsClose=@IsClose where ProductID=@ProductID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@IsClose",strIsClose),
            Config.Conn().CreateDbParameter("@ProductID",strProductID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新推荐状态
        /// <summary>
        /// 更新推荐状态
        /// </summary>
        public void UpdateRecommendStatus(string strProductID, string strIsRecommend)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_Product set IsRecommend=@IsRecommend where ProductID=@ProductID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@IsRecommend",strIsRecommend),
            Config.Conn().CreateDbParameter("@ProductID",strProductID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 转移信息
        /// <summary>
        /// 转移信息
        /// </summary>
        public void TransferInfo(string strProductID, string strClassID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_Product set ClassID=@ClassID where ProductID=@ProductID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ClassID",strClassID),
            Config.Conn().CreateDbParameter("@ProductID",strProductID)};
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
                sql.Append("select ListID from t_Product order by ListID desc limit 0,1");
            }
            else
            {
                sql.Append("select top 1 ListID from t_Product order by ListID desc");
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
                    sql.Append("create table tmp as select ProductID from t_Product where ListID<=@ListID and ListID>@OldListID;");
                    sql.Append("update t_Product set ListID=ListID-1 where ProductID in(select ProductID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_Product set ListID=ListID-1 where ProductID in(select ProductID from t_Product where  ListID<=@ListID and ListID>@OldListID)");
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
                    sql.Append("create table tmp as select ProductID from t_Product where ListID>=@ListID and ListID<@OldListID;");
                    sql.Append("update t_Product set ListID=ListID+1 where ProductID in(select ProductID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_Product set ListID=ListID+1 where ProductID in(select ProductID from t_Product where  ListID>=@ListID and ListID<@OldListID)");
                }
                DbParameter[] cmdParams = {
                Config.Conn().CreateDbParameter("@ListID",strListID),
                Config.Conn().CreateDbParameter("@OldListID",strOldListID)};
                Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
            }
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
                sql.Append("select ProductID from t_Product where IsClose=0 and ClassID=@ClassID order by ProductID asc limit 0,1");
            }
            else
            {
                sql.Append("select top 1 ProductID from t_Product where IsClose=0 and ClassID=@ClassID order by ProductID asc");
            }
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ClassID",strClassID)};
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    return dr["ProductID"].ToString();
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
        public void Click(string strProductID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_Product set ClickNum=ClickNum+1 where ProductID=@ProductID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ProductID",strProductID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strProductID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select " + strFieldName + " from t_Product where ProductID=@ProductID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ProductID",strProductID)};
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
        public string GetPrevID(string strClassID, string strProductID)
        {
            StringBuilder sql = new StringBuilder();
            if (Config.DatabaseType == Config.DatabaseTypeCollection.MySql.ToString())
            {
                sql.Append("select ProductID from t_Product where IsClose=0 and ClassID=@ClassID and ProductID<@ProductID order by ProductID desc limit 0,1");
            }
            else
            {
                sql.Append("select top 1 ProductID from t_Product where IsClose=0 and ClassID=@ClassID and ProductID<@ProductID order by ProductID desc");
            }
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ClassID",strClassID),
            Config.Conn().CreateDbParameter("@ProductID",strProductID)};
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    return dr["ProductID"].ToString();
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
        public string GetNextID(string strClassID, string strProductID)
        {
            StringBuilder sql = new StringBuilder();
            if (Config.DatabaseType == Config.DatabaseTypeCollection.MySql.ToString())
            {
                sql.Append("select ProductID from t_Product where IsClose=0 and ClassID=@ClassID and ProductID>@ProductID order by ProductID asc limit 0,1");
            }
            else
            {
                sql.Append("select top 1 ProductID from t_Product where IsClose=0 and ClassID=@ClassID and ProductID>@ProductID order by ProductID asc");
            }
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ClassID",strClassID),
            Config.Conn().CreateDbParameter("@ProductID",strProductID)};
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    return dr["ProductID"].ToString();
                }
                else
                {
                    return "0";
                }
            }
        }
        #endregion


    }
}
