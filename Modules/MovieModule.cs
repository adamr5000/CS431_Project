using System.Runtime.Remoting.Messaging;
using CS431_Project.Models;
using Nancy;
using Nancy.ModelBinding;
using ServiceStack.OrmLite;

namespace CS431_Project
{
    public class MovieModule : NancyModule
    {
        public MovieModule(OrmLiteConnectionFactory db)
            : base("/movies")
        {
            Get["/"] = _ =>
            {
                var movies = new MovieController(db);
                return movies.ListAll();
            };

            Get["/{movie}"] = req =>
            {
                var movies = new MovieController(db);
                var movie = movies.Lookup(req.movie);
                if (movie == null)
                    return 404;
                return View["MovieDetail", movie];
            };

            Get["/create"] = _ => { return View["NewMovie"]; };

            Post["/create"] = _ =>
            {
                var movie = this.Bind<Movie>(); // Binds the POST result to movie variable (as a movie object)
                var movieController = new MovieController(db);
                movieController.Add(movie);
                return Response.AsRedirect("/movies/" + movie.GetPrettyTitleLink());
            };

            Post["/update/{id}"] = _ => { return 500; };
        }
    }
}