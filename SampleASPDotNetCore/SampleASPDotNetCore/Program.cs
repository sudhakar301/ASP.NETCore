using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SampleASPDotNetCore.Security;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(options =>
  {
      options.TokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          ValidIssuer = builder.Configuration["Jwt:Issuer"],
          ValidAudience = builder.Configuration["Jwt:Issuer"],                                      // by passing same key here it allows to login once you create JWT
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
      };
      options.Events = new JwtBearerEvents                                                         // This will be helpful to pass customized header X-XSRF-TOKEN instead
      {                                                                                            // default 'Authorization' as key and value prefix 'Bearer' in Postnman/Swagger
          OnMessageReceived = context =>
          {
              if (context.Request.Headers.ContainsKey("X-XSRF-TOKEN"))
                  context.Token = context.Request.Headers["X-XSRF-TOKEN"];
              return Task.CompletedTask;
          }
      };
  });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Version = "1.0", Description = "IM API" });
    c.OperationFilter<OperationFilter>();                                                                //add xsrftoken as a header in the swagger
});

builder.Services.AddHttpContextAccessor();                                                              // Dependency Injection for IHttpContextAccessor
builder.Services.AddScoped<IAuthorizationHandler, AuthorizationUserRequirementHandler>();               //Dependency Injection for AuthorizationUserRequirementHandler
builder.Services.AddAuthorization(options => options.DefaultPolicy = new AuthorizationPolicyBuilder()   //Deafult policy added & addrequirements for AuthorizationUserRequirementHandler
                .AddRequirements(new AuthorizationUserRequirement())
                .Build()
            );
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample API");            
            c.RoutePrefix = "api/swagger";
        });
app.MapControllers();                                                                                  // Required to map the controllers else http methods will not be found

app.Run();
