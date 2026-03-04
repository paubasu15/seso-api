using SesoApi.Endpoints;
using SesoApi.Middleware;
using SesoApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddSingleton<TenantStore>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .SetIsOriginAllowed(origin =>
            {
                if (!Uri.TryCreate(origin, UriKind.Absolute, out var uri))
                    return false;
                if (uri.Scheme != "http" && uri.Scheme != "https")
                    return false;
                return uri.Host == "localhost" || uri.Host.EndsWith(".localhost");
            })
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// OpenAPI / Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
app.UseMiddleware<TenantMiddleware>();
app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Endpoints
app.MapHealthEndpoints();
app.MapTenantEndpoints();

app.Run();