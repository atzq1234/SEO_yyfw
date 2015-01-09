<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Article_Add.aspx.cs" Inherits="HxSoft.Web.Admin.Article.Article_Add" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
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
                dialog("上传文件", "iframe:../Upload/File_Upload_Dialog.aspx?ObjName=txtPicture&FolderPath=<%=Config.FileUploadPath %>Article/", "650px", "550px", "");
            });
            $("#btSelect1").click(function () {
                dialog("选择文件", "iframe:../Upload/File_Select_Dialog.aspx?ObjName=txtPicture&FolderPath=<%=Config.FileUploadPath %>Article/", "650px", "550px", "");
            });
            $("#btPreview1").click(function () {
                dialog("图片预览", "iframe:../Upload/File_Preview.aspx?FilePath=" + $("#txtPicture").val(), "500px", "300px", "");
            });

            $("#btUpload2").click(function () {
                dialog("上传文件", "iframe:../Upload/File_Upload_Dialog.aspx?ObjName=txtVideo&FolderPath=<%=Config.FileUploadPath %>Article/", "650px", "550px", "");
            });
            $("#btSelect2").click(function () {
                dialog("选择文件", "iframe:../Upload/File_Select_Dialog.aspx?ObjName=txtVideo&FolderPath=<%=Config.FileUploadPath %>Article/", "650px", "550px", "");
            });
            $("#btPreview2").click(function () {
                dialog("图片预览", "iframe:../Upload/File_Preview.aspx?FilePath=" + $("#txtVideo").val(), "500px", "300px", "");
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
                    文章
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    语言版本：
                </td>
                <td>
                    <asp:DropDownList ID="drpConfigID" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpConfigID_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="drpConfigID"
                        Display="Dynamic" ErrorMessage="请选择" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    栏目分类：
                </td>
                <td>
                    <asp:DropDownList ID="drpClassID" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpClassID"
                        Display="Dynamic" ErrorMessage="请选择" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    标题：
                </td>
                <td>
                    <asp:TextBox ID="txtTitle" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTitle"
                        Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    作者：
                </td>
                <td>
                    <asp:TextBox ID="txtAuthor" runat="server" />
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    来源：
                </td>
                <td>
                    <asp:TextBox ID="txtComeFrom" runat="server" />
                </td>
            </tr>
            <tr class="table_form_row" id="IsThumb" runat="server">
                <td style="width: 15%" align="right">
                    缩略图：
                </td>
                <td>
                    <asp:TextBox ID="txtPicture" runat="server" />
                    <input type="button" value="上传图片" id="btUpload1" />
                    <input type="button" value="选择图片" id="btSelect1" />
                    <input type="button" value=" 预览 " id="btPreview1" />
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    大图：
                </td>
                <td>
                    <asp:TextBox ID="txtVideo" runat="server" />
                    <input type="button" value="上传图片" id="btUpload2" />
                    <input type="button" value="选择图片" id="btSelect2" />
                    <input type="button" value=" 预览 " id="btPreview2" />
                    <asp:Label ID="Err" runat="server" Text="此路径下的图片已不存在,请重新上传" Font-Bold="true" ForeColor="Red"
                        Visible="false"></asp:Label>
                </td>
            </tr>
            <tr class="table_form_row" style="display: none">
                <td style="width: 15%" align="right">
                    标签：
                </td>
                <td>
                    <asp:TextBox ID="txtTags" runat="server" />多个标签请用","分隔
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    关键字：
                </td>
                <td>
                    <asp:TextBox ID="txtKeywords" runat="server" />多个关键字请用","分隔
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    简单描述：
                </td>
                <td>
                    <asp:TextBox ID="txtDescription" runat="server" Columns="40" Rows="8" TextMode="MultiLine" />
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    详细内容：
                </td>
                <td>
                    <FCKeditorV2:FCKeditor ID="txtDetails" runat="server" Height="400px" Width="95%">
                    </FCKeditorV2:FCKeditor>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                </td>
                <td>
                    <input id="btnFormat1" type="button" value="自动排版编辑内容" onclick="FormatText('txtDetails___Frame')" />
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    设置：
                </td>
                <td>
                    <asp:CheckBox ID="chkIsRecommend" runat="server" Text="推荐" />
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    访问数：
                </td>
                <td>
                    <asp:TextBox ID="txtClickNum" runat="server" CssClass="input_shadow">0</asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtClickNum"
                        Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtClickNum"
                        Display="Dynamic" ErrorMessage="只能为数字" ValidationExpression="[0-9]{1,}" SetFocusOnError="True"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    排序号：
                </td>
                <td>
                    <asp:TextBox ID="txtListID" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtListID"
                        Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtListID"
                        Display="Dynamic" ErrorMessage="只能为数字" ValidationExpression="[0-9.]{1,}" SetFocusOnError="True"></asp:RegularExpressionValidator>
                    <asp:HiddenField ID="hidlistID" runat="server" />
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    是否关闭：
                </td>
                <td>
                    <asp:RadioButtonList ID="radIsClose" runat="server" RepeatDirection="Horizontal"
                        RepeatLayout="Flow">
                        <asp:ListItem Value="1">是</asp:ListItem>
                        <asp:ListItem Selected="True" Value="0">否</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    日期：
                </td>
                <td>
                    <asp:DropDownList ID="drpYear" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="drpMonth" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="drpDay" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="drpHour" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="drpMinute" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="drpSecond" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="table_form_btn">
                <td align="right">
                    <div id="hiddendiv" style="display: none;">
                    </div>
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
