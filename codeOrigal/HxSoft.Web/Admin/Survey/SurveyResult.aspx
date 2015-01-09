<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SurveyResult.aspx.cs" Inherits="HxSoft.Web.Admin.Survey.SurveyResult" %>

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
        <!--导航开始-->
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_nav">
            <tr>
                <td>
                    调查结果管理&gt;管理列表(<asp:Label ID="lblSubject" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>)
                </td>
            </tr>
        </table>
        <!--导航结束-->
        <!--操作选项开始-->
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_op">
            <tr>
                <td>
                    操作选项：
                    <asp:LinkButton ID="lbtnDel" runat="server" OnClientClick="javascript:return checkDel('SurveyResultID')" OnClick="btnDel_Click">删除调查结果</asp:LinkButton>
                    <a href="Survey.aspx">返回调查列表</a>
                </td>
            </tr>
        </table>
        <!--操作选项结束-->
        <!--列表开始-->
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list" id="TbList">
            <tr class="table_list_title">
                <td style="width: 2%">
                    <input type="checkbox" name="checkbox" value="checkbox" onclick="javascript:checkAll(this,'SurveyResultID')" />
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?SurveyID=<%=SurveyID %>&OrderKey=SurveyResultID&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    编号<%=GetData.GetOrderSign(strOrderKey, "SurveyResultID",strAscDesc1)%>
                </td>
                <td style="width: 10%">
                    调查结果
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?SurveyID=<%=SurveyID %>&OrderKey=ItemName&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    提交IP<%=GetData.GetOrderSign(strOrderKey, "ItemName",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?SurveyID=<%=SurveyID %>&OrderKey=AddTime&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="点击排序">
                    提交时间<%=GetData.GetOrderSign(strOrderKey, "AddTime",strAscDesc1)%>
                </td>
            </tr>
            <asp:Repeater ID="repList" runat="server">
                <ItemTemplate>
                    <tr class="<%#(Container.ItemIndex+1)%2==0?"table_list_row_alert":"table_list_row_normal" %>">
                        <td style="width: 2%">
                            <input name="SurveyResultID" type="checkbox" id="SurveyResultID" value="<%#Eval("SurveyResultID") %>" onclick="javascript:TrColor2('SurveyResultID',<%#Container.ItemIndex+1 %>)" />
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('SurveyResultID',<%#Container.ItemIndex+1 %>)">
                            <%#Eval("SurveyResultID")%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('SurveyResultID',<%#Container.ItemIndex+1 %>)">
                            <a href="SurveyResult_Details.aspx?SurveyResultID=<%#Eval("SurveyResultID") %>&SurveyID=<%=SurveyID %>>&ProductPage=<%=page %>&<%=UrlOrderPara + UrlPara %>page=<%=page %>">
                                调查结果<%#Container.ItemIndex + 1%>
                            </a>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('SurveyResultID',<%#Container.ItemIndex+1 %>)">
                            <%#Eval("IP")%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('SurveyResultID',<%#Container.ItemIndex+1 %>)">
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
