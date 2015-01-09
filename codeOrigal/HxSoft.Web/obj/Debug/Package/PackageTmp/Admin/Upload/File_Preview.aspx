<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="File_Preview.aspx.cs" Inherits="HxSoft.Web.Admin.Upload.File_Preview" %>

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
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td align="center" valign="middle" style="height: 370px; cursor: pointer;">
                    <img src="<%=strFilePath %>" id="ImgPreview" onload="javascript:DrawImage('ImgPreview', 450, 350)" onclick="javascript:HiddenDialog();" alt="点击关闭" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
