<%@ Page Title="Template" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="SMSTemplate.aspx.cs" Inherits="sms_pipeLine.SMSTemplate" %>
  

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <div  id="Contentpage" style="margin: auto; width:70%">

  
 <h4><u>Template List</u>
      <div class="input-append " style="float:right">
                             <asp:Button ID="AddBtn" runat="server" CssClass=" btn btn-default btn-small" 
                    Text="สร้างข้อความใหม่" onclick="AddBtn_Click" />
           <%--    <asp:LinkButton ID="AddpplBtn" style="float:right" runat="server"  CssClass="btn" onclick="AddpplBtn_Click"><i class="icon-user "></i> เพิ่มบุคคล</asp:LinkButton>--%>
        </div>

</h4>
 <br />
     <asp:GridView ID="TemplateGridView" runat="server" AutoGenerateColumns="false" AllowSorting="true" OnSorting="TemplateGridView_Sorting"
      onrowcommand="TemplateGridView_RowCommand" CssClass="GRIDDETAIL" >
       <HeaderStyle BackColor="#DCDCDC" />
      <AlternatingRowStyle BackColor="#F5F5F5" />
      <Columns>
      <asp:TemplateField HeaderText="ID" ItemStyle-Width="30px"  ItemStyle-HorizontalAlign="Center">
    <ItemTemplate>
        <%# Container.DataItemIndex + 1 %>
    </ItemTemplate>
</asp:TemplateField>
        <asp:BoundField HeaderText="ID" DataField="ID"  SortExpression="ID"   Visible="false" />
         <asp:BoundField HeaderText="MESSAGE" DataField="MESSAGE"  SortExpression="MESSAGE"  ItemStyle-Width="500px" />
         <asp:BoundField HeaderText="UPDATE BY" DataField="UPDATE_BY"  SortExpression="UPDATE_BY"  />
         <asp:BoundField HeaderText="LASTUPDATE" DataField="LASTUPDATE"  SortExpression="LASTUPDATE" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" /> 
       <asp:ButtonField ButtonType="Link" CommandName="EditTempl"  ControlStyle-CssClass="btn-small"  Text="Edit" ItemStyle-HorizontalAlign="Center"   />
     <asp:ButtonField ButtonType="Image" ImageUrl="~/Content/img/small_remove.png"  CommandName="DelTempl"  ControlStyle-CssClass="btn-small" ItemStyle-HorizontalAlign="Center" />
</Columns>
</asp:GridView>


</div>
<asp:Panel ID="grpPanel" CssClass="dimmer"  runat="server" Visible="false"></asp:Panel>
   <asp:Panel ID="AddPanel" runat="server" Visible="false">
   
     <div class="grpPanel">
     <div class="modal-header">
    
                    <asp:Button CssClass="close btn-small" data-dismiss="modal" aria-hidden="true" runat="server" ID="Button1" 
                                    Text="x" onclick="ClosePanel_Click"></asp:Button > 
       <br />
      <asp:Panel ID ="HeadModal" runat="server">      
           <div style=" margin:8px">  
         <asp:Label ID="Label1" runat="server" >ข้อความ : </asp:Label>
         <asp:Label ID="TemplID" runat="server" Text="" Visible="false"></asp:Label>
         </div> 
     </asp:Panel> 
     </div>
   
     <asp:panel ID="result"  runat="server"  >
     <div class="span1 "></div>
         <asp:TextBox ID="TextMessage" Height="150px" AutoPostBack="true"  Width="80%" TextMode="MultiLine" 
             runat="server" ClientIDMode="Static"  onkeypress="return isAvailableKey(event)"  ontextchanged="TextMessage_TextChanged"></asp:TextBox>
       
    </asp:panel>
       
     <div class="modal-footer"> 
       Text Count :   <asp:Label ID="TextCount" ClientIDMode="Static" runat="server" Text="0"></asp:Label>/320
     <asp:Button ID="Update" CssClass="btn btn " runat="server" Text="SAVE" 
                   onclick="Update_Click" ClientIDMode="Static"  Visible="false" />

          <asp:Button ID="Save" CssClass="btn btn " runat="server" Text="ADD" 
                   onclick="Save_Click" ClientIDMode="Static"  Enabled="false"  />
          <asp:Button ID="Button5" CssClass="btn btn " runat="server" Text="CANCEL" OnClick="ClosePanel_Click" />
     </div>
      </div>
   
    </asp:Panel>

    <link href="./Scripts/jquery/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="./Scripts/jquery/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="./Scripts/jquery/jquery-ui.js" type="text/javascript"></script>
 <script type="text/javascript">



      function isAvailableKey(evt) {
          var lbl = document.getElementById("TextMessage");
          var lbl2 = document.getElementById("TextCount");
          var charCode = lbl.innerHTML.length;
         lbl2.innerHTML = charCode;

         if (charCode > 319)
              return false;
          
          return true;
      }

   

      
 </script>

</asp:Content>
