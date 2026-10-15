using PaleuzeBackend.Api.Authentication;
using PaleuzeBackend.Api.Configuration;
using PaleuzeBackend.Api.Middleware;
using PaleuzeBackend.Business.Interfaces;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(opt =>
{
    opt.Conventions.Insert(0, new RoutePrefixConvention("api"));
});
builder.Services.AddOpenApi();

// Injecting custom services before building the app.
builder.Services.AddBusinessServices();
builder.Services.AddDatabaseProvider(builder.Configuration);
builder.Services.AddSecurityProvider(builder.Configuration);

builder.Services.AddModelMapping(builder.Configuration);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUser, CurrentUserAccessor>();

builder.Services.AddExceptionHandler<ExceptionHandlerMiddleware>();
builder.Services.AddProblemDetails();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("Development", policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
    });
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseCors("Development");
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseExceptionHandler();

app.Run();
