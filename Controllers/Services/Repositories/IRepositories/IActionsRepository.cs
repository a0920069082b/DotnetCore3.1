using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FubonMailApi.Models;

namespace FubonMailApi.Controllers.Services.Repositories.IRepositories {
    public interface IActionsRepository {
        Task CreateAsync (ActionsModels Actions);
        Task<IEnumerable<ActionsModels>> ReadAllAsync (string actions);
        Task<ActionsModels> ReadOneAsync (int id);
        Task UpdateAsync (ActionsModels Actions);
        Task<Boolean> DeleteAsync (int id, ActionsModels Actions);

    }
}