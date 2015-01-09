﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Block.aspx.cs" Inherits="HxSoft.Web.Admin.Block.Block" %>

<%@ Import Namespace="HxSoft.Common" %>
<%@ Import Namespace="HxSoft.ClassFactory" %>
<%@ Register Src="../Admin.Config.ascx" TagName="Config" TagPrefix="Admin" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
                                标题：
                            </td>
                            <td>
                                <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
                            </td>
                            <td style="width: 15%" align="right">
                                内容：
                            </td>
                            <td>
                                <asp:TextBox ID="txtBlockContent" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="table_query_btn">
                            <td align="right" style="width: 15%">
                            </td>
                            <td>
                                <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查询" PostBackUrl="Block.aspx" />
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
                    片段内容管理&gt;管理列表
                </td>
            </tr>
        </table>
        <!--导航结束-->
        <!--操作选项开始-->
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_op">
            <tr>
                <td>
                    操作选项：
                    <asp:LinkButton ID="lbtnAdd" runat="server" OnClientClick="javascript:return GoTo('Block_Add.aspx')">添加片段内容</asp:LinkButton>
                    <asp:LinkButton ID="lbtnEdit" runat="server" OnClientClick="javascript:return checkEdit('BlockID')" OnClick="btnEdit_Click">修改片段内容</asp:LinkButton>
                    <asp:LinkButton ID="lbtnDel" runat="server" OnClientClick="javascript:return checkDel('BlockID')" OnClick="btnDel_Click">删除片段内容</asp:LinkButton>
                    <asp:LinkButton ID="lbtnOpen" runat="server" OnClientClick="javascript:return checkOperate('BlockID')" OnClick="btnOpen_Click">批量开放</asp:LinkButton>
                    <asp:LinkButton ID="lbtnClose" runat="server" OnClientClick="javascript:return checkOperate('BlockID')" OnClick="btnClose_Click">批量关闭</asp:LinkButton>
                    <a href="javascript:SearchDialog()">查询片段内容</a>
                </td>
            </tr>
        </table>
        <!--操作选项结束-->
        <!--列表开始-->
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list" id="TbList">
            <tr class="table_list_title">
                <td style="width: 2%">
                    <input type="checkbox" name="checkbox" value="checkbox" onclick="javascript:checkAll(this,'BlockID')" />
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=BlockID&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    编号<%=GetData.GetOrderSign(strOrderKey, "BlockID",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=Title&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    片段标题<%=GetData.GetOrderSign(strOrderKey, "Title",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=AdminID&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    创建人<%=GetData.GetOrderSign(strOrderKey, "AdminID",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=AddTime&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    创建时间<%=GetData.GetOrderSign(strOrderKey, "AddTime",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=IsClose&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    状态<%=GetData.GetOrderSign(strOrderKey, "IsClose",strAscDesc1)%>
                </td>
            </tr>
            <asp:Repeater ID="repList" runat="server">
                <ItemTemplate>
                    <tr class="<%#(Container.ItemIndex+1)%2==0?"table_list_row_alert":"table_list_row_normal" %>">
                        <td style="width: 2%">
                            <input name="BlockID" type="checkbox" id="BlockID" value="<%#Eval("BlockID") %>" onclick="javascript:TrColor2('BlockID',<%#Container.ItemIndex+1 %>)" />
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('BlockID',<%#Container.ItemIndex+1 %>)">
                            <%#Eval("BlockID")%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('BlockID',<%#Container.ItemIndex+1 %>)">
                            <a href="Block_Add.aspx?BlockID=<%#Eval("BlockID") %>&<%=UrlOrderPara + UrlPara %>page=<%=page %>">
                                <%#Eval("Title")%></a>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('BlockID',<%#Container.ItemIndex+1 %>)">
                            <%#Factory.Admin().GetValueByField("AdminName",Eval("AdminID").ToString())%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('BlockID',<%#Container.ItemIndex+1 %>)">
                            <%#Eval("AddTime")%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('BlockID',<%#Container.ItemIndex+1 %>)">
                            <%#GetData.ShowCloseStatus(Eval("IsClose").ToString())%>
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