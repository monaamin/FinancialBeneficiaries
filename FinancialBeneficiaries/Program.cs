using FinancialBeneficiaries.ExternalServices;
using FinancialManagementDataLayer;
using FinancialManagementDataLayer.Repositories;
using FinancialManagementDataLayer.Repositories.Abstractions;
using FinancialManagementServices.UserBeneficialServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddTransient<IUserDetailsManagementService, UserDetailsManagementService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IBeneficiaryManagementService, BeneficiaryManagementService>();
builder.Services.AddTransient<IBeneficiaryRepository, BeneficiaryRepository>();
builder.Services.AddTransient<ITopUpOptionsManagementService, TopUpOptionsManagementService>();
builder.Services.AddTransient<ITopUpOptionsRepository, TopUpOptionsRepository>();
builder.Services.AddTransient<ITransactionManagementService, TransactionManagementService>();
builder.Services.AddTransient<ITopUpTransactionRepository, TopUpTransactionRepository>();
builder.Services.AddTransient<IUserBalanceInformationService, UserBalanceInformationService>();
builder.Services.AddTransient<ITopUpLimitOptionsRepository, TopUpLimitOptionsRepository>();


builder.Services.AddDbContext<FinancialManagementContext>(options =>
{
    options.UseInMemoryDatabase("FinancialManagementDB")
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



SampleData.Initialize(app);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
