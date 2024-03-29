using CS431_Project.Models;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Sqlite;

namespace CS431_Project.Tests
{
    public class InMemoryDbTest
    {
        protected readonly OrmLiteConnectionFactory b;

        public InMemoryDbTest()
        {
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
    }
}