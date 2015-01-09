using System;
using System.Web;
using HxSoft.Common;
using HxSoft.Model;
using HxSoft.BLL;
using System.Web.UI.WebControls;

namespace HxSoft.ClassFactory
{
    public class GetData
    {
        #region 排序标识(add by yang)
        /// <summary>
        /// 排序标识
        /// </summary>
        /// <param name="strOrderKey"></param>
        /// <param name="strOrderField"></param>
        /// <param name="strAscDesc1"></param>
        /// <returns></returns>
        public static string GetOrderSign(string strOrderKey, string strOrderField, string strAscDesc1)
        {
            if (strOrderKey == strOrderField)
            {
                if (strAscDesc1 == "asc")
                    return "<img src=\"../Admin_Themes/Images/up.gif\"/>";
                else
                    return "<img src=\"../Admin_Themes/Images/down.gif\"/>";
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region 关闭状态(add by yang)
        /// <summary>
        /// 关闭状态
        /// </summary>
        /// <param name="strIsClose"></param>
        /// <returns></returns>
        public static string ShowCloseStatus(string strIsClose)
        {
            return strIsClose == "0" ? "开放" : "<span style=\"color:red\">关闭</span>";
        }
        #endregion

        #region 审核状态(add by yang)
        /// <summary>
        /// 审核状态
        /// </summary>
        /// <param name="strIsAudit"></param>
        /// <returns></returns>
        public static string ShowAuditStatus(string strIsAudit)
        {
            return strIsAudit == "1" ? "已审核" : "<span style=\"color:red\">未审核</span>";
        }
        #endregion

        #region 阅读状态(add by yang)
        /// <summary>
        /// 阅读状态
        /// </summary>
        /// <param name="strIsRead"></param>
        /// <returns></returns>
        public static string ShowReadStatus(string strIsRead)
        {
            if (strIsRead == "1")
                return "已读";
            else if (strIsRead == "0")
                return "<span style=\"color:red\">未读</span>";
            else
                return "-";
        }
        #endregion

        #region 回复状态(add by yang)
        /// <summary>
        /// 回复状态
        /// </summary>
        /// <param name="strIsReply"></param>
        /// <returns></returns>
        public static string ShowReplyStatus(string strIsReply)
        {
            return strIsReply == "1" ? "已回复" : "<span style=\"color:red\">未回复</span>";
        }
        #endregion

        #region 结束状态(add by yang)
        /// <summary>
        /// 结束状态
        /// </summary>
        /// <param name="strIsEnd"></param>
        /// <returns></returns>
        public static string ShowEndStatus(string strIsEnd)
        {
            return strIsEnd == "1" ? " | 已结束" : "";
        }
        #endregion

        #region 推荐状态(add by yang)
        /// <summary>
        /// 推荐状态
        /// </summary>
        /// <param name="strIsRecommend"></param>
        /// <returns></returns>
        public static string ShowRecommendStatus(string strIsRecommend)
        {
            return strIsRecommend == "1" ? "<span style=\"color:red\"> |荐</span>" : "";
        }
        #endregion

        #region 分配状态(add by yang)
        /// <summary>
        /// 分配状态
        /// </summary>
        /// <param name="strIsDist"></param>
        /// <returns></returns>
        public static string ShowDistStatus(string strIsDist)
        {
            return strIsDist == "1" ? "<span style=\"color:red\"> |分</span>" : "";
        }
        #endregion

        #region 处理状态(add by yang)
        /// <summary>
        /// 处理状态
        /// </summary>
        /// <param name="strIsDeal"></param>
        /// <returns></returns>
        public static string ShowDealStatus(string strIsDeal)
        {
            return strIsDeal == "1" ? "已处理" : "<span style=\"color:red\">未处理</span>";
        }
        #endregion

        #region 邮件接收状态(add by yang)
        /// <summary>
        /// 邮件接收状态
        /// </summary>
        /// <param name="strIsRec"></param>
        /// <returns></returns>
        public static string ShowRecStatus(string strIsRec)
        {
            return strIsRec == "1" ? "接收" : "<span style=\"color:red\">退订</span>";
        }
        #endregion

        #region 是否显示在导航(add by yang)
        /// <summary>
        /// 是否显示在导航
        /// </summary>
        /// <param name="strIsShowNav"></param>
        /// <returns></returns>
        public static string ShowNavStatus(string strIsShowNav)
        {
            return strIsShowNav == "1" ? "<span style=\"color:red\"> |显</span>" : "";
        }
        #endregion
        //====================
        #region 显示广告位类型(add by yang)
        /// <summary>
        /// 显示广告位类型
        /// </summary>
        /// <param name="strTypeID"></param>
        /// <returns></returns>
        public static string ShowAdPositionType(string strTypeID)
        {
            switch (strTypeID)
            {
                case "1":
                    return "普通显示广告";
                case "2":
                    return "Flash幻灯片广告";
                case "3":
                    return "浮动广告";
                case "4":
                    return "对联广告";
                default:
                    return "";
            }
        }
        #endregion

        #region 显示友情链接类型(add by yang)
        /// <summary>
        /// 显示友情链接类型
        /// </summary>
        /// <param name="strTypeID"></param>
        /// <returns></returns>
        public static string ShowLinkType(string strTypeID)
        {
            switch (strTypeID)
            {
                case "1":
                    return "文字链接";
                case "2":
                    return "图片链接";
                default:
                    return "";
            }
        }
        #endregion

        #region 显示聊天帐号类型(add by yang)
        /// <summary>
        /// 显示聊天帐号类型
        /// </summary>
        /// <param name="strTypeID"></param>
        /// <returns></returns>
        public static string ShowChatType(string strTypeID)
        {
            switch (strTypeID)
            {
                case "1":
                    return "QQ";
                case "2":
                    return "MSN";
                case "3":
                    return "Skype";
                case "4":
                    return "阿里旺旺";
                default:
                    return "";
            }
        }
        #endregion

        #region 显示调查选项类型(add by yang)
        /// <summary>
        /// 显示调查选项类型
        /// </summary>
        /// <param name="strTypeID"></param>
        /// <returns></returns>
        public static string ShowSurveyItemType(string strTypeID)
        {
            switch (strTypeID)
            {
                case "1":
                    return "单选";
                case "2":
                    return "多选";
                case "3":
                    return "文本";
                default:
                    return "";
            }
        }
        #endregion

        #region 列表记录复选框是否可选(add by yang)
        /// <summary>
        /// 列表记录复选框是否可选
        /// </summary>
        /// <param name="strAdminID"></param>
        /// <param name="strLimitValue"></param>
        /// <returns></returns>
        public static string ShowDisabledStatus(string strAdminID, string strLimitValue)
        {
            if (CheckAdminID(strAdminID, strLimitValue))
            {
                return "";
            }
            else
            {
                return "disabled=\"disabled\"";
            }
        }
        #endregion

        #region 文件(夹)复选框是否可选(add by yang)
        /// <summary>
        /// 文件(夹)复选框是否可选
        /// </summary>
        /// <param name="strPath"></param>
        /// <param name="strLimitValue"></param>
        /// <returns></returns>
        public static string ShowPathDisabledStatus(string strPath, string strLimitValue)
        {
            PathModel pathModel = new PathModel();
            pathModel = Factory.Path().GetInfo(strPath.ToLower());
            if (pathModel != null)
            {
                return ShowDisabledStatus(pathModel.AdminID, strLimitValue);
            }
            else
            {
                return "disabled=\"disabled\"";
            }
        }
        #endregion
        //====================
        #region 检查日期(add by yang)
        /// <summary>
        /// 检查日期
        /// </summary>
        /// <param name="strDate"></param>
        /// <returns></returns>
        public static string CheckDate(string strDate)
        {
            if (Convert.ToDateTime(strDate).Year == 1900)
            {
                return "-";
            }
            else
            {
                return strDate;
            }
        }
        #endregion

        #region 检查管理组权限(add by yang)
        /// <summary>
        /// 检查管理组权限
        /// </summary>
        /// <param name="strAdminGroupID"></param>
        /// <param name="strLimitValue"></param>
        /// <returns></returns>
        public static string CheckAdminGroupLimitValue(string strAdminGroupID, string strLimitValue)
        {
            if (Factory.AdminGroup().GetValueByField("LimitValues", strAdminGroupID).IndexOf("," + strLimitValue + ",") > 0)
            {
                return "checked=\"checked\"";
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region 检查会员级别权限(add by yang)
        /// <summary>
        /// 检查会员级别权限
        /// </summary>
        /// <param name="strUserRankID"></param>
        /// <param name="strLimitValue"></param>
        /// <returns></returns>
        public static string CheckUserRankLimitValue(string strUserRankID, string strLimitValue)
        {
            if (Factory.UserRank().GetValueByField("LimitValues", strUserRankID).IndexOf("," + strLimitValue + ",") > 0)
            {
                return "checked=\"checked\"";
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region 检查当前登录管理员的权限(add by yang)
        /// <summary>
        /// 检查当前登录管理员的权限
        /// </summary>
        /// <param name="strLimitValue"></param>
        /// <returns></returns>
        public static bool LimitChk(string strLimitValue)
        {
            if (HttpContext.Current.Session["AdminID"].ToString() == Config.SystemAdminID)//超级管理员不受权限控制
            {
                return true;
            }
            else
            {
                if (Factory.Limit().GetValueByLimitValue("IsClose", strLimitValue) == "0")
                {
                    string strTempLimit = Factory.AdminGroup().GetLimitValues(HttpContext.Current.Session["AdminID"].ToString()).ToString();
                    if (strTempLimit.IndexOf("," + strLimitValue + ",") > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 检查当前登录管理员的权限
        /// </summary>
        /// <param name="strLimitValue"></param>
        public static void LimitChkMsg(string strLimitValue)
        {
            if (LimitChk(strLimitValue) == false)
            {
                Config.ShowEnd("对不起,您没有此操作权限!");
            }
        }
        #endregion

        #region 检查信息创建者是否为当前管理员及当前管理员是否有管理所有信息的权限(add by yang)
        /// <summary>
        /// 检查信息创建者是否为当前管理员及当前管理员是否有管理所有信息的权限
        /// </summary>
        /// <param name="strAdminID"></param>
        /// <param name="strLimitValue"></param>
        /// <returns></returns>
        public static bool CheckAdminID(string strAdminID, string strLimitValue)
        {
            if (LimitChk(strLimitValue))
            {
                return true;
            }
            else
            {
                if (strAdminID == HttpContext.Current.Session["AdminID"].ToString())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion

        #region 检查当前登录会员的权限(add by wj)
        /// <summary>
        /// 检查当前登录会员的权限
        /// </summary>
        /// <param name="strLimitValue"></param>
        /// <returns></returns>
        public static bool LimitChkUser(string strLimitValue)
        {
            if (Factory.Limit().GetValueByLimitValue("IsClose", strLimitValue) == "0")
            {
                string strTempLimit = Factory.UserRank().GetLimitValues(HttpContext.Current.Session["UserID"].ToString()).ToString();
                if (strTempLimit.IndexOf("," + strLimitValue + ",") > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion
        //====================
        #region 取文件(夹)创建人(add by yang)
        /// <summary>
        /// 取文件(夹)创建人
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static string GetPathAdmin(string strPath)
        {
            PathModel pathModel = new PathModel();
            pathModel = Factory.Path().GetInfo(strPath.ToLower());
            if (pathModel != null)
            {
                return Factory.Admin().GetValueByField("AdminName", pathModel.AdminID);
            }
            else
            {
                return "系统文件";
            }
        }
        #endregion

        #region 取会员用户名(add by yang)
        /// <summary>
        /// 取会员用户名
        /// </summary>
        /// <param name="strUserID"></param>
        /// <returns></returns>
        public static string GetUserName(string strUserID)
        {
            if (strUserID == "-1")
            {
                return "游客";
            }
            else
            {
                return Factory.User().GetValueByField("UserName",strUserID);
            }
        }
        #endregion

        //====================
        #region 取站点列表
        /// <summary>
        /// 取站点列表
        /// </summary>
        /// <returns></returns>
        public static void GetConfigList(Repeater rep)
        {
            string sql = "select * from t_Config order by ListID asc";
            Factory.Acc().DataBind(sql, null, Config.DataBindObjTypeCollection.Repeater.ToString(), rep);
        }
        public static void GetConfigList(DropDownList drp, string TF, string VF)
        {
            string sql = "select " + TF + "," + VF + " from t_Config order by ListID asc";
            Factory.Acc().DataBind(sql, null, Config.DataBindObjTypeCollection.DropDownList.ToString(), drp, TF, VF);
        }
        #endregion

        #region 取数据字典列表
        /// <summary>
        /// 取数据字典列表
        /// </summary>
        /// <param name="strParentID"></param>
        /// <returns></returns>
        public static void GetDictionaryList(string strParentID, Repeater rep)
        {
            string sql = "select * from t_Dictionary where IsClose=0 and ParentID=" + strParentID + " order by ListID asc";
            Factory.Acc().DataBind(sql, null, Config.DataBindObjTypeCollection.Repeater.ToString(), rep);
        }
        public static void GetDictionaryList(string strParentID, DropDownList drp, string TF, string VF)
        {
            string sql = "select " + TF + "," + VF + " from t_Dictionary where IsClose=0 and ParentID=" + strParentID + " order by ListID asc";
            Factory.Acc().DataBind(sql, null, Config.DataBindObjTypeCollection.DropDownList.ToString(), drp, TF, VF);
        }
        #endregion

        #region 取地区列表
        /// <summary>
        /// 取地区列表
        /// </summary>
        /// <param name="strParentID"></param>
        /// <returns></returns>
        public static void GetAreaList(string strParentID, Repeater rep)
        {
            string sql = "select * from t_Area where IsClose=0 and ParentID=" + strParentID + " order by ListID asc";
            Factory.Acc().DataBind(sql, null, Config.DataBindObjTypeCollection.Repeater.ToString(), rep);
        }
        public static void GetAreaList(string strParentID, DropDownList drp, string TF, string VF)
        {
            string sql = "select " + TF + "," + VF + " from t_Area where IsClose=0 and ParentID=" + strParentID + " order by ListID asc";
            Factory.Acc().DataBind(sql, null, Config.DataBindObjTypeCollection.DropDownList.ToString(), drp, TF, VF);
        }
        #endregion
    }
}
