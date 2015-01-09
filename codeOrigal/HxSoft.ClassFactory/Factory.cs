using HxSoft.BLL;

namespace HxSoft.ClassFactory
{
    public class Factory
    {
        //私有变量
        private static AccBLL _accbll;
        private static AdBLL _adbll;
        private static AdminBLL _adminbll;
        private static AdminGroupBLL _admingroupbll;
        private static AdminInGroupBLL _adminingroupbll;
        private static AdminLogBLL _adminlogbll;
        private static AdPositionBLL _adpositionbll;
        private static AreaBLL _areabll;
        private static ArticleBLL _articlebll;
        private static ArticlePicBLL _articlepicbll;
        private static ChatBLL _chatbll;
        private static ClassBLL _classbll;
        private static ClassPropertyBLL _classpropertybll;
        private static ClassTemplateBLL _classtemplatebll;
        private static ConfigBLL _configbll;
        private static DictionaryBLL _dictionarybll;
        private static MessageBLL _messagebll;
        private static IndustryBLL _industrybll;
        private static LimitBLL _limitbll;
        private static LinkBLL _linkbll;
        private static PathBLL _pathbll;
        private static UserBLL _userbll;
        private static UserLogBLL _userlogbll;
        private static UserRankBLL _userrankbll;

        private static FeedbackBLL _feedbackbll;
        private static JobBLL _jobbll;
        private static DownloadBLL _downloadbll;
        private static ProductBLL _productbll;
        private static ProductPicBLL _productpicbll;
        private static MailBLL _mailbll;
        private static CollectionBLL _collectionbll;
        private static GuestbookBLL _guestbookbll;

        private static SurveyBLL _surveybll;
        private static SurveyItemBLL _surveyitembll;
        private static SurveyItemOptionBLL _surveyitemoptionbll;
        private static SurveyResultBLL _surveyresultbll;

        private static VideoBLL _videobll;
        private static PhotoBLL _photobll;
        private static SetBLL _setbll;
        private static BlockBLL _blockbll;

        //类初始化
        public Factory()
        {
        }

