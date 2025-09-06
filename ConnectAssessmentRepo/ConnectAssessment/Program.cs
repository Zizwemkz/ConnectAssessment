using ConnectAssessment.Common.Repository;
using ConnectAssessment.Common.Service;
using ConnectAssessment.Repositories;
using ConnectAssessment.Service;
using ConnectAssessment.Services;
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

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins(
                "http://localhost:3000")  // React dev server
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks()
    .AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), name: "sql")
    .AddCheck<CustomApiHealthCheck>("api_health");

builder.Services.AddDbContext<ConnectAssessment.Data.ConnectAssessmentDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

app.UseCors("AllowReactApp");

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI();
app.MapHealthChecks("/health");

app.UseAuthorization();

app.Use(async (context, next) =>
{
    if (context.Request.Method == "OPTIONS")
    {
        context.Response.StatusCode = 200;
        return;
    }
    await next();
});

app.MapControllers();
app.Run();
