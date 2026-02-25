<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="WeeklyReport.aspx.cs" Inherits="LMS.Admin.WeeklyReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="HourlyJob">
     <table class="table table-condensed" style="margin-top: 10px;">
                <thead>
                    <tr style="background-color: #383838; color: white;">
                        <th>Sr#</th>
                      <%--  <th>Employee</th>--%>
                         <th>Job Title</th>
                          <th>Normal Hours</th>
                             <th>Overtime Hours</th>
                          <th>Total Amount</th>
                    </tr>
                </thead>
            <tbody>
    <asp:ListView ID="LV" runat="server">

       
          
           
         
        <ItemTemplate>
            <tr>
                <td>
                    <asp:Label ID="SrNo" runat="server" Text='<%#Container.DataItemIndex + 1%> '></asp:Label>
                </td>

             <%--   <td>
                    <asp:Label ID="Name" runat="server" Text='<%#Eval("Employee.Name")%>'></asp:Label>
                </td>--%>
                   <td>
                    <asp:Label ID="Label10" runat="server" Text='<%#Eval("JobTitle")%>'></asp:Label>
                </td>
                   <td>
                    <asp:Label ID="Label11" runat="server" Text='<%#Eval("NormalHours ")%>'></asp:Label>
                </td>
                
                  <td>
                    <asp:Label ID="Label14" runat="server" Text='<%#Eval("OvertimeHours ")%>'></asp:Label>
                </td>
                  <td>
                    <asp:Label ID="Label13" runat="server" Text='<%#Eval("TotalAmount ")%>'></asp:Label>
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
                </tbody>
          </table>
    </section>
</asp:Content>
