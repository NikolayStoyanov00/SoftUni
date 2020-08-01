using BookShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.DataProcessor.ExportDto
{
    public class ExportCraziestAuthorDto
    {
        public string AuthorName { get; set; }

        public ICollection<CraziestAuthorBookDto> Books { get; set; }
    }

    public class CraziestAuthorBookDto
    {
        public string BookName { get; set; }

        public string BookPrice { get; set; }
    }
}
