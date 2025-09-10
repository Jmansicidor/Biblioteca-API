using Azure;
using BibliotecaApi.Datos;
using BibliotecaApi.DTOs;
using BibliotecaApi.Entitys;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Controllers
{
	[ApiController]
	[Route("api/autores")]
	[Authorize]
	public class AutorController : ControllerBase
	{
		private readonly ApplicationDbContext context;
		private readonly IMapper mapper;

		public AutorController(ApplicationDbContext context, IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;

		}


		[HttpGet]
		public async Task<ActionResult<IEnumerable<AutorResponse>>> Get()
		{
			var autores = await context.Autores.Include(a => a.Libros).ToListAsync();
			return Ok(mapper.Map<List<AutorResponse>>(autores));
		}


		[HttpGet("{id:int}",Name = "ObtenerAutor")]
		public async Task<ActionResult<AutorResponse>> GetById(int id)
		{
			var autor = await context.Autores.Include(a => a.Libros).FirstOrDefaultAsync(a => a.Id == id);
			if (autor is null) return NotFound();

			return Ok(mapper.Map<AutorResponse>(autor));

		}

		[HttpPost]
		public async Task<ActionResult<AutorCreate>> Post(AutorCreate request)
		{
			var autor = mapper.Map<Autor>(request);

			context.Autores.Add(autor);
			await context.SaveChangesAsync();
			var response = mapper.Map<AutorCreate>(autor);

			return CreatedAtRoute("ObtenerAutor", new { id = autor.Id }, response);
		}

		[HttpPut("{id:int}")]
		public async Task<ActionResult> Put(int id, AutorUpdate request)
		{
			var autor = await context.Autores.FirstOrDefaultAsync(a => a.Id == id);
			if (autor is null) return NotFound();

			mapper.Map(request, autor); // Mapster actualiza propiedades

			await context.SaveChangesAsync();
			return NoContent();
		}

		[HttpPatch("{id:int}")]

		public async Task<ActionResult> Patch(int id, JsonPatchDocument<AutorPatchDTO> request)
		{
			var autor = await context.Autores.FirstOrDefaultAsync(a => a.Id == id);
			if (autor is null) return NotFound();

			var autorPatch = mapper.Map<AutorPatchDTO>(autor);
			
			request.ApplyTo(autorPatch, ModelState);

			var isValid = TryValidateModel(autorPatch);

			if (!isValid) return ValidationProblem(ModelState);

			mapper.Map(autorPatch, autor);

			// Mapster actualiza propiedades
			await context.SaveChangesAsync();


			return NoContent();
		}



		[HttpDelete("{id:int}")]
		public async Task<ActionResult> Delete(int id)
		{
			var autor = await context.Autores.FirstOrDefaultAsync(a => a.Id == id);
			if (autor is null) return NotFound();

			context.Autores.Remove(autor);
			await context.SaveChangesAsync();
			return NoContent();
		}

	}
}
