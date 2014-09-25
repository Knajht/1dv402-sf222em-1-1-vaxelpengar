using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1dv402.S1.L01A
{
    class Program
    {
        static void Main(string[] args)
        {
            double subtotal = 0;
            int payedSum = 0;
            uint total;
            bool startOver = true;
            double roundingOffAmount;

            do
            {
                try
                {
                    Console.Write("Ange totalsumma: ");
                    subtotal = double.Parse(Console.ReadLine());
                    if(subtotal < 1)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    startOver = false;
                }
                catch
                {
                    Console.WriteLine("Ange en siffra större än eller lika med 1.");
                }  
            } while (startOver == true);

            startOver = true;
            do
            {
                try
                {
                    Console.Write("Ange erhållet belopp: ");
                    payedSum = int.Parse(Console.ReadLine());
                    if(payedSum < subtotal)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    startOver = false;
                }
                catch
                {
                    Console.WriteLine("Ange ett heltal större än totalsumman.");
                }
            } while (startOver == true);

            total = (uint)Math.Round(subtotal);
            roundingOffAmount = total - subtotal;
            int returnSum = payedSum - (int)total;

            

            Console.WriteLine("KVITTO\n-----------------------------------------");
            Console.WriteLine("Totalt: {0:c2}", subtotal);
            Console.WriteLine("Öresavrundning: {0:c2}", roundingOffAmount);
            Console.WriteLine("Att betala: {0:c2}", total);
            Console.WriteLine("Kontant: {0:c2}", payedSum);
            Console.WriteLine("Tillbaka: {0:c2}\n-----------------------------------------", returnSum);

            returnSum = Exchanger(returnSum, "lappar", 500);
            returnSum = Exchanger(returnSum, "lappar", 100);
            returnSum = Exchanger(returnSum, "lappar", 50);
            returnSum = Exchanger(returnSum, "lappar", 20);
            returnSum = Exchanger(returnSum, "kronor", 10);
            returnSum = Exchanger(returnSum, "kronor", 5);
            returnSum = Exchanger(returnSum, "kronor", 1);

        }
        static int Exchanger(int returnSum, string form, int i)
        {
            int value = returnSum / i;
            returnSum -= i * value;
            returnSum = returnSum % i;
            if(value > 0)
            {
                Console.WriteLine("{0}-{1}: {2}", i, form, value);
            }
            return returnSum;
        }
    }
}
