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
//app.UseCors(policyName);
app.UseAuthorization();

app.MapControllers();
app.UseSwagger();

app.Run();
