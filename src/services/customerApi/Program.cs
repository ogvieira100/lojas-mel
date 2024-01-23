using Asp.Versioning;
using buildingBlocksCore.Data.PersistData.Context;
using buildingBlocksCore.Data.ReadData.Interfaces.Query;
using buildingBlocksCore.Data.ReadData.Query;
using buildingBlocksCore.Identity;
using buildingBlocksCore.IoC;
using buildingBlocksCore.Mediator;
using buildingBlocksCore.Utils;
using buildingBlocksMessageBus.Interfaces;
using buildingBlocksMessageBus.Models;
using customerApi.Automapper;
using customerApi.Model;
using customerApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NetDevPack.Security.JwtExtensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration; // allows both to access and to set up the config
IWebHostEnvironment environment = builder.Environment;

builder.Configuration.AddJsonFile("appsettings.json", true, true)
                    .SetBasePath(environment.ContentRootPath)
                    .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", true, true)
                    .AddEnvironmentVariables();
;

builder.Services.AddHttpContextAccessor();
DataBootstrap
    .ContextBootstrap(builder.Services)
    .NotaBootstrapReady()
    .NotaBootstrapWrite()
    .PedidoItensBootstrapReady()
    .PedidoBootstrapReady()
    .PedidoItensBootstrapWrite()
    .PedidoBootstrapWrite()
    .EnderecoBootstrapReady()
    .ProdutoBootstrapWrite()
    .ProdutoBootstrapReady()
    .FornecedorBootstrapReady()
    .FornecedorBootstrapWrite()
    .CustomerBootstrapWrite()
    .CustomerBootstrapReady()
    ;
// Add services to the container.
builder.Services.AddScoped<IMediatorHandler, MediatorHandler>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
//IMediatorHandler
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter JWT Bearer token **_only_**",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer", // must be lower case
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    // To Enable authorization using Swagger (JWT)  
    option.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new string[] { }}
                });

    option.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "Lojas Mel Api Customer",
        Description = "Esta API é uma Enterprise Applications.",
        Contact = new OpenApiContact() { Name = "Osmar Gonçalves Vieira", Email = "osmargv100@gmail.com" },
        License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
    });

    var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

    if (File.Exists(xmlCommentsFullPath))
        option.IncludeXmlComments(xmlCommentsFullPath);

});
builder.Services.AddHostedService<InsertCustomerIntegrationHandler>()
                .AddHostedService<DeleteCustomerIntegrationHandler>();

builder.Services.AddScoped<LNotifications>();
builder.Services.AddCors(options =>
{

    options.AddPolicy("Development",
          builder =>
              builder
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowAnyOrigin()
              ); // allow credentials

    options.AddPolicy("Production",
        builder =>
            builder
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowAnyOrigin()
              ); // allow credentials
});

/*versioning*/
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
}).AddApiExplorer(
options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddScoped<IUser, AspNetUser>();
//
builder.Services.AddScoped<LNotifications>();

var appSettings = configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettings);
var appSettingsValues = appSettings.Get<AppSettings>();



// JWT Setup


builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = true;
    x.SaveToken = true;
    x.SetJwksOptions(new JwkOptions(appSettingsValues.AutenticacaoJwksUrl));
});

builder.Services.AddAuthorization(options =>
{
    var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
    defaultAuthorizationPolicyBuilder = defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
    options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
});

#region  " Sql "

var connectionString = configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationContext>(options =>
     options.UseSqlServer(connectionString)
     .EnableSensitiveDataLogging()
     .UseLazyLoadingProxies()
     );


#endregion

builder.Services.AddSingleton<IMessageBusRabbitMq, MessageBusRabbitMq>();

builder.Services.AddAutoMapper(typeof(RequestToResponseModelMappingProfile));


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



                               

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
    app.UseCors("Development");
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("Production");
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    using (var appContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>())
    {
        try
        {
            appContext.Database.Migrate();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}

app.Run();
