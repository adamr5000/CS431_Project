using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Nancy;
using ServiceStack.Data;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;

// EF7 http://wildermuth.com/2015/03/17/A_Look_at_ASP_NET_5_Part_3_-_EF7

namespace CS431_Project
{
    public class IndexModule : Nancy.NancyModule
    {
        public IndexModule()
        {
            Get["/"] = _ => new IndexModel("Hello world");
        }
    }

    public class IndexModel
    {
        public string Text;

        public IndexModel(string text)
        {
            this.Text = text;
        }
    }

    public class AdminModule : Nancy.NancyModule
    {
        public AdminModule(OrmLiteConnectionFactory db)
            : base("/admin")
        {
            Get["/create/{recreate?}"] = _ =>
            {
                var admin = new AdminModel(db);
                admin.CreateDatabase(_.recreate== "recreate");
                return admin;
            };

            // Routes that hit the db should be async
            // https://github.com/NancyFx/Nancy/wiki/Async
            Get["/populatetestdata"] = _ =>
            {
                var admin = new AdminModel(db);
                admin.CreateTestData();
                return admin;
            };
            
            Get["/gettestdata"] = _ =>
            {
                var admin = new AdminModel(db);
                admin.ShowTestData();
                return admin;
            };
        }
    }

    public class AdminModel
    {
        private readonly OrmLiteConnectionFactory _db;
        public string Operation;
        public string Status;
        
        public void CreateDatabase(bool recreate = false)
        {
            Operation = "Creating test data";
            var dbconn = new OrmLiteConnectionFactory(Bootstrapper.NoDBConnectionString);
            using (var db = dbconn.Open())
            {
                db.ExecuteSql("CREATE DATABASE CS431PROJECT;");
            }
            Status = "Run the Update-Database command in the Package Manager Console :)";
        }

        public void CreateTestData()
        {
            Operation = "Creating test data";
            using (var db = _db.Open())
            {
                db.DropAndCreateTable<Interest>();
                db.DropAndCreateTable<thing>();
                db.Save(new Interest {Name = "blah", Age = 424, otherthing = new thing {Value = 42}}, references: true);
            }
        }

        public void ShowTestData()
        {
            Operation = "Retrieving test data";
            using (var db = _db.Open())
            {
                Status = db.LoadSelect<Interest>(s => s.Age == 424).First().ToString();
            }
        }

        public AdminModel(OrmLiteConnectionFactory db)
        {
            _db = db;
        }
    }

    public class Interest
    {
        public int InterestId { get; set; } // id must be lower case
        
        public string Name { get; set; }

        public int? Age { get; set; }

        public int otherthingId { get; set; }

        [Reference]
        public thing otherthing { get; set; }

        public override string ToString()
        {
            return $"{InterestId}: {Name} {Age} - {otherthing}";
        }
    }

    public class thing
    {
        public int thingId { get; set; }

        public int InterestId { get; set; }

        public int? Value { get; set; }

        public override string ToString()
        {
            return $"{thingId}: {Value}";
        }
    }
}
