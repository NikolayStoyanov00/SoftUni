using _08._Military_Elite.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace _08._Military_Elite.Interfaces
{
    public interface IEngineer
    {
        public ICollection<Repair> Repairs { get; }
    }
}
