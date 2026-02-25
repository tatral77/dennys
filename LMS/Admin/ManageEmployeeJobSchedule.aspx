<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ManageEmployeeJobSchedule.aspx.cs" Inherits="LMS.Admin.ManageEmployeeJobSchedule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .group{
            background-color:darkgrey;
            font-weight:bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row" style="padding-bottom: 5px">
        <div class="col-md-6" style="background-color: darkred; color: white"><span style="font-size: 14pt" runat="server" id="EmployeeDetail">Job Schedule</span></div>
         <div class="col-md-6" style="background-color: darkred; color: white;text-align:right"><span style="font-size: 14pt" runat="server" id="Budget"></span></div>
       <%-- <div class="col-md-1" style="text-align: right">
            <asp:Button ID="Button1" runat="server" CssClass="btn btn-success" Text="+" OnClick="Button1_Click" />
        </div>--%>
    </div>
    <div>
        <div class="row" style="margin-bottom:5px">
            <div class="col-md-2">Employee Job</div>
            <div class="col-md-10">
                <asp:DropDownList ID="JobTitleDDL" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="JobTitleDDL_SelectedIndexChanged">
                    <asp:ListItem Value="0">Select</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="row" style="margin-bottom:5px">
            <div class="col-md-2">Start Day</div>
            <div class="col-md-4">
                <asp:DropDownList ID="StartWeekDayDDL" class="form-control" runat="server">
                    <asp:ListItem Value="0">Select</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-2">Start Time</div>
            <div class="col-md-4">
                <asp:TextBox ID="StartTimeTxt" class="form-control control-txt" runat="server" Type="Time"></asp:TextBox>
            </div>
        </div>
        <div class="row" style="margin-bottom:5px">
            <div class="col-md-2">End Day</div>
            <div class="col-md-4">
                <asp:DropDownList ID="EndWeekDayDDL" class="form-control" runat="server">
                    <asp:ListItem Value="0">Select</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-2">End Time</div>
            <div class="col-md-4">
                <asp:TextBox ID="EndTimeTxt" class="form-control control-txt" runat="server"  Type="Time"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">  <asp:Button ID="SaveBtn" runat="server" Text="Add" OnClick="SaveBtn_Click" CssClass="btn btn-success" /></div>
        </div>
          <div class="row">
            <div class="col-md-12">  <asp:Label ID="ErrorLbl" runat="server" Text=""  style="color:#ffffff;background-color:red" /></div>
        </div>
    </div>
  <%-- <div class="row" style="background-color: #2258A2; padding-bottom: 10px; padding-left: 10px; color: white; border-radius: 5px">
        <h4>Search</h4>

        <div class="col-md-3" style="text-align: left;">
            <asp:Label ID="Label1" runat="server" Text="Name"></asp:Label><asp:TextBox ID="NameTxt" class="form-control control-txt" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-3" style="text-align: left;">
            <asp:Label ID="Label12" runat="server" Text="Job Title"></asp:Label><asp:TextBox ID="JobTitleTxt" class="form-control control-txt" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-2" style="text-align: left;">
            <asp:Label ID="Label6" runat="server" Text="Is Active"></asp:Label><asp:DropDownList ID="IsActiveDDL" class="form-control" runat="server">
                <asp:ListItem Value="-1">All</asp:ListItem>
                <asp:ListItem Value="1">Yes</asp:ListItem>
                <asp:ListItem Value="0">No</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-md-2">
            <br />
            <asp:Button ID="SearchBtn" runat="server" Text="Search" OnClick="SearchBtn_Click" CssClass="btn btn-success" />
            <asp:Button ID="ResetBtn" runat="server" Text="Reset" OnClick="ResetBtn_Click" CssClass="btn btn-info" />
        </div>
    </div>--%>
  <asp:ListView ID="lvEmployees" runat="server">
    <ItemTemplate>
        <div class="row" style="background-color:gray;color:white;padding:5px;margin-top:5px;padding-left:5px;font-weight:bold;font-size:12pt;">
            <div class="col-md-6"><%# Eval("EmployeeName") %></div>
            <div class="col-md-6" style="text-align:right">Weekly Total: <%# Eval("WeeklyTotal", "$" + "{0:N2}") %></div>
        </div>
     <%--   <div style="background-color:gray;color:white;padding:3px;margin-top:5px;padding-left:5px;"><h4><%# Eval("EmployeeName") %></h4 Weekly Total: <%# Eval("WeeklyTotal", "{0:N2}") %></div>--%>

        <asp:ListView ID="lvDays" runat="server" OnItemDeleting="LV_ItemDeleting" OnItemCommand="lvDays_ItemCommand"
            DataSource='<%# Eval("Items") %>'>

            <LayoutTemplate>
                <table class="table">
                    <tr>
                        <th>Sr#</th>
                        <th>Job</th>
                        <th>Start Week Day</th>
                        <th>End Week Day</th>
                        <th>Normal Time</th>
                         <th>Over Time</th>
                          <th>Normal Rate</th>
                         <th>Overtime Rate</th>
                          <th>Normal Amount</th>
                         <th>Over Time</th>
                       <%-- <th>Is Active</th>--%>
                        <th>Action</th>
                    </tr>
                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                </table>
            </LayoutTemplate>

            <ItemTemplate>
                <tr>
                    <asp:HiddenField ID="HidId" runat="server" Value='<%#Eval("Id")%>' />
                <td>
                    <asp:Label ID="SrNo" runat="server" Text='<%#Container.DataItemIndex + 1%> '></asp:Label>
                </td>
                  <td>
                    <asp:Label ID="Label10" runat="server" Text='<%#Eval("Title")%>'></asp:Label>
                </td>
              
              
                <td>
                    <asp:Label ID="Label11" runat="server" Text='<%#Convert.ToString(Eval("StartWeekDay")) +" "+ Convert.ToString(Eval("StartTime"))%>'></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text='<%#Convert.ToString(Eval("EndWeekDay"))  +" "+ Convert.ToString(Eval("EndTime"))%>'></asp:Label>
                </td>
                  <td>
                    <asp:Label ID="Label2" runat="server" Text='<%#Convert.ToString(Eval("TotalTimeinWords"))%>'></asp:Label>
                </td>
                  <td>
                    <asp:Label ID="Label7" runat="server" Text='<%#Convert.ToString(Eval("TotalOverTimeinWords"))%>'></asp:Label>
                </td>
                  <td>
                    <asp:Label ID="Label4" runat="server" Text='<%#"$" + Convert.ToString(Eval("NormalRate"))%>'></asp:Label>
                </td>
                      <td>
                    <asp:Label ID="Label6" runat="server" Text='<%#"$" + Convert.ToString(Eval("OverTimeRate"))%>'></asp:Label>
                </td>
                  <td>
                    <asp:Label ID="Label5" runat="server" Text='<%#"$" + Convert.ToString(Eval("TotalNormalAmount"))%>'></asp:Label>
                </td>
                     <td>
                    <asp:Label ID="Label3" runat="server" Text='<%#"$" + Convert.ToString(Eval("TotalOverTimeAmount"))%>'></asp:Label>
                </td>
                 <td>
                    <asp:LinkButton ID="editImageButton" CommandArgument="" class="btn btn-warning" runat="server" CommandName="edit"
                        ToolTip="Edit"><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                    <asp:Button 
                    ID="btnDelete" 
                    runat="server" 
                    Text="Delete"
                    CommandName="DeleteRow"
                    CommandArgument='<%# Eval("Id") %>'
                    CssClass="btn btn-danger btn-sm" OnClientClick="return confirm('Are you sure you want to delete record ?');" />
                </td>
                   <%-- <td><%# Eval("WorkDate", "{0:dd-MMM}") %></td>
                    <td><%# Eval("Amount", "{0:N2}") %></td>--%>
                </tr>
            </ItemTemplate>

        </asp:ListView>

       <%-- <div style="text-align:right;font-weight:bold;">
            Weekly Total: <%# Eval("WeeklyTotal", "{0:N2}") %>
        </div>--%>

        <hr />

    </ItemTemplate>
</asp:ListView>
</asp:Content>
