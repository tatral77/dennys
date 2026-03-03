using LMS.Helper;
using LMS.Models;
using LMS.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using static LMS.Repos.EmployeeRepo;

namespace LMS.Admin
{
    public partial class ManageEmployees : System.Web.UI.Page
    {
        EmployeeRepo repo = new EmployeeRepo();
        LocationRepo locationrepo = new LocationRepo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              
                bindLV();
            }
               
        }
        protected void bindLV()
        {
            int id = Convert.ToInt32(Request.QueryString["Id"]);
            Location location = locationrepo.GetLocation(id);
            LocationDetail.InnerText = location.Name;
            List<Employee> employee = repo.GetLocationEmployees(id);
            LV.DataSource = employee;
            LV.DataBind();

        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            LV.SelectedIndex = -1;
            LV.EditIndex = -1;
            LV.InsertItemPosition = InsertItemPosition.FirstItem;
            bindLV();
        }

        protected void LV_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            int id = Convert.ToInt32(Request.QueryString["Id"]);
            TextBox name = (TextBox)LV.InsertItem.FindControl("NameTxt");
            TextBox email = (TextBox)LV.InsertItem.FindControl("EmailTxt");
            TextBox phone = (TextBox)LV.InsertItem.FindControl("PhoneTxt");
            DropDownList IsActiveDDL = (DropDownList)LV.InsertItem.FindControl("IsActiveDDL");
            Employee employee = new Employee();
            employee.LocationId = id;
            employee.Name = Convert.ToString(name.Text);
            employee.Email = Convert.ToString(email.Text);
            employee.Phone = Convert.ToString(phone.Text);
            employee.IsActive = Convert.ToBoolean(IsActiveDDL.SelectedValue);
            employee.CreatedAt = DateTime.Now;
            repo.Add(employee);
            LV.EditIndex = -1;
            LV.InsertItemPosition = InsertItemPosition.None;
            bindLV();
            e.Cancel = true;
            Response.Redirect("ManageEmployees.aspx?id=" + id);
        }

        protected void LV_ItemCreated(object sender, ListViewItemEventArgs e)
        {


        }

        protected void LV_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            LV.EditIndex = -1;
            LV.InsertItemPosition = InsertItemPosition.None;
            LV.SelectedIndex = -1;
            bindLV();
            e.Cancel = true;

        }

        protected void LV_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            LV.InsertItemPosition = InsertItemPosition.None;
            LV.SelectedIndex = -1;
            LV.EditIndex = e.NewEditIndex;
            bindLV();
            e.Cancel = true;
        }

        protected void LV_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(Request.QueryString["Id"]);
            //DepartmentRepo dr = new DepartmentRepo(); 
            HiddenField Id = LV.EditItem.FindControl("HidId") as HiddenField;
            TextBox name = (TextBox)LV.EditItem.FindControl("NameTxt");
            TextBox email = (TextBox)LV.EditItem.FindControl("EmailTxt");
            TextBox phone = (TextBox)LV.EditItem.FindControl("PhoneTxt");
            DropDownList IsActiveDDL = (DropDownList)LV.EditItem.FindControl("IsActiveDDL");
            Employee employee = new Employee();
            employee.Id = Convert.ToInt32(Id.Value);
            employee.Name = Convert.ToString(name.Text);
            employee.Email = Convert.ToString(email.Text);
            employee.Phone = Convert.ToString(phone.Text);
            employee.IsActive = Convert.ToBoolean(IsActiveDDL.SelectedValue);
            employee.UpdatedAt = DateTime.Now;
            repo.Update(employee);
            LV.EditIndex = -1;
            LV.InsertItemPosition = InsertItemPosition.None;
            bindLV();
            e.Cancel = true;
            Response.Redirect("ManageEmployees.aspx?id=" + id);

        }

        protected void LV_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (LV.EditIndex == (e.Item as ListViewDataItem).DataItemIndex)
            {
                //    DropDownList MinistryDDL = e.Item.FindControl("MinistryDDL") as DropDownList;
                //    HiddenField HidMinistriesId = (e.Item.FindControl("HidMinistriesId") as HiddenField);
                //    MinistryDDL.SelectedValue = HidMinistriesId.Value.ToString();

                DropDownList IsActiveDDL = e.Item.FindControl("IsActiveDDL") as DropDownList;
                HiddenField HidIsActive = (e.Item.FindControl("HidIsActive") as HiddenField);
                IsActiveDDL.SelectedValue = HidIsActive.Value.ToString();
            }
        }

        protected string GetStatus(int id)
        {
            if (id == 0)
                return "No";
            else
                return "Yes";

        }

        protected void LV_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            HiddenField Id = (HiddenField)LV.Items[e.ItemIndex].FindControl("HidId");
            if (!string.IsNullOrEmpty(Id.Value))
            {
                int id = Convert.ToInt32(Id.Value);

                repo.Delete(id);

                LV.EditIndex = -1;
                LV.InsertItemPosition = InsertItemPosition.None;
                bindLV();
                e.Cancel = true;
                Response.Redirect("ManageEmployees.aspx?id=" + id);
            }
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            FilterEmployee filterCategory = new FilterEmployee();
            filterCategory.IsActive = -1;
            if (!string.IsNullOrEmpty(NameTxt.Text))
            {
                filterCategory.Name = NameTxt.Text;
            }
            if (Convert.ToInt32(IsActiveDDL.SelectedValue) != -1)
            {
                filterCategory.IsActive = Convert.ToInt32(IsActiveDDL.SelectedValue);
            }
            LV.DataSource = repo.SearchCategory(filterCategory).ToList();
            LV.DataBind();
        }

        protected void LV_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            (LV.FindControl("DataPager1") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            this.bindLV();
        }

        protected void NumberOfRecordsDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetPageSize();
        }
        protected void SetPageSize()
        {
            DropDownList ddl = (LV.FindControl("NumberOfRecordsDDL") as DropDownList);
            DataPager pager = (LV.FindControl("DataPager1") as DataPager);
            if (pager != null)
                pager.PageSize = Convert.ToInt32(ddl.SelectedValue);
            bindLV();
        }

        protected void ResetBtn_Click(object sender, EventArgs e)
        {
            NameTxt.Text = "";
            IsActiveDDL.SelectedIndex = 0;
            DropDownList ddl = (LV.FindControl("NumberOfRecordsDDL") as DropDownList);
            ddl.SelectedIndex = 0;
            SetPageSize();
            bindLV();
        }
    }
}