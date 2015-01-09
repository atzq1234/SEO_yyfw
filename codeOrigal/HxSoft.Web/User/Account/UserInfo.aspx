<%@ Page Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="HxSoft.Web.User.Account.UserInfo" Title="无标题页" %>

<%@ Import Namespace="HxSoft.Common" %>
<%@ Import Namespace="HxSoft.ClassFactory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $f.dom.ready(function () {
            $f("<%=form1.ClientID %>", {
                rules: {
                    txtRealName: { required: 1 },
                    txtEmail: { required: 1, email: 1 },
                    txtMobile: { required: 1, chkTel: 1 },
                    txtAddress: { required: 1 },
                    txtCompany: { required: 1 }
                },
                messages: {
                    txtRealName: { required: "不能为空" },
                    txtEmail: { required: "不能为空", email: "请输入正确的邮件地址" },
                    txtMobile: { required: "不能为空" },
                    txtAddress: { required: "不能为空" },
                    txtCompany: { required: "不能为空" },
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
                姓名：
            </td>
            <td>
                <asp:TextBox ID="txtRealName" runat="server" ruleKey="txtRealName"></asp:TextBox>
            </td>
        </tr>
        <tr class="table_form_row">
            <td style="width: 15%" align="right">
                性别：
            </td>
            <td>
                <asp:RadioButtonList ID="radSex" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem>男</asp:ListItem>
                    <asp:ListItem>女</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="table_form_row">
            <td style="width: 15%" align="right">
                E-mail：
            </td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" ruleKey="txtEmail"></asp:TextBox>
            </td>
        </tr>
        <tr class="table_form_row">
            <td style="width: 15%" align="right">
                手机：
            </td>
            <td>
                <asp:TextBox ID="txtMobile" runat="server" ruleKey="txtMobile"></asp:TextBox>
            </td>
        </tr>
        <tr class="table_form_row">
            <td style="width: 15%" align="right">
                联系地址：
            </td>
            <td>
                <asp:TextBox ID="txtAddress" runat="server" ruleKey="txtAddress"></asp:TextBox>
            </td>
        </tr>
        <tr class="table_form_row">
            <td style="width: 15%" align="right">
                公司名称：
            </td>
            <td>
                <asp:TextBox ID="txtCompany" runat="server" ruleKey="txtCompany"></asp:TextBox>
            </td>
        </tr>
        <tr class="table_form_row">
            <td style="width: 15%" align="right">
                备注：
            </td>
            <td>
                <asp:TextBox ID="txtComment" runat="server" Columns="50" Rows="10" TextMode="MultiLine" ruleKey="txtComment"></asp:TextBox>
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
