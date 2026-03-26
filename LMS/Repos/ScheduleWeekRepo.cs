using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Repos
{
    public class ScheduleWeekRepo
    {
        ApplicationDbContext mSContext;
        public ScheduleWeekRepo()
        {
            mSContext = new ApplicationDbContext();
        }
        public List<ScheduleWeek> GetAll(int Year)
        {
            try
            {
                return mSContext.ScheduleWeeks.Where(e => e.Year == Year).ToList();
            }
            catch
            {
                return null;
            }

        }
        public List<ScheduleWeek> GetActive(int Year)
        {
            try
            {
                return mSContext.ScheduleWeeks.Where(e => e.Year == Year).ToList();
            }
            catch
            {
                return null;
            }

        }
        public ScheduleWeek GetScheduleWeek(int Id)
        {
            try
            {
                return mSContext.ScheduleWeeks.Where(e => e.Id == Id).FirstOrDefault();
            }
            catch
            {
                return null;
            }

        }

        public bool Add(ScheduleWeek entity)
        {
            try
            {
                mSContext.ScheduleWeeks.Add(entity);
                mSContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public bool Add(List<ScheduleWeek> entities)
        {
            try
            {
                using (mSContext)
                {
                    mSContext.ScheduleWeeks.AddRange(entities);
                    mSContext.SaveChanges(); // Inserts all tracked entities into the database
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public bool Update(ScheduleWeek entity)
        {
            try
            {
                ScheduleWeek result = mSContext.ScheduleWeeks.FirstOrDefault(e => e.Id == entity.Id);
                if (result == null)
                {
                    return false;
                }
                //result.CreatedOn = entity.CreatedOn;
                result.Year = entity.Year;
                result.WeekDecription = entity.WeekDecription;
                result.ForcastedSale = entity.ForcastedSale;
                result.Percentage = entity.Percentage;
                mSContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }

        }

        public bool Delete(int id)
        {
            try
            {

                ScheduleWeek result = mSContext.ScheduleWeeks.FirstOrDefault(e => e.Id == id);
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
        public class FilterScheduleWeek
        {
            public string Description { get; set; }
            public int IsActive { get; set; }

        }
        public IQueryable<ScheduleWeek> SearchScheduleWeek(FilterScheduleWeek filter)
        {
            IQueryable<ScheduleWeek> query = mSContext.Set<ScheduleWeek>();
            // assuming that you return all records when nothing is specified in the filter

            if (!string.IsNullOrEmpty(filter.Description))
            {
                query = query.Where(t =>
                    t.WeekDecription.Contains(filter.Description));
            }
            return query;
        }
    }
}