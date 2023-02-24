using AccommodationAssistant.InfrastructureIoC;
using AccommodationAssistant.Presentation.Configurations;
using AccommodationAssistant.Presentation.Contracts;
using AccommodationAssistant.Presentation.Extensions;
using AccommodationAssistant.Presentation.Middlewares;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwagger();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddServices();
builder.Services.AddRepositories();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.Configure<ApiConfiguration>(builder.Configuration.GetSection("Api"));
builder.Services.AddScoped<ContractMapper>();

var connectionString = builder.Configuration.GetValue<string>("ConnectionStrings:DataContext");
builder.Services.AddDbContext(connectionString);

var app = builder.Build();

await app.Services.EnsureDatabaseMigratedAsync();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<ApiKeyMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();