using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HxSoft.Common;

namespace HxSoft.Web.Admin.Part
{
    public partial class thumbnail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"] as string;
            if (id == null)
            {
                Response.StatusCode = 404;
                Response.Write("Not Found");
                Response.End();
                return;
            }
            if (ListThumbnail.ListThum == null)
            {
                Response.StatusCode = 404;
                Response.Write("Not Found");
                Response.End();
                return;
            }
            foreach (Thumbnail thumb in ListThumbnail.ListThum)
            {
                if (thumb.Name == id)
                {
                    Response.ContentType = "image/jpeg";
                    Response.BinaryWrite(thumb.Data);
                    Response.End();
                    return;
                }
            }

            //如果没有得到ID的值，则报404错误
            Response.StatusCode = 404;
            Response.Write("Not Found");
            Response.End();
        }

    }
}