using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace felevesHazi
{
    internal class FixIntervallum : IIdotartam
    {
        private int _intervallum;
        public int Fontossag
        {
            get { return _intervallum; }
            set 
            {
                if (_intervallum != value)
                {
                    _intervallum = value;
                    FontossagValtozott?.Invoke(this, EventArgs.Empty) ;
                }
            }
        }
        public int Kezdet { get; set; }
        public int Veg { get; set; }

        public event EventHandler FontossagValtozott;

        public bool Atfedi(IIdotartam ido)
        {
            for (int i = Kezdet; i < Veg; i++)
            {
                if (ido.Tartalmazza(i))
                {
                    return true;
                }
            }
            return false;
        }

        public bool Tartalmazza(int x)
        {
            if (Kezdet <= x && Veg >= x)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
