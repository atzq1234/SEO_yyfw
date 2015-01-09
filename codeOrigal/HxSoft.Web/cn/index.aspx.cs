using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using HxSoft.Common;
using HxSoft.Model;
using HxSoft.ClassFactory;

namespace HxSoft.Web.cn
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCase();
            }
        }
        protected void BindCase()
        {
            IList<ProductModel> prodList = Factory.Product().GetInfoTopList("1",12);
            //rptCaseList.DataSource = prodList;
            //rptCaseList.DataBind();

            System.Text.StringBuilder sb = new StringBuilder();

            for (int i = 0; i < prodList.Count;i++ )
            {
                if ((i + 1) % 2 != 0)
                {
                    sb.Append("<li>");
                    sb.Append("<div class=\"case-box\">");
                }
              
                sb.Append("<a class=\"case-con\" title=\"京东商城\" href=\"case-details-"+prodList[i].ProductID+".html\">");
                sb.Append("<img src=\"" + prodList[i].SmallPic + "\" title=\"" + prodList[i].ProductName + "\" alt=\"" + prodList[i].ProductName + "\" />");
                sb.Append("<div class=\"case-exp\">");
                sb.Append("  <em></em>");
                sb.Append(" <p><strong>");
                sb.Append(prodList[i].ProductName + "</strong><span>品牌网站设计</span></p>");
                sb.Append("  </div> </a> ");
                if ((i + 1) % 2 == 0)
                {
                    sb.Append("</div></li>");
                }
            }
            ltrlistCase.Text = sb.ToString();
            //Help
            //列表绑定
            rephelpList1.DataSource = Factory.Article().GetInfoList("16",7);
            rephelpList1.DataBind();

            //列表绑定

            rephelpList2.DataSource = Factory.Article().GetInfoList("18", 7);
            rephelpList2.DataBind();

            //列表绑定
            rephelpList3.DataSource = Factory.Article().GetInfoList("19", 7);
            rephelpList3.DataBind();
        }

    }
}