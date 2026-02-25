<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ManageEmployeeSalaryJob.aspx.cs" Inherits="LMS.Admin.ManageEmployeeSalaryJob" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row" style="padding-bottom: 5px">
        <div class="col-md-11" style="background-color:darkred;color:white"><span style="font-size: 18pt" runat="server" id="EmployeeDetail"></span></div>
        <div class="col-md-1" style="text-align: right">
            <asp:Button ID="Button1" runat="server" CssClass="btn btn-success" Text="+" OnClick="AddButton_Click" /></div>
    </div>

    <div class="row" style="background-color: #2258A2; padding-bottom: 10px; padding-left: 10px; color: white; border-radius: 5px">
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
    </div>
    <asp:ListView ID="LV2" runat="server"
        OnItemCreated="LV2_ItemCreated"
        OnItemInserting="LV2_ItemInserting"
        OnItemCanceling="LV2_ItemCanceling"
        OnItemEditing="LV2_ItemEditing"
        OnItemUpdating="LV2_ItemUpdating"
        OnItemDataBound="LV2_ItemDataBound"
        OnItemDeleting="LV2_ItemDeleting"
        GroupPlaceholderID="groupPlaceHolder1"
        ItemPlaceholderID="itemPlaceHolder1"
        OnPagePropertiesChanging="LV2_PagePropertiesChanging">

        <LayoutTemplate>
            <table class="table table-condensed" style="margin-top: 10px;">
                <thead>
                    <tr style="background-color: #383838; color: white;">
                        <th>Sr#</th>
                        <th>Employee</th>
                         <th>Job Title</th>
                          <th>Weekly Salary</th>
                          <th>Remarks</th>
                        <th>Is Active</th>
                        <th>Created At</th>
                        <th>Updated At</th>
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
                    <asp:Label ID="Name" runat="server" Text='<%#Eval("Employee.Name")%>'></asp:Label>
                </td>
                   <td>
                    <asp:Label ID="Label10" runat="server" Text='<%#Eval("JobTitle.Title")%>'></asp:Label>
                </td>
                   <td>
                    <asp:Label ID="Label11" runat="server" Text='<%#Eval("WeeklySalary")%>'></asp:Label>
                </td>
                
                  <td>
                    <asp:Label ID="Label13" runat="server" Text='<%#Eval("Remarks")%>'></asp:Label>
                </td>
               <td>
                    <asp:Label ID="Label3" runat="server" Text='<%#(Convert.ToBoolean(Eval("IsActive"))==true)?"Yes":"No"%>'></asp:Label>
                </td>
                  <td>
                    <asp:Label ID="Label4" runat="server" Text='<%#Eval("CreatedAt")%>'></asp:Label></td>


                <td>
                    <asp:Label ID="Label5" runat="server" Text='<%#Eval("UpdatedAt")%>'></asp:Label></td>
             
                <td>
                     <asp:LinkButton ID="editImageButton" class="btn btn-warning" runat="server" CommandName="edit"
                        ToolTip="Edit"><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                       <%--  <asp:LinkButton ID="deleteImageButton" class="btn btn-danger" runat="server" CommandName="delete"
                        ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete record ?');"><span class="glyphicon glyphicon-erase"></span></asp:LinkButton>--%>
                </td>
            </tr>
        </ItemTemplate>

        <InsertItemTemplate>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text='<%#Container.DataItemIndex + 1%>'></asp:Label></td>
                 

               
                      <td>
                    <asp:DropDownList ID="EmployeeDDL" class="form-control control-txt" runat="server">
                        <asp:ListItem Value="0">Select Employee</asp:ListItem>
                    </asp:DropDownList>

                </td>
                      <td>
                    <asp:DropDownList ID="JobTitleDDL" class="form-control control-txt" runat="server">
                        <asp:ListItem Value="0">Select Job Title</asp:ListItem>
                    </asp:DropDownList>
</td>
                <td>
                    <asp:TextBox ID="WeeklySalaryTxt" class="form-control control-txt" runat="server" Text='<%#Bind("WeeklySalary")%>'></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="g1"
                        ErrorMessage="Required" Style="color: red"
                        ControlToValidate="WeeklySalaryTxt">
                    </asp:RequiredFieldValidator>
                </td>
                <td></td>
                 <td>
                    <asp:TextBox ID="RemarksTxt" class="form-control control-txt" runat="server" Text='<%#Bind("Remarks")%>'></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="g1"
                        ErrorMessage="Required" Style="color: red"
                        ControlToValidate="RemarksTxt">
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
                    <asp:LinkButton class="btn btn-success" ID="saveImageButton" runat="server" CommandName="insert"
                        ToolTip="Save" ValidationGroup="g1"><span class="glyphicon glyphicon-floppy-disk"></span></asp:LinkButton>
                </td>
            </tr>
        </InsertItemTemplate>

        <EditItemTemplate>
            <tr>
                <asp:HiddenField ID="HidId" runat="server" Value='<%#Eval("Id")%>' />
                 <asp:HiddenField ID="HidEmployeeId" runat="server" Value='<%#Eval("EmployeeId")%>' />
                 <asp:HiddenField ID="HidJobTitleId" runat="server" Value='<%#Eval("JobTitleId")%>' />
                   <asp:HiddenField ID="HidIsActive" runat="server" Value='<%#(Convert.ToString(Eval("IsActive")))%>' />
                <td>
                    <asp:Label ID="Label7" runat="server" Text='<%#Container.DataItemIndex + 1%> '></asp:Label>
                </td>

                 <td>
                    <asp:DropDownList ID="EmployeeDDL" class="form-control control-txt" runat="server">
                        <asp:ListItem Value="0">Select Employee</asp:ListItem>
                    </asp:DropDownList>
                     </td>
               
                      <td>
                    <asp:DropDownList ID="JobTitleDDL" class="form-control control-txt" runat="server">
                        <asp:ListItem Value="0">Select Job Title</asp:ListItem>
                    </asp:DropDownList>
                          </td>
                <td>
                    <asp:TextBox ID="WeeklySalaryTxt" class="form-control control-txt" runat="server" Text='<%#Bind("WeeklySalary")%>'></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="g1"
                        ErrorMessage="Required" Style="color: red"
                        ControlToValidate="WeeklySalaryTxt">
                    </asp:RequiredFieldValidator>
                </td>
                <td></td>
                 <td>
                    <asp:TextBox ID="RemarksTxt" class="form-control control-txt" runat="server" Text='<%#Bind("Remarks")%>'></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="g1"
                        ErrorMessage="Required" Style="color: red"
                        ControlToValidate="RemarksTxt">
                    </asp:RequiredFieldValidator>
                </td>
                  <td>
                    <asp:DropDownList ID="IsActiveDDL" class="form-control control-txt" runat="server">
                        <asp:ListItem Value="true">Yes</asp:ListItem>
                        <asp:ListItem Value="false">No</asp:ListItem>
                    </asp:DropDownList>

                <td>

                  <td>
                    <asp:Label ID="Label8" runat="server" Text='<%#Eval("CreatedAt")%>'></asp:Label></td>


                <td>
                    <asp:Label ID="Label9" runat="server" Text='<%#Eval("UpdatedAt")%>'></asp:Label></td>
             
                <td>
                     <asp:LinkButton ID="LinkButton1" class="btn btn-warning" runat="server" CommandName="cancel"
                        ToolTip="Cancel"><span class="glyphicon glyphicon-arrow-left"></span></asp:LinkButton>
                    <asp:LinkButton class="btn btn-success" ValidationGroup="g1" ID="LinkButton2" runat="server" CommandName="update"
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
