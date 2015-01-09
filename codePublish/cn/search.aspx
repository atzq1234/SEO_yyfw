<%@ Page Title="" Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true" CodeBehind="search.aspx.cs" Inherits="HxSoft.Web.cn.search" %>

<%@ Register Src="UserControl/WUC_Banner.ascx" TagName="WUC_Banner" TagPrefix="uc1" %>
<%@ Register Src="UserControl/WUC_Left.ascx" TagName="WUC_Left" TagPrefix="uc2" %>
<%@ Register Src="UserControl/WUC_Nav.ascx" TagName="WUC_Nav" TagPrefix="uc3" %>
<%@ Register Src="UserControl/WUC_Search_List.ascx" TagName="WUC_Search_List" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="banner">
        <uc1:WUC_Banner ID="WUC_Banner1" runat="server" Height="180" Width="960" ParentID="1" ClassID="1" />
    </div>
    
    <div class="center">
    	<div class="aboutMain">
            <div class="nav">
                <uc3:WUC_Nav ID="WUC_Nav1" runat="server" />
            </div>
            <div class="left">
                <uc2:WUC_Left ID="WUC_Left1" runat="server" />
            </div>
            <div class="right">
                <div class="content">
                    <uc4:WUC_Search_List ID="WUC_Search_List1" runat="server" ConfigID="1" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
