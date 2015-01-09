<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HxSoft.Web.User.Login" %>

<%@ Import Namespace="HxSoft.Common" %>
<%@ Import Namespace="HxSoft.ClassFactory" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>会员登录</title>
    <script src="/App_Themes/Js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="/App_Themes/Js/swfobject.js" type="text/javascript"></script>
    <script src="/App_Themes/Js/fancyValidate.min.js" type="text/javascript"></script>
    <script src="/App_Themes/Js/fancyValidate.additional.min.js" type="text/javascript"></script>
    <script src="/App_Themes/Js/fancyValidate.my.additional.js" type="text/javascript"></script>
    <script src="/App_Themes/Js/common.js" type="text/javascript"></script>
    <script src="User_Themes/Js/user.js" type="text/javascript"></script>
    <link href="/App_Themes/Css/fancyValidate.css" rel="stylesheet" type="text/css" />
    <link href="User_Themes/Css/Style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $f.dom.ready(function () {
            $f("<%=form1.ClientID %>", {
                rules: {
                    txtUserName: { required: 1 },
                    txtUserPass: { required: 1 },
                    txtVerifyCode: { required: 1, digit: 1, minlength: 4, maxlength: 4, ajaxVerifyCode: 1 }
                },
                messages: {
                    txtUserName: { required: "不能为空" },
                    txtUserPass: { required: "不能为空" },
                    txtVerifyCode: { required: "不能为空", digit: "请输入四位数字验证码", minlength: "请输入四位数字验证码", maxlength: "请输入四位数字验证码" }
                },
                ruleKey: "ruleKey",
                appendLabel: $f.appendLast,
                findLabel: $f.findLast
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <%if (!Factory.User().IsLogin())
      { %>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="login_form">
        <tbody>
            <tr class="login_form_row">
                <td style="width: 15%" align="right">
                    用户名:
                </td>
                <td>
                    <asp:TextBox ID="txtUserName" runat="server" Columns="40" Width="156px" ruleKey="txtUserName"/>
                </td>
            </tr>
            <tr class="login_form_row">
                <td style="width: 15%" align="right">
                    密码:
                </td>
                <td>
                    <asp:TextBox ID="txtUserPass" runat="server" TextMode="Password" Width="156px"  ruleKey="txtUserPass"/>
                </td>
            </tr>
            <tr class="login_form_row">
                <td style="width: 15%" align="right">
                    验证码:
                </td>
                <td>
                    <asp:TextBox ID="txtVerifyCode" runat="server" Columns="6" CssClass="code" ruleKey="txtVerifyCode" onfocus="RefreshVerifyCode()" />
                    <img src="/Common/VerifyCode.aspx" id="ImgVerifyCode" title="点击刷新验证码" class="code" onclick="RefreshVerifyCode()" />
                </td>
            </tr>
            <tr class="login_form_btn">
                <td>
                </td>
                <td>
                    <asp:Button ID="btnLogin" runat="server" Text="登录" OnClick="btnLogin_Click" />
                    &nbsp;&nbsp;
                    <input type="reset" value="重置" name="reset" />
                    <asp:Label ID="errMsg" runat="server" ForeColor="red"></asp:Label>
                </td>
            </tr>
        </tbody>
    </table>
    <%}
      else
      { %>
    <div style="text-align: center">
        <asp:Label ID="lblUserName" runat="server" ForeColor="red"></asp:Label>
        ,欢迎您!
        <br />
        <a href="Account/UserInfo.aspx">用户资料</a> <a href="Account/PassQuestion.aspx">密码问题</a> <a href="Account/ResetPassword.aspx">修改密码</a> <a href="Message/Message_Add.aspx">添加留言</a> <a href="Message/Message_List.aspx">会员留言</a> <a href="Logout.aspx">退出登录</a>
    </div>
    <%} %>
    </form>
</body>
</html>
