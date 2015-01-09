<%@ Page Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="HxSoft.Web.User.Account.ResetPassword" Title="无标题页" %>

<%@ Import Namespace="HxSoft.Common" %>
<%@ Import Namespace="HxSoft.ClassFactory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $f.dom.ready(function () {
            $f("<%=form1.ClientID %>", {
                rules: {
                    txtOldUserPass: { required: 1},
                    txtNewUserPass: { required: 1 },
                    txtNewUserPassConfirm: { required: 1, compareTo: "txtNewUserPass" }
                },
                messages: {
                    txtOldUserPass: { required: "不能为空" },
                    txtNewUserPass: { required: "不能为空" },
                    txtNewUserPassConfirm: { required: "不能为空", compareTo: "两次新密码不一致" }
                },
                ruleKey: "ruleKey",
                appendLabel: $f.appendLast,
                findLabel: $f.findLast
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_form">
        <tr class="table_form_row">
            <td style="width: 15%" align="right">
                用户名：
            </td>
            <td>
                <asp:Label ID="lblUserName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr class="table_form_row">
            <td style="width: 15%" align="right">
                旧密码：
            </td>
            <td>
                <asp:TextBox ID="txtOldUserPass" runat="server" TextMode="Password" ruleKey="txtOldUserPass"></asp:TextBox>
            </td>
        </tr>
        <tr class="table_form_row">
            <td style="width: 15%" align="right">
                新密码：
            </td>
            <td>
                <asp:TextBox ID="txtNewUserPass" runat="server" TextMode="Password" ruleKey="txtNewUserPass"></asp:TextBox>
            </td>
        </tr>
        <tr class="table_form_row">
            <td style="width: 15%" align="right">
                新密码确认：
            </td>
            <td>
                <asp:TextBox ID="txtNewUserPassConfirm" runat="server" TextMode="Password" ruleKey="txtNewUserPassConfirm"></asp:TextBox>
            </td>
        </tr>
        <tr class="table_form_btn">
            <td style="width: 15%" align="right">
            </td>
            <td>
                <asp:Button ID="btnSave" runat="server" Text="修 改" CssClass="sub1" OnClick="btnSave_Click" />
                <input class="sub1" name="input" type="reset" value="重 置" /><asp:Label ID="errMsg" runat="server" ForeColor="red"></asp:Label>
            </td>
        </tr>
    </table>
    </form>
</asp:Content>
