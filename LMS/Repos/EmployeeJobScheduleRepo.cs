using LMS.DTO;
using LMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LMS.Repos
{
    public class EmployeeJobScheduleRepo
    {
        ApplicationDbContext _Context;
        JobTitleRepo JobTitleRepo;
        EmployeeRepo employeeRepo;
        public EmployeeJobScheduleRepo()
        {
            _Context = new ApplicationDbContext();
            JobTitleRepo = new JobTitleRepo();
            employeeRepo = new EmployeeRepo();
        }
        public List<EmployeeJobSchedule> GetAll(int JobScheduleId)
        {
            ApplicationDbContext _Context2 = new ApplicationDbContext();
            try
            {
                List<EmployeeJobSchedule> employeeJobSchedules = _Context2.EmployeeJobSchedules.Include("EmployeeJob").Include("JobSchedule").Where(e=> e.JobScheduleId == JobScheduleId).OrderBy(e => (e.StartWeekDayId + 3) % 7).ThenBy(e => e.StartTime).ToList();
                return CalculateTimeAndWages(employeeJobSchedules);
            }
            catch
            {
                return null;
            }

        }
        public List<EmployeeJobSchedule> GetActive(int JobScheduleId)
        {
            ApplicationDbContext _Context2 = new ApplicationDbContext();
            try
            {
                List<EmployeeJobSchedule> employeeJobSchedules = GetAll(JobScheduleId);// _Context2.EmployeeJobSchedules.Include("EmployeeJob").Include("JobSchedule").Where(e => e.IsActive == true && e.JobScheduleId == JobScheduleId).OrderBy(e =>(e.StartWeekDayId + 3) % 7).ThenBy(e=>e.StartTime).ToList();
                List<EmployeeJobSchedule> CalculatedEmployeeJobSchedules= CalculateTimeAndWages(employeeJobSchedules);
                return CalculatedEmployeeJobSchedules.Where(e => e.IsActive == true).ToList(); 
            }
            catch
            {
                return null;
            }

        }
        public List<EmployeeJobSchedule> GetActiveByJobId(int JobScheduleId,int JobId)
        {
            ApplicationDbContext _Context2 = new ApplicationDbContext();
            try
            {
                List<EmployeeJobSchedule> employeeJobSchedules = GetAll(JobScheduleId);// _Context2.EmployeeJobSchedules.Include("EmployeeJob").Include("JobSchedule").Where(e => e.IsActive == true && e.JobScheduleId == JobScheduleId).OrderBy(e =>(e.StartWeekDayId + 3) % 7).ThenBy(e=>e.StartTime).ToList();
                List<EmployeeJobSchedule> CalculatedEmployeeJobSchedules = CalculateTimeAndWages(employeeJobSchedules);
                return CalculatedEmployeeJobSchedules.Where(e => e.IsActive == true && e.EmployeeJob.JobTitleId==JobId).ToList();
            }
            catch
            {
                return null;
            }

        }
        public List<EmployeeJobSchedule> CalculateTimeAndWages(List<EmployeeJobSchedule> employeeJobSchedules)
        {
            TimeSpan GrandTotalTime = new TimeSpan(0,0,0);
            TimeSpan JobTotalTime= new TimeSpan(0, 0, 0);
            TimeSpan TotalAllowed = TimeSpan.FromHours(40);
            List<EmployeeJobSchedule> sortedemployeeJobSchedules = employeeJobSchedules.OrderBy(e => e.EmployeeJob.EmployeeId).ToList();
            int CurrentEployee = sortedemployeeJobSchedules[0].EmployeeJob.EmployeeId;
            foreach (EmployeeJobSchedule employeeJobSchedule in sortedemployeeJobSchedules)
            {
                if(CurrentEployee!= employeeJobSchedule.EmployeeJob.EmployeeId)
                {
                    GrandTotalTime = new TimeSpan(0, 0, 0);
                    CurrentEployee = employeeJobSchedule.EmployeeJob.EmployeeId;
                }
                employeeJobSchedule.StartWeekDay = ((DayOfWeek)employeeJobSchedule.StartWeekDayId);
                employeeJobSchedule.EndWeekDay = ((DayOfWeek)employeeJobSchedule.EndWeekDayId);
                employeeJobSchedule.EmployeeJob.JobTitle = JobTitleRepo.GetJobTitle(employeeJobSchedule.EmployeeJob.JobTitleId);
                employeeJobSchedule.EmployeeJob.Employee = employeeRepo.GetEmployee(employeeJobSchedule.EmployeeJob.EmployeeId);
                JobTotalTime = CalculateTotalTime(employeeJobSchedule.StartWeekDayId, employeeJobSchedule.StartTime, employeeJobSchedule.EndWeekDayId, employeeJobSchedule.EndTime);
                if (GrandTotalTime <= TotalAllowed)
                {
                    TimeSpan ExpectedTotalTime = GrandTotalTime + JobTotalTime; 

                    if (ExpectedTotalTime > TotalAllowed) 
                    {
                        employeeJobSchedule.TotalNormalTime = TotalAllowed - GrandTotalTime;  
                        employeeJobSchedule.TotalOverTime = JobTotalTime - employeeJobSchedule.TotalNormalTime;
                    }
                    else
                    {
                        employeeJobSchedule.TotalNormalTime= JobTotalTime;
                        employeeJobSchedule.TotalOverTime = new TimeSpan(0, 0, 0);
                    }
                       
                }
                else
                {
                    employeeJobSchedule.TotalNormalTime= new TimeSpan(0, 0, 0);
                    employeeJobSchedule.TotalOverTime = JobTotalTime;
                }
                GrandTotalTime += JobTotalTime;
                employeeJobSchedule.TotalTimeinWords = FormatDuration(employeeJobSchedule.TotalNormalTime);
                employeeJobSchedule.TotalOverTimeinWords = FormatDuration(employeeJobSchedule.TotalOverTime);
                employeeJobSchedule.NormalRate = employeeJobSchedule.EmployeeJob.Rate;
                employeeJobSchedule.OverTimeRate= employeeJobSchedule.EmployeeJob.OverTimeRate;

                employeeJobSchedule.TotalNormalAmount = employeeJobSchedule.TotalNormalTime.TotalHours * employeeJobSchedule.NormalRate;
                employeeJobSchedule.TotalOverTimeAmount = employeeJobSchedule.TotalOverTime.TotalHours * employeeJobSchedule.OverTimeRate;
            }
            return employeeJobSchedules;
        }
        public double GetTotalExpenses(int JobScheduleId)
        {
            try
            {
                List<EmployeeJobSchedule> employeeJobSchedules = GetEmployeeJobSchedules(JobScheduleId);
                double Totalamount = employeeJobSchedules.Sum(e => e.TotalNormalAmount) + employeeJobSchedules.Sum(e => e.TotalOverTimeAmount);
                return Totalamount;
            }
            catch
            {
                return 0;
            }

        }
        public List<EmployeeJobSchedule> GetEmployeeJobSchedules(int JobScheduleId)
        {
            ApplicationDbContext _Context2=new ApplicationDbContext();
            try
            {
                
                List<EmployeeJobSchedule> employeeJobSchedules = _Context2.EmployeeJobSchedules.Include("EmployeeJob").Include("JobSchedule").Where(e => e.IsActive == true && e.JobScheduleId== JobScheduleId).OrderBy(e => (e.StartWeekDayId + 3) % 7).ThenBy(e => e.StartTime).ToList();
                return CalculateTimeAndWages(employeeJobSchedules);
            }
            catch
            {
                return null;
            }

        }
        public List<EmployeeJobSchedule> GetScheduleByEmployeeJob(int JobScheduleId, int EmployeeJobId)
        {
            if(EmployeeJobId==0)
            {
                return GetEmployeeJobSchedules(JobScheduleId);
            }
            ApplicationDbContext _Context2 = new ApplicationDbContext();
            try
            {
                //List<EmployeeJobSchedule> employeeJobSchedules = _Context2.EmployeeJobSchedules.Include("EmployeeJob").Include("JobSchedule").Where(e => e.EmployeeJobId == EmployeeJobId && e.JobScheduleId==JobScheduleId).OrderBy(e => (e.StartWeekDayId + 3) % 7).ThenBy(e => e.StartTime).ToList();
                //if (employeeJobSchedules.Count!=0)
                //    return CalculateTimeAndWages(employeeJobSchedules);
                //else
                //    return null;
                List<EmployeeJobSchedule> employeeJobSchedules = GetAll(JobScheduleId);// _Context2.EmployeeJobSchedules.Include("EmployeeJob").Include("JobSchedule").Where(e => e.IsActive == true && e.JobScheduleId == JobScheduleId).OrderBy(e =>(e.StartWeekDayId + 3) % 7).ThenBy(e=>e.StartTime).ToList();
                List<EmployeeJobSchedule> CalculatedEmployeeJobSchedules = CalculateTimeAndWages(employeeJobSchedules);
                return CalculatedEmployeeJobSchedules.Where(e => e.EmployeeJobId == EmployeeJobId && e.JobScheduleId == JobScheduleId).ToList();
            }
            catch(Exception ex)
            {
                return null;
            }

        }
        public List<EmployeeWeeklyJobScheduleDto> GetWeeklyEmployeeJobSchedule(int JobScheduleId)
        {
            ApplicationDbContext _Context2 = new ApplicationDbContext();
            try
            {
                //List<EmployeeJobSchedule> employeeJobSchedules = _Context2.EmployeeJobSchedules.Include("EmployeeJob").Include("JobSchedule").Where(e => e.EmployeeJobId == EmployeeJobId && e.JobScheduleId==JobScheduleId).OrderBy(e => (e.StartWeekDayId + 3) % 7).ThenBy(e => e.StartTime).ToList();
                //if (employeeJobSchedules.Count!=0)
                //else
                //    return null;
                List<EmployeeJobSchedule> employeeJobSchedules = GetActive(JobScheduleId);// _Context2.EmployeeJobSchedules.Include("EmployeeJob").Include("JobSchedule").Where(e => e.IsActive == true && e.JobScheduleId == JobScheduleId).OrderBy(e =>(e.StartWeekDayId + 3) % 7).ThenBy(e=>e.StartTime).ToList();
                List<EmployeeJobSchedule> CalculatedEmployeeJobSchedules = CalculateTimeAndWages(employeeJobSchedules);
                List<Employee> Employee = employeeRepo.GetActive();
                List<EmployeeWeeklyJobScheduleDto> employeeWeeklyJobScheduleDtos = new List<EmployeeWeeklyJobScheduleDto>();
                foreach(Employee emp in Employee)
                {                //    return CalculateTimeAndWages(employeeJobSchedules);
                    EmployeeWeeklyJobScheduleDto employeeWeeklyJobScheduleDto = new EmployeeWeeklyJobScheduleDto();
                    employeeWeeklyJobScheduleDto.EmployeeId = emp.Id;
                    employeeWeeklyJobScheduleDto.EmployeeName = emp.Name;
                    employeeWeeklyJobScheduleDto.EmployeeJobSchedules = CalculatedEmployeeJobSchedules.Where(e => e.EmployeeJob.EmployeeId == emp.Id).ToList();
                    employeeWeeklyJobScheduleDtos.Add(employeeWeeklyJobScheduleDto);
                }
                return employeeWeeklyJobScheduleDtos;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public List<EmployeeWeeklyJobScheduleDto> GetWeeklyEmployeeJobScheduleByJob(int JobScheduleId,int JobId)
        {
            ApplicationDbContext _Context2 = new ApplicationDbContext();
            try
            {
                //List<EmployeeJobSchedule> employeeJobSchedules = _Context2.EmployeeJobSchedules.Include("EmployeeJob").Include("JobSchedule").Where(e => e.EmployeeJobId == EmployeeJobId && e.JobScheduleId==JobScheduleId).OrderBy(e => (e.StartWeekDayId + 3) % 7).ThenBy(e => e.StartTime).ToList();
                //if (employeeJobSchedules.Count!=0)
                //else
                //    return null;
                List<EmployeeJobSchedule> employeeJobSchedules = GetActive(JobScheduleId);// _Context2.EmployeeJobSchedules.Include("EmployeeJob").Include("JobSchedule").Where(e => e.IsActive == true && e.JobScheduleId == JobScheduleId).OrderBy(e =>(e.StartWeekDayId + 3) % 7).ThenBy(e=>e.StartTime).ToList();
                List<EmployeeJobSchedule> CalculatedEmployeeJobSchedules = CalculateTimeAndWages(employeeJobSchedules);
                List<Employee> Employee = employeeRepo.GetActive();
                List<EmployeeWeeklyJobScheduleDto> employeeWeeklyJobScheduleDtos = new List<EmployeeWeeklyJobScheduleDto>();
                foreach (Employee emp in Employee)
                {                //    return CalculateTimeAndWages(employeeJobSchedules);
                    EmployeeWeeklyJobScheduleDto employeeWeeklyJobScheduleDto = new EmployeeWeeklyJobScheduleDto();
                    employeeWeeklyJobScheduleDto.EmployeeId = emp.Id;
                    employeeWeeklyJobScheduleDto.EmployeeName = emp.Name;
                    employeeWeeklyJobScheduleDto.EmployeeJobSchedules = CalculatedEmployeeJobSchedules.Where(e => e.EmployeeJob.EmployeeId == emp.Id && e.EmployeeJob.JobTitleId==JobId).ToList();
                    employeeWeeklyJobScheduleDtos.Add(employeeWeeklyJobScheduleDto);
                }
                return employeeWeeklyJobScheduleDtos;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public List<EmployeeJobSchedule> GetScheduleByEmployeeId(int JobScheduleId, int EmployeeId)
        {
            ApplicationDbContext _Context2 = new ApplicationDbContext();
            try
            {
                //List<EmployeeJobSchedule> employeeJobSchedules = _Context2.EmployeeJobSchedules.Include("EmployeeJob").Include("JobSchedule").Where(e => e.EmployeeJob.EmployeeId == EmployeeId && e.JobScheduleId == JobScheduleId).OrderBy(e => (e.StartWeekDayId + 3) % 7).ThenBy(e => e.StartTime).ToList();
                //return CalculateTimeAndWages(employeeJobSchedules);
                List<EmployeeJobSchedule> employeeJobSchedules = GetAll(JobScheduleId);// _Context2.EmployeeJobSchedules.Include("EmployeeJob").Include("JobSchedule").Where(e => e.IsActive == true && e.JobScheduleId == JobScheduleId).OrderBy(e =>(e.StartWeekDayId + 3) % 7).ThenBy(e=>e.StartTime).ToList();
                List<EmployeeJobSchedule> CalculatedEmployeeJobSchedules = CalculateTimeAndWages(employeeJobSchedules);
                return CalculatedEmployeeJobSchedules.Where(e => e.EmployeeJob.EmployeeId == EmployeeId && e.JobScheduleId == JobScheduleId).ToList();
            }
            catch
            {
                return null;
            }

        }
        public bool Add(EmployeeJobSchedule entity)
        {
            try
            {
                _Context.EmployeeJobSchedules.Add(entity);
                _Context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static TimeSpan CalculateTotalTime( int startDay, TimeSpan startTime,
    int endDay, TimeSpan endTime)
        {
            DateTime baseDate = new DateTime(2024, 1, 1); // any fixed Monday

            DateTime start = baseDate.AddDays(startDay).Add(startTime);
            DateTime end = baseDate.AddDays(endDay).Add(endTime);

            // If end is before start → next week
            if (end < start)
                end = end.AddDays(7);

            return end - start;
        }
        public static string FormatDuration(TimeSpan duration)
        {
            int days = duration.Days;
            int hours = duration.Hours;
            int minutes = duration.Minutes;

            List<string> parts = new List<string>();

            if (days > 0)
                parts.Add($"{days} Day{(days > 1 ? "s" : "")}");

            if (hours > 0)
                parts.Add($"{hours} Hour{(hours > 1 ? "s" : "")}");

            if (minutes > 0)
                parts.Add($"{minutes} Minute{(minutes > 1 ? "s" : "")}");

            return string.Join(" ", parts);
        }
        public bool Update(EmployeeJobSchedule entity)
        {
            try
            {
                EmployeeJobSchedule result = _Context.EmployeeJobSchedules.FirstOrDefault(e => e.Id == entity.Id);
                if (result == null)
                {
                    return false;
                }
                result.JobScheduleId = entity.JobScheduleId;
                result.EmployeeJobId = entity.EmployeeJobId;
                result.StartWeekDayId = entity.StartWeekDayId;
                result.StartTime = entity.StartTime;
                result.EndWeekDayId = entity.EndWeekDayId;
                result.EndTime = entity.EndTime;
                result.IsActive = entity.IsActive;
                _Context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool Delete(int id)
        {
            try
            {
                EmployeeJobSchedule result = _Context.EmployeeJobSchedules.FirstOrDefault(e => e.Id == id);
                if (result == null)
                {
                    return false;
                }
                _Context.EmployeeJobSchedules.Remove(result);
                _Context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public class FilterEmployeeJobSchedule
        {
            public string EmployeeName { get; set; }
            public string JobTitle { get; set; }
            public int IsActive { get; set; }

        }
        //public IQueryable<EmployeeJobSchedule> SearchCategory(FilterEmployeeJobSchedule filter)
        //{
        //    IQueryable<EmployeeJobSchedule> query = _Context.Set<EmployeeJobSchedule>();
        //    // assuming that you return all records when nothing is specified in the filter

        //    if (!string.IsNullOrEmpty(filter.JobTitle))
        //    {
        //        query = query.Include("Employee").Include("JobTitle").Where(t =>
        //            t.JobTitle.Title.Contains(filter.JobTitle));
        //    }
        //    if (!string.IsNullOrEmpty(filter.EmployeeName))
        //    {
        //        query = query.Include("Employee").Include("JobTitle").Where(t =>
        //            t.Employee.Name.Contains(filter.EmployeeName));
        //    }
        //    if (filter.IsActive >= 0)
        //    {
        //        bool val = (filter.IsActive == 1 ? true : false);
        //        query = query.Include("Employee").Include("JobTitle").Where(t =>
        //            t.IsActive == val);
        //    }
        //    return query;
        //}
        public static DateTime GetWeekStart(DateTime date)
        {
            int diff = (7 + (date.DayOfWeek - DayOfWeek.Thursday)) % 7;
            DateTime thursday = date.Date.AddDays(-diff);

            return thursday.AddHours(7); // 7:00 AM
        }

        public static List<WeeklyPayroll> CalculatePayrollForAll(
    DateTime anyDate,
    List<Employee> employees,
    List<EmployeeJobSchedule> schedules,
    List<EmployeeJob> jobs)
        {
            DateTime weekStart = GetWeekStart(anyDate);
            DateTime weekEnd = weekStart.AddDays(7);

            var result = new List<WeeklyPayroll>();

            foreach (var emp in employees)
            {
                double runningNormalHours = 0;

                var shifts = schedules
                    .Where(s => s.EmployeeJob.EmployeeId == emp.Id)
                    .Select(s => new
                    {
                        Start = Convert.ToDateTime((s.StartWeekDay, s.StartTime, weekStart)),
                        End = Convert.ToDateTime((s.EndWeekDay, s.EndTime, weekStart)),
                        Job = jobs.First(j => j.JobTitleId == s.EmployeeJobId)
                    })
                    .Where(x => x.End > weekStart && x.Start < weekEnd)
                    .OrderBy(x => x.Start)
                    .ToList();

                foreach (var shift in shifts)
                {
                    double hours = (shift.End - shift.Start).TotalHours;

                    double normal = 0;
                    double overtime = 0;

                    if (runningNormalHours < 40)
                    {
                        double remaining = 40 - runningNormalHours;
                        normal = Math.Min(hours, remaining);
                        overtime = hours - normal;
                    }
                    else
                    {
                        overtime = hours;
                    }

                    // find or create payroll row for this employee + job type
                    var payroll = result.FirstOrDefault(p =>
                        p.EmployeeId == emp.Id &&
                        p.JobTypeId == shift.Job.JobTitleId);

                    if (payroll == null)
                    {
                        payroll = new WeeklyPayroll
                        {
                            EmployeeId = emp.Id,
                            JobTypeId = shift.Job.JobTitleId
                        };
                        result.Add(payroll);
                    }

                    // accumulate hours
                    payroll.NormalHours += normal;
                    payroll.OvertimeHours += overtime;

                    // accumulate amounts
                    payroll.NormalAmount += (double)normal * shift.Job.Rate;
                    payroll.OvertimeAmount += (double)overtime * shift.Job.OverTimeRate;

                    runningNormalHours += normal;
                }
            }

            return result;
        }

    }
}