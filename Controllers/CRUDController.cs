using System;
using System.Collections.Generic;
using Anotar.NLog;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace CS431_Project.Controllers
{
    public class CRUDController<T>
    {
        protected readonly IDbConnectionFactory _db;

        public CRUDController(IDbConnectionFactory db)
        {
            if (db == null)
                throw new ArgumentNullException("IDbConnectionFactory cannot be null");
            _db = db;
        }

        public IEnumerable<T> ListAll()
        {
            using (var db = _db.Open())
            {
                return db.LoadSelect<T>();
            }
        }

        public T Get(int id)
        {
            using (var db = _db.Open())
            {
                return db.LoadSingleById<T>(id);
            }
        }

        public int Add(T item)
        {
            using (var db = _db.Open())
            {
                var id = db.Insert<T>(item, selectIdentity: true);
                LogTo.Debug("Added object to database: {0}: {1}", id, item);
                return checked((int)id);
            }
        }
    }
}