using BibliotecaApi.Entitys;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BibliotecaApi.DTOs
{
	public static class AutorDtoExtensions
	{
		public static AutorResponse ToDto(this Autor autor)
		{
			var nombreCompleto = $"{autor.Name} {autor.SurName}";

			var libros = autor.Libros?.Select(l => new LibroResponse(
				l.Id,
				l.Titulo,
				l.Description
			)).ToList() ?? new List<LibroResponse>();

			return new AutorResponse(
				autor.Id,
				nombreCompleto,
				libros
			);
		}
		
		public static void UpdateEntity(this Autor autor, AutorUpdate request)
		{
			autor.Name = request.Name;
			autor.SurName = request.SurName;
		}


	}
}
