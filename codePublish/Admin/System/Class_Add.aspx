<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Class_Add.aspx.cs" Inherits="HxSoft.Web.Admin._System.Class_Add" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
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
            $("#btUpload1").click(function () {
                dialog("�ϴ��ļ�", "iframe:../Upload/File_Upload_Dialog.aspx?ObjName=txtClassPic&FolderPath=<%=Config.FileUploadPath %>Class/", "650px", "550px", "");
            });
            $("#btSelect1").click(function () {
                dialog("ѡ���ļ�", "iframe:../Upload/File_Select_Dialog.aspx?ObjName=txtClassPic&FolderPath=<%=Config.FileUploadPath %>Class/", "650px", "550px", "");
            });
            $("#btPreview1").click(function () {
                dialog("ͼƬԤ��", "iframe:../Upload/File_Preview.aspx?FilePath=" + $("#txtClassPic").val(), "500px", "300px", "");
            });

            $(".a_config").toggle(function () {
                $(".table_form_classconfig").show();
            }, function () {
                $(".table_form_classconfig").hide();
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_form">
            <tr class="table_form_title">
                <td colspan="2">
                    <asp:Label ID="lblTitle" runat="server">���</asp:Label>
                    ��Ŀ
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    ���԰汾��
                </td>
                <td>
                    &nbsp;
                    <asp:DropDownList ID="drpConfigID" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="drpConfigID" Display="Dynamic" ErrorMessage="��ѡ��" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td align="right" style="width: 15%">
                    ����������
                </td>
                <td>
                    <asp:Label ID="lblParent" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    ��Ŀ���ƣ�
                </td>
                <td>
                    <asp:TextBox ID="txtClassName" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtClassName" Display="Dynamic" ErrorMessage="����Ϊ��" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    �������ƣ�
                </td>
                <td>
                    <asp:TextBox ID="txtClassEnName" runat="server" /><%=Config.FileExt %>
                    <a href="javascript:void(0);" onclick="javascript:GetPinYin('txtClassName','txtClassEnName')">[��ȡ��Ŀ���Ƶ�ƴ��]</a>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtClassEnName" Display="Dynamic" ErrorMessage="����Ϊ��" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    ��Ŀ���ԣ�
                </td>
                <td>
                    <asp:DropDownList ID="drpClassPropertyID" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpClassPropertyID_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="drpClassPropertyID" Display="Dynamic" ErrorMessage="��ѡ��" SetFocusOnError="True" InitialValue="-1"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    ��Ŀģ�壺
                </td>
                <td>
                    <asp:DropDownList ID="drpClassTemplateID" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="drpClassTemplateID" Display="Dynamic" ErrorMessage="��ѡ��" SetFocusOnError="True" InitialValue="-1"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    ��ĿͼƬ��
                </td>
                <td>
                    <asp:TextBox ID="txtClassPic" runat="server" />
                    <input type="button" value="�ϴ�ͼƬ" id="btUpload1" />
                    <input type="button" value="ѡ��ͼƬ" id="btSelect1" />
                    <input type="button" value=" Ԥ�� " id="btPreview1" />
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    �Զ������ӣ�
                </td>
                <td>
                    <asp:TextBox ID="txtLinkUrl" runat="server" />
                    <asp:DropDownList ID="drpTarget" runat="server">
                        <asp:ListItem Value="_blank">�´���</asp:ListItem>
                        <asp:ListItem Value="_parent">������</asp:ListItem>
                        <asp:ListItem Selected="True" Value="_self">������</asp:ListItem>
                        <asp:ListItem Value="_top">��ҳ</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    ���ã�
                </td>
                <td>
                    <asp:CheckBox ID="chkIsGoToFirst" runat="server" Text="��������Ŀʱ,�Ƿ���ת����һ������Ŀ" Checked="true" />
                    <asp:CheckBox ID="chkIsShowNav" runat="server" Text="�Ƿ���ʾ��������" Checked="true" />
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    �ؼ��֣�
                </td>
                <td>
                    <asp:TextBox ID="txtKeywords" runat="server" />
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    ��Ŀ������
                </td>
                <td>
                    <asp:TextBox ID="txtDescription" runat="server" Columns="40" Rows="8" TextMode="MultiLine" />
                </td>
            </tr>
            <tr class="table_form_row" id="trSingle" runat="server">
                <td style="width: 15%" align="right">
                    ��ҳ�����ݣ�
                </td>
                <td>
                    <FCKeditorV2:FCKeditor ID="txtClassContent" runat="server" Height="400px" Width="95%">
                    </FCKeditorV2:FCKeditor>
                </td>
            </tr>
            <tr class="table_form_row" id="trSingle2" runat="server">
                <td style="width: 15%" align="right">
                </td>
                <td>
                    <input id="btnFormat1" type="button" value="�Զ��Ű�༭����" onclick="FormatText('txtClassContent___Frame')" />
                </td>
            </tr>
            <tr class="table_form_row">
                <td align="right">
                    &nbsp;
                </td>
                <td>
                    <a href="javascript:void(0);" class="a_config">��Ŀ��������(���б���Ч)</a>
                </td>
            </tr>
            <tr class="table_form_row table_form_classconfig">
                <td align="right">
                </td>
                <td>
                    <asp:CheckBox ID="chkIsUpdateSubClassConfig" runat="server" Text="ͬ�����µ�����Ŀ(��ͬ��Ŀ����)" />
                </td>
            </tr>
            <tr class="table_form_row table_form_classconfig">
                <td align="right">
                    ��ҳ��¼����
                </td>
                <td>
                    <asp:TextBox ID="txtPageSize" runat="server" Text="12"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtPageSize" Display="Dynamic" ErrorMessage="����Ϊ��" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtPageSize" Display="Dynamic" ErrorMessage="ֻ��Ϊ����" ValidationExpression="[0-9.]{1,}" SetFocusOnError="True"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr class="table_form_row table_form_classconfig">
                <td align="right">
                    ��ʾ��¼������
                </td>
                <td>
                    <asp:TextBox ID="txtTopNum" runat="server" Text="0"></asp:TextBox>ע��Ϊ0ʱ��ʾȫ����¼
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtTopNum" Display="Dynamic" ErrorMessage="����Ϊ��" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtTopNum" Display="Dynamic" ErrorMessage="ֻ��Ϊ����" ValidationExpression="[0-9.]{1,}" SetFocusOnError="True"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr class="table_form_row table_form_classconfig">
                <td align="right">
                </td>
                <td>
                    <asp:CheckBox ID="chkIsShowSub" runat="server" Text="�Ƿ���ʾ����Ŀ��¼" />
                    <asp:CheckBox ID="chkIsOnlyRecommend" runat="server" Text="�Ƿ�ֻ��ʾ�Ƽ���¼" />
                </td>
            </tr>
            <tr class="table_form_row table_form_classconfig">
                <td align="right">
                    ���򷽷���
                </td>
                <td>
                    <asp:DropDownList ID="drpOrderField" runat="server">
                        <asp:ListItem Selected="True" Value="AddTime">��ʱ��</asp:ListItem>
                        <asp:ListItem Value="ListID">�������</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="drpOrderKey" runat="server">
                        <asp:ListItem Selected="True" Value="desc">����</asp:ListItem>
                        <asp:ListItem Value="asc">˳��</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="table_form_row table_form_classconfig">
                <td align="right">
                    ����������
                </td>
                <td>
                    <asp:TextBox ID="txtTitleNum" runat="server" Text="0"></asp:TextBox>ע��Ϊ0ʱ��ʾȫ������
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtTitleNum" Display="Dynamic" ErrorMessage="����Ϊ��" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtTitleNum" Display="Dynamic" ErrorMessage="ֻ��Ϊ����" ValidationExpression="[0-9.]{1,}" SetFocusOnError="True"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr class="table_form_row table_form_classconfig">
                <td align="right">
                    ����������
                </td>
                <td>
                    <asp:TextBox ID="txtDescNum" runat="server" Text="0"></asp:TextBox>ע��Ϊ0ʱ��ʾȫ������
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtDescNum" Display="Dynamic" ErrorMessage="����Ϊ��" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtDescNum" Display="Dynamic" ErrorMessage="ֻ��Ϊ����" ValidationExpression="[0-9.]{1,}" SetFocusOnError="True"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr class="table_form_row table_form_classconfig">
                <td align="right">
                    ��ϸ�������ƣ�
                </td>
                <td>
                    <asp:TextBox ID="txtDataLink" runat="server" Text="article-details-"></asp:TextBox>$id<%=Config.FileExt %>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtDataLink" Display="Dynamic" ErrorMessage="����Ϊ��" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row table_form_classconfig">
                <td align="right">
                    �б���ʽ�����ƣ�
                </td>
                <td>
                    <asp:TextBox ID="txtStyleClass" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    ����ţ�
                </td>
                <td>
                    <asp:TextBox ID="txtListID" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtListID" Display="Dynamic" ErrorMessage="����Ϊ��" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtListID" Display="Dynamic" ErrorMessage="ֻ��Ϊ����" ValidationExpression="[0-9.]{1,}" SetFocusOnError="True"></asp:RegularExpressionValidator>
                    <asp:HiddenField ID="hidlistID" runat="server" />
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    �Ƿ�رգ�
                </td>
                <td>
                    <asp:RadioButtonList ID="radIsClose" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Value="1">��</asp:ListItem>
                        <asp:ListItem Selected="True" Value="0">��</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr class="table_form_btn">
                <td align="right">
                    <div id="hiddendiv" style="display: none;">
                    </div>
                </td>
                <td>
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="����" />
                    <input type="button" name="Submit" value="����" onclick="javascript:history.go(-1);" />
                    <asp:Label ID="errMsg" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
