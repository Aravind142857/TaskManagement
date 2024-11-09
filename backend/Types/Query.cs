using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
namespace backend.Types
{
    public class Query
    {
        public IQueryable<backend.Types.Task> GetTasks(AppDbContext context)
        {
            return context.Tasks;
        }
    }
}