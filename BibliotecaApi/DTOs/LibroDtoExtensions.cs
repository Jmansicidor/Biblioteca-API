using BibliotecaApi.Entitys;

namespace BibliotecaApi.DTOs
{
	public static class LibroDtoExtensions
	{
		public static LibroDTO ToDTO(this Libro libro) =>
			new LibroDTO(
				libro.Titulo,
				libro.Description,
				libro.AutorId
			);


		public static LibroDetailResponse ToDetailDto(this Libro libro)
		{
			var autorNombreCompleto = libro.Autor is not null
				? $"{libro.Autor.Name} {libro.Autor.SurName}"
				: string.Empty;

			return new LibroDetailResponse(
				libro.Id,
				libro.Titulo,
				libro.Description,
				libro.AutorId,
				autorNombreCompleto
			);
		}

		public static Libro ToEntity(this LibroCreate request)
		{
			return new Libro
			{
				Id = request.Id,
				Titulo = request.Titulo,
				Description = request.Description,
				AutorId = request.AutorId
			};
		}

		public static void UpdateEntity(this Libro libro, LibroUpdate request)
		{
			
			libro.Titulo = request.Titulo;
			libro.Description = request.Description;
			libro.AutorId = request.AutorId;
		}

	}
}
