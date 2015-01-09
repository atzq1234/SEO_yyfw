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
    /// �����ݵ��ؼ�
    /// </summary>
    public abstract class BindHelper
    {
        #region ���ݰ󶨵�CheckBoxList,DropDownList,ListBox,RadioButtonList
        /// <summary>
        /// ���ݰ󶨵�CheckBoxList,DropDownList,ListBox,RadioButtonList
        /// </summary>
        /// <param name="dt">�ڴ������е�һ����</param>
        /// <param name="objType">�ؼ�����,��CheckBoxList,DropDownList,ListBox,RadioButtonList,�ɴ�Config.DataBindObjTypeCollection�л�ȡ</param>
        /// <param name="obj">���ݿؼ���</param>
        /// <param name="TF">�ı����ֶ���</param>
        /// <param name="VF">ֵ�ֶ���</param>
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

        #region ���ݰ󶨵�Repeater,DataList,DataGrid,GridView,DetailsView,FormView
        /// <summary>
        /// ���ݰ󶨵�Repeater,DataList,DataGrid,GridView,DetailsView,FormView
        /// </summary>
        /// <param name="dt">�ڴ������е�һ����</param>
        /// <param name="objType">�ؼ�����,��Repeater,DataList,DataGrid,GridView,DetailsView,FormView,�ɴ�Config.DataBindObjTypeCollection�л�ȡ</param>
        /// <param name="obj">���ݿؼ���</param>
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

        #region ���ݰ󶨵�Repeater,DataList,DataGrid,GridView,DetailsView,FormView-(��̨��ҳ)
        /// <summary>
        /// ���ݰ󶨵�Repeater,DataList,DataGrid,GridView,DetailsView,FormView-(��̨��ҳ)
        /// </summary>
        /// <param name="dt">�ڴ������е�һ����</param>
        /// <param name="objType">�ؼ�����,��Repeater,DataList,DataGrid,GridView,DetailsView,FormView,�ɴ�Config.DataBindObjTypeCollection�л�ȡ</param>
        /// <param name="obj">���ݿؼ���</param>
        /// <param name="PageSize">��ҳ��</param>
        /// <param name="CurrentPage">��ǰҳ</param>
        /// <param name="PageUrl">��ҳ����,��list.aspx?��list.aspx?a=1&amp;</param>
        /// <returns>����StringBuilder����</returns>
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
                strPage.Append("<p>��ѯ����ؼ�¼!</p>");
            }
            else
            {
                strPage.Append("��<b>" + pds.PageCount + "</b>ҳ ��<b>" + CurrentPage + "</b>ҳ <b>" + PageSize + "</b>��/ҳ ��<b>" + AllCount + "</b>����¼\n");
                if (pds.IsFirstPage)
                {
                    strPage.Append("<span>��ҳ ��һҳ</span>\n");
                }
                else
                {
                    strPage.Append("<a href=\"" + PageUrl + "page=1\">��ҳ</a>\n");
                    strPage.Append("<a href=\"" + PageUrl + "page=" + (CurrentPage - 1).ToString() + "\">��һҳ</a>\n");
                }
                if (pds.IsLastPage)
                {
                    strPage.Append("<span>��һҳ βҳ</span>\n");
                }
                else
                {
                    strPage.Append("<a href=\"" + PageUrl + "page=" + (CurrentPage + 1).ToString() + "\">��һҳ</a>\n");
                    strPage.Append("<a href=\"" + PageUrl + "page=" + pds.PageCount + "\">βҳ</a>\n");
                }
                strPage.Append("����<select onchange=\"javascript:location.href='" + PageUrl + "page='+this.options[this.selectedIndex].value\">\n");
                for (int i = 1; i <= pds.PageCount; i++)
                {
                    string sel = "";
                    if (i == CurrentPage) sel = "selected";
                    strPage.Append("<option value=\"" + i + "\" " + sel + ">" + i + "</option>\n");
                }
                strPage.Append("</select>ҳ\n");
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

        #region ���ݰ󶨵�Repeater,DataList,DataGrid,GridView,DetailsView,FormView-(���ķ�ҳ)
        /// <summary>
        /// ���ݰ󶨵�Repeater,DataList,DataGrid,GridView,DetailsView,FormView-(���ķ�ҳ)
        /// </summary>
        /// <param name="dt">�ڴ������е�һ����</param>
        /// <param name="objType">�ؼ�����,��Repeater,DataList,DataGrid,GridView,DetailsView,FormView,�ɴ�Config.DataBindObjTypeCollection�л�ȡ</param>
        /// <param name="obj">���ݿؼ���</param>
        /// <param name="PageSize">��ҳ��</param>
        /// <param name="CurrentPage">��ǰҳ</param>
        /// <param name="PageUrl">��ҳ����,��list.aspx?��list.aspx?a=1&amp;</param>
        /// <returns>����StringBuilder����</returns>
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
                strPage.Append("<p>��ѯ����ؼ�¼!</p>");
            }
            else
            {
                //strPage.Append("��<b>" + pds.PageCount + "</b>ҳ ��<b>" + CurrentPage + "</b>ҳ <b>" + PageSize + "</b>��/ҳ ��<b>" + AllCount + "</b>����¼\n");
                if (pds.IsFirstPage)
                {
                   // strPage.Append("<a href=\"?page=1&" + PageUrl + "\">��ҳ</a>");
                    //strPage.Append(" <a>��һҳ</a>\n");
                }
                else
                {
                    strPage.Append("<a href=\"?page=1&" + PageUrl + "\">��ҳ</a>\n");
                    strPage.Append("<a href=\"?page=" + (CurrentPage - 1).ToString() + "&" + PageUrl + "\">��һҳ</a>\n");
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
                   // strPage.Append("<a>��һҳ</a>\n");
                    //strPage.Append("<a href=\"?page=" + pds.PageCount + "&" + PageUrl + "\">βҳ</a>");
                }
                else
                {
                    strPage.Append("<a href=\"?page=" + (CurrentPage + 1).ToString() + "&" + PageUrl + "\">��һҳ</a>\n");
                    strPage.Append("<a href=\"?page=" + pds.PageCount + "&" + PageUrl + "\">βҳ</a>\n");
                }
                //strPage.Append("����<select onchange=\"javascript:location.href='" + PageUrl + "page='+this.options[this.selectedIndex].value\">\n");
                //for (int i = 1; i <= pds.PageCount; i++)
                //{
                //    string sel = "";
                //    if (i == CurrentPage) sel = "selected";
                //    strPage.Append("<option value=\"" + i + "\" " + sel + ">" + i + "</option>\n");
                //}
                //strPage.Append("</select>ҳ\n");
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

        #region ���ݰ󶨵�Repeater,DataList,DataGrid,GridView,DetailsView,FormView-(Ӣ�ķ�ҳ)
        /// <summary>
        /// ���ݰ󶨵�Repeater,DataList,DataGrid,GridView,DetailsView,FormView-(Ӣ�ķ�ҳ)
        /// </summary>
        /// <param name="dt">�ڴ������е�һ����</param>
        /// <param name="objType">�ؼ�����,��Repeater,DataList,DataGrid,GridView,DetailsView,FormView,�ɴ�Config.DataBindObjTypeCollection�л�ȡ</param>
        /// <param name="obj">���ݿؼ���</param>
        /// <param name="PageSize">��ҳ��</param>
        /// <param name="CurrentPage">��ǰҳ</param>
        /// <param name="PageUrl">��ҳ����,��list.aspx?��list.aspx?a=1&amp;</param>
        /// <returns>����StringBuilder����</returns>
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

        #region ���ݰ󶨵�Repeater,DataList,DataGrid,GridView,DetailsView,FormView-(���ַ�ҳ)
        /// <summary>
        /// ���ݰ󶨵�Repeater,DataList,DataGrid,GridView,DetailsView,FormView-(���ַ�ҳ)
        /// </summary>
        /// <param name="dt">�ڴ������е�һ����</param>
        /// <param name="objType">�ؼ�����,��Repeater,DataList,DataGrid,GridView,DetailsView,FormView,�ɴ�Config.DataBindObjTypeCollection�л�ȡ</param>
        /// <param name="obj">���ݿؼ���</param>
        /// <param name="PageSize">��ҳ��</param>
        /// <param name="CurrentPage">��ǰҳ</param>
        /// <param name="PageUrl">��ҳ����,��list.aspx?��list.aspx?a=1&amp;</param>
        /// <returns>����StringBuilder����</returns>
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
                strPage.Append("<p>��ѯ����ؼ�¼!</p>");
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
                    strPage.Append("<li><a href=\"" + PageUrl + "page=" + (CurrentPage - 1).ToString() + "\">��һҳ</a></li>\n");
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
                    strPage.Append("<li><a href=\"" + PageUrl + "page=" + (CurrentPage + 1).ToString() + "\">��һҳ</a></li>\n");
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

        #region ���ݰ󶨵�Repeater,DataList,DataGrid,GridView,DetailsView,FormView-(SQL��ҳ)
        /// <summary>
        /// ���ݰ󶨵�Repeater,DataList,DataGrid,GridView,DetailsView,FormView-(SQL��ҳ)
        /// </summary>
        /// <param name="dt">�ڴ������е�һ����</param>
        /// <param name="objType">�ؼ�����,��Repeater,DataList,DataGrid,GridView,DetailsView,FormView,�ɴ�Config.DataBindObjTypeCollection�л�ȡ</param>
        /// <param name="obj">���ݿؼ���</param>
        /// <param name="PageSize">��ҳ��</param>
        /// <param name="CurrentPage">��ǰҳ</param>
        /// <param name="PageUrl">��ҳ����,��list.aspx?��list.aspx?a=1&amp;</param>
        /// <param name="AllCount">��¼����</param>
        /// <returns>����StringBuilder����</returns>
        public static StringBuilder DataPageBindSql(DataTable dt, string objType, object obj, int PageSize, int CurrentPage, string PageUrl, int AllCount)
        {
            StringBuilder strPage = new StringBuilder();
            int PageCount = AllCount % PageSize == 0 ? AllCount / PageSize : AllCount / PageSize + 1;
            if (CurrentPage < 1) CurrentPage = 1;
            if (CurrentPage > PageCount) CurrentPage = PageCount;
            if (AllCount == 0)
            {
                strPage.Append("<p>��ѯ����ؼ�¼!</p>");
            }
            else
            {
                strPage.Append("��<b>" + PageCount + "</b>ҳ ��<b>" + CurrentPage + "</b>ҳ <b>" + PageSize + "</b>��/ҳ ��<b>" + AllCount + "</b>����¼\n");
                if (CurrentPage == 1)
                {
                    strPage.Append("<span>��ҳ ��һҳ</span>\n");
                }
                else
                {
                    strPage.Append("<a href=\"" + PageUrl + "page=1\">��ҳ</a>\n");
                    strPage.Append("<a href=\"" + PageUrl + "page=" + (CurrentPage - 1).ToString() + "\">��һҳ</a>\n");
                }
                if (CurrentPage == PageCount)
                {
                    strPage.Append("<span>��һҳ βҳ</span>\n");
                }
                else
                {
                    strPage.Append("<a href=\"" + PageUrl + "page=" + (CurrentPage + 1).ToString() + "\">��һҳ</a>\n");
                    strPage.Append("<a href=\"" + PageUrl + "page=" + PageCount + "\">βҳ</a>\n");
                }
                strPage.Append("����<select onchange=\"javascript:location.href='" + PageUrl + "page='+this.options[this.selectedIndex].value\">\n");
                for (int i = 1; i <= PageCount; i++)
                {
                    string sel = "";
                    if (i == CurrentPage) sel = "selected";
                    strPage.Append("<option value=\"" + i + "\" " + sel + ">" + i + "</option>\n");
                }
                strPage.Append("</select>ҳ\n");
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

        #region ���ݰ󶨵�Repeater,DataList,DataGrid,GridView,DetailsView,FormView-(�洢��ҳ)
        /// <summary>
        /// ���ݰ󶨵�Repeater,DataList,DataGrid,GridView,DetailsView,FormView-(�洢��ҳ)
        /// </summary>
        /// <param name="dt">�ڴ������е�һ����</param>
        /// <param name="objType">�ؼ�����,��Repeater,DataList,DataGrid,GridView,DetailsView,FormView,�ɴ�Config.DataBindObjTypeCollection�л�ȡ</param>
        /// <param name="obj">���ݿؼ���</param>
        /// <param name="PageSize">��ҳ��</param>
        /// <param name="CurrentPage">��ǰҳ</param>
        /// <param name="PageUrl">��ҳ����,��list.aspx?��list.aspx?a=1&amp;</param>
        /// <param name="AllCount">��¼����</param>
        /// <returns>����StringBuilder����</returns>
        public static StringBuilder DataPageBindSp(DataTable dt, string objType, object obj, int PageSize, int CurrentPage, string PageUrl, int AllCount)
        {
            StringBuilder strPage = new StringBuilder();
            int PageCount = AllCount % PageSize == 0 ? AllCount / PageSize : AllCount / PageSize + 1;
            if (CurrentPage < 1) CurrentPage = 1;
            if (CurrentPage > PageCount) CurrentPage = PageCount;
            if (AllCount == 0)
            {
                strPage.Append("<p>��ѯ����ؼ�¼!</p>");
            }
            else
            {
                strPage.Append("��<b>" + PageCount + "</b>ҳ ��<b>" + CurrentPage + "</b>ҳ <b>" + PageSize + "</b>��/ҳ ��<b>" + AllCount + "</b>����¼\n");
                if (CurrentPage == 1)
                {
                    strPage.Append("<span>��ҳ ��һҳ</span>\n");
                }
                else
                {
                    strPage.Append("<a href=\"" + PageUrl + "page=1\">��ҳ</a>\n");
                    strPage.Append("<a href=\"" + PageUrl + "page=" + (CurrentPage - 1).ToString() + "\">��һҳ</a>\n");
                }
                if (CurrentPage == PageCount)
                {
                    strPage.Append("<span>��һҳ βҳ</span>\n");
                }
                else
                {
                    strPage.Append("<a href=\"" + PageUrl + "page=" + (CurrentPage + 1).ToString() + "\">��һҳ</a>\n");
                    strPage.Append("<a href=\"" + PageUrl + "page=" + PageCount + "\">βҳ</a>\n");
                }
                strPage.Append("����<select onchange=\"javascript:location.href='" + PageUrl + "page='+this.options[this.selectedIndex].value\">\n");
                for (int i = 1; i <= PageCount; i++)
                {
                    string sel = "";
                    if (i == CurrentPage) sel = "selected";
                    strPage.Append("<option value=\"" + i + "\" " + sel + ">" + i + "</option>\n");
                }
                strPage.Append("</select>ҳ\n");
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
