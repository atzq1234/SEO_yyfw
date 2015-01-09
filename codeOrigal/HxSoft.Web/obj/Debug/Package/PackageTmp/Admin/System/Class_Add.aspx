<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Class_Add.aspx.cs" Inherits="HxSoft.Web.Admin._System.Class_Add" %>

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
                dialog("上传文件", "iframe:../Upload/File_Upload_Dialog.aspx?ObjName=txtClassPic&FolderPath=<%=Config.FileUploadPath %>Class/", "650px", "550px", "");
            });
            $("#btSelect1").click(function () {
                dialog("选择文件", "iframe:../Upload/File_Select_Dialog.aspx?ObjName=txtClassPic&FolderPath=<%=Config.FileUploadPath %>Class/", "650px", "550px", "");
            });
            $("#btPreview1").click(function () {
                dialog("图片预览", "iframe:../Upload/File_Preview.aspx?FilePath=" + $("#txtClassPic").val(), "500px", "300px", "");
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
                    <asp:Label ID="lblTitle" runat="server">添加</asp:Label>
                    栏目
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    语言版本：
                </td>
                <td>
                    &nbsp;
                    <asp:DropDownList ID="drpConfigID" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="drpConfigID" Display="Dynamic" ErrorMessage="请选择" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td align="right" style="width: 15%">
                    所属父级：
                </td>
                <td>
                    <asp:Label ID="lblParent" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    栏目名称：
                </td>
                <td>
                    <asp:TextBox ID="txtClassName" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtClassName" Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    链接名称：
                </td>
                <td>
                    <asp:TextBox ID="txtClassEnName" runat="server" /><%=Config.FileExt %>
                    <a href="javascript:void(0);" onclick="javascript:GetPinYin('txtClassName','txtClassEnName')">[获取栏目名称的拼音]</a>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtClassEnName" Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    栏目属性：
                </td>
                <td>
                    <asp:DropDownList ID="drpClassPropertyID" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpClassPropertyID_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="drpClassPropertyID" Display="Dynamic" ErrorMessage="请选择" SetFocusOnError="True" InitialValue="-1"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    栏目模板：
                </td>
                <td>
                    <asp:DropDownList ID="drpClassTemplateID" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="drpClassTemplateID" Display="Dynamic" ErrorMessage="请选择" SetFocusOnError="True" InitialValue="-1"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    栏目图片：
                </td>
                <td>
                    <asp:TextBox ID="txtClassPic" runat="server" />
                    <input type="button" value="上传图片" id="btUpload1" />
                    <input type="button" value="选择图片" id="btSelect1" />
                    <input type="button" value=" 预览 " id="btPreview1" />
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    自定义链接：
                </td>
                <td>
                    <asp:TextBox ID="txtLinkUrl" runat="server" />
                    <asp:DropDownList ID="drpTarget" runat="server">
                        <asp:ListItem Value="_blank">新窗口</asp:ListItem>
                        <asp:ListItem Value="_parent">父窗口</asp:ListItem>
                        <asp:ListItem Selected="True" Value="_self">本窗口</asp:ListItem>
                        <asp:ListItem Value="_top">整页</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    设置：
                </td>
                <td>
                    <asp:CheckBox ID="chkIsGoToFirst" runat="server" Text="当有子栏目时,是否跳转到第一个子栏目" Checked="true" />
                    <asp:CheckBox ID="chkIsShowNav" runat="server" Text="是否显示在主导航" Checked="true" />
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    关键字：
                </td>
                <td>
                    <asp:TextBox ID="txtKeywords" runat="server" />
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    栏目描述：
                </td>
                <td>
                    <asp:TextBox ID="txtDescription" runat="server" Columns="40" Rows="8" TextMode="MultiLine" />
                </td>
            </tr>
            <tr class="table_form_row" id="trSingle" runat="server">
                <td style="width: 15%" align="right">
                    单页面内容：
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
                    <input id="btnFormat1" type="button" value="自动排版编辑内容" onclick="FormatText('txtClassContent___Frame')" />
                </td>
            </tr>
            <tr class="table_form_row">
                <td align="right">
                    &nbsp;
                </td>
                <td>
                    <a href="javascript:void(0);" class="a_config">栏目参数设置(对列表有效)</a>
                </td>
            </tr>
            <tr class="table_form_row table_form_classconfig">
                <td align="right">
                </td>
                <td>
                    <asp:CheckBox ID="chkIsUpdateSubClassConfig" runat="server" Text="同步更新到子栏目(相同栏目属性)" />
                </td>
            </tr>
            <tr class="table_form_row table_form_classconfig">
                <td align="right">
                    分页记录数：
                </td>
                <td>
                    <asp:TextBox ID="txtPageSize" runat="server" Text="12"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtPageSize" Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtPageSize" Display="Dynamic" ErrorMessage="只能为数字" ValidationExpression="[0-9.]{1,}" SetFocusOnError="True"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr class="table_form_row table_form_classconfig">
                <td align="right">
                    显示记录总数：
                </td>
                <td>
                    <asp:TextBox ID="txtTopNum" runat="server" Text="0"></asp:TextBox>注：为0时显示全部记录
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtTopNum" Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtTopNum" Display="Dynamic" ErrorMessage="只能为数字" ValidationExpression="[0-9.]{1,}" SetFocusOnError="True"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr class="table_form_row table_form_classconfig">
                <td align="right">
                </td>
                <td>
                    <asp:CheckBox ID="chkIsShowSub" runat="server" Text="是否显示子栏目记录" />
                    <asp:CheckBox ID="chkIsOnlyRecommend" runat="server" Text="是否只显示推荐记录" />
                </td>
            </tr>
            <tr class="table_form_row table_form_classconfig">
                <td align="right">
                    排序方法：
                </td>
                <td>
                    <asp:DropDownList ID="drpOrderField" runat="server">
                        <asp:ListItem Selected="True" Value="AddTime">按时间</asp:ListItem>
                        <asp:ListItem Value="ListID">按排序号</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="drpOrderKey" runat="server">
                        <asp:ListItem Selected="True" Value="desc">倒序</asp:ListItem>
                        <asp:ListItem Value="asc">顺序</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="table_form_row table_form_classconfig">
                <td align="right">
                    标题字数：
                </td>
                <td>
                    <asp:TextBox ID="txtTitleNum" runat="server" Text="0"></asp:TextBox>注：为0时显示全部标题
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtTitleNum" Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtTitleNum" Display="Dynamic" ErrorMessage="只能为数字" ValidationExpression="[0-9.]{1,}" SetFocusOnError="True"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr class="table_form_row table_form_classconfig">
                <td align="right">
                    描述字数：
                </td>
                <td>
                    <asp:TextBox ID="txtDescNum" runat="server" Text="0"></asp:TextBox>注：为0时显示全部内容
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtDescNum" Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtDescNum" Display="Dynamic" ErrorMessage="只能为数字" ValidationExpression="[0-9.]{1,}" SetFocusOnError="True"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr class="table_form_row table_form_classconfig">
                <td align="right">
                    详细链接名称：
                </td>
                <td>
                    <asp:TextBox ID="txtDataLink" runat="server" Text="article-details-"></asp:TextBox>$id<%=Config.FileExt %>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtDataLink" Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="table_form_row table_form_classconfig">
                <td align="right">
                    列表样式类名称：
                </td>
                <td>
                    <asp:TextBox ID="txtStyleClass" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr class="table_form_row">
                <td style="width: 15%" align="right">
                    排序号：
                </td>
                <td>
                    <asp:TextBox ID="txtListID" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtListID" Display="Dynamic" ErrorMessage="不能为空" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtListID" Display="Dynamic" ErrorMessage="只能为数字" ValidationExpression="[0-9.]{1,}" SetFocusOnError="True"></asp:RegularExpressionValidator>
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
                    <div id="hiddendiv" style="display: none;">
                    </div>
                </td>
                <td>
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
