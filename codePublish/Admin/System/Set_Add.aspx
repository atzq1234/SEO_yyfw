<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Set_Add.aspx.cs" Inherits="HxSoft.Web.Admin._System.Set_Add" %>

<%@ Import Namespace="HxSoft.Common" %>
<%@ Import Namespace="HxSoft.ClassFactory" %>
<%@ Register Src="../Admin.Config.ascx" TagName="Config" TagPrefix="Admin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <Admin:Config ID="Admin1" runat="server" />
    <script language="javascript" type="text/javascript">
        $(function () {
            $("#btUpload1").click(function () {
                dialog("上传图片", "iframe:../Upload/File_Upload_Dialog.aspx?ObjName=txtWaterPic&FolderPath=<%=Config.FileUploadPath %>WaterPic/", "650px", "550px", "");
            });
            $("#btSelect1").click(function () {
                dialog("选择图片", "iframe:../Upload/File_Select_Dialog.aspx?ObjName=txtWaterPic&FolderPath=<%=Config.FileUploadPath %>WaterPic/", "650px", "550px", "");
            });
            $("#btPreview1").click(function () {
                dialog("图片预览", "iframe:../Upload/File_Preview.aspx?FilePath=" + $("#txtWaterPic").val(), "500px", "300px", "");
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
                    系统配置
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    <b>水印设置</b>
                </td>
                <td>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    水印状态：
                </td>
                <td>
                    <asp:RadioButtonList ID="radWaterState" runat="server" RepeatDirection="Horizontal"
                        RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="0">不生成水印</asp:ListItem>
                        <asp:ListItem Value="1">生成文字水印</asp:ListItem>
                        <asp:ListItem Value="2">生成图片水印</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    水印文字：
                </td>
                <td>
                    <asp:TextBox ID="txtWaterText" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    字体样式：
                </td>
                <td>
                    <asp:DropDownList ID="drFontStyle" runat="server">
                        <asp:ListItem Value="Regular" Selected="True">普通</asp:ListItem>
                        <asp:ListItem Value="Bold">加粗</asp:ListItem>
                        <asp:ListItem Value="Italic">倾斜</asp:ListItem>
                        <asp:ListItem Value="Strikeout">中间有直线通过</asp:ListItem>
                        <asp:ListItem Value="Underline">带下划线</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    字体大小：
                </td>
                <td>
                    <asp:DropDownList ID="drFontSize" runat="server">
                        <asp:ListItem Value="12" Selected="True">12</asp:ListItem>
                        <asp:ListItem Value="14">14</asp:ListItem>
                        <asp:ListItem Value="16">16</asp:ListItem>
                        <asp:ListItem Value="18">18</asp:ListItem>
                        <asp:ListItem Value="20">20</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    字体颜色：
                </td>
                <td>
                    <asp:DropDownList ID="drFontColor" runat="server">
                        <asp:ListItem Value="White" Selected="True">白色</asp:ListItem>
                        <asp:ListItem Value="Red">红色</asp:ListItem>
                        <asp:ListItem Value="Green">绿色</asp:ListItem>
                        <asp:ListItem Value="Blue">蓝色</asp:ListItem>
                        <asp:ListItem Value="Gray">灰色</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    水印图片：
                </td>
                <td>
                    <asp:TextBox ID="txtWaterPic" runat="server"></asp:TextBox>
                    <input type="button" value="上传图片" id="btUpload1" />
                    <input type="button" value="选择图片" id="btSelect1" />
                    <input type="button" value=" 预览 " id="btPreview1" />
                    <asp:Label runat="server" ForeColor="Red" Font-Bold="true" Text="提示:请管理员根据实际情况上传水印图片(水印图片的规格不得大于原始图片的规格)"></asp:Label>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    图片位置：
                </td>
                <td>
                    <asp:DropDownList ID="drPicPosition" runat="server">
                        <asp:ListItem Value="RightB" Selected="True">右下</asp:ListItem>
                        <asp:ListItem Value="RightT">右上</asp:ListItem>
                        <asp:ListItem Value="LeftB">左下</asp:ListItem>
                        <asp:ListItem Value="LeftT">左上</asp:ListItem>
                        <asp:ListItem Value="Center">居中</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
             <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    <b>缩略图设置</b>
                </td>
                <td>
                </td>
            </tr>
           <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    文章缩略图：
                </td>
                <td>
                    <asp:RadioButtonList ID="radArticleThumbState" runat="server" RepeatDirection="Horizontal"
                        RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="0">关闭</asp:ListItem>
                        <asp:ListItem Value="1">开启</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    宽度：
                </td>
                <td>
                    <asp:TextBox ID="txtArticleThumbWidth" Text="100" runat="server" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtArticleThumbWidth" Display="Dynamic" ErrorMessage="只能为数字" ValidationExpression="[0-9]{1,}" SetFocusOnError="True"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    高度：
                </td>
                <td>
                    <asp:TextBox ID="txtArticleThumbHeight" Text="100" runat="server" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtArticleThumbHeight" Display="Dynamic" ErrorMessage="只能为数字" ValidationExpression="[0-9]{1,}" SetFocusOnError="True"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    产品缩略图：
                </td>
                <td>
                    <asp:RadioButtonList ID="radProductThumbState" runat="server" RepeatDirection="Horizontal"
                        RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="0">关闭</asp:ListItem>
                        <asp:ListItem Value="1">开启</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    宽度：
                </td>
                <td>
                    <asp:TextBox ID="txtProductThumbWidth" Text="100" runat="server" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtProductThumbWidth" Display="Dynamic" ErrorMessage="只能为数字" ValidationExpression="[0-9]{1,}" SetFocusOnError="True"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    高度：
                </td>
                <td>
                    <asp:TextBox ID="txtProductThumbHeight" Text="100" runat="server" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtProductThumbHeight" Display="Dynamic" ErrorMessage="只能为数字" ValidationExpression="[0-9]{1,}" SetFocusOnError="True"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    相册缩略图：
                </td>
                <td>
                    <asp:RadioButtonList ID="radPhotoThumbState" runat="server" RepeatDirection="Horizontal"
                        RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="0">关闭</asp:ListItem>
                        <asp:ListItem Value="1">开启</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    宽度：
                </td>
                <td>
                    <asp:TextBox ID="txtPhotoThumbWidth" Text="100" runat="server" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtPhotoThumbWidth" Display="Dynamic" ErrorMessage="只能为数字" ValidationExpression="[0-9]{1,}" SetFocusOnError="True"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    高度：
                </td>
                <td>
                    <asp:TextBox ID="txtPhotoThumbHeight" Text="100" runat="server" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtPhotoThumbHeight" Display="Dynamic" ErrorMessage="只能为数字" ValidationExpression="[0-9]{1,}" SetFocusOnError="True"></asp:RegularExpressionValidator>
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
