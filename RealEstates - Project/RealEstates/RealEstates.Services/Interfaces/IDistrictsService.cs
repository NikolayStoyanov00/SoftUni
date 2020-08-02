using RealEstates.Services.ViewModels;
using System.Collections.Generic;

namespace RealEstates.Services.Interfaces
{
    public interface IDistrictsService
    {
        IEnumerable<DistrictViewModel> GetTopDistrictsByAveragePrice(int count = 10);

        IEnumerable<DistrictViewModel> GetTopDistrictsByNumberOfProperties(int count = 10);
    }
}
