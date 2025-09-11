
using BibliotecaApi.Datos;
using BibliotecaApi.Entitys;
using BibliotecaApi.Servicios;

using BibliotecaAPI.Utilidades;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Area servicios

builder.Services.AddCors(opciones =>
{
	opciones.AddDefaultPolicy(opcionesCORS =>
	{
		opcionesCORS.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
	});
});

builder.Services.AddIdentityCore<Usuario>()
	.AddRoles<IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.AddScoped<UserManager<Usuario>>();
builder.Services.AddScoped<SignInManager<Usuario>>();

//Inyeccion de dependencias
builder.Services.AddTransient<IServiciosUsuarios, ServiciosUsuarios>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication().AddJwtBearer(opciones => { 
	opciones.MapInboundClaims = false;
	opciones.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
	{
		
	    ValidateIssuer = false,
		ValidateAudience = false,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
			System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwtkey"]!)),
		ClockSkew = TimeSpan.Zero
	};
});

builder.Services.AddAuthorization(opciones =>
{
	opciones.AddPolicy("esadmin", politica => politica.RequireClaim("esadmin"));
});


builder.Services.AddControllers();


var config = TypeAdapterConfig.GlobalSettings;
MappingConfig.RegisterMappings(config);
builder.Services.AddSingleton(config);
builder.Services.AddScoped<IMapper, ServiceMapper>();

//Patch

builder.Services.AddControllers().AddNewtonsoftJson();


builder.Services.AddDbContext<ApplicationDbContext>(opciones => 
opciones.UseSqlServer("name=DefaultConnection"));

var app = builder.Build();


//Area middleware
app.UseCors();

app.MapControllers();


app.Run();
