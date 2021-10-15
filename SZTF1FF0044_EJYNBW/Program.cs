using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SZTF1FF0044_EJYNBW
{
    public class Játék
    {
        static Random rng = new Random();
        public Játék()
        {

        }
        public void Mentés()
        {
            Console.WriteLine("mentés fájleneve: ");
            string fájlnév = Console.ReadLine();

            StreamWriter sw = new StreamWriter(fájlnév);


            sw.Close();
        }

        public bool Betöltés( string fájlnév )
        {
            return false;
        }

        public void Kirajzol()
        {
           // Console.Clear();

            Console.WriteLine("F2: Mentés F3: Betöltés F10: Kilépés");
        }

        public bool Vége()
        {
            return false;
        }
        public void Gondolkokdik()
        {
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Játék játék = new Játék();

            while( !játék.Vége() )
            {
                játék.Kirajzol();

                ConsoleKey billentyű = Console.ReadKey().Key;
                switch ( billentyű )
                {
                    case ConsoleKey.LeftArrow:
                        break;

                    case ConsoleKey.F10:
                        return;
                        break;
                    
                    default:
                        Console.WriteLine(billentyű);
                        break;
                }

                játék.Gondolkokdik();
            }
            
        }
    }
}
