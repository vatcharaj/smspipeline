<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PPLEditControl.ascx.cs" Inherits="sms_pipeLine.controls.PPLEditControl" %>
     <%@ Register TagName="CS" TagPrefix="CC"  Src="~/controls/PPLEdit/CSPPLEditControl.ascx"%>
     <%@ Register TagName="TCS" TagPrefix="CC"  Src="~/controls/PPLEdit/TCSPPLEditControl.ascx" %>
     <%@ Register TagName="GC" TagPrefix="CC"  Src="~/controls/PPLEdit/GCPPLEditControl.ascx"%>
       <%@ Register TagName="PTT" TagPrefix="CC"  Src="~/controls/PPLEdit/PTTPPLEditControl.ascx"%>
    <%@ Register TagName="PTTB" TagPrefix="CC"  Src="~/controls/PPLEdit/PTTBPPLEditControl.ascx"%>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <table width="100%" class="recptable">
  <tr>
  <td class="recptable tree">
  <div style="margin:10px">
  <asp:LinkButton ID="RefreshLink" OnClick="RefreshLink_Click"
    CssClass="btn btn-primary" runat="server" style="float:right">
   <i class="icon-refresh icon-white"></i>
   </asp:LinkButton>
      <asp:PlaceHolder ID="LinkHolder" runat="server"></asp:PlaceHolder>
   </div>
  </td>
  <td class="recptable detailRci">
 
     <CC:CS ID="CS" runat="server" Visible="false" />
     <CC:TCS ID="TCS" runat="server" Visible="false" />
     <CC:GC ID="GC" runat="server" Visible="false" />
      <CC:PTT ID="PTT" runat="server" Visible="false" />
    <CC:PTTB ID="PTTB" runat="server" Visible="false" />
  </td>
  </tr>
  </table>
    </ContentTemplate>
</asp:UpdatePanel>