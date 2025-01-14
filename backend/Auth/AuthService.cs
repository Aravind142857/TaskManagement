using backend.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
namespace backend.Auth
{
    public class AuthService(AppDbContext context, IConfiguration configuration, ILogger<AuthService> logger)
    {
        private readonly AppDbContext _context = context;
        private readonly IConfiguration _configuration = configuration;
        private readonly ILogger<AuthService> _logger = logger;

        public async Task<User> Register(UserRegisterInput input)
        {
            try {
                _logger.LogInformation("Attempting to find a user with provided username or email");
            var existingUser =  _context.Users.SingleOrDefault(u => u.Email == input.Email || u.Username == input.Username);
            if (existingUser != null)
            {
                // You could throw a more specific exception here
                throw new Exception("User with the same email or username already exists.");
            }
            // Reaches this point. TODO: Add more debug statements below
            _logger.LogInformation("Could not find a user with provided username or email");
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(input.Password);
            _logger.LogInformation("Hashed the password into {hashPassword}", passwordHash);
            var user = new User
            {
                Username = input.Username,
                Email = input.Email,
                PasswordHash = passwordHash
            };
            _logger.LogInformation("Created user with provided credentials");
            _context.Users.Add(user);
            _logger.LogInformation("Added user to users");
            await _context.SaveChangesAsync();
            return user;
            } catch (Exception ex) {
                throw new GraphQLException("An error occured during registration. {}", ex);
            }
        }

        public string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpiryInMinutes"])),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}