<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportControl.ascx.cs" Inherits="sms_pipeLine.controls.ReportControl" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:UpdatePanel ID="PPL_PANEL" runat="server"   >
      <ContentTemplate>
       <div  style="margin: auto; width:600px; ">
    <h4><u>รายงาน</u></h4>
  <div  style="border:1px solid #f2f2f2; ">
   <table>
   <tr><td>&nbsp&nbsp</td><td>&nbsp&nbsp</td><td>&nbsp&nbsp</td></tr>
   <tr>
          <td align="right"> <span>รายงาน :  </span> </td><td>&nbsp&nbsp</td><td> 
          <asp:DropDownList ID="ReportList"  runat="server"  AutoPostBack="true"
              onselectedindexchanged="ReportList_SelectedIndexChanged"></asp:DropDownList></td>
   </tr>
    
    
      <tr>
          <td align="right">       <span> กลุ่ม :  </span></td><td>&nbsp&nbsp</td>
       <td>
                  <asp:DropDownList ID="GroupList" runat="server">
                  </asp:DropDownList>
              </td>
          
   </tr>
    
    
     
      <tr>
          <td align="right">      <span> ตั้งแต่ :  </span></td><td>&nbsp&nbsp</td><td><asp:Label ID="startLabel" runat="server" Text="Label">
            </asp:Label>&nbsp<asp:ImageButton ID="StaBtn" runat="server" 
              ImageUrl="~/Content/img/plan-blue-calendar.png" Width="22px" Height="22px" 
              onclick="StaBtn_Click"  />
     <asp:Calendar ID="staCalendar" runat="server" Visible="false" 
              style="z-index:9; position:absolute; background-color:White" 
              onselectionchanged="staCalendar_SelectionChanged"  ></asp:Calendar>
  <asp:DropDownList ID="StaTime" runat="server" style=" width:70px; padding: 0px 0px; height:25px">
          </asp:DropDownList>
              </td>
        
   </tr>
   
      
     <tr>
          <td align="right">       <span>ถึง :  </span></td><td>&nbsp&nbsp</td><td><asp:Label ID="endLabel" runat="server" Text="Label">
            </asp:Label>&nbsp<asp:ImageButton ID="EndBtn" runat="server" 
              ImageUrl="~/Content/img/plan-blue-calendar.png" Width="22px" Height="22px" 
              onclick="EndBtn_Click" />
     <asp:Calendar ID="endCalendar" runat="server" Visible="false" 
              style="z-index:9; position:absolute; background-color:White" 
              onselectionchanged="endCalendar_SelectionChanged" ></asp:Calendar>
               <asp:DropDownList ID="EndTime" runat="server" style=" width:70px; padding: 0px 0px; height:25px">
          </asp:DropDownList>
    </td>
   </tr>
     
   
   <tr>
          <td align="right">   </td><td>&nbsp&nbsp</td><td> 
          <asp:Button ID="GenReport" runat="server" Text="รายงาน" 
              onclick="GenReport_Click" CssClass="btn btn-info"  />
                 <asp:Button ID="CancelBtn" runat="server" Text="ยกเลิก" 
              CssClass="btn btn-primary" onclick="CancelBtn_Click"  />
        <%-- <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Content/img/word.png" Width="25px" Height="25px" />--%>
          <%--<asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Content/img/excel.png" Width="25px" Height="25px" />--%>
        <%--   <asp:ImageButton ID="ImageButton3" runat="server"  ImageUrl="~/Content/img/pdf.png" Width="25px" Height="25px" />
