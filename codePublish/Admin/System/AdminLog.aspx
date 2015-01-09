<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminLog.aspx.cs" Inherits="HxSoft.Web.Admin._System.AdminLog" %>

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
                                日志内容：
                            </td>
                            <td>
                                <asp:TextBox ID="txtLogContent" runat="server"></asp:TextBox>
                            </td>
                            <td style="width: 15%" align="right">
                                发生文件：
                            </td>
                            <td>
                                <asp:TextBox ID="txtScriptFile" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="table_query_row">
                            <td style="width: 15%" align="right">
                                IP地址：
                            </td>
                            <td>
                                <asp:TextBox ID="txtIPAddress" runat="server"></asp:TextBox>
                            </td>
                            <td style="width: 15%" align="right">
                                管理员帐号/姓名：
                            </td>
                            <td>
                                <asp:TextBox ID="txtAdmin" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="table_query_row">
                            <td style="width: 15%" align="right">
                                发生时间：
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtAddTime1" runat="server" onClick="WdatePicker()"/><a onclick="WdatePicker({el:'txtAddTime1'})" href="javascript:void(0)"><img src="/App_Themes/Images/calendar.gif" border="0" align="absmiddle" /></a> 至
                                <asp:TextBox ID="txtAddTime2" runat="server" onClick="WdatePicker()"/><a onclick="WdatePicker({el:'txtAddTime2'})" href="javascript:void(0)"><img src="/App_Themes/Images/calendar.gif" border="0" align="absmiddle" /></a>
                            </td>
                        </tr>
                        <tr class="table_query_btn">
                            <td align="right" style="width: 15%">
                            </td>
                            <td colspan="3">
                                <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查询" PostBackUrl="AdminLog.aspx" />
                                <input type="button" value="返回" onclick="javascript:HiddenSearchDialog();" />
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
                    管理员日志管理&gt;管理列表
                </td>
            </tr>
        </table>
        <!--导航结束-->
        <!--操作选项开始-->
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_op">
            <tr>
                <td>
                    操作选项：
                    <asp:LinkButton ID="lbtnDel" runat="server" OnClientClick="javascript:return checkDel('AdminLogID')" OnClick="btnDel_Click">删除管理员日志</asp:LinkButton>
                    <a href="javascript:SearchDialog()">查询管理员日志</a>
                </td>
            </tr>
        </table>
        <!--操作选项结束-->
        <!--列表开始-->
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list" id="TbList">
            <tr class="table_list_title">
                <td style="width: 2%">
                    <input type="checkbox" name="checkbox" value="checkbox" onclick="javascript:checkAll(this,'AdminLogID')" />
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=AdminLogID&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    编号<%=GetData.GetOrderSign(strOrderKey, "AdminLogID",strAscDesc1)%>
                </td>
                <td style="width: 10%">
                    日志内容<%=GetData.GetOrderSign(strOrderKey, "LogContent",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=ScriptFile&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    发生文件<%=GetData.GetOrderSign(strOrderKey, "ScriptFile",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=IPAddress&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    IP地址<%=GetData.GetOrderSign(strOrderKey, "IPAddress",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=AdminID&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    操作人<%=GetData.GetOrderSign(strOrderKey, "AdminID",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=AddTime&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    操作时间<%=GetData.GetOrderSign(strOrderKey, "AddTime",strAscDesc1)%>
                </td>
            </tr>
            <asp:Repeater ID="repList" runat="server">
                <ItemTemplate>
                    <tr class="<%#(Container.ItemIndex+1)%2==0?"table_list_row_alert":"table_list_row_normal" %>">
                        <td style="width: 2%">
                            <input name="AdminLogID" type="checkbox" id="AdminLogID" value="<%#Eval("AdminLogID") %>" onclick="javascript:TrColor2('AdminLogID',<%#Container.ItemIndex+1 %>)" />
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('AdminLogID',<%#Container.ItemIndex+1 %>)">
                            <%#Eval("AdminLogID")%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('AdminLogID',<%#Container.ItemIndex+1 %>)">
                            <%#Eval("LogContent")%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('AdminLogID',<%#Container.ItemIndex+1 %>)">
                            <%#Eval("ScriptFile")%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('AdminLogID',<%#Container.ItemIndex+1 %>)">
                            <%#Eval("IPAddress")%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('AdminLogID',<%#Container.ItemIndex+1 %>)">
                            <%#Factory.Admin().GetValueByField("AdminName",Eval("AdminID").ToString())%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('AdminLogID',<%#Container.ItemIndex+1 %>)">
                            <%#Eval("AddTime")%>
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
