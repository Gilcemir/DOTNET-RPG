using DOTNET_RPG.Data;
using DOTNET_RPG.Services.FighterService;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


var ConnectionStrings = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine(ConnectionStrings);
builder.Services.AddDbContext<DataContext>(options => 
                                            options.UseSqlServer(connectionString: ConnectionStrings));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IFighterService, FighterService>();//Register FighterService. If the controller wants to inject Interface, the implementation class will be fighterService
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
{      //modified to use direct url
    options.SwaggerEndpoint("./swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
