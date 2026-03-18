using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Repos
{
    public class RestaurantWeekRepo
    {
        ApplicationDbContext mSContext;
        public RestaurantWeekRepo()
        {
            mSContext = new ApplicationDbContext();
        }
        public List<ScheduleWeek> GetAll(int Year)
        {
            try
            {
                return mSContext.RestaurantWeeks.Where(e => e.Year == Year).ToList();
            }
            catch
            {
                return null;
            }

        }
      
        public ScheduleWeek GetRestaurantWeek(int Id)
        {
            try
            {
                return mSContext.RestaurantWeeks.Where(e => e.Id == Id).FirstOrDefault();
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
                mSContext.RestaurantWeeks.Add(entity);
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
                    mSContext.RestaurantWeeks.AddRange(entities);
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
                ScheduleWeek result = mSContext.RestaurantWeeks.FirstOrDefault(e => e.Id == entity.Id);
                if (result == null)
                {
                    return false;
                }
                //result.CreatedOn = entity.CreatedOn;
                result.WeekStartDate = entity.WeekStartDate;
                result.WeekEndDate = entity.WeekEndDate;
                mSContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool Delete(int id)
        {
            try
            {

                ScheduleWeek result = mSContext.RestaurantWeeks.FirstOrDefault(e => e.Id == id);
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
        public class FilterRestaurantWeek
        {
            public string Description { get; set; }
            public int IsActive { get; set; }

        }
        public IQueryable<ScheduleWeek> SearchRestaurantWeek(FilterRestaurantWeek filter)
        {
            IQueryable<ScheduleWeek> query = mSContext.Set<ScheduleWeek>();
            // assuming that you return all records when nothing is specified in the filter

            //if (!string.IsNullOrEmpty(filter.Description))
            //{
            //    query = query.Where(t =>
            //        t.Description.Contains(filter.Description));
            //}
            //if (filter.IsActive >= 0)
            //{
            //    bool val = (filter.IsActive == 1 ? true : false);
            //    query = query.Where(t =>
            //        t.IsActive == val);
            //}
            return query;
        }
    }
}