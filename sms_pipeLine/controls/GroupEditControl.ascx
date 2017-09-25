<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupEditControl.ascx.cs" Inherits="sms_pipeLine.controls.GroupEditControl" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
  <table width="100%" class="recptable">
  <tr>
  <td class="recptable tree">
   <div class="scrollbar" id="ex3" >
   <asp:TreeView ID="TreeView1" ExpandDepth="1"  runat="server"  onselectednodechanged="TreeView1_SelectedNodeChanged"  >
  </asp:TreeView>
  </div>
  </td>
  <td class="recptable detailRci">
  <div >
   กลุ่ม : <asp:Label ID="GROUPLabel" runat="server" Text="-" ClientIDMode="Static"></asp:Label>
      <asp:Label ID="GROUP_IDLabel" runat="server" Text="-" ClientIDMode="Static" Visible="false"></asp:Label>
           <div class="input-append " style="float:right">
                <asp:Button ID="AddgroupBtn" runat="server"  OnClick="AddgroupBtn_Click"
                    CssClass="btn btn-default btn-small"  Text="เพิ่มกลุ่ม"  />
                <asp:Button ID="DelGroup" runat="server" CssClass="btn btn-default btn-small" 
                    Text="ลบกลุ่ม" OnClick="DelGroup_Click" OnClientClick="javascript:return ConfirmDel();" />
            </div>

  
  </div>

 <br />
   <asp:GridView ID="Grid_group" runat="server"     onrowcommand="Grid_group_RowCommand"
      CssClass="GRIDDETAIL" AutoGenerateColumns="false"   >
      <HeaderStyle BackColor="#DCDCDC" />
      <AlternatingRowStyle BackColor="#F5F5F5" />
      <Columns>

         <asp:BoundField  HeaderText="GROUP_ID" DataField="GROUP_ID"  Visible="false" /> 
           <asp:BoundField  HeaderText="GROUP NAME" DataField="GROUP_NAME" SortExpression="GROUP_NAME"   /> 
             <asp:BoundField  HeaderText="LASTUPDATE" DataField="LASTUPDATE" SortExpression="LASTUPDATE" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" HtmlEncode="false"   /> 
             <asp:BoundField  HeaderText="UPDATE BY" DataField="UPDATE_BY" SortExpression="UPDATE_BY"   /> 
              <asp:TemplateField > 
                            <ItemTemplate>
                                 <asp:LinkButton runat="server" ID="EditGrp"
                                  Text="Edit" CommandName="EditGrp"  
                                  CommandArgument='<%#Eval("GROUP_ID") %>' ></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
              <%-- <asp:ButtonField ButtonType="Link" CommandName="EditGrp"   ControlStyle-CssClass="btn-small"  Text="Edit"   />--%>
             <asp:BoundField  HeaderText="DEPARTMENT" DataField="DEPARTMENT" Visible="false" />
            <asp:BoundField  HeaderText="DEPARTMENT_ID" DataField="DEPARTMENT_ID" Visible="false"  />
            <asp:BoundField  HeaderText="GROUP_LEVEL" DataField="GROUP_LEVEL"  Visible="false"  />
              <asp:BoundField  HeaderText="MASTER_GROUP_ID" DataField="MASTER_GROUP_ID"  Visible="false"  />
      </Columns>
      </asp:GridView>  
      <div>
         <asp:CheckBoxList ID="cl"   AutoPostBack="true"  
         runat="server"   RepeatColumns="2"
              onselectedindexchanged="cl_SelectedIndexChanged"> </asp:CheckBoxList>
      </div>
  </td>
  </tr>
  </table>
  <asp:Panel ID="grpPanel" CssClass="dimmer"  runat="server" Visible="false"></asp:Panel>
      <asp:Panel ID="AddGroupPanel" runat="server" Visible="false">
      <div class="grpPanel">
     <div class="modal-header">
                    <asp:Button CssClass="close btn-small" data-dismiss="modal" aria-hidden="true" runat="server" ID="ClosePreview" 
                                            Text="x" OnClick="ClosePanel_Click" ></asp:Button >
                    <h5>ชื่อกลุ่ม : </h5>
     </div>
     <div  style="margin-left:13px">
         กรุณาระบุชื่อกลุ่ม :  <asp:TextBox ID="InputGroup" runat="server"></asp:TextBox>
    </div>
           <div class="modal-footer">
          <asp:Button ID="SAVEGrp" CssClass="btn btn " runat="server" Text="SAVE"  OnClick="SAVEGrp_Click"/>
          <asp:Button ID="Button3" CssClass="btn btn " runat="server" Text="CANCEL" OnClick="ClosePanel_Click" />
     </div>
      </div>
    </asp:Panel>
      <asp:Panel ID="EditGrpPanel" runat="server" Visible="false">
       <div class="grpPanel">
     <div class="modal-header">
                    <asp:Button CssClass="close btn-small" data-dismiss="modal" aria-hidden="true" runat="server" ID="Button1" 
                                            Text="x" OnClick="ClosePanel_Click" ></asp:Button >
                    <h5>ชื่อกลุ่ม : </h5>
     </div>
     <div  style="margin-left:13px">
         กลุ่ม :  <asp:TextBox ID="EditGroupTextBox" runat="server"></asp:TextBox>
         <asp:TextBox ID="EditGroupIDTextBox" runat="server" Visible="false"></asp:TextBox>
    </div>
           <div class="modal-footer">
          <asp:Button ID="EditGrp" CssClass="btn btn " runat="server" Text="EDIT"  OnClick="EditGrp_Click"/>
          <asp:Button ID="Button4" CssClass="btn btn " runat="server" Text="CANCEL" OnClick="ClosePanel_Click" />
     </div>
      </div>

    </asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>
  
<script type="text/javascript">
    function ConfirmDel() {
        var gr = document.getElementById("GROUPLabel").innerHTML;
        var Ok = confirm('Are you sure want to DELETE ' + gr + '?');
        if (Ok)
            return true;
        else
            return false;
    }
 </script>