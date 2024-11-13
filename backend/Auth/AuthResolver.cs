using HotChocolate;
using HotCholate.AspNetCore.Authorization;
using backend.Data;
using Bcrypt.Net;

namespace backend.Auth
{
    public class AuthResolver
    {
        private readonly AuthService _authService;
        public AuthResolver(AuthService authService)
        {
            _authService = authService;
        }
        public async Task<string> Register(UserRegisterInput input)
        {
            var user = await _authService.Register(input);
            return _authService.GenerateJwtToken(user);
        }
        public async Task<string> Login(UserLoginInput input, AppDbContext context)
        {
            var user = context.Users.SingleOrDefault(u => u.Email == input.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(input.Password, user.PasswordHash))
            {
                throw new GraphQLException("Invalid email or password");
            }
            return _authService.GenerateJwtToken(user);
        }
    }
}