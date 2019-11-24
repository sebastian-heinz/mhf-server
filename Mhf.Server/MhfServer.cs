/*
 * This file is part of Mhf.Server
 *
 * Mhf.Server is a server implementation for the game "Monster Hunter Frontier Z".
 * Copyright (C) 2019-2020 Mhf Team
 *
 * Github: https://github.com/sebastian-heinz/mhf-server
 *
 * Mhf.Server is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * Mhf.Server is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with Mhf.Server. If not, see <https://www.gnu.org/licenses/>.
 */

using Arrowgene.Services.Logging;
using Arrowgene.Services.Networking.Tcp.Server.AsyncEvent;
using Mhf.Server.Common.Instance;
using Mhf.Server.Database;
using Mhf.Server.Logging;
using Mhf.Server.Model;
using Mhf.Server.Packet;
using Mhf.Server.PacketHandler;
using Mhf.Server.Setting;
using Mhf.Server.Web;
using Mhf.Server.Web.Server.Kestrel;
using Mhf.Server.WebMiddlewares;
using Mhf.Server.WebRoutes;
using Microsoft.Extensions.FileProviders;

namespace Mhf.Server
{
    public class MhfServer
    {
        public MhfSetting Setting { get; }
        public PacketRouter Router { get; }
        public ClientLookup Clients { get; }
        public IDatabase Database { get; }
        public InstanceGenerator Instances { get; }

        public IFileProvider WebFileProvider { get; }

        private readonly QueueConsumer _authConsumer;
        private readonly QueueConsumer _lobbyConsumer;
        private readonly AsyncEventServer _authServer;
        private readonly AsyncEventServer _lobbyServer;
        private readonly WebServer _webServer;
        private readonly MhfLogger _logger;

        public MhfServer(MhfSetting setting)
        {
            Setting = new MhfSetting(setting);

            LogProvider.Configure<MhfLogger>(Setting);
            _logger = LogProvider.Logger<MhfLogger>(this);

            Instances = new InstanceGenerator();
            Clients = new ClientLookup();
            Router = new PacketRouter();
            Database = new MhfDatabaseBuilder().Build(Setting.DatabaseSetting);
            
            _authConsumer = new QueueConsumer(Setting, Setting.ServerSocketSettings);
            _authConsumer.ClientDisconnected += AuthClientDisconnected;
            _authServer = new AsyncEventServer(
                Setting.ListenIpAddress,
                Setting.AuthServerPort,
                _authConsumer,
                Setting.ServerSocketSettings
            );
            
            _lobbyConsumer= new QueueConsumer(Setting, Setting.ServerSocketSettings);
            _lobbyConsumer.ClientDisconnected += LobbyClientDisconnected;
            _lobbyServer = new AsyncEventServer(
                Setting.ListenIpAddress,
                Setting.LobbyServerPort,
                _lobbyConsumer,
                Setting.ServerSocketSettings
            );

            _webServer = new WebServer(Setting, new KestrelWebServer(Setting));
            
            WebFileProvider = new PhysicalFileProvider(Setting.WebSetting.WebFolder);

            LoadPacketHandler();
            LoadWebRoutes();
        }

        private void AuthClientDisconnected(MhfConnection client)
        {
        }
        
        private void LobbyClientDisconnected(MhfConnection client)
        {
        }

        public void Start()
        {
            _authServer.Start();
            _webServer.Start();
        }

        public void Stop()
        {
            _authServer.Stop();
            _webServer.Stop();
        }

        private void LoadPacketHandler()
        {
           _authConsumer.AddHandler(new MsgHeadHandler(this));
        }

        private void LoadWebRoutes()
        {
            _webServer.AddRoute(new IndexRoute());
            _webServer.AddRoute(new AuthLauncherLoginRoute(WebFileProvider));
            _webServer.AddRoute(new AuthLauncherStartRoute(WebFileProvider));
            _webServer.AddRoute(new LauncherIndexRoute(WebFileProvider));
            _webServer.AddRoute(new MhfFileRoute(WebFileProvider));

            // Middleware - Order Matters
            _webServer.AddMiddleware(new StaticFileMiddleware("", WebFileProvider));
        }
    }
}
