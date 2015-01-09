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
    ///行业管理-数据访问类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>
    public class IndustryDAL
    {

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue,string strParentID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Industry where " + strFieldName + "=@" + strFieldName + " and ParentID=@ParentID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@"+strFieldName,strFieldValue),
            Config.Conn().CreateDbParameter("@ParentID",strParentID)};
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

        public bool CheckInfo(string strFieldName, string strFieldValue, string strParentID, string strIndustryID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Industry where " + strFieldName + "=@" + strFieldName + "  and ParentID=@ParentID and IndustryID<>@IndustryID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@"+strFieldName,strFieldValue),
            Config.Conn().CreateDbParameter("@ParentID",strParentID),
            Config.Conn().CreateDbParameter("@IndustryID",strIndustryID)};
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
        public IndustryModel GetInfo(string strIndustryID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Industry where IndustryID=@IndustryID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@IndustryID",strIndustryID)};
            IndustryModel indModel = new IndustryModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    indModel.IndustryName = dr["IndustryName"].ToString();
                    indModel.ParentID = dr["ParentID"].ToString();
                    indModel.ChildNum = dr["ChildNum"].ToString();
                    indModel.ListID = dr["ListID"].ToString();
                    indModel.AdminID = dr["AdminID"].ToString();
                    indModel.AddTime = dr["AddTime"].ToString();
                    indModel.IsClose = dr["IsClose"].ToString();
                    return indModel;
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
        public IndustryModel GetInfo2(string strIndustryID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Industry where IsClose=0 and IndustryID=@IndustryID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@IndustryID",strIndustryID)};
            IndustryModel indModel = new IndustryModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    indModel.IndustryName = dr["IndustryName"].ToString();
                    indModel.ParentID = dr["ParentID"].ToString();
                    indModel.ChildNum = dr["ChildNum"].ToString();
                    indModel.ListID = dr["ListID"].ToString();
                    indModel.AdminID = dr["AdminID"].ToString();
                    indModel.AddTime = dr["AddTime"].ToString();
                    indModel.IsClose = dr["IsClose"].ToString();
                    return indModel;
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
        public void InsertInfo(IndustryModel indModel)
        {
            StringBuilder sql = new StringBuilder("insert into");
            sql.Append(" t_Industry(IndustryName,ParentID,ChildNum,ListID,AdminID,AddTime,IsClose)");
            sql.Append(" values(@IndustryName,@ParentID,@ChildNum,@ListID,@AdminID,@AddTime,@IsClose)");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@IndustryName",indModel.IndustryName),
            Config.Conn().CreateDbParameter("@ParentID",indModel.ParentID),
            Config.Conn().CreateDbParameter("@ChildNum",indModel.ChildNum),
            Config.Conn().CreateDbParameter("@ListID",indModel.ListID),
            Config.Conn().CreateDbParameter("@AdminID",indModel.AdminID),
            Config.Conn().CreateDbParameter("@AddTime",indModel.AddTime),
            Config.Conn().CreateDbParameter("@IsClose",indModel.IsClose)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(IndustryModel indModel, string strIndustryID)
        {
            StringBuilder sql = new StringBuilder("update t_Industry set ");
            sql.Append(" IndustryName=@IndustryName,");
            //sql.Append(" ParentID=@ParentID,");
            //sql.Append(" ChildNum=@ChildNum,");
            sql.Append(" ListID=@ListID,");
            //sql.Append(" AdminID=@AdminID,");
            //sql.Append(" AddTime=@AddTime,");
            sql.Append(" IsClose=@IsClose");
            sql.Append(" where IndustryID=@IndustryID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@IndustryName",indModel.IndustryName),
            //Config.Conn().CreateDbParameter("@ParentID",indModel.ParentID),
            //Config.Conn().CreateDbParameter("@ChildNum",indModel.ChildNum),
            Config.Conn().CreateDbParameter("@ListID",indModel.ListID),
            //Config.Conn().CreateDbParameter("@AdminID",indModel.AdminID),
            //Config.Conn().CreateDbParameter("@AddTime",indModel.AddTime),
            Config.Conn().CreateDbParameter("@IsClose",indModel.IsClose),
            Config.Conn().CreateDbParameter("@IndustryID",strIndustryID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strIndustryID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_Industry where IndustryID=@IndustryID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@IndustryID",strIndustryID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strIndustryID, string strIsClose)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_Industry set IsClose=@IsClose where IndustryID=@IndustryID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@IsClose",strIsClose),
            Config.Conn().CreateDbParameter("@IndustryID",strIndustryID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 移动信息
        /// <summary>
        /// 移动信息
        /// </summary>
        public void MoveInfo(IndustryModel indModel, string strIndustryID)
        {
            StringBuilder sql = new StringBuilder("update t_Industry set ");
            sql.Append(" ParentID=@ParentID,");
            sql.Append(" ListID=@ListID");
            sql.Append(" where IndustryID=@IndustryID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ParentID",indModel.ParentID),
            Config.Conn().CreateDbParameter("@ListID",indModel.ListID),
            Config.Conn().CreateDbParameter("@IndustryID",strIndustryID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 读取排序号
        /// <summary>
        /// 读取排序号
        /// </summary>
        public string GetListID(string strParentID)
        {
            StringBuilder sql = new StringBuilder();
            if (Config.DatabaseType == Config.DatabaseTypeCollection.MySql.ToString())
            {
                sql.Append("select ListID from t_Industry where ParentID=@ParentID order by ListID desc limit 0,1");
            }
            else
            {
                sql.Append("select top 1 ListID from t_Industry where ParentID=@ParentID order by ListID desc");
            }
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ParentID",strParentID)};
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
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
        public void OrderInfo(string strParentID, string strListID, string strOldListID)
        {
            if (Convert.ToInt32(strListID) > Convert.ToInt32(strOldListID))
            {
                StringBuilder sql = new StringBuilder();
                if (Config.DatabaseType == Config.DatabaseTypeCollection.MySql.ToString())
                {
                    sql.Append("create table tmp as select IndustryID from t_Industry where ParentID=@ParentID and ListID<=@ListID and ListID>@OldListID;");
                    sql.Append("update t_Industry set ListID=ListID-1 where IndustryID in(select IndustryID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_Industry set ListID=ListID-1 where IndustryID in(select IndustryID from t_Industry where ParentID=@ParentID and ListID<=@ListID and ListID>@OldListID)");
                }
                DbParameter[] cmdParams = {
                Config.Conn().CreateDbParameter("@ParentID",strParentID),
                Config.Conn().CreateDbParameter("@ListID",strListID),
                Config.Conn().CreateDbParameter("@OldListID",strOldListID)};
                Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
            }
            else if (Convert.ToInt32(strListID) < Convert.ToInt32(strOldListID))
            {
                StringBuilder sql = new StringBuilder();
                if (Config.DatabaseType == Config.DatabaseTypeCollection.MySql.ToString())
                {
                    sql.Append("create table tmp as select IndustryID from t_Industry where ParentID=@ParentID and ListID>=@ListID and ListID<@OldListID;");
                    sql.Append("update t_Industry set ListID=ListID+1 where IndustryID in(select IndustryID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_Industry set ListID=ListID+1 where IndustryID in(select IndustryID from t_Industry where ParentID=@ParentID and ListID>=@ListID and ListID<@OldListID)");
                }
                DbParameter[] cmdParams = {
                Config.Conn().CreateDbParameter("@ParentID",strParentID),
                Config.Conn().CreateDbParameter("@ListID",strListID),
                Config.Conn().CreateDbParameter("@OldListID",strOldListID)};
                Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
            }
        }
        #endregion

        #region 增加子级数
        /// <summary>
        /// 增加子级数
        /// </summary>
        public void AddChildNum(string strIndustryID)
        {
            StringBuilder sql = new StringBuilder("update t_Industry set ");
            sql.Append(" ChildNum=ChildNum+1");
            sql.Append(" where IndustryID=@IndustryID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@IndustryID",strIndustryID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 减少子级数
        /// <summary>
        /// 减少子级数
        /// </summary>
        public void CutChildNum(string strIndustryID)
        {
            StringBuilder sql = new StringBuilder("update t_Industry set ");
            sql.Append(" ChildNum=ChildNum-1");
            sql.Append(" where IndustryID=@IndustryID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@IndustryID",strIndustryID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 显示下拉树形列表
        /// <summary>
        /// 显示下拉树形列表
        /// </summary>
        public void ShowSelectTree(string strParentID, int intLevel, DropDownList drp, string strSql)
        {
            StringBuilder tempStr = new StringBuilder();
            StringBuilder tempHR = new StringBuilder();
            for (int i = 0; i <= intLevel; i++)
            {
                tempHR.Append("│");
            }
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Industry where ParentID=" + strParentID + strSql + " order by ListID asc");
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), null))
            {
                while (dr.Read())
                {
                    drp.Items.Add(new ListItem(tempHR + "├" + dr["IndustryName"].ToString(), dr["IndustryID"].ToString()));
                    if (Convert.ToInt32(dr["ChildNum"]) > 0)
                    {
                        ShowSelectTree(dr["IndustryID"].ToString(), intLevel + 1, drp, strSql);
                    }
                }
            }
        }
        #endregion

        #region 取路径
        /// <summary>
        /// 取路径
        /// </summary>
        public StringBuilder GetPath(string strIndustryID)
        {
            StringBuilder strTemp = new StringBuilder();
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Industry where IndustryID=@IndustryID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@IndustryID",strIndustryID)};
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    string TempParentID = dr["ParentID"].ToString();

                    if (Convert.ToInt32(TempParentID) != 0)
                    {
                        strTemp.Append(GetPath(TempParentID) + "," + strIndustryID);
                        return strTemp;
                    }
                    else
                    {
                        strTemp.Append(strIndustryID);
                        return strTemp;
                    }
                }
                else
                {
                    strTemp.Append(strIndustryID);
                    return strTemp;
                }
            }
        }
        #endregion

        #region 取第一个类别ID
        /// <summary>
        /// 取第一个类别ID
        /// </summary>
        public string GetFirstIndustryID(string strParentID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select IndustryID,ChildNum from t_Industry where IsClose=0 and ParentID=@ParentID order by ListID asc");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ParentID",strParentID)};
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    string TempChildNum = dr["ChildNum"].ToString();
                    string TempIndustryID = dr["IndustryID"].ToString();
                    if (Convert.ToInt32(TempChildNum) > 0)
                    {
                        return GetFirstIndustryID(TempIndustryID);
                    }
                    else
                    {
                        return TempIndustryID;
                    }
                }
                else
                {
                    return strParentID;
                }
            }
        }
        #endregion

        #region 取字段值
        /// <summary>
        /// 取字段值
        /// </summary>
        public string GetValueByField(string strFieldName, string strIndustryID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select " + strFieldName + " from t_Industry where IndustryID=@IndustryID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@IndustryID",strIndustryID)};
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

        #region Ajax取类别
        /// <summary>
        /// Ajax取类别
        /// </summary>
        /// <param name="strParentID"></param>
        /// <param name="strType">select,checkbox,radio</param>
        /// <param name="strObjName"></param>
        /// <returns></returns>
        public StringBuilder AjaxGetIndustryList(string strParentID, string strType, string strObjName)
        {
            StringBuilder strItem = new StringBuilder();
            string sql = "select IndustryID,IndustryName from t_Industry where IsClose=0 and ParentID=" + strParentID + " order by ListID asc";
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), null))
            {
                int i = 0;
                while (dr.Read())
                {
                    i++;
                    if (strType == "select")
                    {
                        strItem.Append("<option value=\"" + dr[0].ToString() + "\">" + dr[1].ToString() + "</option>\n");
                    }
                    if (strType == "checkbox")
                    {
                        strItem.Append("<label><input type=\"checkbox\" name=\"" + strObjName + "\" value=\"" + dr[0].ToString() + "\" id=\"" + strObjName + "_" + i + "\" />" + dr[1].ToString() + "</label>");
                    }
                    if (strType == "radio")
                    {
                        strItem.Append("<label><input type=\"radio\" name=\"" + strObjName + "\" value=\"" + dr[0].ToString() + "\" id=\"" + strObjName + "_" + i + "\" />" + dr[1].ToString() + "</label>");
                    }
                }
            }
            return strItem;
        }
        #endregion

    }
}
