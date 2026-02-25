<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="WeeklyScheduleCalendar.aspx.cs" Inherits="LMS.Admin.WeeklyScheduleCalendar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .container {
            width:90%!important
        }
    </style>
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-1" style='font-weight:bold'>Select Job</div>
            <div class="col-md-5">
                <asp:DropDownList ID="JobDDL" runat="server" CssClass="form-control" OnSelectedIndexChanged="JobDDL_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            </div>
        </div>
    </div>
    <hr />
    <div id="contents" runat="server">

    </div>
</asp:Content>
