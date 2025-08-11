using ConnectAssessment.Common.Repository;
using ConnectAssessment.Common.Service;
using ConnectAssessment.Repositories;
using ConnectAssessment.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<IPalindromeService, PalindromeService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ISettlementRepository, SettlementRepository>();
builder.Services.AddScoped<ICustomerSettlementService, CustomerSettlementService>();
builder.Services.AddScoped<ITransactionFeeService, TransactionFeeService>();
builder.Services.AddScoped<IBankApiService, BankApiService>();
builder.Services.AddHttpClient<IBankApiService, BankApiService>();

builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks()
    .AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), name: "sql")
    .AddCheck<CustomApiHealthCheck>("api_health");

builder.Services.AddDbContext<ConnectAssessment.Data.ConnectAssessmentDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

 var app = builder.Build();



app.UseSwagger();
app.UseSwaggerUI();
app.MapHealthChecks("/health");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
