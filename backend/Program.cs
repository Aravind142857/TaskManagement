using backend;
using backend.Auth;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Types;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using HotChocolate;
Thread.Sleep(10000);
ThreadPool.SetMinThreads(50, 50);
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options => {
    options.AddPolicy("AllowFrontend", 
        policy =>
        {
            policy.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
        });
});
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Trace);
// Add services to the container.
// Configure DbContext to use PostgreSQL, connecting to the database specified in appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    .LogTo(Console.WriteLine, LogLevel.Information));
    // .EnableSensitiveDataLogging());
builder.Services.AddScoped<AuthService>();
builder.Services.AddAuthentication(options => 
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"]!))
    };
});
builder.Services.AddAuthorization();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// builder.Services.AddControllers();
builder.Services
    .AddGraphQLServer()
    .AddMutationType<Mutation>()
    // .AddType<AuthMutationExtensions>()
    .AddQueryType<Query>()
    .AddInputObjectType<backend.Auth.UserLoginInput>()
    .AddInputObjectType<backend.Auth.UserRegisterInput>()
    .AddInputObjectType<backend.Types.NewTaskInput>()
    .AddType<backend.Types.Task>()
    .AddType<backend.Auth.User>();
    
    

var app = builder.Build();
app.UseCors("AllowFrontend");
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
// app.Use(async (context, next)=>
// {
//     var logger = context.RequestServices.GetService<ILogger<Program>>();
//     logger.LogInformation("Request {Method} {Path} started", context.Request.Method, context.Request.Path);
//     await next();
//     logger.LogInformation("Request {Method} {Path} ended", context.Request.Method, context.Request.Path);
// });
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
});
app.Urls.Add("http://0.0.0.0:80");
// app.UseHttpsRedirection();
// app.MapControllers();
app.Run();
