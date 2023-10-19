using Models.DBContext;
using Models.EntityModels;
using Models.Models.Result;
using Repository.Interfaces;

namespace Repository.Implementation
{
    public class CityRepository : RepositoryBase<City>, ICityRepository
    {
        private readonly ezSQLDBContext _dbcontext;

        public CityRepository(ezSQLDBContext dbContext) : base(dbContext)
        {
            _dbcontext = dbContext;
        }

        public CityDetailResult? GetCityDetail(Guid? cityId)
        {
            return _dbcontext.Cities
                .Where(s => s.Id == cityId)
                .Select(s => new CityDetailResult
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
                    CountryId = s.CountryId
                })
                .FirstOrDefault();
        }

    }
}

