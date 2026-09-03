using PaleuzeBackend.Api.Configuration;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(opt =>
{
    opt.Conventions.Insert(0, new RoutePrefixConvention("api"));
});
builder.Services.AddOpenApi();

// Injecting custom services before building the app.
builder.Services.AddBusinessServices();
builder.Services.AddDatabaseProvider(builder.Configuration);
builder.Services.AddCustomAuthentication(builder.Configuration);
builder.Services.AddModelMapping(builder.Configuration);

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

app.Run();
