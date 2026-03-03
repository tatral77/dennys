<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ManageRestaurantWeeks.aspx.cs" Inherits="LMS.Admin.ManageRestaurantWeeks" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row" style="padding-bottom: 5px">
        <div class="col-md-6" style="background-color:darkred;color:white"><span style="font-size: 18pt" runat="server" id="LocationDetail"></span></div>
          <div class="col-md-2" style="text-align: right">
            <asp:TextBox ID="YearTxt" runat="server" CssClass="form-control" type="number"/>
          
          </div>
        <div class="col-md-1" style="text-align: right">
            <asp:Button ID="GenerateWeeksBtn" runat="server" CssClass="btn btn-success" Text="GenerateWeeks" OnClick="GenerateWeeksBtn_Click" /></div>
    </div>
    <asp:ListView ID="LV"  ItemPlaceholderID="itemPlaceHolder" runat="server">
        <LayoutTemplate>
            <table class="table table-condensed" style="margin-top: 10px;">
                <thead>
                    <tr style="background-color: #383838; color: white;">
                        <th>Sr#</th>
                         <th>Week</th>
                        <th>Year</th>
                        <th>Week Start Date</th>
                         <th>Week End Date</th>
                    </tr>
                </thead>
                   <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
               
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <asp:HiddenField ID="HidId" runat="server" Value='<%#Eval("Id")%>' />
                <td>
                    <asp:Label ID="SrNo" runat="server" Text='<%#Container.DataItemIndex + 1%> '></asp:Label>
                </td>
                  <td>
                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("WeekDecription")%>'></asp:Label>
                </td>
                  <td>
                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("Year")%>'></asp:Label>
                </td>
                   <td>
                    <asp:Label ID="Label10" runat="server" Text='<%#Convert.ToDateTime(Eval("WeekStartDate")).ToString("dddd, dd MMMM yyyy hh:mm tt")%>'></asp:Label>
                </td>
                   <td>
                    <asp:Label ID="Label11" runat="server" Text='<%#Convert.ToDateTime(Eval("WeekEndDate")).ToString("dddd, dd MMMM yyyy hh:mm tt")%>'></asp:Label>
                </td>
              
            </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            <tr>
                <td colspan="4">
                    <p>No Data Found</p>
                </td>
            </tr>
        </EmptyDataTemplate>
    </asp:ListView>
</asp:Content>
