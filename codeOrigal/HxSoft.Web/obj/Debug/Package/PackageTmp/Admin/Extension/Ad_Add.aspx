<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ad_Add.aspx.cs" Inherits="HxSoft.Web.Admin.Extension.Ad_Add" %>

<%@ Import Namespace="HxSoft.Common" %>
<%@ Import Namespace="HxSoft.ClassFactory" %>
<%@ Register Src="../Admin.Config.ascx" TagName="Config" TagPrefix="Admin" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <Admin:Config ID="Admin1" runat="server" />
    <script language="javascript" type="text/javascript">
        $(function () {
            $("#btUpload1").click(function () {
                dialog("上传文件", "iframe:../Upload/File_Upload_Dialog.aspx?ObjName=txtAdSmallPic&FolderPath=<%=Config.FileUploadPath %>Ad/", "650px", "550px", "");
            });
            $("#btSelect1").click(function () {
                dialog("选择文件", "iframe:../Upload/File_Select_Dialog.aspx?ObjName=txtAdSmallPic&FolderPath=<%=Config.FileUploadPath %>Ad/", "650px", "550px", "");
            });
            $("#btPreview1").click(function () {
                dialog("图片预览", "iframe:../Upload/File_Preview.aspx?FilePath=" + $("#txtAdSmallPic").val(), "500px", "300px", "");
            });

            $("#btUpload2").click(function () {
                dialog("上传文件", "iframe:../Upload/File_Upload_Dialog.aspx?ObjName=txtAdPath&FolderPath=<%=Config.FileUploadPath %>Ad/", "650px", "550px", "");
            });
            $("#btSelect2").click(function () {
                dialog("选择文件", "iframe:../Upload/File_Select_Dialog.aspx?ObjName=txtAdPath&FolderPath=<%=Config.FileUploadPath %>Ad/", "650px", "550px", "");
            });
            $("#btPreview2").click(function () {
                dialog("图片预览", "iframe:../Upload/File_Preview.aspx?FilePath=" + $("#txtAdPath").val(), "500px", "300px", "");
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_form">
            <tr class="table_form_title">
                <td colspan="4">
                    <asp:Label ID="lblTitle" runat="server">添加</asp:Label>
                    广告
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    广告位：
                </td>
                <td>
                    &nbsp;
                    <asp:DropDownList ID="drpAdPositionID" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="drpAdPositionID" Display="Dynamic" ErrorMessage="请选择" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    广告名称：
                </td>
                <td>
                    <asp:TextBox ID="txtAdName" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAdName" Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    广告介绍：
                </td>
                <td>
                    <asp:TextBox ID="txtAdIntro" runat="server" Columns="40" Rows="8" TextMode="MultiLine" />
                    &nbsp;
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    广告小图：
                </td>
                <td>
                    <asp:TextBox ID="txtAdSmallPic" runat="server" />
                    <input type="button" value="上传文件" id="btUpload1" />
                    <input type="button" value="选择文件" id="btSelect1" />
                    <input type="button" value=" 预览 " id="btPreview1" />
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    广告文件地址：
                </td>
                <td>
                    <asp:TextBox ID="txtAdPath" runat="server" />
                    <input type="button" value="上传文件" id="btUpload2" />
                    <input type="button" value="选择文件" id="btSelect2" />
                    <input type="button" value=" 预览 " id="btPreview2" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtAdPath" Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    链接地址：
                </td>
                <td>
                    <asp:TextBox ID="txtAdLink" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtAdLink" Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    访问数：
                </td>
                <td>
                    <asp:TextBox ID="txtClickNum" runat="server">0</asp:TextBox>
                    &nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtClickNum" Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtClickNum" Display="Dynamic" ErrorMessage="只能为数字" ValidationExpression="[0-9]{1,}" SetFocusOnError="True"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    排序号：
                </td>
                <td>
                    <asp:TextBox ID="txtListID" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtListID" Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtListID" Display="Dynamic" ErrorMessage="只能为数字" ValidationExpression="[0-9.]{1,}" SetFocusOnError="True"></asp:RegularExpressionValidator>
                    <asp:HiddenField ID="hidlistID" runat="server" />
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    是否关闭：
                </td>
                <td>
                    <asp:RadioButtonList ID="radIsClose" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Value="1">是</asp:ListItem>
                        <asp:ListItem Selected="True" Value="0">否</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr class="table_form_btn">
                <td align="right">
                    &nbsp;
                </td>
                <td colspan="3">
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保存" />
                    <input type="button" name="Submit" value="返回" onclick="javascript:history.go(-1);" />
                    <asp:Label ID="errMsg" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
