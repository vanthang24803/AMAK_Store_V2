using AMAK.API.Configurations;
using AMAK.API.Extensions;
using AMAK.API.Common.Extensions;
using AMAK.Application.Services.Mail;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// TODO: Swagger
builder.Services.AddSwaggerGen()
                .AddEndpointsApiExplorer();

// TODO: Controller
builder.Services.AddControllers();

// TODO: Database Connection
builder.Services.AddCustomizedDatabase(builder.Configuration, builder.Environment);
builder.Services.AddHealthChecks();

// TODO: Auto Mapper
builder.Services.AddAutoMapperConfig();

// TODO: Identity
builder.Services.AddIdentity();

// TODO: JWT
builder.Services.AddJwt(builder.Configuration);

// TODO: Authorization
builder.Services.AddAuthorization();

// TODO: Inject Dependency
builder.Services.AddApplication();

// TODO: API Version
builder.Services.AddAPIVersion();

// TODO: Mail Settings
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.AddMiddleware();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();


