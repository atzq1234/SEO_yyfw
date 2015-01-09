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
    public partial class WUC_Product_Details : System.Web.UI.UserControl
    {
        /// <summary>
        /// 信息ID(只读)
        /// </summary>
        public string ProductID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["ProductID"], -1).ToString();
            }
        }
        public ProductModel proModel
        {
            get
            {
                return Factory.Product().GetCacheInfo2(ProductID);
            }
        }
        /// <summary>
        /// 分类ID(只读)
        /// </summary>
        public string ClassID
        {
            get
            {
                if (proModel != null)
                {
                    return proModel.ClassID;
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
               // litClassName.Text = claModel.ClassName;
            }
            //详细内容
            if (proModel != null)
            {
                Factory.Product().Click(ProductID);
                //
                Page.Header.Title = Server.HtmlEncode(proModel.ProductName) + " - " + Page.Header.Title;
                //先清除母版页设置的keywords和description
                Page.Header.Controls.Remove(Page.Header.FindControl("keywords"));
                Page.Header.Controls.Remove(Page.Header.FindControl("description"));
                Page.Header.Controls.Add(Config.SetKeywords(Server.HtmlEncode(proModel.Keywords)));
                Page.Header.Controls.Add(Config.SetDescription(Server.HtmlEncode(proModel.Description)));
                //
                litProductName.Text = proModel.ProductName;
                ltrPostTime.Text=Convert.ToDateTime(proModel.AddTime).ToShortDateString();
                ltrRemark.Text = proModel.Description;
                if (proModel.BigPic != string.Empty)
                    litBigPic.Text = "<img src=\"" + proModel.BigPic + "\" />";//onload=\"javascript:if(this.width>550)this.width=550;\" 
                //litDetails.Text = proModel.Details;
            }
            else
            {
                Config.ShowEnd("参数错误!");
            }

            //上一篇
            string strPrevID = Factory.Product().GetPrevID(ClassID, ProductID);
            if (strPrevID == "0")
            {
                //litPrev.Text = "没有了!";
                ltrPrevRemark.Text = "没有了!";
            }
            else
            {
                litPrev.Text = "<a class=\"prev icon\" href=\"case-details-" + strPrevID + Config.FileExt + "\"></a>";
                ltrNextRemark.Text = "<a  href=\"case-details-" + strPrevID + Config.FileExt + "\">" + Factory.Product().GetValueByField("ProductName", strPrevID) + "</a>";

            }

            //下一篇
            string strNextID = Factory.Product().GetNextID(ClassID,ProductID);
            if (strNextID == "0")
            {
               // litNext.Text = "没有了!";
                ltrNextRemark.Text = "没有了!";
            }
            else
            {
                litNext.Text = "<a class=\"next icon\" href=\"case-details-" + strNextID + Config.FileExt + "\"></a>";
                ltrNextRemark.Text = "<a href=\"case-details-" + strNextID + Config.FileExt + "\">"+Factory.Product().GetValueByField("ProductName",strNextID)+"</a>";
            }
        }
    }
}