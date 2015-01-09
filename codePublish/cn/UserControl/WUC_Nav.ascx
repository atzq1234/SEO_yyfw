<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUC_Nav.ascx.cs" Inherits="HxSoft.Web.cn.UserControl.WUC_Nav" %>
<%@ Import Namespace="HxSoft.Common" %>
<span>您现在的位置：</span><a href="index<%=Config.FileExt %>">首页</a> &gt; <asp:Literal ID="litClassNav" runat="server"></asp:Literal>
