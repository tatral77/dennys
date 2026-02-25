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
    public partial class WeeklyScheduleCalendar : System.Web.UI.Page
    {
        EmployeeJobRepo employeeJobRepo = new EmployeeJobRepo();
        JobTitleRepo JobTitleRepo = new JobTitleRepo();
        JobScheduleRepo jobScheduleRepo = new JobScheduleRepo();
        EmployeeJobScheduleRepo EmployeeJobScheduleScheduleRepo = new EmployeeJobScheduleRepo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int JobScheduleId = Convert.ToInt32(Request.QueryString["Id"]);
                GenerateTable(EmployeeJobScheduleScheduleRepo.GetWeeklyEmployeeJobSchedule(JobScheduleId));
                BindCombos();
            }
        }
        protected void BindCombos()
        {
            var JobTitles = JobTitleRepo.GetActive();
            JobDDL.Items.Add(new ListItem
            {
                Value = "0",
                Text = "All"
            });
            foreach(JobTitle jt in JobTitles)
            {
                ListItem li = new ListItem();
                li.Value = jt.Id.ToString();
                li.Text = jt.Title;
                JobDDL.Items.Add(li);
            }
        }
        protected void GenerateTable(List<EmployeeWeeklyJobScheduleDto> employeeWeeklyJobScheduleDtos)
        {
            double GrandAmountTotal = 0;
            double GrandOverTimeAmountTotal = 0;
            TimeSpan GrandNormalTime = new TimeSpan(0, 0, 0);
            TimeSpan GrandOverTime = new TimeSpan(0, 0, 0);
            List<DayOfWeek> DaysOfWeek = GetWeekDays();
            string table = "<table class='table'><tr><th>Employee Name</th>";
            foreach (DayOfWeek day in DaysOfWeek)
            {
                table += "<th>" + day.ToString() + "</th>";
            }
            table += "<th>Details</th>";
            table += "</tr>";
            if (employeeWeeklyJobScheduleDtos != null)
            {
                foreach (EmployeeWeeklyJobScheduleDto schedule in employeeWeeklyJobScheduleDtos)
                {
                    TimeSpan TotalTime = new TimeSpan(0, 0, 0);
                    TimeSpan TotalOverTime = new TimeSpan(0, 0, 0);
                    double TotalAmount = 0;
                    double TotalOverTimeAmount = 0;
                    table += "<tr>";
                    table += "<td style='font-weight:bold;no-wrap'>" + schedule.EmployeeName + "</td>";
                    foreach (DayOfWeek dayOfWeek in DaysOfWeek)
                    {
                        table += "<td>";
                        List<EmployeeJobSchedule> employeeJobSchedules = schedule.EmployeeJobSchedules.Where(x => x.StartWeekDay == dayOfWeek).ToList();
                        if (employeeJobSchedules.Count != 0)
                        {

                            foreach (EmployeeJobSchedule employeeJobSchedule in employeeJobSchedules)
                            {
                                TotalTime += employeeJobSchedule.TotalNormalTime;
                                TotalOverTime += employeeJobSchedule.TotalOverTime;
                                TotalAmount += employeeJobSchedule.TotalNormalAmount;
                                TotalOverTimeAmount += employeeJobSchedule.TotalOverTimeAmount;
                                table += "<div>" + employeeJobSchedule.StartWeekDay + "(" + employeeJobSchedule.StartTime.ToString(@"hh\:mm") + " to " + employeeJobSchedule.EndTime.ToString(@"hh\:mm") + ")  <span style='font-weight:bold'>" + employeeJobSchedule.EmployeeJob.JobTitle.Title + "</span></div>";
                                if (employeeJobSchedule.TotalNormalTime > new TimeSpan(0, 0, 0))
                                    table += "<div>Time:" + FormatDuration(employeeJobSchedule.TotalNormalTime) + "</div>";
                                if (employeeJobSchedule.TotalOverTime > new TimeSpan(0, 0, 0))
                                    table += "<div>Overtime: " + FormatDuration(employeeJobSchedule.TotalOverTime) + "</div>";
                                double totalamount = employeeJobSchedule.TotalNormalAmount + employeeJobSchedule.TotalOverTimeAmount;
                                table += "<div>$" + totalamount + "</div>";
                               GrandAmountTotal += employeeJobSchedule.TotalNormalAmount;
                                GrandOverTimeAmountTotal += employeeJobSchedule.TotalOverTimeAmount;
                                GrandNormalTime += employeeJobSchedule.TotalNormalTime;
                               GrandOverTime += employeeJobSchedule.TotalOverTime;


                            }
                        }
                        else
                        {
                            table += "<span style='font-weight:bold'>OFF</span>";
                        }
                        table += "</td>";
                    }
                    table += "<td>";
                    table += "<div>Time:" + FormatDuration(TotalTime) + "</div>";
                    table += "<div>Overtime:" + FormatDuration(TotalOverTime) + "</div>";
                    table += "<div> $:" + TotalAmount + "</div>";
                    if (TotalOverTimeAmount > 0)
                        table += "<div> Overtime $:" + TotalOverTimeAmount + "</div>";
                    table += "</td>";

                }
            }
            table += "</tr></table> <hr/>";
            table += "<div class='container'><div class='row'><div class='col-md-12' style='text-align:right'>";
            table += "<span style='font-weight:bold'>Total Normal Time: </span>" + FormatDuration(GrandNormalTime) + "<br>";
            table += "<span style='font-weight:bold'>Total Over Time: </span>" + FormatDuration(GrandOverTime) + "<br>";
            table += "<span style='font-weight:bold'>Total Amount: </span>$" + GrandAmountTotal + "<br>";
            table += "<span style='font-weight:bold'>Overtime Amount: </span>$" + GrandOverTimeAmountTotal;
            table += "</div></div></div>";
            contents.InnerHtml = table;
        }
        public static string FormatDuration(TimeSpan duration)
        {
            int days = duration.Days;
            int hours = duration.Hours;
            int minutes = duration.Minutes;

            List<string> parts = new List<string>();
            if (days == 0 && hours == 0 && minutes == 0)
                return "0 Hours";
            if (days > 0)
                parts.Add($"{days} Day{(days > 1 ? "s" : "")}");

            if (hours > 0)
                parts.Add($"{hours} Hour{(hours > 1 ? "s" : "")}");

            if (minutes > 0)
                parts.Add($"{minutes} Minute{(minutes > 1 ? "s" : "")}");

            return string.Join(" ", parts);
        }

        public List<DayOfWeek> GetWeekDays()
        {
            DayOfWeek startDay = DayOfWeek.Thursday;
            DayOfWeek endDay = DayOfWeek.Wednesday;

            DayOfWeek current = startDay;
            List<DayOfWeek> DaysOfWeek = new List<DayOfWeek>();
            do
            {
                // Text = Day Name, Value = Day ID (0–6)
                DaysOfWeek.Add(current);
                if (current == endDay)
                    break;

                current = (DayOfWeek)(((int)current + 1) % 7);

            } while (true);
            return DaysOfWeek;
        }

        protected void JobDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            int JobScheduleId = Convert.ToInt32(Request.QueryString["Id"]);
            int JobId = Convert.ToInt32(JobDDL.SelectedValue);
            if(JobId==0)
                 GenerateTable(EmployeeJobScheduleScheduleRepo.GetWeeklyEmployeeJobSchedule(JobScheduleId));
            else
            {
                GenerateTable(EmployeeJobScheduleScheduleRepo.GetWeeklyEmployeeJobScheduleByJob(JobScheduleId, JobId));
            }
                
        }
    }
}