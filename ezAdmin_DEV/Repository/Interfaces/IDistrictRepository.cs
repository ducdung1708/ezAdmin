using Models.EntityModels;
using Models.Models.Result;

namespace Repository.Interfaces
{
	public interface IDistrictRepository: IRepositoryBase<District>
	{
        /// <summary>
        /// Get City Info 
        /// </summary>
        /// <param name="districtId"></param>
        /// <returns></returns>
        DistrictDetailResult? GetDistrictDetail(Guid? districtId);

    }
}

