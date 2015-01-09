using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using HxSoft.Common;
using HxSoft.Model;
using HxSoft.ClassFactory;
using Brettle.Web.NeatUpload;

namespace HxSoft.Web.Admin.Upload
{
    public partial class File_BatchUpload_Dialog : System.Web.UI.Page
    {

        public string strFolderPath
        {
            get
            {
                return Config.FolderNameReplace(Config.Request(Request["FolderPath"], Config.FileUploadPath));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

    }
}