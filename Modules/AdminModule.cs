using Nancy;
using ServiceStack.OrmLite;

namespace CS431_Project
{
    public class AdminModule : NancyModule
    {
        public AdminModule(OrmLiteConnectionFactory db)
            : base("/admin")
        {
            Get["/create/{recreate?}"] = _ =>
            {
                var admin = new AdminModel(db);
                admin.CreateDatabase(_.recreate == "recreate");
                admin.CreateTables();
                return admin;
            };

            // Routes that hit the db should be async
            // https://github.com/NancyFx/Nancy/wiki/Async
            Get["/populatetestdata"] = _ =>
            {
                var admin = new AdminModel(db);
                admin.PopulateTestData();
                return admin;
            };
        }
    }
}