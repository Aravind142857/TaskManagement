using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Auth;
using backend.Types;
using Microsoft.EntityFrameworkCore;
namespace backend.Types
{
    public class Query
    {
        [UseFiltering]
        [UseSorting]
        public IQueryable<backend.Types.Task> GetTasks([Service] AppDbContext context, string userId) {
            if (Guid.TryParse(userId, out Guid userGuid)) {
                return context.Tasks.Include(t => t.UserTasks).Where(t => t.UserTasks.Any(ut => ut.UserId == userGuid));
            }
            throw new ArgumentException("Invalid user id");
        }
        public IQueryable<backend.Types.Task> GetAllTasks(AppDbContext context)
        {
            return context.Tasks.Include(t => t.UserTasks).ThenInclude(ut => ut.User);
        }
        public IQueryable<backend.Auth.User> GetUsers(AppDbContext context)
        {
            return context.Users.Include(u => u.UserTasks).ThenInclude(ut => ut.Task);
        }
        public IQueryable<backend.Types.UserTasks> GetUserTasks(AppDbContext context)
        {
            return context.UserTasks
                .Include(ut => ut.User)
                .Include(ut => ut.Task);
        }

    }
}