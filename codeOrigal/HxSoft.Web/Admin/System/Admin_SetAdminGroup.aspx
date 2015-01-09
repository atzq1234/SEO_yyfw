<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin_SetAdminGroup.aspx.cs" Inherits="HxSoft.Web.Admin._System.Admin_SetAdminGroup" %>

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
                    分配管理组
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    管理员：
                </td>
                <td>
                    <asp:Label ID="lblAdminName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    管理组：
                </td>
                <td>
                    &nbsp;
                    <asp:DropDownList ID="drpAdminGroupID" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="drpAdminGroupID" Display="Dynamic" ErrorMessage="请选择" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_btn">
                <td align="right">
                    &nbsp;
                </td>
                <td colspan="3">
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保存" />
                    <input type="button" name="Submit" value="返回" onclick="javascript:location.href='Admin.aspx?<%=UrlOrderPara+UrlPara %>&page=<%=page.ToString() %>';" />
                    <asp:Label ID="errMsg" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
        <div class="DivTitle">
            <strong>已分配管理组列表</strong></div>
        <!--列表开始-->
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="GridView" Width="100%" BorderWidth="0px" OnRowDeleting="GridView1_RowDeleting">
            <Columns>
                <asp:TemplateField HeaderText="管理组">
                    <ItemTemplate>
                        <%#Factory.AdminGroup().GetValueByField("AdminGroupName", Eval("AdminGroupID").ToString())%></ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField HeaderText="操作" ShowDeleteButton="True" />
            </Columns>
            <RowStyle CssClass="GridView_RowStyle" />
            <HeaderStyle CssClass="GridView_HeaderStyle" />
            <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" />
        </asp:GridView>
        <!--列表结束-->
    </div>
    </form>
</body>
</html>
