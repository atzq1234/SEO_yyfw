<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Job_Add.aspx.cs" Inherits="HxSoft.Web.Admin.Job.Job_Add" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
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
                <td colspan="2">
                    <asp:Label ID="lblTitle" runat="server">添加</asp:Label>招聘
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    语言版本：
                </td>
                <td>
                    <asp:DropDownList ID="drpConfigID" runat="server" AutoPostBack="True" onselectedindexchanged="drpConfigID_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="drpConfigID" Display="Dynamic" ErrorMessage="请选择" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    栏目分类：
                </td>
                <td>
                    <asp:DropDownList ID="drpClassID" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="drpClassID" Display="Dynamic" ErrorMessage="请选择" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    职位名称：
                </td>
                <td>
                    <asp:TextBox ID="txtJobName" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtJobName" Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    招聘部门：
                </td>
                <td>
                    <asp:TextBox ID="txtDepartment" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDepartment" Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    招聘人数：
                </td>
                <td>
                    <asp:TextBox ID="txtJobNum" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtJobNum" Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    待遇：
                </td>
                <td>
                    <asp:TextBox ID="txtSalary" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSalary" Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    工作地点：
                </td>
                <td>
                    <asp:TextBox ID="txtWorkPlace" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtWorkPlace" Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row" style="display: none">
                <td style="width: 15%" align="right">
                    标签：
                </td>
                <td>
                    <asp:TextBox ID="txtTags" runat="server" />
                多个标签请用","分隔
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    关键字：
                </td>
                <td>
                    <asp:TextBox ID="txtKeywords" runat="server" />多个关键字请用","分隔
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    内容概要：
                </td>
                <td>
                    <asp:TextBox ID="txtDescription" runat="server" Columns="50" Rows="8" TextMode="MultiLine" />
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    招聘要求：
                </td>
                <td>
                    <asp:TextBox ID="txtDemand" runat="server" Columns="120" Rows="20" TextMode="MultiLine" />
                    <%--
                    <FCKeditorV2:FCKeditor ID="txtDemand" runat="server" Height="400px" Width="95%">
                    </FCKeditorV2:FCKeditor>
                    --%>
                </td>
            </tr>
            <%--<tr class="table_form_row">
                <td style="width: 15%" align="right">
                </td>
                <td>
                    <input id="btnFormat1" type="button" value="自动排版编辑内容" onclick="FormatText('txtDemand___Frame')" />
                </td>
            </tr>--%>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    截止日期：
                </td>
                <td>
                    <asp:TextBox ID="txtEndTime" runat="server" onClick="WdatePicker()"/><a onclick="WdatePicker({el:'txtEndTime'})" href="javascript:void(0)"><img src="/App_Themes/Images/calendar.gif" border="0" align="absmiddle" /></a>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtEndTime" Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    设置：
                </td>
                <td>
                    <asp:CheckBox ID="chkIsRecommend" runat="server" Text="推荐" />
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    访问数：
                </td>
                <td>
                    <asp:TextBox ID="txtClickNum" runat="server" CssClass="input_shadow">0</asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtClickNum" Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtClickNum" Display="Dynamic" ErrorMessage="只能为数字" ValidationExpression="[0-9]{1,}" SetFocusOnError="True"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    排序号：
                </td>
                <td>
                    <asp:TextBox ID="txtListID" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtListID" Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtListID" Display="Dynamic" ErrorMessage="只能为数字" ValidationExpression="[0-9.]{1,}" SetFocusOnError="True"></asp:RegularExpressionValidator>
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
                    <div id="hiddendiv" style="display: none;">
                    </div>
                </td>
                <td>
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
