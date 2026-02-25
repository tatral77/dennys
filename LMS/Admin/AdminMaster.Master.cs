using Microsoft.AspNet.Identity;
using System;
using System.Web;

namespace LMS.Admin
{
    public partial class AdminMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (val1 == false)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }

        protected void LogOutBtn_Click(object sender, EventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            Response.Redirect("~/Account/Login.aspx");
        }
    }
}