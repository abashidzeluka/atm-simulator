using System.Net.Http.Headers;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ორი სრედი შექმენით და პირველმა რიცხვები ჩამოწეროს 0 დან 10 მდე// და მეორემ 10 დან 0 მდე პარალელურად ერთნაირი პრიორიტეტით

            

            Thread first = new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine(i);
                }

                

            });

            Thread second = new Thread(() =>
            {
                for (int i = 10; i > 0; i--)
                {
                    Console.WriteLine(i);
                }


            });

            first.Start();
            second.Start();

        }
    }
}
