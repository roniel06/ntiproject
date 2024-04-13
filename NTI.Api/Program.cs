using NTI.Api.Extensions;
var builder = WebApplication.CreateBuilder(args);
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureAuthentication(builder);
builder.Services.ConfigureDbContext(builder);
builder.Services.ConfigureRepositories();
builder.Services.ConfigureAutoMapper();
builder.Services.ConfigureServices();
builder.Services.ConfigureFluentValidations();
builder.Services.ConfigureSwaggerDoc();
builder.Services.ConfigureCors();
builder.Services.ConfigureOptions(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

