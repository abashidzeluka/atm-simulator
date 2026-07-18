using System;

namespace Domain.Helpers
{
    public class PinGenerator
    {
        public static string Generate()
        {
            Random random = new Random();
            int pin = random.Next(1000, 9999);
            return pin.ToString();
        }
    }
}
