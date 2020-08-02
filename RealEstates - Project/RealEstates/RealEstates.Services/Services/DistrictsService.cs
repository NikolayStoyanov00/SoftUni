using RealEstates.Data;
using RealEstates.Models;
using RealEstates.Services.Interfaces;
using RealEstates.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace RealEstates.Services.Services
{
    public class DistrictsService : IDistrictsService
    {
        private RealEstateDbContext db;

        public DistrictsService(RealEstateDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<DistrictViewModel> GetTopDistrictsByAveragePrice(int count = 10)
        {
            return this.db.Districts
                .OrderByDescending(x => x.Properties.Average(p => (double)p.Price / p.Size))
                .Select(MapToDistrictViewModel())
                .Take(count)
                .ToList();
        }

        public IEnumerable<DistrictViewModel> GetTopDistrictsByNumberOfProperties(int count = 10)
        {
            return this.db.Districts
                .OrderByDescending(x => x.Properties.Count)
                .Select(MapToDistrictViewModel())
                .Take(count)
                .ToList();
        }

        private static Expression<Func<District, DistrictViewModel>> MapToDistrictViewModel()
        {
            return x => new DistrictViewModel
            {
                Name = x.Name,
                AveragePrice = x.Properties.Average(p => (double)p.Price / p.Size),
                MinPrice = x.Properties.Min(p => p.Price),
                MaxPrice = x.Properties.Max(p => p.Price),
                PropertiesCount = x.Properties.Count
            };
        }
    }
}
