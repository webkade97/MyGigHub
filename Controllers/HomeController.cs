using MyGigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace MyGigHub.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var upcomingGigs = _context.Gigs
                .Include(g => g.Artist)
                .Where(g => g.ArtistId == userId);
            
            return View(upcomingGigs);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult TimesheetList()
        {
            ViewBag.Message = "Your List:";

            var userId = User.Identity.GetUserId();
            var myTimesheet = _context.Timesheets
                .Include(g => g.Artist)
                .Where(g => g.ArtistId == userId);

            return View(myTimesheet);
        }

    }
}