using CS431_Project.Models;
using ServiceStack.Data;

namespace CS431_Project.Controllers
{
    public class PromotionController : CRUDController<Promotion>
    {
        public PromotionController(IDbConnectionFactory db) :base(db) { }
    }
}