using AutoMapper;
using LibraryApp.Common.Attributes;
using LibraryApp.Common.Variables;
using LibraryApp.Data.Context;
using LibraryApp.Data.Entities;
using LibraryApp.DataAccess.Repositories.Abstract;
using LibraryApp.DataAccess.Repositories.Concrete;
using LibraryApp.DataAccess.UnitOfWork.Abstract;
using LibraryApp.DataAccess.UnitOfWork.Concrete;
using LibraryApp.ExceptionHandling.Middlewares;
using LibraryApp.Mapper.AutoMapper;
using LibraryApp.WebApi.Data;
using LibraryApp.WebApi.Services.Abstract;
using LibraryApp.WebApi.Services.Concrete;
using LibraryApp.WebToken.Jwt.Abstract;
using LibraryApp.WebToken.Jwt.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<LibraryDbContext>(conf =>
{
    conf.UseSqlServer(ConnectionConstants.DbConnectionString);
    conf.UseLazyLoadingProxies(true);
});
builder.Services.AddIdentity<User, Role>().AddEntityFrameworkStores<LibraryDbContext>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAddressRepository, AdressRepository>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();
builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddScoped<IJwtHelper, JwtHelper>();
builder.Services.AddScoped<IFavouriteAuthorRepository, FavouriteAuthorRepository>();
builder.Services.AddScoped<IBookAuthorRepository, BookAuthorRepository>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 15;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
});
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: "_myAllowOrigins",
        builder =>
        {
            builder
                .WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
           .AddJwtBearer(x =>
           {
               x.RequireHttpsMetadata = false;
               x.SaveToken = true;
               x.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("a90244f19c883e32acb2c721099c9a7f2763273f1660c39e826cbc6943d4c26e9fb947655e7f529dadbf0eaa2159173e3f742edf07cd8654b96c40a5c07827f8")),
                   ValidateIssuer = false,
                   ValidateAudience = false,
                   ClockSkew = TimeSpan.Zero
               };
           });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // configure SwaggerDoc and others

    // add JWT Authentication
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
    c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {securityScheme, new string[] { }}
    });

    // add Basic Authentication
    var basicSecurityScheme = new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        Reference = new OpenApiReference { Id = "BasicAuth", Type = ReferenceType.SecurityScheme }
    };
    c.AddSecurityDefinition(basicSecurityScheme.Reference.Id, basicSecurityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {basicSecurityScheme, new string[] { }}
    });
});
builder.Services.AddAutoMapper(cfg =>
{
    cfg.ValidateInlineMaps = false;
}, typeof(MapperProfiles));
builder.Services.AddScoped<LastActiveActionFilter>();


WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    using (var scope = app.Services.CreateScope())
    {
        UserManager<User> userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        RoleManager<Role> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
        SeedDatabase.Seed(userManager, roleManager).Wait();
        LibraryDbContext context = scope.ServiceProvider.GetService<LibraryDbContext>();
        if (context.Categories.Count() == 0)
        {
            context.Categories.AddRange(
                new List<Category>() {
                         new Category() { Name="Edebiyat", CreatedDate = DateTime.Now , CreatedRoleId = 1 },
                         new Category() { Name="Çocuk ve Gençlik", CreatedDate = DateTime.Now, CreatedRoleId = 1 },
                         new Category() { Name="Eðitim Baþvuru", CreatedDate = DateTime.Now, CreatedRoleId = 1  },
                         new Category() { Name="Araþtýrma - Tarih", CreatedDate = DateTime.Now, CreatedRoleId = 1  },
                         new Category() { Name="Yabancý Dil", CreatedDate = DateTime.Now, CreatedRoleId = 1  },
                         new Category() { Name="Ders / Sýnav Kitaplarý", CreatedDate = DateTime.Now, CreatedRoleId = 1  },
                         new Category() { Name="Din Tasavvuf", CreatedDate = DateTime.Now, CreatedRoleId = 1  },
                         new Category() { Name="Sanat - Tasarým", CreatedDate = DateTime.Now, CreatedRoleId = 1  },
                         new Category() { Name="Felsefe", CreatedDate = DateTime.Now, CreatedRoleId = 1  },
                         new Category() { Name="Hobi", CreatedDate = DateTime.Now, CreatedRoleId = 1  },
                         new Category() { Name="Bilim", CreatedDate = DateTime.Now, CreatedRoleId = 1  },
                         new Category() { Name="Çizgi Roman", CreatedDate = DateTime.Now , CreatedRoleId = 1 },
                         new Category() { Name="Mizah", CreatedDate = DateTime.Now, CreatedRoleId = 1  },
                         new Category() { Name="Prestij Kitaplarý", CreatedDate = DateTime.Now, CreatedRoleId = 1  },
                         new Category() { Name="Mitoloji Efsane", CreatedDate = DateTime.Now, CreatedRoleId = 1  },
                });
        }

        context.SaveChanges();
    }
}

app.UseCors("_myAllowOrigins");

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
