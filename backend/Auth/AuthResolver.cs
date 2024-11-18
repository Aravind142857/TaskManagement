// using HotChocolate;
// using HotChocolate.AspNetCore.Authorization;
// using backend.Data;
// using BCrypt.Net;
// using Microsoft.Extensions.Logging;
// namespace backend.Auth
// {
//     public class AuthResolver
//     {
//         private readonly AuthService _authService;
//         private readonly ILogger<AuthResolver> _logger;
//         public AuthResolver(AuthService authService, ILogger<AuthResolver> logger)
//         {
//             _authService = authService;
//             _logger = logger;
//         }
//         public async Task<string> Register(UserRegisterInput input)
//         {
//             try {
//                 _logger.LogInformation("Registering user with email {Email} and username {Username}", input.Email, input.Username);
//                 var user = await _authService.Register(input);
//                 _logger.LogInformation("User registered successfully with ID {UserId}", user.Id);
//                 return _authService.GenerateJwtToken(user);
//             } catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error registering user with email {Email} and username {Username}", input.Email, input.Username);
//                 throw new GraphQLException("An error occurred during registration.");
//             }
//         }
//         public async Task<string> Login(UserLoginInput input, AppDbContext context)
//         {
//             var user = context.Users.SingleOrDefault(u => u.Email == input.Email);
//             if (user == null || !BCrypt.Net.BCrypt.Verify(input.Password, user.PasswordHash))
//             {
//                 throw new GraphQLException("Invalid email or password");
//             }
//             await Task.Delay(1000);;
//             return _authService.GenerateJwtToken(user);
//         }
//     }
// }