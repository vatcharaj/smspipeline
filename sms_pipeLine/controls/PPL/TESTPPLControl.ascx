<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TESTPPLControl.ascx.cs" Inherits="sms_pipeLine.controls.TESTPPLControl" %>
<asp:Panel ID="PPL_PANEL" runat="server" Visible="false" >
      
      <asp:GridView ID="Grid_ppl" runat="server" onrowcommand="Grid_ppl_RowCommand"  CssClass="GRIDDETAIL" >
       <HeaderStyle BackColor="#DCDCDC" />
      <AlternatingRowStyle BackColor="#F5F5F5" />
      <Columns>
      <asp:ButtonField ButtonType="Image" ImageUrl="~/Content/img/small_remove.png"  CommandName="DelPPL"  ControlStyle-CssClass="btn-small"   />
       <%--  <asp:TemplateField  HeaderText="DELETE" >
                    <ItemStyle  HorizontalAlign="Center"    />
                    <ItemTemplate>
                        <asp:Button ID="Button2" runat="server" Text="x" 
                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                        CssClass="btn-small" CommandName="DelPPL" />
                    </ItemTemplate>
                </asp:TemplateField>--%>
     <%-- <asp:ButtonField ButtonType="Button"  CommandName="DelPPL"  ControlStyle-CssClass="btn-small" HeaderText="DELETE" Text="x"   />--%>
</Columns>
</asp:GridView>
  <asp:Label ID="groupLabel" runat="server" Visible="false"></asp:Label>
  <asp:Label ID="LevelLabel" runat="server" Visible="false"></asp:Label>
</asp:Panel>