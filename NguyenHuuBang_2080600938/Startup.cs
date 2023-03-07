using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NguyenHuuBang_2080600938.Startup))]
namespace NguyenHuuBang_2080600938
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
