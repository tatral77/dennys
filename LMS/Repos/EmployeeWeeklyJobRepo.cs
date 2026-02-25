using LMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LMS.Repos
{
    public class EmployeeWeeklyJobRepo
    {
        ApplicationDbContext mSContext;
        public EmployeeWeeklyJobRepo()
        {
            mSContext = new ApplicationDbContext();
        }
        public List<EmployeeWeeklyJob> GetAll()
        {
            try
            {
                return mSContext.EmployeeWeeklyJobs.Include("Employee").Include("JobTitle").ToList();
            }
            catch
            {
                return null;
            }

        }
        public List<EmployeeWeeklyJob> GetActive()
        {
            try
            {
                return mSContext.EmployeeWeeklyJobs.Include("Employee").Include("JobTitle").Where(e => e.IsActive == true).ToList();
            }
            catch
            {
                return null;
            }

        }
        public List<EmployeeWeeklyJob> GetEmployeeWeeklyJobs(int EmployeeId)
        {
            try
            {
                return mSContext.EmployeeWeeklyJobs.Include("Employee").Include("JobTitle").Where(e => e.EmployeeId == EmployeeId).ToList();
            }
            catch
            {
                return null;
            }

        }
        public EmployeeWeeklyJob GetEmployeeWeeklyJob(int EmployeeWeeklyJobId)
        {
            try
            {
                return mSContext.EmployeeWeeklyJobs.Include("Employee").Include("JobTitle").Where(e => e.Id == EmployeeWeeklyJobId).FirstOrDefault();
            }
            catch
            {
                return null;
            }

        }

        public bool Add(EmployeeWeeklyJob entity)
        {
            try
            {
                mSContext.EmployeeWeeklyJobs.Add(entity);
                mSContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool Update(EmployeeWeeklyJob entity)
        {
            try
            {
                EmployeeWeeklyJob result = mSContext.EmployeeWeeklyJobs.FirstOrDefault(e => e.Id == entity.Id);
                if (result == null)
                {
                    return false;
                }
                result.EmployeeId = entity.EmployeeId;
                result.JobTitleId = entity.JobTitleId;
                result.WeeklySalary = entity.WeeklySalary;
                result.Remarks = entity.Remarks;
                result.IsActive = entity.IsActive;
                result.UpdatedAt = DateTime.Now;
                mSContext.SaveChanges();
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

                EmployeeWeeklyJob result = mSContext.EmployeeWeeklyJobs.FirstOrDefault(e => e.Id == id);
                if (result == null)
                {
                    return false;
                }

                result.ArchivedAt = DateTime.Now;


                mSContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public class FilterEmployeeWeeklyJob
        {
            public string EmployeeName { get; set; }
            public string JobTitle { get; set; }
            public int IsActive { get; set; }

        }
        public IQueryable<EmployeeWeeklyJob> SearchCategory(FilterEmployeeWeeklyJob filter)
        {
            IQueryable<EmployeeWeeklyJob> query = mSContext.Set<EmployeeWeeklyJob>();
            // assuming that you return all records when nothing is specified in the filter

            if (!string.IsNullOrEmpty(filter.JobTitle))
            {
                query = query.Include("Employee").Include("JobTitle").Where(t =>
                    t.JobTitle.Title.Contains(filter.JobTitle));
            }
            if (!string.IsNullOrEmpty(filter.EmployeeName))
            {
                query = query.Include("Employee").Include("JobTitle").Where(t =>
                    t.Employee.Name.Contains(filter.EmployeeName));
            }
            if (filter.IsActive >= 0)
            {
                bool val = (filter.IsActive == 1 ? true : false);
                query = query.Include("Employee").Include("JobTitle").Where(t =>
                    t.IsActive == val);
            }
            return query;
        }
    }
}