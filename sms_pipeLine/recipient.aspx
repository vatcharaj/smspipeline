<%@ Page Title="Recipient Management" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="recipient.aspx.cs" Inherits="sms_pipeLine.reciepient"  EnableEventValidation="false"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register TagName="GD" TagPrefix="CC"  Src="~/controls/GroupDetailControl.ascx"%>
<%@ Register TagName="GE" TagPrefix="CC"  Src="~/controls/GroupEditControl.ascx"%> 
<%@ Register TagName="PPLE" TagPrefix="CC"  Src="~/controls/PPLEditControl.ascx"%>     

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
    #UpdatePanel1 { 
      width:300px; height:100px;
     }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"/>
<h4><u>ระบบจัดการผู้รับ</u>  <asp:ImageButton ImageAlign="AbsMiddle" ViewStateMode="Enabled" ID="ImageButton1" runat="server" ImageUrl="Content/img/excel.png" Width="25px" Height="25px" OnClick="Button1_Click" ToolTip="รายชื่อผู้รับทั้งหมด"/></h4>
   <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server"> 
    <ContentTemplate> 
               
               
               <asp:UpdateProgress id="updateProgress" runat="server"> 
         <ProgressTemplate> 
                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">

                      <span style="border-width: 0px; position: fixed; padding: 50px; background-color: #000000; font-size: 20px; left: 40%; top: 40%; color:White">Processing.....</span>

                </div> 
         </ProgressTemplate> 
    </asp:UpdateProgress> 
     </ContentTemplate> 
 </asp:UpdatePanel>--%>
    <asp:TabContainer ID="TabContainer1" runat="server"   
        TabStripPlacement="TopRight" ActiveTabIndex="0" > 
    <asp:TabPanel ID="GroupTabPanel" runat="server" HeaderText="Group Detail"  >
    <ContentTemplate>
    <CC:GD ID="GD" runat="server" />
 </ContentTemplate>
     </asp:TabPanel> 
     <asp:TabPanel ID="EditPanel" runat="server" HeaderText="Group Editing"   >
      <ContentTemplate>
       <CC:GE ID="GE" runat="server" />
       </ContentTemplate>
      </asp:TabPanel>
      <asp:TabPanel ID="PPLTabPanel" runat="server" HeaderText="Recipient Editing" >
      <ContentTemplate>
        <CC:PPLE ID="PPLE" runat="server" />
       </ContentTemplate>
      </asp:TabPanel>
      
    </asp:TabContainer>
   



</asp:Content>
    