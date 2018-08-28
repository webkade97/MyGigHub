using Microsoft.AspNet.Identity;
using MyGigHub.Models;
using MyGigHub.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyGigHub.Controllers
{
    public class GigController : Controller
    {
        private ApplicationDbContext _Context;
        public GigController()
        {
            _Context = new ApplicationDbContext();
        }

        // GET: Gig
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel();
            viewModel.Genres = _Context.Genres.ToList();
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            viewModel.Genres = _Context.Genres.ToList();
            if (!ModelState.IsValid)
                return View("Create", viewModel);
            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };
            _Context.Gigs.Add(gig);
            _Context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}