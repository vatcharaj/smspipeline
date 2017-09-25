<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TestControl.ascx.cs" Inherits="sms_pipeLine.controls.TestControl" %>
  <asp:Panel ID="GROUP_PANEL" runat="server" Visible="false"  style="max-height:450px; overflow:auto" >
    
      <asp:GridView ID="Grid_group" runat="server" CssClass="GRIDDETAIL"  >
      <HeaderStyle BackColor="#DCDCDC" />
      <AlternatingRowStyle BackColor="#F5F5F5" />
      </asp:GridView>  
      
  </asp:Panel>