<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Limit.aspx.cs" Inherits="HxSoft.Web.Admin._System.Limit" %>

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
                                字段名称：
                            </td>
                            <td>
                                <asp:TextBox ID="txtLimitField" runat="server"></asp:TextBox>
                            </td>
                            <td style="width: 15%" align="right">
                                状态：
                            </td>
                            <td>
                                <asp:RadioButtonList ID="radIsClose" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                    <asp:ListItem Selected="True" Value="-1">不限</asp:ListItem>
                                    <asp:ListItem Value="0">开放</asp:ListItem>
                                    <asp:ListItem Value="1">关闭</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class="table_query_btn">
                            <td align="right" style="width: 15%">
                            </td>
                            <td>
                                <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查询" PostBackUrl="Limit.aspx" />
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
                    权限字段&gt;管理列表(
                    <asp:Label ID="lblNav" runat="server" Text="Label"></asp:Label>
                    )
                </td>
            </tr>
        </table>
        <!--导航结束-->
        <!--操作选项开始-->
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_op">
            <tr>
                <td>
                    操作选项：
                    <asp:LinkButton ID="lbtnAdd" runat="server" OnClientClick="javascript:return GoTo('Limit_Add.aspx')">添加权限字段</asp:LinkButton>
                    <asp:LinkButton ID="lbtnEdit" runat="server" OnClientClick="javascript:return checkEdit('LimitID')" OnClick="btnEdit_Click">修改权限字段</asp:LinkButton>
                    <asp:LinkButton ID="lbtnMove" runat="server" OnClientClick="javascript:return checkOperate('LimitID')" OnClick="lbtnMove_Click">移动权限字段</asp:LinkButton>
                    <asp:LinkButton ID="lbtnDel" runat="server" OnClientClick="javascript:return checkDel('LimitID')" OnClick="btnDel_Click">删除权限字段</asp:LinkButton>
                    <asp:LinkButton ID="lbtnOpen" runat="server" OnClientClick="javascript:return checkOperate('LimitID')" OnClick="btnOpen_Click">批量开放</asp:LinkButton>
                    <asp:LinkButton ID="lbtnClose" runat="server" OnClientClick="javascript:return checkOperate('LimitID')" OnClick="btnClose_Click">批量关闭</asp:LinkButton>
                    <a href="javascript:SearchDialog()">查询权限字段</a>
                    <asp:LinkButton ID="lbtnGoBack" runat="server" OnClientClick="javascript:return GoTo('Limit.aspx')">返回上级</asp:LinkButton>
                </td>
            </tr>
        </table>
        <!--操作选项结束-->
        <!--列表开始-->
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list" id="TbList">
            <tr class="table_list_title">
                <td style="width: 2%">
                    <input type="checkbox" name="checkbox" value="checkbox" onclick="javascript:checkAll(this,'LimitID')" />
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=LimitID&AscDesc=<%=strAscDesc2 %>&ParentID=<%=ParentID %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    编号<%=GetData.GetOrderSign(strOrderKey, "LimitID", strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=LimitField&AscDesc=<%=strAscDesc2 %>&ParentID=<%=ParentID %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    权限字段名称<%=GetData.GetOrderSign(strOrderKey, "LimitField", strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=LimitValue&AscDesc=<%=strAscDesc2 %>&ParentID=<%=ParentID %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    权限字段值<%=GetData.GetOrderSign(strOrderKey, "LimitValue", strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=ChildNum&AscDesc=<%=strAscDesc2 %>&ParentID=<%=ParentID %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    子级数<%=GetData.GetOrderSign(strOrderKey, "ChildNum", strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=ListID&AscDesc=<%=strAscDesc2 %>&ParentID=<%=ParentID %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    排序号<%=GetData.GetOrderSign(strOrderKey, "ListID", strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=AdminID&AscDesc=<%=strAscDesc2 %>&ParentID=<%=ParentID %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    创建人<%=GetData.GetOrderSign(strOrderKey, "AdminID", strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=AddTime&AscDesc=<%=strAscDesc2 %>&ParentID=<%=ParentID %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    创建时间<%=GetData.GetOrderSign(strOrderKey, "AddTime", strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=IsClose&AscDesc=<%=strAscDesc2 %>&ParentID=<%=ParentID %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    状态<%=GetData.GetOrderSign(strOrderKey, "IsClose", strAscDesc1)%>
                </td>
                <td style="width: 10%">
                    操作
                </td>
            </tr>
            <asp:Repeater ID="repList" runat="server">
                <ItemTemplate>
                    <tr class="<%#(Container.ItemIndex+1)%2==0?"table_list_row_alert":"table_list_row_normal" %>">
                        <td style="width: 2%">
                            <input name="LimitID" type="checkbox" id="LimitID" value="<%#Eval("LimitID") %>" onclick="javascript:TrColor2('LimitID',<%#Container.ItemIndex+1 %>)" />
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('LimitID',<%#Container.ItemIndex+1 %>)">
                            <%#Eval("LimitID")%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('LimitID',<%#Container.ItemIndex+1 %>)">
                            <a href="Limit_Add.aspx?ParentID=<%=ParentID %>&LimitID=<%#Eval("LimitID") %>&<%=UrlOrderPara + UrlPara %>page=<%=page %>">
                                <%#Eval("LimitField")%></a><%#GetData.ShowDistStatus(Eval("IsDist").ToString())%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('LimitID',<%#Container.ItemIndex+1 %>)">
                            <%#Eval("LimitValue")%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('LimitID',<%#Container.ItemIndex+1 %>)">
                            <%#Eval("ChildNum")%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('LimitID',<%#Container.ItemIndex+1 %>)">
                            <%#Eval("ListID")%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('LimitID',<%#Container.ItemIndex+1 %>)">
                            <%#Factory.Admin().GetValueByField("AdminName",Eval("AdminID").ToString())%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('LimitID',<%#Container.ItemIndex+1 %>)">
                            <%#Eval("AddTime")%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('LimitID',<%#Container.ItemIndex+1 %>)">
                            <%#GetData.ShowCloseStatus(Eval("IsClose").ToString())%>
                        </td>
                        <td style="width: 10%">
                            <a href="Limit.aspx?ParentID=<%#Eval("LimitID") %>">进入子权限</a>
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
        <div class="note">
            注:<br>
            1.此处的权限记录为系统预设定,只可以进行关闭和开放操作<br>
            2.权限关闭后,除超级管理员外,其他管理员将没有对应的权限<br>
        </div>
    </div>
    </form>
</body>
</html>
