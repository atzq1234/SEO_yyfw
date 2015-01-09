<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Feedback.aspx.cs" Inherits="HxSoft.Web.Admin.Message.Feedback" %>

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
                                反馈类型：
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
                                内容：
                            </td>
                            <td>
                                <asp:TextBox ID="txtFeedbackContent" runat="server"></asp:TextBox>
                            </td>
                            <td style="width: 15%" align="right">
                                IP地址：
                            </td>
                            <td>
                                <asp:TextBox ID="txtIpAddress" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="table_query_row">
                            <td style="width: 15%" align="right">
                                反馈时间：
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtAddTime1" runat="server" onClick="WdatePicker()"/><a onclick="WdatePicker({el:'txtAddTime1'})" href="javascript:void(0)"><img src="/App_Themes/Images/calendar.gif" border="0" align="absmiddle" /></a> 至
                                <asp:TextBox ID="txtAddTime2" runat="server" onClick="WdatePicker()"/><a onclick="WdatePicker({el:'txtAddTime2'})" href="javascript:void(0)"><img src="/App_Themes/Images/calendar.gif" border="0" align="absmiddle" /></a>
                            </td>
                        </tr>
                        <tr class="table_query_row">
                            <td style="width: 15%" align="right">
                                是否处理：
                            </td>
                            <td colspan="3">
                                <asp:RadioButtonList ID="radIsDeal" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                    <asp:ListItem Selected="True" Value="-1">不限</asp:ListItem>
                                    <asp:ListItem Value="0">未处理</asp:ListItem>
                                    <asp:ListItem Value="1">已处理</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class="table_query_btn">
                            <td align="right" style="width: 15%">
                            </td>
                            <td>
                                <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查询" PostBackUrl="Feedback.aspx" />
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
                    反馈管理&gt;管理列表
                </td>
            </tr>
        </table>
        <!--导航结束-->
        <!--操作选项开始-->
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_op">
            <tr>
                <td>
                    操作选项：
                    <asp:LinkButton ID="lbtnDel" runat="server" OnClientClick="javascript:return checkDel('FeedbackID')" OnClick="btnDel_Click">删除信息反馈</asp:LinkButton>
                    <a href="javascript:SearchDialog()">查询信息反馈</a>
                </td>
            </tr>
        </table>
        <!--操作选项结束-->
        <!--列表开始-->
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list" id="TbList">
            <tr class="table_list_title">
                <td style="width: 2%">
                    <input type="checkbox" name="checkbox" value="checkbox" onclick="javascript:checkAll(this,'FeedbackID')" />
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=FeedbackID&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    自动编号<%=GetData.GetOrderSign(strOrderKey, "FeedbackID",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=Title&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    标题<%=GetData.GetOrderSign(strOrderKey, "Title",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=DictionaryID&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    反馈类型<%=GetData.GetOrderSign(strOrderKey, "DictionaryID",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=IpAddress&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    IP地址<%=GetData.GetOrderSign(strOrderKey, "IpAddress",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=AddTime&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    反馈时间<%=GetData.GetOrderSign(strOrderKey, "AddTime",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=IsDeal&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    是否处理<%=GetData.GetOrderSign(strOrderKey, "IsDeal",strAscDesc1)%>
                </td>
            </tr>
            <asp:Repeater ID="repList" runat="server">
                <ItemTemplate>
                    <tr class="<%#(Container.ItemIndex+1)%2==0?"table_list_row_alert":"table_list_row_normal" %>">
                        <td style="width: 2%">
                            <input name="FeedbackID" type="checkbox" id="FeedbackID" value="<%#Eval("FeedbackID") %>" onclick="javascript:TrColor2('FeedbackID',<%#Container.ItemIndex+1 %>)" />
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('FeedbackID',<%#Container.ItemIndex+1 %>)">
                            <%#Eval("FeedbackID")%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('FeedbackID',<%#Container.ItemIndex+1 %>)">
                            <a href="Feedback_View.aspx?FeedbackID=<%#Eval("FeedbackID") %>&<%=UrlOrderPara + UrlPara %>page=<%=page %>">
                                <%#Eval("Title")%></a>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('FeedbackID',<%#Container.ItemIndex+1 %>)">
                            <%#Factory.Dictionary().GetValueByField("DictionaryName", Eval("DictionaryID").ToString())%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('FeedbackID',<%#Container.ItemIndex+1 %>)">
                            <%#Eval("IpAddress")%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('FeedbackID',<%#Container.ItemIndex+1 %>)">
                            <%#Eval("AddTime")%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('FeedbackID',<%#Container.ItemIndex+1 %>)">
                            <%#GetData.ShowDealStatus(Eval("IsDeal").ToString())%>
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
