using System;
using FubonMailApi.Context;

namespace FubonMailApi.Controllers.Services.Repositories.Persistence
{
    public abstract class BaseRepository
    {
        protected readonly CustomContext _context;

        public BaseRepository(CustomContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}