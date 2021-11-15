using System;
using System.IO;

namespace SZTF1FF0044_EJYNBW
{
    public enum Tulajdonosok
    {
        Játékos,
        Gép
    }

    public enum Státuszok
    {
        Nincs,
        Levéve,
        Játékban
    }

    public class Bábu
    {
        public static string Üres = " ";

        public string Karakter()
        {
            if (Státusz == Státuszok.Nincs || Státusz == Státuszok.Levéve)
                return Üres;

            if ( Tulajdonos == Tulajdonosok.Játékos)
                return "J";
            
            return "G";
        }
        public Státuszok Státusz { get; set; } = Státuszok.Nincs;
        public int X { get; set; } = -1;
        public int Y { get; set; } = -1;
        public Tulajdonosok Tulajdonos { get; set; } = Tulajdonosok.Játékos;

        public Bábu(Tulajdonosok tulajdonos)
        {
            this.Tulajdonos = tulajdonos;
        }

        public Bábu() : this(Tulajdonosok.Játékos)
        {
        }
    }


    public class Játék
    {
        public Bábu[] bábuk = new Bábu[18];
        public Random rng = new Random();
        static int[,] malmok =
        {
                { 6, 0 }, { 6, 3 }, { 6, 6 },
                { 5, 1 }, { 5, 3 }, { 5, 5 },
                { 4, 2 }, { 4, 3 }, { 4, 4 },
                { 3, 0 }, { 3, 1 }, { 3, 2 }, { 3, 4 }, { 3, 5 }, { 3, 6 },
                { 2, 2 }, { 2, 3 }, { 2, 4 },
                { 1, 1 }, { 1, 3 }, { 1, 5 },
                { 0, 0 }, { 0, 3 }, { 0, 6 },

                {0,0}, {3,0}, {6,0},
                {1,1}, {3,1}, {5,1},
                {2,2}, {3,1}, {4,2},
                {0,3}, {1,3}, {2,3}, {4,3}, {5,3}, {6,3},
                {2,4}, {3,4}, {4,4},
                {1,5}, {3,5}, {5,5},
                {0,6}, {3,6}, {6,6}
        };


        public Játék()
        {
            for (int i = 0; i < 9; i++)
                bábuk[i] = new Bábu();

            for (int i = 9; i < 18; i++)
                bábuk[i] = new Bábu(Tulajdonosok.Gép);
        }

        public void Mentés()
        {
            Console.Write("Mentés fájleneve: ");
            string fájlnév = Console.ReadLine();

            StreamWriter sw = new StreamWriter(fájlnév);

            for (int i = 0; i < bábuk.Length; i++)
            {
                sw.Write("" + bábuk[i].X + " " + bábuk[i].Y + " ");
                switch( bábuk[i].Státusz )
                {
                    case Státuszok.Nincs:
                        sw.Write("Nincs");
                        break;
                    case Státuszok.Játékban:
                        sw.Write("Játékban");
                        break;
                    default:
                        sw.Write("Levéve");
                        break;
                }

                sw.Write(" ");

                if (bábuk[i].Tulajdonos == Tulajdonosok.Játékos)
                    sw.Write("Játékos");
                else
                    sw.Write("Gép");

                sw.WriteLine();
            }

            sw.Flush();
            sw.Close();
        }

        public void Betöltés()
        {
            Console.Write("Betöltés fájleneve: ");
            string fájlnév = Console.ReadLine();

            if (!File.Exists(fájlnév))
            {
                Console.WriteLine("A fájl nem létezik.");
                Console.ReadLine();
                return;
            }

            StreamReader sr = new StreamReader(fájlnév);

            for (int i = 0; i < bábuk.Length; i++)
            {
                string[] adatok = sr.ReadLine().Split(' ');

                bábuk[i].X = int.Parse(adatok[0]);
                bábuk[i].Y = int.Parse(adatok[1]);
                
                if (adatok[2] == "Nincs")
                    bábuk[i].Státusz = Státuszok.Nincs;
                else
                if (adatok[2] == "Játékban")
                    bábuk[i].Státusz = Státuszok.Játékban;
                else
                    bábuk[i].Státusz = Státuszok.Levéve;

                if (adatok[3] == "Játékos")
                    bábuk[i].Tulajdonos = Tulajdonosok.Játékos;
                else
                    bábuk[i].Tulajdonos = Tulajdonosok.Gép;
            }

            sr.Close();
        }

