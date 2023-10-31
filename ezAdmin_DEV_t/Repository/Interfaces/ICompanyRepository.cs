using Models.EntityModels;
using Models.Models.Result;

namespace Repository.Interfaces
{
	public interface ICompanyRepository: IRepositoryBase<Company>
	{
        /// <summary>
        /// Get Company Info 
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        CompanyDetailResult? GetCompanyDetail(Guid? companyId);

    }
}

