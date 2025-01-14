using backend.Data;
using HotChocolate;
using backend.Auth;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.SignalR;
using backend.Hubs;
namespace backend.Types
{
    public class RegisterResponse
    {
        [GraphQLNonNullType]
        public string Token { get; set; }

        [GraphQLNonNullType]
        public Guid UserId { get; set; }
    }
    public class LoginResponse
    {
        [GraphQLNonNullType]
        public string Token { get; set; }

        [GraphQLNonNullType]
        public Guid UserId { get; set; }
    }
    public class Mutation
    {
        private readonly ILogger<Mutation> _logger;
        private readonly AuthService _authService;
        public Mutation(ILogger<Mutation> logger, AuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }
        public async System.Threading.Tasks.Task<backend.Types.Task> CreateTask([Service] AppDbContext context, backend.Types.NewTaskInput input, string userId)
        {
            if (Guid.TryParse(userId, out Guid guid))
            {
            _logger.LogInformation("CreateTask mutation started");
            DateTime parsedDueDate;
            if (!DateTime.TryParse(input.DueDate, out parsedDueDate))
            {
                throw new ArgumentException("Invalid date format. Please use a valid DateTime string");
            }
            DateTime dueDateUtc = parsedDueDate.ToUniversalTime();

            var task = new backend.Types.Task()
            {
                Title = input.Title, Description = input.Description, DueDate = dueDateUtc, Priority = input.Priority, isCompleted = input.isCompleted
            };
            context.Tasks.Add(task);
            await context.SaveChangesAsync();
            var userTask = new backend.Types.UserTasks()
            {
                TaskId = task.Id,
                UserId = guid
            };
            context.UserTasks.Add(userTask);

            await context.SaveChangesAsync();
            return task;
            }
            else
            {
                throw new ArgumentException("");
            }
        }
        public async System.Threading.Tasks.Task<backend.Types.Task> UpdateTask([Service] AppDbContext context, [Service] IHubContext<CollaborationHub> hubContext , [ID] Guid id, backend.Types.NewTaskInput updatedTask)
        {
            // TODO: Find the task based on userId
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
                await hubContext.Clients.All.SendAsync("TaskUpdated", id, task);
            }
            return task;
        }
        public async System.Threading.Tasks.Task<bool> DeleteTask([Service] AppDbContext context, [ID] Guid id)
        {
            // TODO: Find the task based on userId
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
            // TODO: Find the task based on userId
            var task = await context.Tasks.FindAsync(id);
            if (task != null)
            {
                task.isCompleted = !task.isCompleted;
                await context.SaveChangesAsync();
            }
            return task;
        }
        // public async System.Threading.Tasks.Task<string> Register([Service] AppDbContext context, UserRegisterInput input)
        // {
        //     _logger.LogInformation("Registering from Mutation.cs");
        //     await context.SaveChangesAsync();
        //     return "Registered user ...";
        // }
        public async System.Threading.Tasks.Task<RegisterResponse> Register([Service] AppDbContext context, UserRegisterInput input)
        {
            try {
                _logger.LogInformation("Registering user with email {Email} and username {Username}", input.Email, input.Username);
                var user = await _authService.Register(input);
                _logger.LogInformation("User registered successfully with ID {UserId}", user.Id);
                return new RegisterResponse
                {
                    Token = _authService.GenerateJwtToken(user),
                    UserId = user.Id
                };
            } catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering user with email {Email} and username {Username}", input.Email, input.Username);
                throw new GraphQLException("An error occurred during registration.");
            }
        }
        // public async System.Threading.Tasks.Task<string> Login([Service] AppDbContext context, UserLoginInput input)
        // {
        //     _logger.LogInformation("Logging in from Mutation.cs");
        //     await context.SaveChangesAsync();
        //     return "Logged in user ...";
        // }
        public async System.Threading.Tasks.Task<LoginResponse> Login(UserLoginInput input, [Service] AppDbContext context)
        {
            var user = context.Users.SingleOrDefault(u => u.Email == input.Email);
            _logger.LogInformation("Found a user with {}", user?.Email);
            _logger.LogInformation("Login user");
            if (user == null || !BCrypt.Net.BCrypt.Verify(input.Password, user.PasswordHash))
            {
                throw new GraphQLException("Invalid email or password");
            }
            
            await System.Threading.Tasks.Task.Delay(1000);
            return new LoginResponse
            {
                Token = _authService.GenerateJwtToken(user),
                UserId = user.Id
            };
        }
        public async System.Threading.Tasks.Task<bool> DeleteUser([Service] AppDbContext context, [ID] Guid id)
        {
            var user = await context.Users.FindAsync(id);
            if (user != null)
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
       
    }
}