namespace Mhf.Server.Web.Route
{
    public abstract class ServerWebRoute : WebRoute
    {
        protected MhfServer Server { get; }

        public ServerWebRoute(MhfServer server)
        {
            Server = server;
        }
    }
}
