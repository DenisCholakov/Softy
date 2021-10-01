using System;
using System.Collections.Generic;
using System.Text;
using RealEstates.Services.Models;

namespace RealEstates.Services
{
    public interface IDistrictsService
    {
        IEnumerable<DistrictInfoDto> GetMostExpensiveDistricts(int count);

        decimal AveragePricePerSquareMeter(int districtId);

        double AveragePropertySize(int districtId);
    }
}
