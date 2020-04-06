using FubonMailApi.Models;
using FubonMailApi.Controllers.Services.Communication;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FubonMailApi.Controllers.Services.IServices
{
    public interface IPermissionsService
    {
        Task<SavePermissionsResponse> CreateAsync(PermissionsModels Permissions);
        Task<IEnumerable<PermissionsModels>> ReadAllAsync(string function_name);
        Task<PermissionsModels> ReadOneAsync(int id);
        Task<SavePermissionsResponse> UpdateAsync(int id, PermissionsModels Permissions);
        Task<Boolean> DeleteAsync(int id);
    }
}