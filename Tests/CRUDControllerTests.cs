using System;
using System.Linq;
using CS431_Project.Controllers;
using CS431_Project.Models;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Sqlite;
using Should;
using Xunit;

namespace CS431_Project.Tests
{
    public class CRUDControllerTests: InMemoryDbTest
    {
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
}