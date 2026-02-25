using LMS.Models;
using LMS.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static LMS.Repos.JobScheduleRepo;

namespace LMS.Admin
{
    public partial class ManageJobSchedules : System.Web.UI.Page
    {
        JobScheduleRepo repo = new JobScheduleRepo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindLV();
            }
        }
        protected void bindLV()
        {
            List<JobSchedule> locations = repo.GetAll();
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
            TextBox DescriptionTxt = (TextBox)LV.InsertItem.FindControl("DescriptionTxt");
            TextBox ForecastSaleTxt = (TextBox)LV.InsertItem.FindControl("ForecastSaleTxt");
            TextBox PercentageTxt = (TextBox)LV.InsertItem.FindControl("PercentageTxt");
            JobSchedule location = new JobSchedule();
            location.Description = Convert.ToString(DescriptionTxt.Text);
            location.ForcastedSale= Convert.ToDouble(ForecastSaleTxt.Text);
            location.Percentage = Convert.ToDouble(PercentageTxt.Text);
            location.IsActive = true;
            location.CreatedOn = DateTime.Now;
            repo.Add(location);
            LV.EditIndex = -1;
            LV.InsertItemPosition = InsertItemPosition.None;
            bindLV();
            e.Cancel = true;
            Response.Redirect("ManageJobSchedules.aspx");
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
            TextBox DescriptionTxt = (TextBox)LV.EditItem.FindControl("DescriptionTxt");
            TextBox ForecastSaleTxt = (TextBox)LV.EditItem.FindControl("ForecastSaleTxt");
            TextBox PercentageTxt = (TextBox)LV.EditItem.FindControl("PercentageTxt");
            DropDownList IsActiveDDL = (DropDownList)LV.EditItem.FindControl("IsActiveDDL");
            JobSchedule locationDTO = new JobSchedule();
            locationDTO.Id = Convert.ToInt32(Id.Value);
            locationDTO.Description = Convert.ToString(DescriptionTxt.Text);
            locationDTO.ForcastedSale = Convert.ToDouble(ForecastSaleTxt.Text);
            locationDTO.Percentage = Convert.ToDouble(PercentageTxt.Text);
            locationDTO.IsActive = Convert.ToBoolean(IsActiveDDL.SelectedValue);
            repo.Update(locationDTO);
            LV.EditIndex = -1;
            LV.InsertItemPosition = InsertItemPosition.None;
            bindLV();
            e.Cancel = true;
            Response.Redirect("ManageJobSchedules.aspx");

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
                Response.Redirect("ManageJobSchedules.aspx");
            }
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            FilterJobSchedule filterCategory = new FilterJobSchedule();
            filterCategory.IsActive = -1;
            if (!string.IsNullOrEmpty(DescriptionTxt.Text))
            {
                filterCategory.Description = DescriptionTxt.Text;
            }
            if (Convert.ToInt32(IsActiveDDL.SelectedValue) != -1)
            {
                filterCategory.IsActive = Convert.ToInt32(IsActiveDDL.SelectedValue);
            }
            LV.DataSource = repo.SearchJobSchedule(filterCategory).ToList();
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
            DescriptionTxt.Text = "";
            IsActiveDDL.SelectedIndex = 0;
            DropDownList ddl = (LV.FindControl("NumberOfRecordsDDL") as DropDownList);
            ddl.SelectedIndex = 0;
            SetPageSize();
            bindLV();
        }

    }
}