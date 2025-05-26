using Application;
using ECommerceProdutos.Extensions;
using Infra;
using Infra.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApplicationInjectDependencies();
builder.Services.AddInfraInjectDependencies();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddJwtAuthentication();
builder.Services.AddSwaggerWithAuth();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var databaseInitializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
    await databaseInitializer.InitializeAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
