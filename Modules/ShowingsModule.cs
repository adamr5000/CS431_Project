using System.Runtime.Remoting.Messaging;
using CS431_Project.Models;
using Nancy;
using Nancy.ModelBinding;
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
                return showings.ListAll();
            };

            Get["/{showing}"] = req =>
            {
                var showings = new ShowingsController(db);
                var showing = showings.GetShowing(req.movie);
                if (showing == null)
                    return 404;
                return View["showingDetail", showing];
            };

            Get["/create"] = _ => View["NewShowing"];

            Post["/create"] = _ =>
            {
                var showing = this.Bind<Showing>(); // Binds the POST result to movie variable (as a movie object)
                var showingsController = new ShowingsController(db);
                showingsController.AddShowing(showing);
                return Response.AsRedirect("/showings/" + showing.ShowingId);
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

        public Showing ListAll()
        {
            return null;
        }

        public Showing GetShowing(int showingId)
        {
            return null;
        }

        public void AddShowing(Showing showing)
        {
            
        }
    }
}