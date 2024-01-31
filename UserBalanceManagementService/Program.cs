using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using UserBalanceManagementService;
using UserBalanceManagementService.DataLayer;
using UserBalanceManagementService.DataLayer.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IUserBalanceRepository, UserBalanceRepository>();

builder.Services.AddDbContext<UserBalanceContext>(options =>
{
    options.UseInMemoryDatabase("UserBalanceDB")
        .EnableSensitiveDataLogging()
        .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

SampleData.Initialize(app);

app.UseAuthorization();

app.MapControllers();

app.Run();
