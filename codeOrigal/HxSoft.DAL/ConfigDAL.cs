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
    ///��վ����-���ݷ�����
    /// ������:��С��
    /// ����:2010-12-6
    /// </summary>
    public class ConfigDAL
    {

        #region �����Ϣ,����ĳ�ֶε�Ψһ��
        /// <summary>
        /// �����Ϣ,����ĳ�ֶε�Ψһ��
        /// </summary>
        public bool CheckInfo(string strFieldName, string strFieldValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Config where " + strFieldName + "=@" + strFieldName + "");
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

        public bool CheckInfo(string strFieldName, string strFieldValue, string strConfigID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Config where " + strFieldName + "=@" + strFieldName + " and ConfigID<>@ConfigID");
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
        #endregion

        #region ��ȡ��Ϣ
        /// <summary>
        /// ��ȡ��Ϣ
        /// </summary>
        public ConfigModel GetInfo(string strConfigID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from t_Config where ConfigID=@ConfigID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ConfigID",strConfigID)};
            ConfigModel confModel = new ConfigModel();
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), cmdParams))
            {
                if (dr.Read())
                {
                    confModel.LanguageVer = dr["LanguageVer"].ToString();
                    confModel.WebsiteName = dr["WebsiteName"].ToString();
                    confModel.WebsiteUrl = dr["WebsiteUrl"].ToString();
                    confModel.WebsiteKeywords = dr["WebsiteKeywords"].ToString();
                    confModel.WebsiteDescription = dr["WebsiteDescription"].ToString();
                    confModel.MailReceiveAddress = dr["MailReceiveAddress"].ToString();
                    confModel.MailSmtpServer = dr["MailSmtpServer"].ToString();
                    confModel.MailUserName = dr["MailUserName"].ToString();
                    confModel.MailPassword = dr["MailPassword"].ToString();
                    confModel.FooterInfo = dr["FooterInfo"].ToString();
                    confModel.ListID = dr["ListID"].ToString();
                    confModel.AdminID = dr["AdminID"].ToString();
                    confModel.AddTime = dr["AddTime"].ToString();
                    confModel.IsClose = dr["IsClose"].ToString();
                    return confModel;
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
        public void InsertInfo(ConfigModel confModel)
        {
            StringBuilder sql = new StringBuilder("insert into");
            sql.Append(" t_Config(LanguageVer,WebsiteName,WebsiteUrl,WebsiteKeywords,WebsiteDescription,MailReceiveAddress,MailSmtpServer,MailUserName,MailPassword,FooterInfo,ListID,AdminID,AddTime,IsClose)");
            sql.Append(" values(@LanguageVer,@WebsiteName,@WebsiteUrl,@WebsiteKeywords,@WebsiteDescription,@MailReceiveAddress,@MailSmtpServer,@MailUserName,@MailPassword,@FooterInfo,@ListID,@AdminID,@AddTime,@IsClose)");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@LanguageVer",confModel.LanguageVer),
            Config.Conn().CreateDbParameter("@WebsiteName",confModel.WebsiteName),
            Config.Conn().CreateDbParameter("@WebsiteUrl",confModel.WebsiteUrl),
            Config.Conn().CreateDbParameter("@WebsiteKeywords",confModel.WebsiteKeywords),
            Config.Conn().CreateDbParameter("@WebsiteDescription",confModel.WebsiteDescription),
            Config.Conn().CreateDbParameter("@MailReceiveAddress",confModel.MailReceiveAddress),
            Config.Conn().CreateDbParameter("@MailSmtpServer",confModel.MailSmtpServer),
            Config.Conn().CreateDbParameter("@MailUserName",confModel.MailUserName),
            Config.Conn().CreateDbParameter("@MailPassword",confModel.MailPassword),
            Config.Conn().CreateDbParameter("@FooterInfo",confModel.FooterInfo),
            Config.Conn().CreateDbParameter("@ListID",confModel.ListID),
            Config.Conn().CreateDbParameter("@AdminID",confModel.AdminID),
            Config.Conn().CreateDbParameter("@AddTime",confModel.AddTime),
            Config.Conn().CreateDbParameter("@IsClose",confModel.IsClose)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void UpdateInfo(ConfigModel confModel, string strConfigID)
        {
            StringBuilder sql = new StringBuilder("update t_Config set ");
            sql.Append(" LanguageVer=@LanguageVer,");
            sql.Append(" WebsiteName=@WebsiteName,");
            sql.Append(" WebsiteUrl=@WebsiteUrl,");
            sql.Append(" WebsiteKeywords=@WebsiteKeywords,");
            sql.Append(" WebsiteDescription=@WebsiteDescription,");
            sql.Append(" MailReceiveAddress=@MailReceiveAddress,");
            sql.Append(" MailSmtpServer=@MailSmtpServer,");
            sql.Append(" MailUserName=@MailUserName,");
            sql.Append(" MailPassword=@MailPassword,");
            sql.Append(" FooterInfo=@FooterInfo,");
            sql.Append(" ListID=@ListID,");
            //sql.Append(" AdminID=@AdminID,");
            //sql.Append(" AddTime=@AddTime,");
            sql.Append(" IsClose=@IsClose");
            sql.Append(" where ConfigID=@ConfigID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@LanguageVer",confModel.LanguageVer),
            Config.Conn().CreateDbParameter("@WebsiteName",confModel.WebsiteName),
            Config.Conn().CreateDbParameter("@WebsiteUrl",confModel.WebsiteUrl),
            Config.Conn().CreateDbParameter("@WebsiteKeywords",confModel.WebsiteKeywords),
            Config.Conn().CreateDbParameter("@WebsiteDescription",confModel.WebsiteDescription),
            Config.Conn().CreateDbParameter("@MailReceiveAddress",confModel.MailReceiveAddress),
            Config.Conn().CreateDbParameter("@MailSmtpServer",confModel.MailSmtpServer),
            Config.Conn().CreateDbParameter("@MailUserName",confModel.MailUserName),
            Config.Conn().CreateDbParameter("@MailPassword",confModel.MailPassword),
            Config.Conn().CreateDbParameter("@FooterInfo",confModel.FooterInfo),
            Config.Conn().CreateDbParameter("@ListID",confModel.ListID),
            //Config.Conn().CreateDbParameter("@AdminID",confModel.AdminID),
            //Config.Conn().CreateDbParameter("@AddTime",confModel.AddTime),
            Config.Conn().CreateDbParameter("@IsClose",confModel.IsClose),
            Config.Conn().CreateDbParameter("@ConfigID",strConfigID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region ɾ����Ϣ
        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        public void DeleteInfo(string strConfigID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from t_Config where ConfigID=@ConfigID");
            DbParameter[] cmdParams = {
            Config.Conn().CreateDbParameter("@ConfigID",strConfigID)};
            Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
        }
        #endregion

        #region ȡ�ֶ�ֵ
        /// <summary>
        /// ȡ�ֶ�ֵ
        /// </summary>
        public string GetValueByField(string strFieldName, string strConfigID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select " + strFieldName + " from t_Config where ConfigID=@ConfigID");
            DbParameter[] cmdParams = {
Config.Conn().CreateDbParameter("@ConfigID",strConfigID)};
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

        #region ȡվ���б�
        /// <summary>
        /// ȡվ���б�(a)
        /// </summary>
        public StringBuilder GetConfigList(string strSeparator)
        {
            int intCount = new AccDAL().GetAllCount("select  count(ConfigID) from t_Config where IsClose=0", null);
            StringBuilder strTemp = new StringBuilder();
            string sql = "select  * from t_Config where IsClose=0 order by ListID asc";
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), null))
            {
                int i = 0;
                while (dr.Read())
                {
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
                    strTemp.Append("<a href=\"" + dr["WebsiteUrl"].ToString() + "\" title=\"" + dr["LanguageVer"].ToString() + "\">" + dr["LanguageVer"].ToString() + "</a>");
                    strTemp.Append(strTempSeparator);
                }
            }
            return strTemp;
        }
        #endregion

        #region ��ȡ�����
        /// <summary>
        /// ��ȡ�����
        /// </summary>
        public string GetListID()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select max(ListID) as ListID from t_Config");

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

        #region ������Ϣ
        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <returns></returns>
        public void OrderInfo(string strListID, string strOldListID)
        {
            if (Convert.ToInt32(strListID) > Convert.ToInt32(strOldListID))
            {
                StringBuilder sql = new StringBuilder();
                if (Config.DatabaseType == Config.DatabaseTypeCollection.MySql.ToString())
                {
                    sql.Append("create table tmp as select ConfigID from t_Config where ListID<=@ListID and ListID>@OldListID;");
                    sql.Append("update t_Config set ListID=ListID-1 where ConfigID in(select ConfigID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_Config set ListID=ListID-1 where ConfigID in(select ConfigID from t_Config where  ListID<=@ListID and ListID>@OldListID)");
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
                    sql.Append("create table tmp as select ConfigID from t_Config where ListID>=@ListID and ListID<@OldListID;");
                    sql.Append("update t_Config set ListID=ListID+1 where ConfigID in(select ConfigID from tmp);");
                    sql.Append("drop table tmp;");
                }
                else
                {
                    sql.Append("update t_Config set ListID=ListID+1 where ConfigID in(select ConfigID from t_Config where  ListID>=@ListID and ListID<@OldListID)");
                }
                DbParameter[] cmdParams = {
                Config.Conn().CreateDbParameter("@ListID",strListID),
                Config.Conn().CreateDbParameter("@OldListID",strOldListID)};
                Config.Conn().ExecuteSql(CommandType.Text, sql.ToString(), cmdParams);
            }
        }
        #endregion

        #region ȡĬ��վ��
        /// <summary>
        /// ȡĬ��վ��
        /// </summary>
        public string GetDefaultSiteDir()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select WebsiteUrl from t_Config where IsClose=0 order by ListID asc");
            using (DbDataReader dr = Config.Conn().GetDataReader(CommandType.Text, sql.ToString(), null))
            {
                if (dr.Read())
                {
                    return dr["WebsiteUrl"].ToString();
                }
                else
                {
                    return "/cn/";
                }
            }
        }
        #endregion

    }
}
