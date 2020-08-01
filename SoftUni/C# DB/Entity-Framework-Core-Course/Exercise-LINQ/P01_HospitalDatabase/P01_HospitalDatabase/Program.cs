using Microsoft.EntityFrameworkCore;
using P01_HospitalDatabase.Data;
using System;

namespace P01_HospitalDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            HospitalContext dbContext = new HospitalContext();
            dbContext.Database.Migrate();
        }
    }
}
