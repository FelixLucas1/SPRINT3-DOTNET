using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Sprint03.Context;
using Sprint03.Repository;
using Sprint03.Service;

var builder = WebApplication.CreateBuilder(args);

// carrega connectionstring do appsettings.json
var conn = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(conn) // Isso usa o Oracle.EntityFrameworkCore
);

// 👇 Adicione isto para registrar controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Minha API (Oracle)", Version = "v1" });
});

// Registrar services e repositories
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<ProdutoService>();
builder.Services.AddScoped<PedidoService>();
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<ProdutoRepository>();
builder.Services.AddScoped<PedidoRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

// 👇 Necessário para mapear controllers
app.MapControllers();

app.Run();
