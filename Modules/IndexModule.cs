using Nancy;

// EF7 http://wildermuth.com/2015/03/17/A_Look_at_ASP_NET_5_Part_3_-_EF7

namespace CS431_Project
{
    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = _ => new IndexModel("Hello world");
        }
    }
}