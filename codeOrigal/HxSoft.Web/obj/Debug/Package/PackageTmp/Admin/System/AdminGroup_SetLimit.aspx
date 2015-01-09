<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminGroup_SetLimit.aspx.cs" Inherits="HxSoft.Web.Admin._System.AdminGroup_SetLimit" %>

<%@ Import Namespace="HxSoft.Common" %>
<%@ Import Namespace="HxSoft.ClassFactory" %>
<%@ Register Src="../Admin.Config.ascx" TagName="Config" TagPrefix="Admin" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>�ޱ���ҳ</title>
    <Admin:Config ID="Admin1" runat="server" />
    <script type="text/javascript">
        $(function () {
            $(".input_p").click(function () {
                var i = $(".input_p").index(this);
                $(".input_c").eq(i).find("input").attr("checked", $(this).attr("checked"));
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_form">
            <tr class="table_form_title">
                <td colspan="4">
                    �������Ȩ��
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    �����飺
                </td>
                <td>
                    <asp:Label ID="lblAdminGroupName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    ѡ��
                </td>
                <td>
                    <input type="checkbox" name="checkAll" value="checkbox" id="checkAll" onclick="javascript:checkAll2(this,'LimitValue')" />
                    <label for="checkAll">
                        ѡ��ȫ��</label>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    ����Ȩ�ޣ�
                </td>
                <td>
                    <asp:DataList ID="DataList1" RepeatColumns="2" runat="server" OnItemDataBound="DataList1_ItemDataBound" Width="100%">
                        <ItemTemplate>
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_cancel">
                                <tr>
                                    <td>
                                        <input type="checkbox" name="LimitValue" id="Limit<%#Eval("LimitID") %>" value="<%#Eval("LimitValue") %>" <%#GetData.CheckAdminGroupLimitValue(AdminGroupID, Eval("LimitValue").ToString()) %>  class="input_p"/>
                                        <label for="Limit<%#Eval("LimitID") %>">
                                            <strong>
                                                <%#Eval("LimitField")%></strong></label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="input_c">
                                        <asp:DataList ID="DataList2" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <ItemTemplate>
                                                <input type="checkbox" name="LimitValue" id="Limit<%#Eval("LimitID") %>" value="<%#Eval("LimitValue") %>" <%#GetData.CheckAdminGroupLimitValue(AdminGroupID, Eval("LimitValue").ToString()) %> />
                                                <label for="Limit<%#Eval("LimitID") %>">
                                                    <%#Eval("LimitField")%></label>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                        <ItemStyle Width="50%" Height="50px" VerticalAlign="Top" />
                    </asp:DataList>
                </td>
            </tr>
            <tr class="table_form_btn">
                <td align="right">
                    &nbsp;
                </td>
                <td colspan="3">
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="����" />
                    <input type="button" name="Submit" value="����" onclick="javascript:location.href='AdminGroup.aspx?<%=UrlOrderPara + UrlPara %>&page=<%=page %>';" />
                    <asp:Label ID="errMsg" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
