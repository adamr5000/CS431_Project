using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS431_Project
{
    public class IndexModule : Nancy.NancyModule
    {
        public IndexModule()
        {
            Get["/"] = _ =>
            {
                return "Hello World!";
            };
        }
    }
}
