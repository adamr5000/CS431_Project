using System;
using Microsoft.Owin.Hosting;

namespace CS431_Project
{
    class SelfHost
    {
        static void Main(string[] args)
        {
            var url = "http://+:8080";
            
            // If this throws an error, see "Running without Admin mode" mentioned here:
            // https://github.com/NancyFx/Nancy/wiki/Hosting-nancy-with-owin#katana---httplistener-selfhost

            using (WebApp.Start<OwinStartup>(url))
            {
                Console.WriteLine("Running on {0}", url);
                Console.WriteLine("Press enter to exit");
                Console.ReadLine();
            }
        }
    }
}
