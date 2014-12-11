using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EventNotifier
{
    public class StateObject
    {
        public const int BufferSize = 128;
        public byte[] Buffer = new byte[BufferSize];
    }
}
