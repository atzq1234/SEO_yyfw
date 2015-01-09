using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Caching;
using HxSoft.Common;
using HxSoft.Model;
using HxSoft.DAL;
using System.Data.Common;

namespace HxSoft.BLL
{
    /// <summary>
    /// Acc类,返回DataTable,记录总数,数据绑定
    /// </summary>
    public class AccBLL
    {
        private readonly AccDAL accDAL = new AccDAL();
        
        #region 返回DataTable
        /// <summary>
        /// 返回DataTable
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string strSql, DbParameter[] cmdParams)
        {
            return accDAL.GetDataTable(strSql, cmdParams);
        }
        #endregion

        #region 返回记录总数
        /// <summary>
        /// 返回记录总数
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        public int GetAllCount(string strSql, DbParameter[] cmdParams)
        {
            return accDAL.GetAllCount(strSql, cmdParams);
        }
        #endregion

        #region 数据绑定到CheckBoxList,DropDownList,ListBox,RadioButtonList
        /// <summary>
        /// 数据绑定到CheckBoxList,DropDownList,ListBox,RadioButtonList
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="objType">控件类型,如CheckBoxList,DropDownList,ListBox,RadioButtonList</param>
        /// <param name="obj"></param>
        /// <param name="TF">文本域字段名</param>
        /// <param name="VF">值字段名</param>
        public void DataBind(string strSql, DbParameter[] cmdParams, string objType, object obj, string TF, string VF)
        {
            DataTable dt = accDAL.GetDataTable(strSql, cmdParams);
            BindHelper.DataBind(dt, objType, obj,TF,VF);
        }
        #endregion

        #region 数据绑定到Repeater,DataList,DataGrid,GridView,DetailsView,FormView
        /// <summary>
        /// 数据绑定到Repeater,DataList,DataGrid,GridView,DetailsView,FormView
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="objType">控件类型,如Repeater,DataList,DataGrid,GridView,DetailsView,FormView</param>
        /// <param name="obj"></param>
        public void DataBind(string strSql, DbParameter[] cmdParams, string objType, object obj)
        {
            DataTable dt = accDAL.GetDataTable(strSql, cmdParams);
            BindHelper.DataBind(dt, objType, obj);
        }
        #endregion

        #region 数据绑定到Repeater,DataList,DataGrid,GridView,DetailsView,FormView(分页)
        /// <summary>
        /// 数据绑定到Repeater,DataList,DataGrid,GridView,DetailsView,FormView(分页)
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="objType">控件类型,如Repeater,DataList,DataGrid,GridView,DetailsView,FormView</param>
        /// <param name="obj"></param>
        /// <param name="PageSize"></param>
        /// <param name="CurrentPage"></param>
        /// <param name="PageUrl"></param>
        /// <returns></returns>
        public StringBuilder DataPageBind(string strSql, DbParameter[] cmdParams, string objType, object obj, int PageSize, int CurrentPage, string PageUrl)
        {
            DataTable dt = accDAL.GetDataTable(strSql, cmdParams);
            return BindHelper.DataPageBind(dt, objType, obj, PageSize, CurrentPage, PageUrl);
        }
        #endregion

        #region 数据绑定到Repeater,DataList,DataGrid,GridView,DetailsView,FormView(分页-中文)
        /// <summary>
        /// 数据绑定到Repeater,DataList,DataGrid,GridView,DetailsView,FormView(分页-中文)
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="objType">控件类型,如Repeater,DataList,DataGrid,GridView,DetailsView,FormView</param>
        /// <param name="obj"></param>
        /// <param name="PageSize"></param>
        /// <param name="CurrentPage"></param>
        /// <param name="PageUrl"></param>
        /// <returns></returns>
        public StringBuilder DataPageBindForCn(string strSql, DbParameter[] cmdParams, string objType, object obj, int PageSize, int CurrentPage, string PageUrl)
        {
            DataTable dt = accDAL.GetDataTable(strSql, cmdParams);
            return BindHelper.DataPageBindForCn(dt, objType, obj, PageSize, CurrentPage, PageUrl);
        }
        #endregion

