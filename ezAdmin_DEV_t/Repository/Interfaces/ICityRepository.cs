using Models.EntityModels;
using Models.Models.Result;

namespace Repository.Interfaces
{
	public interface ICityRepository: IRepositoryBase<City>
	{
        /// <summary>
        /// Get City Info 
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        CityDetailResult? GetCityDetail(Guid? cityId);

    }
}

