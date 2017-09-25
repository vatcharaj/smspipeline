<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PTTBPPLEditControl.ascx.cs" Inherits="sms_pipeLine.controls.PPLEdit.PTTBPPLEditControl" %>
<asp:UpdatePanel ID="PPL_PANEL" runat="server" Visible="false"  >
      <ContentTemplate>
         <h6> รายชื่อบุคคลในกลุ่ม ปตท.บริหาร </h6>
       <div>
   ค้นหา :     
           <asp:TextBox ID="SchPPL" runat="server" ontextchanged="SchPPL_TextChanged"  AutoPostBack="true"   ></asp:TextBox>
         <asp:Button ID="AddpplBtn" runat="server" CssClass=" btn btn-default btn-small"  style="float:right; margin-right:10px"
                    Text="เพิ่มบุคคล" onclick="AddpplBtn_Click" />
  </div>
       


  <hr />

      <asp:GridView ID="Grid_ppl" runat="server"  AutoGenerateColumns="false"
       AllowPaging="true" PageSize = "20" OnPageIndexChanging="Grid_ppl_PageIndexChanging"
      onrowcommand="Grid_ppl_RowCommand" CssClass="GRIDDETAIL" AllowSorting="true" OnSorting="Grid_ppl_Sorting"  >
       <HeaderStyle BackColor="#DCDCDC" />
      <AlternatingRowStyle BackColor="#F5F5F5" />
  
      <Columns>
        <asp:TemplateField SortExpression="STATUS" HeaderText="NEW" >
        <ItemTemplate>
        <asp:Image runat="server" ID="myImg"
            ImageUrl='<%# GetImageUrl(Eval("STATUS") as string,Eval("UPDATE_BY") as string) %>'  />
        </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField  HeaderText="ID" DataField="CODE" SortExpression="CODE"   />  
          <asp:BoundField  HeaderText="NAME" DataField="NAME"  SortExpression="NAME"  /> 
           <asp:BoundField  HeaderText="POSITION" DataField="POSITION"  SortExpression="POSITION"   /> 
            <asp:BoundField  HeaderText="UNIT" DataField="COMPANY" SortExpression="COMPANY"   />
            <asp:BoundField  HeaderText="MOBILE" DataField="MOBILE"     />
          <asp:BoundField  HeaderText="LASTUPDATE" DataField="LASTUPDATE" SortExpression="LASTUPDATE"  DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" HtmlEncode="false"      />
            <asp:BoundField  HeaderText="UPDATE_BY" DataField="UPDATE_BY"    SortExpression="UPDATE_BY"   />
            <asp:BoundField  HeaderText="GROUP_KEY" DataField="GROUP_KEY"  Visible="false"  />
             <asp:BoundField  HeaderText="UNITCODE" DataField="UNITCODE"  Visible="false"  />
             <asp:BoundField  HeaderText="POSCODE" DataField="POSCODE"  Visible="false"  />
         <asp:ButtonField ButtonType="Link" CommandName="EditPPL"  ControlStyle-CssClass="btn-small"  Text="Edit"   />
         <asp:ButtonField ButtonType="Image" ImageUrl="~/Content/img/small_remove.png"   CommandName="DelPPL"  ControlStyle-CssClass="btn-small"   />
  
</Columns>
</asp:GridView >

<asp:Panel ID="grpPanel" CssClass="dimmer"  runat="server" Visible="false"></asp:Panel>
   <asp:Panel ID="AddPPLPanel" runat="server" Visible="false" ScrollBars="None">
   
     <div class="grpPanel">
        <div class="modal-header">
            <asp:Button CssClass="close btn-small" data-dismiss="modal" aria-hidden="true" runat="server" ID="Button1" 
            Text="x" onclick="ClosePanel_Click"></asp:Button >  
            
            <asp:Label ID="EditpplLabel" runat="server"  CssClass="span2 " style="text-align:right">แก้ไขข้อมูลรายบุคคล  </asp:Label>
            <br />
       <asp:Panel ID ="HeadModal" runat="server">      
           <div style=" margin:15px">  
             <asp:Label ID="Label2" runat="server" >ค้นหาบุคคล : </asp:Label>
             <asp:TextBox ID="SchBox" runat="server" OnTextChanged="SchBox_Changed" AutoPostBack="true"></asp:TextBox> 
         </div> 
     </asp:Panel> 
     </div>
    <asp:panel ID="resultppl"   runat="server"   >
        <div class="span1 "></div>
        <asp:Label ID="companyLabel" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="GroupIDINLabel" runat="server" Visible="false"></asp:Label>
        <table >
        <tr style="visibility:hidden">
            <td align="right">รหัส : </td>
            <td><asp:Label ID="codeLabel" runat="server" Text=""></asp:Label><asp:Label ID="poscodeLabel" runat="server" Text="" Visible="false"></asp:Label></td>
        </tr>
        <tr>
            <td align="right">  ชื่อ : </td>
            <td><asp:Label ID="NameLabel" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td align="right">ตำแหน่ง : </td>
            <td><asp:Label ID="posnameLabel" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td align="right">  หน่วยงาน : </td>
            <td><asp:Label ID="unitnameLabel" runat="server" Text=""></asp:Label><asp:Label ID="unitcodeLabel" runat="server" Text="" Visible="false"></asp:Label></td>
        </tr>
        <tr>
            <td align="right">  โทร :</td>
            <td> <asp:TextBox ID="TelLabel" runat="server" Text=""></asp:TextBox></td> 
        </tr>
         <tr>
            <td align="right" valign="top"  >  กลุ่ม :</td>
            <td style="width:100%">
            <div  style="height:140px; overflow:auto">
               <asp:CheckBoxList id="cl" runat="server" ></asp:CheckBoxList>
             </div>
            </td> 
        </tr>
        </table>
       
    </asp:panel>
            <asp:panel ID="NoResult" Visible="false"  runat="server"  >
     <div class="span1 "></div>
   <asp:Label ID="errorlabel" runat="server" ForeColor="Red"> ไม่พบข้อมูล</asp:Label> 
    </asp:panel>
         <div class="modal-footer"> 
             <asp:Button ID="UpdatePPL" CssClass="btn btn " runat="server" Text="UPDATE" 
                       onclick="UpdatePPL_Click"   />
              <asp:Button ID="SavePPL" CssClass="btn btn " runat="server" Text="ADD" 
                   onclick="SavePPL_Click"  Enabled="false"  />
              <asp:Button ID="Button5" CssClass="btn btn " runat="server" Text="CANCEL" OnClick="ClosePanel_Click" />
         </div>
      </div>
   
    </asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>
