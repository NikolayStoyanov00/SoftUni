using System;
using System.Collections.Generic;
using System.Text;

namespace _08._Military_Elite.Interfaces
{
    public enum Corps
    {
        Airforces,
        Marines
    }
    public interface ISpecialisedSoldier
    {
        Corps Corps { get; }
    }
}
