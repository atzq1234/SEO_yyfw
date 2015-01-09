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
    ///��Ŀ����-���ݷ�����
    /// ������:��С��
    /// ����:2010-12-6
    /// </summary>
    public class ClassDAL
    {

        #region �����Ϣ,����ĳ�ֶε�Ψһ��
        /// <summary>
        /// �����Ϣ,����ĳ�ֶε�Ψһ��
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue, string strConfigID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Class where " + strFieldName + "=@" + strFieldName + " and ConfigID=@ConfigID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@"+strFieldName,strFieldValue),
            Config.Conn().CreateDbParameter("@ConfigID",strConfigID)};
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

        public bool CheckInfo(string strFieldName, string strFieldValue, string strConfigID, string strClassID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Class where " + strFieldName + "=@" + strFieldName + " and ConfigID=@ConfigID  and ClassID<>@ClassID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@"+strFieldName,strFieldValue),
            Config.Conn().CreateDbParameter("@ConfigID",strConfigID),
            Config.Conn().CreateDbParameter("@ClassID",strClassID)};
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

        public bool CheckInfo2(string strFieldName, string strFieldValue, string strParentID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Class where " + strFieldName + "=@" + strFieldName + " and ParentID=@ParentID");
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

        public bool CheckInfo2(string strFieldName, string strFieldValue, string strParentID, string strClassID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Class where " + strFieldName + "=@" + strFieldName + " and ParentID=@ParentID  and ClassID<>@ClassID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@"+strFieldName,strFieldValue),
            Config.Conn().CreateDbParameter("@ParentID",strParentID),
            Config.Conn().CreateDbParameter("@ClassID",strClassID)};
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
        public ClassModel GetInfo(string strClassID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Class where ClassID=@ClassID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ClassID",strClassID)};
            ClassModel claModel = new ClassModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    claModel.ConfigID = dr["ConfigID"].ToString();
                    claModel.ClassID = dr["ClassID"].ToString();
                    claModel.ClassName = dr["ClassName"].ToString();
                    claModel.ClassEnName = dr["ClassEnName"].ToString();
                    claModel.ClassPropertyID = dr["ClassPropertyID"].ToString();
                    claModel.ClassTemplateID = dr["ClassTemplateID"].ToString();
                    claModel.ClassPic = dr["ClassPic"].ToString();
                    claModel.LinkUrl = dr["LinkUrl"].ToString();
                    claModel.Target = dr["Target"].ToString();
                    claModel.IsGoToFirst = dr["IsGoToFirst"].ToString();
                    claModel.IsShowNav = dr["IsShowNav"].ToString();
                    claModel.Keywords = dr["Keywords"].ToString();
                    claModel.Description = dr["Description"].ToString();
                    claModel.ClassContent = dr["ClassContent"].ToString();
                    claModel.ClassConfig = dr["ClassConfig"].ToString();
                    claModel.ParentID = dr["ParentID"].ToString();
                    claModel.ChildNum = dr["ChildNum"].ToString();
                    claModel.ListID = dr["ListID"].ToString();
                    claModel.AdminID = dr["AdminID"].ToString();
                    claModel.AddTime = dr["AddTime"].ToString();
                    claModel.IsClose = dr["IsClose"].ToString();
                    return claModel;
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
        public ClassModel GetInfo2(string strClassID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Class where IsClose=0 and ClassID=@ClassID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ClassID",strClassID)};
            ClassModel claModel = new ClassModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    claModel.ConfigID = dr["ConfigID"].ToString();
                    claModel.ClassID = dr["ClassID"].ToString();
                    claModel.ClassName = dr["ClassName"].ToString();
                    claModel.ClassEnName = dr["ClassEnName"].ToString();
                    claModel.ClassPropertyID = dr["ClassPropertyID"].ToString();
                    claModel.ClassTemplateID = dr["ClassTemplateID"].ToString();
                    claModel.ClassPic = dr["ClassPic"].ToString();
                    claModel.LinkUrl = dr["LinkUrl"].ToString();
                    claModel.Target = dr["Target"].ToString();
                    claModel.IsGoToFirst = dr["IsGoToFirst"].ToString();
                    claModel.IsShowNav = dr["IsShowNav"].ToString();
                    claModel.Keywords = dr["Keywords"].ToString();
                    claModel.Description = dr["Description"].ToString();
                    claModel.ClassContent = dr["ClassContent"].ToString();
                    claModel.ClassConfig = dr["ClassConfig"].ToString();
                    claModel.ParentID = dr["ParentID"].ToString();
                    claModel.ChildNum = dr["ChildNum"].ToString();
                    claModel.ListID = dr["ListID"].ToString();
                    claModel.AdminID = dr["AdminID"].ToString();
                    claModel.AddTime = dr["AddTime"].ToString();
                    claModel.IsClose = dr["IsClose"].ToString();
                    return claModel;
                }
                else
                {
                    return null;
                }
            }
        }

        public IList<ClassModel> GetInfoListByParentID(string strParentID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Class where IsClose=0 and ParentID=@ParentID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ParentID",strParentID)};
            IList<ClassModel> claLists = new List<ClassModel>();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                while (dr.Read())
                {
                    ClassModel claModel=new ClassModel();
                    claModel.ConfigID = dr["ConfigID"].ToString();
                    claModel.ClassID = dr["ClassID"].ToString();
                    claModel.ClassName = dr["ClassName"].ToString();
                    claModel.ClassEnName = dr["ClassEnName"].ToString();
                    claModel.ClassPropertyID = dr["ClassPropertyID"].ToString();
                    claModel.ClassTemplateID = dr["ClassTemplateID"].ToString();
                    claModel.ClassPic = dr["ClassPic"].ToString();
                    claModel.LinkUrl = dr["LinkUrl"].ToString();
                    claModel.Target = dr["Target"].ToString();
                    claModel.IsGoToFirst = dr["IsGoToFirst"].ToString();
                    claModel.IsShowNav = dr["IsShowNav"].ToString();
                    claModel.Keywords = dr["Keywords"].ToString();
                    claModel.Description = dr["Description"].ToString();
                    claModel.ClassContent = dr["ClassContent"].ToString();
                    claModel.ClassConfig = dr["ClassConfig"].ToString();
                    claModel.ParentID = dr["ParentID"].ToString();
                    claModel.ChildNum = dr["ChildNum"].ToString();
                    claModel.ListID = dr["ListID"].ToString();
                    claModel.AdminID = dr["AdminID"].ToString();
                    claModel.AddTime = dr["AddTime"].ToString();
                    claModel.IsClose = dr["IsClose"].ToString();
                    claLists.Add(claModel);
                }
                if (claLists.Count > 0)
                {
                    return claLists;
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
        public void InsertInfo(ClassModel claModel)
        {
            StringBuilder sql = new StringBuilder("insert into");
            sql.Append(" t_Class(ConfigID,ClassName,ClassEnName,ClassPropertyID,ClassTemplateID,ClassPic,LinkUrl,Target,IsGoToFirst,IsShowNav,Keywords,Description,ClassContent,ClassConfig,ParentID,ChildNum,ListID,AdminID,AddTime,IsClose)");
            sql.Append(" values(@ConfigID,@ClassName,@ClassEnName,@ClassPropertyID,@ClassTemplateID,@ClassPic,@LinkUrl,@Target,@IsGoToFirst,@IsShowNav,@Keywords,@Description,@ClassContent,@ClassConfig,@ParentID,@ChildNum,@ListID,@AdminID,@AddTime,@IsClose)");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ConfigID",claModel.ConfigID),
Config.Conn().CreateDbParameter("@ClassName",claModel.ClassName),
Config.Conn().CreateDbParameter("@ClassEnName",claModel.ClassEnName),
Config.Conn().CreateDbParameter("@ClassPropertyID",claModel.ClassPropertyID),
Config.Conn().CreateDbParameter("@ClassTemplateID",claModel.ClassTemplateID),
Config.Conn().CreateDbParameter("@ClassPic",claModel.ClassPic),
Config.Conn().CreateDbParameter("@LinkUrl",claModel.LinkUrl),
Config.Conn().CreateDbParameter("@Target",claModel.Target),
Config.Conn().CreateDbParameter("@IsGoToFirst",claModel.IsGoToFirst),
Config.Conn().CreateDbParameter("@IsShowNav",claModel.IsShowNav),
Config.Conn().CreateDbParameter("@Keywords",claModel.Keywords),
Config.Conn().CreateDbParameter("@Description",claModel.Description),
Config.Conn().CreateDbParameter("@ClassContent",claModel.ClassContent),
Config.Conn().CreateDbParameter("@ClassConfig",claModel.ClassConfig),
Config.Conn().CreateDbParameter("@ParentID",claModel.ParentID),
Config.Conn().CreateDbParameter("@ChildNum",claModel.ChildNum),
Config.Conn().CreateDbParameter("@ListID",claModel.ListID),
Config.Conn().CreateDbParameter("@AdminID",claModel.AdminID),
Config.Conn().CreateDbParameter("@AddTime",claModel.AddTime),
Config.Conn().CreateDbParameter("@IsClose",claModel.IsClose)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void UpdateInfo(ClassModel claModel, string strClassID)
        {
            StringBuilder sql = new StringBuilder("update t_Class set ");
            sql.Append(" ConfigID=@ConfigID,");
            sql.Append(" ClassName=@ClassName,");
            sql.Append(" ClassEnName=@ClassEnName,");
            sql.Append(" ClassPropertyID=@ClassPropertyID,");
            sql.Append(" ClassTemplateID=@ClassTemplateID,");
            sql.Append(" ClassPic=@ClassPic,");
            sql.Append(" LinkUrl=@LinkUrl,");
            sql.Append(" Target=@Target,");
            sql.Append(" IsGoToFirst=@IsGoToFirst,");
            sql.Append(" IsShowNav=@IsShowNav,");
            sql.Append(" Keywords=@Keywords,");
            sql.Append(" Description=@Description,");
            sql.Append(" ClassContent=@ClassContent,");
            sql.Append(" ClassConfig=@ClassConfig,");
            //sql.Append(" ParentID=@ParentID,");
            //sql.Append(" ChildNum=@ChildNum,");
            sql.Append(" ListID=@ListID,");
            //sql.Append(" AdminID=@AdminID,");
            //sql.Append(" AddTime=@AddTime,");
            sql.Append(" IsClose=@IsClose");
            sql.Append(" where  ClassID=@ClassID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ConfigID",claModel.ConfigID),
Config.Conn().CreateDbParameter("@ClassName",claModel.ClassName),
Config.Conn().CreateDbParameter("@ClassEnName",claModel.ClassEnName),
Config.Conn().CreateDbParameter("@ClassPropertyID",claModel.ClassPropertyID),
Config.Conn().CreateDbParameter("@ClassTemplateID",claModel.ClassTemplateID),
Config.Conn().CreateDbParameter("@ClassPic",claModel.ClassPic),
Config.Conn().CreateDbParameter("@LinkUrl",claModel.LinkUrl),
Config.Conn().CreateDbParameter("@Target",claModel.Target),
Config.Conn().CreateDbParameter("@IsGoToFirst",claModel.IsGoToFirst),
Config.Conn().CreateDbParameter("@IsShowNav",claModel.IsShowNav),
Config.Conn().CreateDbParameter("@Keywords",claModel.Keywords),
Config.Conn().CreateDbParameter("@Description",claModel.Description),
Config.Conn().CreateDbParameter("@ClassContent",claModel.ClassContent),
Config.Conn().CreateDbParameter("@ClassConfig",claModel.ClassConfig),
//Config.Conn().CreateDbParameter("@ParentID",claModel.ParentID),
//Config.Conn().CreateDbParameter("@ChildNum",claModel.ChildNum),
Config.Conn().CreateDbParameter("@ListID",claModel.ListID),
//Config.Conn().CreateDbParameter("@AdminID",claModel.AdminID),
//Config.Conn().CreateDbParameter("@AddTime",claModel.AddTime),
Config.Conn().CreateDbParameter("@IsClose",claModel.IsClose),
Config.Conn().CreateDbParameter("@ClassID",strClassID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region ɾ����Ϣ
        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        public void DeleteInfo(string strClassID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_Class where ClassID=@ClassID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ClassID",strClassID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region ����״̬
        /// <summary>
        /// ����״̬
        /// </summary>
        public void UpdateCloseStatus(string strClassID, string strIsClose)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_Class set IsClose=@IsClose where ClassID=@ClassID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@IsClose",strIsClose),
            Config.Conn().CreateDbParameter("@ClassID",strClassID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region �ƶ���Ϣ
        /// <summary>
        /// �ƶ���Ϣ
        /// </summary>
        public void MoveInfo(ClassModel claModel, string strClassID)
        {
            StringBuilder sql = new StringBuilder("update t_Class set ");
            sql.Append(" ParentID=@ParentID,");
            sql.Append(" ListID=@ListID");
            sql.Append(" where ClassID=@ClassID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ParentID",claModel.ParentID),
            Config.Conn().CreateDbParameter("@ListID",claModel.ListID),
            Config.Conn().CreateDbParameter("@ClassID",strClassID)};
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
                sql.Append("select ListID from t_Class where ParentID=@ParentID order by ListID desc limit 0,1");
            }
            else
            {
                sql.Append("select top 1 ListID from t_Class where ParentID=@ParentID order by ListID desc");
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
                    sql.Append("create table tmp as select ClassID from t_Class where ParentID=@ParentID and ListID<=@ListID and ListID>@OldListID;");
                    sql.Append("update t_Class set ListID=ListID-1 where ClassID in(select ClassID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_Class set ListID=ListID-1 where ClassID in(select ClassID from t_Class where ParentID=@ParentID and ListID<=@ListID and ListID>@OldListID)");
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
                    sql.Append("create table tmp as select ClassID from t_Class where ParentID=@ParentID and ListID>=@ListID and ListID<@OldListID;");
                    sql.Append("update t_Class set ListID=ListID+1 where ClassID in(select ClassID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_Class set ListID=ListID+1 where ClassID in(select ClassID from t_Class where ParentID=@ParentID and ListID>=@ListID and ListID<@OldListID)");
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
        public void AddChildNum(string strClassID)
        {
            StringBuilder sql = new StringBuilder("update t_Class set ");
            sql.Append(" ChildNum=ChildNum+1");
            sql.Append(" where ClassID=@ClassID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ClassID",strClassID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region �����Ӽ���
        /// <summary>
        /// �����Ӽ���
        /// </summary>
        public void CutChildNum(string strClassID)
        {
            StringBuilder sql = new StringBuilder("update t_Class set ");
            sql.Append(" ChildNum=ChildNum-1");
            sql.Append(" where ClassID=@ClassID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ClassID",strClassID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region ��ʾ���������б�
        /// <summary>
        /// ��ʾ���������б�
        /// </summary>
        public void ShowSelectTree(string strParentID, int intLevel, DropDownList drp, string strSql, string strClassPropertyID)
        {
            StringBuilder tempStr = new StringBuilder();
            StringBuilder tempHR = new StringBuilder();
            for (int i = 0; i <= intLevel; i++)
            {
                tempHR.Append("��");
            }
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Class where ParentID=" + strParentID + strSql + " order by ListID asc");
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), null))
            {
                while (dr.Read())
                {
                    if (dr["ClassPropertyID"].ToString() == strClassPropertyID || strClassPropertyID.IndexOf("," + dr["ClassPropertyID"].ToString() + ",") > -1 || strClassPropertyID == "-1")
                    {
                        ClassModel claModel = GetInfo(dr["ParentID"].ToString());
                        if (claModel != null)
                        {
                            if (claModel.ClassPropertyID != dr["ClassPropertyID"].ToString() && !Config.DropdownListIsExistItem(drp, dr["ParentID"].ToString()))
                            {
                                StringBuilder tempHR_1 = new StringBuilder();
                                for (int i = 0; i < intLevel; i++)
                                {
                                    tempHR_1.Append("��");
                                }
                                drp.Items.Add(new ListItem(tempHR_1 + "��" + claModel.ClassName, dr["ParentID"].ToString()));
                            }
                        }
                        drp.Items.Add(new ListItem(tempHR + "��" + dr["ClassName"].ToString(), dr["ClassID"].ToString()));
                    }
                    if (Convert.ToInt32(dr["ChildNum"]) > 0)
                    {
                        ShowSelectTree(dr["ClassID"].ToString(), intLevel + 1, drp, strSql, strClassPropertyID);
                    }
                }
            }
        }
        #endregion

        #region ȡ��һ�����ID
        /// <summary>
        /// ȡ��һ�����ID
        /// </summary>
        public string GetFirstClassID(string strParentID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select ClassID,ChildNum from t_Class where IsClose=0 and ParentID=@ParentID order by ListID asc");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ParentID",strParentID)};
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    string TempChildNum = dr["ChildNum"].ToString();
                    string TempClassID = dr["ClassID"].ToString();
                    if (Convert.ToInt32(TempChildNum) > 0)
                    {
                        return GetFirstClassID(TempClassID);
                    }
                    else
                    {
                        return TempClassID;
                    }
                }
                else
                {
                    return strParentID;
                }
            }
        }
        #endregion

        #region ȡ����ĿID
        /// <summary>
        /// ȡ����ĿID
        /// </summary>
        public StringBuilder GetSubClassID(string strParentID)
        {
            StringBuilder tempStr = new StringBuilder();
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Class where ParentID=" + strParentID + " order by ListID asc");
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), null))
            {
                while (dr.Read())
                {
                    tempStr.Append(dr["ClassID"].ToString() + ",");
                    if (Convert.ToInt32(dr["ChildNum"]) > 0)
                    {
                        tempStr.Append(GetSubClassID(dr["ClassID"].ToString()));
                    }
                }
            }
            return tempStr;
        }
        #endregion

        #region ȡ·��
        /// <summary>
        /// ȡ·��
        /// </summary>
        public StringBuilder GetPath(string strClassID)
        {
            StringBuilder strTemp = new StringBuilder();
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Class where ClassID=@ClassID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ClassID",strClassID)};
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    string TempParentID = dr["ParentID"].ToString();

                    if (Convert.ToInt32(TempParentID) != 0)
                    {
                        strTemp.Append(GetPath(TempParentID) + "," + strClassID);
                        return strTemp;
                    }
                    else
                    {
                        strTemp.Append(strClassID);
                        return strTemp;
                    }
                }
                else
                {
                    strTemp.Append(strClassID);
                    return strTemp;
                }
            }
        }
        #endregion

        #region ȡ��ǰ��Ŀ
        /// <summary>
        /// ȡ��ǰ��Ŀ
        /// </summary>
        public StringBuilder GetClassBlock(string strClassPath, int intDepth)
        {
            StringBuilder strScript = new StringBuilder();
            strScript.Append("<script language=\"javascript\">\n");
            strScript.Append("function ShowClassBlock(){\n");
            strScript.Append("try{\n");
            string[] ArrClassPath = strClassPath.Split(new char[] { ',' });
            for (int i = intDepth; i < ArrClassPath.Length; i++)
            {
                strScript.Append("document.getElementById(\"Class" + ArrClassPath[i] + "\").style.display=\"block\";\n");
            }
            strScript.Append("}\n");
            strScript.Append("catch(e){\n");
            strScript.Append("//do nothing\n");
            strScript.Append("}\n");
            strScript.Append("}\n");
            strScript.Append("ShowClassBlock();\n");
            strScript.Append("</script>\n");
            return strScript;
        }
        #endregion

        #region ȡ��Ŀ�б�
        /// <summary>
        /// ȡ��Ŀ�б�(a)
        /// </summary>
        public StringBuilder GetClassList(string ParentID, string strSeparator, int intShowLen, string strLinkKey)
        {
            int intCount = new AccDAL().GetAllCount("select  count(ClassID) from t_Class where ParentID=" + ParentID + " and IsClose=0", null);
            StringBuilder strTemp = new StringBuilder();
            string sql = "select  * from t_Class where ParentID=" + ParentID + " and IsClose=0 order by ListID asc";
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), null))
            {
                int i = 0;
                while (dr.Read())
                {
                    string strTempLinkUrl, strTempTarget;
                    if (dr["LinkUrl"].ToString() != string.Empty)
                    {
                        strTempLinkUrl = dr["LinkUrl"].ToString();
                        strTempTarget = "target=\"" + dr["Target"].ToString() + "\"";
                    }
                    else
                    {
                        strTempLinkUrl = strLinkKey + dr["ClassEnName"].ToString() + Config.FileExt;
                        strTempTarget = "";
                    }
                    string strTempSeparator;
                    i++;
                    if (i < intCount)
                    {
                        strTempSeparator = strSeparator;
                    }
                    else
                    {
                        strTempSeparator = "";
                    }
                    strTemp.Append("<a href=\"" + strTempLinkUrl + "\" " + strTempTarget + " title=\"" + dr["ClassName"].ToString() + "\">" + Config.ShowPartStr(dr["ClassName"].ToString(), intShowLen) + "</a>");
                    strTemp.Append(strTempSeparator);
                }
            }
            return strTemp;
        }
        /// <summary>
        /// ȡ��Ŀ�б�(li)
        /// </summary>
        public StringBuilder GetClassList(string ParentID, string strStyleClass, string ClassID, int intShowLen, string strLinkKey)
        {
            StringBuilder strTemp = new StringBuilder();
            strTemp.Append("<ul>\n");
            string sql = "select  * from t_Class where ParentID=" + ParentID + " and IsClose=0 order by ListID asc";
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), null))
            {
                while (dr.Read())
                {
                    string strTempLinkUrl, strTempTarget;
                    if (dr["LinkUrl"].ToString() != string.Empty)
                    {
                        strTempLinkUrl = dr["LinkUrl"].ToString();
                        strTempTarget = "target=\"" + dr["Target"].ToString() + "\"";
                    }
                    else
                    {
                        strTempLinkUrl = strLinkKey + dr["ClassEnName"].ToString() + Config.FileExt;
                        strTempTarget = "";
                    }
                    string strTempStyleClass;
                    if (ClassID == dr["ClassID"].ToString())
                    {
                        strTempStyleClass = strStyleClass;
                    }
                    else
                    {
                        strTempStyleClass = "";
                    }
                    strTemp.Append("<li>");
                    strTemp.Append("<a href=\"" + strTempLinkUrl + "\" " + strTempTarget + " title=\"" + dr["ClassName"].ToString() + "\" class=\"" + strTempStyleClass + "\">" + Config.ShowPartStr(dr["ClassName"].ToString(), intShowLen) + "</a>");
                    strTemp.Append("</li>\n");
                }
            }
            strTemp.Append("</ul>\n");
            return strTemp;
        }
        /// <summary>
        /// ȡ��Ŀ�б�(�ݹ�)
        /// </summary>
        public StringBuilder GetClassList(string ParentID, string strStyleClass, string strClassPath, int i, int intShowLen, string strLinkKey)
        {
            StringBuilder strTemp = new StringBuilder();
            strTemp.Append("<ul id=\"Class" + ParentID + "\" style=\"display:none\">\n");
            string sql = "select  * from t_Class where ParentID=" + ParentID + " and IsClose=0 order by ListID asc";
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), null))
            {
                while (dr.Read())
                {
                    string strTempLinkUrl, strTempTarget;
                    if (dr["LinkUrl"].ToString() != string.Empty)
                    {
                        strTempLinkUrl = dr["LinkUrl"].ToString();
                        strTempTarget = "target=\"" + dr["Target"].ToString() + "\"";
                    }
                    else
                    {
                        strTempLinkUrl = strLinkKey + dr["ClassEnName"].ToString() + Config.FileExt;
                        strTempTarget = "";
                    }
                    string strTempStyleClass;
                    strClassPath = "," + strClassPath + ",";
                    if (strClassPath.IndexOf("," + dr["ClassID"].ToString() + ",") > -1)
                    {
                        strTempStyleClass = strStyleClass;
                    }
                    else
                    {
                        strTempStyleClass = "";
                    }
                    strTemp.Append("<li>");
                    strTemp.Append("<a href=\"" + strTempLinkUrl + "\" " + strTempTarget + " title=\"" + dr["ClassName"].ToString() + "\" class=\"" + strTempStyleClass + "\">" + Config.ShowPartStr(dr["ClassName"].ToString(), intShowLen) + "</a>");
                    if (Convert.ToInt32(dr["ChildNum"]) > 0)
                    {
                        strTemp.Append(GetClassList(dr["ClassID"].ToString(), strStyleClass, strClassPath, i + 1, intShowLen, strLinkKey));
                    }
                    strTemp.Append("</li>\n");
                }
            }
            strTemp.Append("</ul>\n");
            return strTemp;
        }
        #endregion

        #region ������ĿӢ����ȡ��ĿID
        /// <summary>
        /// ������ĿӢ����ȡ��ĿID
        /// </summary>
        public string GetClassIDByClassEnName(string strConfigID, string strClassEnName)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select ClassID from t_Class where IsClose=0 and ConfigID=@ConfigID and ClassEnName=@ClassEnName");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ConfigID",strConfigID),
            Config.Conn().CreateDbParameter("@ClassEnName",strClassEnName)};
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    return dr["ClassID"].ToString();
                }
                else
                {
                    return "-1";
                }
            }
        }
        #endregion

        #region ������ĿIDȡ��ĿӢ����
        /// <summary>
        /// ������ĿIDȡ��ĿӢ����
        /// </summary>
        public string GetClassEnNameByClassID(string strClassID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select ClassEnName from t_Class where ClassID=@ClassID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ClassID",strClassID)};
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    return dr["ClassEnName"].ToString();
                }
                else
                {
                    return "";
                }
            }
        }
        #endregion

        #region ȡ�������ID
        /// <summary>
        /// ȡ�������ID
        /// </summary>
        public string GetTopClassID(string strClassID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select ClassID,ParentID from t_Class where IsClose=0 and ClassID=@ClassID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ClassID",strClassID)};
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    string TempParentID = dr["ParentID"].ToString();
                    string TempClassID = dr["ClassID"].ToString();
                    if (Convert.ToInt32(TempParentID) > 0)
                    {
                        return GetTopClassID(TempParentID);
                    }
                    else
                    {
                        return TempClassID;
                    }
                }
                else
                {
                    return strClassID;
                }
            }
        }
        #endregion

        #region ȡ�ֶ�ֵ
        /// <summary>
        /// ȡ�ֶ�ֵ
        /// </summary>
        public string GetValueByField(string strFieldName, string strClassID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select " + strFieldName + " from t_Class where ClassID=@ClassID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ClassID",strClassID)};
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
        /// <returns></returns>
        public StringBuilder AjaxGetClassList(string strParentID, string strType, string strObjName)
        {
            StringBuilder strItem = new StringBuilder();
            string sql = "select ClassID,ClassName from t_Class where IsClose=0 and ParentID=" + strParentID + " order by ListID asc";
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

        #region ��������ĿID��SQL
        /// <summary>
        /// ��������ĿID��SQL
        /// </summary>
        public string GetSubClassSql(string strClassID)
        {
            string strSql;
            if (Config.DatabaseType == Config.DatabaseTypeCollection.Sql.ToString())
            {
                strSql = "select id from f_GetChildClassID(" + strClassID + ")";
            }
            else
            {
                strSql = "-1," + GetSubClassID(strClassID).ToString() + "-1";
            }
            return strSql;
        }
        #endregion

        #region ͬ����������Ŀ����
        /// <summary>
        /// ͬ����������Ŀ����
        /// </summary>
        public void UpdateSubClassConfig(string strClassID, string strClassConfig, string strClassPropertyID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update t_Class set ClassConfig=@ClassConfig where ClassPropertyID=@ClassPropertyID and ClassID in(" + GetSubClassSql(strClassID) + ")");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ClassConfig",strClassConfig),
            Config.Conn().CreateDbParameter("@ClassPropertyID",strClassPropertyID),
            Config.Conn().CreateDbParameter("@ClassID",strClassID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region ������Ŀ·��ȡClassID
        /// <summary>
        /// ������Ŀ·��ȡClassID
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public string GetClassIDByPath(string strClassPath, int index, string strClassID)
        {
            string[] ClassPathArr = strClassPath.Split(new char[] { ',' });
            if (ClassPathArr.Length > index + 1)
            {
                return ClassPathArr[index];
            }
            else
            {
                return strClassID;
            }
        }
        #endregion

    }
}
