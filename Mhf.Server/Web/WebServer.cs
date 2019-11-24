using System.Threading.Tasks;
using Arrowgene.Services.Logging;
using Mhf.Server.Setting;
using Mhf.Server.Web.Middleware;
using Mhf.Server.Web.Route;
using Mhf.Server.Web.Server;

namespace Mhf.Server.Web
{
    /// <summary>
    /// Manages web requests
    /// </summary>
    public class WebServer : IWebServerHandler
    {
        private IWebServer _server;
        private WebRouter _router;
        private WebMiddlewareStack _middlewareStack;
        private ILogger _logger;

        public WebServer(MhfSetting setting, IWebServer server)
        {
            _logger = LogProvider.Logger(this);
            _server = server;
            _router = new WebRouter(setting);
            _server.SetHandler(this);
            _middlewareStack = new WebMiddlewareStack(_router.Route);
        }

        public async Task Start()
        {
            await _server.Start();
        }

        public async Task Stop()
        {
            await _server.Stop();
        }

        public async Task<WebResponse> Handle(WebRequest request)
        {
            WebResponse response = await _middlewareStack.Start(request);
            if (!response.RouteFound)
            {
                _logger.Info($"No route or middleware registered for requested Path: {request.Path}");
            }
            return response;
        }

        public void AddRoute(IWebRoute route)
        {
            _router.AddRoute(route);
        }

        public void AddMiddleware(IWebMiddleware middleware)
        {
            _middlewareStack.Use(next => req => middleware.Handle(req, next));
            // middleware.Use(
            //     next => req =>
            //     {
            //         return next(req);
            //     }
            // );
        }
    }
}