        public int GépBábuSzám()
        {
            int gép = 0;
            for (int i = 0; i < bábuk.Length; i++)
                if (bábuk[i].Tulajdonos == Tulajdonosok.Gép && bábuk[i].Státusz == Státuszok.Játékban)
                    ++gép;

            return gép;
        }

        public int JátékosBábuSzám()
        {
            int játékos = 0;
            for (int i = 0; i < bábuk.Length; i++)
                if (bábuk[i].Tulajdonos == Tulajdonosok.Játékos && bábuk[i].Státusz == Státuszok.Játékban)
                    ++játékos;
            
            return játékos;
        }

        public override string ToString()
        {
            string s = "7 [";
            Bábu bábu = Mező(6, 0);
            if (bábu == null)
                s += Bábu.Üres;
            else
                s += bábu.Karakter();
            s += "]---------[";
            bábu = Mező(6, 3);
            if (bábu == null)
                s += Bábu.Üres;
            else
                s += bábu.Karakter();
            s += "]---------[";
            bábu = Mező(6, 6);
            if (bábu == null)
                s += Bábu.Üres;
            else
                s += bábu.Karakter();
            s += "]";
            s += "  ["+JátékosBábuSzám() + " / " + GépBábuSzám()+"]";
            s += "\n   |           |           |\n";

            s += "6  |  [";
            bábu = Mező(5, 1);
            if (bábu == null)
                s += Bábu.Üres;
            else
                s += bábu.Karakter();
            s += "]-----[";
            bábu = Mező(5, 3);
            if (bábu == null)
                s += Bábu.Üres;
            else
                s += bábu.Karakter();
            s += "]-----[";
            bábu = Mező(5, 5);
            if (bábu == null)
                s += Bábu.Üres;
            else
                s += bábu.Karakter();
            s += "]  |\n   |   |       |       |   |\n";

            s += "5  |   |  [";
            bábu = Mező(4, 2);
            if (bábu == null)
                s += Bábu.Üres;
            else
                s += bábu.Karakter();
            s += "]-[";
            bábu = Mező(4, 3);
            if (bábu == null)
                s += Bábu.Üres;
            else
                s += bábu.Karakter();
            s += "]-[";
            bábu = Mező(4, 4);
            if (bábu == null)
                s += Bábu.Üres;
            else
                s += bábu.Karakter();
            s += "]  |   |\n   |   |   |   |   |   |   |\n";

            s += "4 [";
            bábu = Mező(3, 0);
            if (bábu == null)
                s += Bábu.Üres;
            else
                s += bábu.Karakter();
            s += "]-[";
            bábu = Mező(3, 1);
            if (bábu == null)
                s += Bábu.Üres;
            else
                s += bábu.Karakter();
            s += "]-[";
            bábu = Mező(3, 2);
            if (bábu == null)
                s += Bábu.Üres;
            else
                s += bábu.Karakter();
            s += "]     [";
            bábu = Mező(3, 4);
            if (bábu == null)
                s += Bábu.Üres;
            else
                s += bábu.Karakter();
            s += "]-[";
            bábu = Mező(3, 5);
            if (bábu == null)
                s += Bábu.Üres;
            else
                s += bábu.Karakter();
            s += "]-[";
            bábu = Mező(3, 6);
            if (bábu == null)
                s += Bábu.Üres;
            else
                s += bábu.Karakter();
            s += "]\n   |   |   |   |   |   |   |\n";

            s += "3  |   |  [";
            bábu = Mező(2, 2);
            if (bábu == null)
                s += Bábu.Üres;
            else
                s += bábu.Karakter();
            s += "]-[";
            bábu = Mező(2, 3);
            if (bábu == null)
                s += Bábu.Üres;
            else
                s += bábu.Karakter();
            s += "]-[";
            bábu = Mező(2, 4);
            if (bábu == null)
                s += Bábu.Üres;
            else
                s += bábu.Karakter();
            s += "]  |   |\n   |   |       |       |   |\n";

            s += "2  |  [";
            bábu = Mező(1, 1);
            if (bábu == null)
                s += Bábu.Üres;
            else
                s += bábu.Karakter();
            s += "]-----[";
            bábu = Mező(1, 3);
            if (bábu == null)
                s += Bábu.Üres;
            else
                s += bábu.Karakter();
            s += "]-----[";
            bábu = Mező(1, 5);
            if (bábu == null)
                s += Bábu.Üres;
            else
                s += bábu.Karakter();
            s += "]  |\n   |           |           |\n";

            s += "1 [";
            bábu = Mező(0, 0);
            if (bábu == null)
                s += Bábu.Üres;
            else
                s += bábu.Karakter();
            s += "]---------[";
            bábu = Mező(0, 3);
            if (bábu == null)
                s += Bábu.Üres;
            else
                s += bábu.Karakter();
            s += "]---------[";
            bábu = Mező(0, 6);
            if (bábu == null)
                s += Bábu.Üres;
            else
                s += bábu.Karakter();
            s += "]\n";

            s += "   A   B   C   D   E   F   G";
            return s;
        }

