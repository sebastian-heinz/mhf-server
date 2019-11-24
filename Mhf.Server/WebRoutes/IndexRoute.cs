using System.Threading.Tasks;
using Mhf.Server.Web;
using Mhf.Server.Web.Route;

namespace Mhf.Server.WebRoutes
{
    public class IndexRoute : WebRoute
    {
        public override string Route => "/";

        public override async Task<WebResponse> Get(WebRequest request)
        {
            WebResponse response = new WebResponse();
            response.StatusCode = 200;
            await response.WriteAsync("Welcome - Index Page!");
            return response;
        }
    }
}
