<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dictionary.aspx.cs" Inherits="HxSoft.Web.Admin._System.Dictionary" %>

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
                                �ֵ����ƣ�
                            </td>
                            <td>
                                <asp:TextBox ID="txtDictionaryName" runat="server"></asp:TextBox>
                            </td>
                            <td style="width: 15%" align="right">
                                ״̬��
                            </td>
                            <td>
                                <asp:RadioButtonList ID="radIsClose" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                    <asp:ListItem Selected="True" Value="-1">����</asp:ListItem>
                                    <asp:ListItem Value="0">����</asp:ListItem>
                                    <asp:ListItem Value="1">�ر�</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class="table_query_btn">
                            <td align="right" style="width: 15%">
                            </td>
                            <td>
                                <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="��ѯ" PostBackUrl="Dictionary.aspx" />
                                <input type="button" value="����" onclick="javascript:HiddenSearchDialog();" />
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
        <!--��ѯ������-->
        <!--������ʼ-->
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_nav">
            <tr>
                <td>
                    �ֵ����&gt;�����б�(
                    <asp:Label ID="lblNav" runat="server" Text="Label"></asp:Label>
                    )
                </td>
            </tr>
        </table>
        <!--��������-->
        <!--����ѡ�ʼ-->
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_op">
            <tr>
                <td>
                    ����ѡ�
                    <asp:LinkButton ID="lbtnAdd" runat="server" OnClientClick="javascript:return GoTo('Dictionary_Add.aspx')">����ֵ�</asp:LinkButton>
                    <asp:LinkButton ID="lbtnEdit" runat="server" OnClientClick="javascript:return checkEdit('DictionaryID')" OnClick="btnEdit_Click">�޸��ֵ�</asp:LinkButton>
                    <asp:LinkButton ID="lbtnMove" runat="server" OnClientClick="javascript:return checkOperate('DictionaryID')" OnClick="lbtnMove_Click">�ƶ��ֵ�</asp:LinkButton>
                    <asp:LinkButton ID="lbtnDel" runat="server" OnClientClick="javascript:return checkDel('DictionaryID')" OnClick="btnDel_Click">ɾ���ֵ�</asp:LinkButton>
                    <asp:LinkButton ID="lbtnOpen" runat="server" OnClientClick="javascript:return checkOperate('DictionaryID')" OnClick="btnOpen_Click">��������</asp:LinkButton>
                    <asp:LinkButton ID="lbtnClose" runat="server" OnClientClick="javascript:return checkOperate('DictionaryID')" OnClick="btnClose_Click">�����ر�</asp:LinkButton>
                    <a href="javascript:SearchDialog()">��ѯ�ֵ�</a>
                    <asp:LinkButton ID="lbtnGoBack" runat="server" OnClientClick="javascript:return GoTo('Dictionary.aspx')">�����ϼ�</asp:LinkButton>
                </td>
            </tr>
        </table>
        <!--����ѡ�����-->
        <!--�б�ʼ-->
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list" id="TbList">
            <tr class="table_list_title">
                <td style="width: 2%">
                    <input type="checkbox" name="checkbox" value="checkbox" onclick="javascript:checkAll(this,'DictionaryID')" />
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=DictionaryID&AscDesc=<%=strAscDesc2 %>&ParentID=<%=ParentID %>&<%=UrlPara %>page=<%=page%>')" title="�������">
                    ���<%=GetData.GetOrderSign(strOrderKey, "DictionaryID",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=DictionaryName&AscDesc=<%=strAscDesc2 %>&ParentID=<%=ParentID %>&<%=UrlPara %>page=<%=page%>')" title="�������">
                    �ֵ�����<%=GetData.GetOrderSign(strOrderKey, "DictionaryName", strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=DictionaryVal&AscDesc=<%=strAscDesc2 %>&ParentID=<%=ParentID %>&<%=UrlPara %>page=<%=page%>')" title="�������">
                    �ֵ�ֵ<%=GetData.GetOrderSign(strOrderKey, "DictionaryVal", strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=ChildNum&AscDesc=<%=strAscDesc2 %>&ParentID=<%=ParentID %>&<%=UrlPara %>page=<%=page%>')" title="�������">
                    �Ӽ���<%=GetData.GetOrderSign(strOrderKey, "ChildNum", strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=ListID&AscDesc=<%=strAscDesc2 %>&ParentID=<%=ParentID %>&<%=UrlPara %>page=<%=page%>')" title="�������">
                    �����<%=GetData.GetOrderSign(strOrderKey, "ListID", strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=AdminID&AscDesc=<%=strAscDesc2 %>&ParentID=<%=ParentID %>&<%=UrlPara %>page=<%=page%>')" title="�������">
                    ������<%=GetData.GetOrderSign(strOrderKey, "AdminID", strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=AddTime&AscDesc=<%=strAscDesc2 %>&ParentID=<%=ParentID %>&<%=UrlPara %>page=<%=page%>')" title="�������">
                    ����ʱ��<%=GetData.GetOrderSign(strOrderKey, "AddTime", strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=IsClose&AscDesc=<%=strAscDesc2 %>&ParentID=<%=ParentID %>&<%=UrlPara %>page=<%=page%>')" title="�������">
                    ״̬<%=GetData.GetOrderSign(strOrderKey, "IsClose", strAscDesc1)%>
                </td>
                <td style="width: 10%">
                    ����
                </td>
            </tr>
            <asp:Repeater ID="repList" runat="server">
                <ItemTemplate>
                    <tr class="<%#(Container.ItemIndex+1)%2==0?"table_list_row_alert":"table_list_row_normal" %>">
                        <td style="width: 2%">
                            <input name="DictionaryID" type="checkbox" id="DictionaryID" value="<%#Eval("DictionaryID") %>" onclick="javascript:TrColor2('DictionaryID',<%#Container.ItemIndex+1 %>)" />
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('DictionaryID',<%#Container.ItemIndex+1 %>)">
                            <%#Eval("DictionaryID")%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('DictionaryID',<%#Container.ItemIndex+1 %>)">
                            <a href="Dictionary_Add.aspx?DictionaryID=<%#Eval("DictionaryID") %>&ParentID=<%=ParentID %>&<%=UrlOrderPara + UrlPara %>page=<%=page %>">
                                <%#Eval("DictionaryName")%></a>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('DictionaryID',<%#Container.ItemIndex+1 %>)">
                            <%#Eval("DictionaryVal")%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('DictionaryID',<%#Container.ItemIndex+1 %>)">
                            <%#Eval("ChildNum")%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('DictionaryID',<%#Container.ItemIndex+1 %>)">
                            <%#Eval("ListID")%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('DictionaryID',<%#Container.ItemIndex+1 %>)">
                            <%#Factory.Admin().GetValueByField("AdminName",Eval("AdminID").ToString())%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('DictionaryID',<%#Container.ItemIndex+1 %>)">
                            <%#Eval("AddTime")%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('DictionaryID',<%#Container.ItemIndex+1 %>)">
                            <%#GetData.ShowCloseStatus(Eval("IsClose").ToString())%>
                        </td>
                        <td style="width: 10%">
                            <a href="Dictionary.aspx?ParentID=<%#Eval("DictionaryID") %>">�������ֵ�</a>
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
