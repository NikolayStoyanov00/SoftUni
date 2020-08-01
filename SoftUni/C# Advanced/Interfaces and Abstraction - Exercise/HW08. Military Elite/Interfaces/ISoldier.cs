using System;
using System.Collections.Generic;
using System.Text;

namespace _08._Military_Elite.Interfaces
{
    public interface ISoldier
    {
        public int Id { get; }

        public string FirstName { get;}
        public string LastName { get; }
    }
}
