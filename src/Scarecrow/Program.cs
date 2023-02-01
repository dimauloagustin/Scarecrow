using GitClient.Clients.BitBucket;
using GitClient.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add git client service
builder.Services.AddSingleton<IGitClient, BitBucketClientAdapter>((_services) => new BitBucketClientAdapter("agustin_di_maulo", "ATBBuwUrNKbSgnZMmRWytxDZXfECA4454D44"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }