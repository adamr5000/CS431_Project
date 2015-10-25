using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Anotar.NLog;
using CS431_Project.Controllers;
using CS431_Project.Models;
using Nancy;
using Nancy.ModelBinding;
using Nancy.ViewEngines.Razor.HtmlHelpers;
using ServiceStack.OrmLite;

namespace CS431_Project.Modules
{
    public class PurchaseModule : NancyModule
    {
        public PurchaseModule(OrmLiteConnectionFactory db)
            : base("/purchases")
        {
            {
                const string obj = "Purchase";

                Get["/"] = _ =>
                {
                    var controller = new PurchaseController(db);
                    return View[obj + "List", controller.ListAll()];
                };

                Get["/{id}"] = req =>
                {
                    var controller = new PurchaseController(db);
                    var item = controller.Get(req.id);
                    if (item == null)
                        return 404;
                    return View[obj + "Detail", item];
                };

                Get["/create"] = _ =>
                {
                    var model = new
                    {
                        Movies = (new MovieController(db)).ListAll().Movies,
                        Showings = (new ShowingsController(db)).ListAll(),
                        Customers = new List<Customer>(),
                        Promotions = (new PromotionController(db)).ListAll(),
                    };
                    
                    var selects = new
                    {
                        Movies = model.Movies.Select(movie => new SelectListItem(movie.Title, movie.MovieId.ToString(), false)),
                        Showings = model.Showings.Select(showing => new SelectListItem(showing.Time.ToString(), showing.ShowingId.ToString(), false)),
                        Customers = model.Customers.Select(showing => new SelectListItem(showing.Name.ToString(), showing.CustomerId.ToString(), false)),
                        Promotions = model.Promotions.Select(showing => new SelectListItem(showing.PromotionName.ToString(), showing.PromotionId.ToString(), false)),
                    };
                    return View["New" + obj, selects];
                };

                Post["/create"] = _ =>
                {
                    var item = this.Bind<Purchase>();
                    LogTo.Debug("Adding purchase: {0}", item);
                    var controller = new PurchaseController(db);
                    var newId = controller.Add(item);
                    return Response.AsRedirect(ModulePath + "/" + newId);
                };

                Post["/update/{id}"] = _ => { return 500; };
            }
        }
    }
}