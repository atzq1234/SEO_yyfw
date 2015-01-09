<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
        "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Page language="c#" AutoEventWireup="false" Inherits="Brettle.Web.NeatUpload.ProgressPage" %>
<%@ Register TagPrefix="Upload" Namespace="Brettle.Web.NeatUpload" Assembly="Brettle.Web.NeatUpload" %>
<%--
NeatUpload - an HttpModule and User Controls for uploading large files
Copyright (C) 2005  Dean Brettle

This library is free software; you can redistribute it and/or
modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation; either
version 2.1 of the License, or (at your option) any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public
License along with this library; if not, write to the Free Software
Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
--%>
<html>
	<head runat="server">
		<title>�ϴ�����</title>
		<link rel="stylesheet" type="text/css" title="default" href="default.css" />		
		<style type="text/css">
		body, form, table, tr, td {
			margin: 0px;
			border: 0px none;
			padding: 0px;
			font-size:12px;
		}

		html, body, form, #progressDisplayCenterer {
			width: 100%;
			height: 100%;
		}
		
		#progressDisplayCenterer {
			vertical-align: middle;
			margin: 0 auto;
		}
		
		#progressDisplay {
			vertical-align: middle;
			width: 100%;
		}
		
		#barTd {
			width: 100%;
		}
		
		#statusDiv {
			border-width: 1px;
			border-style: solid;
			padding: 0px;
			position: relative;
			width: 100%;
			text-align: center;
			z-index: 1; 
		}
		
		#barDiv,#barDetailsDiv {
			border: 0px none ; 
			margin: 0px; 
			padding: 0px; 
			position: absolute; 
			top: 0pt; 
			left: 0pt; 
			z-index: -1; 
			width: 75%;
			height:100%;
		}
		</style>
	</head>
	<body>
		<form id="dummyForm" runat="server">
		<table id="progressDisplayCenterer">
		<tr>
		<td>
		<table id="progressDisplay" class="ProgressDisplay">
		<tr>
		<td id="barTd" >
			<div id="statusDiv" runat="server" class="StatusMessage">&#160;
				<Upload:DetailsSpan id="normalInProgress" runat="server" WhenStatus="NormalInProgress" style="font-weight: normal; white-space: nowrap;">
					��ɱ���:<%# FormatCount(BytesRead) %>/<%# FormatCount(BytesTotal) %> <%# CountUnits %>(<%# String.Format("{0:0%}", FractionComplete) %>)
                    �ϴ��ٶ�:<%# FormatRate(BytesPerSec) %>
					ʣ��ʱ��:<%# FormatTimeSpan(TimeRemaining) %>
				</Upload:DetailsSpan>
				<Upload:DetailsSpan id="chunkedInProgress" runat="server" WhenStatus="ChunkedInProgress" style="font-weight: normal; white-space: nowrap;">
					<%# FormatCount(BytesRead) %> <%# CountUnits %>
					�ϴ��ٶ�:<%# FormatRate(BytesPerSec) %>
					��ʱ:<%# FormatTimeSpan(TimeElapsed) %>
				</Upload:DetailsSpan>
				<Upload:DetailsSpan id="processing" runat="server" WhenStatus="ProcessingInProgress ProcessingCompleted" style="font-weight: normal; white-space: nowrap;">
					<%# ProcessingHtml %>
				</Upload:DetailsSpan>
				<Upload:DetailsSpan id="completed" runat="server" WhenStatus="Completed">
					�ϴ��ɹ�!�ļ���С:<%# FormatCount(BytesRead) %> <%# CountUnits %> 
					�ϴ��ٶ�:<%# FormatRate(BytesPerSec) %> 
					��ʱ:<%# FormatTimeSpan(TimeElapsed) %> 
				</Upload:DetailsSpan>
				<Upload:DetailsSpan id="cancelled" runat="server" WhenStatus="Cancelled">
					ȡ���ɹ�!
				</Upload:DetailsSpan>
				<Upload:DetailsSpan id="rejected" runat="server" WhenStatus="Rejected">
					���ܾ�:<%# Rejection != null ? Rejection.Message : "" %>
				</Upload:DetailsSpan>
				<Upload:DetailsSpan id="error" runat="server" WhenStatus="Failed">
					����:<%# Failure != null ? Failure.Message : "" %>
				</Upload:DetailsSpan>
				<Upload:DetailsDiv id="barDetailsDiv" runat="server" UseHtml4="true"
					 Width='<%# Unit.Percentage(Math.Floor(100*FractionComplete)) %>' CssClass="ProgressBar"></Upload:DetailsDiv>	
			</div>
		</td>
		<td>
			<asp:HyperLink id="cancel" runat="server" Visible='<%# CancelVisible %>' NavigateUrl='<%# CancelUrl %>' ToolTip="ȡ���ϴ�" CssClass="ImageButton" ><img id="cancelImage" src="cancel.png" alt="ȡ���ϴ�" /></asp:HyperLink>
			<asp:HyperLink id="refresh" runat="server" Visible='<%# StartRefreshVisible %>' NavigateUrl='<%# StartRefreshUrl %>' ToolTip="ˢ��" CssClass="ImageButton" ><img id="refreshImage" src="refresh.png" alt="ˢ��" /></asp:HyperLink>
			<asp:HyperLink id="stopRefresh" runat="server" Visible='<%# StopRefreshVisible %>' NavigateUrl='<%# StopRefreshUrl %>' ToolTip="ֹͣˢ��" CssClass="ImageButton"><img id="stopRefreshImage" src="stop_refresh.png" alt="ֹͣˢ��" /></asp:HyperLink>
		</td>
		</tr>
		</table>
		</td>
		</tr>
		</table>
		</form>
	</body>
</html>
