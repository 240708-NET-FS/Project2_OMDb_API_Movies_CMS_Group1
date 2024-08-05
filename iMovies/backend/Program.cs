using OMDbProject.Services;
using OMDbProject.Services.Interfaces;
using OMDbProject.Models;
using OMDbProject.Repositories.Interfaces;
using OMDbProject.Repositories;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS 
builder.Services.AddCors(co => {
    co.AddPolicy("CORS" , pb =>{
        pb.WithOrigins("*")
        .AllowAnyHeader();
    });
});
//CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS

//JSON serealizer to ignore cycles
builder.Services.AddControllers()
.AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//Register dependencies
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IFollowerService, FollowerService>();
builder.Services.AddScoped<ILikeService, LikeService>();
builder.Services.AddScoped<IRankingService, RankingService>();
builder.Services.AddScoped<IUserMovieService, UserMovieService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();


builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Logging.AddConsole();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS
app.UseCors("CORS"); //<-USE CORS with your policy name
//CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS CORS

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
