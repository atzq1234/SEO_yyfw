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
    public partial class WUC_Article_Details : System.Web.UI.UserControl
    {

        /// <summary>
        /// 信息ID(只读)
        /// </summary>
        public string ArticleID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["ArticleID"], -1).ToString();
            }
        }
        public ArticleModel artModel
        {
            get
            {
                return Factory.Article().GetCacheInfo2(ArticleID);
            }
        }
        /// <summary>
        /// 分类ID(只读)
        /// </summary>
        public string ClassID
        {
            get
            {
                if (artModel != null)
                {
                    return artModel.ClassID;
                }
                else
                {
                    return "-1";
                }
            }
        }
        public string ClassEnName { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            //子菜单绑定
            IList<ClassModel> dicList = Factory.Class().GetInfoListByParentID(Config.SysHelp);
            if (dicList != null)
            {
                rptChildClassList.DataSource = dicList;
                rptChildClassList.DataBind();
            }

            //栏目名称
            ClassModel claModel = new ClassModel();
            claModel = Factory.Class().GetCacheInfo2(ClassID);
            if (claModel != null)
            {
                ClassEnName = claModel.ClassEnName;
                litClassName.Text = claModel.ClassName;
            }
            //详细内容
            if (artModel != null)
            {
                Factory.Article().Click(ArticleID);
                //
                Page.Header.Title = Server.HtmlEncode(artModel.Title) + " - " + Page.Header.Title;
                //先清除母版页设置的keywords和description
                Page.Header.Controls.Remove(Page.Header.FindControl("keywords"));
                Page.Header.Controls.Remove(Page.Header.FindControl("description"));
                Page.Header.Controls.Add(Config.SetKeywords(Server.HtmlEncode(artModel.Keywords)));
                Page.Header.Controls.Add(Config.SetDescription(Server.HtmlEncode(artModel.Description)));
                //
                litTitle.Text = artModel.Title;
                litComeFrom.Text = artModel.ComeFrom;
                litAuthor.Text = artModel.Author;
                litAddTime.Text = artModel.AddTime;
                litDetails.Text = artModel.Details;
                StringBuilder strVideo = new StringBuilder();
                if (!string.IsNullOrEmpty(artModel.Video))
                {
                    strVideo.Append("<div align=\"center\" id=\"video\">");
                    if (Config.IsPicture(artModel.Video))
                    {
                        strVideo.Append("<img src=\"" + artModel.Video + "\" onload=\"javascript:if(this.width>550){this.width=550;}\"/>");
                    }
                    else
                    {
                        strVideo.Append("<script type=\"text/javascript\">");
                        strVideo.Append("showVideo(\"" + artModel.Video + "\", \"" + artModel.Picture + "\",\"540\",\"320\",\"video\");");
                        strVideo.Append("</script>");
                    }
                    strVideo.Append("</div>");
                }
                litVideo.Text = strVideo.ToString();
                litDetails.Text = artModel.Details;
                artpicture.ImageUrl = artModel.Picture;
               
            }
            else
            {
                Config.ShowEnd("参数错误!");
            }

            //上一篇
            string strPrevID = Factory.Article().GetPrevID(ClassID, ArticleID);
            if (strPrevID == "0")
            {
                litPrev.Text = "没有了!";
            }
            else
            {
                litPrev.Text = "<a href=\"article-details-" + strPrevID + Config.FileExt + "?#maodian\">" + Factory.Article().GetValueByField("Title", strPrevID) + "</a>";
            }

            //下一篇
            string strNextID = Factory.Article().GetNextID(ClassID, ArticleID);
            if (strNextID == "0")
            {
                litNext.Text = "没有了!";
            }
            else
            {
                litNext.Text = "<a href=\"article-details-" + strNextID + Config.FileExt + "?#maodian\">" + Factory.Article().GetValueByField("Title", strNextID) + "</a>";
            }

        }
    }
}