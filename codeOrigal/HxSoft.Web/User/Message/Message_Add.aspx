<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="Message_Add.aspx.cs" Inherits="HxSoft.Web.User.Message.Message_Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        $(function () {
            $.formValidator.initConfig({ formID: "<%=form1.ClientID %>", submitonce: true, debug: false, submitAfterAjaxPrompt: "有数据正在异步验证，请稍等..." });

            $("#<%=drpDictionaryID.ClientID %>")
        .formValidator({ onShow: "", onFocus: "提示:请选择留言分类", onCorrect: "输入正确" })
        .inputValidator({ min: 1, onError: "请选择留言分类" });

            $("#<%=txtTitle.ClientID %>")
        .formValidator({ onShow: "", onFocus: "提示:请输入留言主题", onCorrect: "输入正确" })
        .inputValidator({ min: 1, onError: "留言主题不能为空" });

            $("#<%=txtMessageContent.ClientID %>")
        .formValidator({ onShow: "", onFocus: "提示:请输入留言内容", onCorrect: "输入正确" })
        .inputValidator({ min: 1, onError: "留言内容不能为空" });
        });
    </script>
    <script type="text/javascript">
        $f.dom.ready(function () {
            $f("<%=form1.ClientID %>", {
                rules: {
                    drpDictionaryID: { min: 1 },
                    txtTitle: { required: 1 },
                    txtMessageContent: { required: 1 }
                },
                messages: {
                    drpDictionaryID: { min: "请选择" },
                    txtTitle: { required: "不能为空" },
                    txtMessageContent: { required: "不能为空" }
                },
                ruleKey: "ruleKey",
                appendLabel: $f.appendLast,
                findLabel: $f.findLast
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_form">
        <tr class="table_form_row">
            <td style="width: 15%" align="right">
                留言分类：
            </td>
            <td>
                <asp:DropDownList ID="drpDictionaryID" runat="server" ruleKey="drpDictionaryID">
                </asp:DropDownList>
            </td>
        </tr>
        <tr class="table_form_row">
            <td style="width: 15%" align="right">
                主题：
            </td>
            <td>
                <asp:TextBox ID="txtTitle" runat="server" Width="200px" ruleKey="txtTitle"></asp:TextBox>
            </td>
        </tr>
        <tr class="table_form_row">
            <td style="width: 15%" align="right">
                留言内容:
            </td>
            <td>
                <asp:TextBox ID="txtMessageContent" runat="server" TextMode="MultiLine" Columns="40" Rows="8" ruleKey="txtMessageContent"></asp:TextBox>
            </td>
        </tr>
        <tr class="table_form_btn">
            <td style="width: 15%" align="right">
            </td>
            <td>
                <asp:Button ID="btnSave" runat="server" Text="提 交" CssClass="sub1" OnClick="btnSave_Click" />
                <input class="sub1" name="input" type="reset" value="重 置" /><asp:Label ID="errMsg" runat="server" ForeColor="red"></asp:Label>
            </td>
        </tr>
    </table>
    </form>
</asp:Content>
