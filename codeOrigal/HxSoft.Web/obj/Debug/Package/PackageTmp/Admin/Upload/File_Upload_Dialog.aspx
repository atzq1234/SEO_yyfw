<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="File_Upload_Dialog.aspx.cs" Inherits="HxSoft.Web.Admin.Upload.File_Upload_Dialog" %>

<%@ Register Assembly="Brettle.Web.NeatUpload" Namespace="Brettle.Web.NeatUpload" TagPrefix="Upload" %>
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
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_form">
            <tr class="table_form_row">
                <td align="right" style="width: 20%">
                    命名方式：
                </td>
                <td>
                    <asp:RadioButtonList ID="radNameType" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="0">原文件名</asp:ListItem>
                        <asp:ListItem Value="1">自动命名</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:HiddenField ID="hidFolderPath" runat="server" />
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 20%" align="right">
                    上传文件：
                </td>
                <td class="file">
                    <Upload:InputFile ID="InputFile1" runat="server" MaxLength="-1" Size="-1" />
                </td>
            </tr>
            <tr class="table_form_btn">
                <td align="right" style="width: 20%">
                </td>
                <td>
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="上传" />
                    <input type="button" value="取消" onclick="javascript:HiddenDialog();" />
                    <asp:Label ID="errMsg" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="center" style="height: 30px;">
                    <div id="UploadProgressBar1" style="display: none">
                        <Upload:ProgressBar ID="ProgressBar1" runat="server" Height="30px" Inline="True" Triggers="btnSave" Width="500px">
                        </Upload:ProgressBar>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="center" valign="middle">
                    <div id="divPreview">
                    </div>
                    <img id="ImgPreviewSize" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
