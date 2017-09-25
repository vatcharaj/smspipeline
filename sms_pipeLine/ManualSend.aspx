<%@ Page Title="Manual Send" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ManualSend.aspx.cs" Inherits="sms_pipeLine.ManualSend" %>
  

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager1" runat="server" >
 </asp:ScriptManager>
 <div  class="row" style="width: 700px;margin:0 auto; ">
    <h4>สร้างข้อความและระบุผู้รับ</h4>

<%--<asp:Label ID="FieldLabel" Text="Label:" runat="server"></asp:Label>--%>

    <br />
  
  <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
    <ContentTemplate>

  <table>
    <tr valign="top">
        <td align="right" style="width:200px">
        หมายเลข :
        </td><td>
       
        <asp:TextBox  ID="GroupTosend" runat="server" onclick="return false;"
                ClientIDMode="Static"  Width="500px"       ></asp:TextBox>
                                      <br />
            <asp:Label ID="Label1" Font-Italic="true" ForeColor="Red" runat="server" Text="*Split Each Mobile Number with ' , ' "></asp:Label>
         </td>
     </tr>

   
       <tr valign="top">
         <td align="right" style="width:200px">
         เลื่อกข้อความที่สร้าง :
         </td>
        <td>
           <asp:DropDownList ID="TemplList" runat="server" Width="512px"  AutoPostBack="true"
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
       
     <div>
 <div class="pull-left" >
      Text Count :   <asp:Label ID="TextCount" ClientIDMode="Static" runat="server" Text="0"></asp:Label>/320
    
      </div>
    
            <div class="input-append pull-right"  style="margin-right:20px"> 
         
                <asp:Button ID="SendBtn" runat="server" CssClass="btn btn-default" Text="SEND" onclick="SendBtn_Click" 
                      />
                <asp:Button ID="PreviewBtn" runat="server" CssClass="btn btn-default" 
                    Text="PREVIEW" onclick="PreviewBtn_Click" />
                <asp:Button ID="CancelBtn" runat="server" CssClass="btn btn-default" 
                    Text="CANCEL" onclick="CancelBtn_Click" />
            </div>  </div>
            <br />
             <asp:CheckBox ID="SenderCheck" Checked="true" ClientIDMode="Static" TextAlign="Right" runat="server" />
             <label id="SenderCheckLabel">From GasControl</label>
           </td>
         </tr>
  
    
     </table>
       
      
            <br />
            <br />
        

   <asp:Panel ID="grpPanel" CssClass="dimmer"  runat="server" Visible="false" ClientIDMode="Static"></asp:Panel>
    


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


<link href="./Scripts/jquery/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="./Scripts/jquery/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="./Scripts/jquery/jquery-ui.js" type="text/javascript"></script>
 <script type="text/javascript">

      

      function isAvailableKey(evt) {
          var lbl =  document.getElementById("MSGTEXT");
         var charCode = lbl.innerHTML.length; 
          var lbl2 =  document.getElementById("TextCount");
         lbl2.innerHTML =charCode;
          if (charCode >319)
              return false;

          return true;
      }

   

 </script>
</asp:Content>
