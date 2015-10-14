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

            Get["/create"] = _ => { return View["NewMovie"]; };

            Post["/create"] = _ =>
            {
                var movie = this.Bind<Movie>(); // Binds the POST result to movie variable (as a movie object)
                var movieController = new MovieController(db);
                movieController.AddMovie(movie);
                return 200;
            };

            Post["/update/{id}"] = _ => { return 500; };
        }
    }
}