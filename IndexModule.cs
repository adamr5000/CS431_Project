using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Microsoft.Data.Entity;
using Nancy;

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
        public AdminModule()
            : base("/admin")
        {
            Get["/create/{recreate?}"] = _ =>
            {
                var admin = new AdminModel();
                admin.CreateDatabase(_.recreate== "recreate");
                return admin;
            };

            // Routes that hit the db should be async
            // https://github.com/NancyFx/Nancy/wiki/Async
            Get["/populatetestdata"] = _ =>
            {
                var admin = new AdminModel();
                admin.CreateTestData();
                return admin;
            };
            
            Get["/gettestdata"] = _ =>
            {
                var admin = new AdminModel();
                admin.ShowTestData();
                return admin;
            };
        }
    }

    public class AdminModel
    {
        public string Operation;
        public string Status;
        
        public void CreateDatabase(bool recreate = false)
        {
            Operation = "Creating test data";
            Status = "Run the Update-Database command in the Package Manager Console :)";
        }

        public void CreateTestData()
        {
            Operation = "Creating test data";
            using (var db = new InterestContext())
            {
                db.interests.Add(new Interest {Name = "blah", Age = 424, otherthing = new thing {Value = 42}});
                var count = db.SaveChanges();
                Status = $"{count} records saved";
            }
        }

        public void ShowTestData()
        {
            Operation = "Retrieving test data";
            using (var db = new InterestContext())
            {
                Status = db.interests.First().ToString();
            }
        }
    }

    public class InterestContext : DbContext
    {
        public DbSet<Interest> interests { get; set; }

        public InterestContext()
        {
            Database.EnsureCreated();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // "Server=localhost;Port=5432;User Id=postgres;Password=password;Database=cs431project;"
            optionsBuilder.UseNpgsql(@"Server=localhost;Port=5432;Database=cs431project;User Id=postgres;Password=password;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

    [Table("interest")]
    public class Interest
    {
        [Column("id")]
        public int InterestId { get; set; } // id must be lower case
        
        [Column("name")]
        public string Name { get; set; }

        [Column("age")]
        public int? Age { get; set; }


        [Column("otherthing")]
        public thing otherthing { get; set; }

        public override string ToString()
        {
            return $"{InterestId}: {Name} {Age} - {otherthing}";
        }
    }

    public class thing
    {
        [Column("id")]
        public int thingId { get; set; }

        [Column("value")]
        public int? Value { get; set; }

        public override string ToString()
        {
            return $"{thingId}: {Value}";
        }
    }
}
