using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Repos
{
    public class LocationWeekRepo
    {
        ApplicationDbContext mSContext;
        public LocationWeekRepo()
        {
            mSContext = new ApplicationDbContext();
        }
        public List<LocationWeek> GetAll(int LocationId,int Year)
        {
            try
            {
                var locationweeks= mSContext.LocationWeeks.Include("ScheduleWeek").Where(e => e.LocationId == LocationId && e.ScheduleWeek.Year==Year).ToList();
                return locationweeks;
            }
            catch
            {
                return null;
            }

        }
        public List<LocationWeek> GetActive(int LocationId, int Year)
        {
            try
            {
                return mSContext.LocationWeeks.Include("ScheduleWeek").Where(e => e.LocationId == LocationId && e.ScheduleWeek.Year == Year).ToList();
            }
            catch
            {
                return null;
            }

        }
        public LocationWeek GetLocationWeek(int Id)
        {
            try
            {
                return mSContext.LocationWeeks.Where(e => e.Id == Id).FirstOrDefault();
            }
            catch
            {
                return null;
            }

        }

        public bool Add(LocationWeek entity)
        {
            try
            {
                mSContext.LocationWeeks.Add(entity);
                mSContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public bool Add(List<LocationWeek> entities)
        {
            try
            {
                using (mSContext)
                {
                    mSContext.LocationWeeks.AddRange(entities);
                    mSContext.SaveChanges(); // Inserts all tracked entities into the database
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public bool Update(LocationWeek entity)
        {
            try
            {
                LocationWeek result = mSContext.LocationWeeks.FirstOrDefault(e => e.Id == entity.Id);
                if (result == null)
                {
                    return false;
                }
                //result.CreatedOn = entity.CreatedOn;
                result.ScheduleWeekId = entity.ScheduleWeekId;
                result.LocationId = entity.LocationId;
               // result.Description = entity.Description;
                result.ForcastedSale = entity.ForcastedSale;
                result.Percentage = entity.Percentage;
               // result.IsActive = entity.IsActive;
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
    }
}