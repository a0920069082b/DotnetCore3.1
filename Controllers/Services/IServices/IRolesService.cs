using FubonMailApi.Resources.Request;
using FubonMailApi.Resources.Response;
using FubonMailApi.Controllers.Services.Communication;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FubonMailApi.Controllers.Services.IServices
{
    public interface IRolesService
    {
        Task<SaveRolesResponse> CreateAsync(InsertRolesResource resource);
        Task<IEnumerable<RolesResource>> ReadAllAsync(string role);
        Task<IEnumerable<RolesResource>> ReadOneAsync(int id);
        Task<SaveRolesResponse> UpdateAsync(int id, UpdateRolesResource Roles);
        Task<Boolean> DeleteAsync(int id);
    }
}