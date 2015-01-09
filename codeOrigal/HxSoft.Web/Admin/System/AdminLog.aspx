<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminLog.aspx.cs" Inherits="HxSoft.Web.Admin._System.AdminLog" %>

<%@ Import Namespace="HxSoft.Common" %>
<%@ Import Namespace="HxSoft.ClassFactory" %>
<%@ Register Src="../Admin.Config.ascx" TagName="Config" TagPrefix="Admin" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>�ޱ���ҳ</title>
    <Admin:Config ID="Admin1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
        <!--��ѯ����ʼ-->
        <div id="DivQuery">
            <div id="floatBoxBg" style="filter: alpha(opacity=0); opacity: 0;">
            </div>
            <div id="floatBox" class="floatBox">
                <div id="drag" class="title" onmousedown="DragDialog()">
                    <h4>
                        ��ѯ</h4>
                    <span>�ر�</span></div>
                <div class="content">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_query">
                        <tr class="table_query_row">
                            <td style="width: 15%" align="right">
                                ��־���ݣ�
                            </td>
                            <td>
                                <asp:TextBox ID="txtLogContent" runat="server"></asp:TextBox>
                            </td>
                            <td style="width: 15%" align="right">
                                �����ļ���
                            </td>
                            <td>
                                <asp:TextBox ID="txtScriptFile" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="table_query_row">
                            <td style="width: 15%" align="right">
                                IP��ַ��
                            </td>
                            <td>
                                <asp:TextBox ID="txtIPAddress" runat="server"></asp:TextBox>
                            </td>
                            <td style="width: 15%" align="right">
                                ����Ա�ʺ�/������
                            </td>
                            <td>
                                <asp:TextBox ID="txtAdmin" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="table_query_row">
                            <td style="width: 15%" align="right">
                                ����ʱ�䣺
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtAddTime1" runat="server" onClick="WdatePicker()"/><a onclick="WdatePicker({el:'txtAddTime1'})" href="javascript:void(0)"><img src="/App_Themes/Images/calendar.gif" border="0" align="absmiddle" /></a> ��
                                <asp:TextBox ID="txtAddTime2" runat="server" onClick="WdatePicker()"/><a onclick="WdatePicker({el:'txtAddTime2'})" href="javascript:void(0)"><img src="/App_Themes/Images/calendar.gif" border="0" align="absmiddle" /></a>
                            </td>
                        </tr>
                        <tr class="table_query_btn">
                            <td align="right" style="width: 15%">
                            </td>
                            <td colspan="3">
                                <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="��ѯ" PostBackUrl="AdminLog.aspx" />
                                <input type="button" value="����" onclick="javascript:HiddenSearchDialog();" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <!--��ѯ������-->
        <!--������ʼ-->
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_nav">
            <tr>
                <td>
                    ����Ա��־����&gt;�����б�
                </td>
            </tr>
        </table>
        <!--��������-->
        <!--����ѡ�ʼ-->
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_op">
            <tr>
                <td>
                    ����ѡ�
                    <asp:LinkButton ID="lbtnDel" runat="server" OnClientClick="javascript:return checkDel('AdminLogID')" OnClick="btnDel_Click">ɾ������Ա��־</asp:LinkButton>
                    <a href="javascript:SearchDialog()">��ѯ����Ա��־</a>
                </td>
            </tr>
        </table>
        <!--����ѡ�����-->
        <!--�б�ʼ-->
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list" id="TbList">
            <tr class="table_list_title">
                <td style="width: 2%">
                    <input type="checkbox" name="checkbox" value="checkbox" onclick="javascript:checkAll(this,'AdminLogID')" />
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=AdminLogID&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="�������">
                    ���<%=GetData.GetOrderSign(strOrderKey, "AdminLogID",strAscDesc1)%>
                </td>
                <td style="width: 10%">
                    ��־����<%=GetData.GetOrderSign(strOrderKey, "LogContent",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=ScriptFile&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="�������">
                    �����ļ�<%=GetData.GetOrderSign(strOrderKey, "ScriptFile",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=IPAddress&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="�������">
                    IP��ַ<%=GetData.GetOrderSign(strOrderKey, "IPAddress",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=AdminID&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="�������">
                    ������<%=GetData.GetOrderSign(strOrderKey, "AdminID",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=AddTime&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="�������">
                    ����ʱ��<%=GetData.GetOrderSign(strOrderKey, "AddTime",strAscDesc1)%>
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
        <!--�б����-->
        <!--��ҳ��ʼ-->
        <div id="pager" runat="server">
        </div>
        <!--��ҳ����-->
    </div>
    </form>
</body>
</html>