        public bool Vége()
        {
            for (int i = 0; i < bábuk.Length; i++)
                if (bábuk[i].Státusz == Státuszok.Nincs)
                    return false;

            if (JátékosBábuSzám() < 3)
            {
                Console.WriteLine("A gép nyert.");
                return true;
            }

            if (GépBábuSzám() < 3)
            {
                Console.WriteLine("A játékos nyert.");
                return true;
            }

            return false;
        }

        public void Lépjen()
        {
        }

        public Bábu ÚjJátékosBábu()
        {
            for (int i = 0; i < bábuk.Length; i++)
                if (bábuk[i].Státusz == Státuszok.Nincs && bábuk[i].Tulajdonos == Tulajdonosok.Játékos )
                    return bábuk[i];
            return null;
        }

        public Bábu ÚjGépBábu()
        {
            for (int i = 0; i < bábuk.Length; i++)
                if (bábuk[i].Státusz == Státuszok.Nincs && bábuk[i].Tulajdonos == Tulajdonosok.Gép )
                    return bábuk[i];
            return null;
        }

        public Bábu Mező(int x, int y)
        {
            for (int i = 0; i < bábuk.Length; i++)
                if (bábuk[i].X == x && bábuk[i].Y == y && bábuk[i].Státusz == Státuszok.Játékban )
                    return bábuk[i];

            return null;
        }

        public bool ÉrvényesMező( int x, int y )
        {
            for (int i = 0; i < malmok.GetLength(0); i++)
                if (x == malmok[i, 0] && y == malmok[i, 1])
                    return true;

            return false;
        }

        public int[] HovaTegyenAGép()
        {
            for (int j = 0; j < bábuk.Length; j++)
            {
                if (bábuk[j].Tulajdonos == Tulajdonosok.Gép || bábuk[j].Státusz != Státuszok.Játékban)
                    continue;
                
                for (int i = 0; i < malmok.GetLength(0); i++)
                {
                    if( malmok[i,0] == bábuk[j].X && malmok[i,1] == bábuk[j].Y )
                    {
                        int k = i - i % 3;

                        Bábu bábu1 = Mező(malmok[k + 0, 0], malmok[k + 0, 1]);
                        Bábu bábu2 = Mező(malmok[k + 1, 0], malmok[k + 1, 1]);
                        Bábu bábu3 = Mező(malmok[k + 2, 0], malmok[k + 2, 1]);

                        if((bábu1 != null) && ((bábu2 != null) || (bábu3 != null)) || ((bábu2 != null) && (bábu3 != null)) )
                        {
                            if ( (bábu1 != null) && bábu1.Tulajdonos == Tulajdonosok.Gép)
                                continue;
                            if ( (bábu2 != null) && bábu2.Tulajdonos == Tulajdonosok.Gép)
                                continue;
                            if ( (bábu3 != null) && bábu3.Tulajdonos == Tulajdonosok.Gép)
                                continue;

                            if( bábu1 == null )
                                return new int[] { malmok[k + 0, 0], malmok[k + 0, 1] };

                            if (bábu2 == null)
                                return new int[] { malmok[k + 1, 0], malmok[k + 1, 1] };

                            if (bábu3 == null)
                                return new int[] { malmok[k + 2, 0], malmok[k + 2, 1] };
                        }
                    }
                }
            }

            int x, y;

            do
            {
                x = rng.Next(0, 7);
                y = rng.Next(0, 7);
            }
            while (Mező(x, y) != null || !ÉrvényesMező(x, y));

            return new int[] { x, y};
        }

