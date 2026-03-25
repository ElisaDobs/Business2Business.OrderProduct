using Business2Business.Common.Authentication.BusinessLogic;
using Business2Business.Common.Authentication.Interfaces;
using Business2Business.Common.B2BDAL;
using Business2Business.Common.ErrorLoggers.Interfaces;
using Business2Business.Common.ErrorLoggers.Repository;
using Business2Business.Common.Orders.Interfaces;
using Business2Business.Common.Orders.Repository;
using Business2Business.Common.Products.Interfaces;
using Business2Business.Common.Products.Repository;
using Business2Business.Common.Users.Interfaces;
using Business2Business.Common.Users.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Security.Cryptography;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Business To Business API",
        Version = "v1"
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorisation",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "Jwt",
        In = ParameterLocation.Header,
        Description = "Please provide JWT bearer token: "
    });
});
string? _issuer = builder.Configuration["Jwt:Issuer"];
string? _audience = builder.Configuration["Jwt:Audience"];
RSA rsa = RSA.Create(2048);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new RsaSecurityKey(RSA.Create(2048)),//new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration.GetValue<string>("Jwt:Issuer"),
        ValidAudience = builder.Configuration.GetValue<string>("Jwt:Audience")
    };
});

builder.Services.AddAuthorization();
//Dependency Injection implemenation (new's not allowed)
B2BDBContext? instance = B2BDBContext.CreateInstance(builder.Configuration.GetConnectionString("B2BConnectionString"));
builder.Services.AddSingleton<IB2BAuthenticationManager>(_service => ActivatorUtilities.CreateInstance<B2BAuthenticationManager>(_service, _issuer, _audience));
builder.Services.AddSingleton<ILoggerRepository>(_service => ActivatorUtilities.CreateInstance<LoggerRepository>(_service, instance));
builder.Services.AddSingleton<IUserRepository>(_service => ActivatorUtilities.CreateInstance<UserRepository>(_service, instance));
builder.Services.AddSingleton<IProductRepository>(_service => ActivatorUtilities.CreateInstance<ProductRepository>(_service, instance));
builder.Services.AddSingleton<IOrderRepository>(_service => ActivatorUtilities.CreateInstance<OrderRepository>(_service, instance));
builder.Services.AddSingleton<IOrderProductRepository>(_service => ActivatorUtilities.CreateInstance<OrderProductRepository>(_service, instance));
// Add services to the container.
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(options => 
            options.AllowAnyMethod()
                   .AllowAnyHeader()
                   .SetIsOriginAllowed(origin => true) // allow any origin
                   .AllowCredentials()); //allow credentials

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
