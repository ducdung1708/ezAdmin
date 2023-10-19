using Models.DBContext;
using Models.EntityModels;
using Models.Models.Result;
using Repository.Interfaces;

namespace Repository.Implementation
{
    public class DistrictRepository : RepositoryBase<District>, IDistrictRepository
    {
        private readonly ezSQLDBContext _dbcontext;

        public DistrictRepository(ezSQLDBContext dbContext) : base(dbContext)
        {
            _dbcontext = dbContext;
        }

        public DistrictDetailResult? GetDistrictDetail(Guid? districtId)
        {
            return _dbcontext.Districts
                .Where(s => s.Id == districtId)
                .Select(s => new DistrictDetailResult
                {
                    Id = s.Id,
                    Name = s.Name,
                    Code = s.Code,
                    AirportCode = s.AirportCode,
                    PhoneCode = s.PhoneCode,
                    ExtraCode = s.ExtraCode,
                    PostalCode = s.PostalCode,
                    Latitude = s.Latitude,
                    Longtitude = s.Longtitude,
                    SortIndex = s.SortIndex,
                    Active = s.Active,
                    CreatedDate = s.CreatedDate,
                    CreatedBy = s.CreatedBy,
                    UpdatedDate = s.UpdatedDate,
                    UpdatedBy = s.UpdatedBy,
                    CityId = s.CityId
                })
                .FirstOrDefault();
        }

    }
}

