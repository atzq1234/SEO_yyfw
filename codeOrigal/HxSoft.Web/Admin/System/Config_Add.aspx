<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Config_Add.aspx.cs" Inherits="HxSoft.Web.Admin._System.Config_Add" ValidateRequest="False" %>

<%@ Import Namespace="HxSoft.Common" %>
<%@ Import Namespace="HxSoft.ClassFactory" %>
<%@ Register Assembly="Brettle.Web.NeatUpload" Namespace="Brettle.Web.NeatUpload" TagPrefix="Upload" %>
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
                    <asp:Label ID="lblTitle" runat="server">添加</asp:Label>网站配置
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    网站状态：
                </td>
                <td>
                    <asp:RadioButtonList ID="radIsClose" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="0">开放</asp:ListItem>
                        <asp:ListItem Value="1">关闭</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    语言版本：
                </td>
                <td>
                    <asp:TextBox ID="txtLanguageVer" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtLanguageVer" Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    网站名称：
                </td>
                <td>
                    <asp:TextBox ID="txtWebsiteName" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtWebsiteName" Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    网站目录：
                </td>
                <td>
                    <asp:TextBox ID="txtWebsiteUrl" runat="server" />注:以"/"开始,以"/"结束,如"/cn/"
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtWebsiteUrl" Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    网站关键字：
                </td>
                <td>
                    <asp:TextBox ID="txtWebsiteKeywords" runat="server" Columns="40" Rows="8" TextMode="MultiLine" />
                    网站关键字:多个请用“,”不超过100个字符,分隔,显示在META中
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    网站描述：
                </td>
                <td>
                    <asp:TextBox ID="txtWebsiteDescription" runat="server" Columns="40" Rows="8" TextMode="MultiLine" />
                    不超过200个字符,分隔,显示在META中
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    邮件接收地址：
                </td>
                <td>
                    <asp:TextBox ID="txtMailReceiveAddress" runat="server" />多个请用逗号“,”隔开
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtMailReceiveAddress" Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    邮件发送服务器：
                </td>
                <td>
                    <asp:TextBox ID="txtMailSmtpServer" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtMailSmtpServer" Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    邮件发送帐号：
                </td>
                <td>
                    <asp:TextBox ID="txtMailUserName" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtMailUserName" Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="请输入正确的邮箱帐号" ControlToValidate="txtMailUserName" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" SetFocusOnError="True"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    邮件发送密码：
                </td>
                <td>
                    <asp:TextBox ID="txtMailPassword" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtMailPassword" Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    邮件发送帐号对应的密码,如需更改,请直接输入密码明文,系统将自动进行加密处理
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    网站底部信息：
                </td>
                <td>
                    <asp:TextBox ID="txtFooterInfo" runat="server" Columns="40" Rows="8" TextMode="MultiLine" />
                    支持HTML
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    排序号：
                </td>
                <td>
                    <asp:TextBox ID="txtListID" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtListID" Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtListID" Display="Dynamic" ErrorMessage="只能为数字" ValidationExpression="[0-9.]{1,}" SetFocusOnError="True"></asp:RegularExpressionValidator>
                    <asp:HiddenField ID="hidlistID" runat="server" />
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
