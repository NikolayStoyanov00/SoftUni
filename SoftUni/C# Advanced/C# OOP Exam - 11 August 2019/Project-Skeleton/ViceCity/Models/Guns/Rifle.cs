using System;
using System.Collections.Generic;
using System.Text;
using ViceCity.Models.Guns.Contracts;

namespace ViceCity.Models.Guns
{
    public class Rifle : Gun, IGun
    {
        private const int bulletsPerBarrel = 50;
        private const int totalBullets = 500;

        public Rifle(string name) 
            : base(name, bulletsPerBarrel, totalBullets)
        {
        }

        public override int Fire()
        {
            if (this.BulletsPerBarrel <= 0)
            {
                this.BulletsPerBarrel = 50;
                this.TotalBullets -= 50;
            }

            if (this.CanFire == true)
            {
                this.BulletsPerBarrel -= 5;
                return 5;
            }

            return 0;
        }
    }
}
