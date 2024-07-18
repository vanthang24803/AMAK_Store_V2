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
builder.Services.AddCustomizedHealthCheck(builder.Configuration, builder.Environment);

// TODO: Auto Mapper
builder.Services.AddAutoMapperConfig();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();


