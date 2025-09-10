
using BibliotecaApi.Datos;
using BibliotecaApi.Utilidades;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Area servicios



builder.Services.AddIdentityCore<IdentityUser>()
	.AddRoles<IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

//builder.Services.AddScoped<IUserClaimsPrincipalFactory<IdentityUser>, UserClaimsPrincipalFactory<IdentityUser, IdentityRole>>();
builder.Services.AddScoped<UserManager<IdentityUser>>();
builder.Services.AddScoped<SignInManager<IdentityUser>>();
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

app.MapControllers();


app.Run();
