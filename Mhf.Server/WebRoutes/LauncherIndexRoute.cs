using System.Threading.Tasks;
using Mhf.Server.Web;
using Mhf.Server.Web.Route;
using Microsoft.Extensions.FileProviders;

namespace Mhf.Server.WebRoutes
{
    public class LauncherIndexRoute : FileWebRoute
    {
        public LauncherIndexRoute(IFileProvider fileProvider) : base(fileProvider)
        {
        }

        public override string Route => "/launcher/";
        
        public override async Task<WebResponse> Get(WebRequest request)
        {
            WebResponse response = new WebResponse();
            response.StatusCode = 200;
            response.Header.Add("content-type", "text/html; charset=UTF-8");
            IFileInfo startHtml = FileProvider.GetFileInfo("launcher/index.html");
            await response.WriteAsync(startHtml);
            return response;
        }
    }
}
