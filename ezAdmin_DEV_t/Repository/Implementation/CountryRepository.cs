using Models.DBContext;
using Models.EntityModels;
using Models.Models.Result;
using Repository.Interfaces;

namespace Repository.Implementation
{    

    public class CountryRepository : RepositoryBase<Country>, ICountryRepository
    {
        private readonly ezSQLDBContext _dbcontext;

        public CountryRepository(ezSQLDBContext dbContext) : base(dbContext)
        {
            _dbcontext = dbContext;
        }

        public CountryDetailResult? GetCountryDetail(int countryId)
        {
            return _dbcontext.Countries
                .Where(s => s.Id == countryId)
                .Select(s => new CountryDetailResult
                {
                    Id = s.Id,
                    Name = s.Name,
                    Code = s.Code,
                    Description = s.Description,
                    CurrencyName = s.CurrencyName,
                    CurrencyFormat = s.CurrencyFormat,
                    DecimalSeparator = s.DecimalSeparator,
                    Symbol = s.Symbol
                })
                .FirstOrDefault();
        }

        public CountryDetailResult? GetMaxIdDetail()
        {
            return _dbcontext.Countries
                .OrderByDescending(t => t.Id)
                .Select(s => new CountryDetailResult
                {
                    Id = s.Id,
                    Name = s.Name,
                    Code = s.Code,
                    Description = s.Description,
                    CurrencyName = s.CurrencyName,
                    CurrencyFormat = s.CurrencyFormat,
                    DecimalSeparator = s.DecimalSeparator,
                    Symbol = s.Symbol
                })
                .FirstOrDefault();
        }
    }
}

