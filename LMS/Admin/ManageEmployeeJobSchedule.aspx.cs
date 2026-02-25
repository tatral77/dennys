using LMS.DTO;
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
    public partial class ManageEmployeeJobSchedule : System.Web.UI.Page
    {
        EmployeeJobRepo employeeJobRepo=new EmployeeJobRepo();
        JobScheduleRepo jobScheduleRepo = new JobScheduleRepo();
        EmployeeJobScheduleRepo EmployeeJobScheduleScheduleRepo = new EmployeeJobScheduleRepo();
        string lastrec = "";
        int i = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                BindCombos();
                BindListView();
            }
        }
        protected void BindListView()
        {
            int JobScheduleId = Convert.ToInt32(Request.QueryString["Id"]);
            JobSchedule js = jobScheduleRepo.GetJobSchedule(JobScheduleId);
         
            int EmployeeJobId = Convert.ToInt32(JobTitleDDL.SelectedValue);
            List<EmployeeJobSchedule> employeeJobSchedules = new List<EmployeeJobSchedule>();
            if (EmployeeJobId==0)
                employeeJobSchedules= EmployeeJobScheduleScheduleRepo.GetEmployeeJobSchedules(JobScheduleId);
            else
                employeeJobSchedules = EmployeeJobScheduleScheduleRepo.GetScheduleByEmployeeJob(JobScheduleId, EmployeeJobId);
            if (employeeJobSchedules != null)
            {
                var data = employeeJobSchedules
                .Where(x => x.IsActive)
                .GroupBy(x => new { x.EmployeeJob.EmployeeId, x.EmployeeJob.Employee.Name })
                .Select(g => new
                {
                    EmployeeName = g.Key.Name,
                    Items = g.Select(x => new
                    {
                        x.Id,
                        x.EmployeeJob.JobTitle.Title,
                        x.StartWeekDay,
                        x.StartTime,
                        x.EndWeekDay,
                        x.EndTime,
                        x.NormalRate,
                        x.OverTimeRate,
                        x.TotalNormalTime,
                        x.TotalOverTime,
                        x.TotalNormalAmount,
                        x.TotalOverTimeAmount,
                        x.TotalTimeinWords,
                        x.TotalOverTimeinWords
                    }).ToList(),
                    WeeklyTotal = g.Sum(x => x.TotalNormalAmount + x.TotalOverTimeAmount)
                })
                .ToList();
                double GrandTotal = 0;
                foreach (EmployeeJobSchedule ejs in employeeJobSchedules)
                {
                    GrandTotal += ejs.TotalNormalAmount;
                    GrandTotal += ejs.TotalOverTimeAmount;
                }
                double TotalBudget= (js.ForcastedSale * js.Percentage) / 100;
                Budget.InnerText ="Total Budget (" + TotalBudget + ")    "   + "Remaining Budget(" + (((js.ForcastedSale * js.Percentage) / 100) - EmployeeJobScheduleScheduleRepo.GetTotalExpenses(JobScheduleId)) + ")";
                lvEmployees.DataSource = data;// EmployeeJobScheduleScheduleRepo.GetEmployeeJobSchedules(JobScheduleId);
                lvEmployees.DataBind();
            }
        }
        protected string AddGroupingRow()
        {
            string currentSemesterValue = Convert.ToString(Eval("EmployeeJob.Employee.Name"));

            if (currentSemesterValue != lastrec)
            {
                lastrec = currentSemesterValue;
                i++;
                return String.Format("<tr class=group><td colspan=10>" + Eval("EmployeeJob.Employee.Name") + "</td><tr>");

            }
            else return String.Empty;
        }
        protected void BindCombos()
        {
           
            DayOfWeek startDay = DayOfWeek.Thursday;
            DayOfWeek endDay = DayOfWeek.Wednesday;

            DayOfWeek current = startDay;

            do
            {
                // Text = Day Name, Value = Day ID (0–6)
                StartWeekDayDDL.Items.Add(
               
                    new ListItem(current.ToString(), ((int)current).ToString())
                );
                EndWeekDayDDL.Items.Add(

                    new ListItem(current.ToString(), ((int)current).ToString())
                );
                if (current == endDay)
                    break;

                current = (DayOfWeek)(((int)current + 1) % 7);

            } while (true);
            List<EmployeeJob> employeeJobs = employeeJobRepo.GetActive();
            if(employeeJobs!=null)
            {
                foreach(EmployeeJob ej in employeeJobs)
                {
                    string Emloyee=ej.Employee.Name + "( " + ej.JobTitle.Title + ")";
                    JobTitleDDL.Items.Add(

                     new ListItem(Emloyee,ej.Id.ToString()));
                }
            }
        }
        protected void StartWeekDayDDL_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void NumberOfRecordsDDL_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            int StartDay= Convert.ToInt32(StartWeekDayDDL.SelectedValue);
            int Endday= Convert.ToInt32(EndWeekDayDDL.SelectedValue);
            TimeSpan StartTime= TimeSpan.Parse(StartTimeTxt.Text);
            TimeSpan EndTime = TimeSpan.Parse(EndTimeTxt.Text);
            TimeSpan TimeLimit = new TimeSpan(7, 0, 0);
            if (StartDay == 4 && Endday == 4 && EndTime > TimeLimit)
            {
                ErrorLbl.Text = "End Time should be less than Thursday 7:00 AM";
            }
            else
            {
                int JobScheduleId = Convert.ToInt32(Request.QueryString["Id"]);
                EmployeeJobSchedule employeeJobSchedule = new EmployeeJobSchedule();
                employeeJobSchedule.JobScheduleId = JobScheduleId;
                employeeJobSchedule.EmployeeJobId = Convert.ToInt32(JobTitleDDL.SelectedValue);
                employeeJobSchedule.StartWeekDayId = StartDay;
                employeeJobSchedule.EndWeekDayId = Endday;
                employeeJobSchedule.StartTime = StartTime;
                employeeJobSchedule.EndTime = EndTime;
                employeeJobSchedule.IsActive = true;
                EmployeeJobScheduleScheduleRepo.Add(employeeJobSchedule);
                BindListView();
                ErrorLbl.Text = "";
            }

        }
        protected void LV_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            //int id = Convert.ToInt32(Request.QueryString["Id"]);
            //TextBox name = (TextBox)LV.InsertItem.FindControl("NameTxt");
            //TextBox email = (TextBox)LV.InsertItem.FindControl("EmailTxt");
            //TextBox phone = (TextBox)LV.InsertItem.FindControl("PhoneTxt");
            //DropDownList IsActiveDDL = (DropDownList)LV.InsertItem.FindControl("IsActiveDDL");
            //Employee employee = new Employee();
            //employee.LocationId = id;
            //employee.Name = Convert.ToString(name.Text);
            //employee.Email = Convert.ToString(email.Text);
            //employee.Phone = Convert.ToString(phone.Text);
            //employee.IsActive = Convert.ToBoolean(IsActiveDDL.SelectedValue);
            //employee.CreatedAt = DateTime.Now;
            //repo.Add(employee);
            //LV.EditIndex = -1;
            //LV.InsertItemPosition = InsertItemPosition.None;
            //BingListView();
            //e.Cancel = true;
            //Response.Redirect("ManageEmployees.aspx?id=" + id);
        }

        protected void LV_ItemCreated(object sender, ListViewItemEventArgs e)
        {


        }

        protected void LV_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            lvEmployees.EditIndex = -1;
            lvEmployees.InsertItemPosition = InsertItemPosition.None;
            lvEmployees.SelectedIndex = -1;
            BindListView();
            e.Cancel = true;

        }

        protected void LV_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            lvEmployees.InsertItemPosition = InsertItemPosition.None;
            lvEmployees.SelectedIndex = -1;
            lvEmployees.EditIndex = e.NewEditIndex;
            BindListView();
            e.Cancel = true;
        }

        protected void LV_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            //int id = Convert.ToInt32(Request.QueryString["Id"]);
            ////DepartmentRepo dr = new DepartmentRepo(); 
            //HiddenField Id = LV.EditItem.FindControl("HidId") as HiddenField;
            //TextBox name = (TextBox)LV.EditItem.FindControl("NameTxt");
            //TextBox email = (TextBox)LV.EditItem.FindControl("EmailTxt");
            //TextBox phone = (TextBox)LV.EditItem.FindControl("PhoneTxt");
            //DropDownList IsActiveDDL = (DropDownList)LV.EditItem.FindControl("IsActiveDDL");
            //Employee employee = new Employee();
            //employee.Id = Convert.ToInt32(Id.Value);
            //employee.Name = Convert.ToString(name.Text);
            //employee.Email = Convert.ToString(email.Text);
            //employee.Phone = Convert.ToString(phone.Text);
            //employee.IsActive = Convert.ToBoolean(IsActiveDDL.SelectedValue);
            //employee.UpdatedAt = DateTime.Now;
            //repo.Update(employee);
            //LV.EditIndex = -1;
            //LV.InsertItemPosition = InsertItemPosition.None;
            //bindLV();
            //e.Cancel = true;
            //Response.Redirect("ManageEmployees.aspx?id=" + id);

        }

        protected void LV_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (lvEmployees.EditIndex == (e.Item as ListViewDataItem).DataItemIndex)
            {
                //    DropDownList MinistryDDL = e.Item.FindControl("MinistryDDL") as DropDownList;
                //    HiddenField HidMinistriesId = (e.Item.FindControl("HidMinistriesId") as HiddenField);
                //    MinistryDDL.SelectedValue = HidMinistriesId.Value.ToString();

                //DropDownList IsActiveDDL = e.Item.FindControl("IsActiveDDL") as DropDownList;
                //HiddenField HidIsActive = (e.Item.FindControl("HidIsActive") as HiddenField);
                //IsActiveDDL.SelectedValue = HidIsActive.Value.ToString();
            }
        }

        protected string GetStatus(int id)
        {
            if (id == 0)
                return "No";
            else
                return "Yes";

        }
        protected void lvDays_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteRow")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                EmployeeJobScheduleScheduleRepo.Delete(id);
                lvEmployees.EditIndex = -1;
                lvEmployees.InsertItemPosition = InsertItemPosition.None;
                BindListView();
            }
        }
        protected void LV_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            //int id = Convert.ToInt32(e.CommandArgument);

            //if (!string.IsNullOrEmpty(Id.Value))
            //{
            //    int id = Convert.ToInt32(Id.Value);

            //    EmployeeJobScheduleScheduleRepo.Delete(id);

            //    lvEmployees.EditIndex = -1;
            //    lvEmployees.InsertItemPosition = InsertItemPosition.None;
            //    BindListView();
            //    e.Cancel = true;
            //   // Response.Redirect("ManageEmployees.aspx");
            //}
        }

        protected void JobTitleDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindListView();
            //int JobScheduleId = Convert.ToInt32(Request.QueryString["Id"]);
            //int EmployeeJobId = Convert.ToInt32(JobTitleDDL.SelectedValue);
            //var employeeJobSchedules = EmployeeJobScheduleScheduleRepo.GetScheduleByEmployeeJob(JobScheduleId,EmployeeJobId);
            //var data = employeeJobSchedules
            //.Where(x => x.IsActive)
            //.GroupBy(x => new { x.EmployeeJob.EmployeeId, x.EmployeeJob.Employee.Name })
            //.Select(g => new
            //{
            //    EmployeeName = g.Key.Name,
            //    Items = g.Select(x => new
            //    {
            //        x.Id,
            //        x.EmployeeJob.JobTitle.Title,
            //        x.StartWeekDay,
            //        x.StartTime,
            //        x.EndWeekDay,
            //        x.EndTime,
            //        x.Rate,
            //        x.TotalTime,
            //        x.TotalAmount,
            //        x.TotalTimeinWords
            //    }).ToList(),
            //    WeeklyTotal = g.Sum(x => x.TotalAmount)
            //})
            //.ToList();
            //lvEmployees.DataSource = data;// EmployeeJobScheduleScheduleRepo.GetEmployeeJobSchedules(JobScheduleId);
            //lvEmployees.DataBind();
        }
    }
}