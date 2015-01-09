<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Class.aspx.cs" Inherits="HxSoft.Web.Admin._System.Class" %>

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
                                ��Ŀ���ƣ�
                            </td>
                            <td>
                                <asp:TextBox ID="txtClassName" runat="server"></asp:TextBox>
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
                                <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="��ѯ" PostBackUrl="Class.aspx" />
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
                    ��Ŀ����&gt;�����б�(
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
                    <asp:LinkButton ID="lbtnAdd" runat="server" OnClientClick="javascript:return GoTo('Class_Add.aspx')">�����Ŀ</asp:LinkButton>
                    <asp:LinkButton ID="lbtnEdit" runat="server" OnClientClick="javascript:return checkEdit('ClassID')" OnClick="btnEdit_Click">�޸���Ŀ</asp:LinkButton>
                    <asp:LinkButton ID="lbtnMove" runat="server" OnClientClick="javascript:return checkOperate('ClassID')" OnClick="lbtnMove_Click">�ƶ���Ŀ</asp:LinkButton>
                    <asp:LinkButton ID="lbtnDel" runat="server" OnClientClick="javascript:return checkDel('ClassID')" OnClick="btnDel_Click">ɾ����Ŀ</asp:LinkButton>
                    <asp:LinkButton ID="lbtnOpen" runat="server" OnClientClick="javascript:return checkOperate('ClassID')" OnClick="btnOpen_Click">��������</asp:LinkButton>
                    <asp:LinkButton ID="lbtnClose" runat="server" OnClientClick="javascript:return checkOperate('ClassID')" OnClick="btnClose_Click">�����ر�</asp:LinkButton>
                    <a href="javascript:SearchDialog()">��ѯ��Ŀ</a>
                    <asp:LinkButton ID="lbtnGoBack" runat="server" OnClientClick="javascript:return GoTo('Class.aspx')">�����ϼ�</asp:LinkButton>
                </td>
            </tr>
        </table>
        <!--����ѡ�����-->
        <!--�б�ʼ-->
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list" id="TbList">
            <tr class="table_list_title">
                <td style="width: 2%">
                    <input type="checkbox" name="checkbox" value="checkbox" onclick="javascript:checkAll(this,'ClassID')" />
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=ClassID&AscDesc=<%=strAscDesc2 %>&ParentID=<%=ParentID %>&<%=UrlPara %>page=<%=page%>')" title="�������">
                    ���<%=GetData.GetOrderSign(strOrderKey, "ClassID",strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=ClassName&AscDesc=<%=strAscDesc2 %>&ParentID=<%=ParentID %>&<%=UrlPara %>page=<%=page%>')" title="�������">
                    ��Ŀ����<%=GetData.GetOrderSign(strOrderKey, "ClassName", strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=ConfigID&AscDesc=<%=strAscDesc2 %>&ParentID=<%=ParentID %>&<%=UrlPara %>page=<%=page%>')" title="�������">
                    ���԰汾<%=GetData.GetOrderSign(strOrderKey, "ConfigID", strAscDesc1)%>
                </td>
                <td style="width: 10%" onclick="javascript:GoTo('?OrderKey=ClassPropertyID&AscDesc=<%=strAscDesc2 %>&ParentID=<%=ParentID %>&<%=UrlPara %>page=<%=page%>')" title="�������">
                    ��Ŀ����<%=GetData.GetOrderSign(strOrderKey, "ClassPropertyID", strAscDesc1)%>
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
                            <input name="ClassID" type="checkbox" id="ClassID" value="<%#Eval("ClassID") %>" onclick="javascript:TrColor2('ClassID',<%#Container.ItemIndex+1 %>)" />
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('ClassID',<%#Container.ItemIndex+1 %>)">
                            <%#Eval("ClassID")%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('ClassID',<%#Container.ItemIndex+1 %>)">
                            <a href="Class_Add.aspx?ClassID=<%#Eval("ClassID") %>&ParentID=<%=ParentID %>&<%=UrlOrderPara + UrlPara %>page=<%=page %>">
                                <%#Eval("ClassName")%></a><%#GetData.ShowNavStatus(Eval("IsShowNav").ToString()) %>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('ClassID',<%#Container.ItemIndex+1 %>)">
                            <%#Factory.Config().GetValueByField("LanguageVer", Eval("ConfigID").ToString())%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('ClassID',<%#Container.ItemIndex+1 %>)">
                            <%#Factory.ClassProperty().GetValueByField("PropertyName", Eval("ClassPropertyID").ToString())%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('ClassID',<%#Container.ItemIndex+1 %>)">
                            <%#Eval("ChildNum")%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('ClassID',<%#Container.ItemIndex+1 %>)">
                            <%#Eval("ListID")%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('ClassID',<%#Container.ItemIndex+1 %>)">
                            <%#Factory.Admin().GetValueByField("AdminName",Eval("AdminID").ToString())%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('ClassID',<%#Container.ItemIndex+1 %>)">
                            <%#Eval("AddTime")%>
                        </td>
                        <td style="width: 10%" onclick="javascript:TrColor('ClassID',<%#Container.ItemIndex+1 %>)">
                            <%#GetData.ShowCloseStatus(Eval("IsClose").ToString())%>
                        </td>
                        <td style="width: 10%">
                            <a href="Class.aspx?ParentID=<%#Eval("ClassID") %>">��������Ŀ</a>
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
