using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FubonMailApi.Models;

namespace FubonMailApi.Controllers.Services.Repositories.IRepositories
{

    public interface IRolePermissionsRepository
    {
        Task CreateAsync(List<RolePermissionsModels> RolePermissions);
        Task<IEnumerable<RolePermissionsModels>> ReadAllAsync(string role);
        Task<RolePermissionsModels> ReadOneAsync(int id);
        Task UpdateAsync(RolePermissionsModels RolePermissions);
        Task<Boolean> DeleteAsync(IEnumerable<RolePermissionsModels> RolePermissions);

    }
}