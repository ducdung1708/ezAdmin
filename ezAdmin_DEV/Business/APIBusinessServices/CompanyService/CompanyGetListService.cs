using System;
using System.Linq.Expressions;
using Business.APIBusinessServices.ThirtyPartyApp;
using Models.EntityModels;
using Models.Models.Request;
using Models.Models.Response;
using Repository.Interfaces;

namespace Business.APIBusinessServices.CompanyService
{
    public class CompanyGetListService : BaseBusinessServices<CompanyGetListRequest, List<CompanyGetListResponse>>
    {
        private readonly ICompanyRepository _companyRepository;
        private List<CompanyGetListResponse> _listCompanyQuery;

        public CompanyGetListService(
            ICompanyRepository companyRepository,
            SlackSendMessageServices slackSendMessageServices,
            IHttpContextAccessor httpContextAccessor,            
            string successMessageDefault = ""            
            ) : base(slackSendMessageServices, httpContextAccessor, successMessageDefault)
        {
            _companyRepository = companyRepository;
        }

        //Preprocess data request before using
        //Make data is beautiful
        public override void P1GenerateObjects()
        {
            _dataRequest.Name = _dataRequest.Name ?? "";
            _dataRequest.PageIndex = _dataRequest.PageIndex ?? 1;
            _dataRequest.PageSize = _dataRequest.PageSize ?? 20;
        }

        /// <summary>
        /// Check data valid???
        /// </summary>
        public override void P2PostValidation()
        {
            
        }

        public override void P3AccessDatabase()
        {
            int skipRows = _dataRequest.PageSize.Value * (_dataRequest.PageIndex.Value - 1);

            Expression<Func<Company, bool>> companyConditionSearch = s => (s.Name ?? "").Contains(_dataRequest.Name);

            _listCompanyQuery = _companyRepository
                .GetBy(companyConditionSearch)
                .Select(s => new CompanyGetListResponse
                {
                    Id = s.Id,
                    Address = s.Address,
                    Name = s.Name,
                    Email = s.Email
                    
                })
                .Skip(skipRows)
                .Take(_dataRequest.PageSize.Value)
                .ToList();
        }

        public override void P4GenerateResponseData()
        {
            _dataResponse = _listCompanyQuery;            
        }
    }
}

