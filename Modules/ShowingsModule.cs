using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Anotar.NLog;
using CS431_Project.Controllers;
using CS431_Project.Models;
using CS431_Project.Modules;
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
                var showing = showings.Get(req.showing);
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
                var showingId = showingsController.Add(showing);
                return Response.AsRedirect(ModulePath + "/" + showingId);
            };

            Post["/update/{id}"] = _ => { return 500; };
        }
    }
}