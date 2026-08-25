using FluentValidation;
using FluentValidation.AspNetCore;
using Ixp.Interview.Api.Auth;
using Ixp.Interview.Api.Data;
using Ixp.Interview.Api.Middleware;
using Ixp.Interview.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddSingleton<FaultStore>();
builder.Services.AddSingleton<IFaultCache, FaultCache>();
builder.Services.AddScoped<ICurrentUserAccessor, CurrentUserAccessor>();
builder.Services.AddScoped<IFaultService, FaultService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();
app.UseMiddleware<CorrelationIdMiddleware>();
app.UseMiddleware<ApiKeyMiddleware>();
app.UseMiddleware<CurrentUserMiddleware>();
app.MapControllers();

app.Run();

public partial class Program;
