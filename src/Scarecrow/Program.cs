using GitClient.Clients.BitBucket;
using GitClient.Interfaces;
using Scarecrow.Core.Pipe;
using Scarecrow.Core.Pipe.Factories;
using System.IO.Abstractions;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())); ;
builder.Services.AddSingleton<IGitClient, BitBucketClientAdapter>((_services) => new BitBucketClientAdapter("agustin_di_maulo", "ATBBuwUrNKbSgnZMmRWytxDZXfECA4454D44", new FileSystem()));

builder.Services.AddSingleton<IFileSystem, FileSystem>();
builder.Services.AddSingleton<IRulesMapper, RulesMapper>();
builder.Services.AddSingleton<PipeFactory>();
builder.Services.AddSingleton<Pipe>(s => {
    var fs = s.GetRequiredService<IFileSystem>() ?? throw new NullReferenceException();
    var pf = s.GetRequiredService<PipeFactory>() ?? throw new NullReferenceException();
    return pf.CreateFromJson(fs.File.ReadAllText("./ConfigFiles/config.json"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors((p) => { p.AllowAnyHeader(); p.AllowAnyMethod(); p.AllowAnyOrigin(); });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }