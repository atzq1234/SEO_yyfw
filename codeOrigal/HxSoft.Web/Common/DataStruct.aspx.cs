using System;
using System.Data;
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
using HxSoft.ClassFactory;

namespace HxSoft.Web.Common
{
    public partial class DataStruct : System.Web.UI.Page
    {
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
                Table_List();
            }
        }


        //±í
        private void Table_List()
        {
            string sql = "select * from sysobjects where xtype='U' order by [name] asc";
            Factory.Acc().DataBind(sql.ToString(), null, Config.DataBindObjTypeCollection.Repeater.ToString(), repNav);
            Factory.Acc().DataBind(sql.ToString(), null, Config.DataBindObjTypeCollection.Repeater.ToString(), repTable);
        }


        //ÁÐ
        protected void repTable_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater repCol = (Repeater)e.Item.FindControl("repCol");
            DataRowView drv = (DataRowView)e.Item.DataItem;
            string strTableName = drv["Name"].ToString();
            StringBuilder sql = new StringBuilder("SELECT ");
            sql.Append("[TableName] = D.NAME,");
            sql.Append("[ColumnSort] = A.COLORDER,");
            sql.Append("[FieldName] = A.NAME,");
            sql.Append("[IsIdentity] = CASE WHEN COLUMNPROPERTY(A.ID,A.NAME, 'ISIDENTITY ')=1 THEN '¡Ì' ELSE ' ' END,");
            sql.Append("[IsPrimaryKey] = CASE WHEN EXISTS (SELECT 1 FROM SYSOBJECTS WHERE XTYPE = 'PK ' AND  PARENT_OBJ = A.ID AND ");
            sql.Append("NAME IN (SELECT NAME FROM SYSINDEXES WHERE ");
            sql.Append("INDID IN (SELECT INDID FROM SYSINDEXKEYS WHERE ID = A.ID AND  COLID = A.COLID) ");
            sql.Append(")) THEN '¡Ì' ELSE ' ' END,");
            sql.Append("[DataType] = B.NAME,");
            sql.Append("[Length] = A.LENGTH,");
            sql.Append("[DecimalDigit] = ISNULL(COLUMNPROPERTY(A.ID,A.NAME, 'SCALE '),0),");
            sql.Append("[IsNull] = CASE WHEN A.ISNULLABLE=1 THEN '¡Ì' ELSE ' ' END,");
            sql.Append("[DefaultValue] = ISNULL(E.TEXT, ' '),");
            sql.Append("[ColumnDescription] = ISNULL(G.[VALUE], ' ') ");
            sql.Append("FROM SYSCOLUMNS A ");
            sql.Append("LEFT JOIN SYSTYPES B ON A.XUSERTYPE = B.XUSERTYPE ");
            sql.Append("INNER JOIN SYSOBJECTS D ON A.ID = D.ID AND D.XTYPE = 'U ' AND D.NAME = '" + strTableName + "'");
            sql.Append("LEFT JOIN SYSCOMMENTS E ON A.CDEFAULT = E.ID ");
            sql.Append("LEFT JOIN sys.extended_properties G ON A.ID = G.major_id AND A.COLID = G.minor_id ");
            sql.Append("LEFT JOIN sys.extended_properties F ON D.ID = F.major_id AND F.minor_id = 0 ");
            sql.Append("ORDER BY D.NAME,A.COLORDER");
            Factory.Acc().DataBind( sql.ToString(),null, Config.DataBindObjTypeCollection.Repeater.ToString(), repCol);
        }
    }
}
