using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FubonMailApi.Context;
using Microsoft.EntityFrameworkCore;
using FubonMailApi.Models;
using FubonMailApi.Controllers.Services.Repositories.IRepositories;
using FubonMailApi.Controllers.Services.Repositories.Persistence;

namespace FubonMailApi.Controllers.Services.Repositories
{
    public class ActionsRepository : BaseRepository, IActionsRepository
    {

        public ActionsRepository(CustomContext context) : base(context)
        {

        }

        public async Task CreateAsync(ActionsModels Actioons)
        {
            await _context.actions.AddAsync(Actioons);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ActionsModels>> ReadAllAsync(string action)
        {
            var Actions = from a in _context.actions
                          select a;

            if(!string.IsNullOrEmpty(action))
                Actions = Actions.Where(a => a.action == action);
            
            Actions = Actions.Include(c => c.create_user)
                             .Include(u => u.update_user);
                             
            return await Actions.ToListAsync();
        }

        public async Task<ActionsModels> ReadOneAsync(int id)
        {
            ActionsModels Actioons = await _context.actions
                .Include(c => c.create_user)
                .Include(u => u.update_user)
                .SingleOrDefaultAsync(a => a.action_id == id);

            return Actioons;
        }

        public async Task UpdateAsync(ActionsModels Actioons)
        {
            _context.actions.Update(Actioons);
            await _context.SaveChangesAsync();
        }

        public async Task<Boolean> DeleteAsync(int id, ActionsModels Actioons)
        {
            _context.actions.Remove(Actioons);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}