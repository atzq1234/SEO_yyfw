<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Message_View.aspx.cs" Inherits="HxSoft.Web.Admin.Message.Message_View" %>

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
                <td colspan="3">
                    回复留言
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    创建信息：
                </td>
                <td>
                    <asp:Label ID="lblAddTime" runat="server"></asp:Label>
                    |
                    <asp:Label ID="lblUserName" runat="server"></asp:Label>
                    <asp:HiddenField ID="hidDictionaryID" runat="server" />
                </td>
                <td style="width: 15%" align="center">
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    标题：
                </td>
                <td>
                    <asp:Label ID="lblTitle" runat="server"></asp:Label>
                </td>
                <td style="width: 15%" align="center">
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    内容：
                </td>
                <td align="left">
                    <asp:Label ID="lblMessageContent" runat="server"></asp:Label>
                </td>
                <td style="width: 15%" align="center">
                </td>
            </tr>
            <asp:Repeater ID="repList" runat="server" OnItemCommand="repList_ItemCommand">
                <ItemTemplate>
                    <tr class="table_form_row">
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr class="table_form_row">
                        <td style="width: 15%" align="right">
                            #<%# Container.ItemIndex + 1%>楼：
                        </td>
                        <td>
                            <%#Eval("AddTime") %>
                            |
                            <%#GetData.GetUserName(Eval("UserID").ToString())%>
                            <%#Factory.Admin().GetValueByField("AdminName",Eval("AdminID").ToString())%>
                        </td>
                        <td style="width: 15%" align="center">
                            <%if (IsCanDel)
                              {%><asp:LinkButton ID="lbtnDel" CommandName="Del" CommandArgument='<%#Eval("MessageID") %>' OnClientClick="return confirm('确认要删除吗？')" runat="server" CausesValidation="false">删除</asp:LinkButton><%} %>
                        </td>
                    </tr>
                    <tr class="table_form_row">
                        <td style="width: 15%" align="right">
                        </td>
                        <td>
                            <%#Eval("MessageContent") %>
                        </td>
                        <td style="width: 15%" align="center">
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr class="table_form_row">
                <td colspan="3">
                </td>
            </tr>
            <tr class="table_form_row" id="tr1_1" runat="server">
                <td style="width: 15%" align="right">
                    回复内容：
                </td>
                <td>
                    <asp:TextBox ID="txtMessageContent" runat="server" Columns="40" Rows="8" TextMode="MultiLine" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtMessageContent" Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
                <td style="width: 15%">
                </td>
            </tr>
            <tr class="table_form_row" id="tr1_2" runat="server">
                <td style="width: 15%" align="right">
                    &nbsp;
                </td>
                <td>
                    <asp:CheckBox ID="chkIsEnd" runat="server" Text="是否结束该留言" />
                </td>
                <td style="width: 15%">
                    &nbsp;
                </td>
            </tr>
            <tr class="table_form_btn" id="tr1_3" runat="server">
                <td align="right">
                    &nbsp;
                </td>
                <td>
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保存" />
                    <input type="button" name="Submit" value="返回" onclick="location.href='Message.aspx?<%=UrlOrderPara + UrlPara %>page=<%=page %>';" />
                    <asp:Label ID="errMsg" runat="server" ForeColor="Red"></asp:Label>
                </td>
                <td style="width: 15%">
                </td>
            </tr>
            <tr class="table_form_btn" id="tr2_1" runat="server">
                <td align="right">
                    &nbsp;
                </td>
                <td>
                    <asp:Button ID="btnReply" runat="server" OnClick="btnReply_Click" Text="设置为已回复" />
                    <input type="button" name="Submit" value="返回" onclick="location.href='Message.aspx?<%=UrlOrderPara + UrlPara %>page=<%=page %>';" />
                </td>
                <td style="width: 15%">
                </td>
            </tr>
            <tr class="table_form_btn" id="tr2_2" runat="server">
                <td align="right">
                    &nbsp;
                </td>
                <td>
                    <input type="button" name="Submit" value="返回" onclick="location.href='Message.aspx?<%=UrlOrderPara + UrlPara %>page=<%=page %>';" />
                </td>
                <td style="width: 15%">
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
