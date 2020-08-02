using RealEstates.Data;
using RealEstates.Models;
using RealEstates.Services.Interfaces;
using RealEstates.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;

namespace RealEstates.Services.Services
{
    public class PropertiesService : IPropertiesService
    {
        private RealEstateDbContext db;
        public PropertiesService(RealEstateDbContext db)
        {
            this.db = db;
        }

        public void Create(string district, int size, int? year, int price, string propertyType, string buildingType, int? floor, int? maxFloors)
        {
            if (district == null)
            {
                throw new ArgumentNullException(nameof(district));
            }

            //Property
            var property = new RealEstateProperty
            {
                Size = size,
                Price = price,
                Year = year < 1800 ? null : year,
                Floor = floor <= 0 ? null : floor,
                TotalNumberOfFloors = maxFloors <= 0 ? null : maxFloors,

            };

            //District
            var districtEntity = db
                .Districts
                .FirstOrDefault(x => x.Name.Trim() == district.Trim());

            if (districtEntity == null)
            {
                districtEntity = new District
                {
                    Name = district
                };
            }

            property.District = districtEntity;

            //Property Type

            var propertyTypeEntity = this.db.PropertyTypes.FirstOrDefault(x => x.Name.Trim() == propertyType.Trim());

            if (propertyTypeEntity == null)
            {
                propertyTypeEntity = new PropertyType { Name = propertyType };
            }

            property.PropertyType = propertyTypeEntity;

            //Building Type
            var buildingTypeEntity = this.db.BuildingTypes.FirstOrDefault(x => x.Name.Trim() == buildingType.ToString().Trim());

            if (buildingTypeEntity == null)
            {
                buildingTypeEntity = new BuildingType{ Name = buildingType };
            }

            property.BuildingType = buildingTypeEntity;

            this.db.RealEstateProperties.Add(property);
            db.SaveChanges();

            this.UpdateTags(property.Id);
        }

        public IEnumerable<PropertyViewModel> Search(int minYear, int maxYear, int minSize, int maxSize)
        {
            return db.RealEstateProperties
                .Where(x => x.Year >= minYear && x.Year <= maxYear && x.Size >= minSize && x.Size <= maxSize)
                .Select(MapToPropertyViewModel())
                .OrderBy(x => x.Price)
                .ToList();
        }

        public IEnumerable<PropertyViewModel> SearchByPrice(int minPrice, int maxPrice)
        {
            return this.db.RealEstateProperties
                .Where(x => x.Price >= minPrice && x.Price <= maxPrice)
                .Select(MapToPropertyViewModel())
                .OrderBy(x => x.Price)
                .ToList();
        }

        public void UpdateTags(int propertyId)
        {
            var property = this.db.RealEstateProperties.FirstOrDefault(x => x.Id == propertyId);
            property.Tags.Clear();

            if (property.Year.HasValue && property.Year < 1990)
            {
                var tag = this.GetOrCreateTag("OldBuilding");
                property.Tags.Add(new RealEstatePropertyTag { Tag = tag });
            }

            if (property.Size > 120)
            {
                var tag = this.GetOrCreateTag("HugeApartment");
                property.Tags.Add(new RealEstatePropertyTag { Tag = tag });
            }

            if (property.Year > 2018 && property.TotalNumberOfFloors > 5)
            {
                var tag = this.GetOrCreateTag("HasParking");
                property.Tags.Add(new RealEstatePropertyTag { Tag = tag });
            }

            if (property.Floor == property.TotalNumberOfFloors)
            {
                var tag = this.GetOrCreateTag("LastFloor");
                property.Tags.Add(new RealEstatePropertyTag { Tag = tag });
            }

            if (((double)property.Price / property.Size) < 800)
            {
                var tag = this.GetOrCreateTag("ExpensiveProperty");
                property.Tags.Add(new RealEstatePropertyTag { Tag = tag });
            }

            if (((double)property.Price / property.Size) > 2000)
            {
                var tag = this.GetOrCreateTag("ExpensiveProperty");
                property.Tags.Add(new RealEstatePropertyTag { Tag = tag });
            }

            this.db.SaveChanges();
        }

        private Tag GetOrCreateTag(string tagName)
        {
            var tagEntity = this.db.Tags.FirstOrDefault(x => x.Name.Trim() == tagName.Trim());

            if (tagEntity == null)
            {
                tagEntity = new Tag { Name = tagName };
            }

            return tagEntity;
        }

        private static Expression<Func<RealEstateProperty, PropertyViewModel>> MapToPropertyViewModel()
        {
            return x => new PropertyViewModel
            {
                Price = x.Price,
                Floor = 
                "" +
                (x.Floor ?? 0) 
                + "/" 
                + (x.TotalNumberOfFloors ?? 0),
                Size = x.Size,
                Year = x.Year,
                BuildingType = x.BuildingType.Name,
                District = x.District.Name,
                PropertyType = x.PropertyType.Name
            };
        }
    }
}
