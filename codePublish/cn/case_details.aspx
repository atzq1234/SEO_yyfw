<%@ Page Title="" Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true"
    CodeBehind="case_details.aspx.cs" Inherits="HxSoft.Web.cn.case_details" %>

<%@ Register Src="UserControl/WUC_Banner.ascx" TagName="WUC_Banner" TagPrefix="uc1" %>
<%@ Register Src="UserControl/WUC_Left.ascx" TagName="WUC_Left" TagPrefix="uc2" %>
<%@ Register Src="UserControl/WUC_Nav.ascx" TagName="WUC_Nav" TagPrefix="uc3" %>
<%@ Register Src="UserControl/WUC_Product_Details.ascx" TagName="WUC_Product_Details"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <uc4:WUC_Product_Details ID="WUC_Product_Details1" runat="server" />
</asp:Content>
