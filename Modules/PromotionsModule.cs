using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anotar.NLog;
using CS431_Project.Controllers;
using CS431_Project.Models;
using Nancy;
using Nancy.ModelBinding;
using Nancy.ViewEngines.Razor.HtmlHelpers;
using ServiceStack.OrmLite;

namespace CS431_Project.Modules
{

    public class PromotionsModule: NancyModule
    {
        public PromotionsModule(OrmLiteConnectionFactory db)
            : base("/promotions")
        {
            const string obj = "Promotion";
            // Would like to totally DRY this class out, but things get a little clunky
            // without careful planning. Not worthwhile for such a small project.

            Get["/"] = _ =>
            {
                var controller = new PromotionController(db);
                return View[obj + "List", controller.ListAll()];
            };

            Get["/{id}"] = req =>
            {
                var controller = new PromotionController(db);
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
                var item = this.Bind<Promotion>();
                LogTo.Debug("Adding promotion: {0}", item);
                var controller = new PromotionController(db);
                var newId = controller.Add(item);
                return Response.AsRedirect(ModulePath  + "/" + newId);
            };

            Post["/update/{id}"] = _ => { return 500; };
        }
    }
}
