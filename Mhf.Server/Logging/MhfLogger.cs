using System;
using Arrowgene.Services.Logging;
using Arrowgene.Services.Networking.Tcp;
using Mhf.Server.Model;
using Mhf.Server.Packet;
using Mhf.Server.Setting;

namespace Mhf.Server.Logging
{
    public class MhfLogger : Logger
    {
        private MhfSetting _setting;

        public MhfLogger() : this(null)
        {
        }

        public MhfLogger(string identity, string zone = null) : base(identity, zone)
        {
        }

        public override void Initialize(string identity, string zone, object configuration)
        {
            base.Initialize(identity, zone, configuration);
            _setting = configuration as MhfSetting;
            if (_setting == null)
            {
                Error("Couldn't apply MhfLogger configuration");
            }
        }

        public void Info(MhfClient client, string message, params object[] args)
        {
            Write(LogLevel.Info, null, $"{client.Identity} {message}", args);
        }

        public void Info(MhfConnection connection, string message, params object[] args)
        {
            MhfClient client = connection.Client;
            if (client != null)
            {
                Info(client, message, args);
                return;
            }

            Write(LogLevel.Info, null, $"{connection.Identity} {message}", args);
        }

        public void Debug(MhfClient client, string message, params object[] args)
        {
            Write(LogLevel.Debug, null, $"{client.Identity} {message}", args);
        }

        public void Error(MhfClient client, string message, params object[] args)
        {
            Write(LogLevel.Error, null, $"{client.Identity} {message}", args);
        }

        public void Error(MhfConnection connection, string message, params object[] args)
        {
            MhfClient client = connection.Client;
            if (client != null)
            {
                Error(client, message, args);
                return;
            }

            Write(LogLevel.Error, null, $"{connection.Identity} {message}", args);
        }

        public void Exception(MhfClient client, Exception exception)
        {
            Write(LogLevel.Error, null, $"{client.Identity} {exception}");
        }

        public void Exception(MhfConnection connection, Exception exception)
        {
            MhfClient client = connection.Client;
            if (client != null)
            {
                Exception(client, exception);
                return;
            }

            Write(LogLevel.Error, null, $"{connection.Identity} {exception}");
        }

        public void Info(ITcpSocket socket, string message, params object[] args)
        {
            Write(LogLevel.Info, null, $"[{socket.Identity}] {message}", args);
        }

        public void Debug(ITcpSocket socket, string message, params object[] args)
        {
            Write(LogLevel.Debug, null, $"[{socket.Identity}] {message}", args);
        }

        public void Error(ITcpSocket socket, string message, params object[] args)
        {
            Write(LogLevel.Error, null, $"[{socket.Identity}] {message}", args);
        }

        public void Exception(ITcpSocket socket, Exception exception)
        {
            Write(LogLevel.Error, null, $"[{socket.Identity}] {exception}");
        }

        public void LogIncomingPacket(MhfClient client, MhfPacket packet)
        {
            if (_setting.LogIncomingPackets)
            {
                MhfLogPacket logPacket = new MhfLogPacket(client.Identity, packet, MhfLogType.PacketIn);
                WritePacket(logPacket);
            }
        }

        public void LogIncomingPacket(MhfConnection connection, MhfPacket packet)
        {
            MhfClient client = connection.Client;
            if (client != null)
            {
                LogIncomingPacket(client, packet);
                return;
            }

            if (!_setting.LogIncomingPackets)
            {
                return;
            }

            MhfLogPacket logPacket = new MhfLogPacket(connection.Identity, packet, MhfLogType.PacketIn);
            WritePacket(logPacket);
        }

        public void LogUnknownIncomingPacket(MhfClient client, MhfPacket packet)
        {
            if (_setting.LogUnknownIncomingPackets)
            {
                MhfLogPacket logPacket = new MhfLogPacket(client.Identity, packet, MhfLogType.PacketUnhandled);
                WritePacket(logPacket);
            }
        }

        public void LogUnknownIncomingPacket(MhfConnection connection, MhfPacket packet)
        {
            MhfClient client = connection.Client;
            if (client != null)
            {
                LogUnknownIncomingPacket(client, packet);
                return;
            }

            if (!_setting.LogIncomingPackets)
            {
                return;
            }

            MhfLogPacket logPacket =
                new MhfLogPacket(connection.Identity, packet, MhfLogType.PacketUnhandled);
            WritePacket(logPacket);
        }

        public void LogOutgoingPacket(MhfClient client, MhfPacket packet)
        {
            if (_setting.LogOutgoingPackets)
            {
                MhfLogPacket logPacket = new MhfLogPacket(client.Identity, packet, MhfLogType.PacketOut);
                WritePacket(logPacket);
            }
        }

        public void LogOutgoingPacket(MhfConnection connection, MhfPacket packet)
        {
            MhfClient client = connection.Client;
            if (client != null)
            {
                LogOutgoingPacket(client, packet);
                return;
            }

            if (!_setting.LogIncomingPackets)
            {
                return;
            }

            MhfLogPacket logPacket = new MhfLogPacket(connection.Identity, packet, MhfLogType.PacketOut);
            WritePacket(logPacket);
        }

        private void WritePacket(MhfLogPacket packet)
        {
            Write(LogLevel.Info, packet, packet.ToLogText());
        }
    }
}
