using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWPF.Messages
{
    class WindkrachtMessage
    {
        public enum MessageType { Updated, Deleted, Inserted, GetAll, GetWhere };

        public WindkrachtMessage(MessageType Actie)
        {
            Type = Actie;
        }

        public MessageType Type { get; set; }
    }
}
