
using BibliotecaApi.Datos;
using BibliotecaApi.DTOs;
using BibliotecaApi.Entitys;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BibliotecaApi.Controllers
{
	[ApiController]
	[Route("api/libros")]
	public class LibroController : Controller
	{

		private readonly ApplicationDbContext context;
		private readonly IMapper mapper;
		public LibroController(ApplicationDbContext context, IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
		}


		[HttpGet]
		public async Task<ActionResult<IEnumerable<LibroResponse>>> Get()
		{
			var libros = await context.Libros.ToListAsync();
			return Ok(mapper.Map<List<LibroResponse>>(libros));
		}


		[HttpGet("{id:int}")]
		public async Task<ActionResult<LibroDetailResponse>> GetLibroById(int id)
		{
			var libro = await context.Libros
				.Include(l => l.Autores)
				.ThenInclude(al => al.Autor)
				.FirstOrDefaultAsync(l => l.Id == id);

			if (libro is null)
			{
				return NotFound();
			}

			return libro.Adapt<LibroDetailResponse>();
		}



		[HttpPost]
		public async Task<ActionResult<LibroDetailResponse>> Post(LibroCreate libroCreateDto)
		{
			// Validar que existan los autores en la BD
			var autoresExistentes = await context.Autores
				.Where(a => libroCreateDto.AutoresIds.Contains(a.Id))
				.ToListAsync();

			if (autoresExistentes.Count != libroCreateDto.AutoresIds.Count)
			{
				return BadRequest("Uno o más autores no existen en la base de datos.");
			}

			// Crear la entidad libro
			var libro = new Libro
			{
				Titulo = libroCreateDto.Titulo,
				Description = libroCreateDto.Description,
				Autores = libroCreateDto.AutoresIds
					.Select(id => new AutorLibro { AutorId = id })
					.ToList()
			};

			context.Libros.Add(libro);
			await context.SaveChangesAsync();

			// Mapear la respuesta
			var response = libro.Adapt<LibroDetailResponse>();

			return CreatedAtAction(nameof(GetLibroById), new { id = libro.Id }, response);
		}


		[HttpPut("{id:int}")]
		public async Task<ActionResult> Put(int id, LibroUpdate libroUpdate)
		{
			var autoresExistentes = await context.Autores
				.Where(a => libroUpdate.AutoresIds.Contains(a.Id))
				.ToListAsync();

			if (autoresExistentes.Count != libroUpdate.AutoresIds.Count)
			{
				return BadRequest("Uno o más autores no existen en la base de datos.");
			}

			var libroDB = await context.Libros
				.Include(l => l.Autores)
				.FirstOrDefaultAsync(l => l.Id == id);

			if (libroDB is null)
				return NotFound();

			// Actualizar campos simples
			libroDB.Titulo = libroUpdate.Titulo;
			libroDB.Description = libroUpdate.Description;

			// Actualizar autores
			libroDB.Autores.Clear();

			foreach (var autorId in libroUpdate.AutoresIds)
			{
				libroDB.Autores.Add(new AutorLibro
				{
					AutorId = autorId
					// EF setea LibroId automáticamente
				});
			}

			await context.SaveChangesAsync();
			return NoContent();
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
