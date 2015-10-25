using System;
using CS431_Project.Controllers;
using CS431_Project.Models;
using CS431_Project.Modules;
using ServiceStack.OrmLite;

namespace CS431_Project
{
    public class MovieController: CRUDController<Movie>
    {
        public MovieController(OrmLiteConnectionFactory db) :base(db) { }

        public new MovieList ListAll()
        {
            using (var db = _db.Open())
            {
                return new MovieList("All movies", db.Select<Movie>());
            }
        }

        public Movie Lookup(string MovieName)
        {
            // MovieName can be a number (database ID number) or string (movie title)
            // (Looking back, that was probably a bad design decision; lookup should probably be more like search)
            int id = 0;
            if (int.TryParse(MovieName, out id))
            {
                return Get(id);
            }

            // See if it's a string
            MovieName = MovieName.Replace('-', ' '); // Movie name URLs have hyphens rather than spaces
            using (var db = _db.Open())
            {
                return db.Single<Movie>(x => x.Title.ToLower() == MovieName.ToLower());

            }
        }
    }
}