--%>
          </td>
       
   </tr>

    </table>
    
      </div>
      <asp:Label ID="errorLabel" runat="server" Text="*กรุณาเลือกชนิดรายงาน" Visible="false" ForeColor="Red"></asp:Label>
    

 
        

  </div>


  <asp:Panel ID="grpPanel" CssClass="dimmer"  runat="server" Visible="false"></asp:Panel>
   <asp:Panel ID="reportPanel" runat="server" Visible="false"  >
     <div class="reportresultpanel">
          <div class="modal-header">
            <asp:Button CssClass="close btn-small" data-dismiss="modal" aria-hidden="true" runat="server" ID="Button1" 
            Text="x" onclick="ClosePanel_Click"></asp:Button > 
             <asp:ImageButton ID="ExcelBtn"  runat="server" 
                  ImageUrl="~/Content/img/excel.png" Width="25px" Height="25px" 
                  style=" margin-right:10px" onclick="ExcelBtn_Click"  />
             
            <asp:Label ID="ReportNameLabel" runat="server"   style="text-align:right; font-weight:bold">XXXXXXXX  </asp:Label>
             
              <br />
          </div>
          <div style="margin:12px; max-height:450px; overflow:auto">
        
              <asp:GridView ID="ReportResultView" runat="server" 
               Width="100%" AllowSorting="true"
               AllowPaging="true" 
               OnSorting="ReportResultView_Sorting" 
               OnPageIndexChanging="ReportResultView_PageIndexChanging" 
               PageSize="20"
                 onrowcommand="ReportResultView_RowCommand"
              AutoGenerateColumns="false">
               <PagerSettings  Mode="NumericFirstLast"    
               FirstPageText="First" PreviousPageText="Previous" 
               NextPageText="Next" LastPageText="Last" /> 
                 <HeaderStyle BackColor="#DCDCDC" />
                  <AlternatingRowStyle BackColor="#F5F5F5" />
              <Columns>
             <%--  <asp:BoundField  HeaderText="" DataField="LOG_ID" SortExpression="LOG_ID" Visible="false"   /> --%>
             <asp:TemplateField  Visible="false" >
              <HeaderTemplate>LOG ID</HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LOGIDLabel" runat="server" Text= '<%#Eval("LOGID")%>' ></asp:Label>
                    </ItemTemplate>
               </asp:TemplateField>
              
                    <asp:BoundField  HeaderText="SEND DATE" DataField="SEND_DATE_STR" SortExpression="SEND_DATE"  HtmlEncode="false"   />  
                    <asp:BoundField  HeaderText="GROUP" DataField="GROUP_NAME"  SortExpression="GROUP_NAME"  /> 
                     <asp:BoundField  HeaderText="DEPARTMENT" DataField="DEPARTMENT"  SortExpression="DEPARTMENT"  ItemStyle-HorizontalAlign="Center" /> 
                 
                  <%--  <asp:BoundField  HeaderText="MOBILE" DataField="MOBILE"  SortExpression="MOBILE"  />  --%>
                       <asp:TemplateField  >
                    <HeaderTemplate>MOBILE</HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="MOBILELabel" runat="server" Text= '<%#Eval("MOBILE")%>' ></asp:Label>
                        <asp:TextBox ID="MOBILETextBox" runat="server" Text= '<%#Eval("MOBILE")%>' CssClass="span2 input-small" Visible="false"></asp:TextBox>
                    </ItemTemplate>
                    </asp:TemplateField>  
                      <asp:BoundField  HeaderText="SENDER" DataField="SENDER" SortExpression="SENDER"   /> 
                      <asp:BoundField  HeaderText="STATUS" DataField="STATUS" SortExpression="STATUS"  ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField  HeaderText="NOTE" DataField="SEND_NOTE" SortExpression="SEND_NOTE"   /> 
                        <asp:BoundField  HeaderText="NAME" DataField="NAME"  SortExpression="NAME"  />
                   <asp:BoundField  HeaderText="COMPANY" DataField="COMPANY" SortExpression="COMPANY"    />
                    <asp:BoundField  HeaderText="MESSAGE" DataField="MESSAGE" SortExpression="MESSAGE"  ItemStyle-Wrap="true" />
                 
                   
                
                <%--    <asp:BoundField  HeaderText="RESENT DATE" DataField="RESEND_DATE" SortExpression="RESEND_DATE" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" HtmlEncode="false"   />--%>
                   <%--  <asp:BoundField  HeaderText="ATTEMPT" DataField="ATTEMPT" SortExpression="ATTEMPT"   ItemStyle-HorizontalAlign="Center"  />--%>
                  <%--     <asp:ButtonField ButtonType="Link" CommandName="EdtMobile"  ControlStyle-CssClass="btn-small"  Text="Edit Mobile"   />--%>
                 
                    
                    
                      <asp:TemplateField    ItemStyle-HorizontalAlign="Center" ShowHeader="False"   FooterStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="EditLB" runat="server" OnClick="EditLB_Click">Edit</asp:LinkButton>
                                <asp:LinkButton ID="SaveLB" runat="server" Visible="false" OnClick="SaveLB_Click">Save</asp:LinkButton>
                               <asp:LinkButton ID="CancelLB" runat="server" Visible="false" OnClick="CancelLB_Click">Cancel</asp:LinkButton>
                            </ItemTemplate>
                    </asp:TemplateField>


                 <asp:ButtonField ButtonType="Link" CommandName="resend"  ItemStyle-HorizontalAlign="Center"  Text="Resend"   />
                <%--   <asp:TemplateField>
                   <ItemTemplate>
                       <asp:Button ID="RESENDbtn" runat="server" Text="RESEND"  CommandName="resend" />
                   </ItemTemplate>
                   </asp:TemplateField>--%>
              </Columns>
              </asp:GridView>

              <asp:Chart ID="SumChart" runat="server" style="margin-left:35%" >
                  <Series>
                  </Series>
                    <legends>   
                     <asp:Legend Alignment="Center" Docking="Bottom"     
                                IsTextAutoFit="False" Name="Default"            
                                    LegendStyle="Row" />
                                      </legends>               
                  <ChartAreas>
                      <asp:ChartArea Name="ChartArea1">
                      </asp:ChartArea>
                  </ChartAreas>
              </asp:Chart>

              <asp:Label ID="NodataLabel" runat="server" Text="* NO DATA" ForeColor="Red" Visible="false"></asp:Label>
              <br />
          </div>
    </div>
   </asp:Panel>
            
        <asp:UpdateProgress id="updateProgress" runat="server">
         <ProgressTemplate>
                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                      <span style="border-width: 0px; position: fixed; padding: 50px; background-color: #000000; font-size: 20px; left: 40%; top: 40%; color:White">Processing ...</span>
                </div>
         </ProgressTemplate>
    </asp:UpdateProgress>
      </ContentTemplate>
 </asp:UpdatePanel>
 <div style="margin: auto; width:600px; ">
 <br />
   <h4><u>รายงานประจำเดือน</u></h4>
  <div  style="border:1px solid #f2f2f2; ">
      <asp:GridView ID="MonthlyList" BorderStyle="None" 
         onrowcommand="MonthlyList_RowCommand"
       GridLines="None" 
       runat="server" AutoGenerateColumns="false">
        <Columns>
          <asp:BoundField ItemStyle-Width="120px"  DataField="FILEMMYYYY"   /> 
          <asp:BoundField    DataField="LASTUPDATE"   /> 
           <asp:TemplateField > 
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="LdReport"
                                  Text="Load" CommandName="LdReport_CMD"
                                    ImageUrl="~/Content/img/saveicon.png"
                                     Width="20px" Height="20px"
                                  CommandArgument='<%#Eval("PATHFILE") %>' ></asp:ImageButton>
                            </ItemTemplate>
                 </asp:TemplateField>
         

          <asp:BoundField  HeaderText="Path"  DataField="PATHFILE"  Visible="false"  /> 
          </Columns>
       </asp:GridView>
   </div>
   </div>