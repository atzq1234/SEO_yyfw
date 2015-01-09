<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="File_Select_Dialog.aspx.cs" Inherits="HxSoft.Web.Admin.Upload.File_Select_Dialog" %>

<%@ Import Namespace="HxSoft.Common" %>
<%@ Import Namespace="HxSoft.ClassFactory" %>
<%@ Register Src="../Admin.Config.ascx" TagName="Config" TagPrefix="Admin" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>�ޱ���ҳ</title>
    <Admin:Config ID="Admin1" runat="server" />
    <script language="javascript" type="text/javascript">
        $(function () {
            $("#lbtnCreateFolder").click(function () {
                dialog("�����ļ���", "iframe:Folder_Create.aspx?FolderPath=<%=strFolderPath %>", "350px", "120px", "");
                return false;
            });
            $("#lbtnUploadFile").click(function () {
                dialog("�ϴ��ļ�", "iframe:File_Upload_Dialog.aspx?DialogType=SelectDialog&FolderPath=<%=strFolderPath %>&W=450&H=250", "550px", "450px", "");
                return false;
            });
            $("#lbtnBatchUploadFile").click(function () {

                dialog("�����ϴ��ļ�", "iframe:File_BatchUpload_Dialog.aspx?DialogType=SelectDialog&FolderPath=<%=strFolderPath %>&W=500&H=300", "550px", "450px", "");
                return false;
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
        <!--������ʼ-->
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_file_form">
            <tr>
                <td style="width: 10%;">
                    �ļ�·����
                </td>
                <td align="left" style="width: 40%;">
                    <asp:DropDownList ID="drpFolderPath" runat="server" Width="90%">
                    </asp:DropDownList>
                </td>
                <td style="width: 10%;">
                    �����ļ���
                </td>
                <td align="left" style="width: 40%;">
                    <asp:TextBox ID="txtFileNameKey" runat="server"></asp:TextBox>
                    <asp:HiddenField ID="hidIsSearch" runat="server" Value="1"/>
                    <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="��ѯ"/>
                </td>
            </tr>
        </table>
        <!--��������-->
        <!--����ѡ�ʼ-->
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_op">
            <tr>
                <td>
                    ����ѡ�
                    <asp:LinkButton ID="lbtnCreateFolder" runat="server">�����ļ���</asp:LinkButton>
                    <asp:LinkButton ID="lbtnDelFolder" runat="server" OnClientClick="javascript:return checkDel('FolderName')" OnClick="lbtnDelFolder_Click">ɾ����ѡ�ļ���</asp:LinkButton>
                    <asp:LinkButton ID="lbtnUploadFile" runat="server">�ϴ��ļ�</asp:LinkButton>
                    <asp:LinkButton ID="lbtnDelFile" runat="server" OnClientClick="javascript:return checkDel('FileName')" OnClick="lbtnDelFile_Click">ɾ����ѡ�ļ�</asp:LinkButton>
                    <asp:LinkButton ID="lbtnBatchUploadFile" runat="server">�����ϴ��ļ�</asp:LinkButton>
                </td>
            </tr>
        </table>
        <!--����ѡ�����-->
        <!--�б�ʼ-->
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_list" id="TbList">
            <tr class="table_list_title">
                <td style="width: 2%">
                    <input type="checkbox" name="checkbox" value="checkbox" onclick="javascript:checkAll2(this,'FolderName');checkAll2(this,'FileName')" />
                </td>
                <td style="width: 5%" nowrap>
                    ����
                </td>
                <td style="width: 10%" nowrap>
                    ����<span class="red">(����ļ���ѡ��)</span>
                </td>
                <td style="width: 10%" nowrap>
                    ����/��С
                </td>
                <td style="width: 10%" nowrap>
                    ������
                </td>
                <td style="width: 10%" nowrap>
                    ����ʱ��
                </td>
            </tr>
            <asp:Repeater ID="repList" runat="server">
                <ItemTemplate>
                    <tr class="<%#(Container.ItemIndex+1)%2==0?"table_list_row_alert":"table_list_row_normal" %>">
                        <td style="width: 2%">
                            <%#Eval("CheckBox")%>
                        </td>
                        <td style="width: 5%" nowrap>
                            <img src="../Admin_Themes/FileImages/<%#Eval("Type")%>.gif" width="16" height="16" alt="<%#Eval("Type")%>�ļ�" />
                        </td>
                        <td style="width: 10%" nowrap align="left">
                            <a href="<%#Eval("Link") %>" title="���ѡ��">
                                <%#Eval("Name")%>
                            </a>
                            <%#ShowPreview(Eval("Name").ToString()) %>
                        </td>
                        <td style="width: 10%" nowrap>
                            <%#Eval("CountOrSize")%>
                        </td>
                        <td style="width: 10%" nowrap>
                            <%#Eval("Author")%>
                        </td>
                        <td style="width: 10%" nowrap>
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
