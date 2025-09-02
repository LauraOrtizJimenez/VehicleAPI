using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger (UI) + soporte de endpoints
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS abierto en desarrollo (útil para Swagger/cliente web)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // Documentación interactiva
    app.UseSwagger();      // -> /swagger/v1/swagger.json
    app.UseSwaggerUI();    // -> /swagger

    // Evitar problemas de redirección cuando usas solo HTTP en local
    // (si quieres forzar HTTPS en dev, mueve UseHttpsRedirection() aquí)
    app.UseCors("AllowAll");
}
else
{
    // En producción sí redirigimos a HTTPS
    app.UseHttpsRedirection();
}

// (si usas auth más adelante, déjalo)
app.UseAuthorization();

app.MapControllers();

app.Run();