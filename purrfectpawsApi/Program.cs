using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PurrfectpawsApi.DatabaseDbContext;
using System.Text;

var MyAlloSpecificationOrigins = "_myAllowSpecificationOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAlloSpecificationOrigins,
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<PurrfectpawsContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnString"));
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAlloSpecificationOrigins);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
