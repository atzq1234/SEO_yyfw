<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Chat_Add.aspx.cs" Inherits="HxSoft.Web.Admin.Extension.Chat_Add" %>

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
                    <asp:Label ID="lblTitle" runat="server">添加</asp:Label>
                    帐号
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    语言版本：
                </td>
                <td>
                    &nbsp;
                    <asp:DropDownList ID="drpConfigID" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpConfigID" Display="Dynamic" ErrorMessage="请选择" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    帐号类型：
                </td>
                <td>
                    <asp:RadioButtonList ID="radTypeID" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="1">QQ</asp:ListItem>
                        <asp:ListItem Value="2">MSN</asp:ListItem>
                        <asp:ListItem Value="3">Skype</asp:ListItem>
                        <asp:ListItem Value="4">阿里旺旺</asp:ListItem>
                    </asp:RadioButtonList>
                    &nbsp;
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    昵称：
                </td>
                <td>
                    <asp:TextBox ID="txtNickName" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNickName" Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    帐号：
                </td>
                <td>
                    <asp:TextBox ID="txtAccount" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtAccount" Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    Key值：
                </td>
                <td>
                    <asp:TextBox ID="txtChatKey" runat="server" />
                    &nbsp;
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    排序号：
                </td>
                <td>
                    <asp:TextBox ID="txtListID" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtListID" Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtListID" Display="Dynamic" ErrorMessage="只能为数字" ValidationExpression="[0-9.]{1,}" SetFocusOnError="True"></asp:RegularExpressionValidator>
                    <asp:HiddenField ID="hidlistID" runat="server" />
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
            <tr class="table_form_btn">
                <td align="right">
                </td>
                <td colspan="3">
                    1).如QQ与MSN使用加密方式,请输入Key值,反之留空.<br />
                    2).QQ在线状态地址(取sigkey值):http://wp.qq.com/index.html<br />
                    3).MSN在线状态在址(取invitee值,不包括@apps.messenger.live.com部分)<br />
                    &nbsp; &nbsp;&nbsp; http://settings.messenger.live.com/applications/websettings.aspx
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
