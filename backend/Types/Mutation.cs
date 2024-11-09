using backend.Data;
using HotChocolate;

namespace backend.Types
{
    public class Mutation
    {
        public async System.Threading.Tasks.Task<backend.Types.Task> CreateTask([Service] AppDbContext context, backend.Types.TaskInput input)
        {
            DateTime parsedDueDate;
            if (!DateTime.TryParse(input.DueDate, out parsedDueDate))
            {
                throw new ArgumentException("Invalid date format. Please use a valid DateTime string");
            }
            DateTime dueDateUtc = parsedDueDate.ToUniversalTime();
            var task = new backend.Types.Task(){
                Id = input.Id, Title = input.Title, Description = input.Description, DueDate = dueDateUtc, Priority = input.Priority, isCompleted = input.isCompleted};
            context.Tasks.Add(task);
            await context.SaveChangesAsync();
            return task;
        }
        public async System.Threading.Tasks.Task<backend.Types.Task> UpdateTask([Service] AppDbContext context, int id, backend.Types.Task updatedtask)
        {
            var task = await context.Tasks.FindAsync(id);
            if (task != null)
            {
                task.Title = updatedtask.Title;
                task.Description = updatedtask.Description;
                task.DueDate = updatedtask.DueDate;
                task.Priority = updatedtask.Priority;
                task.isCompleted = updatedtask.isCompleted;
                await context.SaveChangesAsync();
            }
            return task;
        }
        public async System.Threading.Tasks.Task<bool> DeleteTask([Service] AppDbContext context, int id)
        {
            var task = await context.Tasks.FindAsync(id);
            if (task != null)
            {
                context.Tasks.Remove(task);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}