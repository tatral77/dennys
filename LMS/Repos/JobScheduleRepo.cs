using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Repos
{
    public class JobScheduleRepo
    {
        ApplicationDbContext mSContext;
        public JobScheduleRepo()
        {
            mSContext = new ApplicationDbContext();
        }
        public List<JobSchedule> GetAll()
        {
            try
            {
                return mSContext.JobSchedules.ToList();
            }
            catch
            {
                return null;
            }

        }
        public List<JobSchedule> GetActive()
        {
            try
            {
                return mSContext.JobSchedules.Where(e => e.IsActive == true).ToList();
            }
            catch
            {
                return null;
            }

        }
        public JobSchedule GetJobSchedule(int Id)
        {
            try
            {
                return mSContext.JobSchedules.Where(e => e.Id == Id).FirstOrDefault();
            }
            catch
            {
                return null;
            }

        }

        public bool Add(JobSchedule entity)
        {
            try
            {
                mSContext.JobSchedules.Add(entity);
                mSContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool Update(JobSchedule entity)
        {
            try
            {
                JobSchedule result = mSContext.JobSchedules.FirstOrDefault(e => e.Id == entity.Id);
                if (result == null)
                {
                    return false;
                }
                //result.CreatedOn = entity.CreatedOn;
                result.Description = entity.Description;
                result.ForcastedSale = entity.ForcastedSale;
                result.Percentage = entity.Percentage;
                result.IsActive = entity.IsActive;
                mSContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
                return false;
            }

        }

        public bool Delete(int id)
        {
            try
            {

                JobSchedule result = mSContext.JobSchedules.FirstOrDefault(e => e.Id == id);
                if (result == null)
                {
                    return false;
                }

                mSContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public class FilterJobSchedule
        {
            public string Description { get; set; }
            public int IsActive { get; set; }

        }
        public IQueryable<JobSchedule> SearchJobSchedule(FilterJobSchedule filter)
        {
            IQueryable<JobSchedule> query = mSContext.Set<JobSchedule>();
            // assuming that you return all records when nothing is specified in the filter

            if (!string.IsNullOrEmpty(filter.Description))
            {
                query = query.Where(t =>
                    t.Description.Contains(filter.Description));
            }
            if (filter.IsActive >= 0)
            {
                bool val = (filter.IsActive == 1 ? true : false);
                query = query.Where(t =>
                    t.IsActive == val);
            }
            return query;
        }
    }
}