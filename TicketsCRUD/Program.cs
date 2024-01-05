using Microsoft.EntityFrameworkCore;
using TicketsCRUD.Core.AutpMapperConfig;
using TicketsCRUD.Core.Context;

var builder = WebApplication.CreateBuilder(args);

// Config DataBase
builder.Services.AddDbContext<ApplicationDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("local"));
});

// Config AutoMapper
// Configuration d'AutoMapper dans le contexte de l'injection de dépendances (DI)
builder.Services.AddAutoMapper(typeof(AutoMapperConfigProfil));

// Add services to the container.


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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
