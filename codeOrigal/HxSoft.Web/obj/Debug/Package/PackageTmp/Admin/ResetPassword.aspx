<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="HxSoft.Web.Admin.ResetPassword" %>

<%@ Import Namespace="HxSoft.Common" %>
<%@ Import Namespace="HxSoft.ClassFactory" %>
<%@ Register Src="Admin.Config.ascx" TagName="Config" TagPrefix="Admin" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>�ޱ���ҳ</title>
    <Admin:Config ID="Admin1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_form">
            <tr class="table_form_title">
                <td colspan="4">
                    �޸�����
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    ����Ա�ʺţ�
                </td>
                <td>
                    <asp:Label ID="lblAdminName" runat="server"></asp:Label>
                    &nbsp;
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    �����룺
                </td>
                <td>
                    <asp:TextBox ID="txtOldAdminPass" runat="server" TextMode="Password" Width="125px" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtOldAdminPass" Display="Dynamic" ErrorMessage="����Ϊ��" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    �����룺
                </td>
                <td>
                    <asp:TextBox ID="txtNewAdminPass" runat="server" TextMode="Password" Width="125px" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtNewAdminPass" Display="Dynamic" ErrorMessage="����Ϊ��" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    ������ȷ�ϣ�
                </td>
                <td>
                    <asp:TextBox ID="txtNewAdminPassConfirm" runat="server" TextMode="Password" Width="125px" />
                    &nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNewAdminPassConfirm" Display="Dynamic" ErrorMessage="����Ϊ��" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtNewAdminPass" ControlToValidate="txtNewAdminPassConfirm" Display="Dynamic" ErrorMessage="���������벻һ��"></asp:CompareValidator>
                </td>
            </tr>
            <tr class="table_form_btn">
                <td align="right">
                    &nbsp;
                </td>
                <td colspan="3">
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="����" />
                    <input type="reset" name="button" value="����" />
                    <asp:Label ID="errMsg" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
