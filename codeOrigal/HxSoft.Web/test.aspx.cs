using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HxSoft.Common;

namespace HxSoft.Web
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            string strType = drpType.SelectedValue;
            switch (strType)
            {
                case "date":
                    errMsg.Text = Config.IsDate(txtData.Text).ToString();
                    break;
                case "number":
                    errMsg.Text = Config.IsNumeric(txtData.Text).ToString();
                    break;
                default:
                    break;
            }
        }
    }
}