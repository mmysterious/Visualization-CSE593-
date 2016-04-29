using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StockData.Startup))]
namespace StockData
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
