using Microsoft.EntityFrameworkCore;
using P03_SalesDatabase.Data;
using System;

namespace P03_SalesDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            SalesContext salesContext = new SalesContext();

            var sales = salesContext
                    .Sales
                    .FromSqlRaw("ALTER TABLE Sales\n" +
                                "ALTER COLUMN Date GETDATE()");

            salesContext.SaveChanges();

            salesContext.Database.Migrate(); 
        }
    }
}
