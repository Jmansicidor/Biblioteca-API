using BibliotecaApi.Datos;
using BibliotecaApi.DTOs;
using BibliotecaApi.Entitys;
using MapsterMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static BibliotecaApi.DTOs.ComentarioDTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BibliotecaApi.Controllers
{
	[Route("api/libros/{libroId:int}/comentarios")]
	[ApiController]
	public class ComentarioController : ControllerBase
	{
		private readonly ApplicationDbContext context;
		private readonly IMapper mapper;

		public ComentarioController(ApplicationDbContext context, IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
		}



			// GET: api/<ComentarioController>
		[HttpGet]
		public async Task<ActionResult<List<ComentarioResponse>>> Get(int libroId)
		{
			var existeLibro = await context.Libros.AnyAsync(l => l.Id == libroId);
			if (!existeLibro) return NotFound();

			var cometarios = await context.Comentarios.Where(l => l.LibroId == libroId)
							.OrderByDescending(x => x.DatePost).ToListAsync();
			return Ok(mapper.Map<List<ComentarioResponse>>(cometarios));
		}

		// GET api/<ComentarioController>/5
		[HttpGet("{id}")]
		public async Task<ActionResult<ComentarioResponse>> GetById(Guid id)
		{
			var comentario = await context.Comentarios.FirstOrDefaultAsync(x => x.Id == id);

			if (comentario is null) return NotFound();

			return Ok(mapper.Map<ComentarioResponse>(comentario));
		}

		// POST api/<ComentarioController>
		[HttpPost]
		public async Task<ActionResult> Post(int libroId, ComentarioUpdate comentarioUpdate)
		{
			var existeLibro = await context.Libros.AnyAsync(l => l.Id == libroId);
			if (!existeLibro) return NotFound();
			var comentario = mapper.Map<Comentario>(comentarioUpdate);
			comentario.LibroId = libroId;
			comentario.DatePost = DateTime.UtcNow;
			context.Comentarios.Add(comentario);
			await context.SaveChangesAsync();
			var comentarioResponse = mapper.Map<ComentarioResponse>(comentario);
			return CreatedAtAction(nameof(GetById), new { id = comentario.Id,libroId }, comentarioResponse);
		}

		// PUT api/<ComentarioController>/5

		[HttpPatch("{id}")]

		public async Task<ActionResult> Patch(Guid id,int libroId, JsonPatchDocument<ComentarioPatchDTO> request)
		{
			var comentario = await context.Comentarios.FirstOrDefaultAsync(c => c.Id == id);

			var existeLibro = await context.Libros.AnyAsync(l => l.Id == libroId);
			if (!existeLibro) return NotFound();
		

			var comentarioPatch = mapper.Map<ComentarioPatchDTO>(comentario);

			request.ApplyTo(comentarioPatch, ModelState);

			var isValid = TryValidateModel(comentarioPatch);

			if (!isValid) return ValidationProblem(ModelState);

			mapper.Map(comentarioPatch, comentario);

			// Mapster actualiza propiedades
			await context.SaveChangesAsync();


			return NoContent();
		}

		// DELETE api/<ComentarioController>/5
		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(Guid id,int libroId)
		{
			var existeLibro = await context.Libros.AnyAsync(l => l.Id == libroId);
			if (!existeLibro) return NotFound();

			var registroBorrados = await context.Comentarios.Where(c => c.Id == id).ExecuteDeleteAsync();

			if (registroBorrados == 0)
			{
				return NotFound();
			}

			return NoContent();
		}
	}
}
