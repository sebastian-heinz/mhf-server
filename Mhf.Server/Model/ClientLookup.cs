﻿using System.Collections.Generic;

 namespace Mhf.Server.Model
{
    public class ClientLookup
    {
        private readonly List<MhfClient> _clients;

        private readonly object _lock = new object();

        public ClientLookup()
        {
            _clients = new List<MhfClient>();
        }

        /// <summary>
        /// Returns all Clients.
        /// </summary>
        public List<MhfClient> GetAll()
        {
            lock (_lock)
            {
                return new List<MhfClient>(_clients);
            }
        }

        /// <summary>
        /// Adds a Client.
        /// </summary>
        public void Add(MhfClient client)
        {
            if (client == null)
            {
                return;
            }

            lock (_lock)
            {
                _clients.Add(client);
            }
        }

        /// <summary>
        /// Removes the Client from all lists and lookup tables.
        /// </summary>
        public void Remove(MhfClient client)
        {
            lock (_lock)
            {
                _clients.Remove(client);
            }
        }

        /// <summary>
        /// Returns a Client by AccountId if it exists.
        /// </summary>
        public MhfClient GetByAccountId(int accountId)
        {
            List<MhfClient> clients = GetAll();
            foreach (MhfClient client in clients)
            {
                if (client.Account.Id == accountId)
                {
                    return client;
                }
            }

            return null;
        }
    }
}
