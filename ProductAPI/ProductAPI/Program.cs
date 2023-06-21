using ProductAPI.Contexts;
using ProductAPI.Repositories;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Newtonsoft.Json.Converters;
using ProductAPI.Configurations;
using GraphQL.Server;
using InventoryAPI.Schemas;
using GraphQL.Server.Ui.Playground;
using Steeltoe.Discovery.Client;
using Polly;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
// Add services to the container.

//builder.Services.AddControllers();

builder.Services.AddControllers(options =>
{
    options.RespectBrowserAcceptHeader = true;
    options.ReturnHttpNotAcceptable = true;
})
    .AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new StringEnumConverter()))
    .AddXmlSerializerFormatters()

.AddMvcOptions(options => options.OutputFormatters.Add(new CsvOutputFormatter()));
builder.Services.AddDiscoveryClient(configuration);
builder.Services.AddDbContext<ProductContext>(options =>
options.UseSqlServer(configuration.
GetConnectionString("ProdConn")));
builder.Services.AddTransient<IProductRepo, ProductRepo>(); 

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddApiVersioning(x =>
//{
//    x.DefaultApiVersion = new ApiVersion(1, 0);
//    x.AssumeDefaultVersionWhenUnspecified = true;
//    x.ReportApiVersions = true;
//    x.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
//});
builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                    new HeaderApiVersionReader("x-api-version"),
                                                    new MediaTypeApiVersionReader("x-api-version"));
});

builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});
var policyName = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policyName,
                      builder =>
                      {
                          builder
                            .WithOrigins("http://localhost:*", "")
                             //.WithOrigins("http://localhost:4200")
                             // specifying the allowed origin
                             // .WithMethods("GET") // defining the allowed HTTP method
                             //.AllowAnyOrigin()
                             // .WithHeaders(HeaderNames.ContentType, "ApiKey")
                             .AllowAnyMethod()
                            .AllowAnyHeader(); // allowing any header to be sent
                      });
});

builder.Services.AddScoped<ProductSchema>();
builder.Services.AddGraphQL()
               .AddSystemTextJson()
               .AddGraphTypes(typeof(ProductSchema), ServiceLifetime.Scoped);

//Retry Policy

builder.Services.AddHttpClient("CategoryApiClient", c =>
{
    c.BaseAddress = new Uri("http://localhost:6001/");
}).AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(new[]
{
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(15),
                 TimeSpan.FromSeconds(15)
            }));


//Circuit Breaker Policy
//circuit opens up after 2 consecutive trials

//builder.Services.AddHttpClient("cartApiClient", c => {
//    c.BaseAddress =
//new Uri("http://localhost:5097");
//})
//.AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(2, TimeSpan.FromMinutes(2)));


//Bulkhead Policy

builder.Services.AddSingleton<Polly.Bulkhead.AsyncBulkheadPolicy>((x) =>
{
    var policy = Policy.BulkheadAsync(
        maxParallelization: 5,
        maxQueuingActions: 5);

    return policy;
});



var app = builder.Build();
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    // app.UseSwaggerUI();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });
}
app.UseGraphQL<ProductSchema>();
app.UseGraphQLPlayground(options: new PlaygroundOptions());

app.UseHttpsRedirection();
app.UseCors(policyName);
app.UseAuthorization();

app.MapControllers();
app.UseSwagger();

app.Run();
