<%@ Page Title="Report" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="report.aspx.cs" Inherits="sms_pipeLine.report" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
    <%@ Register TagName="GD" TagPrefix="CC"  Src="~/controls/GroupDetailControl.ascx"%>
<%@ Register TagName="RP" TagPrefix="CC"  Src="~/controls/ReportControl.ascx"%>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"/>

  <cc:RP ID="REPORTCONTROL" runat="server" />
    

</asp:Content>
