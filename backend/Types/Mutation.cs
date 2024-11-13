using backend.Data;
using HotChocolate;

namespace backend.Types
{
    public class Mutation
    {
        public async System.Threading.Tasks.Task<backend.Types.Task> CreateTask([Service] AppDbContext context, backend.Types.NewTaskInput input)
        {
            DateTime parsedDueDate;
            if (!DateTime.TryParse(input.DueDate, out parsedDueDate))
            {
                throw new ArgumentException("Invalid date format. Please use a valid DateTime string");
            }
            DateTime dueDateUtc = parsedDueDate.ToUniversalTime();
            var task = new backend.Types.Task(){
                Title = input.Title, Description = input.Description, DueDate = dueDateUtc, Priority = input.Priority, isCompleted = input.isCompleted};
            context.Tasks.Add(task);
            await context.SaveChangesAsync();
            return task;
        }
        public async System.Threading.Tasks.Task<backend.Types.Task> UpdateTask([Service] AppDbContext context, [ID] Guid id, backend.Types.NewTaskInput updatedTask)
        {
            var task = await context.Tasks.FindAsync(id);
            if (task != null)
            {
                DateTime parsedDueDate;
                if (!DateTime.TryParse(updatedTask.DueDate, out parsedDueDate))
                {
                    throw new ArgumentException("Invalid date format. Please use a valid DateTime string");
                }
                DateTime dueDateUtc = parsedDueDate.ToUniversalTime();
                task.Title = updatedTask.Title;
                task.Description = updatedTask.Description;
                task.DueDate = dueDateUtc;
                task.Priority = updatedTask.Priority;
                task.isCompleted = updatedTask.isCompleted;
                await context.SaveChangesAsync();
            }
            return task;
        }
        public async System.Threading.Tasks.Task<bool> DeleteTask([Service] AppDbContext context, [ID] Guid id)
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
        public async System.Threading.Tasks.Task<backend.Types.Task> ToggleTaskCompletion([Service] AppDbContext context, [ID] Guid id, bool isCompleted)
        {
            var task = await context.Tasks.FindAsync(id);
            if (task != null)
            {
                task.isCompleted = !task.isCompleted;
                await context.SaveChangesAsync();
            }
            return task;
        }
    }
}