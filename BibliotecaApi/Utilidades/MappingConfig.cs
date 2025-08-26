using BibliotecaApi.DTOs;
using BibliotecaApi.Entitys;
using Mapster;

namespace BibliotecaApi.Utilidades
{
	public static class MappingConfig
	{
		public static void RegisterMappings(TypeAdapterConfig config)
		{
			// Autor → AutorResponse
			config.NewConfig<Autor, AutorResponse>()
				.Map(dest => dest.NombreCompleto, src => src.Name + " " + src.SurName)
				.Map(dest => dest.Libros, src => src.Libros.Adapt<List<LibroResponse>>());

			// AutorCreate → Autor
			config.NewConfig<AutorCreate, Autor>();

			// Libro → LibroResponse
			config.NewConfig<Libro, LibroResponse>();

			// Libro → LibroDetailResponse
			config.NewConfig<Libro, LibroDetailResponse>()
				.Map(dest => dest.AutorNombreCompleto,
					 src => src.Autor != null ? src.Autor.Name + " " + src.Autor.SurName : string.Empty);

			// LibroCreate → Libro
			config.NewConfig<LibroCreate, Libro>();

			// LibroUpdate → Libro
			config.NewConfig<LibroUpdate, Libro>();
		}
	}

}