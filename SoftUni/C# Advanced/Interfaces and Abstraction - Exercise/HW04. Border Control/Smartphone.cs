using System;
using System.Collections.Generic;
using System.Text;

namespace _04
{
    public class Smartphone : ICallable, IBrowsable
    {
        public string Call(string phoneNumber)
        {
            foreach (var character in phoneNumber)
            {
                if (!char.IsDigit(character))
                {
                    return "Invalid number!";
                }
            }
            return $"Calling... {phoneNumber}";
        }

        public string Browse(string websiteUrl)
        {
            foreach (var character in websiteUrl)
            {
                if (char.IsDigit(character))
                {
                    return "Invalid URL!";
                }
            }
            return $"Browsing: {websiteUrl}!";
        }

    }
}
