using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace HxSoft.Common
{
    /// <summary>
    /// 绑定数据到控件
    /// </summary>
    public abstract class BindHelper
    {
        #region 数据绑定到CheckBoxList,DropDownList,ListBox,RadioButtonList
        /// <summary>
        /// 数据绑定到CheckBoxList,DropDownList,ListBox,RadioButtonList
        /// </summary>
        /// <param name="dt">内存数据中的一个表</param>
        /// <param name="objType">控件类型,如CheckBoxList,DropDownList,ListBox,RadioButtonList,可从Config.DataBindObjTypeCollection中获取</param>
        /// <param name="obj">数据控件名</param>
        /// <param name="TF">文本域字段名</param>
        /// <param name="VF">值字段名</param>
        public static void DataBind(DataTable dt, string objType, object obj, string TF, string VF)
        {
            switch (objType)
            {
                case "CheckBoxList":
                    ((CheckBoxList)obj).DataTextField = TF;
                    ((CheckBoxList)obj).DataValueField = VF;
                    ((CheckBoxList)obj).DataSource = dt;
                    ((CheckBoxList)obj).DataBind();
                    break;
                case "DropDownList":
                    ((DropDownList)obj).DataTextField = TF;
                    ((DropDownList)obj).DataValueField = VF;
                    ((DropDownList)obj).DataSource = dt;
                    ((DropDownList)obj).DataBind();
                    break;
                case "ListBox":
                    ((ListBox)obj).DataTextField = TF;
                    ((ListBox)obj).DataValueField = VF;
                    ((ListBox)obj).DataSource = dt;
                    ((ListBox)obj).DataBind();
                    break;
                case "RadioButtonList":
                    ((RadioButtonList)obj).DataTextField = TF;
                    ((RadioButtonList)obj).DataValueField = VF;
                    ((RadioButtonList)obj).DataSource = dt;
                    ((RadioButtonList)obj).DataBind();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 数据绑定到Repeater,DataList,DataGrid,GridView,DetailsView,FormView
        /// <summary>
        /// 数据绑定到Repeater,DataList,DataGrid,GridView,DetailsView,FormView
        /// </summary>
        /// <param name="dt">内存数据中的一个表</param>
        /// <param name="objType">控件类型,如Repeater,DataList,DataGrid,GridView,DetailsView,FormView,可从Config.DataBindObjTypeCollection中获取</param>
        /// <param name="obj">数据控件名</param>
        public static void DataBind(DataTable dt, string objType, object obj)
        {
            switch (objType)
            {
                case "Repeater":
                    ((Repeater)obj).DataSource = dt.DefaultView;
                    ((Repeater)obj).DataBind();
                    break;
                case "DataList":
                    ((DataList)obj).DataSource = dt.DefaultView;
                    ((DataList)obj).DataBind();
                    break;
                case "DataGrid":
                    ((DataGrid)obj).DataSource = dt.DefaultView;
                    ((DataGrid)obj).DataBind();
                    break;
                case "GridView":
                    ((GridView)obj).DataSource = dt.DefaultView;
                    ((GridView)obj).DataBind();
                    break;
                case "DetailsView":
                    ((DetailsView)obj).DataSource = dt.DefaultView;
                    ((DetailsView)obj).DataBind();
                    break;
                case "FormView":
                    ((FormView)obj).DataSource = dt.DefaultView;
                    ((FormView)obj).DataBind();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 数据绑定到Repeater,DataList,DataGrid,GridView,DetailsView,FormView-(后台分页)
        /// <summary>
        /// 数据绑定到Repeater,DataList,DataGrid,GridView,DetailsView,FormView-(后台分页)
        /// </summary>
        /// <param name="dt">内存数据中的一个表</param>
        /// <param name="objType">控件类型,如Repeater,DataList,DataGrid,GridView,DetailsView,FormView,可从Config.DataBindObjTypeCollection中获取</param>
        /// <param name="obj">数据控件名</param>
        /// <param name="PageSize">分页数</param>
        /// <param name="CurrentPage">当前页</param>
        /// <param name="PageUrl">分页链接,如list.aspx?或list.aspx?a=1&amp;</param>
        /// <returns>返回StringBuilder对象</returns>
        public static StringBuilder DataPageBind(DataTable dt, string objType, object obj, int PageSize, int CurrentPage, string PageUrl)
        {
            int AllCount;
            StringBuilder strPage = new StringBuilder();
            AllCount = dt.Rows.Count;
            PagedDataSource pds = new PagedDataSource();
            pds.DataSource = dt.DefaultView;
            pds.AllowPaging = true;
            pds.PageSize = PageSize;
            if (CurrentPage < 1) CurrentPage = 1;
            if (CurrentPage > pds.PageCount) CurrentPage = pds.PageCount;
            pds.CurrentPageIndex = CurrentPage - 1;
            if (AllCount == 0)
            {
                strPage.Append("<p>查询无相关记录!</p>");
            }
            else
            {
                strPage.Append("共<b>" + pds.PageCount + "</b>页 第<b>" + CurrentPage + "</b>页 <b>" + PageSize + "</b>条/页 共<b>" + AllCount + "</b>条记录\n");
                if (pds.IsFirstPage)
                {
                    strPage.Append("<span>首页 上一页</span>\n");
                }
                else
                {
                    strPage.Append("<a href=\"" + PageUrl + "page=1\">首页</a>\n");
                    strPage.Append("<a href=\"" + PageUrl + "page=" + (CurrentPage - 1).ToString() + "\">上一页</a>\n");
                }
                if (pds.IsLastPage)
                {
                    strPage.Append("<span>下一页 尾页</span>\n");
                }
                else
                {
                    strPage.Append("<a href=\"" + PageUrl + "page=" + (CurrentPage + 1).ToString() + "\">下一页</a>\n");
                    strPage.Append("<a href=\"" + PageUrl + "page=" + pds.PageCount + "\">尾页</a>\n");
                }
                strPage.Append("跳到<select onchange=\"javascript:location.href='" + PageUrl + "page='+this.options[this.selectedIndex].value\">\n");
                for (int i = 1; i <= pds.PageCount; i++)
                {
                    string sel = "";
                    if (i == CurrentPage) sel = "selected";
                    strPage.Append("<option value=\"" + i + "\" " + sel + ">" + i + "</option>\n");
                }
                strPage.Append("</select>页\n");
            }
            switch (objType)
            {
                case "Repeater":
                    ((Repeater)obj).DataSource = pds;
                    ((Repeater)obj).DataBind();
                    break;
                case "DataList":
                    ((DataList)obj).DataSource = pds;
                    ((DataList)obj).DataBind();
                    break;
                case "DataGrid":
                    ((DataGrid)obj).DataSource = pds;
                    ((DataGrid)obj).DataBind();
                    break;
                case "GridView":
                    ((GridView)obj).DataSource = pds;
                    ((GridView)obj).DataBind();
                    break;
                case "DetailsView":
                    ((DetailsView)obj).DataSource = pds;
                    ((DetailsView)obj).DataBind();
                    break;
                case "FormView":
                    ((FormView)obj).DataSource = pds;
                    ((FormView)obj).DataBind();
                    break;
                default:
                    break;
            }
            return strPage;
        }
        #endregion

        #region 数据绑定到Repeater,DataList,DataGrid,GridView,DetailsView,FormView-(中文分页)
        /// <summary>
        /// 数据绑定到Repeater,DataList,DataGrid,GridView,DetailsView,FormView-(中文分页)
        /// </summary>
        /// <param name="dt">内存数据中的一个表</param>
        /// <param name="objType">控件类型,如Repeater,DataList,DataGrid,GridView,DetailsView,FormView,可从Config.DataBindObjTypeCollection中获取</param>
        /// <param name="obj">数据控件名</param>
        /// <param name="PageSize">分页数</param>
        /// <param name="CurrentPage">当前页</param>
        /// <param name="PageUrl">分页链接,如list.aspx?或list.aspx?a=1&amp;</param>
        /// <returns>返回StringBuilder对象</returns>
        public static StringBuilder DataPageBindForCn(DataTable dt, string objType, object obj, int PageSize, int CurrentPage, string PageUrl)
        {
            int AllCount;
            StringBuilder strPage = new StringBuilder();
            AllCount = dt.Rows.Count;
            PagedDataSource pds = new PagedDataSource();
            pds.DataSource = dt.DefaultView;
            pds.AllowPaging = true;
            pds.PageSize = PageSize;
            if (CurrentPage < 1) CurrentPage = 1;
            if (CurrentPage > pds.PageCount) CurrentPage = pds.PageCount;
            pds.CurrentPageIndex = CurrentPage - 1;
            if (AllCount == 0)
            {
                strPage.Append("<p>查询无相关记录!</p>");
            }
            else
            {
                //strPage.Append("共<b>" + pds.PageCount + "</b>页 第<b>" + CurrentPage + "</b>页 <b>" + PageSize + "</b>条/页 共<b>" + AllCount + "</b>条记录\n");
                if (pds.IsFirstPage)
                {
                   // strPage.Append("<a href=\"?page=1&" + PageUrl + "\">首页</a>");
                    //strPage.Append(" <a>上一页</a>\n");
                }
                else
                {
                    strPage.Append("<a href=\"?page=1&" + PageUrl + "\">首页</a>\n");
                    strPage.Append("<a href=\"?page=" + (CurrentPage - 1).ToString() + "&" + PageUrl + "\">上一页</a>\n");
                }

                for (int i = CurrentPage - 5; i < CurrentPage + 5; i++)
                {
                    if (i >=0&i<pds.PageCount)
                    {
                        if ((i + 1) == CurrentPage)
                        {
                            strPage.Append("<b>"+CurrentPage+"</b>");
                        }
                        else
                        {
                            strPage.Append("<a href=\"?page=" + (i + 1) + "&" + PageUrl + "\">" + (i + 1) + "</a>\n");
                        }
                    }
                }

                if (pds.IsLastPage)
                {
                   // strPage.Append("<a>下一页</a>\n");
                    //strPage.Append("<a href=\"?page=" + pds.PageCount + "&" + PageUrl + "\">尾页</a>");
                }
                else
                {
                    strPage.Append("<a href=\"?page=" + (CurrentPage + 1).ToString() + "&" + PageUrl + "\">下一页</a>\n");
                    strPage.Append("<a href=\"?page=" + pds.PageCount + "&" + PageUrl + "\">尾页</a>\n");
                }
                //strPage.Append("跳到<select onchange=\"javascript:location.href='" + PageUrl + "page='+this.options[this.selectedIndex].value\">\n");
                //for (int i = 1; i <= pds.PageCount; i++)
                //{
                //    string sel = "";
                //    if (i == CurrentPage) sel = "selected";
                //    strPage.Append("<option value=\"" + i + "\" " + sel + ">" + i + "</option>\n");
                //}
                //strPage.Append("</select>页\n");
            }
            switch (objType)
            {
                case "Repeater":
                    ((Repeater)obj).DataSource = pds;
                    ((Repeater)obj).DataBind();
                    break;
                case "DataList":
                    ((DataList)obj).DataSource = pds;
                    ((DataList)obj).DataBind();
                    break;
                case "DataGrid":
                    ((DataGrid)obj).DataSource = pds;
                    ((DataGrid)obj).DataBind();
                    break;
                case "GridView":
                    ((GridView)obj).DataSource = pds;
                    ((GridView)obj).DataBind();
                    break;
                case "DetailsView":
                    ((DetailsView)obj).DataSource = pds;
                    ((DetailsView)obj).DataBind();
                    break;
                case "FormView":
                    ((FormView)obj).DataSource = pds;
                    ((FormView)obj).DataBind();
                    break;
                default:
                    break;
            }
            return strPage;
        }
        #endregion

        #region 数据绑定到Repeater,DataList,DataGrid,GridView,DetailsView,FormView-(英文分页)
        /// <summary>
        /// 数据绑定到Repeater,DataList,DataGrid,GridView,DetailsView,FormView-(英文分页)
        /// </summary>
        /// <param name="dt">内存数据中的一个表</param>
        /// <param name="objType">控件类型,如Repeater,DataList,DataGrid,GridView,DetailsView,FormView,可从Config.DataBindObjTypeCollection中获取</param>
        /// <param name="obj">数据控件名</param>
        /// <param name="PageSize">分页数</param>
        /// <param name="CurrentPage">当前页</param>
        /// <param name="PageUrl">分页链接,如list.aspx?或list.aspx?a=1&amp;</param>
        /// <returns>返回StringBuilder对象</returns>
        public static StringBuilder DataPageBindForEn(DataTable dt, string objType, object obj, int PageSize, int CurrentPage, string PageUrl)
        {
            int AllCount;
            StringBuilder strPage = new StringBuilder();
            AllCount = dt.Rows.Count;
            PagedDataSource pds = new PagedDataSource();
            pds.DataSource = dt.DefaultView;
            pds.AllowPaging = true;
            pds.PageSize = PageSize;
            if (CurrentPage < 1) CurrentPage = 1;
            if (CurrentPage > pds.PageCount) CurrentPage = pds.PageCount;
            pds.CurrentPageIndex = CurrentPage - 1;
            if (AllCount == 0)
            {
                strPage.Append("<p>Nothing!</p>");
            }
            else
            {
                strPage.Append("Page:<b>" + CurrentPage + "</b>/<b>" + pds.PageCount + "</b> <b>" + PageSize + "</b>Record/Page <b>" + AllCount + "</b>Records\n");
                if (pds.IsFirstPage)
                {
                    strPage.Append("<span>First Previous</span>\n");
                }
                else
                {
                    strPage.Append("<a href=\"" + PageUrl + "page=1\">First</a>\n");
                    strPage.Append("<a href=\"" + PageUrl + "page=" + (CurrentPage - 1).ToString() + "\">Previous</a>\n");
                }
                if (pds.IsLastPage)
                {
                    strPage.Append("<span>Next Last</span>\n");
                }
                else
                {
                    strPage.Append("<a href=\"" + PageUrl + "page=" + (CurrentPage + 1).ToString() + "\">Next</a>\n");
                    strPage.Append("<a href=\"" + PageUrl + "page=" + pds.PageCount + "\">Last</a>\n");
                }
                strPage.Append("Go<select onchange=\"javascript:location.href='" + PageUrl + "page='+this.options[this.selectedIndex].value\">\n");
                for (int i = 1; i <= pds.PageCount; i++)
                {
                    string sel = "";
                    if (i == CurrentPage) sel = "selected";
                    strPage.Append("<option value=\"" + i + "\" " + sel + ">" + i + "</option>\n");
                }
                strPage.Append("</select>Page\n");
            }
            switch (objType)
            {
                case "Repeater":
                    ((Repeater)obj).DataSource = pds;
                    ((Repeater)obj).DataBind();
                    break;
                case "DataList":
                    ((DataList)obj).DataSource = pds;
                    ((DataList)obj).DataBind();
                    break;
                case "DataGrid":
                    ((DataGrid)obj).DataSource = pds;
                    ((DataGrid)obj).DataBind();
                    break;
                case "GridView":
                    ((GridView)obj).DataSource = pds;
                    ((GridView)obj).DataBind();
                    break;
                case "DetailsView":
                    ((DetailsView)obj).DataSource = pds;
                    ((DetailsView)obj).DataBind();
                    break;
                case "FormView":
                    ((FormView)obj).DataSource = pds;
                    ((FormView)obj).DataBind();
                    break;
                default:
                    break;
            }
            return strPage;
        }
        #endregion

        #region 数据绑定到Repeater,DataList,DataGrid,GridView,DetailsView,FormView-(数字分页)
        /// <summary>
        /// 数据绑定到Repeater,DataList,DataGrid,GridView,DetailsView,FormView-(数字分页)
        /// </summary>
        /// <param name="dt">内存数据中的一个表</param>
        /// <param name="objType">控件类型,如Repeater,DataList,DataGrid,GridView,DetailsView,FormView,可从Config.DataBindObjTypeCollection中获取</param>
        /// <param name="obj">数据控件名</param>
        /// <param name="PageSize">分页数</param>
        /// <param name="CurrentPage">当前页</param>
        /// <param name="PageUrl">分页链接,如list.aspx?或list.aspx?a=1&amp;</param>
        /// <returns>返回StringBuilder对象</returns>
        public static StringBuilder DataPageBindForNum(DataTable dt, string objType, object obj, int PageSize, int CurrentPage, string PageUrl)
        {
            int AllCount;
            StringBuilder strPage = new StringBuilder();
            AllCount = dt.Rows.Count;
            PagedDataSource pds = new PagedDataSource();
            pds.DataSource = dt.DefaultView;
            pds.AllowPaging = true;
            pds.PageSize = PageSize;
            if (CurrentPage < 1) CurrentPage = 1;
            if (CurrentPage > pds.PageCount) CurrentPage = pds.PageCount;
            pds.CurrentPageIndex = CurrentPage - 1;
            if (AllCount == 0)
            {
                strPage.Append("<p>查询无相关记录!</p>");
            }
            else
            {
                int zoomsize = 5;
                int zoom, downlimit, uplimit;
                zoom = (CurrentPage - CurrentPage % zoomsize) / zoomsize;
                if (CurrentPage % zoomsize == 0)
                {
                    zoom = zoom - 1;
                }
                downlimit = zoom * zoomsize + 1;
                uplimit = downlimit + zoomsize - 1;

                if (uplimit > pds.PageCount)
                {
                    uplimit = pds.PageCount;
                }
                strPage.Append("<ul>");
                strPage.Append("<li>" + AllCount.ToString() + "</li>");
                strPage.Append("<li>" + CurrentPage.ToString() + "/" + pds.PageCount.ToString() + "</li>");
                if (!pds.IsFirstPage)
                {
                    strPage.Append("<li><a href=\"" + PageUrl + "page=" + (CurrentPage - 1).ToString() + "\">上一页</a></li>\n");
                }
                for (int i = downlimit; i <= uplimit; i++)
                {
                    if (i == CurrentPage)
                    {
                        strPage.Append("<li><a href=\"" + PageUrl + "page=" + i.ToString() + "\" class=\"selected\">" + i.ToString() + "</a></li>");
                    }
                    else
                    {
                        strPage.Append("<li><a href=\"" + PageUrl + "page=" + i.ToString() + "\">" + i.ToString() + "</a></li>");
                    }
                }
                if (!pds.IsLastPage)
                {
                    strPage.Append("<li><a href=\"" + PageUrl + "page=" + (CurrentPage + 1).ToString() + "\">下一页</a></li>\n");
                }
                strPage.Append("</ul>");
            }
            switch (objType)
            {
                case "Repeater":
                    ((Repeater)obj).DataSource = pds;
                    ((Repeater)obj).DataBind();
                    break;
                case "DataList":
                    ((DataList)obj).DataSource = pds;
                    ((DataList)obj).DataBind();
                    break;
                case "DataGrid":
                    ((DataGrid)obj).DataSource = pds;
                    ((DataGrid)obj).DataBind();
                    break;
                case "GridView":
                    ((GridView)obj).DataSource = pds;
                    ((GridView)obj).DataBind();
                    break;
                case "DetailsView":
                    ((DetailsView)obj).DataSource = pds;
                    ((DetailsView)obj).DataBind();
                    break;
                case "FormView":
                    ((FormView)obj).DataSource = pds;
                    ((FormView)obj).DataBind();
                    break;
                default:
                    break;
            }
            return strPage;
        }
        #endregion

        #region 数据绑定到Repeater,DataList,DataGrid,GridView,DetailsView,FormView-(SQL分页)
        /// <summary>
        /// 数据绑定到Repeater,DataList,DataGrid,GridView,DetailsView,FormView-(SQL分页)
        /// </summary>
        /// <param name="dt">内存数据中的一个表</param>
        /// <param name="objType">控件类型,如Repeater,DataList,DataGrid,GridView,DetailsView,FormView,可从Config.DataBindObjTypeCollection中获取</param>
        /// <param name="obj">数据控件名</param>
        /// <param name="PageSize">分页数</param>
        /// <param name="CurrentPage">当前页</param>
        /// <param name="PageUrl">分页链接,如list.aspx?或list.aspx?a=1&amp;</param>
        /// <param name="AllCount">记录总数</param>
        /// <returns>返回StringBuilder对象</returns>
        public static StringBuilder DataPageBindSql(DataTable dt, string objType, object obj, int PageSize, int CurrentPage, string PageUrl, int AllCount)
        {
            StringBuilder strPage = new StringBuilder();
            int PageCount = AllCount % PageSize == 0 ? AllCount / PageSize : AllCount / PageSize + 1;
            if (CurrentPage < 1) CurrentPage = 1;
            if (CurrentPage > PageCount) CurrentPage = PageCount;
            if (AllCount == 0)
            {
                strPage.Append("<p>查询无相关记录!</p>");
            }
            else
            {
                strPage.Append("共<b>" + PageCount + "</b>页 第<b>" + CurrentPage + "</b>页 <b>" + PageSize + "</b>条/页 共<b>" + AllCount + "</b>条记录\n");
                if (CurrentPage == 1)
                {
                    strPage.Append("<span>首页 上一页</span>\n");
                }
                else
                {
                    strPage.Append("<a href=\"" + PageUrl + "page=1\">首页</a>\n");
                    strPage.Append("<a href=\"" + PageUrl + "page=" + (CurrentPage - 1).ToString() + "\">上一页</a>\n");
                }
                if (CurrentPage == PageCount)
                {
                    strPage.Append("<span>下一页 尾页</span>\n");
                }
                else
                {
                    strPage.Append("<a href=\"" + PageUrl + "page=" + (CurrentPage + 1).ToString() + "\">下一页</a>\n");
                    strPage.Append("<a href=\"" + PageUrl + "page=" + PageCount + "\">尾页</a>\n");
                }
                strPage.Append("跳到<select onchange=\"javascript:location.href='" + PageUrl + "page='+this.options[this.selectedIndex].value\">\n");
                for (int i = 1; i <= PageCount; i++)
                {
                    string sel = "";
                    if (i == CurrentPage) sel = "selected";
                    strPage.Append("<option value=\"" + i + "\" " + sel + ">" + i + "</option>\n");
                }
                strPage.Append("</select>页\n");
            }
            switch (objType)
            {
                case "Repeater":
                    ((Repeater)obj).DataSource = dt;
                    ((Repeater)obj).DataBind();
                    break;
                case "DataList":
                    ((DataList)obj).DataSource = dt;
                    ((DataList)obj).DataBind();
                    break;
                case "DataGrid":
                    ((DataGrid)obj).DataSource = dt;
                    ((DataGrid)obj).DataBind();
                    break;
                case "GridView":
                    ((GridView)obj).DataSource = dt;
                    ((GridView)obj).DataBind();
                    break;
                case "DetailsView":
                    ((DetailsView)obj).DataSource = dt;
                    ((DetailsView)obj).DataBind();
                    break;
                case "FormView":
                    ((FormView)obj).DataSource = dt;
                    ((FormView)obj).DataBind();
                    break;
                default:
                    break;
            }
            return strPage;
        }
        #endregion

        #region 数据绑定到Repeater,DataList,DataGrid,GridView,DetailsView,FormView-(存储分页)
        /// <summary>
        /// 数据绑定到Repeater,DataList,DataGrid,GridView,DetailsView,FormView-(存储分页)
        /// </summary>
        /// <param name="dt">内存数据中的一个表</param>
        /// <param name="objType">控件类型,如Repeater,DataList,DataGrid,GridView,DetailsView,FormView,可从Config.DataBindObjTypeCollection中获取</param>
        /// <param name="obj">数据控件名</param>
        /// <param name="PageSize">分页数</param>
        /// <param name="CurrentPage">当前页</param>
        /// <param name="PageUrl">分页链接,如list.aspx?或list.aspx?a=1&amp;</param>
        /// <param name="AllCount">记录总数</param>
        /// <returns>返回StringBuilder对象</returns>
        public static StringBuilder DataPageBindSp(DataTable dt, string objType, object obj, int PageSize, int CurrentPage, string PageUrl, int AllCount)
        {
            StringBuilder strPage = new StringBuilder();
            int PageCount = AllCount % PageSize == 0 ? AllCount / PageSize : AllCount / PageSize + 1;
            if (CurrentPage < 1) CurrentPage = 1;
            if (CurrentPage > PageCount) CurrentPage = PageCount;
            if (AllCount == 0)
            {
                strPage.Append("<p>查询无相关记录!</p>");
            }
            else
            {
                strPage.Append("共<b>" + PageCount + "</b>页 第<b>" + CurrentPage + "</b>页 <b>" + PageSize + "</b>条/页 共<b>" + AllCount + "</b>条记录\n");
                if (CurrentPage == 1)
                {
                    strPage.Append("<span>首页 上一页</span>\n");
                }
                else
                {
                    strPage.Append("<a href=\"" + PageUrl + "page=1\">首页</a>\n");
                    strPage.Append("<a href=\"" + PageUrl + "page=" + (CurrentPage - 1).ToString() + "\">上一页</a>\n");
                }
                if (CurrentPage == PageCount)
                {
                    strPage.Append("<span>下一页 尾页</span>\n");
                }
                else
                {
                    strPage.Append("<a href=\"" + PageUrl + "page=" + (CurrentPage + 1).ToString() + "\">下一页</a>\n");
                    strPage.Append("<a href=\"" + PageUrl + "page=" + PageCount + "\">尾页</a>\n");
                }
                strPage.Append("跳到<select onchange=\"javascript:location.href='" + PageUrl + "page='+this.options[this.selectedIndex].value\">\n");
                for (int i = 1; i <= PageCount; i++)
                {
                    string sel = "";
                    if (i == CurrentPage) sel = "selected";
                    strPage.Append("<option value=\"" + i + "\" " + sel + ">" + i + "</option>\n");
                }
                strPage.Append("</select>页\n");
            }
            switch (objType)
            {
                case "Repeater":
                    ((Repeater)obj).DataSource = dt;
                    ((Repeater)obj).DataBind();
                    break;
                case "DataList":
                    ((DataList)obj).DataSource = dt;
                    ((DataList)obj).DataBind();
                    break;
                case "DataGrid":
                    ((DataGrid)obj).DataSource = dt;
                    ((DataGrid)obj).DataBind();
                    break;
                case "GridView":
                    ((GridView)obj).DataSource = dt;
                    ((GridView)obj).DataBind();
                    break;
                case "DetailsView":
                    ((DetailsView)obj).DataSource = dt;
                    ((DetailsView)obj).DataBind();
                    break;
                case "FormView":
                    ((FormView)obj).DataSource = dt;
                    ((FormView)obj).DataBind();
                    break;
                default:
                    break;
            }
            return strPage;
        }
        #endregion

    }
}
