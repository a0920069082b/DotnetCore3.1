using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FubonMailApi.Models;

namespace FubonMailApi.Controllers.Services.Repositories.IRepositories
{

    public interface IRolesRepository
    {
        Task CreateAsync(RolesModels Roles);
        Task<IEnumerable<RolesModels>> ReadAllAsync(string role);

        Task<RolesModels> ReadOneAsync(int id);
        Task UpdateAsync(RolesModels Roles);
        Task<Boolean> DeleteAsync(RolesModels Roles);

    }
}