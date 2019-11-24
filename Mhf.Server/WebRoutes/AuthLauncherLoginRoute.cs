using System.Threading.Tasks;
using Mhf.Server.Web;
using Mhf.Server.Web.Route;
using Microsoft.Extensions.FileProviders;

namespace Mhf.Server.WebRoutes
{
    public class AuthLauncherLoginRoute : FileWebRoute
    {
        public AuthLauncherLoginRoute(IFileProvider fileProvider) : base(fileProvider)
        {
        }
        
        public override string Route => "/auth/launcher/login";

        public override async Task<WebResponse> Post(WebRequest request)
        {
            WebResponse response = new WebResponse();
            response.StatusCode = 200;
            response.Header.Add("content-type", "text/html; charset=UTF-8");
            IFileInfo startHtml = FileProvider.GetFileInfo("auth/launcher/login.html");
            await response.WriteAsync(startHtml);
            return response;
        }
    }
}
