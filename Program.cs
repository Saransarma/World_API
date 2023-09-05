using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using World_Api.Common;
using World_Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Configure CORS

builder.Services.AddCors(Options =>
{
    Options.AddPolicy("CustomPolicy",x=>x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});


#endregion


#region Configure Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(Options=>Options.UseSqlServer(connectionString));
#endregion

#region Configure AutoMapping
builder.Services.AddAutoMapper(typeof(MappingProfile));
#endregion



builder.Services.AddControllers();
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


app.UseCors("CustomPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
