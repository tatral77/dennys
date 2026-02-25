using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LMS.Repos
{
    public class JobTitleRepo
    {
        ApplicationDbContext mSContext;
        public JobTitleRepo()
        {
            mSContext = new ApplicationDbContext();
        }
        public List<JobTitle> GetAll()
        {
            try
            {
                return mSContext.JobTitles.ToList();
            }
            catch
            {
                return null;
            }

        }
        public List<JobTitle> GetActive()
        {
            try
            {
                return mSContext.JobTitles.Where(e => e.IsActive == true).ToList();
            }
            catch
            {
                return null;
            }

        }
        public JobTitle GetJobTitle(int JobTitleId)
        {
            try
            {
                return mSContext.JobTitles.Where(e => e.Id == JobTitleId).FirstOrDefault();
            }
            catch
            {
                return null;
            }

        }

        public bool Add(JobTitle entity)
        {
            try
            {
                mSContext.JobTitles.Add(entity);
                mSContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool Update(JobTitle entity)
        {
            try
            {
                JobTitle result = mSContext.JobTitles.FirstOrDefault(e => e.Id == entity.Id);
                if (result == null)
                {
                    return false;
                }
                result.Title = entity.Title;
                result.IsActive = entity.IsActive;
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

                JobTitle result = mSContext.JobTitles.FirstOrDefault(e => e.Id == id);
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
        public class FilterJobTitle
        {
            public string JobTitle { get; set; }
            public int IsActive { get; set; }

        }
        public IQueryable<JobTitle> SearchCategory(FilterJobTitle filter)
        {
            IQueryable<JobTitle> query = mSContext.Set<JobTitle>();
            // assuming that you return all records when nothing is specified in the filter

            if (!string.IsNullOrEmpty(filter.JobTitle))
            {
                query = query.Where(t =>
                    t.Title.Contains(filter.JobTitle));
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