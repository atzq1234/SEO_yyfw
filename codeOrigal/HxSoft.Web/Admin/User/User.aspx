<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="HxSoft.Web.Admin.User.User" %>

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
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_query">
                    <tr class="table_query_row">
                        <td style="width: 15%" align="right">
                            会员级别：
                        </td>
                        <td>
                            <asp:DropDownList ID="drpUserRankID" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 15%" align="right">
                            会员帐号：
                        </td>
                        <td>
                            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="table_query_row">
                        <td style="width: 15%" align="right">
                            姓名：
                        </td>
                        <td>
                            <asp:TextBox ID="txtRealName" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 15%" align="right">
                            E-mail：
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="table_query_row">
                        <td style="width: 15%" align="right">
                            是否审核：
                        </td>
                        <td>
                            <asp:RadioButtonList ID="radIsAudit" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Selected="True" Value="-1">不限</asp:ListItem>
                                <asp:ListItem Value="0">未审核</asp:ListItem>
                                <asp:ListItem Value="1">已审核</asp:ListItem>
                            </asp:RadioButtonList>
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
                            <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查询" PostBackUrl="User.aspx" />
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
        <!--查询表单结束-->
        <!--导航开始-->
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_nav">
            <tr>
                <td>
                    会员管理&gt;管理列表
                </td>
            </tr>
        </table>
        <!--导航结束-->
        <!--操作选项开始-->
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_op">
            <tr>
                <td>
                    操作选项：
                    <asp:LinkButton ID="lbtnAdd" runat="server" OnClientClick="javascript:return GoTo('User_Add.aspx')">添加会员</asp:LinkButton>
                    <asp:LinkButton ID="lbtnEdit" runat="server" OnClientClick="javascript:return checkEdit('UserID')" OnClick="btnEdit_Click">修改会员</asp:LinkButton>
                    <asp:LinkButton ID="lbtnDel" runat="server" OnClientClick="javascript:return checkDel('UserID')" OnClick="btnDel_Click">删除会员</asp:LinkButton>
                    <asp:LinkButton ID="lbtnOpen" runat="server" OnClientClick="javascript:return checkOperate('UserID')" OnClick="btnOpen_Click">批量开放</asp:LinkButton>
                    <asp:LinkButton ID="lbtnClose" runat="server" OnClientClick="javascript:return checkOperate('UserID')" OnClick="btnClose_Click">批量关闭</asp:LinkButton>
                    <asp:LinkButton ID="lbtnAudit" runat="server" OnClientClick="javascript:return checkOperate('UserID')" OnClick="btnAudit_Click">审核</asp:LinkButton>
                    <asp:LinkButton ID="lbtnNoAudit" runat="server" OnClientClick="javascript:return checkOperate('UserID')" OnClick="btnNoAudit_Click">取消审核</asp:LinkButton>
                    <asp:LinkButton ID="lbtnImport" runat="server" OnClientClick="javascript:return GoTo('User_Import.aspx')">批量导入会员</asp:LinkButton>
                    <asp:LinkButton ID="lbtnExport" runat="server" OnClientClick="javascript:return GoTo('User_Export.aspx')">批量导出会员</asp:LinkButton>
                    <asp:LinkButton ID="lbtnEmailExport" runat="server" OnClientClick="javascript:return GoTo('User_Email_Export.aspx')">批量导出E-mail</asp:LinkButton>
                    <a href="javascript:SearchDialog()">查询会员</a>
                </td>
            </tr>
        </table>
        <!--操作选项结束-->
        <!--列表开始-->
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list" id="TbList">
            <tr class="table_list_title">
                <td style="width: 2%">
                    <input type="checkbox" name="checkbox" value="checkbox" onclick="javascript:checkAll(this,'UserID')" />
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=UserID&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    编号<%=GetData.GetOrderSign(strOrderKey, "UserID",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=UserName&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    会员帐号<%=GetData.GetOrderSign(strOrderKey, "UserName",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=RealName&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    姓名<%=GetData.GetOrderSign(strOrderKey, "RealName",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=Email&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    E-mail<%=GetData.GetOrderSign(strOrderKey, "Email",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=UserRankID&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    会员级别<%=GetData.GetOrderSign(strOrderKey, "UserRankID", strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=Point&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    积分<%=GetData.GetOrderSign(strOrderKey, "Point",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=LoginNum&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    登录次数<%=GetData.GetOrderSign(strOrderKey, "LoginNum",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=LastLoginTime&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    上次登录时间<%=GetData.GetOrderSign(strOrderKey, "LastLoginTime",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=ThisLoginTime&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    本次登录时间<%=GetData.GetOrderSign(strOrderKey, "ThisLoginTime",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=AddTime&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    注册时间<%=GetData.GetOrderSign(strOrderKey, "AddTime",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=IsClose&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    状态<%=GetData.GetOrderSign(strOrderKey, "IsClose",strAscDesc1)%>
                </td>
            </tr>
            <asp:Repeater ID="repList" runat="server">
                <ItemTemplate>
                    <tr class="<%#(Container.ItemIndex+1)%2==0?"table_list_row_alert":"table_list_row_normal" %>">
                        <td style="width: 2%">
                            <input name="UserID" type="checkbox" id="UserID" value="<%#Eval("UserID") %>" onclick="javascript:TrColor2('UserID',<%#Container.ItemIndex+1 %>)" />
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('UserID',<%#Container.ItemIndex+1 %>)">
                            <%#Eval("UserID")%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('UserID',<%#Container.ItemIndex+1 %>)">
                            <a href="User_Add.aspx?UserID=<%#Eval("UserID") %>&<%=UrlOrderPara + UrlPara %>page=<%=page %>">
                                <%#Eval("UserName")%>
                            </a>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('UserID',<%#Container.ItemIndex+1 %>)">
                            <%#Eval("RealName")%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('UserID',<%#Container.ItemIndex+1 %>)">
                            <%#Eval("Email")%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('UserID',<%#Container.ItemIndex+1 %>)">
                            <%#Factory.UserRank().GetValueByField("UserRankName", Eval("UserRankID").ToString())%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('UserID',<%#Container.ItemIndex+1 %>)">
                            <%#Eval("Point")%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('UserID',<%#Container.ItemIndex+1 %>)">
                            <%#Eval("LoginNum")%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('UserID',<%#Container.ItemIndex+1 %>)">
                            <%#GetData.CheckDate(Eval("LastLoginTime").ToString())%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('UserID',<%#Container.ItemIndex+1 %>)">
                            <%#GetData.CheckDate(Eval("ThisLoginTime").ToString())%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('UserID',<%#Container.ItemIndex+1 %>)">
                            <%#Eval("AddTime")%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('UserID',<%#Container.ItemIndex+1 %>)">
                            <%#GetData.ShowAuditStatus(Eval("IsAudit").ToString())%>
                            |
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
