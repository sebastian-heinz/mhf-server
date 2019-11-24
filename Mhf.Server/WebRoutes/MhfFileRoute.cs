using System.Threading.Tasks;
using Mhf.Server.Web;
using Mhf.Server.Web.Route;
using Microsoft.Extensions.FileProviders;

namespace Mhf.Server.WebRoutes
{
    public class MhfFileRoute : FileWebRoute
    {
        public MhfFileRoute(IFileProvider fileProvider) : base(fileProvider)
        {
        }

        public override string Route => "/mhf_file.php";

        public override async Task<WebResponse> Get(WebRequest request)
        {
            if (request.QueryParameter.ContainsKey("key"))
            {
                IFileInfo mhfFile = FileProvider.GetFileInfo("MHFUP_00.DAT");
                WebResponse response = new WebResponse();
                response.StatusCode = 200;
                response.Header.Add("content-type", "application/octet-stream");
                response.Header.Add("content-disposition", "inline; filename=\"MHFUP_00.DAT\"");
                response.Header.Add("connection", "close");
                await response.WriteAsync(mhfFile);
                return response;
            }

            if (request.QueryParameter.ContainsKey("chk"))
            {
                WebResponse response = new WebResponse();
                response.StatusCode = 200;
                response.Header.Add("content-type", "application/octet-stream");
                response.Header.Add("connection", "close");
                await response.WriteAsync("[mhf Check Message:0]");
                return response;
            }

            return await WebResponse.NotFound();
        }
    }
}
