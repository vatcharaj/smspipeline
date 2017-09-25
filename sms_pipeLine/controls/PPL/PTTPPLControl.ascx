<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PTTPPLControl.ascx.cs" Inherits="sms_pipeLine.controls.PPL.PTTPPLControl" %>
<asp:UpdatePanel ID="PPL_PANEL" runat="server" Visible="false"  >
      <ContentTemplate>
      <asp:GridView ID="Grid_ppl" runat="server"  AutoGenerateColumns="false"
       AllowPaging="true" PageSize = "20" OnPageIndexChanging="Grid_ppl_PageIndexChanging"
      onrowcommand="Grid_ppl_RowCommand" CssClass="GRIDDETAIL" AllowSorting="true" OnSorting="Grid_ppl_Sorting"  >
       <HeaderStyle BackColor="#DCDCDC" />
      <AlternatingRowStyle BackColor="#F5F5F5" />
  
      <Columns>
        <asp:BoundField  HeaderText="ID" DataField="EMPLOYEE_ID" SortExpression="EMPLOYEE_ID"   />  
          <asp:BoundField  HeaderText="NAME" DataField="NAME"  SortExpression="NAME"  /> 
           <asp:BoundField  HeaderText="POSITION" DataField="POSITION"  SortExpression="POSITION"   /> 
            <asp:BoundField  HeaderText="UNIT" DataField="COMPANY" SortExpression="COMPANY"   />
            <asp:BoundField  HeaderText="MOBILE" DataField="MOBILE"     />
            <asp:BoundField  HeaderText="GROUP_KEY" DataField="GROUP_KEY"  Visible="false"  />
         <asp:ButtonField ButtonType="Link" CommandName="EditPPL"  ControlStyle-CssClass="btn-small"  Text="Edit"  Visible="false"  />
         <asp:ButtonField ButtonType="Image" ImageUrl="~/Content/img/small_remove.png"  CommandName="DelPPL"  ControlStyle-CssClass="btn-small"  Visible="false"  />
    
</Columns>
</asp:GridView  >  
<asp:Panel ID="grpPanel" CssClass="dimmer"  runat="server" Visible="false"></asp:Panel>
   <asp:Panel ID="AddPPLPanel" runat="server" Visible="false">
   
     <div class="grpPanel">
     <div class="modal-header">
     
                    <asp:Button CssClass="close btn-small" data-dismiss="modal" aria-hidden="true" runat="server" ID="Button1" 
                                    Text="x" onclick="ClosePanel_Click"></asp:Button > 
              
        <asp:Label ID="Label1" runat="server"  CssClass="span2 " style="text-align:right">แก้ไขข้อมูลรายบุคคล  </asp:Label>
     <br />
     </div>
     <asp:panel ID="resultppl"   runat="server"  >
     <div class="span1 "></div>
     <table>
   <tr style="visibility:hidden">
      <td align="right">รหัส : </td>
      <td><asp:Label ID="EmployeeIDLabel" runat="server" Text=""></asp:Label></td>
      </tr>
      <tr>
      <td align="right">
       ชื่อ : </td>
       <td><asp:Label ID="NameLabel" runat="server" Text=""></asp:Label></td>
       </tr>
       <tr>
       <td align="right">ตำแหน่ง : </td>
       <td><asp:Label ID="posnameLabel" runat="server" Text=""></asp:Label></td>
       </tr>
       <tr>
       <td align="right">
       บริษัท : </td>
       <td><asp:Label ID="unitnameLabel" runat="server" Text=""></asp:Label></td>
       </tr>
       <tr>
       <td align="right">  เบอร์โทร :</td>
       <td> <asp:TextBox ID="TelLabel" runat="server" Text="Label"></asp:TextBox></td>
           
       </tr>
       
       </table>
       
    </asp:panel>
           
     <div class="modal-footer"> 
     <asp:Button ID="UpdatePPL" CssClass="btn btn " runat="server" Text="SAVE" 
                   onclick="UpdatePPL_Click"   />

         
          <asp:Button ID="Button5" CssClass="btn btn " runat="server" Text="CANCEL" OnClick="ClosePanel_Click" />
     </div>
      </div>
   
    </asp:Panel>
  <asp:Label ID="groupLabel" runat="server" Visible="false"></asp:Label>
  <asp:Label ID="LevelLabel" runat="server" Visible="false"></asp:Label>
  </ContentTemplate>
</asp:UpdatePanel>