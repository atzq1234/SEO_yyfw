<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUC_Survey_Details.ascx.cs" Inherits="HxSoft.Web.cn.UserControl.WUC_Survey_Details" %>
<%@ Import Namespace="HxSoft.ClassFactory" %>
<script type="text/javascript">
    $f.dom.ready(function () {
        $f("<%=form1.ClientID %>", {
            rules: {
                inputKey: { required: 1 },
                textareaKey: { required: 1 }
            },
            messages: {
                inputKey: { required: "请选择" },
                textareaKey: { required: "不能为空" }
            },
            ruleKey: "ruleKey",
            appendLabel: $f.appendLast,
            findLabel: $f.findLast
        });
    });
</script>
<div class="videoTop">
<h1 class="tit">
    <asp:Literal ID="litClassName" runat="server"></asp:Literal></h1></div>
<div class="survey_details">
    <h2 class="sub">
        <asp:Literal ID="litSubject" runat="server"></asp:Literal></h2>
    <div class="intr">
        <asp:Literal ID="litIntrContent" runat="server"></asp:Literal></div>
    <form id="form1" runat="server">
    <asp:Repeater ID="repSurveyItem" runat="server">
        <ItemTemplate>
            <dl>
                <dt>
                    <%#Container.ItemIndex+1%>.
                    <%#Eval("ItemName")%></dt>
                <%#ShowServeyItemOption(Eval("SurveyItemID").ToString(), Eval("TypeID").ToString(), Container.ItemIndex.ToString())%>
            </dl>
        </ItemTemplate>
    </asp:Repeater>
    <asp:Button ID="btnSave" runat="server" Text="提交" OnClick="btnSave_Click" CssClass="submit" />
    </form>
</div>
