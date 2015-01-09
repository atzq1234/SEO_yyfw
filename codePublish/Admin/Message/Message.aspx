<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Message.aspx.cs" Inherits="HxSoft.Web.Admin.Message.Message" %>

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
        <!--查询表单开始-->
        <div id="DivQuery">
            <div id="floatBoxBg" style="filter: alpha(opacity=0); opacity: 0;">
            </div>
            <div id="floatBox" class="floatBox">
                <div id="drag" class="title" onmousedown="DragDialog()">
                    <h4>
                        查询</h4>
                    <span>关闭</span></div>
                <div class="content">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_query">
                        <tr class="table_query_row">
                            <td style="width: 15%" align="right">
                                留言分类：
                            </td>
                            <td>
                                <asp:DropDownList ID="drpDictionaryID" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 15%" align="right">
                                标题：
                            </td>
                            <td>
                                <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="table_query_row">
                            <td style="width: 15%" align="right">
                                留言人：
                            </td>
                            <td>
                                <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                            </td>
                            <td style="width: 15%" align="right">
                                是否回复：
                            </td>
                            <td>
                                <asp:RadioButtonList ID="radIsReply" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                    <asp:ListItem Selected="True" Value="-1">不限</asp:ListItem>
                                    <asp:ListItem Value="0">未回复</asp:ListItem>
                                    <asp:ListItem Value="1">已回复</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class="table_query_btn">
                            <td align="right" style="width: 15%">
                            </td>
                            <td>
                                <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查询" PostBackUrl="Message.aspx" />
                                <input type="button" value="返回" onclick="javascript:HiddenSearchDialog();" />
                            </td>
                            <td align="right" style="width: 15%">
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <!--查询表单结束-->
        <!--导航开始-->
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_nav">
            <tr>
                <td>
                    留言管理&gt;管理列表
                </td>
            </tr>
        </table>
        <!--导航结束-->
        <!--操作选项开始-->
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_op">
            <tr>
                <td>
                    操作选项：
                    <asp:LinkButton ID="lbtnDel" runat="server" OnClientClick="javascript:return checkDel('MessageID')" OnClick="btnDel_Click">删除留言</asp:LinkButton>
                    <a href="javascript:SearchDialog()">查询留言</a>
                </td>
            </tr>
        </table>
        <!--操作选项结束-->
        <!--列表开始-->
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list" id="TbList">
            <tr class="table_list_title">
                <td style="width: 2%">
                    <input type="checkbox" name="checkbox" value="checkbox" onclick="javascript:checkAll(this,'MessageID')" />
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=Title&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    标题<%=GetData.GetOrderSign(strOrderKey, "Title",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=DictionaryID&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    留言分类<%=GetData.GetOrderSign(strOrderKey, "DictionaryID",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=UserID&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    留言人<%=GetData.GetOrderSign(strOrderKey, "UserID",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=AddTime&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    留言时间<%=GetData.GetOrderSign(strOrderKey, "AddTime",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=IsClose&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    回复状态<%=GetData.GetOrderSign(strOrderKey, "IsReply", strAscDesc1)%>
                </td>
            </tr>
            <asp:Repeater ID="repList" runat="server">
                <ItemTemplate>
                    <tr class="<%#(Container.ItemIndex+1)%2==0?"table_list_row_alert":"table_list_row_normal" %>">
                        <td style="width: 2%">
                            <input name="MessageID" type="checkbox" id="MessageID" value="<%#Eval("MessageID") %>" onclick="javascript:TrColor2('MessageID',<%#Container.ItemIndex+1 %>)" />
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('MessageID',<%#Container.ItemIndex+1 %>)" align="left">
                            <a href="Message_View.aspx?MessageID=<%#Eval("MessageID") %>&<%=UrlOrderPara + UrlPara %>page=<%=page %>">
                                <%#Eval("Title")%>(<%#Eval("ReplyCount")%>) </a>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('MessageID',<%#Container.ItemIndex+1 %>)">
                            <%#Factory.Dictionary().GetValueByField("DictionaryName", Eval("DictionaryID").ToString())%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('MessageID',<%#Container.ItemIndex+1 %>)">
                            <%#GetData.GetUserName(Eval("UserID").ToString())%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('MessageID',<%#Container.ItemIndex+1 %>)">
                            <%#Eval("AddTime")%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('MessageID',<%#Container.ItemIndex+1 %>)">
                            <%#GetData.ShowReplyStatus(Eval("IsReply").ToString())%><%#GetData.ShowEndStatus(Eval("IsEnd").ToString())%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <!--列表结束-->
        <!--分页开始-->
        <div id="pager" runat="server">
        </div>
        <!--分页结束-->
    </div>
    </form>
</body>
</html>
