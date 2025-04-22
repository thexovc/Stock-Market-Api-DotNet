using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using stockMarket.interfaces;
using stockMarket.Interfaces;
using stockMarket.Repository;
using stockMarket.Service;
using stockModel.Data;
using stockModel.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    (options) =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
        options.AddSecurityDefinition(
            "Bearer",
            new OpenApiSecurityScheme
            {
                Description =
                    "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
            }
        );

        options.AddSecurityRequirement(
            new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer",
                        },
                    },
                    Array.Empty<string>()
                },
            }
        );
    }
);

builder
    .Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft
            .Json
            .ReferenceLoopHandling
            .Ignore
    );

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder
    .Services.AddIdentity<AppUser, IdentityRole>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 12;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
    })
    .AddEntityFrameworkStores<ApplicationDBContext>()
    .AddDefaultTokenProviders();

builder
    .Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme =
            options.DefaultScheme =
            options.DefaultSignInScheme =
            options.DefaultSignOutScheme =
            options.DefaultForbidScheme =
            options.DefaultChallengeScheme =
                JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])
            ),
        };

        // Add this event handler to process the token without "Bearer" prefix
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Headers["Authorization"].ToString();
                // Console.WriteLine($"\n---- Token Processing ----");
                // Console.WriteLine($"Raw Authorization header: {accessToken}");

                // If token doesn't start with "Bearer ", assume it's just the token
                if (!string.IsNullOrEmpty(accessToken) && !accessToken.StartsWith("Bearer "))
                {
                    // Console.WriteLine($"Setting token without Bearer prefix: {accessToken}");
                    context.Token = accessToken;
                }
                else
                {
                    Console.WriteLine($"Token already has Bearer prefix or is empty");
                }
                // Console.WriteLine($"Final token value: {context.Token}");
                // Console.WriteLine($"------------------------\n");

                return Task.CompletedTask;
            },
        };
    });

builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

// app.Use(
//     async (context, next) =>
//     {
//         Console.WriteLine("\n==== Incoming Request Details ====");
//         Console.WriteLine($"Path: {context.Request.Path}");
//         Console.WriteLine($"Method: {context.Request.Method}");
//         Console.WriteLine("Headers:");
//         foreach (var header in context.Request.Headers)
//         {
//             Console.WriteLine($"  {header.Key}: {header.Value}");
//         }
//         // Console.WriteLine("================================\n");

//         await next(); // Continue to the next middleware
//     }
// );

// // With this:
// app.UseCors(x =>
//     x.WithOrigins("http://localhost:3000", "https://yourdomain.com") // Specify actual origins
//         .AllowAnyMethod()
//         .AllowAnyHeader()
//         .AllowCredentials()
// );

app.UseCors(x =>
    x.SetIsOriginAllowed(origin => true) // Be careful with this in production!
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
