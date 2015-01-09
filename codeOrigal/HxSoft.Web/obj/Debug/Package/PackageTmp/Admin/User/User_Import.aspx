<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="User_Import.aspx.cs" Inherits="HxSoft.Web.Admin.User.User_Import" %>

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
                dialog("上传文件", "iframe:../Upload/File_Upload_Dialog.aspx?ObjName=txtImportFile&FolderPath=<%=Config.FileUploadPath %>User/", "650px", "550px", "");
            });
            $("#btSelect1").click(function () {
                dialog("选择文件", "iframe:../Upload/File_Select_Dialog.aspx?ObjName=txtImportFile&FolderPath=<%=Config.FileUploadPath %>User/", "650px", "550px", "");
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
                    <asp:Label ID="lblTitle" runat="server">批量导入</asp:Label>
                    会员
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    选择文件：
                </td>
                <td>
                    <asp:TextBox ID="txtImportFile" runat="server" />
                    <input type="button" value="上传文件" id="btUpload1" />
                    <input type="button" value="选择文件" id="btSelect1" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtImportFile" Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    会员级别：
                </td>
                <td>
                    <asp:DropDownList ID="drpUserRankID" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="drpUserRankID" Display="Dynamic" ErrorMessage="请选择" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_btn">
                <td align="right">
                    &nbsp;
                </td>
                <td>
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="批量导入" />
                    <input type="button" name="Submit" value="返回" onclick="javascript:history.go(-1);" />
                    <asp:Label ID="errMsg" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr class="table_form_btn">
                <td align="right">
                    &nbsp;
                </td>
                <td>
                    表名必须为“sheet1”，第一行为“ 用户名、 密码 、姓名 、性别 、邮件地址 、联系电话 、联系地址 、公司名称 、积分 、备注”，相对应数据务必按此顺序填写，且每一列的数据类型必须相同，为数字就只能为数字。<br>
                    手机号码等数值在Excel中的设置<br />
                    选上要转换的列，点菜单栏上的“数据”--“分列”--选“分隔符号”（下一步）--下一步--“列数据格式”选“文本”--完成。即可实现转换。
                </td>
            </tr>
            <tr class="table_form_btn">
                <td align="right">
                    导入结果：
                </td>
                <td>
                    <asp:Label ID="errReslut" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
