<%@ Page Title="Administrator" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="admin.aspx.cs" Inherits="sms_pipeLine.admin" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<div  id="Contentpage" style="margin: auto; width:800px">

  
 <h4><u>Administrator List</u>
      <div class="input-append " style="float:right">
                             <asp:Button ID="AddpplBtn" runat="server" CssClass=" btn btn-default btn-small" 
                    Text="เพิ่มบุคคล" onclick="AddpplBtn_Click" />
           <%--    <asp:LinkButton ID="AddpplBtn" style="float:right" runat="server"  CssClass="btn" onclick="AddpplBtn_Click"><i class="icon-user "></i> เพิ่มบุคคล</asp:LinkButton>--%>
        </div>

</h4>
 <br />
     <asp:GridView ID="AdminGridView" runat="server" AutoGenerateColumns="false" AllowSorting="true" OnSorting="AdminGridView_Sorting"
      onrowcommand="AdminGridView_RowCommand" CssClass="GRIDDETAIL" >
       <HeaderStyle BackColor="#DCDCDC" />
      <AlternatingRowStyle BackColor="#F5F5F5" />
      <Columns>
        <asp:BoundField HeaderText="ID" DataField="EMPLOYEE_ID"  SortExpression="EMPLOYEE_ID"  />
         <asp:BoundField HeaderText="NAME" DataField="FULLNAMETH"  SortExpression="FULLNAMETH"  />
         <asp:BoundField HeaderText="POSITION" DataField="POSNAME"  SortExpression="POSNAME"  />
          <asp:BoundField HeaderText="UNIT" DataField="UNITNAME"   SortExpression="UNITNAME" />
            <asp:BoundField HeaderText="ADMIN" DataField="IS_ADMIN"   SortExpression="IS_ADMIN"  ItemStyle-HorizontalAlign="Center" />
       <asp:ButtonField ButtonType="Link" CommandName="EditPPL"  ControlStyle-CssClass="btn-small"  Text="Edit"   />
     <asp:ButtonField ButtonType="Image" ImageUrl="~/Content/img/small_remove.png"  CommandName="DelPPL"  ControlStyle-CssClass="btn-small" />
</Columns>
</asp:GridView>


</div>
 <asp:Panel ID="grpPanel" CssClass="dimmer"  runat="server" Visible="false"></asp:Panel>
   <asp:Panel ID="AddPPLPanel" runat="server" Visible="false">
   
     <div class="grpPanel">
     <div class="modal-header">
    
                    <asp:Button CssClass="close btn-small" data-dismiss="modal" aria-hidden="true" runat="server" ID="Button1" 
                                    Text="x" onclick="ClosePanel_Click"></asp:Button > 
       <br />
      <asp:Panel ID ="HeadModal" runat="server">      
           <div style=" margin:15px">  
         <asp:Label ID="Label1" runat="server" >ค้นหาบุคคล : </asp:Label>
         <asp:TextBox ID="SchBox" runat="server" OnTextChanged="SchBox_Changed"></asp:TextBox> 
         </div> 
     </asp:Panel> 
     </div>
   
     <asp:panel ID="resultppl" Visible="false"  runat="server"  >
     <div class="span1 "></div>
     <table>
     <tr>
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
       หน่วยงาน : </td>
       <td><asp:Label ID="unitnameLabel" runat="server" Text=""></asp:Label></td>
       </tr>
       <tr>
       <td align="right">  <asp:CheckBox ID="IsAdminChk" runat="server" Checked="false" /></td>
       <td> Set to Administrator</td>
       </tr>
       
       </table>
       
    </asp:panel>
             <asp:panel ID="NoResult" Visible="false"  runat="server"  >
     <div class="span1 "></div>
   <asp:Label ID="errorlabel" runat="server" ForeColor="Red"> ไม่พบข้อมูล</asp:Label> 
    </asp:panel>
     <div class="modal-footer"> 
     <asp:Button ID="UpdatePPL" CssClass="btn btn " runat="server" Text="SAVE" 
                   onclick="UpdatePPL_Click"  Visible="false" />

          <asp:Button ID="SavePPL" CssClass="btn btn " runat="server" Text="ADD" 
                   onclick="SavePPL_Click"  Enabled="false"  />
          <asp:Button ID="Button5" CssClass="btn btn " runat="server" Text="CANCEL" OnClick="ClosePanel_Click" />
     </div>
      </div>
   
    </asp:Panel>



</asp:Content>