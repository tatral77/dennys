using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LMS.Repos
{
    public class LocationRepo
    {
        ApplicationDbContext mSContext;
        public LocationRepo()
        {
            mSContext = new ApplicationDbContext();
        }
        public List<Location> GetAll()
        {
            try
            {
                return mSContext.Locations.ToList();
            }
            catch
            {
                return null;
            }

        }
        public List<Location> GetActive()
        {
            try
            {
                return mSContext.Locations.Where(e => e.IsActive == true).ToList();
            }
            catch
            {
                return null;
            }

        }
        public Location GetLocation(int LocationId)
        {
            try
            {
                return mSContext.Locations.Where(e => e.Id == LocationId).FirstOrDefault();
            }
            catch
            {
                return null;
            }

        }
        public List<Location> GetLocationEmployees(int LocationId)
        {
            try
            {
                return mSContext.Locations.Include("Employees").Where(e => e.Id == LocationId).ToList();
            }
            catch
            {
                return null;
            }

        }

        public bool Add(Location entity)
        {
            try
            {
                mSContext.Locations.Add(entity);
                mSContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool Update(Location entity)
        {
            try
            {
                Location result = mSContext.Locations.FirstOrDefault(e => e.Id == entity.Id);
                if (result == null)
                {
                    return false;
                }
                result.Name = entity.Name;
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

                Location result = mSContext.Locations.FirstOrDefault(e => e.Id == id);
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
        public class SearchLocation
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int IsActive { get; set; }
        }
        public IQueryable<Location> SearchDepartments(SearchLocation filter)
        {
            IQueryable<Location> query = mSContext.Set<Location>();
            // assuming that you return all records when nothing is specified in the filter

            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(t =>
                    t.Name.Contains(filter.Name));
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