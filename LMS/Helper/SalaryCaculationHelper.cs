using LMS.Models;
using LMS.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Helper
{
    public class SalaryCaculationHelper
    {
        EmployeeJobScheduleRepo EmployeeJobScheduleRepo = new EmployeeJobScheduleRepo();
        public WeeklyPaymentResult CalculateWeeklySchedulePayment(List<EmployeeJobSchedule> schedules)
        {
            //List<EmployeeJobSchedule> OrderedEmployeeJobSchedule = schedules
            //    .OrderBy(s => s.StartWeekDayId)
            //    .ThenBy(s => s.StartTime)
            //    .ToList();

            List<WeeklyPaymentByJob> weeklyPaymentByJobs;
            foreach (EmployeeJobSchedule employeeJobSchedule in schedules)
            {
                List<EmployeeJobSchedule> employeeJobSchedules= EmployeeJobScheduleRepo.GetScheduleByEmployeeId(employeeJobSchedule.JobScheduleId, employeeJobSchedule.EmployeeJob.EmployeeId);
                weeklyPaymentByJobs = new List<WeeklyPaymentByJob>();
                foreach(EmployeeJobSchedule EmployeeSchedule in employeeJobSchedules)
                {
                    WeeklyPaymentByJob weeklyPaymentByJob = new WeeklyPaymentByJob();
                    weeklyPaymentByJob.EmployeeId = employeeJobSchedule.EmployeeJob.EmployeeId;
                    weeklyPaymentByJob.JobTitleId = employeeJobSchedule.EmployeeJob.JobTitleId;
                    weeklyPaymentByJob.JobTitle = employeeJobSchedule.EmployeeJob.JobTitle.Title;
                    weeklyPaymentByJob.NormalRate = employeeJobSchedule.EmployeeJob.Rate;
                    weeklyPaymentByJob.OvertimeHours = employeeJobSchedule.EmployeeJob.OverTimeRate;
                    weeklyPaymentByJob.EmployeeName = employeeJobSchedule.EmployeeJob.Employee.Name;
                    //weeklyPaymentByJob.
                }




            }



                return null;








            //    double runningHours = 0;

            //    var result = new WeeklyPaymentResult();

            //    foreach (var shift in ordered)
            //    {
            //      //  double shiftHours = shift.TotalTime.TotalHours;
            //        double shiftHours = shift.TotalTime.TotalHours;

            //        double normal = 0;
            //        double overtime = 0;

            //        if (runningHours < 40)
            //        {
            //            normal = Math.Min(shiftHours, 40 - runningHours);
            //            overtime = shiftHours - normal;
            //        }
            //        else
            //        {
            //            overtime = shiftHours;
            //        }

            //        double amount =
            //            (normal * shift.EmployeeJob.Rate) +
            //            (overtime * shift.EmployeeJob.OverTimeRate);

            //        // ---- FIND OR CREATE JOB BUCKET ----
            //        var jobBucket = result.Jobs
            //            .FirstOrDefault(j => j.JobTitleId == shift.EmployeeJob.JobTitleId);

            //        if (jobBucket == null)
            //        {
            //            jobBucket = new WeeklyPaymentByJob
            //            {
            //                JobTitleId = shift.EmployeeJob.JobTitleId,
            //                JobTitle = shift.EmployeeJob.JobTitle?.Title ?? ""
            //            };

            //            result.Jobs.Add(jobBucket);
            //        }

            //        // ---- ADD TO JOB TOTALS ----
            //        jobBucket.NormalHours += normal;
            //        jobBucket.OvertimeHours += overtime;
            //        jobBucket.TotalAmount += amount;

            //        // ---- ADD TO OVERALL TOTALS ----
            //        runningHours += shiftHours;

            //        result.NormalHours += normal;
            //        result.OvertimeHours += overtime;
            //        result.TotalHours += shiftHours;
            //        result.TotalAmount += amount;
            //    }

            //    return result;
            //}


        }
        public List<EmployeeJobSchedule> GetEmployeeJobSchedules(int EmployeeId)
        {
            //EmployeeJobScheduleRepo.GetScheduleByEmployeeJob(EmployeeId);
            return null;

        }
    }
}