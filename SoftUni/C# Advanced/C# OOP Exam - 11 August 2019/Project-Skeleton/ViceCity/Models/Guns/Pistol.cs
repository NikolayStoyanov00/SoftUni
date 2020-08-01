using System;
using System.Collections.Generic;
using System.Text;
using ViceCity.Models.Guns.Contracts;

namespace ViceCity.Models.Guns
{
    public class Pistol : Gun, IGun
    {
        private const int bulletsPerBarrel = 10;
        private const int totalBullets = 100;

        public Pistol(string name) 
            : base(name, bulletsPerBarrel, totalBullets)
        {
        }

        public override int Fire()
        {
            if (this.BulletsPerBarrel == 0)
            {
                this.BulletsPerBarrel = 10;
                this.TotalBullets -= 10;
            }

            if (this.CanFire == true)
            {
                this.BulletsPerBarrel--;
                return 1;
            }

            return 0;
        }
    }
}