        #region 类工厂方法
        #region Acc
        /// <summary>
        /// Acc类,返回DataTable,记录总数,数据绑定
        /// </summary>
        /// <returns></returns>
        public static AccBLL Acc()
        {
            if (_accbll == null)
            {
                _accbll = new AccBLL();
            }
            return _accbll;
        }
        #endregion
        #region 广告管理
        /// <summary>
        /// 广告管理
        /// </summary>
        /// <returns></returns>
        public static AdBLL Ad()
        {
            if (_adbll == null)
            {
                _adbll = new AdBLL();
            }
            return _adbll;
        }
        #endregion
        #region 管理员管理
        /// <summary>
        /// 管理员管理
        /// </summary>
        /// <returns></returns>
        public static AdminBLL Admin()
        {
            if (_adminbll == null)
            {
                _adminbll = new AdminBLL();
            }
            return _adminbll;
        }
        #endregion
        #region 管理组管理
        /// <summary>
        /// 管理组管理
        /// </summary>
        /// <returns></returns>
        public static AdminGroupBLL AdminGroup()
        {
            if (_admingroupbll == null)
            {
                _admingroupbll = new AdminGroupBLL();
            }
            return _admingroupbll;
        }
        #endregion
        #region 管理员管理组管理
        /// <summary>
        /// 管理员管理组管理
        /// </summary>
        /// <returns></returns>
        public static AdminInGroupBLL AdminInGroup()
        {
            if (_adminingroupbll == null)
            {
                _adminingroupbll = new AdminInGroupBLL();
            }
            return _adminingroupbll;
        }
        #endregion
        #region 管理员日志管理
        /// <summary>
        /// 管理员日志管理
        /// </summary>
        /// <returns></returns>
        public static AdminLogBLL AdminLog()
        {
            if (_adminlogbll == null)
            {
                _adminlogbll = new AdminLogBLL();
            }
            return _adminlogbll;
        }
        #endregion
        #region 广告位管理
        /// <summary>
        /// 广告位管理
        /// </summary>
        /// <returns></returns>
        public static AdPositionBLL AdPosition()
        {
            if (_adpositionbll == null)
            {
                _adpositionbll = new AdPositionBLL();
            }
            return _adpositionbll;
        }
        #endregion
        #region 地区管理
        /// <summary>
        /// 地区管理
        /// </summary>
        /// <returns></returns>
        public static AreaBLL Area()
        {
            if (_areabll == null)
            {
                _areabll = new AreaBLL();
            }
            return _areabll;
        }
        #endregion
        #region 文章管理
        /// <summary>
        /// 文章管理
        /// </summary>
        /// <returns></returns>
        public static ArticleBLL Article()
        {
            if (_articlebll == null)
            {
                _articlebll = new ArticleBLL();
            }
            return _articlebll;
        }
        #endregion
        #region 文章图片管理
        /// <summary>
        /// 文章图片管理
        /// </summary>
        /// <returns></returns>
        public static ArticlePicBLL ArticlePic()
        {
            if (_articlepicbll == null)
            {
                _articlepicbll = new ArticlePicBLL();
            }
            return _articlepicbll;
        }
        #endregion
        #region 聊天工具
        /// <summary>
        /// 聊天工具
        /// </summary>
        /// <returns></returns>
        public static ChatBLL Chat()
        {
            if (_chatbll == null)
            {
                _chatbll = new ChatBLL();
            }
            return _chatbll;
        }
        #endregion
        #region 栏目管理
        /// <summary>
        /// 栏目管理
        /// </summary>
        /// <returns></returns>
        public static ClassBLL Class()
        {
            if (_classbll == null)
            {
                _classbll = new ClassBLL();
            }
            return _classbll;
        }
        #endregion
        #region 栏目属性
        /// <summary>
        /// 栏目属性
        /// </summary>
        /// <returns></returns>
        public static ClassPropertyBLL ClassProperty()
        {
            if (_classpropertybll == null)
            {
                _classpropertybll = new ClassPropertyBLL();
            }
            return _classpropertybll;
        }
        #endregion
        #region 栏目模板
        /// <summary>
        /// 栏目模板
        /// </summary>
        /// <returns></returns>
        public static ClassTemplateBLL ClassTemplate()
        {
            if (_classtemplatebll == null)
            {
                _classtemplatebll = new ClassTemplateBLL();
            }
            return _classtemplatebll;
        }
        #endregion
        #region 网站配置
        /// <summary>
        /// 网站配置
        /// </summary>
        /// <returns></returns>
        public static ConfigBLL Config()
        {
            if (_configbll == null)
            {
                _configbll = new ConfigBLL();
            }
            return _configbll;
        }
        #endregion
        #region 数据字典
        /// <summary>
        /// 数据字典
        /// </summary>
        /// <returns></returns>
        public static DictionaryBLL Dictionary()
        {
            if (_dictionarybll == null)
            {
                _dictionarybll = new DictionaryBLL();
            }
            return _dictionarybll;
        }
        #endregion
        #region 会员留言
        /// <summary>
        /// 会员留言
        /// </summary>
        /// <returns></returns>
        public static MessageBLL Message()
        {
            if (_messagebll == null)
            {
                _messagebll = new MessageBLL();
            }
            return _messagebll;
        }
        #endregion
        #region 行业管理
        /// <summary>
        /// 行业管理
        /// </summary>
        /// <returns></returns>
        public static IndustryBLL Industry()
        {
            if (_industrybll == null)
            {
                _industrybll = new IndustryBLL();
            }
            return _industrybll;
        }
        #endregion
        #region 权限字段
        /// <summary>
        /// 权限字段
        /// </summary>
        /// <returns></returns>
        public static LimitBLL Limit()
        {
            if (_limitbll == null)
            {
                _limitbll = new LimitBLL();
            }
            return _limitbll;
        }
        #endregion
        #region 友情链接
        /// <summary>
        /// 友情链接
        /// </summary>
        /// <returns></returns>
        public static LinkBLL Link()
        {
            if (_linkbll == null)
            {
                _linkbll = new LinkBLL();
            }
            return _linkbll;
        }
        #endregion
        #region 文件管理
        /// <summary>
        /// 文件管理
        /// </summary>
        /// <returns></returns>
        public static PathBLL Path()
        {
            if (_pathbll == null)
            {
                _pathbll = new PathBLL();
            }
            return _pathbll;
        }
        #endregion
        #region 会员管理
        /// <summary>
        /// 会员管理
        /// </summary>
        /// <returns></returns>
        public static UserBLL User()
        {
            if (_userbll == null)
            {
                _userbll = new UserBLL();
            }
            return _userbll;
        }
        #endregion
        #region 会员日志管理
        /// <summary>
        /// 会员日志管理
        /// </summary>
        /// <returns></returns>
        public static UserLogBLL UserLog()
        {
            if (_userlogbll == null)
            {
                _userlogbll = new UserLogBLL();
            }
            return _userlogbll;
        }
        #endregion
        #region 会员级别管理
        /// <summary>
        /// 会员级别管理
        /// </summary>
        /// <returns></returns>
        public static UserRankBLL UserRank()
        {
            if (_userrankbll == null)
            {
                _userrankbll = new UserRankBLL();
            }
            return _userrankbll;
        }
        #endregion

