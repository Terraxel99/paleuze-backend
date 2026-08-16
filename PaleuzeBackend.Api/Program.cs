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
builder.Services.AddModelMapping();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
