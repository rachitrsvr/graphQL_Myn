using GraphQL.Demo.Api.Data;
using GraphQL.Demo.Api.DataLoader;
using GraphQL.Demo.Api.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();
builder.Host.UseSerilog();
//services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings"));
var sqlConnectionString = builder.Configuration["PostgreSqlConnectionString"];

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(sqlConnectionString));

builder.Services.AddGraphQLServer()
    .AddQueryFieldToMutationPayloads()
    .AddGlobalObjectIdentification()
    .AddQueryType()
        .AddTypeExtension<UserQueries>()
    .AddMutationType()
         .AddTypeExtension<UserMutations>()
    .AddFiltering()
    .AddSorting()
    .AddDataLoader<UserByIdDataLoader>();



var app = builder.Build();
app.UseRouting();

// Use the CORS policy
app.UseCors("AllowOrigin");
app.MapGraphQL();

app.MapGet("/", () => "Welcome to GraphQL Demo Api");

app.Run();