using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Numerics;
using Business.APIBusinessServices.ThirtyPartyApp;
using Infrastructure.ConstantsDefine.KeywordTranslate;
using Models.Models.Response;
using Models.Models.Result;
using Repository.Implementation;
using Repository.Interfaces;

namespace Business.APIBusinessServices.CompanyService
{
	public class CompanyGetDetailService : BaseBusinessServices<Guid?, CompanyGetDetailResponse>
    {
        private readonly ICompanyRepository _companyRepository;       
        private CompanyDetailResult? _companyDetailResult;

        public CompanyGetDetailService(
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
            if (!_dataRequest.HasValue)
            {
                throw new BaseExceptionResult { Messages = ValidationKeywords.VALIDATION_FAIL };
            }         
        }

        public override void P2PostValidation()
        {
            
        }

        public override void P3AccessDatabase()
        {

            _companyDetailResult = _companyRepository.GetCompanyDetail(_dataRequest.Value);

            if (_companyDetailResult == null)
            {
                throw new BaseExceptionResult
                {
                    StatusCode = StatusCodes.Status404NotFound.ToString(),
                    Messages = SiteKeywords.SITE_NOT_EXIST
                };
            }
        }

        public override void P4GenerateResponseData()
        {
            
            _dataResponse = new CompanyGetDetailResponse
            {
                Id = _companyDetailResult.Id,
                Name = _companyDetailResult.Name,
                Address = _companyDetailResult.Address,
                Email = _companyDetailResult.Email,
                Phone = _companyDetailResult.Phone,
                TaxCode = _companyDetailResult.TaxCode,
                PartnerID = _companyDetailResult.PartnerID,
                PartnerUrl = _companyDetailResult.PartnerUrl,
                Username = _companyDetailResult.Username,
                Password = _companyDetailResult.Password,
                PartnerUrl2 = _companyDetailResult.PartnerUrl2,
                Username2 = _companyDetailResult.Username2,
                Password2 = _companyDetailResult.Password2,
                Avatar = _companyDetailResult.Avatar,
                CountryCode = _companyDetailResult.CountryCode,
                Status = _companyDetailResult.Status,
                PartnerConnectStatus = _companyDetailResult.PartnerConnectStatus
            };
        }
    }
}

