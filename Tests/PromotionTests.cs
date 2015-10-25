using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS431_Project.Controllers;
using CS431_Project.Models;
using CS431_Project.Modules;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Sqlite;
using Should;
using Xunit;

namespace CS431_Project.Tests
{
    public class CRUDControllerTests
    {
        private readonly OrmLiteConnectionFactory b;

        public CRUDControllerTests()
        {
            // This should all be moved into a parent class
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

        [Fact]
        public void StartsEmpty()
        {
            var controller = new CRUDController<Promotion>(b);
            controller.ListAll().ShouldBeEmpty();
        }

        [Fact]
        public void ItemCanBeAdded()
        {
            var controller = new CRUDController<Promotion>(b);
            controller.Add(new Promotion
            {
                Expiration = DateTime.Now,
                PromotionCode = "",
                PromoType = Promotion.PromotionType.FlatRate,
                PromotionName = "5 off",
                PromoValue = 5
            });

            controller.ListAll().Count().ShouldEqual(1);
        }
    }

    public class PromotionControllerTests
    {
        private readonly OrmLiteConnectionFactory b;

        public PromotionControllerTests()
        {
            // This should all be moved into a parent class
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

        [Fact]
        public void ItemsCanBeLookedUp()
        {
            var controller = new PromotionController(b);
            var theshowing = new Promotion
            {
                Expiration = DateTime.Now,
                PromotionCode = "",
                PromoType = Promotion.PromotionType.FlatRate,
                PromotionName = "5 off",
                PromoValue = 5
            };

            var showing = controller.Add(theshowing);
            var retrieved = controller.Get(showing);

            //retrieved.ShouldEqual(theshowing);
            // TODO: Need object comparison sans Id
        }
    }
}
