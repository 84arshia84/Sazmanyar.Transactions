using Autofac.Extensions.DependencyInjection;
using Autofac;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;
using InfraStructure.Config;
using ApplicationService.Mapper.SettingMapper;
using Autofac.Core;
using ApplicationService.ServicesContract.ExceptionHandling;
using AppCore.UnitOfWork;
using InfraStructure.UnitOfWork;
using ApplicationService.Services.ExceptionHandlingService;
using InfraStructure.DataBase;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;
try
{


var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")), ServiceLifetime.Scoped);
builder.Services.AddSingleton<DbContextFactory>(sp =>
{
    var options = sp.GetRequiredService<DbContextOptions<AppDbContext>>();
    return new DbContextFactory(options);
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IErrorLoggerService, ErrorLoggerService>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "Sazmanyar",
            ValidAudience = "Clients",
            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes("b8139415067317b5793bcdfcc5dda4843f3120a255d6dbc2c3109559b761d544"))
        };
    });
builder.Services.AddControllers(
    options =>
    {
        var jsonInputFormatter = options.InputFormatters.OfType<Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonInputFormatter>().Single();
        jsonInputFormatter.SupportedMediaTypes.Add("multipart/form-data");
    }
    ).AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
         builder =>
         {
             builder.AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader();
         });
});
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "using Autofac.Extensions.DependencyInjection;\r\nusing Autofac;\r\nusing InfrStructure.DataBase;\r\nusing Microsoft.EntityFrameworkCore;\r\nusing Microsoft.OpenApi.Models;\r\nusing System;\r\nusing InfraStructure.Config;\r\nusing ApplicationService.Mapper.SettingMapper;\r\nusing Autofac.Core;\r\nusing ApplicationService.ServicesContract.ExceptionHandling;\r\nusing AppCore.UnitOfWork;\r\nusing InfraStructure.UnitOfWork;\r\nusing ApplicationService.Services.ExceptionHandlingService;\r\nusing InfraStructure.DataBase;\r\nusing Microsoft.AspNetCore.Authentication.JwtBearer;\r\nusing Microsoft.IdentityModel.Tokens;\r\nusing System.Text;\r\nusing System.Text.Json;\r\ntry\r\n{\r\n\r\n\r\nvar builder = WebApplication.CreateBuilder(args);\r\nvar env = builder.Environment;\r\nbuilder.Services.AddDbContext<AppDbContext>(options =>\r\n    options.UseSqlServer(builder.Configuration.GetConnectionString(\"DbConnection\")), ServiceLifetime.Scoped);\r\nbuilder.Services.AddSingleton<DbContextFactory>(sp =>\r\n{\r\n    var options = sp.GetRequiredService<DbContextOptions<AppDbContext>>();\r\n    return new DbContextFactory(options);\r\n});\r\nbuilder.Services.AddScoped<IUnitOfWork, UnitOfWork>();\r\nbuilder.Services.AddScoped<IErrorLoggerService, ErrorLoggerService>();\r\nbuilder.Services.AddAuthentication(options =>\r\n{\r\n    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;\r\n    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;\r\n})\r\n    .AddJwtBearer(options =>\r\n    {\r\n        options.TokenValidationParameters = new TokenValidationParameters\r\n        {\r\n            ValidateIssuer = true,\r\n            ValidateAudience = true,\r\n            ValidateLifetime = true,\r\n            ValidateIssuerSigningKey = true,\r\n            ValidIssuer = \"Sazmanyar\",\r\n            ValidAudience = \"Clients\",\r\n            IssuerSigningKey =\r\n                new SymmetricSecurityKey(\r\n                    Encoding.UTF8.GetBytes(\"b8139415067317b5793bcdfcc5dda4843f3120a255d6dbc2c3109559b761d544\"))\r\n        };\r\n    });\r\nbuilder.Services.AddControllers(\r\n    options =>\r\n    {\r\n        var jsonInputFormatter = options.InputFormatters.OfType<Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonInputFormatter>().Single();\r\n        jsonInputFormatter.SupportedMediaTypes.Add(\"multipart/form-data\");\r\n    }\r\n    ).AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);\r\n\r\n// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle\r\nbuilder.Services.AddEndpointsApiExplorer();\r\nbuilder.Services.AddSwaggerGen();\r\nbuilder.Services.AddCors(options =>\r\n{\r\n    options.AddPolicy(\"AllowAllOrigins\",\r\n         builder =>\r\n         {\r\n             builder.AllowAnyOrigin()\r\n                 .AllowAnyMethod()\r\n                 .AllowAnyHeader();\r\n         });\r\n});\r\nbuilder.Services.AddSwaggerGen(option =>\r\n{\r\n    option.SwaggerDoc(\"v1\", new OpenApiInfo { Title = \"Demo API\", Version = \"v1\" });\r\n    option.AddSecurityDefinition(\"Bearer\", new OpenApiSecurityScheme\r\n    {\r\n        In = ParameterLocation.Header,\r\n        Description = \"Please enter a valid token\",\r\n        Name = \"Authorization\",\r\n        Type = SecuritySchemeType.Http,\r\n        BearerFormat = \"JWT\",\r\n        Scheme = \"Bearer\"\r\n    });\r\n    option.AddSecurityRequirement(new OpenApiSecurityRequirement\r\n{\r\n        {\r\n            new OpenApiSecurityScheme\r\n            {\r\n                Reference = new OpenApiReference\r\n                {\r\n                    Type=ReferenceType.SecurityScheme,\r\n                    Id=\"Bearer\"\r\n                }\r\n            },\r\n            new string[]{}\r\n        }\r\n});\r\n});\r\n//Sql Config For EF Core\r\nbuilder.Services.AddDbContextPool<AppDbContext>(c => c.UseSqlServer(builder.Configuration.GetConnectionString(\"DbConnection\")));\r\n//End\r\nbuilder.Services.AddHttpClient();\r\n//config autofac\r\nbuilder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());\r\nbuilder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new AutofacModule(builder.Configuration.GetConnectionString(\"DbConnection\"))));\r\nHost.CreateDefaultBuilder(args).UseServiceProviderFactory(new AutofacServiceProviderFactory());\r\n\r\nbuilder.Logging.ClearProviders();\r\nbuilder.Logging.AddEventLog(eventLogSettings =>\r\n{\r\n    eventLogSettings.LogName = \"Sazmanyar\";\r\n    eventLogSettings.SourceName = \"Sazmanyar.Transaction.API\";\r\n});\r\n\r\nvar autofac = new ContainerBuilder();\r\nautofac.RegisterModule(new AutofacModule(builder.Configuration.GetConnectionString(\"DbConnection\")));\r\n// end config autofac\r\nbuilder.Services.AddHttpContextAccessor();\r\nvar app = builder.Build();\r\n// Configure the HTTP request pipeline.\r\napp.UseSwagger();\r\n\r\napp.UseSwaggerUI(c => c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None));\r\n\r\napp.UseCors(\"AllowAllOrigins\");\r\n\r\napp.UseHttpsRedirection();\r\n\r\nif (env.IsDevelopment())\r\n{\r\n    app.MapControllers().AllowAnonymous();\r\n}\r\nelse\r\n{\r\n    app.UseAuthentication();\r\n    app.UseAuthorization();\r\n    app.MapControllers();\r\n    //using (var scope = app.Services.CreateScope())\r\n    //{\r\n    //    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();\r\n    //    dbContext.Database.Migrate();\r\n    //}\r\n}\r\napp.Run();\r\n}\r\ncatch (Exception ex)\r\n{\r\n    string s = ex.InnerException == null ? \"\" : ex.InnerException.Message == null ? \"\" : ex.InnerException.Message;\r\n    System.IO.File.WriteAllLines(@\"C:\\Sazmanyar\\tam\\errors\\runTimError.txt\", new string[] { ex.Message, s });\r\n}",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
{
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
});
});
//Sql Config For EF Core
builder.Services.AddDbContextPool<AppDbContext>(c => c.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
//End
builder.Services.AddHttpClient();
//config autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new AutofacModule(builder.Configuration.GetConnectionString("DbConnection"))));
Host.CreateDefaultBuilder(args).UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Logging.ClearProviders();
builder.Logging.AddEventLog(eventLogSettings =>
{
    eventLogSettings.LogName = "Sazmanyar";
    eventLogSettings.SourceName = "Sazmanyar.Transaction.API";
});

var autofac = new ContainerBuilder();
autofac.RegisterModule(new AutofacModule(builder.Configuration.GetConnectionString("DbConnection")));
// end config autofac
builder.Services.AddHttpContextAccessor();
var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseSwagger();

app.UseSwaggerUI(c => c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None));

app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

if (env.IsDevelopment())
{
    app.MapControllers().AllowAnonymous();
}
else
{
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    //using (var scope = app.Services.CreateScope())
    //{
    //    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    //    dbContext.Database.Migrate();
    //}
}
app.Run();
}
catch (Exception ex)
{
    string s = ex.InnerException == null ? "" : ex.InnerException.Message == null ? "" : ex.InnerException.Message;
    System.IO.File.WriteAllLines(@"C:\Sazmanyar\tam\errors\runTimError.txt", new string[] { ex.Message, s });
}