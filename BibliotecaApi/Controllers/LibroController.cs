
using BibliotecaApi.Datos;
using BibliotecaApi.DTOs;
using BibliotecaApi.Entitys;
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

		[HttpGet("{id:int}", Name = "ObtenerLibro")]
		public async Task<ActionResult<LibroDetailResponse>> Get(int id)
		{
			var libro = await context.Libros.Include(l => l.Autor).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

			if (libro is null)
				return NotFound();

			return Ok(mapper.Map<LibroDetailResponse>(libro));
		}


		[HttpPost]
		public async Task<ActionResult<LibroDetailResponse>> Post(LibroCreate request)
		{
			var existeAutor = await context.Autores.AnyAsync(a => a.Id == request.AutorId);
			if (!existeAutor) return BadRequest($"No existe el autor con id {request.AutorId}");

			var libro = mapper.Map<Libro>(request);

			context.Libros.Add(libro);
			await context.SaveChangesAsync();

			// Recargar con autor incluido
			var libroCreado = await context.Libros.Include(l => l.Autor).FirstAsync(l => l.Id == libro.Id);

			return CreatedAtRoute("ObtenerLibro", new { id = libroCreado.Id }, mapper.Map<LibroDetailResponse>(libroCreado));
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

			mapper.Map(request, libro);

			// Actualizamos los datos
		

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
