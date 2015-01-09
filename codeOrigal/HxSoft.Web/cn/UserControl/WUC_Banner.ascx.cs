using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using HxSoft.Common;
using HxSoft.Model;
using HxSoft.ClassFactory;

namespace HxSoft.Web.cn.UserControl
{
    public partial class WUC_Banner : System.Web.UI.UserControl
    {
        public string AdPositionID { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IList<AdModel> list = Factory.Ad().GetInfoList(AdPositionID);
                if (list!=null)
                {
                    rptlist.DataSource = list;
                    rptlist.DataBind();
                }
            }
        }
    }
}