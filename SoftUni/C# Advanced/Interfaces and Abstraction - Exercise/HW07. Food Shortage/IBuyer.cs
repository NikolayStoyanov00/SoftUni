using System;
using System.Collections.Generic;
using System.Text;

namespace _07._Food_Shortage
{
    public interface IBuyer
    {
        public int Food { get; set; }

        void BuyFood();
    }
}
