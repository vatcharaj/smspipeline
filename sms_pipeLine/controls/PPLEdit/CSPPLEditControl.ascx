<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CSPPLEditControl.ascx.cs" Inherits="sms_pipeLine.controls.CSPPLEditControl" %>
<asp:UpdatePanel ID="PPL_PANEL" runat="server" Visible="false"  >
      <ContentTemplate>
         <h6> รายชื่อบุคคลในกลุ่ม บซ.ตจก. 
         <div id="updateby" style="float:right">แก้ไขเมื่อ :
      <asp:Label ID="updateLabel" runat="server" Text="Label"></asp:Label>
         </div>
             <h6>
             </h6>
             <div>
                 ค้นหา :
                 <asp:TextBox ID="SchPPL" runat="server" AutoPostBack="true" 
                     ontextchanged="SchPPL_TextChanged"></asp:TextBox>
                 <asp:Button ID="AddpplBtn" runat="server" 
                  CssClass=" btn btn-default btn-small"  Visible="false"
                     onclick="AddpplBtn_Click" style="float:right; margin-right:10px"  
                     Text="เพิ่มบุคคล" />
             </div>
             <hr />
             <asp:GridView ID="Grid_ppl" runat="server" AllowPaging="true" 
                 AllowSorting="true" AutoGenerateColumns="false" CssClass="GRIDDETAIL" 
                 OnPageIndexChanging="Grid_ppl_PageIndexChanging" 
                 onrowcommand="Grid_ppl_RowCommand" OnSorting="Grid_ppl_Sorting" PageSize="20">
                 <HeaderStyle BackColor="#DCDCDC" />
                 <AlternatingRowStyle BackColor="#F5F5F5" />
                 <Columns>
                     <asp:BoundField DataField="PERSON_ID" HeaderText="ID" 
                         SortExpression="PERSON_ID" />
                     <asp:BoundField DataField="NAME" HeaderText="NAME" SortExpression="NAME" />
                     <asp:BoundField DataField="POSITION" HeaderText="POSITION" 
                         SortExpression="POSITION" />
                     <asp:BoundField DataField="COMPANY" HeaderText="COMPANY" 
                         SortExpression="COMPANY" />
                     <asp:BoundField DataField="MOBILE" HeaderText="MOBILE" />
                     <asp:BoundField DataField="GROUP_KEY" HeaderText="GROUP_KEY" Visible="false" />
                     <asp:ButtonField ButtonType="Link" CommandName="EditPPL" 
                         ControlStyle-CssClass="btn-small" Text="Edit" />
                     <asp:ButtonField ButtonType="Image" CommandName="DelPPL" 
                         ControlStyle-CssClass="btn-small" ImageUrl="~/Content/img/small_remove.png" />
                 </Columns>
             </asp:GridView>
             <asp:Panel ID="grpPanel" runat="server" CssClass="dimmer" Visible="false">
             </asp:Panel>
             <asp:Panel ID="AddPPLPanel" runat="server" ScrollBars="None" Visible="false">
                 <div class="grpPanel">
                     <div class="modal-header">
                         <asp:Button ID="Button1" runat="server" aria-hidden="true" 
                             CssClass="close btn-small" data-dismiss="modal" onclick="ClosePanel_Click" 
                             Text="x" />
                         <asp:Label ID="EditpplLabel" runat="server" CssClass="span2 " 
                             style="text-align:right">แก้ไขข้อมูลรายบุคคล  </asp:Label>
                         <br />
                         <asp:Panel ID="HeadModal" runat="server">
                             <div style=" margin:15px">
                                 <asp:Label ID="Label2" runat="server">ค้นหาบุคคล : </asp:Label>
                                 <asp:TextBox ID="SchBox" runat="server" AutoPostBack="true" 
                                     OnTextChanged="SchBox_Changed"></asp:TextBox>
                             </div>
                         </asp:Panel>
                     </div>
                     <asp:Panel ID="resultppl" runat="server">
                         <div class="span1 ">
                         </div>
                         <asp:Label ID="companyLabel" runat="server" Visible="false"></asp:Label>
                         <asp:Label ID="GroupIDINLabel" runat="server" Visible="false"></asp:Label>
                         <table>
                             <tr style="visibility:hidden">
                                 <td align="right">
                                     รหัส :
                                 </td>
                                 <td>
                                     <asp:Label ID="EmployeeIDLabel" runat="server" Text=""></asp:Label>
                                 </td>
                             </tr>
                             <tr>
                                 <td align="right">
                                     ชื่อ :
                                 </td>
                                 <td>
                                     <asp:Label ID="NameLabel" runat="server" Text=""></asp:Label>
                                 </td>
                             </tr>
                             <tr>
                                 <td align="right">
                                     ตำแหน่ง :
                                 </td>
                                 <td>
                                     <asp:Label ID="posnameLabel" runat="server" Text=""></asp:Label>
                                 </td>
                             </tr>
                             <tr>
                                 <td align="right">
                                     บริษัท :
                                 </td>
                                 <td>
                                     <asp:Label ID="unitnameLabel" runat="server" Text=""></asp:Label>
                                 </td>
                             </tr>
                             <tr>
                                 <td align="right">
                                     โทร :</td>
                                 <td>
                                     <asp:TextBox ID="TelLabel" runat="server" Text=""></asp:TextBox>
                                 </td>
                             </tr>
                             <tr>
                                 <td align="right" valign="top">
                                     กลุ่ม :</td>
                                 <td style="width:100%">
                                     <div style="height:140px; overflow:auto">
                                         <asp:CheckBoxList ID="cl" runat="server">
                                         </asp:CheckBoxList>
                                     </div>
                                 </td>
                             </tr>
                         </table>
                     </asp:Panel>
                     <asp:Panel ID="NoResult" runat="server" Visible="false">
                         <div class="span1 ">
                         </div>
                         <asp:Label ID="errorlabel" runat="server" ForeColor="Red"> ไม่พบข้อมูล</asp:Label>
                     </asp:Panel>
                     <div class="modal-footer">
                         <asp:Button ID="UpdatePPL" runat="server" CssClass="btn btn " 
                             onclick="UpdatePPL_Click" Text="UPDATE" />
                         <asp:Button ID="SavePPL" runat="server" CssClass="btn btn " Enabled="false" 
                             onclick="SavePPL_Click" Text="ADD" />
                         <asp:Button ID="Button5" runat="server" CssClass="btn btn " 
                             OnClick="ClosePanel_Click" Text="CANCEL" />
                     </div>
                 </div>
             </asp:Panel>
             <h6>
             </h6>
         </h6>
</ContentTemplate>
</asp:UpdatePanel>