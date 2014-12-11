using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventNotifier
{
    //Class that will hold the serialized output of a json line from the server
    public class JEvent
    {
        public double time;
        public string server;
        public string realm;
        public string key;
        public Dictionary<string, string> tokens;
    }
}
