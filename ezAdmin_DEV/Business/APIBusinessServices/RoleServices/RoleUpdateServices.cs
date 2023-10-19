using Business.APIBusinessServices.ThirtyPartyApp;
using Infrastructure.ConstantsDefine.KeywordTranslate;
using Models.DBContext;
using Models.EntityModels;
using Models.Models.Request;
using Repository.Interfaces;

namespace Business.APIBusinessServices.Role
{
    public class RoleUpdateServices : BaseBusinessServices<GroupUserRoleUpdateRequest, object>
    {
        private readonly ezSQLDBContext _dbcontext;
        private readonly IRolesRepository _rolesRepository;

        List<MenuDefineAspNetGroupUser> groupUserRoles = new List<MenuDefineAspNetGroupUser>();
        List<MenuDefineAspNetGroupUser> listRolesAdd = new List<MenuDefineAspNetGroupUser>();
        List<MenuDefineAspNetGroupUser> listRolesDelete = new List<MenuDefineAspNetGroupUser>();

        private Guid groupUserIDRequest;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbcontext"></param>
        /// <param name="httpContextAccessor"></param>
        /// <param name="successMessageDefault"></param>
        public RoleUpdateServices(
            ezSQLDBContext dbcontext,
            IRolesRepository rolesRepository,
            SlackSendMessageServices slackSendMessageServices,
            IHttpContextAccessor httpContextAccessor,
            string successMessageDefault = CommonKeywords.UPDATE_SUCCESS) 
            : base(slackSendMessageServices, httpContextAccessor, successMessageDefault)
        {
            _dbcontext = dbcontext;
            _rolesRepository = rolesRepository;
        }

        public override void P1GenerateObjects()
        {
            groupUserIDRequest = _dataRequest.GroupUserID.Value;
            groupUserRoles = _rolesRepository
                .GetBy(x => x.AspNetGroupUserId == groupUserIDRequest)
                .ToList();
            foreach (RoleUpdateRequest roleUpdate in _dataRequest.RolesData)
            {
                var itemRoleLevel1UpdateDetail = groupUserRoles.FirstOrDefault(e => e.MenuCode == roleUpdate.MenuCode);
                bool itemRoleLevel1StatusUpdate = CheckStatusUpdateRole(roleUpdate);
                if (itemRoleLevel1UpdateDetail != null)
                {
                    if (!itemRoleLevel1StatusUpdate)
                    {
                        listRolesDelete.Add(itemRoleLevel1UpdateDetail);
                    }
                }
                else
                {
                    if (itemRoleLevel1StatusUpdate)
                    {
                        listRolesAdd.Add(new MenuDefineAspNetGroupUser
                        {
                            MenuDefineAspNetGroupUserId = Guid.NewGuid(),
                            AspNetGroupUserId = groupUserIDRequest,
                            CreatedDate = DateTime.UtcNow,
                            MenuCode = roleUpdate.MenuCode,
                        });
                    }
                }
                List<RoleUpdateRequest> rolesLevel2Update = roleUpdate.Childs ?? new List<RoleUpdateRequest>();
                foreach (RoleUpdateRequest roleLevel2Update in rolesLevel2Update)
                {
                    var itemRoleLevel2UpdateDetail = groupUserRoles.FirstOrDefault(e => e.MenuCode == roleLevel2Update.MenuCode);
                    bool itemRoleLevel2StatusUpdate = CheckStatusUpdateRole(roleLevel2Update);
                    if (itemRoleLevel2UpdateDetail != null)
                    {
                        if (!itemRoleLevel2StatusUpdate)
                        {
                            listRolesDelete.Add(itemRoleLevel2UpdateDetail);
                        }
                    }
                    else
                    {
                        if (itemRoleLevel2StatusUpdate)
                        {
                            listRolesAdd.Add(new MenuDefineAspNetGroupUser
                            {
                                MenuDefineAspNetGroupUserId = Guid.NewGuid(),
                                AspNetGroupUserId = groupUserIDRequest,
                                CreatedDate = DateTime.UtcNow,
                                MenuCode = roleLevel2Update.MenuCode,
                            });
                        }
                    }
                    List<RoleUpdateRequest> rolesLevel3Update = roleLevel2Update.Childs ?? new List<RoleUpdateRequest>();
                    foreach (RoleUpdateRequest roleLevel3Update in rolesLevel3Update)
                    {
                        var itemRoleLevel3UpdateDetail = groupUserRoles.FirstOrDefault(e => e.MenuCode == roleLevel3Update.MenuCode);
                        bool itemRoleLevel3StatusUpdate = CheckStatusUpdateRole(roleLevel3Update);
                        if (itemRoleLevel3UpdateDetail != null)
                        {
                            if (!itemRoleLevel3StatusUpdate)
                            {
                                listRolesDelete.Add(itemRoleLevel3UpdateDetail);
                            }
                        }
                        else
                        {
                            if (itemRoleLevel3StatusUpdate)
                            {
                                listRolesAdd.Add(new MenuDefineAspNetGroupUser
                                {
                                    MenuDefineAspNetGroupUserId = Guid.NewGuid(),
                                    AspNetGroupUserId = groupUserIDRequest,
                                    CreatedDate = DateTime.UtcNow,
                                    MenuCode = roleLevel3Update.MenuCode,
                                });
                            }
                        }
                    }
                }
            }
        }

        public override void P2PostValidation()
        {
        }
    

        public override void P3AccessDatabase()
        {
            _rolesRepository.AddRange(listRolesAdd);
            _rolesRepository.DeleteRange(listRolesDelete);
            _dbcontext.SaveChanges();
        }

        public override void P4GenerateResponseData()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleRequest"></param>
        /// <returns></returns>
        private bool CheckStatusUpdateRole(RoleUpdateRequest roleRequest)
        {
            return GetItemsRole(roleRequest).Any(s => s.Status ?? false);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleRequest"></param>
        /// <returns></returns>
        private List<RoleUpdateRequest> GetItemsRole(RoleUpdateRequest roleRequest)
        {
            List<RoleUpdateRequest> itemsRoleResult = new List<RoleUpdateRequest>();
            List<RoleUpdateRequest> rolesLevel2 = roleRequest.Childs;
            if (rolesLevel2 != null)
            {
                foreach (RoleUpdateRequest roleLevel2 in rolesLevel2)
                {
                    itemsRoleResult.Add(roleLevel2);
                    List<RoleUpdateRequest> rolesLevel3 = roleLevel2.Childs;
                    if (rolesLevel3 != null)
                    {
                        foreach (RoleUpdateRequest roleLevel3 in rolesLevel3)
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
