using Business.APIBusinessServices.ThirtyPartyApp;
using Infrastructure.ConstantsDefine.KeywordTranslate;
using Models.EntityModels;
using Models.Models.Response;
using Models.Models.Result;
using Repository.Interfaces;


namespace Business.APIBusinessServices.Menu
{
    public class RoleGetServices : BaseBusinessServices<Guid?, List<RolesGroupUserResponse>>
    {
        private readonly IRolesRepository _menuGroupUserRepository;
        private readonly IAspNetGroupUserRepository _aspNetGroupUserRepository;
        List<RolesResult> groupRoleTemplates = new List<RolesResult>();
        List<RolesResult> groupUserRoles = new List<RolesResult>();
        List<RolesGroupUserResponse> RoleResult = new List<RolesGroupUserResponse>();

        public RoleGetServices(
            IRolesRepository menuGroupUserRepository,
            IAspNetGroupUserRepository aspNetGroupUserRepository,
            SlackSendMessageServices slackSendMessageServices,
            IHttpContextAccessor httpContextAccessor,
            string successMessageDefault = CommonKeywords.GET_DATA_SUCESS) 
            : base(slackSendMessageServices, httpContextAccessor, successMessageDefault)
        {
            _menuGroupUserRepository = menuGroupUserRepository;
            _aspNetGroupUserRepository = aspNetGroupUserRepository;
        }
    
        public override void P1GenerateObjects()
        {
            if (!_dataRequest.HasValue)
            {
                throw new BaseExceptionResult { Messages = ValidationKeywords.GROUP_USER_IS_REQUIRED };
            }
            AspNetGroupUser groupUserDetail = _aspNetGroupUserRepository.GetById<Guid>(_dataRequest.Value);
            if (groupUserDetail == null)
            {
                throw new BaseExceptionResult { Messages = CommonKeywords.GROUP_USER_NOT_EXIST };
            }
            if (!groupUserDetail.SiteId.HasValue)
            {
                throw new BaseExceptionResult { Messages = ValidationKeywords.VALIDATION_FAIL };
            }
            AspNetGroupUser groupUserTemplateDetail = _aspNetGroupUserRepository.GetGroupUserTemplateDetail(groupUserDetail.GroupUserCode);
            if ( groupUserTemplateDetail == null)
            {
                throw new BaseExceptionResult { Messages = ValidationKeywords.VALIDATION_FAIL };
            }
            groupRoleTemplates = _menuGroupUserRepository.GetMenuByGroupId(groupUserTemplateDetail.AspNetGroupUserId);
            groupUserRoles = _menuGroupUserRepository.GetMenuByGroupId(_dataRequest);
        }

        public override void P2PostValidation()
        {
            var rolesLevel1 = groupRoleTemplates
                .Where(e => e.MenuParentCode == null)
                .OrderBy(s => s.MenuOrder)
                .Select(e => new RolesGroupUserResponse
                {
                    RoleId = null,
                    Keyword = e.Keyword,
                    MenuCode = e.MenuCode,
                    Icon = e.Icon,
                    Status = false,
                    Type = e.Type,
                    Indeterminate = false,
                    Childs = new List<RolesGroupUserResponse>()
                })
                .ToList();
            foreach (RolesGroupUserResponse roleLevel1 in rolesLevel1)
            {
                var result = groupUserRoles.FirstOrDefault(e => e.MenuCode == roleLevel1.MenuCode);
                if (result != null)
                {
                    roleLevel1.RoleId = result.RoleId;
                    roleLevel1.Status = true;
                }
                var rolesLevel2 = groupRoleTemplates
                    .Where(e => e.MenuParentCode == roleLevel1.MenuCode)
                    .OrderBy(s => s.MenuOrder)
                    .Select(e => new RolesGroupUserResponse
                    {
                        RoleId = null,
                        Keyword = e.Keyword,
                        MenuCode = e.MenuCode,
                        Icon = e.Icon,
                        Status = false,
                        Type= e.Type,
                        Indeterminate = false,
                        Childs = new List<RolesGroupUserResponse>()
                    })
                    .ToList();

                foreach (RolesGroupUserResponse roleLevel2 in rolesLevel2)
                {
                    var resultChild = groupUserRoles.FirstOrDefault(e => e.MenuCode == roleLevel2.MenuCode);
                    if (resultChild != null)
                    {
                        roleLevel2.RoleId = resultChild.RoleId;
                        roleLevel2.Status = true;
                    }
                    var rolesLevel3 = groupRoleTemplates
                        .Where(e => e.MenuParentCode == roleLevel2.MenuCode)
                        .OrderBy(s => s.MenuOrder)
                        .Select(e => new RolesGroupUserResponse
                        {
                            RoleId = null,
                            Keyword = e.Keyword,
                            MenuCode = e.MenuCode,
                            Icon = e.Icon,
                            Status = false,
                            Type = e.Type,
                            Indeterminate = false,
                            Childs = new List<RolesGroupUserResponse>()
                        })
                        .ToList();
                    foreach (RolesGroupUserResponse action in rolesLevel3)
                    {
                        var resultAction = groupUserRoles.FirstOrDefault(e => e.MenuCode == action.MenuCode);

                        if (resultAction != null)
                        {
                            action.RoleId = resultAction.RoleId;
                            action.Status = true;
                        }
                        roleLevel2.Childs.Add(action);
                    }
                    roleLevel2.Indeterminate = IndeterminateStatusNode(roleLevel2);
                    roleLevel1.Childs.Add(roleLevel2);
                }
                roleLevel1.Indeterminate = IndeterminateStatusNode(roleLevel1);
                RoleResult.Add(roleLevel1);
            }
        }

        public override void P3AccessDatabase()
        {

        }

        public override void P4GenerateResponseData()
        {
            _dataResponse = RoleResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleRequest"></param>
        /// <returns></returns>
        private bool IndeterminateStatusNode(RolesGroupUserResponse roleRequest)
        {
            List<RolesGroupUserResponse> roleItems = GetItemsInTreeRole(roleRequest);
            int totelItem = roleItems.Count();;
            if (totelItem <= 1)
            {
                return roleRequest.RoleId.HasValue && !(roleRequest.Status ?? false);
            }
            int totalActive = roleItems.Count(s => s.RoleId.HasValue && (s.Status ?? false));
            if (totalActive == 0) 
            {
                return false;
            }
            return totelItem != totalActive;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleRequest"></param>
        /// <returns></returns>
        private List<RolesGroupUserResponse> GetItemsInTreeRole(RolesGroupUserResponse roleRequest)
        {
            List<RolesGroupUserResponse> itemsRoleResult = new List<RolesGroupUserResponse>();
            List<RolesGroupUserResponse> rolesLevel2 = roleRequest.Childs;
            if (rolesLevel2 != null)
            {
                foreach (RolesGroupUserResponse roleLevel2 in rolesLevel2)
                {
                    itemsRoleResult.Add(roleLevel2);
                    List<RolesGroupUserResponse> rolesLevel3 = roleLevel2.Childs;
                    if (rolesLevel3 != null)
                    {
                        foreach (RolesGroupUserResponse roleLevel3 in rolesLevel3)
                        {
                            itemsRoleResult.Add(roleLevel3);
                        }
                    }
                }
            }
            if (itemsRoleResult.Count() == 0)
            {
                itemsRoleResult.Add(roleRequest);
            }
            return itemsRoleResult;
        }
    }
}
