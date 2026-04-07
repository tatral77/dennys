using LMS.Helper;
using LMS.Models;
using LMS.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static LMS.Repos.ScheduleWeekRepo;

namespace LMS.Admin
{
    public partial class ManageScheduleWeeks : System.Web.UI.Page
    {
        ScheduleWeekRepo scheduleWeekRepo = new ScheduleWeekRepo();
        ScheduleWeekRepo GetscheduleWeekRepo = new ScheduleWeekRepo();
        LocationWeekRepo locationWeekRepo = new LocationWeekRepo();
        LocationWeekRepo GetlocationWeekRepo = new LocationWeekRepo();
        LocationRepo locationrepo = new LocationRepo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                for (int i=2026;i<=2099;i++)
                {
                    ListItem li = new ListItem();
                    li.Value = i.ToString();
                    li.Text = i.ToString();
                    YearsDDL.Items.Add(li);
                }
                bindLV();
            }
        }
        protected void bindLV()
        {
            int locationId = Convert.ToInt32(Request.QueryString["Id"]);
            Location location = locationrepo.GetLocation(locationId);
            LocationDetail.InnerText = location.Name + " ( Weeks )";
            int Year = Convert.ToInt32(YearsDDL.SelectedValue);
            List<LocationWeek> locations = GetlocationWeekRepo.GetAll(locationId,Year);
            LV.DataSource = locations;
            LV.DataBind();

        }
        protected void GenerateWeeksBtn_Click(object sender, EventArgs e)
        {
            int Year = Convert.ToInt32(YearsDDL.SelectedValue);
            if (Year != 0)
            {
                List<ScheduleWeek> result = scheduleWeekRepo.GetAll(Year);
                if (result.Count == 0)
                {
                    List<ScheduleWeek> restaurantWeeks = WeeksHelper.GenerateRestaurantWeeks(Year);
                    scheduleWeekRepo.Add(restaurantWeeks);
                
                }
                GenerateLocationWeeks(Year);
            }
           
            bindLV();
        }
        protected void GenerateLocationWeeks(int Year)
        {
            int locationId = Convert.ToInt32(Request.QueryString["Id"]);
            if (Year != 0)
            {
                List<LocationWeek> result = locationWeekRepo.GetAll(locationId, Year);
                if (result.Count == 0)
                {
                    List<ScheduleWeek> scheduleWeeks = GetscheduleWeekRepo.GetAll(Year);
                    List<LocationWeek> locationWeeks = new List<LocationWeek>();
                    foreach(ScheduleWeek scheduleWeek in scheduleWeeks)
                    {
                        LocationWeek locationWeek = new LocationWeek();
                        locationWeek.LocationId = locationId;
                        locationWeek.ScheduleWeekId = scheduleWeek.Id;
                        locationWeek.ForcastedSale = 0;
                        locationWeek.Percentage = 0;
                        locationWeeks.Add(locationWeek);

                    }
                    locationWeekRepo.Add(locationWeeks);
                }
            }
            bindLV();
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
            //int Year = Convert.ToInt32(YearsDDL.SelectedValue);
            //TextBox DescriptionTxt = (TextBox)LV.InsertItem.FindControl("DescriptionTxt");
            //TextBox ForecastSaleTxt = (TextBox)LV.InsertItem.FindControl("ForecastSaleTxt");
            //TextBox PercentageTxt = (TextBox)LV.InsertItem.FindControl("PercentageTxt");
            //ScheduleWeek jobSchedule = new ScheduleWeek();
            //jobSchedule.Year = Year;
            //jobSchedule.WeekDecription = Convert.ToString(DescriptionTxt.Text);
            //jobSchedule.ForcastedSale = Convert.ToDouble(ForecastSaleTxt.Text);
            //jobSchedule.Percentage = Convert.ToDouble(PercentageTxt.Text);
            //repo.Add(jobSchedule);
            //LV.EditIndex = -1;
            //LV.InsertItemPosition = InsertItemPosition.None;
            //bindLV();
            //e.Cancel = true;
            //Response.Redirect("ManageScheduleWeeks.aspx?id=" + Year);
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
            int locationId = Convert.ToInt32(Request.QueryString["Id"]);
            HiddenField HidId = LV.EditItem.FindControl("HidId") as HiddenField;
            HiddenField HidWeekId = LV.EditItem.FindControl("HidWeekId") as HiddenField;
            TextBox DescriptionTxt = (TextBox)LV.EditItem.FindControl("DescriptionTxt");
            TextBox ForecastSaleTxt = (TextBox)LV.EditItem.FindControl("ForecastSaleTxt");
            TextBox PercentageTxt = (TextBox)LV.EditItem.FindControl("PercentageTxt");
            LocationWeek jobSchedule = new LocationWeek();
            jobSchedule.ScheduleWeekId=Convert.ToInt32(HidWeekId.Value); ;
            jobSchedule.Id = Convert.ToInt32(HidId.Value);
            //jobSchedule.Description = Convert.ToString(DescriptionTxt.Text);
            jobSchedule.ForcastedSale = Convert.ToDouble(ForecastSaleTxt.Text);
            jobSchedule.Percentage = Convert.ToDouble(PercentageTxt.Text);
            jobSchedule.LocationId = locationId;
            locationWeekRepo.Update(jobSchedule);
            LV.EditIndex = -1;
            LV.InsertItemPosition = InsertItemPosition.None;
            bindLV();
            e.Cancel = true;
           // Response.Redirect("ManageScheduleWeeks.aspx?id=" + Year);
        }

        protected void LV_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            //if (LV.EditIndex == (e.Item as ListViewDataItem).DataItemIndex)
            //{
            //    //    DropDownList MinistryDDL = e.Item.FindControl("MinistryDDL") as DropDownList;
            //    //    HiddenField HidMinistriesId = (e.Item.FindControl("HidMinistriesId") as HiddenField);
            //    //    MinistryDDL.SelectedValue = HidMinistriesId.Value.ToString();

            //    DropDownList IsActiveDDL = e.Item.FindControl("IsActiveDDL") as DropDownList;
            //    HiddenField HidIsActive = (e.Item.FindControl("HidIsActive") as HiddenField);
            //    IsActiveDDL.SelectedValue = HidIsActive.Value.ToString();
            //}
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
            int Year = Convert.ToInt32(YearsDDL.SelectedValue);
            HiddenField Id = (HiddenField)LV.Items[e.ItemIndex].FindControl("HidId");
            if (!string.IsNullOrEmpty(Id.Value))
            {
                int id = Convert.ToInt32(Id.Value);

                scheduleWeekRepo.Delete(id);

                LV.EditIndex = -1;
                LV.InsertItemPosition = InsertItemPosition.None;
                bindLV();
                e.Cancel = true;
                Response.Redirect("ManageScheduleWeeks.aspx?id=" + Year);
            }
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            //FilterJobSchedule filterCategory = new FilterJobSchedule();
            //filterCategory.IsActive = -1;
            //if (!string.IsNullOrEmpty(DescriptionTxt.Text))
            //{
            //    filterCategory.Description = DescriptionTxt.Text;
            //}
            //if (Convert.ToInt32(IsActiveDDL.SelectedValue) != -1)
            //{
            //    filterCategory.IsActive = Convert.ToInt32(IsActiveDDL.SelectedValue);
            //}
            //LV.DataSource = repo.SearchJobSchedule(filterCategory).ToList();
            //LV.DataBind();
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

        protected void YearsDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindLV();
        }

        protected string GetEmployeeScheduleLink(int WeekId)
        {
            int LocationId = Convert.ToInt32(Request.QueryString["Id"]);
            return "ManageEmployeeJobSchedule.aspx?Id=" + WeekId + "&LocationId=" + LocationId;
        }
        protected string GetCalendarLink(int WeekId)
        {
            int LocationId = Convert.ToInt32(Request.QueryString["Id"]);
            return "WeeklyScheduleCalendar.aspx?Id=" + WeekId + "&LocationId=" + LocationId;
        }
    }
}