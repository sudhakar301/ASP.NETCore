using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SampleASPDotNetCore;
using SampleASPDotNetCore._MBehaviors;
using SampleASPDotNetCore.Data;
using SampleASPDotNetCore.Security;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

# region MediatR Line 1 Or 2 [AddMediatR]we can use
builder.Services.AddMediatR(config=>config.RegisterServicesFromAssemblyContaining<Program>()); // Register MediatR services
builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(Program).Assembly)); // Register MediatR services for the current assembly
builder.Services.AddSingleton<MFakeDataStore>(); // Register the MFakeDataStore as a singleton service

# endregion

#region FluentValidation
builder.Services.AddProblemDetails();
// Register ProblemDetails for global error handling
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly); // Register all validators from the current assembly
builder.Services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>)); // Register the MValidationBehavior as a pipeline behavior for all requests
#endregion
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
builder.Services.AddScoped<IAuthorizationHandler, RoleAuthorizationRequirementHandler>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policyBuilder =>                                                        //Added Admin policy for Admin role
    {
        policyBuilder.AddRequirements(new RoleAuthorizationRequirement());
    });
    options.DefaultPolicy = new AuthorizationPolicyBuilder()   //Deafult policy added & addrequirements for AuthorizationUserRequirementHandler
               .AddRequirements(new AuthorizationUserRequirement())
               .Build();
           } );
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
                          policy =>
                          {
                              policy.AllowAnyHeader()
                                    .AllowAnyMethod()
                                    .WithOrigins("http://localhost:43200");
                          });
});

var app = builder.Build();
app.UseExceptionHandler();
app.UseCors("CorsPolicy");
app.UseSwagger();
app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample API");            
            c.RoutePrefix = "api/swagger";
        });
app.MapControllers();                                                                                  // Required to map the controllers else http methods will not be found

app.Run();
