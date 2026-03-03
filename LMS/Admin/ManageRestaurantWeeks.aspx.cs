using LMS.Helper;
using LMS.Models;
using LMS.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LMS.Admin
{
    public partial class ManageRestaurantWeeks : System.Web.UI.Page
    {
        RestaurantWeekRepo restaurantWeekRepo = new RestaurantWeekRepo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int year = DateTime.Now.Year;
                BindLV(year);
            }
        }
        protected void BindLV(int year)
        {
            List<RestaurantWeek> result = restaurantWeekRepo.GetAll(year);
            LV.DataSource = result;
            LV.DataBind();
        }
        protected void GenerateWeeksBtn_Click(object sender, EventArgs e)
        {
            int year = Convert.ToInt32(YearTxt.Text);
            List<RestaurantWeek> result = restaurantWeekRepo.GetAll(year);
            if (result.Count==0)
            {
                List<RestaurantWeek> restaurantWeeks = WeeksHelper.GenerateRestaurantWeeks(year);
                restaurantWeekRepo.Add(restaurantWeeks);
            }
            BindLV(year);
        }
    }
}