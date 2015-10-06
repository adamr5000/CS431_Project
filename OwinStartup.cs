using Owin;

namespace CS431_Project
{
    public class OwinStartup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy();
        }
    }
}