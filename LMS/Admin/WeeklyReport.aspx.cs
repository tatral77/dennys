using LMS.Helper;
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
    public partial class WeeklyReport : System.Web.UI.Page
    {
        EmployeeJobScheduleRepo repo = new EmployeeJobScheduleRepo();
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
            List<EmployeeJobSchedule> employeeJobSchedules = repo.GetActive(1);
            WeeklyPaymentResult weeklyPaymentResult = new WeeklyPaymentResult();
           // weeklyPaymentResult = new SalaryCaculationHelper().CalculateWeeklyPayment(employeeJobSchedules);
            LV.DataSource = weeklyPaymentResult.Jobs;
            LV.DataBind();

        }
    }
}