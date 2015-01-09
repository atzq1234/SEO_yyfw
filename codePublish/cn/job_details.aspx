<%@ Page Title="" Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true" CodeBehind="job_details.aspx.cs" Inherits="HxSoft.Web.cn.job_details" %>

<%@ Register Src="UserControl/WUC_Banner.ascx" TagName="WUC_Banner" TagPrefix="uc1" %>
<%@ Register Src="UserControl/WUC_Left.ascx" TagName="WUC_Left" TagPrefix="uc2" %>
<%@ Register Src="UserControl/WUC_Nav.ascx" TagName="WUC_Nav" TagPrefix="uc3" %>
<%@ Register Src="UserControl/WUC_Job_Details.ascx" TagName="WUC_Job_Details" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="banner">
        <uc1:WUC_Banner ID="WUC_Banner1" runat="server" Height="300" Width="700" />
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
                    <uc4:WUC_Job_Details ID="WUC_Job_Details1" runat="server" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
