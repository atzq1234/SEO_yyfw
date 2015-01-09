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
    ///地区分类-数据访问类
    /// 创建人:杨小明
    /// 日期:2010-12-6
    /// </summary>
    public class AreaDAL
    {
        OleDbHelper datAc = new OleDbHelper(Config.AreaConnStr);

        #region 检查信息,保持某字段的唯一性
        /// <summary>
        /// 检查信息,保持某字段的唯一性
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue, string strParentID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Area where " + strFieldName + "=@" + strFieldName + " and ParentID=@ParentID");
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

        public bool CheckInfo(string strFieldName, string strFieldValue, string strParentID, string strAreaID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Area where " + strFieldName + "=@" + strFieldName + "  and ParentID=@ParentID and AreaID<>@AreaID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@"+strFieldName,strFieldValue),
            Config.Conn().CreateDbParameter("@ParentID",strParentID),
            Config.Conn().CreateDbParameter("@AreaID",strAreaID)};
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
        public AreaModel GetInfo(string strAreaID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Area where AreaID=@AreaID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AreaID",strAreaID)};
            AreaModel areaModel = new AreaModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    areaModel.AreaName = dr["AreaName"].ToString();
                    areaModel.ParentID = dr["ParentID"].ToString();
                    areaModel.ChildNum = dr["ChildNum"].ToString();
                    areaModel.ListID = dr["ListID"].ToString();
                    areaModel.AdminID = dr["AdminID"].ToString();
                    areaModel.AddTime = dr["AddTime"].ToString();
                    areaModel.IsClose = dr["IsClose"].ToString();
                    return areaModel;
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
        public AreaModel GetInfo2(string strAreaID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Area where IsClose=0 and AreaID=@AreaID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AreaID",strAreaID)};
            AreaModel areaModel = new AreaModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    areaModel.AreaName = dr["AreaName"].ToString();
                    areaModel.ParentID = dr["ParentID"].ToString();
                    areaModel.ChildNum = dr["ChildNum"].ToString();
                    areaModel.ListID = dr["ListID"].ToString();
                    areaModel.AdminID = dr["AdminID"].ToString();
                    areaModel.AddTime = dr["AddTime"].ToString();
                    areaModel.IsClose = dr["IsClose"].ToString();
                    return areaModel;
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
        public void InsertInfo(AreaModel areaModel)
        {
            StringBuilder sql = new StringBuilder("insert into");
            sql.Append(" t_Area(AreaName,ParentID,ChildNum,ListID,AdminID,AddTime,IsClose)");
            sql.Append(" values(@AreaName,@ParentID,@ChildNum,@ListID,@AdminID,@AddTime,@IsClose)");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AreaName",areaModel.AreaName),
            Config.Conn().CreateDbParameter("@ParentID",areaModel.ParentID),
            Config.Conn().CreateDbParameter("@ChildNum",areaModel.ChildNum),
            Config.Conn().CreateDbParameter("@ListID",areaModel.ListID),
            Config.Conn().CreateDbParameter("@AdminID",areaModel.AdminID),
            Config.Conn().CreateDbParameter("@AddTime",areaModel.AddTime),
            Config.Conn().CreateDbParameter("@IsClose",areaModel.IsClose)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新信息
        /// <summary>
        /// 更新信息
        /// </summary>
        public void UpdateInfo(AreaModel areaModel, string strAreaID)
        {
            StringBuilder sql = new StringBuilder("update t_Area set ");
            sql.Append(" AreaName=@AreaName,");
            //sql.Append(" ParentID=@ParentID,");
            //sql.Append(" ChildNum=@ChildNum,");
            sql.Append(" ListID=@ListID,");
            //sql.Append(" AdminID=@AdminID,");
            //sql.Append(" AddTime=@AddTime,");
            sql.Append(" IsClose=@IsClose");
            sql.Append(" where AreaID=@AreaID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AreaName",areaModel.AreaName),
            //Config.Conn().CreateDbParameter("@ParentID",areaModel.ParentID),
            //Config.Conn().CreateDbParameter("@ChildNum",areaModel.ChildNum),
            Config.Conn().CreateDbParameter("@ListID",areaModel.ListID),
            //Config.Conn().CreateDbParameter("@AdminID",areaModel.AdminID),
            //Config.Conn().CreateDbParameter("@AddTime",areaModel.AddTime),
            Config.Conn().CreateDbParameter("@IsClose",areaModel.IsClose),
            Config.Conn().CreateDbParameter("@AreaID",strAreaID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 删除信息
        /// <summary>
        /// 删除信息
        /// </summary>
        public void DeleteInfo(string strAreaID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_Area where AreaID=@AreaID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AreaID",strAreaID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 更新状态
        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateCloseStatus(string strAreaID, string strIsClose)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_Area set IsClose=@IsClose where AreaID=@AreaID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@IsClose",strIsClose),
            Config.Conn().CreateDbParameter("@AreaID",strAreaID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 移动信息
        /// <summary>
        /// 移动信息
        /// </summary>
        public void MoveInfo(AreaModel areaModel, string strAreaID)
        {
            StringBuilder sql = new StringBuilder("update t_Area set ");
            sql.Append(" ParentID=@ParentID,");
            sql.Append(" ListID=@ListID");
            sql.Append(" where AreaID=@AreaID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ParentID",areaModel.ParentID),
            Config.Conn().CreateDbParameter("@ListID",areaModel.ListID),
            Config.Conn().CreateDbParameter("@AreaID",strAreaID)};
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
                sql.Append("select ListID from t_Area where ParentID=@ParentID order by ListID desc limit 0,1");
            }
            else
            {
                sql.Append("select top 1 ListID from t_Area where ParentID=@ParentID order by ListID desc");
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
                    sql.Append("create table tmp as select AreaID from t_Area where ParentID=@ParentID and ListID<=@ListID and ListID>@OldListID;");
                    sql.Append("update t_Area set ListID=ListID-1 where AreaID in(select AreaID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_Area set ListID=ListID-1 where AreaID in(select AreaID from t_Area where ParentID=@ParentID and ListID<=@ListID and ListID>@OldListID)");
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
                    sql.Append("create table tmp as select AreaID from t_Area where ParentID=@ParentID and ListID>=@ListID and ListID<@OldListID;");
                    sql.Append("update t_Area set ListID=ListID+1 where AreaID in(select AreaID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_Area set ListID=ListID+1 where AreaID in(select AreaID from t_Area where ParentID=@ParentID and ListID>=@ListID and ListID<@OldListID)");
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
        public void AddChildNum(string strAreaID)
        {
            StringBuilder sql = new StringBuilder("update t_Area set ");
            sql.Append(" ChildNum=ChildNum+1");
            sql.Append(" where AreaID=@AreaID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AreaID",strAreaID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region 减少子级数
        /// <summary>
        /// 减少子级数
        /// </summary>
        public void CutChildNum(string strAreaID)
        {
            StringBuilder sql = new StringBuilder("update t_Area set ");
            sql.Append(" ChildNum=ChildNum-1");
            sql.Append(" where AreaID=@AreaID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AreaID",strAreaID)};
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
            sql.Append("select * from t_Area where ParentID=" + strParentID + strSql + " order by ListID asc");
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), null))
            {
                while (dr.Read())
                {
                    drp.Items.Add(new ListItem(tempHR + "├" + dr["AreaName"].ToString(), dr["AreaID"].ToString()));
                    if (Convert.ToInt32(dr["ChildNum"]) > 0)
                    {
                        ShowSelectTree(dr["AreaID"].ToString(), intLevel + 1, drp, strSql);
                    }
                }
            }
        }
        #endregion

        #region 取路径
        /// <summary>
        /// 取路径
        /// </summary>
        public StringBuilder GetPath(string strAreaID)
        {
            StringBuilder strTemp = new StringBuilder();
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Area where AreaID=@AreaID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@AreaID",strAreaID)};
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    string TempParentID = dr["ParentID"].ToString();

                    if (Convert.ToInt32(TempParentID) != 0)
                    {
                        strTemp.Append(GetPath(TempParentID) + "," + strAreaID);
                        return strTemp;
                    }
                    else
                    {
                        strTemp.Append(strAreaID);
                        return strTemp;
                    }
                }
                else
                {
                    strTemp.Append(strAreaID);
                    return strTemp;
                }
            }
        }
        #endregion

        #region 取第一个类别ID
        /// <summary>
        /// 取第一个类别ID
        /// </summary>
        public string GetFirstAreaID(string strParentID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select AreaID,ChildNum from t_Area where IsClose=0 and ParentID=@ParentID order by ListID asc");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ParentID",strParentID)};
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    string TempChildNum = dr["ChildNum"].ToString();
                    string TempAreaID = dr["AreaID"].ToString();
                    if (Convert.ToInt32(TempChildNum) > 0)
                    {
                        return GetFirstAreaID(TempAreaID);
                    }
                    else
                    {
                        return TempAreaID;
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
        public string GetValueByField(string strFieldName, string strAreaID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select " + strFieldName + " from t_Area where AreaID=@AreaID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@AreaID",strAreaID)};
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
        public StringBuilder AjaxGetAreaList(string strParentID, string strType, string strObjName)
        {
            StringBuilder strItem = new StringBuilder();
            string sql = "select AreaID,AreaName from t_Area where IsClose=0 and ParentID=" + strParentID + " order by ListID asc";
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
