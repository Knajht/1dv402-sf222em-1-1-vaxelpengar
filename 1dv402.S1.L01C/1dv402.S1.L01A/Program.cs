using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1dv402.S1.L01C
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {            
                double subtotal = ReadPositiveDouble("Ange totalsumma: ");
                uint payedSum = ReadUint("Ange erhållet belopp: ", subtotal);

                uint total = (uint)Math.Round(subtotal, MidpointRounding.AwayFromZero);
                double roundingOffAmount = total - subtotal;
                uint returnSum = payedSum - total;

                Console.WriteLine("KVITTO\n-----------------------------------------");
                Console.WriteLine("Totalt: {0:c2}", subtotal);
                Console.WriteLine("Öresavrundning: {0:c2}", roundingOffAmount);
                Console.WriteLine("Att betala: {0:c2}", total);
                Console.WriteLine("Kontant: {0:c2}", payedSum);
                Console.WriteLine("Tillbaka: {0:c2}\n-----------------------------------------", returnSum);

                SplitIntoDenominations(returnSum);

                ViewMessage("Tryck ny tangent för ny beräkning - Esc avslutar");

            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
        static void SplitIntoDenominations(uint returnSum)
        {
            uint count = 0;
            uint[] values = { 500, 100, 50, 20, 10, 5, 1 };
            string form = null; ;
            
            foreach (uint i in values)
            {
                count = returnSum / i;
                returnSum -= i * count;
                returnSum = returnSum % i;
                if (count > 0)
                {
                    switch(i)
                    {
                        case 500:
                        case 100:
                        case 50:
                        case 20:
                            form = "lappar";
                            break;
                        case 10:
                        case 5:
                        case 1:
                            form = "kronor";
                            break;

                    }
                    
                    Console.WriteLine("{0}-{1}: {2}", i, form, count);
                }
            }
        }

        static double ReadPositiveDouble(string prompt)
        {
            double subtotal = 0;
            bool startOver = true;
            string input = null;

            do
            {
                try
                {
                    Console.Write(prompt);
                    input = (Console.ReadLine());
                    subtotal = double.Parse(input);
                    if (Math.Round(subtotal, MidpointRounding.AwayFromZero) < 1)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    startOver = false;
                }
                catch
                {
                    ViewMessage(String.Format("FEL! '{0}' kan inte tolkas som en giltig summa pengar.", input), true);
                }
            } while (startOver == true);

            return subtotal;
        }

        static uint ReadUint(string prompt, double minimum)
        {
            uint payedSum = 0;
            bool startOver = true;
            do
            {
                try
                {
                    Console.Write(prompt);
                    payedSum = uint.Parse(Console.ReadLine());
                    if (payedSum < Math.Round(minimum, MidpointRounding.AwayFromZero))
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    startOver = false;
                }
                catch
                {
                    ViewMessage("Ange ett heltal större än eller lika med totalsumman.", true);
                }
            } while (startOver == true);
            return payedSum;
        }

        static void ViewMessage(string message, bool IsError = false)
        {
            if(IsError == false)
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\n{0}", message);
                Console.ResetColor();
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n{0}", message);
                Console.ResetColor();
            }

        }

        static void ViewReceipt(double subtotal, double roundingOffAmount, uint total, uint cash, uint change, uint[] notes, uint[] denominations)
        {

        }

    }
}
