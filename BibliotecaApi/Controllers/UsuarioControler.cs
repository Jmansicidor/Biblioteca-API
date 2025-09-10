using BibliotecaApi.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BibliotecaApi.Controllers
{
	[ApiController]
	[Route("api/usuarios")]
	[Authorize]
	public class UsuarioControler : ControllerBase
	{
		private readonly UserManager<IdentityUser> userManager;
		private readonly IConfiguration configuration;
		private readonly SignInManager<IdentityUser> signInManager;

		public UsuarioControler(UserManager<IdentityUser> userManager, IConfiguration configuration, SignInManager<IdentityUser> signInManager)
		{
			this.userManager = userManager;
			this.configuration = configuration;
			this.signInManager = signInManager;
		}


		[HttpPost("registro")]
		[AllowAnonymous]
		public async Task<ActionResult<RepuestaAutenticacionDTO>> Registro(CredencialesUsuarioDTO credencialesUsuarioDTO)
		{
			var usuario = new IdentityUser { UserName = credencialesUsuarioDTO.Email, Email = credencialesUsuarioDTO.Email };
			var resultado = await userManager.CreateAsync(usuario, credencialesUsuarioDTO.Password);
			if (resultado.Succeeded)
			{
				var respuestaAutenticacion = await ConstruirToken(credencialesUsuarioDTO);
				return respuestaAutenticacion;
			}
			else
			{
				return BadRequest(resultado.Errors);
			}
		}

		[HttpPost("login")]
		[AllowAnonymous]
		public async Task<ActionResult<RepuestaAutenticacionDTO>> Login(CredencialesUsuarioDTO credencialesUsuarioDTO)
		{
			var usuario = await userManager.FindByEmailAsync(credencialesUsuarioDTO.Email);
			if (usuario != null)
			{
				var resultado = await signInManager.CheckPasswordSignInAsync(usuario, credencialesUsuarioDTO.Password!,lockoutOnFailure: false);
				if (resultado.Succeeded)
				{
					return await ConstruirToken(credencialesUsuarioDTO);
				}
			}
			return Unauthorized();
		}

		private async Task<RepuestaAutenticacionDTO> ConstruirToken(CredencialesUsuarioDTO credencialesUsuarioDTO)
		{
			var claims = new List<System.Security.Claims.Claim>
			{
				new System.Security.Claims.Claim("email", credencialesUsuarioDTO.Email),
				new System.Security.Claims.Claim("lo que quieras","Cualquier valor")
			};

			var usuario =  await userManager.FindByEmailAsync(credencialesUsuarioDTO.Email);
			var claimsDB = await userManager.GetClaimsAsync(usuario!);
			claims.AddRange(claimsDB);

			var llave = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["Jwtkey"]!));
			var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);
			var expiracion = DateTime.UtcNow.AddYears(1);

			var securityToken = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
				claims: claims,
				expires: expiracion,
				signingCredentials: creds);

			var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler().WriteToken(securityToken);

			return new RepuestaAutenticacionDTO
			{
				Token = tokenHandler,
				Expiracion = expiracion
			};
		}

	}
}
