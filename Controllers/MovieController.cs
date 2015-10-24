using System;
using CS431_Project.Models;
using ServiceStack.OrmLite;

namespace CS431_Project
{
    public class MovieController
    {
        private readonly OrmLiteConnectionFactory _db;

        public MovieController(OrmLiteConnectionFactory db)
        {
            _db = db;
        }

        public MovieList ListAll()
        {
            using (var db = _db.Open())
            {
                return new MovieList("All movies", db.Select<Movie>());
            }
        }

        public Movie Lookup(string MovieName)
        {
            // MovieName can be a number (database ID number) or string (movie title)
            int id = 0;
            if (int.TryParse(MovieName, out id))
            {
                using (var db = _db.Open())
                {
                    return db.SingleById<Movie>(id);
                }
            }

            // See if it's a string
            MovieName = MovieName.Replace('-', ' '); // Movie name URLs have hyphens rather than spaces
            using (var db = _db.Open())
            {
                return db.Single<Movie>(x => x.Title.ToLower() == MovieName.ToLower());

            }
        }

        public void AddMovie(Movie m)
        {
            // Validate movie
            using (var db = _db.Open())
            {
                var id = db.Insert(m, selectIdentity: true);
                // Log id
            }
        }
    }
}