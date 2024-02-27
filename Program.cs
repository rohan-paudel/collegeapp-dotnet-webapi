using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.RateLimiting;
using CollegeAppDotnetWebApi;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// builder
//     .Services
//     .Configure<KestrelServerOptions>(options =>
//     {
//         options.ConfigureHttpsDefaults(
//             options => options.ClientCertificateMode = ClientCertificateMode.RequireCertificate
//         );
//         options.ListenAnyIP(
//             5272,
//             lisOptions =>
//             {
//                 lisOptions.UseHttps("/data/cert.pfx", "hello");
//             }
//         );
//     });

// builder
//     .Services
//     .Configure<KestrelServerOptions>(options =>
//     {
//         options.ListenAnyIP(5272);
//     });

builder
    .Services
    .AddDbContextPool<AppDataContext>(
        options =>
            options.UseMySql(
                SqlSetupConstants.DevConnectionString,
                ServerVersion.AutoDetect(SqlSetupConstants.DevConnectionString)
            )
    );

builder
    .Services
    .AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme)
    .AddCertificate(options => { });

builder
    .Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.LoginPath = "/Account/Login";
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
    });

builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);

builder
    .Services
    .AddAuthorizationBuilder()
    .AddPolicy(
        "PolicyForMobileDevice",
        p =>
        {
            p.RequireAuthenticatedUser();
            p.AddAuthenticationSchemes(IdentityConstants.BearerScheme);
        }
    );

var cookiePolicyOptions = new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Strict, };

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ICategoryCountDL, CategoryCountDL>();
builder.Services.AddScoped<IUserNoteManagementDL, UserNoteManagementDL>();
builder.Services.AddScoped<IVideoCipherAdminDL, VideoCipherAdminDL>();
builder.Services.AddScoped<INoticeBoardManagementDL, NoticeBoardManagementDL>();
builder.Services.AddScoped<IVdoCipherDL, VdoCipherDL>();
builder.Services.AddScoped<IQuoteManagementDL, QuoteManagementDL>();
builder.Services.AddScoped<IChapterTestDL, ChapterTestDL>();
builder.Services.AddScoped<ITestManagementDL, TestManagementDL>();
builder.Services.AddScoped<IDiscussionManagementDL, DiscussionManagementDL>();
builder.Services.AddScoped<INoteManagementDL, NoteManagementDL>();
builder.Services.AddScoped<ICollegeManagementDL, CollegeManagementDL>();
builder.Services.AddScoped<ITopicManagementDL, TopicManagementDL>();
builder.Services.AddScoped<ISubjectManagementDL, SubjectManagementDL>();
builder.Services.AddScoped<ICategoryManagementDL, CategoryManagementDL>();
builder.Services.AddScoped<IUserManagementDL, UserManagementDL>();
builder.Services.AddScoped<IUserLoginDL, UserLoginDL>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder
    .Services
    .AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "College App - Tejilo", Version = "v1" });

        c.AddSecurityDefinition(
            "Bearer",
            new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            }
        );
        c.AddSecurityRequirement(
            new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            }
        );
    });

builder
    .Services
    .AddCors(options =>
    {
        options.AddPolicy(
            "AllowAll",
            builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }
        );
    });

builder
    .Services
    .AddIdentity<TejiloUser, IdentityRole>(options =>
    {
        options.User.RequireUniqueEmail = true;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
        options.Lockout.MaxFailedAccessAttempts = 5;
    })
    .AddEntityFrameworkStores<AppDataContext>();

builder
    .Services
    .AddRateLimiter(options =>
    {
        options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
        options.AddPolicy(
            "fixed",
            httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.Connection.RemoteIpAddress?.ToString(),
                    factory: _ =>
                        new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 5,
                            QueueLimit = 3,
                            Window = TimeSpan.FromSeconds(1)
                        }
                )
        );
    });

builder
    .Services
    .AddHttpClient(
        "VdoCipherClient",
        client =>
        {
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            // client.DefaultRequestHeaders.Add("Content-Type", "application/json");
            client
                .DefaultRequestHeaders
                .Add(
                    "Authorization",
                    "Apisecret JpTbYAfcaXeKDiJt6h6MSppcJVgVGnkuYKzrQQDdbxVSBDJIe08qgtlXrthbjV5a"
                );
        }
    );

/// START OF JWT TOKEN SERVICE

// builder
//     .Services
//     .AddAuthentication(options =>
//     {
//         options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//         options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//         options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//     })
//     .AddJwtBearer(o =>
//     {
//         o.TokenValidationParameters = new TokenValidationParameters()
//         {
//             ValidIssuer = "https://tejilo.com.np",
//             ValidAudience = "https://tejilo.com.np",
//             IssuerSigningKey = new SymmetricSecurityKey(
//                 Encoding
//                     .UTF8
//                     .GetBytes(
//                         "this-is-server-jwt-key-for-encryption-12344-#$@%%#@-the_hello_98889))&^^&"
//                     )
//             ),
//             ValidateAudience = true,
//             ValidateIssuer = true,
//             ValidateIssuerSigningKey = true,
//             ValidateLifetime = true
//         };

//         o.Events = new JwtBearerEvents
//         {
//             OnTokenValidated = context =>
//             {
//                 var claimsIdentity = context.Principal?.Identity as ClaimsIdentity;
//                 if (
//                     claimsIdentity != null
//                     && claimsIdentity.HasClaim(c => c.Type == ClaimTypes.Role)
//                 )
//                 {
//                     var roleClaims = claimsIdentity.FindAll(c => c.Type == ClaimTypes.Role);
//                     foreach (var roleClaim in roleClaims)
//                     {
//                         claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, roleClaim.Value));
//                     }
//                 }

//                 return Task.CompletedTask;
//             }
//         };
//     });

// END OF JWT


// builder
//     .Services
//     .AddAuthorization(options =>
//     {
//         options.FallbackPolicy = new AuthorizationPolicyBuilder()
//             .RequireAuthenticatedUser()
//             .Build();
//     });

var app = builder.Build();

FirebaseApp.Create(
    new AppOptions()
    {
        Credential = GoogleCredential.FromFile(
            Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "testing-ac356-firebase-adminsdk-vepp3-5b81cb5d82.json"
            )
        ),
    }
);

// string uploadsFolder = "/data";
// if (!Directory.Exists(uploadsFolder))
// {
//     // Directory.CreateDirectory(uploadsFolder);
// }

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) { }
app.UseSwagger();

app.UseCookiePolicy(cookiePolicyOptions);
app.UseSwaggerUI();
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// app.UseStaticFiles(
//     new StaticFileOptions
//     {
//         FileProvider = new PhysicalFileProvider(uploadsFolder),
//         RequestPath = "/data"
//     }
// );

app.UseRateLimiter();
app.MapControllers().RequireRateLimiting("fixed");

// app.MapGet(
//         "/files/{fileName}",
//         IResult (string fileName) =>
//         {
//             var filePath = $"{uploadsFolder}/{fileName}";

//             if (File.Exists(filePath))
//             {
//                 return TypedResults.PhysicalFile(filePath, fileDownloadName: $"{fileName}");
//             }

//             return TypedResults.NotFound("No file found with the supplied file name");
//         }
//     )
//     .WithName("GetFileByName");

app.Run();
