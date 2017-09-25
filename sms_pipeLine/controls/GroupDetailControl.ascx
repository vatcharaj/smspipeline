<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupDetailControl.ascx.cs" Inherits="sms_pipeLine.controls.GroupDetailControl" %>
 
    
     <%@ Register TagName="CS" TagPrefix="CC"  Src="~/controls/GroupView/CsControl.ascx"%>
     <%@ Register TagName="TCS" TagPrefix="CC"  Src="~/controls/GroupView/TCSControl.ascx" %>
     <%@ Register TagName="GC" TagPrefix="CC"  Src="~/controls/GroupView/GCControl.ascx" %>
     <%@ Register TagName="TEST" TagPrefix="CC"  Src="~/controls/GroupView/TestControl.ascx" %>
     <%@ Register TagName="PTT" TagPrefix="CC"  Src="~/controls/GroupView/PTTControl.ascx" %>
     <%@ Register TagName="PTTB" TagPrefix="CC"  Src="~/controls/GroupView/PTTBControl.ascx" %>
     <%@ Register TagName="CSPPL" TagPrefix="CC"  Src="~/controls/PPL/CSPPLControl.ascx" %>
    <%@ Register TagName="TCSPPL" TagPrefix="CC"  Src="~/controls/PPL/TCSPPLControl.ascx" %>
    <%@ Register TagName="GCPPL" TagPrefix="CC"  Src="~/controls/PPL/GCPPLControl.ascx" %>
    <%@ Register TagName="TESTPPL" TagPrefix="CC"  Src="~/controls/PPL/TESTPPLControl.ascx" %>
        <%@ Register TagName="PTTPPL" TagPrefix="CC"  Src="~/controls/PPL/PTTPPLControl.ascx" %>
             <%@ Register TagName="PTTBPPL" TagPrefix="CC"  Src="~/controls/PPL/PTTBPPLControl.ascx" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
<%-- <h4><u>ระบบจัดการผู้รับ</u></h4>--%>
 
        <%--   <div class="input-append " style="float:right">
                <asp:Button ID="AddgroupBtn" runat="server" 
                    CssClass="btn btn-default btn-small"  Text="เพิ่มกลุ่ม" 
                    onclick="AddgroupBtn_Click"/>
                <asp:Button ID="AddpplBtn" runat="server" CssClass="btn btn-default btn-small" 
                    Text="เพิ่มบุคคล" onclick="AddpplBtn_Click" />
                <asp:Button ID="DelGroup" runat="server" CssClass="btn btn-default btn-small" 
                    Text="ลบกลุ่ม" OnClientClick="javascript:return ConfirmDel();" onclick="DelGroup_Click" />
            </div>--%>


  <table width="100%" class="recptable">
  <tr>
  <td class="recptable tree">
  
         
   
   <div class="scrollbar" id="ex3" ><asp:LinkButton ID="RefreshLink" OnClick="RefreshLink_Click"
    CssClass="btn btn-primary" runat="server" style="float:right">
   <i class="icon-refresh icon-white"></i>
   </asp:LinkButton>
   <asp:TreeView ID="TreeView1" ExpandDepth="1"  runat="server" 
           onselectednodechanged="TreeView1_SelectedNodeChanged" >
  </asp:TreeView>
  </div>
  </td>
  <td class="recptable detailRci">
  <div >

   กลุ่ม : <asp:Label ID="GROUPLabel" runat="server" Text="-"></asp:Label>
     <CC:CS ID="CS" runat="server" Visible="false" />
     <CC:TCS ID="TCS" runat="server" Visible="false" />
     <CC:GC ID="GC" runat="server" Visible="false" />
     <CC:TEST ID="TEST" runat="server" Visible="false" />
        <CC:PTT ID="PTT" runat="server" Visible="false" />
            <CC:PTTB ID="PTTB" runat="server" Visible="false" />
<br />
 บุคคล : <asp:Label ID="PPLLabel" runat="server" Text="-"></asp:Label> <div style="float:right">จำนวน :<asp:Label ID="CountPPL" runat="server" Text=""></asp:Label> คน</div>
  <CC:CSPPL ID="CSPPL" runat="server" Visible="false" />
         <CC:TCSPPL ID="TCSPPL" runat="server" Visible="false" />
     <CC:GCPPL ID="GCPPL" runat="server" Visible="false" />
     <CC:TESTPPL ID="TESTPPL" runat="server" Visible="false" />
      <CC:PTTPPL ID="PTTPPL" runat="server" Visible="false" />
        <CC:PTTBPPL ID="PTTBPPL" runat="server" Visible="false" />
  </div>

  </td>
  </tr>
  </table>


    </ContentTemplate>
 </asp:UpdatePanel>
<%--  ---------------------------  ADD PPL---------------------------%>
   <asp:Panel ID="AddPPLPanel" runat="server" Visible="false">
   
     <div class="grpPanel">
     <div class="modal-header">
                    <asp:Button CssClass="close btn-small" data-dismiss="modal" aria-hidden="true" runat="server" ID="Button1" 
                                            Text="x" ></asp:Button >
                 
                        <h5>  กลุ่ม :  <asp:Label ID="GroupPPLPanel" runat="server" Text="Label"></asp:Label> </h5><br /> 
                        <asp:Label ID="Label1" runat="server"  CssClass="span2 " style="text-align:right">ค้นหาบุคคล : </asp:Label>
                        <div class="input-append">
                         <asp:TextBox ID="SchPPLBox" CssClass="span2" runat="server"></asp:TextBox> 
                         <asp:DropDownList ID="PPLDepartList" CssClass="span2" runat="server" >
                        </asp:DropDownList>
                        <asp:Button ID="SchPPLbtn" CssClass="btn btn" runat="server" Text="ค้นหา" 
                                onclick="SchPPLbtn_Click" />
                        </div>
     </div>
     <asp:panel ID="resultppl"  runat="server"  >
     <div class="span1 "></div>
           <table>
           <tr>
           <td>
         <asp:ListBox ID="ResultListBox"  
         CssClass="span4 listBoxH" SelectionMode="Multiple" runat="server"  ></asp:ListBox>
         </td>
         <td>
          <asp:Button ID="Add2Final" runat="server" Text=">>" onclick="Add2Final_Click" /><br />
            <asp:Button ID="RemoveFinal" runat="server" Text="<<" 
                 onclick="RemoveFinal_Click" />
            </td>
            <td>
         <asp:ListBox ID="FinalListBox" SelectionMode="Multiple"   CssClass="span4 listBoxH" 
                    runat="server" ></asp:ListBox>
         </td>
        </tr>
       </table>
    </asp:panel>
     <div class="modal-footer">
          <asp:Button ID="SavePPL" CssClass="btn btn " runat="server" Text="SAVE"   />
          <asp:Button ID="Button5" CssClass="btn btn " runat="server" Text="CANCEL"  />
     </div>
      </div>
   
    </asp:Panel>
   <script type="text/javascript">
       function ConfirmDel() {
           var Ok = confirm('Are you sure want to DELETE this group?');
           if (Ok)
               return true;
           else
               return false;
       }
   


 </script>

 