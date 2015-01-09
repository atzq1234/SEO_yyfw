<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUC_Job_Details.ascx.cs" Inherits="HxSoft.Web.cn.UserControl.WUC_Job_Details" %>
<script type="text/javascript">
    $f.dom.ready(function () {
        $f("<%=form1.ClientID %>", {
            rules: {
                txtName: { required: 1 },
                txtTel: { required: 1, chkTel: 1 },
                txtEmail: { required: 1, email: 1 },
                FileUpload1: { required: 1 },
                txtVerifyCode: { required: 1, digit: 1, minlength: 4, maxlength: 4, ajaxVerifyCode: 1 }
            },
            messages: {
                txtName: { required: "不能为空" },
                txtTel: { required: "不能为空" },
                txtEmail: { required: "不能为空", email: "请输入正确的邮件地址" },
                FileUpload1: { required: "请上传doc或docx文件" },
                txtVerifyCode: { required: "不能为空", digit: "请输入四位数字验证码", minlength: "请输入四位数字验证码", maxlength: "请输入四位数字验证码" }
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
<div class="job_details">
    <form id="form1" runat="server">
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="table_info">
        <tr>
            <td colspan="4" class="title">
                职位信息
            </td>
        </tr>
        <tr>
            <td class="tit">
                招聘部门
            </td>
            <td class="info">
                <asp:Literal ID="litDepartment" runat="server"></asp:Literal>
            </td>
            <td class="tit">
                职位名称
            </td>
            <td class="info">
                <asp:Literal ID="litJobName" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="tit">
                招聘人数
            </td>
            <td class="info">
                <asp:Literal ID="litJobNum" runat="server"></asp:Literal>
            </td>
            <td class="tit">
                待遇
            </td>
            <td class="info">
                <asp:Literal ID="litSalary" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="tit">
                工作地点
            </td>
            <td class="info">
                <asp:Literal ID="litWorkPlace" runat="server"></asp:Literal>
            </td>
            <td class="tit">
                截止日期
            </td>
            <td class="info">
                <asp:Literal ID="litEndTime" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="tit">
                招聘要求
            </td>
            <td colspan="3" class="demand">
                <asp:Literal ID="litDemand" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="table_form">
        <tr>
            <td colspan="2" class="title">
                我要应聘
            </td>
        </tr>
        <tr>
            <td class="tit">
                姓 名：
            </td>
            <td class="inp">
                <asp:TextBox ID="txtName" runat="server" CssClass="input"  ruleKey="txtName"/>
            </td>
        </tr>
        <tr>
            <td class="tit">
                E-mail：
            </td>
            <td class="inp">
                <asp:TextBox ID="txtEmail" runat="server" CssClass="input"  ruleKey="txtEmail"/>
            </td>
        </tr>
        <tr>
            <td class="tit">
                联系电话：
            </td>
            <td class="inp">
                <asp:TextBox ID="txtTel" runat="server" CssClass="input"  ruleKey="txtTel"/>
            </td>
        </tr>
        <tr>
            <td class="tit">
                上传文件：
            </td>
            <td class="inp">
                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="input"  ruleKey="FileUpload1"/>
            </td>
        </tr>
        <tr>
            <td class="tit">
                验证码：
            </td>
            <td class="inp">
                    <asp:TextBox ID="txtVerifyCode" runat="server" Columns="6" CssClass="code" ruleKey="txtVerifyCode" onfocus="RefreshVerifyCode()" />
                    <img src="/Common/VerifyCode.aspx" id="ImgVerifyCode" title="点击刷新验证码" class="code" onclick="RefreshVerifyCode()" />
            </td>
        </tr>
        <tr>
            <td class="tit">
            </td>
            <td class="inp">
                <asp:Button ID="btnSend" runat="server" Text="提交" OnClick="btnSend_Click" CssClass="submit" />
                &nbsp;&nbsp;
                <input type="reset" value="重置" name="reset" class="reset" /><asp:Label ID="errMsg" runat="server" ForeColor="red"></asp:Label>
            </td>
        </tr>
    </table>
    </form>
</div>
