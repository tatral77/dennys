<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ManageJobSchedules.aspx.cs" Inherits="LMS.Admin.ManageJobSchedules" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="row" style="padding-bottom: 5px">
        <div class="col-md-6"><span style="font-size: 18pt">Job Schedules</span></div>
        <div class="col-md-6" style="text-align: right">
            <asp:Button ID="Button1" runat="server" CssClass="btn btn-success" Text="+" OnClick="AddButton_Click" /></div>
    </div>

    <div class="row" style="background-color: #2258A2; padding-bottom: 10px; padding-left: 10px; color: white; border-radius: 5px">
        <h4>Search</h4>

        <div class="col-md-3" style="text-align: left;">
            <asp:Label ID="Label1" runat="server" Text="DescriptionTxt"></asp:Label><asp:TextBox ID="DescriptionTxt" class="form-control control-txt" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-2" style="text-align: left;">
            <asp:Label ID="Label6" runat="server" Text="Is Active"></asp:Label><asp:DropDownList ID="IsActiveDDL" class="form-control" runat="server">
                <asp:ListItem Value="-1">Select</asp:ListItem>
                <asp:ListItem Value="1">Yes</asp:ListItem>
                <asp:ListItem Value="2">No</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-md-2">
            <br />
            <asp:Button ID="SearchBtn" runat="server" Text="Search" OnClick="SearchBtn_Click" CssClass="btn btn-success" />
            <asp:Button ID="ResetBtn" runat="server" Text="Reset" OnClick="ResetBtn_Click" CssClass="btn btn-info" />
        </div>
    </div>


    <asp:ListView ID="LV" runat="server"
        OnItemCreated="LV_ItemCreated"
        OnItemInserting="LV_ItemInserting"
        OnItemCanceling="LV_ItemCanceling"
        OnItemEditing="LV_ItemEditing"
        OnItemUpdating="LV_ItemUpdating"
        OnItemDataBound="LV_ItemDataBound"
        OnItemDeleting="LV_ItemDeleting"
        GroupPlaceholderID="groupPlaceHolder1"
        ItemPlaceholderID="itemPlaceHolder1"
        OnPagePropertiesChanging="LV_PagePropertiesChanging">

        <LayoutTemplate>
            <table class="table table-condensed" style="margin-top: 10px;">
                <thead>
                    <tr style="background-color: #383838; color: white;">
                        <th>Sr#</th>
                        <th>Description</th>
                        <th>Forcast Sale</th>
                        <th>Budget Percentage</th>
                        <th>Created On</th>
                        <th>Is Active</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                <tr>
                    <td colspan="6">
                        <asp:DropDownList ID="NumberOfRecordsDDL" runat="server" AutoPostBack="true" OnSelectedIndexChanged="NumberOfRecordsDDL_SelectedIndexChanged">
                            <asp:ListItem Value="2">2</asp:ListItem>
                            <asp:ListItem Value="4">4</asp:ListItem>
                        </asp:DropDownList>
                        <asp:DataPager ID="DataPager1" runat="server" PagedControlID="LV" PageSize="5">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="true" ShowPreviousPageButton="false"
                                    ShowNextPageButton="true" />
                                <asp:NumericPagerField ButtonType="Link" />
                                <asp:NextPreviousPagerField ButtonType="Button" PreviousPageText="Prev" ShowNextPageButton="false" ShowLastPageButton="true"
                                    ShowPreviousPageButton="true" />
                            </Fields>
                        </asp:DataPager>


                    </td>
                </tr>
            </table>
            <tbody>
        </LayoutTemplate>
        <GroupTemplate>
            <tr>
                <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
            </tr>
        </GroupTemplate>
        <ItemTemplate>
            <tr>
                <asp:HiddenField ID="HidId" runat="server" Value='<%#Eval("Id")%>' />
                <td>
                    <asp:Label ID="SrNo" runat="server" Text='<%#Container.DataItemIndex + 1%> '></asp:Label>
                </td>

                <td>
                   
                   <asp:Label ID="Name" runat="server" Text='<%#Eval("Description")%>'></asp:Label>
                </td>
                 
                 <td>
                    <asp:Label ID="sale" runat="server" Text='<%#Eval("ForcastedSale")%>'></asp:Label>
                </td>
                 <td>
                    <asp:Label ID="ghgh" runat="server" Text='<%#Eval("Percentage")%>'></asp:Label>
                </td>
                   <td>
                    <asp:Label ID="createdon" runat="server" Text='<%#Eval("CreatedOn")%>'></asp:Label>
                </td>

               <td>
                    <asp:Label ID="Label3" runat="server" Text='<%#(Convert.ToBoolean(Eval("IsActive"))==true)?"Yes":"No"%>'></asp:Label>
                </td>

                <td>
                     <asp:LinkButton ID="editImageButton" class="btn btn-warning" runat="server" CommandName="edit"
                        ToolTip="Edit"><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                     <a href="ManageEmployeeJobSchedule.aspx?Id=<%#Eval("Id")%>" class="btn btn-success">Employees Schedule </a>
                     <a href="WeeklyScheduleCalendar.aspx?Id=<%#Eval("Id")%>" class="btn btn-success">View Schedule</a>
                       <%--  <asp:LinkButton ID="deleteImageButton" class="btn btn-danger" runat="server" CommandName="delete"
                        ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete record ?');"><span class="glyphicon glyphicon-erase"></span></asp:LinkButton>--%>
                </td>
            </tr>
        </ItemTemplate>

        <InsertItemTemplate>
            <tr>
                <td>
                    <asp:Label ID="SrNo" runat="server" Text='<%#Container.DataItemIndex + 1%>'></asp:Label></td>
                <td>
                    <asp:TextBox ID="DescriptionTxt" class="form-control control-txt" runat="server" Text="" TextMode="MultiLine"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="FV_Name" runat="server" ValidationGroup="g1"
                        ErrorMessage="Required" Style="color: red"
                        ControlToValidate="DescriptionTxt">
                    </asp:RequiredFieldValidator>
                </td>
                 <td>
                    <asp:TextBox ID="ForecastSaleTxt" class="form-control control-txt" runat="server" Text="" TextMode="MultiLine"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="g1"
                        ErrorMessage="Required" Style="color: red"
                        ControlToValidate="ForecastSaleTxt">
                    </asp:RequiredFieldValidator>
                </td>
                 <td>
                    <asp:TextBox ID="PercentageTxt" class="form-control control-txt" runat="server" Text="" TextMode="MultiLine"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="g1"
                        ErrorMessage="Required" Style="color: red"
                        ControlToValidate="PercentageTxt">
                    </asp:RequiredFieldValidator>
                </td>
                 <td>
                    <asp:DropDownList ID="IsActiveDDL" class="form-control control-txt" runat="server">
                        <asp:ListItem Value="true">Yes</asp:ListItem>
                        <asp:ListItem Value="false">No</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td></td>
                <td></td>
                <td>
                       <asp:LinkButton ID="cancelImageButton" class="btn btn-warning" runat="server" CommandName="cancel"
                        ToolTip="Cancel"><span class="glyphicon glyphicon-arrow-left"></span></asp:LinkButton>
                    <asp:LinkButton class="btn btn-success" ID="saveImageButton" runat="server" CommandName="insert"
                        ToolTip="Save" ValidationGroup="g1"><span class="glyphicon glyphicon-floppy-disk"></span></asp:LinkButton>
                </td>
            </tr>
        </InsertItemTemplate>

        <EditItemTemplate>
            <tr>
                <asp:HiddenField ID="HidId" runat="server" Value='<%#Eval("Id")%>' />
                   <asp:HiddenField ID="HidIsActive" runat="server" Value='<%#Eval("IsActive")%>' />
                <td>
                    <asp:Label ID="SrNo" runat="server" Text='<%#Container.DataItemIndex + 1%> '></asp:Label>
                </td>

                <td>
                    <asp:TextBox ID="DescriptionTxt" class="form-control control-txt" runat="server" Text='<%#Bind("Description")%>'></asp:TextBox>
                    <asp:RequiredFieldValidator ID="FV_Name" runat="server" ValidationGroup="g1"
                        ErrorMessage="Required" Style="color: red"
                        ControlToValidate="DescriptionTxt">
                    </asp:RequiredFieldValidator>
                </td>
                   <td>
                    <asp:TextBox ID="ForecastSaleTxt" class="form-control control-txt" runat="server" Text='<%#Bind("ForcastedSale")%>'></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="g1"
                        ErrorMessage="Required" Style="color: red"
                        ControlToValidate="ForecastSaleTxt">
                    </asp:RequiredFieldValidator>
                </td>
                   <td>
                    <asp:TextBox ID="PercentageTxt" class="form-control control-txt" runat="server" Text='<%#Bind("Percentage")%>'></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="g1"
                        ErrorMessage="Required" Style="color: red"
                        ControlToValidate="PercentageTxt">
                    </asp:RequiredFieldValidator>
                </td>
                  <td>
                    <asp:DropDownList ID="IsActiveDDL" class="form-control control-txt" runat="server">
                        <asp:ListItem Value="true">Yes</asp:ListItem>
                        <asp:ListItem Value="false">No</asp:ListItem>
                    </asp:DropDownList>

                <td>

                <td>
                     <asp:LinkButton ID="cancelImageButton" class="btn btn-warning" runat="server" CommandName="cancel"
                        ToolTip="Cancel"><span class="glyphicon glyphicon-arrow-left"></span></asp:LinkButton>
                    <asp:LinkButton class="btn btn-success" ValidationGroup="g1" ID="saveImageButton" runat="server" CommandName="update"
                        ToolTip="Save"><span class="glyphicon glyphicon-floppy-disk"></span></asp:LinkButton>
                </td>
            </tr>
        </EditItemTemplate>

        <EmptyDataTemplate>
            <tr>
                <td colspan="4">
                    <p>No Data Found</p>
                </td>
            </tr>
        </EmptyDataTemplate>

    </asp:ListView>
</asp:Content>
