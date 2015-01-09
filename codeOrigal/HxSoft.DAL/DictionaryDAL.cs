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
    ///�����ֵ�-���ݷ�����
    /// ������:��С��
    /// ����:2010-12-6
    /// </summary>
    public class DictionaryDAL
    {

        #region �����Ϣ,����ĳ�ֶε�Ψһ��
        /// <summary>
        /// �����Ϣ,����ĳ�ֶε�Ψһ��
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue, string strParentID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Dictionary where " + strFieldName + "=@" + strFieldName + " and ParentID=@ParentID");
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

        public bool CheckInfo(string strFieldName, string strFieldValue, string strParentID, string strDictionaryID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Dictionary where " + strFieldName + "=@" + strFieldName + "  and ParentID=@ParentID and DictionaryID<>@DictionaryID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@"+strFieldName,strFieldValue),
            Config.Conn().CreateDbParameter("@ParentID",strParentID),
            Config.Conn().CreateDbParameter("@DictionaryID",strDictionaryID)};
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
        public DictionaryModel GetInfo(string strDictionaryID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Dictionary where DictionaryID=@DictionaryID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@DictionaryID",strDictionaryID)};
            DictionaryModel dictModel = new DictionaryModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    dictModel.DictionaryName = dr["DictionaryName"].ToString();
                    dictModel.DictionaryVal = dr["DictionaryVal"].ToString();
                    dictModel.ParentID = dr["ParentID"].ToString();
                    dictModel.ChildNum = dr["ChildNum"].ToString();
                    dictModel.ListID = dr["ListID"].ToString();
                    dictModel.AdminID = dr["AdminID"].ToString();
                    dictModel.AddTime = dr["AddTime"].ToString();
                    dictModel.IsClose = dr["IsClose"].ToString();
                    return dictModel;
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// ǰ̨��ȡ��Ϣ
        /// </summary>
        public DictionaryModel GetInfo2(string strDictionaryID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Dictionary where IsClose=0 and DictionaryID=@DictionaryID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@DictionaryID",strDictionaryID)};
            DictionaryModel dictModel = new DictionaryModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    dictModel.DictionaryName = dr["DictionaryName"].ToString();
                    dictModel.DictionaryVal = dr["DictionaryVal"].ToString();
                    dictModel.ParentID = dr["ParentID"].ToString();
                    dictModel.ChildNum = dr["ChildNum"].ToString();
                    dictModel.ListID = dr["ListID"].ToString();
                    dictModel.AdminID = dr["AdminID"].ToString();
                    dictModel.AddTime = dr["AddTime"].ToString();
                    dictModel.IsClose = dr["IsClose"].ToString();
                    return dictModel;
                }
                else
                {
                    return null;
                }
            }
        }

        public IList<DictionaryModel> GetInfoListByParentID(string strParentID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Dictionary where IsClose=0 and ParentID=@ParentID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ParentID",strParentID)};
            IList<DictionaryModel> dictList = new List<DictionaryModel>();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                while (dr.Read())
                {
                    DictionaryModel dictModel=new DictionaryModel();
                    dictModel.DictionaryID = dr["DictionaryID"].ToString();
                    dictModel.DictionaryName = dr["DictionaryName"].ToString();
                    dictModel.DictionaryVal = dr["DictionaryVal"].ToString();
                    dictModel.ParentID = dr["ParentID"].ToString();
                    dictModel.ChildNum = dr["ChildNum"].ToString();
                    dictModel.ListID = dr["ListID"].ToString();
                    dictModel.AdminID = dr["AdminID"].ToString();
                    dictModel.AddTime = dr["AddTime"].ToString();
                    dictModel.IsClose = dr["IsClose"].ToString();
                   dictList.Add(dictModel);
                }
                if (dictList.Count > 0)
                {
                    return dictList;
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
        public void InsertInfo(DictionaryModel dictModel)
        {
            StringBuilder sql = new StringBuilder("insert into");
            sql.Append(" t_Dictionary(DictionaryName,DictionaryVal,ParentID,ChildNum,ListID,AdminID,AddTime,IsClose)");
            sql.Append(" values(@DictionaryName,@DictionaryVal,@ParentID,@ChildNum,@ListID,@AdminID,@AddTime,@IsClose)");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@DictionaryName",dictModel.DictionaryName),
            Config.Conn().CreateDbParameter("@DictionaryVal",dictModel.DictionaryVal),
            Config.Conn().CreateDbParameter("@ParentID",dictModel.ParentID),
            Config.Conn().CreateDbParameter("@ChildNum",dictModel.ChildNum),
            Config.Conn().CreateDbParameter("@ListID",dictModel.ListID),
            Config.Conn().CreateDbParameter("@AdminID",dictModel.AdminID),
            Config.Conn().CreateDbParameter("@AddTime",dictModel.AddTime),
            Config.Conn().CreateDbParameter("@IsClose",dictModel.IsClose)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void UpdateInfo(DictionaryModel dictModel, string strDictionaryID)
        {
            StringBuilder sql = new StringBuilder("update t_Dictionary set ");
            sql.Append(" DictionaryName=@DictionaryName,");
            sql.Append(" DictionaryVal=@DictionaryVal,");
            //sql.Append(" ParentID=@ParentID,");
            //sql.Append(" ChildNum=@ChildNum,");
            sql.Append(" ListID=@ListID,");
            //sql.Append(" AdminID=@AdminID,");
            //sql.Append(" AddTime=@AddTime,");
            sql.Append(" IsClose=@IsClose");
            sql.Append(" where DictionaryID=@DictionaryID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@DictionaryName",dictModel.DictionaryName),
            Config.Conn().CreateDbParameter("@DictionaryVal",dictModel.DictionaryVal),
            //Config.Conn().CreateDbParameter("@ParentID",dictModel.ParentID),
            //Config.Conn().CreateDbParameter("@ChildNum",dictModel.ChildNum),
            Config.Conn().CreateDbParameter("@ListID",dictModel.ListID),
            //Config.Conn().CreateDbParameter("@AdminID",dictModel.AdminID),
            //Config.Conn().CreateDbParameter("@AddTime",dictModel.AddTime),
            Config.Conn().CreateDbParameter("@IsClose",dictModel.IsClose),
            Config.Conn().CreateDbParameter("@DictionaryID",strDictionaryID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region ɾ����Ϣ
        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        public void DeleteInfo(string strDictionaryID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_Dictionary where DictionaryID=@DictionaryID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@DictionaryID",strDictionaryID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region ����״̬
        /// <summary>
        /// ����״̬
        /// </summary>
        public void UpdateCloseStatus(string strDictionaryID, string strIsClose)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_Dictionary set IsClose=@IsClose where DictionaryID=@DictionaryID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@IsClose",strIsClose),
            Config.Conn().CreateDbParameter("@DictionaryID",strDictionaryID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region �ƶ���Ϣ
        /// <summary>
        /// �ƶ���Ϣ
        /// </summary>
        public void MoveInfo(DictionaryModel dictModel, string strDictionaryID)
        {
            StringBuilder sql = new StringBuilder("update t_Dictionary set ");
            sql.Append(" ParentID=@ParentID,");
            sql.Append(" ListID=@ListID");
            sql.Append(" where DictionaryID=@DictionaryID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ParentID",dictModel.ParentID),
            Config.Conn().CreateDbParameter("@ListID",dictModel.ListID),
            Config.Conn().CreateDbParameter("@DictionaryID",strDictionaryID)};
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
                sql.Append("select ListID from t_Dictionary where ParentID=@ParentID order by ListID desc limit 0,1");
            }
            else
            {
                sql.Append("select top 1 ListID from t_Dictionary where ParentID=@ParentID order by ListID desc");
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
                    sql.Append("create table tmp as select DictionaryID from t_Dictionary where ParentID=@ParentID and ListID<=@ListID and ListID>@OldListID;");
                    sql.Append("update t_Dictionary set ListID=ListID-1 where DictionaryID in(select DictionaryID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_Dictionary set ListID=ListID-1 where DictionaryID in(select DictionaryID from t_Dictionary where ParentID=@ParentID and ListID<=@ListID and ListID>@OldListID)");
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
                    sql.Append("create table tmp as select DictionaryID from t_Dictionary where ParentID=@ParentID and ListID>=@ListID and ListID<@OldListID;");
                    sql.Append("update t_Dictionary set ListID=ListID+1 where DictionaryID in(select DictionaryID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_Dictionary set ListID=ListID+1 where DictionaryID in(select DictionaryID from t_Dictionary where ParentID=@ParentID and ListID>=@ListID and ListID<@OldListID)");
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
        public void AddChildNum(string strDictionaryID)
        {
            StringBuilder sql = new StringBuilder("update t_Dictionary set ");
            sql.Append(" ChildNum=ChildNum+1");
            sql.Append(" where DictionaryID=@DictionaryID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@DictionaryID",strDictionaryID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region �����Ӽ���
        /// <summary>
        /// �����Ӽ���
        /// </summary>
        public void CutChildNum(string strDictionaryID)
        {
            StringBuilder sql = new StringBuilder("update t_Dictionary set ");
            sql.Append(" ChildNum=ChildNum-1");
            sql.Append(" where DictionaryID=@DictionaryID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@DictionaryID",strDictionaryID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region ��ʾ���������б�
        /// <summary>
        /// ��ʾ���������б�
        /// </summary>
        public StringBuilder ShowSelectTree(string strParentID, int intLevel, string strSelDictionaryID, string strSql)
        {
            StringBuilder tempStr = new StringBuilder();
            string strSel;
            StringBuilder tempHR = new StringBuilder();
            for (int i = 0; i <= intLevel; i++)
            {
                tempHR.Append("��&nbsp;");
            }
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Dictionary where ParentID=" + strParentID + strSql + " order by ListID asc");
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), null))
            {
                while (dr.Read())
                {
                    if (dr["DictionaryID"].ToString() == strSelDictionaryID)
                    {
                        strSel = "selected=\"selected\"";
                    }
                    else
                    {
                        strSel = "";
                    }
                    tempStr.Append("<option value=\"" + dr["DictionaryID"].ToString() + "\" " + strSel + ">" + tempHR + "��" + dr["DictionaryName"].ToString() + "</option>");
                    if (Convert.ToInt32(dr["ChildNum"]) > 0)
                    {
                        tempStr.Append(ShowSelectTree(dr["DictionaryID"].ToString(), intLevel + 1, strSelDictionaryID, strSql));
                    }
                }
            }
            return tempStr;
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
            sql.Append("select * from t_Dictionary where ParentID=" + strParentID + strSql + " order by ListID asc");
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), null))
            {
                while (dr.Read())
                {
                    drp.Items.Add(new ListItem(tempHR + "��" + dr["DictionaryName"].ToString(), dr["DictionaryID"].ToString()));
                    if (Convert.ToInt32(dr["ChildNum"]) > 0)
                    {
                        ShowSelectTree(dr["DictionaryID"].ToString(), intLevel + 1, drp, strSql);
                    }
                }
            }
        }
        #endregion

        #region ȡ·��
        /// <summary>
        /// ȡ·��
        /// </summary>
        public StringBuilder GetPath(string strDictionaryID)
        {
            StringBuilder strTemp = new StringBuilder();
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Dictionary where DictionaryID=@DictionaryID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@DictionaryID",strDictionaryID)};
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    string TempParentID = dr["ParentID"].ToString();

                    if (Convert.ToInt32(TempParentID) != 0)
                    {
                        strTemp.Append(GetPath(TempParentID) + "," + strDictionaryID);
                        return strTemp;
                    }
                    else
                    {
                        strTemp.Append(strDictionaryID);
                        return strTemp;
                    }
                }
                else
                {
                    strTemp.Append(strDictionaryID);
                    return strTemp;
                }
            }
        }
        #endregion

        #region ȡ��һ�����ID
        /// <summary>
        /// ȡ��һ�����ID
        /// </summary>
        public string GetFirstDictionaryID(string strParentID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select DictionaryID,ChildNum from t_Dictionary where IsClose=0 and ParentID=@ParentID order by ListID asc");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ParentID",strParentID)};
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    string TempChildNum = dr["ChildNum"].ToString();
                    string TempDictionaryID = dr["DictionaryID"].ToString();
                    if (Convert.ToInt32(TempChildNum) > 0)
                    {
                        return GetFirstDictionaryID(TempDictionaryID);
                    }
                    else
                    {
                        return TempDictionaryID;
                    }
                }
                else
                {
                    return strParentID;
                }
            }
        }
        #endregion

        #region ȡ�ӷ���ID
        /// <summary>
        /// ȡ�ӷ���ID
        /// </summary>
        public StringBuilder GetSubDictionaryID(string strParentID)
        {
            StringBuilder tempStr = new StringBuilder();
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Dictionary where ParentID=" + strParentID + " order by ListID asc");
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), null))
            {
                while (dr.Read())
                {
                    tempStr.Append(dr["DictionaryID"].ToString() + ",");
                    if (Convert.ToInt32(dr["ChildNum"]) > 0)
                    {
                        tempStr.Append(GetSubDictionaryID(dr["DictionaryID"].ToString()));
                    }
                }
            }
            return tempStr;
        }
        #endregion

        #region ȡ�ֶ�ֵ
        /// <summary>
        /// ȡ�ֶ�ֵ
        /// </summary>
        public string GetValueByField(string strFieldName, string strDictionaryID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select " + strFieldName + " from t_Dictionary where DictionaryID=@DictionaryID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@DictionaryID",strDictionaryID)};
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


        #region Ajaxȡ���
        /// <summary>
        /// Ajaxȡ���
        /// </summary>
        /// <param name="strParentID"></param>
        /// <param name="strType">select,checkbox,radio</param>
        /// <param name="strObjName"></param>
        /// <param name="intValType">0��1,0ȡID,1ȡVal</param>
        /// <returns></returns>
        public StringBuilder AjaxGetDictionaryList(string strParentID, string strType, string strObjName, int intValType)
        {
            StringBuilder strItem = new StringBuilder();
            string sql = "select DictionaryID,DictionaryVal,DictionaryName from t_Dictionary where IsClose=0 and ParentID=" + strParentID + " order by ListID asc";
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), null))
            {
                int i = 0;
                while (dr.Read())
                {
                    i++;
                    if (strType == "select")
                    {
                        strItem.Append("<option value=\"" + dr[intValType].ToString() + "\">" + dr[2].ToString() + "</option>\n");
                    }
                    if (strType == "checkbox")
                    {
                        strItem.Append("<label><input type=\"checkbox\" name=\"" + strObjName + "\" value=\"" + dr[intValType].ToString() + "\" id=\"" + strObjName + "_" + i + "\" />" + dr[2].ToString() + "</label>");
                    }
                    if (strType == "radio")
                    {
                        strItem.Append("<label><input type=\"radio\" name=\"" + strObjName + "\" value=\"" + dr[intValType].ToString() + "\" id=\"" + strObjName + "_" + i + "\" />" + dr[2].ToString() + "</label>");
                    }
                }
            }
            return strItem;
        }
        #endregion

        #region ���������ID��SQL
        /// <summary>
        /// ��������ĿID��SQL
        /// </summary>
        public string GetSubDictionarySql(string strDictionaryID)
        {
            string strSql;
            if (Config.DatabaseType == Config.DatabaseTypeCollection.Sql.ToString())
            {
                strSql = "select id from f_GetChildDictionaryID(" + strDictionaryID + ")";
            }
            else
            {
                strSql = GetSubDictionaryID(strDictionaryID).ToString();
            }
            return strSql;
        }
        #endregion

    }
}
