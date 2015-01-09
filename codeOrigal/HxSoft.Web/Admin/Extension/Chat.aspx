<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Chat.aspx.cs" Inherits="HxSoft.Web.Admin.Extension.Chat" %>

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
                                �ʺ����ͣ�
                            </td>
                            <td>
                                <asp:DropDownList ID="drpTypeID" runat="server">
                                    <asp:ListItem Selected="True" Value="-1">����</asp:ListItem>
                                    <asp:ListItem Value="1">QQ</asp:ListItem>
                                    <asp:ListItem Value="2">MSN</asp:ListItem>
                                    <asp:ListItem Value="3">Skype</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 15%" align="right">
                                �ǳƣ�
                            </td>
                            <td>
                                <asp:TextBox ID="txtNickName" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="table_query_row">
                            <td style="width: 15%" align="right">
                                �ʺţ�
                            </td>
                            <td>
                                <asp:TextBox ID="txtAccount" runat="server"></asp:TextBox>
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
                                <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="��ѯ" PostBackUrl="Chat.aspx" />
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
                    �ʺŹ���&gt;�����б�
                </td>
            </tr>
        </table>
        <!--��������-->
        <!--����ѡ�ʼ-->
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_op">
            <tr>
                <td>
                    ����ѡ�
                    <asp:LinkButton ID="lbtnAdd" runat="server" OnClientClick="javascript:return GoTo('Chat_Add.aspx')">����ʺ�</asp:LinkButton>
                    <asp:LinkButton ID="lbtnEdit" runat="server" OnClientClick="javascript:return checkEdit('ChatID')" OnClick="btnEdit_Click">�޸��ʺ�</asp:LinkButton>
                    <asp:LinkButton ID="lbtnDel" runat="server" OnClientClick="javascript:return checkDel('ChatID')" OnClick="btnDel_Click">ɾ���ʺ�</asp:LinkButton>
                    <asp:LinkButton ID="lbtnOpen" runat="server" OnClientClick="javascript:return checkOperate('ChatID')" OnClick="btnOpen_Click">��������</asp:LinkButton>
                    <asp:LinkButton ID="lbtnClose" runat="server" OnClientClick="javascript:return checkOperate('ChatID')" OnClick="btnClose_Click">�����ر�</asp:LinkButton>
                    <a href="javascript:SearchDialog()">��ѯ�ʺ�</a>
                </td>
            </tr>
        </table>
        <!--����ѡ�����-->
        <!--�б�ʼ-->
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list" id="TbList">
            <tr class="table_list_title">
                <td style="width: 2%">
                    <input type="checkbox" name="checkbox" value="checkbox" onclick="javascript:checkAll(this,'ChatID')" />
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=ChatID&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="�������">
                    ���<%=GetData.GetOrderSign(strOrderKey, "ChatID",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=NickName&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="�������">
                    �ǳ�<%=GetData.GetOrderSign(strOrderKey, "NickName",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=Account&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="�������">
                    �ʺ�<%=GetData.GetOrderSign(strOrderKey, "Account",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=TypeID&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="�������">
                    ���԰汾<%=GetData.GetOrderSign(strOrderKey, "ConfigID",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=TypeID&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="�������">
                    �ʺ�����<%=GetData.GetOrderSign(strOrderKey, "TypeID",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=ListID&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="�������">
                    �����<%=GetData.GetOrderSign(strOrderKey, "ListID",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=AdminID&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="�������">
                    ������<%=GetData.GetOrderSign(strOrderKey, "AdminID",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=AddTime&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="�������">
                    ����ʱ��<%=GetData.GetOrderSign(strOrderKey, "AddTime",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=IsClose&AscDesc=<%=strAscDesc2 %>&<%=UrlPara %>page=<%=page%>')" title="�������">
                    ״̬<%=GetData.GetOrderSign(strOrderKey, "IsClose",strAscDesc1)%>
                </td>
            </tr>
            <asp:Repeater ID="repList" runat="server">
                <ItemTemplate>
                    <tr class="<%#(Container.ItemIndex+1)%2==0?"table_list_row_alert":"table_list_row_normal" %>">
                        <td style="width: 2%">
                            <input name="ChatID" type="checkbox" id="ChatID" value="<%#Eval("ChatID") %>" onclick="javascript:TrColor2('ChatID',<%#Container.ItemIndex+1 %>)" />
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('ChatID',<%#Container.ItemIndex+1 %>)">
                            <%#Eval("ChatID")%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('ChatID',<%#Container.ItemIndex+1 %>)">
                            <a href="Chat_Add.aspx?ChatID=<%#Eval("ChatID") %>&<%=UrlOrderPara + UrlPara %>page=<%=page %>">
                                <%#Eval("NickName")%></a>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('ChatID',<%#Container.ItemIndex+1 %>)">
                            <%#Eval("Account")%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('ChatID',<%#Container.ItemIndex+1 %>)">
                            <%#Factory.Config().GetValueByField("LanguageVer", Eval("ConfigID").ToString())%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('ChatID',<%#Container.ItemIndex+1 %>)">
                            <%#GetData.ShowChatType(Eval("TypeID").ToString())%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('ChatID',<%#Container.ItemIndex+1 %>)">
                            <%#Eval("ListID")%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('ChatID',<%#Container.ItemIndex+1 %>)">
                            <%#Factory.Admin().GetValueByField("AdminName",Eval("AdminID").ToString())%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('ChatID',<%#Container.ItemIndex+1 %>)">
                            <%#Eval("AddTime")%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('ChatID',<%#Container.ItemIndex+1 %>)">
                            <%#GetData.ShowCloseStatus(Eval("IsClose").ToString())%>
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
