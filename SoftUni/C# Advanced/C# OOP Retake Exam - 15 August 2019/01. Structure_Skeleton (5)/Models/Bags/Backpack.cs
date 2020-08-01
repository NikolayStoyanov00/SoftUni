using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceStation.Models.Bags
{
    public class Backpack : IBag
    {
        public Backpack()
        {
            items = new List<string>();
        }

        private List<string> items;
        public ICollection<string> Items
        {
            get => this.items;

            private set
            {
                this.items = value.ToList();
            }
        }
    }
}
