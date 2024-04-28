using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace felevesHazi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //// Létrehozunk néhány FixIntervallum és ParosOrak objektumot
            ////FixIntervallum fix1 = new FixIntervallum { Fontossag = 1, Kezdet = 0, Veg = 5 };
            ////FixIntervallum fix2 = new FixIntervallum { Fontossag = 2, Kezdet = 6, Veg = 10 };
            ////FixIntervallum fix3 = new FixIntervallum { Fontossag = 3, Kezdet = 11, Veg = 15 };
            ////ParosOrak paros = new ParosOrak();

            ////// Létrehozzuk a listát és hozzáadjuk az objektumokat
            ////IdotartamList lista = new IdotartamList();
            ////lista.ElemBeszuras(fix1);
            ////lista.ElemBeszuras(fix2);
            ////lista.ElemBeszuras(fix3);
            ////lista.ElemBeszuras(paros);

            ////// Elemek kiírása
            ////Console.WriteLine("Elemek a listában:");
            ////lista.ElemKiir();
            ////Console.WriteLine();

            ////// Szűrés fontosság alapján
            ////Console.WriteLine("Szűrt lista (fontosság >= 20):");
            ////ListHelp szurtLista = lista.SzuresFontossagAlapjan(new int[] { 1, 2 });
            ////IdotartamList szurtIdotartamLista = new IdotartamList();
            ////ListHelp curr = szurtLista;
            ////while (curr != null)
            ////{
            ////    szurtIdotartamLista.ElemBeszuras(curr.Cucc);
            ////    curr = curr.Kov;
            ////}
            ////szurtIdotartamLista.ElemKiir();
            ////Console.WriteLine();

            //// Legnagyobb fontosságú lista keresése
            ////        Console.WriteLine("Legnagyobb fontosságú lista:");
            ////        ListHelp maxFontossaguLista = lista.MaxFontossaguLista();
            ////        IdotartamList maxFontossaguIdotartamLista = new IdotartamList();
            ////        curr = maxFontossaguLista;
            ////        while (curr != null)
            ////        {
            ////            maxFontossaguIdotartamLista.ElemBeszuras(curr.Cucc);
            ////            curr = curr.Kov;
            ////        }
            ////        maxFontossaguIdotartamLista.ElemKiir();

            Random rnd = new Random();
            IdotartamList idoList = new IdotartamList(20);

            // Létrehozunk néhány példa időtartamot és hozzáadjuk az idoListhez
            for (int i = 0; i < 10; i++)
            {
                FixIntervallum fixIntervallum = new FixIntervallum
                {
                    Fontossag = rnd.Next(1, 6),  // Random fontosság 1 és 5 között
                    Kezdet = rnd.Next(0, 12),  // Random kezdőidő 0 és 1200 perc között
                    Veg = rnd.Next(0, 44)   // Random végidő 1201 és 1440 perc között
                };

                // Hozzáadjuk az eseménykezelőt a fontosság változásának figyeléséhez
                fixIntervallum.FontossagValtozott += idoList.Uj_FontossagValtozott;
                idoList.ElemBeszuras(fixIntervallum);

            }

            for (int i = 0; i < 10; i++)
            {
                ParosOrak parosOrak = new ParosOrak();
                idoList.ElemBeszuras(parosOrak);
                parosOrak.FontossagValtozott += idoList.Uj_FontossagValtozott;
            }

            // Teszteljük a kiírást
            Console.WriteLine("Kezdeti állapot:");
            idoList.ElemKiir();

            // Teszteljük a törlést
            idoList.Torles(1, 0, 100);
            Console.WriteLine("\nÁllapot a törlés után:");
            idoList.ElemKiir();

            // Teszteljük a szűrést
            int[] fontossagiSzintek = new int[] { 2, 3 };
            ListHelp szurtElemek = idoList.SzuresFontossagAlapjan(fontossagiSzintek);
            Console.WriteLine("\nSzűrt elemek:");
            PrintList(szurtElemek);

            ////Teszteljük a visszalépéses keresést
            ////IIdotartam[] R = new IIdotartam[100];
            ////ListHelp current = idoList.Fej;
            ////for (int i = 0; i < 100 && current != null; i++)
            ////{
            ////    R[i] = current.Cucc;
            ////    current = current.Kov;
            ////}
            ////IIdotartam[] optSolution = idoList.GetOptimalSolution(R);
            ////Console.WriteLine("\nOptimális megoldás:");
            ////for (int i = 0; i < optSolution.Length; i++)
            ////{
            ////    if (optSolution[i] == null) continue;
            ////    if (optSolution[i] is FixIntervallum)
            ////    {
            ////        FixIntervallum fixIntervallum = optSolution[i] as FixIntervallum;
            ////        Console.WriteLine($"FixIntervallum: Fontossag = {fixIntervallum.Fontossag}, Kezdet = {fixIntervallum.Kezdet}, Veg = {fixIntervallum.Veg}");
            ////    }
            ////    else if (optSolution[i] is ParosOrak)
            ////    {
            ////        ParosOrak parosOrak = optSolution[i] as ParosOrak;
            ////        Console.WriteLine($"ParosOrak: Fontossag = {parosOrak.Fontossag}");
            ////    }
            ////}


            ////IIdotartam[] R = idoList.GetElements();
            ////IIdotartam[] optSolution = idoList.GetOptimalSolution(R);
            ////Console.WriteLine("\nOptimális megoldás:");
            ////for (int i = 0; i < optSolution.Length; i++)
            ////{
            ////    if (optSolution[i] == null) continue;
            ////    if (optSolution[i] is FixIntervallum)
            ////    {
            ////        FixIntervallum fixIntervallum = optSolution[i] as FixIntervallum;
            ////        Console.WriteLine($"FixIntervallum: Fontossag = {fixIntervallum.Fontossag}, Kezdet = {fixIntervallum.Kezdet}, Veg = {fixIntervallum.Veg}");
            ////    }
            ////    else if (optSolution[i] is ParosOrak)
            ////    {
            ////        ParosOrak parosOrak = optSolution[i] as ParosOrak;
            ////        Console.WriteLine($"ParosOrak: Fontossag = {parosOrak.Fontossag}");
            ////    }
            ////}
            ////IdotartamList idoList = new IdotartamList();

            //////Hozzunk létre példa intervallumokat és adjuk hozzá őket a listához
            ////FixIntervallum fi1 = new FixIntervallum { Fontossag = 2, Kezdet = 4, Veg = 6 };
            ////FixIntervallum fi2 = new FixIntervallum { Fontossag = 5, Kezdet = 1, Veg = 3 };
            ////FixIntervallum fi3 = new FixIntervallum { Fontossag = 1, Kezdet = 7, Veg = 9 };
            ////idoList.ElemBeszuras(fi1);
            ////idoList.ElemBeszuras(fi2);
            ////idoList.ElemBeszuras(fi3);

            //////Kiírjuk az időintervallumokat a listában
            ////Console.WriteLine("Az időintervallumok a listában:");
            ////idoList.ElemKiir();

            //////Teszteljük a torlés metódust
            ////idoList.Torles(1, 7, 9);
            ////Console.WriteLine("\nTöröltük az 1 fontosságú, 7-9 közötti időintervallumot:");
            ////idoList.ElemKiir();

            //////Teszteljük a szűrés metódust
            ////int[] fontossagiSzintek = new int[] { 2, 5 };
            ////ListHelp szurtElemek = idoList.SzuresFontossagAlapjan(fontossagiSzintek);
            ////Console.WriteLine("\nA szűrt elemek:");
            ////ListHelp current = szurtElemek;
            ////while (current != null)
            ////{
            ////    Console.WriteLine($"Fontossag = {current.Cucc.Fontossag}");
            ////    current = current.Kov;
            ////}
            ////IIdotartam[] optimalisMegoldas = idoList.GetOptimalSolution(idoList.GetElements());
            ////Console.WriteLine("\nAz optimális megoldás:");
            ////for (int i = 0; i < optimalisMegoldas.Length; i++)
            ////{
            ////    if (optimalisMegoldas[i] != null)
            ////    {
            ////        Console.WriteLine($"Fontossag = {optimalisMegoldas[i].Fontossag}");
            ////    }
            ////}
            ////IdotartamList idoList = new IdotartamList(5);  //A lista méretét 5-re állítottuk

            ////FixIntervallum fix1 = new FixIntervallum { Fontossag = 2, Kezdet = 4, Veg = 7 };
            ////idoList.ElemBeszuras(fix1);

            ////FixIntervallum fix2 = new FixIntervallum { Fontossag = 3, Kezdet = 4, Veg = 7 };
            ////idoList.ElemBeszuras(fix2);

            ////FixIntervallum fix3 = new FixIntervallum { Fontossag = 1, Kezdet = 8, Veg = 11 };
            ////idoList.ElemBeszuras(fix3);

            ////idoList.ElemKiir();
            IIdotartam[] optimalisMegoldas = idoList.GetOptimalSolution(idoList.GetElements());

            Console.WriteLine("\nAz optimális megoldás:");
            for (int i = 0; i < optimalisMegoldas.Length; i++)
            {
                if (optimalisMegoldas[i] != null)
                {
                    if (optimalisMegoldas[i] is FixIntervallum fixIntervallum)
                    {
                        Console.WriteLine($"FixIntervallum - Fontossag = {fixIntervallum.Fontossag}, Kezdet = {fixIntervallum.Kezdet}, Veg = {fixIntervallum.Veg}");
                    }
                    else if (optimalisMegoldas[i] is ParosOrak)
                    {
                        Console.WriteLine($"ParosOrak - Fontossag = {optimalisMegoldas[i].Fontossag}");
                    }
                }
            }

            Console.ReadLine();

            //IIdotartam[] R = idoList.GetFixIntervallums();
            //IIdotartam[] optimalSolution = idoList.GetOptimalSolution(R);

            //// Kiírjuk az optimális megoldást
            //foreach (var idotartam in optimalSolution)
            //{
            //    if (idotartam != null)
            //    {
            //        Console.WriteLine($", Hasznosság: {idotartam.Fontossag}");
            //    }
            //}


        }

        static void PrintList(ListHelp list)
        {
            ListHelp current = list;
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
    }
}
