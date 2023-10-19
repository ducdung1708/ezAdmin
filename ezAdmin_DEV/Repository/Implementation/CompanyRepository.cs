using Models.DBContext;
using Models.EntityModels;
using Models.Models.Result;
using Repository.Interfaces;

namespace Repository.Implementation
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        private readonly ezSQLDBContext _dbcontext;

        public CompanyRepository(ezSQLDBContext dbContext) : base(dbContext)
        {
            _dbcontext = dbContext;
        }

        public CompanyDetailResult? GetCompanyDetail(Guid? companyId)
        {
            return _dbcontext.Companies
                .Where(s => s.Id == companyId)
                .Select(s => new CompanyDetailResult
                {
                    Id = s.Id,
                    Name = s.Name,
                    Address = s.Address,
                    Email = s.Email,
                    Phone = s.Phone,
                    TaxCode = s.TaxCode,
                    PartnerID = s.Partner,
                    PartnerUrl = s.PartnerUrl,
                    Username = s.Username,
                    Password = s.Password,
                    PartnerUrl2 = s.PartnerUrl2,
                    Username2 = s.Username2,
                    Password2 = s.Password2,
                    Avatar = s.Avatar,                    
                    CountryCode = s.CountryCode,
                    Status = s.Status,
                    PartnerConnectStatus = s.PartnerConnectStatus
                })
                .FirstOrDefault();
        }

    }
}

