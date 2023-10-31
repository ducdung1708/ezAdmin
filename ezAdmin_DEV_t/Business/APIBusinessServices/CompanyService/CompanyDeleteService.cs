using System;
using Business.APIBusinessServices.ThirtyPartyApp;
using Infrastructure.ConstantsDefine.KeywordTranslate;
using Models.Models.Request;
using Models.Models.Response;
using Models.Models.Result;
using Repository.Interfaces;

namespace Business.APIBusinessServices.CompanyService
{
    public class CompanyDeleteService : BaseBusinessServices<Guid?, CompanyDeleteResponse>
    {
        private readonly ICompanyRepository _companyRepository;
        private int nr;

        public CompanyDeleteService(
            ICompanyRepository companyRepository,
            SlackSendMessageServices slackSendMessageServices,
            IHttpContextAccessor httpContextAccessor,
            string successMessageDefault = ""
        ) : base(slackSendMessageServices, httpContextAccessor, successMessageDefault)
        {
            _companyRepository = companyRepository;
        }

        public override void P1GenerateObjects()
        {
            
        }

        public override void P2PostValidation()
        {
            if (_dataRequest == null || !_dataRequest.HasValue)
            {
                throw new BaseExceptionResult { Messages = ValidationKeywords.VALIDATION_FAIL };
            }
            
        }

        public override void P3AccessDatabase()
        {
             _companyRepository.Delete<Guid>(_dataRequest.Value);
             nr = _companyRepository.Save();
        }

        public override void P4GenerateResponseData()
        {
            _dataResponse = new CompanyDeleteResponse
            {
                Id = _dataRequest.Value,
                NumberOfRows = nr
            };
        }
    }
}
