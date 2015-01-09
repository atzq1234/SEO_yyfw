using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HxSoft.Common;
using System.IO;

namespace HxSoft.Web.Common
{
    public partial class Export : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            GridView_Bind();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 9999;

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "utf-8";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(System.DateTime.Now.ToString("yyyyMMddhhmmss")) + ".xls");

            Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            Response.ContentType = "application/ms-excel";
            Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=utf-8\">");
            EnableViewState = false;

            GridView_Bind();

            System.IO.StringWriter oStringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter ohtmltextwriter = new HtmlTextWriter(oStringWrite);

            GridView1.RenderControl(ohtmltextwriter);

            Response.Write(oStringWrite.ToString());
            Response.End();
        }

        public void GridView_Bind()
        {
            DataSql dat = new DataSql();
            dat.ConnStr = "Data Source=" + txtServer.Text + ";Initial Catalog=" + txtDb.Text + ";User ID=" + txtUser.Text + ";Password=" + txtPass.Text + ";";
            DataSet ds = dat.GetDataSet(CommandType.Text, txtSql.Text, null);
            DataTable dt = ds.Tables[0];
            BindHelper.DataBind(dt, Config.DataBindObjTypeCollection.GridView.ToString(), GridView1);
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }
    }
}