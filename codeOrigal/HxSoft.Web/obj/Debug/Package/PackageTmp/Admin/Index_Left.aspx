<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index_Left.aspx.cs" Inherits="HxSoft.Web.Admin.Index_Left" %>

<%@ Import Namespace="HxSoft.Common" %>
<%@ Import Namespace="HxSoft.ClassFactory" %>
<%@ Register Src="Admin.Config.ascx" TagName="Config" TagPrefix="Admin" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <Admin:Config ID="Admin1" runat="server" />
    <style type="text/css">
        body
        {
            background-color: #025AAA;
        }
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {
            $(".MenuBox:[title!='other'] div:nth-child(1)").click(function () {
                var i = $(".MenuBox:[title!='other'] div:nth-child(1)").index(this);
                if ($(".MenuBox:[title!='other'] div:nth-child(1)").eq(i).attr("class") == "expand") {
                    $(".MenuBox:[title!='other'] div:nth-child(1)").eq(i).attr("class", "contract");
                    $(".MenuBox:[title!='other'] div:nth-child(2)").eq(i).show();
                }
                else {
                    $(".MenuBox:[title!='other'] div:nth-child(1)").eq(i).attr("class", "expand");
                    $(".MenuBox:[title!='other'] div:nth-child(2)").eq(i).hide();
                }
            });

            $(".tree ul li a").click(function () {
                $(".tree ul li:[class!='treetop']").removeClass();
                $(this).parent().addClass("selected");
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="lefttop1">
        <div id="lefttop">
        </div>
    </div>
    <div id="leftmenu">
        <%-------------------------------------------------系统配置----------------------------------------------------------------%>
        <% if (GetData.LimitChk("Config") || GetData.LimitChk("Class") || GetData.LimitChk("Area") || GetData.LimitChk("Industry") || GetData.LimitChk("Dictionary") || GetData.LimitChk("Limit") || GetData.LimitChk("AdminGroup") || GetData.LimitChk("Admin") || GetData.LimitChk("AdminLog"))
           { %>
        <div class="MenuBox">
            <div class="expand">
                <a>系统配置</a></div>
            <div class="tree" style="display: none;">
                <ul>
                    <li class="treetop"></li>
                    <%  if (GetData.LimitChk("Set"))
                        {%>
                    <li><a href="System/Set_Add.aspx" target="MainFrm">系统配置</a></li>
                    <%} %>
                    <%  if (GetData.LimitChk("Config"))
                        {%>
                    <li><a href="System/Config.aspx" target="MainFrm">网站配置</a></li>
                    <%} %>
                    <% if (GetData.LimitChk("Class"))
                       { %>
                    <li><a href="System/Class.aspx" target="MainFrm">栏目管理</a></li>
                    <%} %>
                    <% if (GetData.LimitChk("ClassProperty"))
                       { %>
                    <li><a href="System/ClassProperty.aspx" target="MainFrm">栏目属性</a></li>
                    <%} %>
                    <% if (GetData.LimitChk("ClassTemplate"))
                       { %>
                    <li><a href="System/ClassTemplate.aspx" target="MainFrm">栏目模板</a></li>
                    <%} %>
                    <% if (GetData.LimitChk("Area"))
                       { %>
                    <li><a href="System/Area.aspx" target="MainFrm">地区管理</a></li>
                    <%} %>
                    <% if (GetData.LimitChk("Industry"))
                       { %>
                    <li><a href="System/Industry.aspx" target="MainFrm">行业管理</a></li>
                    <%} %>
                    <%  if (GetData.LimitChk("Dictionary"))
                        { %>
                    <li><a href="System/Dictionary.aspx" target="MainFrm">数据字典</a></li>
                    <%} %>
                    <%  if (GetData.LimitChk("Limit"))
                        { %>
                    <li><a href="System/Limit.aspx" target="MainFrm">权限字段</a></li>
                    <%} %>
                    <%  if (GetData.LimitChk("AdminGroup"))
                        { %>
                    <li><a href="System/AdminGroup.aspx" target="MainFrm">管理组管理</a></li>
                    <%} %>
                    <%  if (GetData.LimitChk("Admin"))
                        { %>
                    <li><a href="System/Admin.aspx" target="MainFrm">管理员管理</a></li>
                    <%} %>
                    <% if (GetData.LimitChk("AdminLog"))
                       { %>
                    <li><a href="System/AdminLog.aspx" target="MainFrm">管理员日志</a></li>
                    <%} %>
                </ul>
            </div>
        </div>
        <%} %>
        <%-------------------------------------------------应用管理----------------------------------------------------------------%>
        <% if (GetData.LimitChk("Article") || GetData.LimitChk("Product") || GetData.LimitChk("Job") || GetData.LimitChk("Download") || GetData.LimitChk("Survey"))
           { %>
        <div class="MenuBox">
            <div class="expand">
                <a>应用管理</a></div>
            <div class="tree" style="display: none;">
                <ul>
                    <li class="treetop"></li>
                    <%if (GetData.LimitChk("Article"))
                      { %>
                    <li><a href="Article/Article.aspx" target="MainFrm">文章管理</a></li>
                    <%} %>
                    <%if (GetData.LimitChk("Product"))
                      { %>
                    <li><a href="Product/Product.aspx" target="MainFrm">产品管理</a></li>
                    <%} %>
                    <%if (GetData.LimitChk("Job"))
                      { %>
                    <li><a href="Job/Job.aspx" target="MainFrm">招聘管理</a></li>
                    <%} %>
                    <%if (GetData.LimitChk("Download"))
                      { %>
                    <li><a href="Download/Download.aspx" target="MainFrm">下载管理</a></li>
                    <%} %>
                    <%if (GetData.LimitChk("Survey"))
                      { %>
                    <li><a href="Survey/Survey.aspx" target="MainFrm">调查管理</a></li>
                    <%} %>
                    <%if (GetData.LimitChk("Video"))
                      { %>
                    <li><a href="Video/Video.aspx" target="MainFrm">视频管理</a></li>
                    <%} %>
                    <%if (GetData.LimitChk("Photo"))
                      { %>
                    <li><a href="Photo/Photo.aspx" target="MainFrm">相册管理</a></li>
                    <%} %>
                    <%if (GetData.LimitChk("Block"))
                      { %>
                    <li><a href="Block/Block.aspx" target="MainFrm">片段内容</a></li>
                    <%} %>
                </ul>
            </div>
        </div>
        <%} %>
        <%-------------------------------------------------会员管理----------------------------------------------------------------%>
        <% if (GetData.LimitChk("UserRank") || GetData.LimitChk("User") || GetData.LimitChk("UserLog"))
           { %>
        <div class="MenuBox">
            <div class="expand">
                <a>会员管理</a></div>
            <div class="tree" style="display: none;">
                <ul>
                    <li class="treetop"></li>
                    <%if (GetData.LimitChk("UserRank"))
                      { %>
                    <li><a href="User/UserRank.aspx" target="MainFrm">会员级别</a></li>
                    <%} %>
                    <%if (GetData.LimitChk("User"))
                      { %>
                    <li><a href="User/User.aspx" target="MainFrm">会员管理</a></li>
                    <%} %>
                    <%if (GetData.LimitChk("UserLog"))
                      { %>
                    <li><a href="User/UserLog.aspx" target="MainFrm">会员日志</a></li>
                    <%} %>
                </ul>
            </div>
        </div>
        <%} %>
        <%-------------------------------------------------留言反馈----------------------------------------------------------------%>
        <% if (GetData.LimitChk("Message") || GetData.LimitChk("Guestbook") || GetData.LimitChk("Feedback"))
           { %>
        <div class="MenuBox">
            <div class="expand">
                <a>留言反馈</a></div>
            <div class="tree" style="display: none;">
                <ul>
                    <li class="treetop"></li>
                    <%if (GetData.LimitChk("Message"))
                      { %>
                    <li><a href="Message/Message.aspx" target="MainFrm">会员留言</a></li>
                    <%} %>
                    <%if (GetData.LimitChk("Guestbook"))
                      { %>
                    <li><a href="Message/Guestbook.aspx" target="MainFrm">留言管理</a></li>
                    <%} %>
                    <%if (GetData.LimitChk("Feedback"))
                      { %>
                    <li><a href="Message/Feedback.aspx" target="MainFrm">信息反馈</a></li>
                    <%} %>
                </ul>
            </div>
        </div>
        <%} %>
        <%-------------------------------------------------扩展功能----------------------------------------------------------------%>
        <% if (GetData.LimitChk("AdPosition") || GetData.LimitChk("Ad") || GetData.LimitChk("Link") || GetData.LimitChk("Chat") || GetData.LimitChk("Mail") || GetData.LimitChk("BackUp") || GetData.LimitChk("SiteMap"))
           { %>
        <div class="MenuBox">
            <div class="expand">
                <a>扩展功能</a></div>
            <div class="tree" style="display: none;">
                <ul>
                    <li class="treetop"></li>
                    <% if (GetData.LimitChk("AdPosition"))
                       { %>
                    <li><a href="Extension/AdPosition.aspx" target="MainFrm">广告位管理</a></li>
                    <%} %>
                    <% if (GetData.LimitChk("Ad"))
                       { %>
                    <li><a href="Extension/Ad.aspx" target="MainFrm">广告管理</a></li>
                    <%} %>
                    <% if (GetData.LimitChk("Link"))
                       { %>
                    <li><a href="Extension/Link.aspx" target="MainFrm">友情链接</a></li>
                    <%} %>
                    <% if (GetData.LimitChk("Chat"))
                       { %>
                    <li><a href="Extension/Chat.aspx" target="MainFrm">聊天帐号</a></li>
                    <%} %>
                    <% if (GetData.LimitChk("Mail"))
                       { %>
                    <li><a href="Extension/Mail.aspx" target="MainFrm">邮件订阅</a></li>
                    <%} %>
                    <% if (GetData.LimitChk("BackUp"))
                       { %>
                    <li><a href="Extension/DataBackup.aspx" target="MainFrm">备份数据库</a> </li>
                    <%} %>
                    <% if (GetData.LimitChk("SiteMap"))
                       { %>
                    <li><a href="Extension/SiteMap.aspx" target="MainFrm">站点地图</a> </li>
                    <%} %>
                </ul>
            </div>
        </div>
        <%} %>
        <div class="MenuBox" title="other">
            <div class="reset">
                <span><a href="ResetPassword.aspx" target="MainFrm">修改密码</a></span><span class="ml30"><a href="LogOut.aspx" target="_parent">退出系统</a></span></div>
        </div>
    </div>
    </form>
</body>
</html>
