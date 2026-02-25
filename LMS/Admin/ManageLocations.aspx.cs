using LMS.Models;
using LMS.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using static LMS.Repos.LocationRepo;

namespace LMS.Admin
{
    public partial class ManageLocations : System.Web.UI.Page
    {
        LocationRepo repo = new LocationRepo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindLV();
            }
        }
        protected void bindLV()
        {
            List<Location> locations = repo.GetAll();
            LV.DataSource = locations;
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
            TextBox name = (TextBox)LV.InsertItem.FindControl("NameTxt");
            Location location = new Location();
            location.Name = Convert.ToString(name.Text);
            location.IsActive = true;
            location.CreatedAt = DateTime.Now;
            location.UpdatedAt = DateTime.Now;
            repo.Add(location);
            LV.EditIndex = -1;
            LV.InsertItemPosition = InsertItemPosition.None;
            bindLV();
            e.Cancel = true;
            Response.Redirect("ManageLocations.aspx");
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
            //DepartmentRepo dr = new DepartmentRepo(); 
            HiddenField Id = LV.EditItem.FindControl("HidId") as HiddenField;
            TextBox name = (TextBox)LV.EditItem.FindControl("NameTxt");
            DropDownList IsActiveDDL = (DropDownList)LV.EditItem.FindControl("IsActiveDDL");
            Location locationDTO = new Location();
            locationDTO.Id = Convert.ToInt32(Id.Value);
            locationDTO.Name = Convert.ToString(name.Text);
            locationDTO.IsActive = Convert.ToBoolean(IsActiveDDL.SelectedValue);
            repo.Update(locationDTO);
            LV.EditIndex = -1;
            LV.InsertItemPosition = InsertItemPosition.None;
            bindLV();
            e.Cancel = true;
            Response.Redirect("ManageLocations.aspx");

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
                Response.Redirect("ManageLocations.aspx");
            }
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            SearchLocation search = new SearchLocation();
            List<Location> locations = new List<Location>();
            LocationRepo dr = new LocationRepo();
            if (!string.IsNullOrEmpty(NameTxt.Text))
            {
                search.Name = NameTxt.Text;
            }
            if (!IsActiveDDL.SelectedValue.Equals("Select"))
            {
                search.IsActive = Convert.ToInt32(IsActiveDDL.SelectedValue);
            }

            LV.DataSource = dr.SearchDepartments(search).ToList();
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