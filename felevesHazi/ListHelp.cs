using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace felevesHazi
{
    internal class ListHelp
    {
        public IIdotartam Cucc { get; set; }
        public ListHelp Kov { get; set; }
        public int TeljesFontossag { get; set; } //Ez tarolja az osszes idotartam fontossagat

        public ListHelp(IIdotartam cucc)
        {
            this.Cucc = cucc;
            this.TeljesFontossag = cucc.Fontossag;
        }
    }
}
