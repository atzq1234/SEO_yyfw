using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using HxSoft.Common;
using HxSoft.Model;
using HxSoft.ClassFactory;
using System.Data;

namespace HxSoft.Web.cn.UserControl
{
    public partial class WUC_Guestbook : System.Web.UI.UserControl
    {
        private string _classid;
        private int _pagesize;
        /// <summary>
        /// 分类ID
        /// </summary>
        public string ClassID
        {
            get { return _classid; }
            set { _classid = value; }
        }
        /// <summary>
        /// 分页数
        /// </summary>
        public int PageSize
        {
            get { return _pagesize; }
            set { _pagesize = value; }
        }
        /// <summary>
        /// 分页(只读)
        /// </summary>
        public int page
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["page"], 1);
            }
        }
        #region****查询参数****
        public string SearchKey
        {
            get
            {
                return Config.Request(Request["SearchKey"], "");
            }
        }
        #endregion
        #region****查询语句****
        public string SqlQuery
        {
            get
            {
                StringBuilder TempSql = new StringBuilder("");
                if (SearchKey != "") TempSql.Append(" and BookContent like '%" + SearchKey + "%'");
                return TempSql.ToString();
            }
        }
        #endregion
        #region****Url参数****
        public string UrlPara
        {
            get
            {
                StringBuilder TempUrl = new StringBuilder("");
                TempUrl.Append("SearchKey=" + Server.UrlEncode(SearchKey) + "&");
                return TempUrl.ToString();
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            //栏目名称
            //ClassModel claModel = new ClassModel();
            //claModel = Factory.Class().GetCacheInfo2(ClassID);
            //if (claModel != null)
            //{
            //    Page.Header.Title = Server.HtmlEncode(claModel.ClassName) + " - " + Page.Header.Title;
            //    //先清除母版页设置的keywords和description
            //    Page.Header.Controls.Remove(Page.Header.FindControl("keywords"));
            //    Page.Header.Controls.Remove(Page.Header.FindControl("description"));
            //    Page.Header.Controls.Add(Config.SetKeywords(Server.HtmlEncode(claModel.Keywords)));
            //    Page.Header.Controls.Add(Config.SetDescription(Server.HtmlEncode(claModel.Description)));
            //    //
            //    //litClassName.Text = claModel.ClassName;
            //}

            ////列表绑定
            //string sql = "select * from t_Guestbook where IsClose=0 and IsReply=1 " + SqlQuery + " order by AddTime desc";
            //pager.InnerHtml = Factory.Acc().DataPageBindForCn(sql, null,Config.DataBindObjTypeCollection.Repeater.ToString(), repList, PageSize, page, "?" + UrlPara).ToString();
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            //string strVerifyCode = txtVerifyCode.Text.Trim();

            //if (Session["VerifyCode"] == null)
            //{
            //    errMsg.Text = "验证码有误";
            //}
            //else
            //{
            //    if (strVerifyCode != Session["VerifyCode"].ToString())
            //    {
            //        errMsg.Text = "验证码有误";
            //    }
            //    else
            //    {
            GuestbookModel gbookModel = new GuestbookModel();
            gbookModel.NickName = txtNickName.Value.Trim();
            gbookModel.TelePhone = txtTelePhone.Value;
            gbookModel.Email = txtEmail.Value;
            gbookModel.BookContent = Config.HTMLCls(txtBookContent.Value.Trim());
            gbookModel.IpAddress = Request.UserHostAddress.ToString();
            gbookModel.AddTime = DateTime.Now.ToString();
            gbookModel.IsReply = "0";
            gbookModel.ReplyContent = "";
            gbookModel.ReplyTime = "1900-1-1";
            gbookModel.AdminID = "0";
            gbookModel.IsClose = "0";
            if (gbookModel.NickName == string.Empty)
            {
                errMsg.Text = "请输入昵称!";
            }
            else if (gbookModel.BookContent == string.Empty)
            {
                errMsg.Text = "请输入留言内容!";
            }
            else
            {
                Factory.Guestbook().InsertInfo(gbookModel);
                Config.MsgGotoUrl("留言成功,请等待回复！", Request.UrlReferrer.ToString());
            }
            // }
            //}
        }

    }
}