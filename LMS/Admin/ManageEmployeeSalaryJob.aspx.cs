using LMS.Models;
using LMS.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LMS.Admin
{
    public partial class ManageEmployeeSalaryJob : System.Web.UI.Page
    {
        EmployeeWeeklyJobRepo repo = new EmployeeWeeklyJobRepo();
        EmployeeRepo emprepo = new EmployeeRepo();
        JobTitleRepo jobTitleRepo = new JobTitleRepo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindLV2();
            }
        }
        protected void bindLV2()
        {
            int EmployeeId = Convert.ToInt32(Request.QueryString["Id"]);
            List<EmployeeWeeklyJob> employeeWeeklyJobs = repo.GetEmployeeWeeklyJobs(EmployeeId);
            if(employeeWeeklyJobs != null)
           // Employee emp = emprepo.GetEmployee(EmployeeId);
            EmployeeDetail.InnerText = employeeWeeklyJobs[0].Employee.Name;
            List<EmployeeWeeklyJob> employee = repo.GetEmployeeWeeklyJobs(EmployeeId);
            LV2.DataSource = employee;
            LV2.DataBind();

        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            LV2.SelectedIndex = -1;
            LV2.EditIndex = -1;
            LV2.InsertItemPosition = InsertItemPosition.FirstItem;
            bindLV2();
        }

        protected void LV2_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            int EmployeeId = Convert.ToInt32(Request.QueryString["Id"]);
            //DropDownList EmployeeDDL = (DropDownList)LV2.InsertItem.FindControl("EmployeeDDL");
            DropDownList JobTitleDDL = (DropDownList)LV2.InsertItem.FindControl("JobTitleDDL");
            TextBox WeeklySalaryTxt = (TextBox)LV2.InsertItem.FindControl("WeeklySalaryTxt");
            TextBox RemarksTxt = (TextBox)LV2.InsertItem.FindControl("RemarksTxt");
            DropDownList IsActiveDDL = (DropDownList)LV2.InsertItem.FindControl("IsActiveDDL");
            EmployeeWeeklyJob entity = new EmployeeWeeklyJob();
            entity.EmployeeId = EmployeeId;// Convert.ToInt32(EmployeeDDL.SelectedValue);
            entity.JobTitleId = Convert.ToInt32(JobTitleDDL.SelectedValue);
            entity.WeeklySalary = Convert.ToDouble(WeeklySalaryTxt.Text);
            entity.Remarks = Convert.ToString(RemarksTxt.Text);
            entity.IsActive = Convert.ToBoolean(IsActiveDDL.SelectedValue);
            entity.CreatedAt = DateTime.Now;
            repo.Add(entity);
            LV2.EditIndex = -1;
            LV2.InsertItemPosition = InsertItemPosition.None;
            bindLV2();
            e.Cancel = true;
            Response.Redirect("ManageEmployeeWeeklyJobs.aspx?id=" + EmployeeId);
        }

        protected void LV2_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            //DropDownList EmployeeDDL = new DropDownList();
            //EmployeeDDL = (e.Item.FindControl("EmployeeDDL") as DropDownList);
            //if (EmployeeDDL != null)
            //{
            //    List<Employee> ps = emprepo.GetActive();
            //    if (ps != null)
            //    {
            //        foreach (Employee row in ps)
            //        {
            //            ListItem li = new ListItem();
            //            li.Value = row.Id.ToString();
            //            li.Text = row.Name.ToString();
            //            EmployeeDDL.Items.Add(li);
            //        }
            //    }

            //}
            DropDownList JobTitleDDL = new DropDownList();
            JobTitleDDL = (e.Item.FindControl("JobTitleDDL") as DropDownList);
            if (JobTitleDDL != null)
            {
                List<JobTitle> jobtitles = jobTitleRepo.GetActive();
                if (jobtitles != null)
                {
                    foreach (JobTitle row in jobtitles)
                    {
                        ListItem li = new ListItem();
                        li.Value = row.Id.ToString();
                        li.Text = row.Title.ToString();
                        JobTitleDDL.Items.Add(li);
                    }
                }

            }

        }

        protected void LV2_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            LV2.EditIndex = -1;
            LV2.InsertItemPosition = InsertItemPosition.None;
            LV2.SelectedIndex = -1;
            bindLV2();
            e.Cancel = true;

        }

        protected void LV2_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            LV2.InsertItemPosition = InsertItemPosition.None;
            LV2.SelectedIndex = -1;
            LV2.EditIndex = e.NewEditIndex;
            bindLV2();
            e.Cancel = true;
        }

        protected void LV2_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            int EmployeeId = Convert.ToInt32(Request.QueryString["Id"]);
            //DepartmentRepo dr = new DepartmentRepo(); 
            HiddenField Id = LV2.EditItem.FindControl("HidId") as HiddenField;
            //DropDownList EmployeeDDL = (DropDownList)LV2.EditItem.FindControl("EmployeeDDL");
            DropDownList JobTitleDDL = (DropDownList)LV2.EditItem.FindControl("JobTitleDDL");
            TextBox WeeklySalaryTxt = (TextBox)LV2.EditItem.FindControl("WeeklySalaryTxt");
            TextBox RemarksTxt = (TextBox)LV2.EditItem.FindControl("RemarksTxt");
            TextBox phone = (TextBox)LV2.EditItem.FindControl("PhoneTxt");
            DropDownList IsActiveDDL = (DropDownList)LV2.EditItem.FindControl("IsActiveDDL");
            EmployeeWeeklyJob entity = new EmployeeWeeklyJob();
            entity.Id = Convert.ToInt32(Id.Value);
            entity.EmployeeId = EmployeeId;
            entity.JobTitleId = Convert.ToInt32(JobTitleDDL.SelectedValue);
            entity.WeeklySalary = Convert.ToDouble(WeeklySalaryTxt.Text);
            //entity.OverTimeRate = entity.Rate + (entity.Rate / 2);
            entity.Remarks = Convert.ToString(RemarksTxt.Text);
            entity.IsActive = Convert.ToBoolean(IsActiveDDL.SelectedValue);
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;
            repo.Update(entity);
            LV2.EditIndex = -1;
            LV2.InsertItemPosition = InsertItemPosition.None;
            bindLV2();
            e.Cancel = true;
            Response.Redirect("ManageEmployeeWeeklyJobs.aspx?id=" + EmployeeId);

        }

        protected void LV2_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (LV2.EditIndex == (e.Item as ListViewDataItem).DataItemIndex)
            {
                //DropDownList EmployeeDDL = e.Item.FindControl("EmployeeDDL") as DropDownList;
                //HiddenField HidEmployeeId = (e.Item.FindControl("HidEmployeeId") as HiddenField);
                //EmployeeDDL.SelectedValue = HidEmployeeId.Value.ToString();

                DropDownList JobTitleDDL = e.Item.FindControl("JobTitleDDL") as DropDownList;
                HiddenField HidJobTitleId = (e.Item.FindControl("HidJobTitleId") as HiddenField);
                JobTitleDDL.SelectedValue = HidJobTitleId.Value.ToString();

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

        protected void LV2_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {

            HiddenField Id = (HiddenField)LV2.Items[e.ItemIndex].FindControl("HidId");
            if (!string.IsNullOrEmpty(Id.Value))
            {
                int id = Convert.ToInt32(Id.Value);

                repo.Delete(id);

                LV2.EditIndex = -1;
                LV2.InsertItemPosition = InsertItemPosition.None;
                bindLV2();
                e.Cancel = true;
            }
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            //FilterEmployeeWeeklyJob filterCategory = new FilterEmployeeWeeklyJob();
            //filterCategory.IsActive = -1;
            //if (!string.IsNullOrEmpty(NameTxt.Text))
            //{
            //    filterCategory.EmployeeName = NameTxt.Text;
            //}
            //if (!string.IsNullOrEmpty(JobTitleTxt.Text))
            //{
            //    filterCategory.JobTitle = JobTitleTxt.Text;
            //}
            //if (Convert.ToInt32(IsActiveDDL.SelectedValue) != -1)
            //{
            //    filterCategory.IsActive = Convert.ToInt32(IsActiveDDL.SelectedValue);
            //}
            //LV2.DataSource = repo.SearchCategory(filterCategory).ToList();
            //LV2.DataBind();
        }

        protected void LV2_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            (LV2.FindControl("DataPager1") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            this.bindLV2();
        }

        protected void NumberOfRecordsDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetPageSize();
        }
        protected void SetPageSize()
        {
            DropDownList ddl = (LV2.FindControl("NumberOfRecordsDDL") as DropDownList);
            DataPager pager = (LV2.FindControl("DataPager1") as DataPager);
            if (pager != null)
                pager.PageSize = Convert.ToInt32(ddl.SelectedValue);
            bindLV2();
        }

        protected void ResetBtn_Click(object sender, EventArgs e)
        {
            NameTxt.Text = "";
            IsActiveDDL.SelectedIndex = 0;
            DropDownList ddl = (LV2.FindControl("NumberOfRecordsDDL") as DropDownList);
            ddl.SelectedIndex = 0;
            SetPageSize();
            bindLV2();
        }
    }
}