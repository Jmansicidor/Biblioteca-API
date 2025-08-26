using AutoMapper;
using BibliotecaApi.Datos;
using BibliotecaApi.DTOs;
using BibliotecaApi.Entitys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Controllers
{
	[ApiController]
	[Route("api/autores")]
	public class AutorController : ControllerBase
	{
		private readonly ApplicationDbContext context;
		
		public AutorController(ApplicationDbContext context)
		{
			this.context = context;
			

		}


		[HttpGet]
		public async Task<ActionResult<IEnumerable<AutorResponse>>> Get()
		{
			var response = await context.Autores
										.Select(a => a.ToDto())
										.ToListAsync();

			return Ok(response);
		}


		[HttpGet("{id:int}",Name = "ObtenerAutor")]
		public async Task<ActionResult<AutorResponse>> GetById(int id)
		{
			var autor = await context.Autores.FirstOrDefaultAsync(x => x.Id == id);

			if (autor is null)
				return NotFound();

			return Ok(autor.ToDto());

		}

		[HttpPost]
		public async Task<ActionResult> Post(Autor autor)
		{
			context.Add(autor);
			await context.SaveChangesAsync();
			return CreatedAtRoute("ObtenerAutor", new {id = autor.Id }, autor);

		}

		[HttpPut("{id:int}")]
		public async Task<ActionResult> Put(int id, AutorUpdate request)
		{
			var autor = await context.Autores.FirstOrDefaultAsync(a => a.Id == id);

			if (id != autor.Id)
				return BadRequest("Autor no encontrado para actualizar");

			autor.UpdateEntity(request);
			await context.SaveChangesAsync();
			return Ok();

		}

		[HttpDelete("{id:int}")]
		public async Task<ActionResult> Delete(int id)
		{
			var AutoresDelete = await context.Autores.Where(x => x.Id == id).ExecuteDeleteAsync();

			if (AutoresDelete == 0)
				return NotFound();

			return NoContent(); ;

		}

	}
}
