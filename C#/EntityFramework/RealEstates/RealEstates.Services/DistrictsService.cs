using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using AutoMapper.QueryableExtensions;
using RealEstates.Data;
using RealEstates.Services.Models;

namespace RealEstates.Services
{
    public class DistrictsService : BaseService, IDistrictsService
    {
        private ApplicationDbContext _context;

        public DistrictsService(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<DistrictInfoDto> GetMostExpensiveDistricts(int count)
        {
            var districts = this._context.Districts
                .ProjectTo<DistrictInfoDto>(this.Mapper.ConfigurationProvider)
                .OrderByDescending(x => x.AveragePricePerSquareMeter).Take(count)
                .ToList();

            return districts;
        }

        public decimal AveragePricePerSquareMeter(int districtId)
        {
            var district = this._context.Districts.FirstOrDefault(d => d.Id == districtId);

            if (district == null)
            {
                return 0;
            }

            // this._context.Entry(district).Collection(d => d.Properties).Load();

            return district.Properties.Average(x => x.Price / (decimal) x.Size) ?? 0;
        }

        public double AveragePropertySize(int districtId)
        {
            var district = this._context.Districts.FirstOrDefault(d => d.Id == districtId);

            if (district == null)
            {
                return 0;
            }

            // this._context.Entry(district).Collection(d => d.Properties).Load();

            return district.Properties.Average(p => p.Size);
        }
    }
}
