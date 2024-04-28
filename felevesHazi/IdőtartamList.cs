using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace felevesHazi
{

     class IdotartamList
    {
        private ListHelp fej;

        public IdotartamList()
        {
            fej = null;
        }
        
        public void ElemBeszuras(IIdotartam ido)
        {
            ListHelp help = new ListHelp(ido);
            ListHelp ujElem = new ListHelp(ido);
            ujElem.Cucc = ido;
            if (fej == null || fej.Cucc.Fontossag >= ido.Fontossag)
            {
                help.Kov = fej;
                fej = help;
            }
            else
            {
                ListHelp current = fej;
                while (current.Kov != null && current.Kov.Cucc.Fontossag < ido.Fontossag)
                {
                    current = current.Kov;
                }
                help.Kov = current.Kov;
                current.Kov = help;
            }
            ido.FontossagValtozott += Uj_FontossagValtozott;
        }

        public void Torles(int fontossag, int kezdid, int vegid)
        {
            ListHelp prev = null;
            ListHelp curr = fej;

            while (curr != null)
            {
                if (curr.Cucc.Fontossag == fontossag && curr.Cucc.Tartalmazza(kezdid) && curr.Cucc.Tartalmazza(vegid))
                {
                    // Az elem megfelel a szűrési feltételeknek, eltávolítjuk a listából
                    if (prev == null)
                    {
                        // Az első elemet távolítjuk el, frissítenünk kell a lista fejét
                        fej = curr.Kov;
                    }
                    else
                    {
                        prev.Kov = curr.Kov;
                    }
                }
                else
                {
                    //Az elem nem felel meg a szuresifelteteleknek, kovi
                    prev = curr;
                }
                curr = curr.Kov;
            }
        }

        public ListHelp SzuresFontossagAlapjan(int[] fontossagiSzintek)
        {
            // Létrehozunk egy új listát a szűrt elemek számára
            ListHelp szurtElemekFeje = null;
            ListHelp szurtElemekVege = null;

            ListHelp curr= fej;
            while (curr != null)
            {
                // Megnézzük, hogy az aktuális elem fontossága szerepel-e a fontossági szintek között
                for (int i = 0; i < fontossagiSzintek.Length; i++)
                {
                    if (curr.Cucc.Fontossag == fontossagiSzintek[i])
                    {
                        //Az elem fontossaga megfelel a szuresi felteteleknek, hozzaadjuk a szurt elemek listájához
                        if (szurtElemekFeje == null)
                        {
                            szurtElemekFeje = szurtElemekVege = new ListHelp(curr.Cucc);
                        }
                        else
                        {
                            szurtElemekVege = szurtElemekVege.Kov = new ListHelp(curr.Cucc);

                        }
                    }
                }
                curr = curr.Kov;
            }
            //visszaadjuk a szurt elemek listajat
            return szurtElemekFeje;
        }

       



        public void ElemKiir()
        {
            ListHelp current = fej;
            while (current != null)
            {
                if (current.Cucc is FixIntervallum)
                {
                    FixIntervallum fixIntervallum = current.Cucc as FixIntervallum;
                    Console.WriteLine($"FixIntervallum: Fontossag = {fixIntervallum.Fontossag}, Kezdet = {fixIntervallum.Kezdet}, Veg = {fixIntervallum.Veg}");
                }
                else if (current.Cucc is ParosOrak)
                {
                    ParosOrak parosOrak = current.Cucc as ParosOrak;
                    Console.WriteLine($"ParosOrak: Fontossag = {parosOrak.Fontossag}");
                }
                current = current.Kov;
            }
        }

        private IIdotartam[] E;
        private IIdotartam[] OPT;
        private bool van;
        private int maxHasznossag;
        private int N;

        public IdotartamList(int maxSize)
        {
            this.E = new IIdotartam[maxSize];
            this.OPT = new IIdotartam[maxSize];
            this.van = false;
            this.maxHasznossag = 0;
            this.N = maxSize;
        }

        private int maxFixIntervallumFontossag;
        public void VisszalepesesKereses(int szint, IIdotartam[] Rszint)
        {
            if (szint == Rszint.Length)
            {
                int aktualisHasznossag = HasznossagSzamitas();
                if (!van || aktualisHasznossag > N)
                {
                    van = true;
                    N = aktualisHasznossag;
                    OPT = (IIdotartam[])E.Clone();
                }
                return;
            }

            E[szint] = null;
            VisszalepesesKereses(szint + 1, Rszint);

            if (Ft(szint, Rszint[szint]))
            {
                if (Rszint[szint] is FixIntervallum || Rszint[szint].Fontossag >= maxFixIntervallumFontossag)
                {
                    E[szint] = Rszint[szint];
                    VisszalepesesKereses(szint + 1, Rszint);
                }
            }
        }

        


        private int HasznossagSzamitas()
        {
            int osszHasznossag = 0;
            for (int i = 0; i < E.Length; i++)
            {
                if (E[i] != null)
                {
                    if (E[i] is FixIntervallum fixIntervallum)
                    {
                        osszHasznossag += fixIntervallum.Fontossag + (fixIntervallum.Veg - fixIntervallum.Kezdet);
                        if (fixIntervallum.Fontossag > maxFixIntervallumFontossag)
                        {
                            maxFixIntervallumFontossag = fixIntervallum.Fontossag;
                        }
                    }
                    else
                    {
                        osszHasznossag += E[i].Fontossag;
                    }
                }
            }
            return osszHasznossag;
        }
        public IIdotartam[] GetOptimalSolution(IIdotartam[] R)
        {
            E = new IIdotartam[R.Length];
            VisszalepesesKereses(0, R);
            return OPT;
        }


        private bool Ft(int szint, IIdotartam rszinti)
        {
            for (int i = 0; i < szint; i++)
            {
                if (E[i] != null && E[i].Atfedi(rszinti))
                {
                    return false;
                }
            }
            return true;
        }

        private bool Fk(int szint, IIdotartam[] Rszint)
        {
            return Rszint != null && szint < Rszint.Length && Rszint[szint] != null;
        }

        private int Josag(IIdotartam[] E)
        {
            int sum = 0;
            for (int i = 0; i < E.Length; i++)
            {
                if (E[i] != null)
                {
                    sum += E[i].Fontossag;
                }
            }
            return sum;
        }

        public IIdotartam[] GetElements()
        {
            // Számoljuk meg az elemeket
            int count = 0;
            ListHelp current = fej;
            while (current != null)
            {
                count++;
                current = current.Kov;
            }

            // Hozzunk létre egy tömböt a pontos elemszámmal
            IIdotartam[] elements = new IIdotartam[count];
            current = fej;
            int i = 0;
            while (current != null)
            {
                elements[i++] = current.Cucc;
                current = current.Kov;
            }
            return elements;
        }

        public void FontossagValtozas()
        {
            IIdotartam[] optimalisMegoldas = GetOptimalSolution(GetElements());
            Console.WriteLine("\nAz optimális megoldás a fontosság változása után:");
            for (int i = 0; i < optimalisMegoldas.Length; i++)
            {
                if (optimalisMegoldas[i] != null)
                {
                    Console.WriteLine($"Fontossag = {optimalisMegoldas[i].Fontossag}");
                }
            }
        }

        public void Uj_FontossagValtozott(object sender, EventArgs e)
        {
            // Újra végrehajtjuk a leválogatást/keresést
            GetOptimalSolution(GetElements());
            Console.WriteLine("A fontosság megváltozott");
        }


    }
}