        #region 信息反馈
        /// <summary>
        /// 信息反馈
        /// </summary>
        /// <returns></returns>
        public static FeedbackBLL Feedback()
        {
            if (_feedbackbll == null)
            {
                _feedbackbll = new FeedbackBLL();
            }
            return _feedbackbll;
        }
        #endregion
        #region 招聘管理
        /// <summary>
        /// 招聘管理
        /// </summary>
        /// <returns></returns>
        public static JobBLL Job()
        {
            if (_jobbll == null)
            {
                _jobbll = new JobBLL();
            }
            return _jobbll;
        }
        #endregion
        #region 下载管理
        /// <summary>
        /// 下载管理
        /// </summary>
        /// <returns></returns>
        public static DownloadBLL Download()
        {
            if (_downloadbll == null)
            {
                _downloadbll = new DownloadBLL();
            }
            return _downloadbll;
        }
        #endregion
        #region 产品管理
        /// <summary>
        /// 产品管理
        /// </summary>
        /// <returns></returns>
        public static ProductBLL Product()
        {
            if (_productbll == null)
            {
                _productbll = new ProductBLL();
            }
            return _productbll;
        }
        #endregion
        #region 产品图片管理
        /// <summary>
        /// 产品图片管理
        /// </summary>
        /// <returns></returns>
        public static ProductPicBLL ProductPic()
        {
            if (_productpicbll == null)
            {
                _productpicbll = new ProductPicBLL();
            }
            return _productpicbll;
        }
        #endregion
        #region 邮件订阅
        /// <summary>
        /// 邮件订阅
        /// </summary>
        /// <returns></returns>
        public static MailBLL Mail()
        {
            if (_mailbll == null)
            {
                _mailbll = new MailBLL();
            }
            return _mailbll;
        }
        #endregion
        #region 收藏管理
        /// <summary>
        /// 收藏管理
        /// </summary>
        /// <returns></returns>
        public static CollectionBLL Collection()
        {
            if (_collectionbll == null)
            {
                _collectionbll = new CollectionBLL();
            }
            return _collectionbll;
        }
        #endregion
        #region 留言板
        /// <summary>
        /// 留言板
        /// </summary>
        /// <returns></returns>
        public static GuestbookBLL Guestbook()
        {
            if (_guestbookbll == null)
            {
                _guestbookbll = new GuestbookBLL();
            }
            return _guestbookbll;
        }
        #endregion

        #region 在线调查
        /// <summary>
        /// 在线调查
        /// </summary>
        /// <returns></returns>
        public static SurveyBLL Survey()
        {
            if (_surveybll == null)
            {
                _surveybll = new SurveyBLL();
            }
            return _surveybll;
        }
        #endregion
        #region 调查项
        /// <summary>
        /// 调查选项
        /// </summary>
        /// <returns></returns>
        public static SurveyItemBLL SurveyItem()
        {
            if (_surveyitembll == null)
            {
                _surveyitembll = new SurveyItemBLL();
            }
            return _surveyitembll;
        }
        #endregion
        #region 调查项选项
        /// <summary>
        /// 调查子选项
        /// </summary>
        /// <returns></returns>
        public static SurveyItemOptionBLL SurveyItemOption()
        {
            if (_surveyitemoptionbll == null)
            {
                _surveyitemoptionbll = new SurveyItemOptionBLL();
            }
            return _surveyitemoptionbll;
        }
        #endregion
        #region 调查结果
        /// <summary>
        /// 调查结果
        /// </summary>
        /// <returns></returns>
        public static SurveyResultBLL SurveyResult()
        {
            if (_surveyresultbll == null)
            {
                _surveyresultbll = new SurveyResultBLL();
            }
            return _surveyresultbll;
        }
        #endregion

        #region 视频管理
        /// <summary>
        /// 视频管理
        /// </summary>
        /// <returns></returns>
        public static VideoBLL Video()
        {
            if (_videobll == null)
            {
                _videobll = new VideoBLL();
            }
            return _videobll;
        }
        #endregion
        #region 相册管理
        /// <summary>
        /// 相册管理
        /// </summary>
        /// <returns></returns>
        public static PhotoBLL Photo()
        {
            if (_photobll == null)
            {
                _photobll = new PhotoBLL();
            }
            return _photobll;
        }
        #endregion
        #region 水印管理
        /// <summary>
        /// 水印管理
        /// </summary>
        /// <returns></returns>
        public static SetBLL Set()
        {
            if (_setbll == null)
            {
                _setbll = new SetBLL();
            }
            return _setbll;
        }
        #endregion
        #region 片段内容管理
        /// <summary>
        /// 片段内容管理
        /// </summary>
        /// <returns></returns>
        public static BlockBLL Block()
        {
            if (_blockbll == null)
            {
                _blockbll = new BlockBLL();
            }
            return _blockbll;
        }
        #endregion
        #endregion
    }
}
