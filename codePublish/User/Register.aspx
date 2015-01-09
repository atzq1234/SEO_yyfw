<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="HxSoft.Web.User.Register" %>

<%@ Import Namespace="HxSoft.Common" %>
<%@ Import Namespace="HxSoft.ClassFactory" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
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
                    txtUserName: { required: 1,ajaxUserName:1 },
                    txtUserPass: { required: 1 },
                    txtUserPassConfirm: { required: 1, compareTo: "txtUserPass" },
                    txtPassQuestion: { required: 1},
                    txtPassAnswer: { required: 1},
                    txtRealName: { required: 1 },
                    txtEmail: { required: 1, email: 1 },
                    txtMobile: { required: 1, chkTel: 1 },
                    txtAddress: { required: 1 },
                    txtCompany: { required: 1 },
                    txtVerifyCode: { required: 1, digit: 1, minlength: 4, maxlength: 4, ajaxVerifyCode: 1 }
                },
                messages: {
                    txtUserName: { required: "不能为空" },
                    txtUserPass: { required: "不能为空" },
                    txtUserPassConfirm: { required: "不能为空",compareTo:"两次密码不一致" },
                    txtPassQuestion: { required: "不能为空" },
                    txtPassAnswer: { required: "不能为空" },
                    txtRealName: { required: "不能为空" },
                    txtEmail: { required: "不能为空", email: "请输入正确的邮件地址" },
                    txtMobile: { required: "不能为空" },
                    txtAddress: { required: "不能为空" },
                    txtCompany: { required: "不能为空" },
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
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="reg_form">
        <tr class="reg_form_row">
            <td style="width: 15%" align="right">
                用户名：
            </td>
            <td>
                <asp:TextBox ID="txtUserName" runat="server" ruleKey="txtUserName"></asp:TextBox>
            </td>
        </tr>
        <tr class="reg_form_row">
            <td style="width: 15%" align="right">
                密码：
            </td>
            <td>
                <asp:TextBox ID="txtUserPass" runat="server" TextMode="Password" ruleKey="txtUserPass" />
            </td>
        </tr>
        <tr class="reg_form_row">
            <td style="width: 15%" align="right">
                确认密码：
            </td>
            <td>
                <asp:TextBox ID="txtUserPassConfirm" runat="server" TextMode="Password" ruleKey="txtUserPassConfirm" />
            </td>
        </tr>
        <tr class="reg_form_row">
            <td style="width: 15%" align="right">
                密码保护问题：
            </td>
            <td>
                <asp:TextBox ID="txtPassQuestion" runat="server" ruleKey="txtPassQuestion"></asp:TextBox>
            </td>
        </tr>
        <tr class="reg_form_row">
            <td style="width: 15%" align="right">
                密码保护答案：
            </td>
            <td>
                <asp:TextBox ID="txtPassAnswer" runat="server" ruleKey="txtPassAnswer"></asp:TextBox>
            </td>
        </tr>
        <tr class="reg_form_row">
            <td style="width: 15%" align="right">
                姓名：
            </td>
            <td>
                <asp:TextBox ID="txtRealName" runat="server" ruleKey="txtRealName"></asp:TextBox>
            </td>
        </tr>
        <tr class="reg_form_row">
            <td style="width: 15%" align="right">
                性别：
            </td>
            <td>
                <asp:RadioButtonList ID="radSex" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Selected="True">男</asp:ListItem>
                    <asp:ListItem>女</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="reg_form_row">
            <td style="width: 15%" align="right">
                E-mail：
            </td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" ruleKey="txtEmail"></asp:TextBox>
            </td>
        </tr>
        <tr class="reg_form_row">
            <td style="width: 15%" align="right">
                联系电话：
            </td>
            <td>
                <asp:TextBox ID="txtMobile" runat="server" ruleKey="txtMobile"></asp:TextBox>
            </td>
        </tr>
        <tr class="reg_form_row">
            <td style="width: 15%" align="right">
                联系地址：
            </td>
            <td>
                <asp:TextBox ID="txtAddress" runat="server" ruleKey="txtAddress"></asp:TextBox>
            </td>
        </tr>
        <tr class="reg_form_row">
            <td style="width: 15%" align="right">
                公司名称：
            </td>
            <td>
                <asp:TextBox ID="txtCompany" runat="server" ruleKey="txtCompany"></asp:TextBox>
            </td>
        </tr>
        <tr class="reg_form_row">
            <td style="width: 15%" align="right">
                备注：
            </td>
            <td>
                <asp:TextBox ID="txtComment" runat="server" Columns="50" Rows="10" TextMode="MultiLine" ruleKey="txtComment"></asp:TextBox>
            </td>
        </tr>
        <tr class="reg_form_row">
            <td style="width: 15%" align="right">
                验证码：
            </td>
            <td>
                    <asp:TextBox ID="txtVerifyCode" runat="server" Columns="6" CssClass="code" ruleKey="txtVerifyCode" onfocus="RefreshVerifyCode()" />
                    <img src="/Common/VerifyCode.aspx" id="ImgVerifyCode" title="点击刷新验证码" class="code" onclick="RefreshVerifyCode()" />
            </td>
        </tr>
        <tr class="reg_form_btn">
            <td style="width: 15%" align="right">
                &nbsp;
            </td>
            <td>
                <asp:Button ID="btnSave" runat="server" Text="注 册" OnClick="btnSave_Click" />
                <input name="input" type="reset" value="重 置" />
                <asp:Label ID="errMsg" runat="server" ForeColor="red"></asp:Label>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
