using LMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LMS.Repos
{
    public class EmployeeJobRepo
    {
        ApplicationDbContext mSContext;
        public EmployeeJobRepo()
        {
            mSContext = new ApplicationDbContext();
        }
        public List<EmployeeJob> GetAll()
        {
            try
            {
                return mSContext.EmployeeJobs.Include("Employee").Include("JobTitle").ToList();
            }
            catch
            {
                return null;
            }

        }
        public List<EmployeeJob> GetActive()
        {
            try
            {
                return mSContext.EmployeeJobs.Include("Employee").Include("JobTitle").Where(e => e.IsActive == true).ToList();
            }
            catch
            {
                return null;
            }

        }
        public List<EmployeeJob> GetEmployeeJobs(int EmployeeId)
        {
            try
            {
                return mSContext.EmployeeJobs.Include("Employee").Include("JobTitle").Where(e => e.EmployeeId == EmployeeId).ToList();
            }
            catch
            {
                return null;
            }

        }
        public EmployeeJob GetEmployeeJob(int EmployeeJobId)
        {
            try
            {
                return mSContext.EmployeeJobs.Include("Employee").Include("JobTitle").Where(e => e.Id == EmployeeJobId).FirstOrDefault();
            }
            catch
            {
                return null;
            }

        }

        public bool Add(EmployeeJob entity)
        {
            try
            {
                mSContext.EmployeeJobs.Add(entity);
                mSContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool Update(EmployeeJob entity)
        {
            try
            {
                EmployeeJob result = mSContext.EmployeeJobs.FirstOrDefault(e => e.Id == entity.Id);
                if (result == null)
                {
                    return false;
                }
                result.EmployeeId = entity.EmployeeId;
                result.JobTitleId = entity.JobTitleId;
                result.Rate = entity.Rate;
                result.OverTimeRate = entity.Rate + (entity.Rate / 2);
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

                EmployeeJob result = mSContext.EmployeeJobs.FirstOrDefault(e => e.Id == id);
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
        public class FilterEmployeeJob
        {
            public string EmployeeName { get; set; }
            public string JobTitle { get; set; }
            public int IsActive { get; set; }

        }
        public IQueryable<EmployeeJob> SearchCategory(FilterEmployeeJob filter)
        {
            IQueryable<EmployeeJob> query = mSContext.Set<EmployeeJob>();
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