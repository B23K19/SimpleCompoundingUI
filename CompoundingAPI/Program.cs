using CompoundingAPI.Data;
using CompoundingAPI.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;


var builder = WebApplication.CreateBuilder(args);
var connectionString = "server=crossover.proxy.rlwy.net;port=52666;database=railway;user=root;password=dEJQoIVQjOHYSShtWOsyPiVjCIGYMByd;";
var serverVersion = new MySqlServerVersion(new Version(9, 3, 0));

// Add services to the container.
builder.Services.AddDbContext<CompoundInterestAppDbContext>(options =>
    options.UseMySql(connectionString, serverVersion)
);
/*builder.Services.AddDbContext<CompoundInterestAppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);*/
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CompoundInterestAppDbContext>();
    db.Database.EnsureCreated();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy =>
{
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader();
});

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
