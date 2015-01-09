<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SurveyItem_Add.aspx.cs" Inherits="HxSoft.Web.Admin.Survey.SurveyItem_Add" %>

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
                    <asp:Label ID="lblTitle" runat="server">添加</asp:Label>调查选项
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    选项名称：
                </td>
                <td>
                    <asp:TextBox ID="txtItemName" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtItemName" Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    选项类型：
                </td>
                <td>
                    <asp:DropDownList ID="drpTypeID" runat="server">
                        <asp:ListItem Text="请选择" Value="-1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="单选" Value="1"></asp:ListItem>
                        <asp:ListItem Text="多选" Value="2"></asp:ListItem>
                        <asp:ListItem Text="文本" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="drpTypeID" Display="Dynamic" ErrorMessage="请选择" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    选择项(供单选或多选)：
                </td>
                <td>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_cancel">
                        <tr>
                            <td>
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            名称：<asp:TextBox ID="txtItemOptionName" runat="server" />
                                            <asp:Button ID="btnAddSurveyItemOption" runat="server" Text="添加选项" CausesValidation="False" OnClick="btnAddSurveyItemOption_Click" />
                                            <asp:Label ID="errSurveyItemOption" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="gvSurveyItemOption" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="4" GridLines="None" OnRowEditing="gvSurveyItemOption_RowEditing" OnRowCancelingEdit="gvSurveyItemOption_RowCancelingEdit" OnRowDeleting="gvSurveyItemOption_RowDeleting" OnRowUpdating="gvSurveyItemOption_RowUpdating" CssClass="GridView">
                        <AlternatingRowStyle CssClass="GridView_AlternatingRowStyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="名称">
                                <EditItemTemplate>
                                    <asp:TextBox ID="ItemOptionName" runat="server" Text='<%# Eval("ItemOptionName") %>' Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblItemOptionName" runat="server" Text='<%# Eval("ItemOptionName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="20%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="排序号">
                                <EditItemTemplate>
                                    <asp:TextBox ID="ListID" runat="server" Text='<%# Eval("ListID") %>' Width="100%"></asp:TextBox>
                                    <asp:HiddenField ID="OldListID" runat="server" Value='<%# Eval("ListID") %>'/>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblListID" runat="server" Text='<%# Eval("ListID") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="20%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="编辑" ShowHeader="False">
                                <EditItemTemplate>
                                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Update" Text="更新"></asp:LinkButton>
                                    <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消"></asp:LinkButton>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="编辑"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="20%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="删除" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False" CommandName="Delete" Text="删除" OnClientClick="return confirm('您真的要删除此记录吗?')"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="20%" />
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle CssClass="GridView_EditRowStyle" />
                        <HeaderStyle CssClass="GridView_HeaderStyle" />
                        <RowStyle CssClass="GridView_RowStyle" />
                    </asp:GridView>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    排序号：
                </td>
                <td>
                    <asp:TextBox ID="txtListID" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtListID" Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtListID" Display="Dynamic" ErrorMessage="只能为数字" ValidationExpression="[0-9.]{1,}" SetFocusOnError="True"></asp:RegularExpressionValidator>
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
                    <input type="button" name="Submit" value="返回" onclick="javascript:location.href='SurveyItem.aspx?SurveyID=<%=SurveyID %>';" />
                    <asp:Label ID="errMsg" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
