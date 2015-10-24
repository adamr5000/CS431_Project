using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Anotar.NLog;
using CS431_Project.Models;
using Nancy;
using Nancy.ModelBinding;
using Nancy.ViewEngines.Razor.HtmlHelpers;
using ServiceStack;
using ServiceStack.OrmLite;

namespace CS431_Project
{
    public class ShowingsModule : NancyModule
    {
        public ShowingsModule(OrmLiteConnectionFactory db)
            : base("/showings")
        {
            Get["/"] = _ =>
            {
                var showings = new ShowingsController(db);
                return View["ShowingList", showings.ListAll()];
            };

            Get["/{showing}"] = req =>
            {
                var showings = new ShowingsController(db);
                var showing = showings.GetShowing(req.showing);
                if (showing == null)
                    return 404;
                return View["ShowingDetail", showing];
            };

            Get["/create"] = _ =>
            {
                var movieController = new MovieController(db);
                var movies = movieController.ListAll().Movies;
                var selectlist = movies.Select(movie => new SelectListItem(movie.Title, movie.MovieId.ToString(), false)).ToList();
                return View["NewShowing", selectlist];
            };

            Post["/create"] = _ =>
            {
                var showing = this.Bind<Showing>();
                LogTo.Debug("Adding showing: {0}", showing);
                var showingsController = new ShowingsController(db);
                var showingId = showingsController.AddShowing(showing);
                return Response.AsRedirect("/showings/" + showingId);
            };

            Post["/update/{id}"] = _ => { return 500; };
        }
    }

    public class ShowingsController
    {
        private readonly OrmLiteConnectionFactory _db;

        public ShowingsController(OrmLiteConnectionFactory db)
        {
            _db = db;
        }

        public IEnumerable<Showing> ListAll()
        {
            using (var db = _db.Open())
            {
                return db.LoadSelect<Showing>(showing => true);
            }
        }

        public Showing GetShowing(int showingId)
        {
            using (var db = _db.Open())
            {
                return db.LoadSingleById<Showing>(showingId);
            }
        }

        public int AddShowing(Showing showing)
        {
            showing.AvailableSeats = showing.TotalSeats;
            
            using (var db = _db.Open())
            {
                var id = db.Insert(showing, selectIdentity: true);
                // Log id
                return checked((int) id);
            }
        }
    }
}