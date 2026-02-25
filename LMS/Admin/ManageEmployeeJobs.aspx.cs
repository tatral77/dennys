using LMS.Models;
using LMS.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using static LMS.Repos.EmployeeJobRepo;

namespace LMS.Admin
{
    public partial class ManageEmployeeJobs : System.Web.UI.Page
    {
        EmployeeJobRepo employeeJobRepo = new EmployeeJobRepo();
        EmployeeWeeklyJobRepo employeeWeeklyJobRepo = new EmployeeWeeklyJobRepo();
        EmployeeRepo emprepo = new EmployeeRepo();
        JobTitleRepo jobTitleRepo = new JobTitleRepo();
        #region Hourly Job
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindLV();
                bindLV2();
            }
        }
        protected void bindLV()
        {
            int id = Convert.ToInt32(Request.QueryString["Id"]);
            Employee emp = emprepo.GetEmployee(id);
            EmployeeDetail.InnerText = emp.Name;
            List<EmployeeJob> employee = employeeJobRepo.GetEmployeeJobs(id);
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
            DropDownList EmployeeDDL = (DropDownList)LV.InsertItem.FindControl("EmployeeDDL");
            DropDownList JobTitleDDL = (DropDownList)LV.InsertItem.FindControl("JobTitleDDL");
            TextBox RateTxt = (TextBox)LV.InsertItem.FindControl("RateTxt");
            TextBox RemarksTxt = (TextBox)LV.InsertItem.FindControl("RemarksTxt");
            DropDownList IsActiveDDL = (DropDownList)LV.InsertItem.FindControl("IsActiveDDL");
            EmployeeJob entity = new EmployeeJob();
            entity.EmployeeId = id;
            entity.JobTitleId = Convert.ToInt32(JobTitleDDL.SelectedValue);
            entity.Rate = Convert.ToDouble(RateTxt.Text);
            entity.OverTimeRate = entity.Rate + (entity.Rate / 2);
            entity.Remarks = Convert.ToString(RemarksTxt.Text);
            entity.IsActive = Convert.ToBoolean(IsActiveDDL.SelectedValue);
            entity.CreatedAt = DateTime.Now;
            employeeJobRepo.Add(entity);
            LV.EditIndex = -1;
            LV.InsertItemPosition = InsertItemPosition.None;
            bindLV();
            e.Cancel = true;
            Response.Redirect("ManageEmployeeJobs.aspx?id=" + id);
        }

        protected void LV_ItemCreated(object sender, ListViewItemEventArgs e)
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
            DropDownList EmployeeDDL = (DropDownList)LV.EditItem.FindControl("EmployeeDDL");
            DropDownList JobTitleDDL = (DropDownList)LV.EditItem.FindControl("JobTitleDDL");
            TextBox RateTxt = (TextBox)LV.EditItem.FindControl("RateTxt");
            TextBox RemarksTxt = (TextBox)LV.EditItem.FindControl("RemarksTxt");
            TextBox phone = (TextBox)LV.EditItem.FindControl("PhoneTxt");
            DropDownList IsActiveDDL = (DropDownList)LV.EditItem.FindControl("IsActiveDDL");
            EmployeeJob entity = new EmployeeJob();
            entity.Id = Convert.ToInt32(Id.Value);
            entity.EmployeeId = id;
            entity.JobTitleId = Convert.ToInt32(JobTitleDDL.SelectedValue);
            entity.Rate = Convert.ToDouble(RateTxt.Text);
            //entity.OverTimeRate = entity.Rate + (entity.Rate / 2);
            entity.Remarks = Convert.ToString(RemarksTxt.Text);
            entity.IsActive = Convert.ToBoolean(IsActiveDDL.SelectedValue);
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;
            employeeJobRepo.Update(entity);
            LV.EditIndex = -1;
            LV.InsertItemPosition = InsertItemPosition.None;
            bindLV();
            e.Cancel = true;
            Response.Redirect("ManageEmployeeJobs.aspx?id=" + id);

        }

        protected void LV_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (LV.EditIndex == (e.Item as ListViewDataItem).DataItemIndex)
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

        protected void LV_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            HiddenField Id = (HiddenField)LV.Items[e.ItemIndex].FindControl("HidId");
            if (!string.IsNullOrEmpty(Id.Value))
            {
                int id = Convert.ToInt32(Id.Value);

                employeeJobRepo.Delete(id);

                LV.EditIndex = -1;
                LV.InsertItemPosition = InsertItemPosition.None;
                bindLV();
                e.Cancel = true;
                Response.Redirect("ManageEmployeeJobs.aspx");
            }
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            FilterEmployeeJob filterCategory = new FilterEmployeeJob();
            filterCategory.IsActive = -1;
            if (!string.IsNullOrEmpty(NameTxt.Text))
            {
                filterCategory.EmployeeName = NameTxt.Text;
            }
            if (!string.IsNullOrEmpty(JobTitleTxt.Text))
            {
                filterCategory.JobTitle = JobTitleTxt.Text;
            }
            if (Convert.ToInt32(IsActiveDDL.SelectedValue) != -1)
            {
                filterCategory.IsActive = Convert.ToInt32(IsActiveDDL.SelectedValue);
            }
            LV.DataSource = employeeJobRepo.SearchCategory(filterCategory).ToList();
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
        #endregion HourlyJob

        #region Salary Job
       
        protected void bindLV2()
        {
            int EmployeeId = Convert.ToInt32(Request.QueryString["Id"]);
            List<EmployeeWeeklyJob> employeeWeeklyJobs = employeeWeeklyJobRepo.GetEmployeeWeeklyJobs(EmployeeId);
            if (employeeWeeklyJobs != null && employeeWeeklyJobs.Count>0)
                // Employee emp = emprepo.GetEmployee(EmployeeId);
                EmployeeDetail.InnerText = employeeWeeklyJobs[0].Employee.Name;
            List<EmployeeWeeklyJob> employee = employeeWeeklyJobRepo.GetEmployeeWeeklyJobs(EmployeeId);
            LV2.DataSource = employee;
            LV2.DataBind();

        }

        protected void AddButton2_Click(object sender, EventArgs e)
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
            employeeWeeklyJobRepo.Add(entity);
            LV2.EditIndex = -1;
            LV2.InsertItemPosition = InsertItemPosition.None;
            bindLV2();
            e.Cancel = true;
            Response.Redirect("ManageEmployeeJobs.aspx?id=" + EmployeeId);
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
            employeeWeeklyJobRepo.Update(entity);
            LV2.EditIndex = -1;
            LV2.InsertItemPosition = InsertItemPosition.None;
            bindLV2();
            e.Cancel = true;
            Response.Redirect("ManageEmployeeJobs.aspx?id=" + EmployeeId);

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


        protected void LV2_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {

            HiddenField Id = (HiddenField)LV2.Items[e.ItemIndex].FindControl("HidId");
            if (!string.IsNullOrEmpty(Id.Value))
            {
                int id = Convert.ToInt32(Id.Value);

                employeeWeeklyJobRepo.Delete(id);

                LV2.EditIndex = -1;
                LV2.InsertItemPosition = InsertItemPosition.None;
                bindLV2();
                e.Cancel = true;
            }
        }

        //protected void SearchBtn_Click(object sender, EventArgs e)
        //{
        //    //FilterEmployeeWeeklyJob filterCategory = new FilterEmployeeWeeklyJob();
        //    //filterCategory.IsActive = -1;
        //    //if (!string.IsNullOrEmpty(NameTxt.Text))
        //    //{
        //    //    filterCategory.EmployeeName = NameTxt.Text;
        //    //}
        //    //if (!string.IsNullOrEmpty(JobTitleTxt.Text))
        //    //{
        //    //    filterCategory.JobTitle = JobTitleTxt.Text;
        //    //}
        //    //if (Convert.ToInt32(IsActiveDDL.SelectedValue) != -1)
        //    //{
        //    //    filterCategory.IsActive = Convert.ToInt32(IsActiveDDL.SelectedValue);
        //    //}
        //    //LV2.DataSource = repo.SearchCategory(filterCategory).ToList();
        //    //LV2.DataBind();
        //}

        protected void LV2_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            (LV2.FindControl("DataPager1") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            this.bindLV2();
        }

        //protected void NumberOfRecordsDDL_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    SetPageSize();
        //}
        //protected void SetPageSize()
        //{
        //    DropDownList ddl = (LV2.FindControl("NumberOfRecordsDDL") as DropDownList);
        //    DataPager pager = (LV2.FindControl("DataPager1") as DataPager);
        //    if (pager != null)
        //        pager.PageSize = Convert.ToInt32(ddl.SelectedValue);
        //    bindLV2();
        //}

        //protected void ResetBtn_Click(object sender, EventArgs e)
        //{
        //    NameTxt.Text = "";
        //    IsActiveDDL.SelectedIndex = 0;
        //    DropDownList ddl = (LV2.FindControl("NumberOfRecordsDDL") as DropDownList);
        //    ddl.SelectedIndex = 0;
        //    SetPageSize();
        //    bindLV2();
        //}
        #endregion

    }
}