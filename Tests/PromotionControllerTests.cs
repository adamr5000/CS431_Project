using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CS431_Project.Controllers;
using CS431_Project.Models;
using CS431_Project.Modules;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Sqlite;
using Xunit;

namespace CS431_Project.Tests
{
    public class PromotionControllerTests: InMemoryDbTest
    {
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
