using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Eureka;
using Steeltoe.Discovery.Client;
using MMLib.SwaggerForOcelot.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOcelot(builder.Configuration).AddEureka();
builder.Services.AddSwaggerForOcelot(builder.Configuration);

builder.Services.AddDiscoveryClient(builder.Configuration);

builder.Configuration.AddJsonFile("Ocelot.json", optional: false, reloadOnChange: true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.UseOcelot().Wait();

app.Run();