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
using System.Text;
using HxSoft.Common;
using HxSoft.Model;
using HxSoft.ClassFactory;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using System.Data.Common;

namespace HxSoft.Web.Admin._System
{
    public partial class Class_Add : System.Web.UI.Page
    {
        /// <summary>
        /// 创建人:杨小明
        /// 日期:2010-12-6
        /// </summary>
        //定义全局变量
        public string ClassID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["ClassID"], 0).ToString();
            }
        }
        public int page
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["page"], 1);
            }
        }
        public string ParentID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["ParentID"], 0).ToString();
            }
        }
        #region ****排序参数****
        public string strOrderKey
        {
            get
            {
                return Config.Request(Request["OrderKey"], "ListID");
            }
        }
        public string strAscDesc1
        {
            get
            {
                return Config.Request(Request["AscDesc"], "asc");
            }
        }
        public string strAscDesc2
        {
            get
            {
                if (strAscDesc1 == "asc")
                    return "desc";
                else
                    return "asc";
            }
        }
        #endregion
        #region ****排序语句****
        public string SqlOrder
        {
            get
            {
                return " order by " + strOrderKey + " " + strAscDesc1;
            }
        }
        #endregion
        #region ****Url排序参数****
        public string UrlOrderPara
        {
            get
            {
                StringBuilder TempUrl = new StringBuilder("");
                TempUrl.Append("OrderKey=" + Server.UrlEncode(strOrderKey) + "&");
                TempUrl.Append("AscDesc=" + Server.UrlEncode(strAscDesc1) + "&");
                return TempUrl.ToString();
            }
        }
        #endregion
        #region ****查询参数****
        public string strClassName
        {
            get
            {
                return Config.Request(Request["txtClassName"], "");
            }
        }
        public string strIsClose
        {
            get
            {
                return Config.Request(Request["radIsClose"], "-1");
            }
        }
        #endregion
        #region ****查询语句****
        public string SqlQuery
        {
            get
            {
                StringBuilder TempSql = new StringBuilder("");
                if (strClassName != "") TempSql.Append(" and ClassName like @ClassName");
                if (strIsClose != "-1") TempSql.Append(" and IsClose =@IsClose");
                return TempSql.ToString();
            }
        }
        #endregion
        #region****DbParameter参数****
        public DbParameter[] SqlParams
        {
            get
            {
                List<DbParameter> listParams = new List<DbParameter>();
                if (strClassName != "") listParams.Add(Config.Conn().CreateDbParameter("@ClassName", "%" + strClassName + "%"));
                if (strIsClose != "-1") listParams.Add(Config.Conn().CreateDbParameter("@IsClose", strIsClose));
                return listParams.ToArray();
            }
        }
        #endregion
        #region ****Url参数****
        public string UrlPara
        {
            get
            {
                StringBuilder TempUrl = new StringBuilder("");
                TempUrl.Append("txtClassName=" + Server.UrlEncode(strClassName) + "&");
                TempUrl.Append("radIsClose=" + Server.UrlEncode(strIsClose) + "&");
                return TempUrl.ToString();
            }
        }
        #endregion
        //页面初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            if (!Page.IsPostBack)
            {
                //语言版本
                GetData.GetConfigList(drpConfigID, "LanguageVer", "ConfigID");
                this.drpConfigID.Items.Insert(0, new ListItem("请选择", "-1"));

                //栏目属性
                Factory.Acc().DataBind("select * from t_ClassProperty where IsClose=0 order by ListID asc", null, Config.DataBindObjTypeCollection.DropDownList.ToString(), drpClassPropertyID, "PropertyName", "ClassPropertyID");
                drpClassPropertyID.Items.Insert(0, new ListItem("请选择", "-1"));

                //栏目模板
                drpClassTemplateID.Items.Insert(0, new ListItem("请选择", "-1"));

                if (ClassID == "0")
                {
                    GetData.LimitChkMsg("ClassAdd");
                    lblTitle.Text = "添加";

                    lblParent.Text = Factory.Class().ShowPath(ParentID).ToString();
                    string strListID = Factory.Class().GetListID(ParentID);
                    txtListID.Text = strListID;
                    hidlistID.Value = strListID;

                    trSingle.Visible = false;
                    trSingle2.Visible = false;


                    //继承语言版本.栏目属性.栏目参数
                    ClassModel claModel = Factory.Class().GetInfo(ParentID);
                    if (claModel != null)
                    {
                        //语言版本
                        Config.setDefaultSelected(drpConfigID, claModel.ConfigID);
                        //栏目属性
                        Config.setDefaultSelected(drpClassPropertyID, claModel.ClassPropertyID);
                        //栏目模板
                        ClassTemplate_List(claModel.ClassPropertyID);
                        Config.setDefaultSelected(drpClassTemplateID, claModel.ClassTemplateID);
                        if (claModel.ClassPropertyID == Config.SysSinglePageMouldID)
                        {
                            trSingle.Visible = true;
                            trSingle2.Visible = true;
                        }
                        else
                        {
                            trSingle.Visible = false;
                            trSingle2.Visible = false;
                        }
                        //栏目参数
                        ClassConfig claConfig = (ClassConfig)JsonConvert.DeserializeObject(claModel.ClassConfig, typeof(ClassConfig));
                        txtPageSize.Text = claConfig.PageSize.ToString();
                        txtTopNum.Text = claConfig.TitleNum.ToString();
                        chkIsShowSub.Checked = claConfig.IsShowSub;
                        chkIsOnlyRecommend.Checked = claConfig.IsOnlyRecommend;
                        Config.setDefaultSelected(drpOrderField, claConfig.OrderField);
                        Config.setDefaultSelected(drpOrderKey, claConfig.OrderKey);
                        txtTitleNum.Text = claConfig.TitleNum.ToString();
                        txtDataLink.Text = claConfig.DataLink;
                        txtStyleClass.Text = claConfig.StyleClass;
                    }
                }
                else
                {
                    GetData.LimitChkMsg("ClassEdit");
                    lblTitle.Text = "修改";
                    ShowInfo();
                }
            }
        }
        //保存数据
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ClassModel claModel = new ClassModel();
            string strOldListID = hidlistID.Value;
            claModel.ConfigID = drpConfigID.SelectedValue;
            claModel.ClassName = txtClassName.Text.Trim();
            claModel.ClassEnName = txtClassEnName.Text.Trim();
            claModel.ClassPropertyID = drpClassPropertyID.SelectedValue;
            claModel.ClassTemplateID = drpClassTemplateID.SelectedValue;
            claModel.ClassPic = txtClassPic.Text.Trim();
            claModel.LinkUrl = txtLinkUrl.Text.Trim();
            claModel.Target = drpTarget.SelectedValue.Trim();
            if (chkIsGoToFirst.Checked)
            {
                claModel.IsGoToFirst = "1";
            }
            else
            {
                claModel.IsGoToFirst = "0";
            }
            if (chkIsShowNav.Checked)
            {
                claModel.IsShowNav = "1";
            }
            else
            {
                claModel.IsShowNav = "0";
            }
            claModel.Keywords = txtKeywords.Text.Trim();
            claModel.Description = Config.HTMLCls(txtDescription.Text.Trim());
            claModel.ClassContent = txtClassContent.Value;
            //******************************
            //栏目参数
            StringWriter sw = new StringWriter();
            JsonTextWriter tw = new JsonTextWriter(sw);
            tw.WriteStartObject();
            tw.WritePropertyName("PageSize");
            tw.WriteValue(Convert.ToInt32(txtPageSize.Text));
            tw.WritePropertyName("TopNum");
            tw.WriteValue(Convert.ToInt32(txtTopNum.Text));
            tw.WritePropertyName("IsShowSub");
            tw.WriteValue(chkIsShowSub.Checked);
            tw.WritePropertyName("IsOnlyRecommend");
            tw.WriteValue(chkIsOnlyRecommend.Checked);
            tw.WritePropertyName("OrderField");
            tw.WriteValue(drpOrderField.SelectedValue);
            tw.WritePropertyName("OrderKey");
            tw.WriteValue(drpOrderKey.SelectedValue);
            tw.WritePropertyName("TitleNum");
            tw.WriteValue(Convert.ToInt32(txtTitleNum.Text));
            tw.WritePropertyName("DescNum");
            tw.WriteValue(Convert.ToInt32(txtDescNum.Text));
            tw.WritePropertyName("DataLink");
            tw.WriteValue(txtDataLink.Text);
            tw.WritePropertyName("StyleClass");
            tw.WriteValue(txtStyleClass.Text);
            tw.WriteEndObject();
            tw.Flush();
            string json = sw.ToString();
            claModel.ClassConfig = json;
            //******************************
            claModel.ParentID = ParentID;
            claModel.ChildNum = "0";
            claModel.ListID = txtListID.Text.Trim();
            claModel.AdminID = Session["AdminID"].ToString();
            claModel.AddTime = DateTime.Now.ToString();
            claModel.IsClose = radIsClose.SelectedValue;
            if (ClassID == "0")
            {
                if (!Factory.Class().CheckInfo2("ClassName", claModel.ClassName, claModel.ParentID))
                {
                    if (!Factory.Class().CheckInfo("ClassEnName", claModel.ClassEnName, claModel.ConfigID))
                    {
                        Factory.Class().OrderInfo(claModel.ParentID, claModel.ListID, strOldListID);
                        Factory.Class().InsertInfo(claModel);
                        Factory.AdminLog().InsertLog("添加名称为\"" + claModel.ClassName + "\"的栏目。", Session["AdminID"].ToString());
                        Config.MsgGotoUrl("添加成功！", "Class.aspx?ParentID=" + claModel.ParentID);
                    }
                    else
                    {
                        errMsg.Text = "已存在相同链接名称!";
                    }
                }
                else
                {
                    errMsg.Text = "已存在相同栏目!";
                }
            }
            else
            {
                ClassModel claModel_2 = new ClassModel();
                claModel_2 = Factory.Class().GetInfo(ClassID);
                if (claModel_2 != null)
                {
                    if (GetData.CheckAdminID(claModel_2.AdminID, "ClassAll"))//检查创建者
                    {
                        if (!Factory.Class().CheckInfo2("ClassName", claModel.ClassName, claModel.ParentID, ClassID))
                        {
                            if (!Factory.Class().CheckInfo("ClassEnName", claModel.ClassEnName, claModel.ConfigID, ClassID))
                            {
                                Factory.Class().OrderInfo(claModel.ParentID, claModel.ListID, strOldListID);
                                Factory.Class().UpdateInfo(claModel, ClassID);
                                if (chkIsUpdateSubClassConfig.Checked)
                                {
                                    Factory.Class().UpdateSubClassConfig(ClassID, claModel.ClassConfig, claModel.ClassPropertyID);
                                    CacheHelper.RemoveAllCache();
                                }
                                Factory.AdminLog().InsertLog("修改编号为" + ClassID + "的栏目。", Session["AdminID"].ToString());
                                Config.MsgGotoUrl("修改成功！", "Class.aspx?ParentID=" + claModel.ParentID + "&" + UrlOrderPara + UrlPara + "page=" + page);
                            }
                            else
                            {
                                errMsg.Text = "已存在相同链接名称!";
                            }
                        }
                        else
                        {
                            errMsg.Text = "已存在相同栏目!";
                        }
                    }
                }
            }
        }

        //显示数据
        protected void ShowInfo()
        {
            ClassModel claModel = new ClassModel();
            claModel = Factory.Class().GetInfo(ClassID);
            if (claModel != null)
            {
                if (GetData.CheckAdminID(claModel.AdminID, "ClassAll"))//检查创建者
                {
                    Config.setDefaultSelected(drpConfigID, claModel.ConfigID);
                    txtClassName.Text = claModel.ClassName;
                    txtClassEnName.Text = claModel.ClassEnName;
                    Config.setDefaultSelected(drpClassPropertyID, claModel.ClassPropertyID);
                    ClassTemplate_List(claModel.ClassPropertyID);
                    Config.setDefaultSelected(drpClassTemplateID, claModel.ClassTemplateID);
                    if (claModel.ClassPropertyID == Config.SysSinglePageMouldID)
                    {
                        trSingle.Visible = true;
                        trSingle2.Visible = true;
                    }
                    else
                    {
                        trSingle.Visible = false;
                        trSingle2.Visible = false;
                    }
                    lblParent.Text = Factory.Class().ShowPath(claModel.ParentID).ToString();
                    txtClassPic.Text = claModel.ClassPic;
                    txtLinkUrl.Text = claModel.LinkUrl;
                    Config.setDefaultSelected(drpTarget, claModel.Target);
                    if (claModel.IsGoToFirst == "1")
                    {
                        chkIsGoToFirst.Checked = true;
                    }
                    else
                    {
                        chkIsGoToFirst.Checked = false;
                    }
                    if (claModel.IsShowNav == "1")
                    {
                        chkIsShowNav.Checked = true;
                    }
                    else
                    {
                        chkIsShowNav.Checked = false;
                    }
                    txtKeywords.Text = claModel.Keywords;
                    txtDescription.Text = Config.HTMLToTextarea(claModel.Description);
                    txtClassContent.Value = claModel.ClassContent;
                    //栏目参数
                    ClassConfig claConfig = (ClassConfig)JsonConvert.DeserializeObject(claModel.ClassConfig, typeof(ClassConfig));
                    txtPageSize.Text = claConfig.PageSize.ToString();
                    txtTopNum.Text = claConfig.TopNum.ToString();
                    chkIsShowSub.Checked = claConfig.IsShowSub;
                    chkIsOnlyRecommend.Checked = claConfig.IsOnlyRecommend;
                    Config.setDefaultSelected(drpOrderField, claConfig.OrderField);
                    Config.setDefaultSelected(drpOrderKey, claConfig.OrderKey);
                    txtTitleNum.Text = claConfig.TitleNum.ToString();
                    txtDataLink.Text = claConfig.DataLink;
                    txtStyleClass.Text = claConfig.StyleClass;
                    //++++++++
                    //txtChildNum.Text = claModel.ChildNum;
                    txtListID.Text = claModel.ListID;
                    hidlistID.Value = claModel.ListID;
                    //txtAddAdminID.Text = claModel.AddAdminID;
                    //txtAddTime.Text = claModel.AddTime;
                    radIsClose.ClearSelection();
                    Config.setDefaultSelected(radIsClose, claModel.IsClose);
                }
                else
                {
                    Config.ShowEnd("您没有查看此信息的权限");
                }
            }
            else
            {
                Config.ShowEnd("您没有查看此信息的权限！");
            }
        }


        private void ClassTemplate_List(string strClassPropertyID)
        {
            Factory.Acc().DataBind("select * from t_ClassTemplate where ClassPropertyID=" + strClassPropertyID + " and IsClose=0 order by ListID asc", null, Config.DataBindObjTypeCollection.DropDownList.ToString(), drpClassTemplateID, "TemplateName", "ClassTemplateID");
            drpClassTemplateID.Items.Insert(0, new ListItem("请选择", "-1"));
        }

        //根据栏目属性显示或隐藏
        protected void drpClassPropertyID_SelectedIndexChanged(object sender, EventArgs e)
        {
            //单页面
            if (drpClassPropertyID.SelectedValue == Config.SysSinglePageMouldID)
            {
                trSingle.Visible = true;
                trSingle2.Visible = true;
            }
            else
            {
                trSingle.Visible = false;
                trSingle2.Visible = false;
            }

            //详细链接
            if (drpClassPropertyID.SelectedValue == Config.SysArticleMouldID)
            {
                txtDataLink.Text = "article-details-";
            }
            else if (drpClassPropertyID.SelectedValue == Config.SysProductMouldID)
            {
                txtDataLink.Text = "product-details-";
            }
            else if (drpClassPropertyID.SelectedValue == Config.SysJobMouldID)
            {
                txtDataLink.Text = "job-details-";
            }
            else if (drpClassPropertyID.SelectedValue == Config.SysDownloadMouldID)
            {
                txtDataLink.Text = "download-details-";
            }
            else if (drpClassPropertyID.SelectedValue == Config.SysSurveyMouldID)
            {
                txtDataLink.Text = "survey-details-";
            }
            else if (drpClassPropertyID.SelectedValue == Config.SysVideoMouldID)
            {
                txtDataLink.Text = "video-details-";
            }
            else
            {
                txtDataLink.Text = txtClassEnName.Text;
            }

            //栏目模板
            ClassTemplate_List(drpClassPropertyID.SelectedValue);
        }

    }
}
