using System.Threading.Tasks;
using Mhf.Server.Web;
using Mhf.Server.Web.Route;
using Microsoft.Extensions.FileProviders;

namespace Mhf.Server.WebRoutes
{
    public class AuthLauncherStartRoute : FileWebRoute
    {
        public AuthLauncherStartRoute(IFileProvider fileProvider) : base(fileProvider)
        {
        }

        public override string Route => "/auth/launcher/start.html";
        
        public override async Task<WebResponse> Get(WebRequest request)
        {
            WebResponse response = new WebResponse();
            response.StatusCode = 200;
            response.Header.Add("content-type", "text/html; charset=UTF-8");
            IFileInfo startHtml = FileProvider.GetFileInfo("auth/launcher/start.html");
            await response.WriteAsync(startHtml);
            return response;
        }
    }
}
