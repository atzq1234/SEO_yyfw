using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using HxSoft.Common;

namespace HxSoft.Web.Common
{
    public partial class DataBackup : System.Web.UI.Page
    {
        DataSql dat = new DataSql(Config.SqlConnStr);
        public string act
        {
            get
            {
                return Config.Request(Request.QueryString["act"], "");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (act.Equals("s"))
            {
                Backup();
            }
        }

        private void Backup()
        {
            string sql_name = "Select Name From Master..SysDataBases Where DbId=(Select Dbid From Master..SysProcesses Where Spid = @@spid)";
            using (SqlDataReader dr = dat.GetDataReader(CommandType.Text, sql_name, null))
            {
                if (dr.Read())
                {
                    string strDatabaseName = dr[0].ToString();
                    string strBackupPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + "App_Data\\back_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".bak";
                    string strBackupSql = "BACKUP DATABASE " + strDatabaseName + " TO DISK ='" + strBackupPath + "'   WITH INIT, NOUNLOAD, NAME=N'" + strDatabaseName + " Full backup', SKIP, STATS = 10, NOFORMAT";
                    try
                    {
                        dat.ExecuteSql(CommandType.Text, strBackupSql, null);
                        Config.ShowEnd("备份成功!");
                    }
                    catch (Exception ex)
                    {
                        Config.ShowEnd(ex.Message);
                    }
                }
            }
        }
    }
}
