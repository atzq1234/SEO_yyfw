<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SurveyItem.aspx.cs" Inherits="HxSoft.Web.Admin.Survey.SurveyItem" %>

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
                                选项名称：
                            </td>
                            <td>
                                <asp:TextBox ID="txtItemName" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="table_query_row">
                            <td style="width: 15%" align="right">
                                选项类型：
                            </td>
                            <td>
                                <asp:DropDownList ID="drpTypeID" runat="server">
                                    <asp:ListItem Text="不限" Value="-1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="单选" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="多选" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="文本" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="table_query_row">
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
                            <td align="right" style="width: 15%">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr class="table_query_btn">
                            <td align="right" style="width: 15%">
                            </td>
                            <td>
                                <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查询" PostBackUrl="SurveyItem.aspx" />
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
                    调查选项管理&gt;管理列表(<asp:Label ID="lblSubject" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>)
                </td>
            </tr>
        </table>
        <!--导航结束-->
        <!--操作选项开始-->
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_op">
            <tr>
                <td>
                    操作选项：
                    <asp:LinkButton ID="lbtnAdd" runat="server" OnClientClick="javascript:return GoTo('SurveyItem_Add.aspx')">添加调查选项</asp:LinkButton>
                    <asp:LinkButton ID="lbtnEdit" runat="server" OnClientClick="javascript:return checkEdit('SurveyItemID')" OnClick="lbtnEdit_Click">修改调查选项</asp:LinkButton>
                    <asp:LinkButton ID="lbtnDel" runat="server" OnClientClick="javascript:return checkDel('SurveyItemID')" OnClick="btnDel_Click">删除调查选项</asp:LinkButton>
                    <asp:LinkButton ID="lbtnOpen" runat="server" OnClientClick="javascript:return checkOperate('SurveyItemID')" OnClick="btnOpen_Click">批量开放</asp:LinkButton>
                    <asp:LinkButton ID="lbtnClose" runat="server" OnClientClick="javascript:return checkOperate('SurveyItemID')" OnClick="btnClose_Click">批量关闭</asp:LinkButton>
                    <a href="javascript:SearchDialog()">查询调查选项</a> <a href="Survey.aspx">返回调查列表</a>
                </td>
            </tr>
        </table>
        <!--操作选项结束-->
        <!--列表开始-->
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list" id="TbList">
            <tr class="table_list_title">
                <td style="width: 2%">
                    <input type="checkbox" name="checkbox" value="checkbox" onclick="javascript:checkAll(this,'SurveyItemID')" />
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?SurveyID=<%=SurveyID %>&OrderKey=SurveyItemID&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    编号<%=GetData.GetOrderSign(strOrderKey, "SurveyItemID",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?SurveyID=<%=SurveyID %>&OrderKey=ItemName&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    选项名称<%=GetData.GetOrderSign(strOrderKey, "ItemName",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?SurveyID=<%=SurveyID %>&OrderKey=TypeID&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    选项类型<%=GetData.GetOrderSign(strOrderKey, "TypeID",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?SurveyID=<%=SurveyID %>&OrderKey=ListID&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    排序号<%=GetData.GetOrderSign(strOrderKey, "ListID",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?SurveyID=<%=SurveyID %>&OrderKey=AdminID&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    创建人<%=GetData.GetOrderSign(strOrderKey, "AdminID",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?SurveyID=<%=SurveyID %>&OrderKey=AddTime&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    创建时间<%=GetData.GetOrderSign(strOrderKey, "AddTime",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?SurveyID=<%=SurveyID %>&OrderKey=IsClose&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    状态<%=GetData.GetOrderSign(strOrderKey, "IsClose",strAscDesc1)%>
                </td>
            </tr>
            <asp:Repeater ID="repList" runat="server">
                <ItemTemplate>
                    <tr class="<%#(Container.ItemIndex+1)%2==0?"table_list_row_alert":"table_list_row_normal" %>">
                        <td style="width: 2%">
                            <input name="SurveyItemID" type="checkbox" id="SurveyItemID" value="<%#Eval("SurveyItemID") %>" onclick="javascript:TrColor2('SurveyItemID',<%#Container.ItemIndex+1 %>)" />
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('SurveyItemID',<%#Container.ItemIndex+1 %>)">
                            <%#Eval("SurveyItemID")%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('SurveyItemID',<%#Container.ItemIndex+1 %>)">
                            <a href="SurveyItem_Add.aspx?SurveyItemID=<%#Eval("SurveyItemID") %>&SurveyID=<%=SurveyID %>&<%=UrlOrderPara + UrlPara %>page=<%=page %>">
                                <%#Eval("ItemName")%>
                            </a>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('SurveyItemID',<%#Container.ItemIndex+1 %>)">
                            <%#GetData.ShowSurveyItemType(Eval("TypeID").ToString())%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('SurveyItemID',<%#Container.ItemIndex+1 %>)">
                            <%#Eval("ListID")%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('SurveyItemID',<%#Container.ItemIndex+1 %>)">
                            <%#Factory.Admin().GetValueByField("AdminName",Eval("AdminID").ToString())%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('SurveyItemID',<%#Container.ItemIndex+1 %>)">
                            <%#Eval("AddTime")%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('SurveyItemID',<%#Container.ItemIndex+1 %>)">
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
