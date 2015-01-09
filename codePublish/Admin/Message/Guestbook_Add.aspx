<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Guestbook_Add.aspx.cs" Inherits="HxSoft.Web.Admin.Message.Guestbook_Add" %>

<%@ Import Namespace="HxSoft.Common" %>
<%@ Import Namespace="HxSoft.ClassFactory" %>
<%@ Register Src="../Admin.Config.ascx" TagName="Config" TagPrefix="Admin" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <Admin:Config ID="Admin1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_form">
            <tr class="table_form_title">
                <td colspan="4">
                    <asp:Label ID="lblTitle" runat="server">添加</asp:Label>留言
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    昵称：
                </td>
                <td>
                    <asp:TextBox ID="txtNickName" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNickName" Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
              <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    电话：
                </td>
                <td>
                    <asp:TextBox ID="txtTelePhone" runat="server" />
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTelePhone" Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                </td>
            </tr>
              <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    Email：
                </td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server" />
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    留言内容：
                </td>
                <td>
                    <asp:TextBox ID="txtBookContent" runat="server" Columns="50" Rows="10" TextMode="MultiLine" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBookContent" Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    IP：
                </td>
                <td>
                    <asp:Literal ID="litIpAddress" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    留言时间：
                </td>
                <td>
                    <asp:Literal ID="litAddTime" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    回复内容：
                </td>
                <td>
                    <asp:TextBox ID="txtReplyContent" runat="server" Columns="50" Rows="10" TextMode="MultiLine" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtReplyContent" Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    回复时间：
                </td>
                <td>
                    <asp:Literal ID="litReplyTime" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    回复人：
                </td>
                <td>
                    <asp:Literal ID="litAdminID" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    是否关闭：
                </td>
                <td>
                    <asp:RadioButtonList ID="radIsClose" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Value="1">是</asp:ListItem>
                        <asp:ListItem Selected="True" Value="0">否</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr class="table_form_btn">
                <td align="right">
                    &nbsp;
                </td>
                <td colspan="3">
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保存" />
                    <input type="button" name="Submit" value="返回" onclick="javascript:history.go(-1);" />
                    <asp:Label ID="errMsg" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
