using System;
using Models.EntityModels;
using Models.Models.Result;

namespace Repository.Interfaces
{
    public interface ICountryRepository : IRepositoryBase<Country>
    {

        /// <summary>
        /// Get a detail info of a country object
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        CountryDetailResult? GetCountryDetail(int countryId);
        CountryDetailResult? GetMaxIdDetail();

    }
}

