using System;
using CS431_Project.Models;
using ServiceStack.OrmLite;

namespace CS431_Project
{
    public class AdminModel // Named controller, but actually a ridiculous model-controller hybrid. Sorry..
    {
        private readonly OrmLiteConnectionFactory _db;
        public string Operation;
        public string Status;

        public AdminModel(OrmLiteConnectionFactory db)
        {
            _db = db;
        }

        public void CreateDatabase(bool recreate = false)
        {
            Operation = "Creating database";
            var dbconn = new OrmLiteConnectionFactory(Bootstrapper.NoDBConnectionString);
            using (var db = dbconn.Open())
            {
                if (recreate)
                {
                    try
                    {
                        db.ExecuteSql("DROP DATABASE CS431PROJECT;");
                    }
                    catch (Exception)
                    {
                        // Log
                        throw;
                    }
                }
                try
                {
                    db.ExecuteSql("CREATE DATABASE CS431PROJECT;");
                }
                catch (Exception)
                {
                    // Log
                    throw;
                }
            }
            Status = "Probably succeeded";
        }

        public void CreateTables()
        {
            Operation = "Recreating tables";
            using (var db = _db.Open())
            {
                db.DropAndCreateTable<Movie>();
                db.DropAndCreateTable<Customer>();
                db.DropAndCreateTable<Showing>();
                db.DropAndCreateTable<Purchase>();
                db.DropAndCreateTable<Promotion>();
            }
        }

        public void PopulateTestData()
        {
            Operation = "Creating test data";
        }
    }
}