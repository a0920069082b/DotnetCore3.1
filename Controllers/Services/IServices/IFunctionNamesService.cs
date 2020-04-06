using FubonMailApi.Resources.Request;
using FubonMailApi.Resources.Response;
using FubonMailApi.Controllers.Services.Communication;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FubonMailApi.Controllers.Services.IServices
{
    public interface IFunctionNamesService
    {
        Task<SaveFunctionNamesResponse> CreateAsync(InsertFunctionNamesResource FunctionNames);
        Task<IEnumerable<FunctionNamesResource>> ReadAllAsync(string function_name);
        Task<FunctionNamesResource> ReadOneAsync(int id);
        Task<SaveFunctionNamesResponse> UpdateAsync(int id, UpdateFunctionNamesResource FunctionNames);
        Task<Boolean> DeleteAsync(int id);
    }
}