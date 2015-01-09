using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using HxSoft.Common;
using HxSoft.Model;
using HxSoft.ClassFactory;
using HxSoft.Web.cn.UserControl;
using Newtonsoft.Json;
using System.IO;

namespace HxSoft.Web.cn
{
    /// <summary>
    /// 栏目判断页
    /// </summary>
    public partial class about : System.Web.UI.Page
    {
        /// <summary>
        /// 栏目英文名
        /// </summary>
        public string ClassEnName
        {
            get
            {
                string strClassEnName = Config.Request(Request.QueryString["ClassEnName"], "about");
                return strClassEnName;
            }
        }
        /// <summary>
        /// 栏目ID
        /// </summary>
        public string ClassID
        {
            get
            {
                Site_Config config = (Site_Config)base.Master.FindControl("Site1");
                string configID = config.ConfigID;//查找语言版本ID
                string strTempClassID = Factory.Class().GetClassIDByClassEnName(configID, ClassEnName);
                string strReturn = strTempClassID;
                ClassModel claModel = Factory.Class().GetCacheInfo2(strTempClassID);
                if (claModel != null)
                {
                    if (claModel.IsGoToFirst == "1" && Convert.ToInt32(claModel.ChildNum) > 0)//判断是否跳到子栏目
                    {
                        strReturn = Factory.Class().GetFirstClassID(strTempClassID);
                    }
                }
                else
                {
                    Response.Redirect("index" + Config.FileExt);//未找到栏目,则跳转到首页
                }
                return strReturn;
            }
        }
        /// <summary>
        /// 栏目路径
        /// </summary>
        public string ClassPath
        {
            get
            {
                return Factory.Class().GetPath(ClassID).ToString();
            }
        }