        #region 数据绑定到Repeater,DataList,DataGrid,GridView,DetailsView,FormView(分页-英文)
        /// <summary>
        /// 数据绑定到Repeater,DataList,DataGrid,GridView,DetailsView,FormView(分页-英文)
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="objType">控件类型,如Repeater,DataList,DataGrid,GridView,DetailsView,FormView</param>
        /// <param name="obj"></param>
        /// <param name="PageSize"></param>
        /// <param name="CurrentPage"></param>
        /// <param name="PageUrl"></param>
        /// <returns></returns>
        public StringBuilder DataPageBindForEn(string strSql, DbParameter[] cmdParams, string objType, object obj, int PageSize, int CurrentPage, string PageUrl)
        {
            DataTable dt = accDAL.GetDataTable(strSql, cmdParams);
            return BindHelper.DataPageBindForEn(dt, objType, obj, PageSize, CurrentPage, PageUrl);
        }
        #endregion

        #region 数据绑定到Repeater,DataList,DataGrid,GridView,DetailsView,FormView(分页-数字)
        /// <summary>
        /// 数据绑定到Repeater,DataList,DataGrid,GridView,DetailsView,FormView(分页-英文)
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="objType">控件类型,如Repeater,DataList,DataGrid,GridView,DetailsView,FormView</param>
        /// <param name="obj"></param>
        /// <param name="PageSize"></param>
        /// <param name="CurrentPage"></param>
        /// <param name="PageUrl"></param>
        /// <returns></returns>
        public StringBuilder DataPageBindForNum(string strSql, DbParameter[] cmdParams, string objType, object obj, int PageSize, int CurrentPage, string PageUrl)
        {
            DataTable dt=accDAL.GetDataTable(strSql,cmdParams);
            return BindHelper.DataPageBindForNum(dt, objType, obj, PageSize, CurrentPage, PageUrl);
        }
        #endregion

        #region 数据绑定到Repeater,DataList,DataGrid,GridView,DetailsView,FormView-(SQL分页)
        /// <summary>
        /// 数据绑定到Repeater,DataList,DataGrid,GridView,DetailsView,FormView-(SQL分页)
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="FieldKey">主键</param>
        /// <param name="FieldShow">显示字段</param>
        /// <param name="FieldOrder">排序</param>
        /// <param name="Where">条件</param>
        /// <param name="objType">控件类型,如Repeater,DataList,DataGrid,GridView,DetailsView,FormView,可从Config.DataBindObjTypeCollection中获取</param>
        /// <param name="obj">数据控件名</param>
        /// <param name="PageSize">分页数</param>
        /// <param name="CurrentPage">当前页</param>
        /// <param name="PageUrl">分页链接,如list.aspx?或list.aspx?a=1&amp;</param>
        /// <returns>返回StringBuilder对象</returns>
        public StringBuilder DataPageBindSql(string TableName, string FieldKey, string FieldShow, string FieldOrder, string Where, DbParameter[] cmdParams, string objType, object obj, int PageSize, int CurrentPage, string PageUrl)
        {
            int AllCount = 0;
            DataTable dt = accDAL.GetDataTable(TableName, FieldKey, CurrentPage, PageSize, FieldShow, FieldOrder, Where, ref AllCount, cmdParams);
            return BindHelper.DataPageBindSql(dt, objType, obj, PageSize, CurrentPage, PageUrl, AllCount);
        }
        #endregion
        
        #region 数据绑定到Repeater,DataList,DataGrid,GridView,DetailsView,FormView-(存储分页)
        /// <summary>
        /// 数据绑定到Repeater,DataList,DataGrid,GridView,DetailsView,FormView-(存储分页)
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="FieldKey">主键</param>
        /// <param name="FieldShow">显示字段</param>
        /// <param name="FieldOrder">排序</param>
        /// <param name="Where">条件</param>
        /// <param name="objType">控件类型,如Repeater,DataList,DataGrid,GridView,DetailsView,FormView,可从Config.DataBindObjTypeCollection中获取</param>
        /// <param name="obj">数据控件名</param>
        /// <param name="PageSize">分页数</param>
        /// <param name="CurrentPage">当前页</param>
        /// <param name="PageUrl">分页链接,如list.aspx?或list.aspx?a=1&amp;</param>
        /// <returns>返回StringBuilder对象</returns>
        public StringBuilder DataPageBindSp(string TableName, string FieldKey, string FieldShow, string FieldOrder, string Where, string objType, object obj, int PageSize, int CurrentPage, string PageUrl)
        {
            int AllCount = 0;
            DataTable dt = accDAL.GetDataTable(TableName, FieldKey, CurrentPage, PageSize, FieldShow, FieldOrder, Where, ref AllCount);
            return BindHelper.DataPageBindSp(dt, objType, obj, PageSize, CurrentPage, PageUrl, AllCount);
        }
        #endregion

    }
}
