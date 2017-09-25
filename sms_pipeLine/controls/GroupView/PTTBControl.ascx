<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PTTBControl.ascx.cs" Inherits="sms_pipeLine.controls.GroupView.PTTBControl" %>
<asp:UpdatePanel  ID="GROUP_PANEL" runat="server" Visible="false"  style="max-height:450px; overflow:auto" >
     <ContentTemplate>
      <asp:GridView ID="Grid_group" runat="server"  AllowSorting="true" OnSorting="Grid_group_Sorting"
      CssClass="GRIDDETAIL" AutoGenerateColumns="false"  >
      <HeaderStyle BackColor="#DCDCDC" />
      <AlternatingRowStyle BackColor="#F5F5F5" />
      <Columns>
         <asp:BoundField  HeaderText="GROUP_ID" DataField="GROUP_ID"  Visible="false" /> 
           <asp:BoundField  HeaderText="GROUP NAME" DataField="GROUP_NAME" SortExpression="GROUP_NAME"   /> 
             <asp:BoundField  HeaderText="DEPARTMENT" DataField="DEPARTMENT" />
            <asp:BoundField  HeaderText="DEPARTMENT_ID" DataField="DEPARTMENT_ID" Visible="false"  />
            <asp:BoundField  HeaderText="GROUP_LEVEL" DataField="GROUP_LEVEL"  Visible="false"  />
              <asp:BoundField  HeaderText="MASTER_GROUP_ID" DataField="MASTER_GROUP_ID"  Visible="false"  />
      </Columns>
      </asp:GridView>  
       </ContentTemplate>
  </asp:UpdatePanel >