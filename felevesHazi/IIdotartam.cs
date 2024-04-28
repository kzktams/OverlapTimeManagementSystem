using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace felevesHazi
{
    interface IIdotartam
    {
        int Fontossag { get; set; }
        bool Tartalmazza(int x);
        bool Atfedi(IIdotartam ido);
        event EventHandler FontossagValtozott;

    }
}
