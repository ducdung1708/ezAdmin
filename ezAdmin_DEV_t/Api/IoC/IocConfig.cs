using Business.APIBusinessServices.Account;
using Business.APIBusinessServices.Language;
using Business.APIBusinessServices.Resource;
using Business.APIBusinessServices.Role;
using Business.APIBusinessServices.ThirtyPartyApp;
using Business.ezID;
using Infrastructure.Core.Cache;
using Infrastructure.Core.Email;
using Models.DBContext;
using Repository.Implementation;
using Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Business.APIBusinessServices.Menu;
using Business.APIBusinessServices.CompanyService;
using Business.APIBusinessServices.AccountServices;
using Business.APIBusinessServices.CountryServices;
using Business.APIBusinessServices.CityService;
using Business.APIBusinessServices.DistrictService;

namespace Api.Ioc
{
    public static class IocConfig
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICacheService, CacheService>();
            services.AddDbContextPool<ezSQLDBContext>(options =>
            {
                string connectionString = configuration.GetConnectionString("ezDBConnection");
                options.UseSqlServer(connectionString, sqlOpt =>
                {
                    sqlOpt.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null
                    );
                });
            }, 200);
            #region ==== Services ====

            #region ===== App Services =====
            services.AddTransient<AccountAuthorizedServices>();
            services.AddTransient<AccountSignOutServices>();
            services.AddTransient<GetResourceAppServices>();
            services.AddTransient<GetLanguageService>();
            services.AddTransient<UserSiteInfoGetSessionServices>();
            #endregion

            #region ===== Account Services =====
            services.AddTransient<AccountGetListServices>();
            services.AddTransient<AccountInviteServices>();
            services.AddTransient<AccountUpdateService>();
            services.AddTransient<AccountUpdateStatusServices>();
            services.AddTransient<AccountDeleteServices>();
            #endregion

            #region ===== Account Roles Services =====
            services.AddTransient<RoleGetServices>();
            services.AddTransient<RoleUpdateServices>();
            services.AddTransient<UserRoleChangeGroupServices>();
            #endregion

            

            #region ===== Company Services =====
            services.AddTransient<CompanyGetListService>();
            services.AddTransient<CompanyGetDetailService>();
            services.AddTransient<CompanyCreateService>();
            services.AddTransient<CompanyUpdateService>();
            services.AddTransient<CompanyDeleteService>();
            #endregion

            #region ===== Company Services =====
            services.AddTransient<CountryGetListService>();
            services.AddTransient<CountryGetDetailService>();
            services.AddTransient<CountryCreateService>();
            services.AddTransient<CountryUpdateService>();
            services.AddTransient<CountryDeleteService>();
            #endregion

            #region ===== City Services =====
            services.AddTransient<CityGetListService>();
            services.AddTransient<CityGetDetailService>();
            services.AddTransient<CityCreateService>();
            services.AddTransient<CityUpdateService>();
            services.AddTransient<CityDeleteService>();
            #endregion

            #region ===== District Services =====
            services.AddTransient<DistrictGetListService>();
            services.AddTransient<DistrictGetDetailService>();
            services.AddTransient<DistrictCreateService>();
            services.AddTransient<DistrictUpdateService>();
            services.AddTransient<DistrictDeleteService>();
            #endregion

            #region ===== Common Services =====
            services.AddTransient<SlackSendMessageServices>();
            services.AddTransient<ezIDAPIServices>();
            services.AddTransient<EmailService>();
            #endregion

            #endregion
            #region ==== Repositories ====
            services.AddTransient(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddScoped<IAspNetUserRepository, AspNetUserRepository>();
            services.AddScoped<IAspNetUserSessionRepository, AspNetUserSessionRepository>();
            services.AddScoped<IAspNetUserSiteRepository, AspNetUserSiteRepository>();            
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<IAspNetGroupUserRepository, AspNetGroupUserRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();            
            services.AddScoped<IRolesRepository, RolesRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IDistrictRepository, DistrictRepository>();

            #endregion
        }
    }
}