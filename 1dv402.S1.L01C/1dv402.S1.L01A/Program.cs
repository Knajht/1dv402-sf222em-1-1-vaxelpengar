using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using Message = _1dv402.S1.L01A.Properties.Messages;
// Har haft problem med namngivningen när jag bytt nivå på labben, får hitta ett bättre sätt i framtiden.

namespace _1dv402.S1.L01C
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {            
                double subtotal = ReadPositiveDouble(Message.Total_Prompt);
                uint payedSum = ReadUint(Message.PayedSum_Prompt, subtotal);
                uint total = (uint)Math.Round(subtotal, MidpointRounding.AwayFromZero);
                double roundingOffAmount = total - subtotal;
                uint returnSum = payedSum - total;
                uint[] denominations = { 500, 100, 50, 20, 10, 5, 1 };
                uint[] notes = SplitIntoDenominations(returnSum, denominations);

                ViewReceipt(subtotal, roundingOffAmount, total, payedSum, returnSum, notes, denominations);
                ViewMessage(Message.Continue_Prompt);

            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
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
                    input = Console.ReadLine();
                    subtotal = double.Parse(input);
                    if (Math.Round(subtotal, MidpointRounding.AwayFromZero) < 1)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    startOver = false;
                }
                catch
                {
                    ViewMessage(String.Format(Message.WrongInput_Error, input), true);
                }
            } while (startOver == true);

            return subtotal;
        }
        static uint ReadUint(string prompt, double minimum)
        {
            uint payedSum = 0;
            bool startOver = true;
            string input = null;

            do
            {
                try
                {
                    Console.Write(prompt);
                    input = Console.ReadLine();
                    payedSum = uint.Parse(input);
                    if (payedSum < Math.Round(minimum, MidpointRounding.AwayFromZero))
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    startOver = false;
                }
                catch (ArgumentOutOfRangeException)
                {
                    ViewMessage(Message.LowCash_Error, true);
                }
                catch
                {
                    ViewMessage(String.Format(Message.WrongInput_Error, input), true);
                    ViewMessage(Message.LowCash_Error, true);
                }
            } while (startOver == true);
            return payedSum;
        }
        static uint[] SplitIntoDenominations(uint returnSum, uint[] denominations)
        {
            uint count = 0;
            int iterator = 0;
            uint[] notes = new uint[denominations.GetLength(0)];

            foreach (uint i in denominations)
            {
                count = returnSum / i;
                returnSum -= i * count;
                returnSum = returnSum % i;
                notes[iterator] = count;
                iterator++;
            }
            return notes;
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
        static void ViewReceipt(double subtotal, double roundingOffAmount, uint total, uint payedSum, uint returnSum, uint[] notes, uint[] denominations)
        {
            int iterator = 0;
            string form = null;

            Console.WriteLine("\n");
            Console.WriteLine(Message.RecieptStart);
            Console.WriteLine(Message.RecieptLine);
            Console.WriteLine(String.Format("{0, -20} {1} {2, 20:c2}", Message.RecieptSubtotal, ":", subtotal));
            Console.WriteLine(String.Format("{0, -20} {1} {2, 20:c2}", Message.RecieptRounding, ":", roundingOffAmount));
            Console.WriteLine(String.Format("{0, -20} {1} {2, 20:c2}", Message.RecieptTotal, ":", total));
            Console.WriteLine(String.Format("{0, -20} {1} {2, 20:c2}", Message.RecieptPayedSum, ":", payedSum));
            Console.WriteLine(String.Format("{0, -20} {1} {2, 20:c2}", Message.RecieptReturnSum, ":", returnSum));
            Console.WriteLine(Message.RecieptLine);

            foreach (uint i in notes)
            {
                if (i > 0)
                {
                    uint value = denominations[iterator];
                    
                    switch (value)
                    {
                        case 500:
                        case 100:
                        case 50:
                        case 20:
                            form = Message.Denomination_Form1;
                            break;
                        case 10:
                        case 5:
                        case 1:
                            form = Message.Denomination_Form2;
                            break;
                    }
                    Console.WriteLine(String.Format("{0, 4}-{1, -15} {2} {3, 20}", value, form, ":", i));
                }
                iterator++;
            }
        }
    }
}
