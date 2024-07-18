using AMAK.API.Configurations;
using AMAK.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// TODO: Swagger
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

// TODO: Controller
builder.Services.AddControllers();

// TODO: Database Connection
builder.Services.AddCustomizedDatabase(builder.Configuration, builder.Environment);
builder.Services.AddHealthChecks();

// TODO: Auto Mapper
builder.Services.AddAutoMapperConfig();

// TODO: Identity
builder.Services.CustomIdentity();

builder.Services.AddDIScope();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MiddlewareCustom();
app.MapControllers();
app.Run();


