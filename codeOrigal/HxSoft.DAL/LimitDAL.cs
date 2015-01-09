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
    ///Ȩ���ֶ�-���ݷ�����
    /// ������:��С��
    /// ����:2010-12-6
    /// </summary>
    public class LimitDAL
    {

        #region �����Ϣ,����ĳ�ֶε�Ψһ��
        /// <summary>
        /// �����Ϣ,����ĳ�ֶε�Ψһ��
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Limit where " + strFieldName + "=@" + strFieldName + "");
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

        public bool CheckInfo(string strFieldName, string strFieldValue, string strLimitID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Limit where " + strFieldName + "=@" + strFieldName + " and LimitID<>@LimitID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@"+strFieldName,strFieldValue),
            Config.Conn().CreateDbParameter("@LimitID",strLimitID)};
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

        #region ��ȡ��Ϣ
        /// <summary>
        /// ��ȡ��Ϣ
        /// </summary>
        public LimitModel GetInfo(string strLimitID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Limit where LimitID=@LimitID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@LimitID",strLimitID)};
            LimitModel limModel = new LimitModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    limModel.LimitField = dr["LimitField"].ToString();
                    limModel.LimitValue = dr["LimitValue"].ToString();
                    limModel.ParentID = dr["ParentID"].ToString();
                    limModel.ChildNum = dr["ChildNum"].ToString();
                    limModel.ListID = dr["ListID"].ToString();
                    limModel.IsDist = dr["IsDist"].ToString();
                    limModel.AdminID = dr["AdminID"].ToString();
                    limModel.AddTime = dr["AddTime"].ToString();
                    limModel.IsClose = dr["IsClose"].ToString();
                    return limModel;
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// ����Ȩ��ֵ��ȡ��Ϣ
        /// </summary>
        public LimitModel GetInfoByLimitValue(string strLimitValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Limit where LimitValue=@LimitValue");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@LimitValue",strLimitValue)};
            LimitModel limModel = new LimitModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    limModel.LimitID = dr["LimitID"].ToString();
                    limModel.LimitField = dr["LimitField"].ToString();
                    limModel.LimitValue = dr["LimitValue"].ToString();
                    limModel.ParentID = dr["ParentID"].ToString();
                    limModel.ChildNum = dr["ChildNum"].ToString();
                    limModel.ListID = dr["ListID"].ToString();
                    limModel.IsDist = dr["IsDist"].ToString();
                    limModel.AdminID = dr["AdminID"].ToString();
                    limModel.AddTime = dr["AddTime"].ToString();
                    limModel.IsClose = dr["IsClose"].ToString();
                    return limModel;
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void InsertInfo(LimitModel limModel)
        {
            StringBuilder sql = new StringBuilder("insert into");
            sql.Append(" t_Limit(LimitField,LimitValue,ParentID,ChildNum,ListID,IsDist,AdminID,AddTime,IsClose)");
            sql.Append(" values(@LimitField,@LimitValue,@ParentID,@ChildNum,@ListID,@IsDist,@AdminID,@AddTime,@IsClose)");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@LimitField",limModel.LimitField),
            Config.Conn().CreateDbParameter("@LimitValue",limModel.LimitValue),
            Config.Conn().CreateDbParameter("@ParentID",limModel.ParentID),
            Config.Conn().CreateDbParameter("@ChildNum",limModel.ChildNum),
            Config.Conn().CreateDbParameter("@ListID",limModel.ListID),
            Config.Conn().CreateDbParameter("@IsDist",limModel.IsDist),
            Config.Conn().CreateDbParameter("@AdminID",limModel.AdminID),
            Config.Conn().CreateDbParameter("@AddTime",limModel.AddTime),
            Config.Conn().CreateDbParameter("@IsClose",limModel.IsClose)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void UpdateInfo(LimitModel limModel, string strLimitID)
        {
            StringBuilder sql = new StringBuilder("update t_Limit set ");
            sql.Append(" LimitField=@LimitField,");
            sql.Append(" LimitValue=@LimitValue,");
            //sql.Append(" ParentID=@ParentID,");
            //sql.Append(" ChildNum=@ChildNum,");
            sql.Append(" ListID=@ListID,");
            sql.Append(" IsDist=@IsDist,");
            //sql.Append(" AdminID=@AdminID,");
            //sql.Append(" AddTime=@AddTime,");
            sql.Append(" IsClose=@IsClose");
            sql.Append(" where LimitID=@LimitID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@LimitField",limModel.LimitField),
            Config.Conn().CreateDbParameter("@LimitValue",limModel.LimitValue),
            //Config.Conn().CreateDbParameter("@ParentID",limModel.ParentID),
            //Config.Conn().CreateDbParameter("@ChildNum",limModel.ChildNum),
            Config.Conn().CreateDbParameter("@ListID",limModel.ListID),
            Config.Conn().CreateDbParameter("@IsDist",limModel.IsDist),
            //Config.Conn().CreateDbParameter("@AdminID",limModel.AdminID),
            //Config.Conn().CreateDbParameter("@AddTime",limModel.AddTime),
            Config.Conn().CreateDbParameter("@IsClose",limModel.IsClose),
            Config.Conn().CreateDbParameter("@LimitID",strLimitID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region ɾ����Ϣ
        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        public void DeleteInfo(string strLimitID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_Limit where LimitID=@LimitID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@LimitID",strLimitID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region ����״̬
        /// <summary>
        /// ����״̬
        /// </summary>
        public void UpdateCloseStatus(string strLimitID, string strIsClose)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_Limit set IsClose=@IsClose where LimitID=@LimitID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@IsClose",strIsClose),
            Config.Conn().CreateDbParameter("@LimitID",strLimitID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
         #endregion

        #region �ƶ���Ϣ
        /// <summary>
        /// �ƶ���Ϣ
        /// </summary>
        public void MoveInfo(LimitModel limModel, string strLimitID)
        {
            StringBuilder sql = new StringBuilder("update t_Limit set ");
            sql.Append(" ParentID=@ParentID,");
            sql.Append(" ListID=@ListID");
            sql.Append(" where LimitID=@LimitID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ParentID",limModel.ParentID),
            Config.Conn().CreateDbParameter("@ListID",limModel.ListID),
            Config.Conn().CreateDbParameter("@LimitID",strLimitID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region ��ȡ�����
        /// <summary>
        /// ��ȡ�����
        /// </summary>
        public string GetListID(string strParentID)
        {
            StringBuilder sql = new StringBuilder();
            if (Config.DatabaseType == Config.DatabaseTypeCollection.MySql.ToString())
            {
                sql.Append("select ListID from t_Limit where ParentID=@ParentID order by ListID desc limit 0,1");
            }
            else
            {
                sql.Append("select top 1 ListID from t_Limit where ParentID=@ParentID order by ListID desc");
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

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strParentID, string strListID, string strOldListID)
        {
            if (Convert.ToInt32(strListID) > Convert.ToInt32(strOldListID))
            {
                StringBuilder sql = new StringBuilder();
                if (Config.DatabaseType == Config.DatabaseTypeCollection.MySql.ToString())
                {
                    sql.Append("create table tmp as select LimitID from t_Limit where ParentID=@ParentID and ListID<=@ListID and ListID>@OldListID;");
                    sql.Append("update t_Limit set ListID=ListID-1 where LimitID in(select LimitID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_Limit set ListID=ListID-1 where LimitID in(select LimitID from t_Limit where ParentID=@ParentID and ListID<=@ListID and ListID>@OldListID)");
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
                    sql.Append("create table tmp as select LimitID from t_Limit where ParentID=@ParentID and ListID>=@ListID and ListID<@OldListID;");
                    sql.Append("update t_Limit set ListID=ListID+1 where LimitID in(select LimitID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_Limit set ListID=ListID+1 where LimitID in(select LimitID from t_Limit where ParentID=@ParentID and ListID>=@ListID and ListID<@OldListID)");
                }
                DbParameter[] cmdParams = {
                Config.Conn().CreateDbParameter("@ParentID",strParentID),
                Config.Conn().CreateDbParameter("@ListID",strListID),
                Config.Conn().CreateDbParameter("@OldListID",strOldListID)};
                Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
            }
        }
        #endregion

        #region �����Ӽ���
        /// <summary>
        /// �����Ӽ���
        /// </summary>
        public void AddChildNum(string strLimitID)
        {
            StringBuilder sql = new StringBuilder("update t_Limit set ");
            sql.Append(" ChildNum=ChildNum+1");
            sql.Append(" where LimitID=@LimitID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@LimitID",strLimitID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region �����Ӽ���
        /// <summary>
        /// �����Ӽ���
        /// </summary>
        public void CutChildNum(string strLimitID)
        {
            StringBuilder sql = new StringBuilder("update t_Limit set ");
            sql.Append(" ChildNum=ChildNum-1");
            sql.Append(" where LimitID=@LimitID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@LimitID",strLimitID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region ��ʾ���������б�
        /// <summary>
        /// ��ʾ���������б�
        /// </summary>
        public void ShowSelectTree(string strParentID, int intLevel, DropDownList drp, string strSql)
        {
            StringBuilder tempStr = new StringBuilder();
            StringBuilder tempHR = new StringBuilder();
            for (int i = 0; i <= intLevel; i++)
            {
                tempHR.Append("��");
            }
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Limit where ParentID=" + strParentID + strSql + " order by ListID asc");
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), null))
            {
                while (dr.Read())
                {
                    drp.Items.Add(new ListItem(tempHR + "��" + dr["LimitField"].ToString(), dr["LimitID"].ToString()));
                    if (Convert.ToInt32(dr["ChildNum"]) > 0)
                    {
                        ShowSelectTree(dr["LimitID"].ToString(), intLevel + 1, drp, strSql);
                    }
                }
            }
        }
        #endregion

        #region ȡ·��
        /// <summary>
        /// ȡ·��
        /// </summary>
        public StringBuilder GetPath(string strLimitID)
        {
            StringBuilder strTemp = new StringBuilder();
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Limit where LimitID=@LimitID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@LimitID",strLimitID)};
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    string TempParentID = dr["ParentID"].ToString();

                    if (Convert.ToInt32(TempParentID) != 0)
                    {
                        strTemp.Append(GetPath(TempParentID) + "," + strLimitID);
                        return strTemp;
                    }
                    else
                    {
                        strTemp.Append(strLimitID);
                        return strTemp;
                    }
                }
                else
                {
                    strTemp.Append(strLimitID);
                    return strTemp;
                }
            }
        }
        #endregion

        #region ȡ�ֶ�ֵ
        /// <summary>
        /// ȡ�ֶ�ֵ
        /// </summary>
        public string GetValueByField(string strFieldName, string strLimitID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select " + strFieldName + " from t_Limit where LimitID=@LimitID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@LimitID",strLimitID)};
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

        #region ����Ȩ��ֵȡ�ֶ�ֵ
        /// <summary>
        /// ����Ȩ��ֵȡ�ֶ�ֵ
        /// </summary>
        public string GetValueByLimitValue(string strFieldName, string strLimitValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select " + strFieldName + " from t_Limit where LimitValue=@LimitValue");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@LimitValue",strLimitValue)};
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