        /// <summary>
        /// 主栏目ID
        /// </summary>
        public string ParentID
        {
            get
            {
                return Factory.Class().GetClassIDByPath(ClassPath, 0, ClassID);
            }
        }
        /// <summary>
        /// 当前主栏目ID
        /// </summary>
        public string CurrentParentID
        {
            get
            {
                return Factory.Class().GetClassIDByPath(ClassPath, 0, ClassID);
            }
        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            WUC_Header WUC_Header1 = (WUC_Header)this.Master.FindControl("WUC_Header1");
            WUC_Header1.CurrentParentID = CurrentParentID;
            //WUC_Banner1.ParentID = ParentID;
            //WUC_Banner1.ClassID = ClassID;
            //WUC_Left1.ParentID = ParentID;
            //WUC_Left1.ClassID = ClassID;
            //WUC_Left1.ClassPath = ClassPath;
            //WUC_Nav1.ClassID = ClassID;
            //WUC_Nav1.ClassPath = ClassPath;

            ClassModel claModel = new ClassModel();
            claModel = Factory.Class().GetCacheInfo2(ClassID);
            if (claModel != null)
            {
                //外链
                if (claModel.ClassPropertyID == Config.SysOuterLinkMouldID)
                {
                    Response.Redirect(claModel.LinkUrl);
                }
                else
                {
                    string strTemplatePath = Factory.ClassTemplate().GetValueByField("TemplatePath", claModel.ClassTemplateID);
                    if (!File.Exists(Server.MapPath(strTemplatePath)))
                    {
                        Config.ShowEnd("模板不存在!");
                    }
                    else
                    {
                        //栏目参数
                        ClassConfig claConfig = (ClassConfig)JsonConvert.DeserializeObject(claModel.ClassConfig, typeof(ClassConfig));
                        //单页面
                        if (claModel.ClassPropertyID == Config.SysSinglePageMouldID)
                        {
                            WUC_Class_Content WUC_Class_Content1 = (WUC_Class_Content)LoadControl(strTemplatePath);
                            WUC_Class_Content1.ClassID = ClassID;
                            panelBox.Controls.Add(WUC_Class_Content1);
                        }
                        //文章
                        else if (claModel.ClassPropertyID == Config.SysArticleMouldID)
                        {
                            WUC_Article_List WUC_Article_List1 = (WUC_Article_List)LoadControl(strTemplatePath);
                            WUC_Article_List1.ClassID = ClassID;
                            WUC_Article_List1.PageSize = claConfig.PageSize;
                            WUC_Article_List1.TopNum = claConfig.TopNum;
                            WUC_Article_List1.IsShowSub = claConfig.IsShowSub;
                            WUC_Article_List1.IsOnlyRecommend = claConfig.IsOnlyRecommend;
                            WUC_Article_List1.OrderField = claConfig.OrderField;
                            WUC_Article_List1.OrderKey = claConfig.OrderKey;
                            WUC_Article_List1.TitleNum = claConfig.TitleNum;
                            WUC_Article_List1.DataLink = claConfig.DataLink;
                            WUC_Article_List1.StyleClass = claConfig.StyleClass;
                            
                            panelBox.Controls.Add(WUC_Article_List1);
                        }
                        //产品
                        else if (claModel.ClassPropertyID == Config.SysProductMouldID)
                        {
                            WUC_Product_List WUC_Product_List1 = (WUC_Product_List)LoadControl(strTemplatePath);
                            WUC_Product_List1.ClassID = ClassID;
                            WUC_Product_List1.PageSize = claConfig.PageSize;
                            WUC_Product_List1.TopNum = claConfig.TopNum;
                            WUC_Product_List1.IsShowSub = claConfig.IsShowSub;
                            WUC_Product_List1.IsOnlyRecommend = claConfig.IsOnlyRecommend;
                            WUC_Product_List1.OrderField = claConfig.OrderField;
                            WUC_Product_List1.OrderKey = claConfig.OrderKey;
                            WUC_Product_List1.TitleNum = claConfig.TitleNum;
                            WUC_Product_List1.DataLink = claConfig.DataLink;
                            WUC_Product_List1.StyleClass = claConfig.StyleClass;
                            panelBox.Controls.Add(WUC_Product_List1);
                        }
                        //招聘
                        else if (claModel.ClassPropertyID == Config.SysJobMouldID)
                        {
                            WUC_Job_List WUC_Job_List1 = (WUC_Job_List)LoadControl(strTemplatePath);
                            WUC_Job_List1.ClassID = ClassID;
                            WUC_Job_List1.PageSize = claConfig.PageSize;
                            WUC_Job_List1.TopNum = claConfig.TopNum;
                            WUC_Job_List1.IsShowSub = claConfig.IsShowSub;
                            WUC_Job_List1.IsOnlyRecommend = claConfig.IsOnlyRecommend;
                            WUC_Job_List1.OrderField = claConfig.OrderField;
                            WUC_Job_List1.OrderKey = claConfig.OrderKey;
                            WUC_Job_List1.TitleNum = claConfig.TitleNum;
                            WUC_Job_List1.DataLink = claConfig.DataLink;
                            WUC_Job_List1.StyleClass = claConfig.StyleClass;
                            panelBox.Controls.Add(WUC_Job_List1);
                        }
                        //下载
                        else if (claModel.ClassPropertyID == Config.SysDownloadMouldID)
                        {
                            WUC_Download_List WUC_Download_List1 = (WUC_Download_List)LoadControl(strTemplatePath);
                            WUC_Download_List1.ClassID = ClassID;
                            WUC_Download_List1.PageSize = claConfig.PageSize;
                            WUC_Download_List1.TopNum = claConfig.TopNum;
                            WUC_Download_List1.IsShowSub = claConfig.IsShowSub;
                            WUC_Download_List1.IsOnlyRecommend = claConfig.IsOnlyRecommend;
                            WUC_Download_List1.OrderField = claConfig.OrderField;
                            WUC_Download_List1.OrderKey = claConfig.OrderKey;
                            WUC_Download_List1.TitleNum = claConfig.TitleNum;
                            WUC_Download_List1.DataLink = claConfig.DataLink;
                            WUC_Download_List1.StyleClass = claConfig.StyleClass;
                            panelBox.Controls.Add(WUC_Download_List1);
                        }
                        //调查
                        else if (claModel.ClassPropertyID == Config.SysSurveyMouldID)
                        {
                            WUC_Survey_List WUC_Survey_List1 = (WUC_Survey_List)LoadControl(strTemplatePath);
                            WUC_Survey_List1.ClassID = ClassID;
                            WUC_Survey_List1.PageSize = claConfig.PageSize;
                            WUC_Survey_List1.TopNum = claConfig.TopNum;
                            WUC_Survey_List1.IsShowSub = claConfig.IsShowSub;
                            WUC_Survey_List1.IsOnlyRecommend = claConfig.IsOnlyRecommend;
                            WUC_Survey_List1.OrderField = claConfig.OrderField;
                            WUC_Survey_List1.OrderKey = claConfig.OrderKey;
                            WUC_Survey_List1.TitleNum = claConfig.TitleNum;
                            WUC_Survey_List1.DataLink = claConfig.DataLink;
                            WUC_Survey_List1.StyleClass = claConfig.StyleClass;
                            panelBox.Controls.Add(WUC_Survey_List1);
                        }
                        //友情链接
                        else if (claModel.ClassPropertyID == Config.SysLinkMouldID)
                        {
                            WUC_Link WUC_Link1 = (WUC_Link)LoadControl(strTemplatePath);
                            WUC_Link1.ClassID = ClassID;
                            WUC_Link1.ConfigID = claModel.ConfigID;
                            panelBox.Controls.Add(WUC_Link1);
                        }
                        //网站地图
                        else if (claModel.ClassPropertyID == Config.SysSitemapMouldID)
                        {
                            WUC_Sitemap WUC_Sitemap1 = (WUC_Sitemap)LoadControl(strTemplatePath);
                            WUC_Sitemap1.ConfigID = claModel.ConfigID;
                            WUC_Sitemap1.ClassID = ClassID;
                            panelBox.Controls.Add(WUC_Sitemap1);
                        }
                        //信息反馈
                        else if (claModel.ClassPropertyID == Config.SysFeedbackMouldID)
                        {
                            WUC_Feedback WUC_Feedback1 = (WUC_Feedback)LoadControl(strTemplatePath);
                            WUC_Feedback1.ConfigID = claModel.ConfigID;
                            WUC_Feedback1.ClassID = ClassID;
                            panelBox.Controls.Add(WUC_Feedback1);
                        }
                        //留言本
                        else if (claModel.ClassPropertyID == Config.SysGuestbookMouldID)
                        {
                            WUC_Guestbook WUC_Guestbook1 = (WUC_Guestbook)LoadControl(strTemplatePath);
                            WUC_Guestbook1.ClassID = ClassID;
                            WUC_Guestbook1.PageSize = claConfig.PageSize;
                            panelBox.Controls.Add(WUC_Guestbook1);
                        }
                        //视频
                        else if (claModel.ClassPropertyID == Config.SysVideoMouldID)
                        {
                            WUC_Video_List WUC_Video_List1 = (WUC_Video_List)LoadControl(strTemplatePath);
                            WUC_Video_List1.ClassID = ClassID;
                            WUC_Video_List1.PageSize = claConfig.PageSize;
                            WUC_Video_List1.TopNum = claConfig.TopNum;
                            WUC_Video_List1.IsShowSub = claConfig.IsShowSub;
                            WUC_Video_List1.IsOnlyRecommend = claConfig.IsOnlyRecommend;
                            WUC_Video_List1.OrderField = claConfig.OrderField;
                            WUC_Video_List1.OrderKey = claConfig.OrderKey;
                            WUC_Video_List1.TitleNum = claConfig.TitleNum;
                            WUC_Video_List1.DataLink = claConfig.DataLink;
                            WUC_Video_List1.StyleClass = claConfig.StyleClass;
                            panelBox.Controls.Add(WUC_Video_List1);
                        }
                        //相册
                        else if (claModel.ClassPropertyID == Config.SysPhotoMouldID)
                        {
                            WUC_Photo_List WUC_Photo_List1 = (WUC_Photo_List)LoadControl(strTemplatePath);
                            WUC_Photo_List1.ClassID = ClassID;
                            WUC_Photo_List1.PageSize = claConfig.PageSize;
                            WUC_Photo_List1.TopNum = claConfig.TopNum;
                            WUC_Photo_List1.IsShowSub = claConfig.IsShowSub;
                            WUC_Photo_List1.IsOnlyRecommend = claConfig.IsOnlyRecommend;
                            WUC_Photo_List1.OrderField = claConfig.OrderField;
                            WUC_Photo_List1.OrderKey = claConfig.OrderKey;
                            WUC_Photo_List1.TitleNum = claConfig.TitleNum;
                            WUC_Photo_List1.DataLink = claConfig.DataLink;
                            WUC_Photo_List1.StyleClass = claConfig.StyleClass;
                            panelBox.Controls.Add(WUC_Photo_List1);
                        }
                    }
                }
            }
        }
    }
}