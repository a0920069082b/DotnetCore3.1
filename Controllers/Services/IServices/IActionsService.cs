using FubonMailApi.Resources.Request;
using FubonMailApi.Resources.Response;
using FubonMailApi.Controllers.Services.Communication;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FubonMailApi.Controllers.Services.IServices
{
    public interface IActionsService
    {
        Task<SaveActionsResponse> CreateAsync(InsertActionsResource resource);
        Task<IEnumerable<ActionsResource>> ReadAllAsync(string action);
        Task<ActionsResource> ReadOneAsync(int id);
        Task<SaveActionsResponse> UpdateAsync(int id, UpdateActionsResource Actions);
        Task<Boolean> DeleteAsync(int id);
    }
}