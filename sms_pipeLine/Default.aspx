<%@ Page Title="ระบบส่ง SMS แจ้งเตือนเหตุการณ์ตามแนวท่อ" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="sms_pipeLine._Default"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" ClientIDMode="Static" runat="server" ContentPlaceHolderID="MainContent">

    <asp:ScriptManager ID="ScriptManager1" runat="server" >
 </asp:ScriptManager>
 <div >
  <div  class="row" style="width: 700px;margin:0 auto; ">
    <h4>สร้างข้อความและเลือกผู้รับ</h4>

<%--<asp:Label ID="FieldLabel" Text="Label:" runat="server"></asp:Label>--%>

    <br /><asp:DropDownList ID="GroupList" runat="server" Width="200px" Visible="false"  ></asp:DropDownList>
  <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
    <ContentTemplate>

  <table>
    <tr valign="top">
        <td align="right" style="width:200px">
        ค้นหา :
        </td><td>
        <div class="input-append" >
                               <asp:TextBox  style="border-right:none" ID="GroupTosend" runat="server" onclick="return false;"
                                        ClientIDMode="Static"  Width="430px"  onkeypress="autocomplete()"   ontextchanged="GroupTosend_TextChanged"    ></asp:TextBox>
                       
                                 <asp:Button CssClass="btn btn" runat="server" ID="Tobtn" Text="ค้นหา" 
                                  onclick="Tobtn_Click" />
                                 <asp:Label Width="30px" ID="none" runat="server" Visible="true"></asp:Label>
                         
         </div> 
         </td>
     </tr>

     <tr valign="top">
         <td align="right" style="width:200px">
         เลื่อกกลุ่มผู้รับ :
         </td>
        <td>
         <asp:DropDownList ID="GroupList_selectall"   onselectedindexchanged="GroupList_selectall_SelectedIndexChanged"
          runat="server" Width="500px" AutoPostBack="true" ></asp:DropDownList>
        </td>
      </tr>
       <tr valign="top">
         <td align="right" style="width:200px">
         เลื่อกข้อความที่สร้าง :
         </td>
        <td>
           <asp:DropDownList ID="TemplList" runat="server" Width="500px"  AutoPostBack="true"
            onselectedindexchanged="TemplList_SelectedIndexChanged"></asp:DropDownList>
        </td>
        </tr>
         <tr valign="top">
         <td align="right" style="width:200px">
         ข้อความ :
         </td>
        <td>
            <asp:TextBox TextMode="MultiLine" Height="180px" Width="500px" runat="server"  
                ID="MSGTEXT"   onkeypress="return isAvailableKey(event)"   
                AutoPostBack="true"   ClientIDMode="Static" 
                ontextchanged="MSGTEXT_TextChanged"  ></asp:TextBox>
   

           </td>
         </tr>
  
    
     </table>
       
       <div class="pull-left" style="margin-left:170px">
      Text Count :   <asp:Label ID="TextCount" ClientIDMode="Static" runat="server" Text="0"></asp:Label>/320
    
      </div>
    
            <div class="input-append pull-right"  style="margin-right:20px"> 
         
                <asp:Button ID="SendBtn" runat="server" CssClass="btn btn-default" Text="SEND" 
                    onclick="SendBtn_Click"  />
                <asp:Button ID="PreviewBtn" runat="server" CssClass="btn btn-default" 
                    Text="PREVIEW" onclick="PreviewBtn_Click" />
                <asp:Button ID="CancelBtn" runat="server" CssClass="btn btn-default" 
                    Text="CANCEL" onclick="CancelBtn_Click" />
            </div>
            <br />
            <br />
            <asp:Panel ID="listReci" runat="server" Visible="false">
           
       
            <table>
            <tr valign="top">
            <td align="right" style="width:200px">    </td>
            <td style="width:500px"> <asp:CheckBox ID="SenderCheck" Checked="true" ClientIDMode="Static" TextAlign="Right" runat="server" />
        <label id="SenderCheckLabel">From GasControl</label></td>
            </tr>
            <tr valign="top">
            <td align="right" style="width:200px">
            รายชื่อผู้รับ :
            </td>
            <td  style="width:500px" >
                <asp:GridView ID="listReciGrid" runat="server"
                 Font-Size="11px" OnRowCommand="listReciGrid_RowCommand"
                AutoGenerateColumns="false" Width="100%"  >
                 <HeaderStyle BackColor="#DCDCDC" />
                  <AlternatingRowStyle BackColor="#F5F5F5" />
                <Columns>
                <asp:BoundField  HeaderText="GROUP ID" DataField="GROUP_ID"   />  
                <asp:BoundField  HeaderText="GROUP NAME" DataField="GROUP_NAME"    />  
                <asp:BoundField  HeaderText="DEPARTMENT" DataField="DEPARTMENT"    />
                 <asp:BoundField  HeaderText="DEPARTMENT_ID" DataField="DEPARTMENT_ID" Visible="false"    />
             <%--   <asp:BoundField  HeaderText="NAME" DataField="NAME"     />
                <asp:BoundField  HeaderText="PHONE" DataField="PHONE"  />--%>
              <%--  <asp:ButtonField ButtonType="Button"  CommandName="DelPPL" 
                 ControlStyle-CssClass="btn-small" HeaderText="DELETE" Text="x"   />--%>

                    <asp:TemplateField  >
                    <ItemStyle  HorizontalAlign="Center"  />
                    <ItemTemplate>
                    <asp:ImageButton ID="Button2" 
                     runat="server"  CommandName="DelPPL" 
                       CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                        ImageUrl="~/Content/img/small_remove.png" />
                   
                    </ItemTemplate>
                </asp:TemplateField>
                </Columns>
               </asp:GridView>
               </td></tr>
             </table>
           </asp:Panel>

   <asp:Panel ID="grpPanel" CssClass="dimmer"  runat="server" Visible="false" ClientIDMode="Static"></asp:Panel>
    <asp:Panel ID="DetailPanel" runat="server" Visible="false" ScrollBars="Vertical">
      <div class="grpPanel">
     <div class="modal-header">
                    <asp:Button CssClass="close btn-small" data-dismiss="modal" aria-hidden="true" runat="server" ID="ClosePanel" 
                                            Text="x" onclick="ClosePanel_Click"></asp:Button >
                    <h5>เลือกกลุ่มหรือรายชื่อผู้รับ : <asp:Label ID="productTankName" runat="server"></asp:Label></h5>
                 
       <%--             <div class="pull-right">
                <asp:Button ID="Button4" CssClass="btn btn " runat="server" Text="SELECT" onclick="SELECTGrp_Click"  />
               <asp:Button ID="Button5" CssClass="btn btn " runat="server" Text="CANCEL" OnClick="ClosePanel_Click" />
                 </div>--%>
                
          </div>

           <div  style="margin:13px; max-height:300px; overflow:auto"">
           <asp:GridView ID="GroupResultGrid"  DataKeyNames="GROUP_ID" Width="100%" 
            runat="server" AutoGenerateColumns="false" Font-Size="11px"  >
               <HeaderStyle BackColor="#DCDCDC" />
             <AlternatingRowStyle BackColor="#F5F5F5" />
           <Columns>
                <asp:BoundField   HeaderText="GROUP ID" DataField="GROUP_ID"      />  
                <asp:BoundField  HeaderText="GROUP NAME" DataField="GROUP_NAME"    />  
                <asp:BoundField  HeaderText="DEPARTMENT" DataField="DEPARTMENT"     />
               <%--  <asp:BoundField  HeaderText="NAME" DataField="NAME"     />
                <asp:BoundField  HeaderText="DESCRIPTION" DataField="DESCR"     />--%>
                   <asp:TemplateField HeaderText="SELECT"  >
                    <ItemStyle Width="20px" HorizontalAlign="Center"  />
                    <HeaderTemplate>
                        <asp:CheckBox runat="server" ID="chkSelectedAll" OnCheckedChanged="chkSelectedAll_CheckedChanged"
                          AutoPostBack="true"   />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox runat="server" ID="chkSelected" OnCheckedChanged="chkSelected_CheckedChanged"
                          AutoPostBack="true"   />
                    </ItemTemplate>
                </asp:TemplateField>
           </Columns>
          </asp:GridView>
           </div>
           <div class="modal-footer">
          <asp:Button ID="SELECTGrp" CssClass="btn btn " runat="server" Text="SELECT" 
                   onclick="SELECTGrp_Click" />
          <asp:Button ID="Button3" CssClass="btn btn " runat="server" Text="CANCEL" OnClick="ClosePanel_Click" />
     </div>
      </div>
    </asp:Panel>


    <asp:Panel ID="PreviewPanel" runat="server" Visible="false">
      <div class="grpPanel">
     <div class="modal-header">
                    <asp:Button CssClass="close btn-small" data-dismiss="modal" aria-hidden="true" runat="server" ID="ClosePreview" 
                                            Text="x" onclick="ClosePanel_Click"></asp:Button >
                    <h5>ข้อความที่จะส่ง : </h5>
     </div>
     <div  style="margin:13px; max-height:300px; overflow:auto">
          <asp:PlaceHolder ID="SMSHolder" runat="server"></asp:PlaceHolder>
    </div>
      </div>
    </asp:Panel>

  <asp:Panel ID="PreviewSendPanel" runat="server" Visible="false">
      <div class="grpPanel">
     <div class="modal-header">
                    <asp:Button CssClass="close btn-small" data-dismiss="modal" aria-hidden="true" runat="server" ID="Button1" 
                                            Text="x" onclick="ClosePanel_Click"></asp:Button >
                    <h5>ตรวจสอบข้อมูล : </h5>
     </div>
     <div  style="margin:13px; max-height:300px; overflow:auto">
     <table style="width:80%">
     <tr valign="top"><td style="width:80px" >  ข้อความ : </td>
         <td><asp:Label ID="MSGLabel" runat="server" Text=""></asp:Label></td>
       </tr>
        <tr>
         <td style=" height:20px"> </td> <td> </td>
       </tr>
       <tr  valign="top"><td style="width:80px"> กลุ่ม : </td>
      
       <td>  
         <asp:GridView ID="ListGrpChk" runat="server"
                 Font-Size="11px" 
                AutoGenerateColumns="false" Width="100%"  >
                 <HeaderStyle BackColor="#DCDCDC" />
                  <AlternatingRowStyle BackColor="#F5F5F5" />
                <Columns>
                <asp:BoundField  HeaderText="GROUP ID" DataField="GROUP_ID"   />  
                <asp:BoundField  HeaderText="GROUP NAME" DataField="GROUP_NAME"    />  
                <asp:BoundField  HeaderText="DEPARTMENT" DataField="DEPARTMENT"    />
                 <asp:BoundField  HeaderText="DEPARTMENT_ID" DataField="DEPARTMENT_ID" Visible="false"    />
                </Columns>
               </asp:GridView>
       </td>
        </tr> 
       </table>  
    </div> 
     <div class="modal-footer">
          <asp:Button ID="SndFinal" CssClass="btn btn " runat="server" Text="OK" 
              onclick="SndFinal_Click"    />
          <asp:Button ID="Button5" CssClass="btn btn " runat="server" Text="CANCEL" OnClick="ClosePanel_Click" />
     </div>
      </div>
    </asp:Panel>




        <asp:UpdateProgress id="updateProgress" runat="server">
         <ProgressTemplate>
                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                      <span style="border-width: 0px; position: fixed; padding: 50px; background-color: #000000; font-size: 20px; left: 40%; top: 40%; color:White">Sending ...</span>
                </div>
         </ProgressTemplate>
    </asp:UpdateProgress>


       </ContentTemplate>
    </asp:UpdatePanel>
    </div>
  
</div>

 <link href="./Scripts/jquery/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="./Scripts/jquery/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="./Scripts/jquery/jquery-ui.js" type="text/javascript"></script>
 <script type="text/javascript">

      

      a = <%= this.javaSerial.Serialize(this.names) %>;

    $(document).ready(function () {
  
    
    if(a !=null){
                $("#GroupTosend").autocomplete({
                    source: a
                });
               
            }

    });
    function autocomplete()
    {
    
    if(a !=null){
                $("#GroupTosend").autocomplete({
                    source: a
                });
               
            }
    }
      function isAvailableKey(evt) {
          var lbl =  document.getElementById("MSGTEXT");
         var charCode = lbl.innerHTML.length; 
          var lbl2 =  document.getElementById("TextCount");
         lbl2.innerHTML =charCode;
          if (charCode >319)
              return false;

          return true;
      }

   
//      function showIt() {
//  document.getElementById("grpPanel").style.visibility = "visible";
//        }
//      function closeIt() {
//  document.getElementById("grpPanel").style.visibility = "hidden";
//        }
//      
 </script>
 
</asp:Content>