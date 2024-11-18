// using backend.Auth;
// using backend.Data;

// namespace backend.Types
// {
//     [ExtendObjectType(typeof(Mutation))]
//     public class AuthMutationExtensions
//     {
//         private readonly AuthResolver _authResolver;
//         private readonly ILogger<AuthMutationExtensions> _logger;
//         public AuthMutationExtensions(AuthResolver authResolver, ILogger<AuthMutationExtensions> logger)
//         {
//             _authResolver = authResolver;
//             _logger = logger;
//         }
//         public Task<string> Register(UserRegisterInput input)
//         {
//             _logger.LogInformation("AuthMutationExtensions Registering...");
//             return _authResolver.Register(input);
//         }
//         public Task<string> Login([Service] AppDbContext context, UserLoginInput input)
//         {
//             return _authResolver.Login(input, context);
//         }
//     }
// }