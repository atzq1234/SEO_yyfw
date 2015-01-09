<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Folder_Create.aspx.cs" Inherits="HxSoft.Web.Admin.Upload.Folder_Create" %>

<%@ Import Namespace="HxSoft.Common" %>
<%@ Import Namespace="HxSoft.ClassFactory" %>
<%@ Register Src="../Admin.Config.ascx" TagName="Config" TagPrefix="Admin" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>�ޱ���ҳ</title>
    <Admin:Config ID="Admin1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
        <table width="90%" border="0" cellpadding="0" cellspacing="0" style="margin: 0 auto 0 auto" class="table_file_form">
            <tr>
                <td align="left" style="height: 20px">
                </td>
            </tr>
            <tr>
                <td align="left" style="height: 20px">
                    �ļ������ƣ�<asp:TextBox ID="txtFolderName" runat="server"></asp:TextBox>
                    <asp:Button ID="btnCreateFolder" runat="server" Text="����" OnClick="btnCreateFolder_Click" />
                </td>
            </tr>
            <tr>
                <td align="left" style="height: 20px">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFolderName" Display="Dynamic" ErrorMessage="�ļ������Ʋ���Ϊ��" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtFolderName" Display="Dynamic" ErrorMessage="�ļ�������ֻ�ܰ�����ĸ�����֡��»���" ValidationExpression="[0-9A-Za-z_]{1,}" SetFocusOnError="True"></asp:RegularExpressionValidator>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
