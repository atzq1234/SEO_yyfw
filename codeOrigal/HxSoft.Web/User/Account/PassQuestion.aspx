<%@ Page Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="PassQuestion.aspx.cs" Inherits="HxSoft.Web.User.Account.PassQuestion" Title="无标题页" %>

<%@ Import Namespace="HxSoft.Common" %>
<%@ Import Namespace="HxSoft.ClassFactory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $f.dom.ready(function () {
            $f("<%=form1.ClientID %>", {
                rules: {
                    txtOldPassAnswer: { required: 1},
                    txtNewPassQuestion: { required: 1 },
                    txtNewPassAnswer: { required: 1}
                },
                messages: {
                    txtOldPassAnswer: { required: "不能为空" },
                    txtNewPassQuestion: { required: "不能为空" },
                    txtNewPassAnswer: { required: "不能为空"}
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
                原密码保护问题：
            </td>
            <td>
                <asp:Label ID="lblPassQuestion" runat="server"></asp:Label>
            </td>
        </tr>
        <tr class="table_form_row">
            <td style="width: 15%" align="right">
                原密码保护答案：
            </td>
            <td>
                <asp:TextBox ID="txtOldPassAnswer" runat="server" ruleKey="txtOldPassAnswer"></asp:TextBox>
            </td>
        </tr>
        <tr class="table_form_row">
            <td style="width: 15%" align="right">
                新密码保护问题：
            </td>
            <td>
                <asp:TextBox ID="txtNewPassQuestion" runat="server" ruleKey="txtNewPassQuestion"></asp:TextBox>
            </td>
        </tr>
        <tr class="table_form_row">
            <td style="width: 15%" align="right">
                新密码保护答案：
            </td>
            <td>
                <asp:TextBox ID="txtNewPassAnswer" runat="server" ruleKey="txtNewPassAnswer"></asp:TextBox>
            </td>
        </tr>
        <tr class="table_form_btn">
            <td style="width: 15%" align="right">
            </td>
            <td>
                <asp:Button ID="btnSave" runat="server" Text="修 改" OnClick="btnSave_Click" />
                <input name="input" type="reset" value="重 置" /><asp:Label ID="errMsg" runat="server" ForeColor="red"></asp:Label>
            </td>
        </tr>
    </table>
    </form>
</asp:Content>
