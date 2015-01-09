using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using HxSoft.Common;
using HxSoft.Model;
using HxSoft.ClassFactory;

namespace HxSoft.Web.cn.UserControl
{
    public partial class WUC_Survey_Details : System.Web.UI.UserControl
    {
        /// <summary>
        /// 信息ID(只读)
        /// </summary>
        public string SurveyID
        {
            get
            {
                return Config.RequestNumeric(Request.QueryString["SurveyID"], -1).ToString();
            }
        }
        public SurveyModel surModel
        {
            get
            {
                return Factory.Survey().GetCacheInfo2(SurveyID);
            }
        }
        /// <summary>
        /// 分类ID(只读)
        /// </summary>
        public string ClassID
        {
            get
            {
                if (surModel != null)
                {
                    return surModel.ClassID;
                }
                else
                {
                    return "-1";
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //栏目名称
            ClassModel claModel = new ClassModel();
            claModel = Factory.Class().GetCacheInfo2(ClassID);
            if (claModel != null)
            {
                litClassName.Text = claModel.ClassName;
            }
            //详细内容
            if (surModel != null)
            {
                Factory.Survey().Click(SurveyID);
                //
                Page.Header.Title = Server.HtmlEncode(surModel.Subject) + " - " + Page.Header.Title;
                //先清除母版页设置的keywords和description
                Page.Header.Controls.Remove(Page.Header.FindControl("keywords"));
                Page.Header.Controls.Remove(Page.Header.FindControl("description"));
                Page.Header.Controls.Add(Config.SetKeywords(Server.HtmlEncode(surModel.Subject)));
                Page.Header.Controls.Add(Config.SetDescription(Server.HtmlEncode(surModel.IntrContent)));
                //
                litSubject.Text = surModel.Subject;
                litIntrContent.Text = surModel.IntrContent;

                //
                string sql = "select * from t_SurveyItem where IsClose=0 and SurveyID=" + SurveyID + " order by ListID asc";
                Factory.Acc().DataBind(sql, null, Config.DataBindObjTypeCollection.Repeater.ToString(), repSurveyItem);
            }
            else
            {
                Config.ShowEnd("参数错误!");
            }
        }

        #region 显示调查选项
        /// <summary>
        /// 显示调查选项
        /// </summary>
        public StringBuilder ShowServeyItemOption(string strSurveyItemID, string strTypeID, string strIndex)
        {
            StringBuilder strTemp = new StringBuilder();
            strTemp.Append("<dd>");
            if (strTypeID == "3")
            {
                strTemp.Append("<textarea id=\"Item" + strIndex + "\" name=\"Item" + strIndex + "\" rows=\"7\" cols=\"40\" ruleKey=\"textareaKey\"></textarea>");
            }
            else
            {
                string strSql = "select * from t_SurveyItemOption where SurveyItemID=" + strSurveyItemID + " order by ListID asc";
                DataTable dt = Factory.Acc().GetDataTable(strSql, null);
                int i = 0;
                for (int n = 0; n < dt.Rows.Count; n++)
                {
                    DataRow dr = dt.Rows[n];
                    if (strTypeID == "1")
                    {
                        strTemp.Append("<label><input  name=\"Item" + strIndex + "\"  id=\"Item" + strIndex + i.ToString() + "\" type=\"radio\" value=\"" + dr["ItemOptionName"].ToString() + "\" ruleKey=\"inputKey\"/>" + dr["ItemOptionName"].ToString() + "</label>");
                    }
                    else if (strTypeID == "2")
                    {
                        strTemp.Append("<label><input  name=\"Item" + strIndex + "\"  id=\"Item" + strIndex + i.ToString() + "\" type=\"checkbox\" value=\"" + dr["ItemOptionName"].ToString() + "\"  ruleKey=\"inputKey\"/>" + dr["ItemOptionName"].ToString() + "</label>");
                    }
                    i++;
                }
            }
            strTemp.Append("</dd>");
            return strTemp;
        }
        #endregion


        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            StringBuilder strServeyResult = new StringBuilder("");
            strServeyResult.Append("<table width=\"500\" border=\"0\" align=\"center\" cellpadding=\"5\" cellspacing=\"1\" bgcolor=\"#000000\">\n");
            strServeyResult.Append("<tbody>\n");
            strServeyResult.Append("<tr>\n");
            strServeyResult.Append("<td align=\"center\" bgcolor=\"#FFFFFF\">" + litSubject.Text + "</td>\n");
            strServeyResult.Append("</tr>\n");
            strServeyResult.Append("<tr>\n");
            strServeyResult.Append("<td align=\"left\" bgcolor=\"#FFFFFF\">" + litIntrContent.Text + "</td>\n");
            strServeyResult.Append("</tr>\n");
            string sql_1 = "select * from t_SurveyItem where IsClose=0 and SurveyID=" + SurveyID + " order by ListID asc";
            DataTable dt_1 = Factory.Acc().GetDataTable(sql_1, null);
            int i = 0;
            for (int n = 0; n < dt_1.Rows.Count; n++)
            {
                DataRow dr_1 = dt_1.Rows[n];
                string strTypeID = dr_1["TypeID"].ToString();
                strServeyResult.Append("<tr><td align=\"left\" bgcolor=\"#FFFFFF\">\n");
                strServeyResult.Append("<table>");
                strServeyResult.Append("<tr><td>" + (i + 1) + "、" + dr_1["ItemName"].ToString() + "</td></tr>\n");
                if (strTypeID == "3")
                {
                    strServeyResult.Append("<tr><td>答：" + Config.HTMLCls(Config.Request(Request.Form["Item" + i],"")) + "</td></tr>\n");
                }
                else
                {
                    string sql_2 = "select * from t_SurveyItemOption where SurveyItemID=" + dr_1["SurveyItemID"].ToString()+" order by ListID asc";
                    DataTable dt_2 = Factory.Acc().GetDataTable(sql_2,null);
                    for (int m = 0; m < dt_2.Rows.Count; m++)
                    {
                        DataRow dr_2 = dt_2.Rows[m];
                        if (strTypeID == "1")
                        {
                            string strSel = "";
                            if (dr_2["ItemOptionName"].ToString() == Config.HTMLCls(Config.Request(Request.Form["Item" + i],"")))
                            {
                                strSel = "checked=\"checked\"";
                            }
                            strServeyResult.Append("<tr><td><input type=\"radio\" value=\"" + dr_2["ItemOptionName"].ToString() + "\" " + strSel + "/>" + dr_2["ItemOptionName"].ToString() + "</td></tr>\n");
                        }
                        else if (strTypeID == "2")
                        {
                            string strSel = "";
                            if (Config.HTMLCls(Config.Request(Request.Form["Item" + i],"")).IndexOf(dr_2["ItemOptionName"].ToString()) > -1)
                            {
                                strSel = "checked=\"checked\"";
                            }
                            strServeyResult.Append("<tr><td><input type=\"checkbox\" value=\"" + dr_2["ItemOptionName"].ToString() + "\"" + strSel + " />" + dr_2["ItemOptionName"].ToString() + "</td></tr>\n");
                        }
                    }
                    strServeyResult.Append("</table>");
                    strServeyResult.Append("</td></tr>\n");
                    i++;
                }
            }
            strServeyResult.Append("</tbody>\n");
            strServeyResult.Append("</table>\n");

            //将反馈信息添加到数据库中
            SurveyResultModel surResModel = new SurveyResultModel();
            surResModel.SurveyID = SurveyID;
            surResModel.SurveyContent = strServeyResult.ToString();
            surResModel.IP = Request.UserHostAddress;
            surResModel.AddTime = DateTime.Now.ToString();
            Factory.SurveyResult().InsertInfo(surResModel);

            Config.MsgGotoUrl("提交成功!", Request.UrlReferrer.ToString());
        }

        public string ShowObj(string strTypeID, string strIndex)
        {
            if (strTypeID == "1")
            {
                return ":radio[name='Item" + strIndex + "']";
            }
            else if (strTypeID == "2")
            {
                return ":checkbox[name='Item" + strIndex + "']";
            }
            else
            {
                return "#Item" + strIndex;
            }
        }
    }
}