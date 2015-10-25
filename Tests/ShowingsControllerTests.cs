using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS431_Project.Controllers;
using CS431_Project.Models;
using ServiceStack.Data;
using Should;
using Xunit;

namespace CS431_Project.Tests
{
    public class ShowingsControllerTests: InMemoryDbTest
    {
        [Fact]
        public void ShowingCanBeLookedUp()
        {
            var showingsController = new ShowingsController(b);
            var theshowing = new Showing
            {
                Movie = null,
                Price = 10,
                ScreenNumber = 10,
                Time = new DateTime(),
                TotalSeats = 100
            };

            int showing = showingsController.Add(theshowing);
            var retrieved = showingsController.Get(showing);

            retrieved.MovieId.ShouldEqual(theshowing.MovieId);
            retrieved.Price.ShouldEqual(theshowing.Price);
            retrieved.ScreenNumber.ShouldEqual(theshowing.ScreenNumber);
            retrieved.Time.ShouldEqual(theshowing.Time);
            retrieved.TotalSeats.ShouldEqual(theshowing.TotalSeats);
        }

        [Fact]
        public void SeatsAvailableGetsSet()
        {
            var showingsController = new ShowingsController(b);
            var theshowing = new Showing
            {
                Movie = null,
                Price = 10,
                ScreenNumber = 10,
                Time = new DateTime(),
                TotalSeats = 100
            };

            var showing = showingsController.Add(theshowing);
            var retrieved = showingsController.Get(showing);

            retrieved.AvailableSeats.ShouldEqual(theshowing.TotalSeats);
        }
    }
}
