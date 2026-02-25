using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LMS.Repos
{
    public class EmployeeRepo
    {
        ApplicationDbContext mSContext;
        public EmployeeRepo()
        {
            mSContext = new ApplicationDbContext();
        }
        public List<Employee> GetAll()
        {
            try
            {
                return mSContext.Employees.ToList();
            }
            catch
            {
                return null;
            }

        }
        public List<Employee> GetActive()
        {
            try
            {
                return mSContext.Employees.Where(e => e.IsActive == true).ToList();
            }
            catch
            {
                return null;
            }

        }
        public Employee GetEmployee(int EmployeeId)
        {
            try
            {
                return mSContext.Employees.Where(e => e.Id == EmployeeId).FirstOrDefault();
            }
            catch
            {
                return null;
            }

        }
        public List<Employee> GetLocationEmployees(int LocationId)
        {
            try
            {
                return mSContext.Employees.Where(e => e.LocationId == LocationId).ToList();
            }
            catch
            {
                return null;
            }

        }
        public bool Add(Employee entity)
        {
            try
            {
                mSContext.Employees.Add(entity);
                mSContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool Update(Employee entity)
        {
            try
            {
                Employee result = mSContext.Employees.FirstOrDefault(e => e.Id == entity.Id);
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

                Employee result = mSContext.Employees.FirstOrDefault(e => e.Id == id);
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
        public class FilterEmployee
        {
            public string Name { get; set; }
            public int IsActive { get; set; }

        }
        public IQueryable<Employee> SearchCategory(FilterEmployee filter)
        {
            IQueryable<Employee> query = mSContext.Set<Employee>();
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