        public bool VanEMalom( int x, int y, Tulajdonosok tulajdonos )
        {
            for (int i = 0; i < malmok.GetLength(0); i++)
            {
                if (malmok[i, 0] == x && malmok[i, 1] == y)
                {
                    int j = 0;
                    for (int k = i - i % 3; k < (i - i % 3) + 3; k++)
                    {
                        Bábu bábu = Mező(malmok[k, 0], malmok[k, 1]);

                        if (bábu != null && bábu.Tulajdonos == tulajdonos)
                            ++j;
                    }
                    if( j == 3)
                        return true;
                }
            }
            return false;
        }

        public bool Mozoghat( int x0, int y0, int x, int y)
        {
            Bábu bábu = Mező(x0, y0);
            if (bábu == null)
                return false;

            for (int i = 0; i < malmok.GetLength(0); i++)
            {
                if (malmok[i, 0] == x0 && malmok[i, 1] == y0)
                {
                    if( (i % 3) == 0 )
                    {
                        int k = i + 1;
                        Bábu jobbrabábu = Mező(malmok[k, 0], malmok[k, 1]);
                        if (jobbrabábu == null && malmok[k, 0] == x && malmok[k, 1] == y)
                            return true;
                    }
                    if ((i % 3) == 1)
                    {
                        int k = i - 1;
                        Bábu balrabábu = Mező(malmok[k, 0], malmok[k, 1]);
                        if (balrabábu == null && malmok[k, 0] == x && malmok[k, 1] == y)
                            return true;
                        k = i + 1;
                        Bábu jobbrabábu = Mező(malmok[k, 0], malmok[k, 1]);
                        if (jobbrabábu == null && malmok[k, 0] == x && malmok[k, 1] == y)
                            return true;
                    }
                    if ((i % 3) == 2)
                    {
                        int k = i - 1;
                        Bábu balrabábu = Mező(malmok[k, 0], malmok[k, 1]);
                        if (balrabábu == null && malmok[k, 0] == x && malmok[k, 1] == y)
                            return true;
                    }
                }
            }
            return false;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Játék játék = new Játék();

            while( !játék.Vége() )
            {
                Console.Clear();
                Console.WriteLine(játék.ToString());

                Console.Write("\"Mentés\"\n\"Betöltés\"\n\"Kilép\"\nlépés: ");

                string s = Console.ReadLine().ToLower();

                if (s.Equals("betöltés") || s.Equals("b"))
                {
                    játék.Betöltés();
                    continue;
                }

                if (s.Equals("mentés") || s.Equals("m"))
                {
                    játék.Mentés();
                    continue;
                }

                if (s.Equals("kilép") || s.Equals("q"))
                    return;

                Bábu bábu;
                int x, y;

                if ((bábu = játék.ÚjJátékosBábu()) != null)
                {
                    if (s.Length != 2)
                    {
                        Console.WriteLine("pölö: A1\n");
                        Console.ReadLine();
                        continue;
                    }

                    x = s[1] - '1';
                    y = s[0] - 'a';

                    if (!játék.ÉrvényesMező(x, y))
                    {
                        Console.WriteLine("Érvénytelen mező.\n");
                        Console.ReadLine();
                        continue;
                    }

                    if (játék.Mező(x, y) != null)
                    {
                        Console.WriteLine("Ott már van bábú.");
                        Console.ReadLine();
                        continue;
                    }

                    bábu.X = x;
                    bábu.Y = y;
                    bábu.Státusz = Státuszok.Játékban;
                }
                else
                {
                    if (s.Length != 5)
                    {
                        Console.WriteLine("pölö: A1 B2\n");
                        Console.ReadLine();
                        continue;
                    }

                    int x0 = s[1] - '1';
                    int y0 = s[0] - 'a';

                    x = s[4] - '1';
                    y = s[3] - 'a';

                    bábu = játék.Mező(x0, y0);
                    if (bábu == null || bábu.Tulajdonos != Tulajdonosok.Játékos)
                    {
                        Console.WriteLine("Ott nincs bábuja.");
                        Console.ReadLine();
                        continue;
                    }

                    if (játék.Mozoghat(x0, y0, x, y) == false) { 
                        Console.WriteLine("Oda nem mozoghat.");
                        Console.ReadLine();
                        continue;
                    }

                    bábu.X = x;
                    bábu.Y = y;
                }

                if ( játék.VanEMalom(x,y,Tulajdonosok.Játékos))
                {
                    bool kész = false;

                    while (!kész)
                    {
                        Console.Clear();
                        Console.WriteLine(játék.ToString());
                        Console.Write("\nMalom volt!\nLeütendő bábú: ");

                        s = Console.ReadLine().ToLower();

                        if (s.Length != 2)
                        {
                            Console.WriteLine("pölö: A1\n");
                            Console.ReadLine();
                            continue;
                        }

                        x = s[1] - '1';
                        y = s[0] - 'a';

                        if (!játék.ÉrvényesMező(x, y))
                        {
                            Console.WriteLine("Érvénytelen mező.\n");
                            Console.ReadLine();
                            continue;
                        }

                        bábu = játék.Mező(x, y);
                        if ( bábu == null)
                        {
                            Console.WriteLine("Bábút válasszon.");
                            Console.ReadLine();
                            continue;
                        }

                        if (bábu.Tulajdonos != Tulajdonosok.Gép )
                        {
                            Console.WriteLine("Ellenfél bábuját válassza.");
                            Console.ReadLine();
                            continue;
                        }

                        if( játék.VanEMalom(bábu.X, bábu.Y, Tulajdonosok.Gép) && játék.GépBábuSzám() > 3 )
                        {
                            Console.WriteLine("Azt nem ütheti.");
                            Console.ReadLine();
                            continue;
                        }

                        bábu.Státusz = Státuszok.Levéve;
                        kész = true;
                    }
                }

                if ((bábu = játék.ÚjGépBábu()) != null)
                {
                    int[] m = játék.HovaTegyenAGép();

                    bábu.X = m[0];
                    bábu.Y = m[1];
                    bábu.Státusz = Státuszok.Játékban;

                    x = bábu.X;
                    y = bábu.Y;

                    Console.WriteLine("A gép ide tett: " + (char)(y + 'A') + (char)(x + '1'));
                }
                else
                {
                    bool kész = false;

                    while (!kész)
                    {
                        Console.WriteLine("A gép lép.");

                        int i = játék.rng.Next(9, 18);

                        if (játék.bábuk[i].Státusz != Státuszok.Játékban)
                            continue;

                        x = játék.rng.Next(0, 7);
                        y = játék.rng.Next(0, 7);

                        if (játék.Mező(x, y) != null || !játék.ÉrvényesMező(x, y) || !játék.Mozoghat(játék.bábuk[i].X, játék.bábuk[i].Y, x, y) )
                            continue;
                       
                        Console.Clear();
                        Console.WriteLine(játék.ToString());
                        Console.WriteLine("A gép ide lépett: " + (char)(játék.bábuk[i].Y + 'A') + (char)(játék.bábuk[i].X + '1') +" " + (char)(y + 'A') + (char)(x + '1'));

                        játék.bábuk[i].X = x;
                        játék.bábuk[i].Y = y;

                        kész = true;
                    }
                }

                if (játék.VanEMalom(x, y, Tulajdonosok.Gép))
                {
                    bool kész = false;

                    while (!kész)
                    {
                        Console.Clear();
                        Console.WriteLine(játék.ToString());
                        Console.Write("\nMalom volt!\nGép jön.");

                        int i = játék.rng.Next(0, 9);

                        if (játék.bábuk[i].Státusz != Státuszok.Játékban)
                            continue;

                        if (játék.VanEMalom(játék.bábuk[i].X, játék.bábuk[i].Y, Tulajdonosok.Játékos) && játék.JátékosBábuSzám() > 3)
                            continue;

                        játék.bábuk[i].Státusz = Státuszok.Levéve;
                        kész = true;
                    }
                }
            }
        }
    }
}
