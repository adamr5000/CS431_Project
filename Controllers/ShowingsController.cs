using CS431_Project.Models;
using ServiceStack.Data;

namespace CS431_Project.Controllers
{
    public class ShowingsController : CRUDController<Showing>
    {
        public ShowingsController(IDbConnectionFactory db) : base(db) { }

        public new int Add(Showing showing)
        {
            showing.AvailableSeats = showing.TotalSeats;

            return base.Add(showing);
        }
    }
}