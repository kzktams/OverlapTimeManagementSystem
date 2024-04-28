using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace felevesHazi
{
    internal class ParosOrak : IIdotartam
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
                    FontossagValtozott?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public ParosOrak(){ Fontossag = 3;}

        public event EventHandler FontossagValtozott;

        public bool Atfedi(IIdotartam ido)
        {
            for (int i = 0; i < 1440; i++)
            {
                if (Tartalmazza(i) && ido.Tartalmazza(i))
                {
                    return true;
                }
            }
            return false;
        }

        public bool Tartalmazza(int x)
        {
            int ora = x / 60;
            if (ora%2==0)
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
