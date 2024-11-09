using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Types;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using HotChocolate;
Thread.Sleep(10000);
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Configure DbContext to use PostgreSQL, connecting to the database specified in appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddType<backend.Types.Task>()
    .AddMutationType<Mutation>()
    .AddInputObjectType<backend.Types.TaskInput>();
// builder.Services.AddCors(options => {
//     options.AddDefaultPolicy(builder => {
//         builder
//             .WithOrigins("")
//     })
// })
// builder.Services.AddControllers();

var app = builder.Build();
app.Urls.Add("http://0.0.0.0:80");
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}
app.UseRouting();
// app.MapGraphQL();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
});
// app.UseHttpsRedirection();
// app.UseAuthorization();
// app.MapControllers();
app.Run();
