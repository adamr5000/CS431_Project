using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS431_Project.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Sqlite;
using Should;
using Xunit;

namespace CS431_Project.Tests
{
    public class DBFixture : IDisposable
    {
        public DBFixture()
        {
            b = new OrmLiteConnectionFactory(":memory:", SqliteOrmLiteDialectProvider.Instance, true);
            b.AutoDisposeConnection = false;

            using (var db = b.Open())
            {
                db.DropAndCreateTable<Movie>();
                db.DropAndCreateTable<Customer>();
                db.DropAndCreateTable<Showing>();
                db.DropAndCreateTable<Purchase>();
                db.DropAndCreateTable<Promotion>();
            }
        }

        public void Dispose()
        {
            // destroy db
        }

        public OrmLiteConnectionFactory b { get; set; }
    }

    public class ShowingsControllerTests: IClassFixture<DBFixture>
    {
        private readonly DBFixture d;

        public ShowingsControllerTests(DBFixture d)
        {
            this.d = d;
        }

        [Fact]
        public void StartsEmpty()
        {
            var showingsController = new ShowingsController(d.b);
            showingsController.ListAll().ShouldBeEmpty();
        }

        [Fact]
        public void ShowingCanBeAdded()
        {
            var showingsController = new ShowingsController(d.b);
            showingsController.AddShowing(new Showing
            {
                Movie = null,
                Price = 10,
                ScreenNumber = 10,
                Time = new DateTime(),
                TotalSeats = 100
            });

            showingsController.ListAll().Count().ShouldEqual(1);
        }

        [Fact]
        public void ShowingCanBeLookedUp()
        {
            var showingsController = new ShowingsController(d.b);
            var theshowing = new Showing
            {
                Movie = null,
                Price = 10,
                ScreenNumber = 10,
                Time = new DateTime(),
                TotalSeats = 100
            };

            var showing = showingsController.AddShowing(theshowing);
            var retrieved = showingsController.GetShowing(showing);

            retrieved.MovieId.ShouldEqual(theshowing.MovieId);
            retrieved.Price.ShouldEqual(theshowing.Price);
            retrieved.ScreenNumber.ShouldEqual(theshowing.ScreenNumber);
            retrieved.Time.ShouldEqual(theshowing.Time);
            retrieved.TotalSeats.ShouldEqual(theshowing.TotalSeats);
        }

        [Fact]
        public void SeatsAvailableGetsSet()
        {
            var showingsController = new ShowingsController(d.b);
            var theshowing = new Showing
            {
                Movie = null,
                Price = 10,
                ScreenNumber = 10,
                Time = new DateTime(),
                TotalSeats = 100
            };

            var showing = showingsController.AddShowing(theshowing);
            var retrieved = showingsController.GetShowing(showing);

            retrieved.AvailableSeats.ShouldEqual(theshowing.TotalSeats);
        }
    }
}
