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
using HxSoft.Common;

namespace HxSoft.Web.Common
{
    public partial class VerifyCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            VryImgGen gen = new VryImgGen();
            string verifyCode = gen.CreateVerifyCode(4, 0);
            Session["VerifyCode"] = verifyCode.ToUpper();
            System.Drawing.Bitmap bitmap = gen.CreateImage(verifyCode.ToUpper());
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            Response.Clear();
            Response.ContentType = "image/Png";
            Response.BinaryWrite(ms.GetBuffer());
            bitmap.Dispose();
            ms.Close();
            Response.End();
        }
    }
}
