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
        /// ������:��С��
        /// ����:2010-12-6
        /// </summary>
        //����ȫ�ֱ���
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
        #region ****�������****
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
        #region ****�������****
        public string SqlOrder
        {
            get
            {
                return " order by " + strOrderKey + " " + strAscDesc1;
            }
        }
        #endregion
        #region ****Url�������****
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
        #region ****��ѯ����****
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
        #region ****��ѯ���****
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
        #region****DbParameter����****
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
        #region ****Url����****
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
        //ҳ���ʼ��
        protected void Page_Load(object sender, EventArgs e)
        {
            Factory.Admin().LoginChk();
            if (!Page.IsPostBack)
            {
                //���԰汾
                GetData.GetConfigList(drpConfigID, "LanguageVer", "ConfigID");
                this.drpConfigID.Items.Insert(0, new ListItem("��ѡ��", "-1"));

                //��Ŀ����
                Factory.Acc().DataBind("select * from t_ClassProperty where IsClose=0 order by ListID asc", null, Config.DataBindObjTypeCollection.DropDownList.ToString(), drpClassPropertyID, "PropertyName", "ClassPropertyID");
                drpClassPropertyID.Items.Insert(0, new ListItem("��ѡ��", "-1"));

                //��Ŀģ��
                drpClassTemplateID.Items.Insert(0, new ListItem("��ѡ��", "-1"));

                if (ClassID == "0")
                {
                    GetData.LimitChkMsg("ClassAdd");
                    lblTitle.Text = "���";

                    lblParent.Text = Factory.Class().ShowPath(ParentID).ToString();
                    string strListID = Factory.Class().GetListID(ParentID);
                    txtListID.Text = strListID;
                    hidlistID.Value = strListID;

                    trSingle.Visible = false;
                    trSingle2.Visible = false;


                    //�̳����԰汾.��Ŀ����.��Ŀ����
                    ClassModel claModel = Factory.Class().GetInfo(ParentID);
                    if (claModel != null)
                    {
                        //���԰汾
                        Config.setDefaultSelected(drpConfigID, claModel.ConfigID);
                        //��Ŀ����
                        Config.setDefaultSelected(drpClassPropertyID, claModel.ClassPropertyID);
                        //��Ŀģ��
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
                        //��Ŀ����
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
                    lblTitle.Text = "�޸�";
                    ShowInfo();
                }
            }
        }
        //��������
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
            //��Ŀ����
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
                        Factory.AdminLog().InsertLog("�������Ϊ\"" + claModel.ClassName + "\"����Ŀ��", Session["AdminID"].ToString());
                        Config.MsgGotoUrl("��ӳɹ���", "Class.aspx?ParentID=" + claModel.ParentID);
                    }
                    else
                    {
                        errMsg.Text = "�Ѵ�����ͬ��������!";
                    }
                }
                else
                {
                    errMsg.Text = "�Ѵ�����ͬ��Ŀ!";
                }
            }
            else
            {
                ClassModel claModel_2 = new ClassModel();
                claModel_2 = Factory.Class().GetInfo(ClassID);
                if (claModel_2 != null)
                {
                    if (GetData.CheckAdminID(claModel_2.AdminID, "ClassAll"))//��鴴����
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
                                Factory.AdminLog().InsertLog("�޸ı��Ϊ" + ClassID + "����Ŀ��", Session["AdminID"].ToString());
                                Config.MsgGotoUrl("�޸ĳɹ���", "Class.aspx?ParentID=" + claModel.ParentID + "&" + UrlOrderPara + UrlPara + "page=" + page);
                            }
                            else
                            {
                                errMsg.Text = "�Ѵ�����ͬ��������!";
                            }
                        }
                        else
                        {
                            errMsg.Text = "�Ѵ�����ͬ��Ŀ!";
                        }
                    }
                }
            }
        }

        //��ʾ����
        protected void ShowInfo()
        {
            ClassModel claModel = new ClassModel();
            claModel = Factory.Class().GetInfo(ClassID);
            if (claModel != null)
            {
                if (GetData.CheckAdminID(claModel.AdminID, "ClassAll"))//��鴴����
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
                    //��Ŀ����
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
                    Config.ShowEnd("��û�в鿴����Ϣ��Ȩ��");
                }
            }
            else
            {
                Config.ShowEnd("��û�в鿴����Ϣ��Ȩ�ޣ�");
            }
        }


        private void ClassTemplate_List(string strClassPropertyID)
        {
            Factory.Acc().DataBind("select * from t_ClassTemplate where ClassPropertyID=" + strClassPropertyID + " and IsClose=0 order by ListID asc", null, Config.DataBindObjTypeCollection.DropDownList.ToString(), drpClassTemplateID, "TemplateName", "ClassTemplateID");
            drpClassTemplateID.Items.Insert(0, new ListItem("��ѡ��", "-1"));
        }

        //������Ŀ������ʾ������
        protected void drpClassPropertyID_SelectedIndexChanged(object sender, EventArgs e)
        {
            //��ҳ��
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

            //��ϸ����
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

            //��Ŀģ��
            ClassTemplate_List(drpClassPropertyID.SelectedValue);
        }

    }
}
