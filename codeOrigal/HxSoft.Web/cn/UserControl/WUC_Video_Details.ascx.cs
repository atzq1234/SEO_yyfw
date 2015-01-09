using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using HxSoft.Common;
using System.Data;
using HxSoft.Model;
using HxSoft.ClassFactory;
using System.Data.Common;

namespace HxSoft.Web.cn.UserControl
{
    public partial class WUC_Video_Details : System.Web.UI.UserControl
    {

        /// <summary>
        /// 信息ID(只读)
        /// </summary>
        public string VideoID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["VideoID"], -1).ToString();
            }
        }
        public VideoModel viModel
        {
            get
            {
                return Factory.Video().GetCacheInfo2(VideoID);
            }
        }
        /// <summary>
        /// 分类ID(只读)
        /// </summary>
        public string ClassID
        {
            get
            {
                if (viModel != null)
                {
                    return viModel.ClassID;
                }
                else
                {
                    return "-1";
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //栏目名称
            ClassModel claModel = new ClassModel();
            claModel = Factory.Class().GetCacheInfo2(ClassID);
            if (claModel != null)
            {
                litClassName.Text = claModel.ClassName;
            }
            //详细内容
            if (viModel != null)
            {
                //
                Page.Header.Title = Server.HtmlEncode(viModel.Title) + " - " + Page.Header.Title;
                //先清除母版页设置的keywords和description
                Page.Header.Controls.Remove(Page.Header.FindControl("keywords"));
                Page.Header.Controls.Remove(Page.Header.FindControl("description"));
                Page.Header.Controls.Add(Config.SetDescription(Server.HtmlEncode(viModel.Description)));
                //
                litVideoTitle.Text = viModel.Title;
                litDescription.Text = viModel.Description;
                StringBuilder strVideo = new StringBuilder();
                if (!string.IsNullOrEmpty(viModel.VideoPath))
                {
                    strVideo.Append("<div align=\"center\" id=\"video\">");
                    strVideo.Append("<script type=\"text/javascript\">");
                    strVideo.Append("showVideo(\"" + viModel.VideoPath + "\", \"" + viModel.VideoPic + "\",\"540\",\"320\",\"video\");");
                    strVideo.Append("</script>");
                    strVideo.Append("</div>");
                }
                litVideo.Text = strVideo.ToString();

            }
            else
            {
                Config.ShowEnd("参数错误!");
            }

            //上一篇
            string strPrevID = Factory.Video().GetPrevID(ClassID, VideoID);
            if (strPrevID == "0")
            {
                litPrev.Text = "没有了!";
            }
            else
            {
                litPrev.Text = "<a href=\"video-details-" + strPrevID + Config.FileExt + "\">" + Factory.Video().GetValueByField("Title", strPrevID) + "</a>";
            }

            //下一篇
            string strNextID = Factory.Video().GetNextID(ClassID, VideoID);
            if (strNextID == "0")
            {
                litNext.Text = "没有了!";
            }
            else
            {
                litNext.Text = "<a href=\"video-details-" + strNextID + Config.FileExt + "\">" + Factory.Video().GetValueByField("Title", strNextID) + "</a>";
            }

        }
    }
}