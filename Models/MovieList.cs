using System.Collections.Generic;
using CS431_Project.Models;

namespace CS431_Project
{
    public class MovieList
    {
        public readonly List<Movie> Movies;
        public readonly string Title;

        public MovieList(string title, List<Movie> movies)
        {
            Title = title;
            Movies = movies;
        }
    }
}