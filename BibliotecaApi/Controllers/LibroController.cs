using Azure.Core;
using BibliotecaApi.Datos;
using BibliotecaApi.Datos;
using BibliotecaApi.DTOs;
using BibliotecaApi.Entitys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BibliotecaApi.Controllers
{
	[ApiController]
	[Route("api/libros")]
	public class LibroController : Controller
	{

		private readonly ApplicationDbContext context;
		public LibroController(ApplicationDbContext context)
		{
			this.context = context;

		}


		[HttpGet]
		public async Task<ActionResult<IEnumerable<LibroResponse>>> Get()
		{
			var response = await context.Libros.AsNoTracking().ToListAsync();

			return Ok(response.Select(l => l.ToDTO()));
		}

		[HttpGet("{id:int}", Name = "ObtenerLibro")]
		public async Task<ActionResult<LibroDetailResponse>> Get(int id)
		{
			var libros = await context.Libros.Include(l => l.Autor).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

			if (libros is null)
				return NotFound();

			return Ok(value: libros.ToDetailDto());
		}


		[HttpPost]
		public async Task<ActionResult<LibroDetailResponse>> Post(LibroCreate libro)
		{
			var existeAutor = await context.Autores.AnyAsync(x => x.Id == libro.AutorId);

			if (!existeAutor) return BadRequest($"El auto de id{libro.AutorId} no existe");

			var request = libro.ToEntity();
			context.Add(request);
			await context.SaveChangesAsync();
			// Recuperamos con autor incluido para la respuesta detallada
			var libroCreado = await context.Libros
												.Include(l => l.Autor)
												.FirstAsync(l => l.Id == request.Id);

			return CreatedAtRoute("ObtenerLibro", new { id = libroCreado.Id }, libroCreado.ToDetailDto());
		}

		[HttpPut("{id:int}")]
		public async Task<ActionResult> Put(int id, LibroUpdate request)
		{
			var libro = await context.Libros.FirstOrDefaultAsync(l => l.Id == id);
			if (libro is null)
				return NotFound();

			var existeAutor = await context.Autores.AnyAsync(x => x.Id == request.AutorId);
			if (!existeAutor)
				return BadRequest($"El autor con id {request.AutorId} no existe");

			// Actualizamos los datos
			libro.UpdateEntity(request);

			await context.SaveChangesAsync();

			return Ok(); // 204
		}

		[HttpDelete("{id:int}")]
		public async Task<ActionResult> Delete(int id) {

			var libro = await context.Libros.FirstOrDefaultAsync(x => x.Id == id);
			if (libro is null) 
				return NotFound($"Libro no con id {id} no encontrado");

			context.Libros.Remove(libro);
			await context.SaveChangesAsync();

			return NoContent();
		}


	}
}
