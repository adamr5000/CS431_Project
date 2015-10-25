using Anotar.NLog;
using CS431_Project.Controllers;
using CS431_Project.Models;
using Nancy;
using Nancy.ModelBinding;
using ServiceStack.OrmLite;

namespace CS431_Project.Modules
{
    public class PurchaseModule : NancyModule
    {
        public PurchaseModule(OrmLiteConnectionFactory db)
            : base("/purchase[s]?")
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
                    return View["New" + obj];
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