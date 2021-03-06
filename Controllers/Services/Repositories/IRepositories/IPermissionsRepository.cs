using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FubonMailApi.Models;

namespace FubonMailApi.Controllers.Services.Repositories.IRepositories {
    public interface IPermissionsRepository {
        Task CreateAsync (PermissionsModels Permissions);
        Task<IEnumerable<PermissionsModels>> ReadAllAsync (string function_name);
        Task<PermissionsModels> ReadOneAsync (int id);
        Task UpdateAsync (PermissionsModels Permissions);
        Task<Boolean> DeleteAsync (int id, PermissionsModels Permissions);

    }
}