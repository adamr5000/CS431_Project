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