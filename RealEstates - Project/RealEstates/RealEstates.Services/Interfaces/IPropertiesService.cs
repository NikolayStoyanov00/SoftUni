﻿using RealEstates.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstates.Services.Interfaces
{
    public interface IPropertiesService
    {
        public void Create(string district, int size, int? year, int price, string propertyType, string buildingType, int? floor, int? maxFloors);

        void UpdateTags(int propertyId);

        IEnumerable<PropertyViewModel> Search(int minYear, int maxYear, int minSize, int maxSize);

        IEnumerable<PropertyViewModel> SearchByPrice(int minPrice, int maxPrice);
    }
}
