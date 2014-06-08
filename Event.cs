using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventNotifier
{
    public class Event
    {
        public DateTime time;
        public string monster;
        public string info;
        public string server;
        public string realm;

        public Event(string Time, string Monster, string Info, string Server, string Realm)
        {
            time = DateTime.Parse(Time);
            monster = Monster;
            info = Info;
            server = Server;
            realm = Realm;
        }
    }
}
