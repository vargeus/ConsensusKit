﻿using System.Collections.Generic;
using Tcgv.ConsensusKit.Control;

namespace Tcgv.ConsensusKit.Exchange
{
    public class MessageBuffer
    {
        public MessageBuffer()
        {
            map = new Dictionary<MessageType, Dictionary<Instance, HashSet<Message>>>();
        }

        public void Add(Instance r, Message msg)
        {
            lock (map)
            {
                EnsureMap(msg.Type, r);
                map[msg.Type][r].Add(msg);
            }
        }

        public HashSet<Message> Filter(MessageType mType, Instance r)
        {
            lock (map)
            {
                EnsureMap(mType, r);
                return new HashSet<Message>(map[mType][r]);
            }
        }

        private void EnsureMap(MessageType mType, Instance r)
        {
            if (!map.ContainsKey(mType))
                map.Add(mType, new Dictionary<Instance, HashSet<Message>>());

            if (!map[mType].ContainsKey(r))
                map[mType].Add(r, new HashSet<Message>());
        }

        private Dictionary<MessageType, Dictionary<Instance, HashSet<Message>>> map;
    }
}
