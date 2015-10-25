using CS431_Project.Models;
using ServiceStack.Data;

namespace CS431_Project.Controllers
{
    public class PurchaseController : CRUDController<Purchase>
    {
        public PurchaseController(IDbConnectionFactory db) : base(db) { }
    }
}