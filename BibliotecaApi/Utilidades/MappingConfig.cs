using BibliotecaApi.DTOs;
using BibliotecaApi.Entitys;
using Mapster;
using static BibliotecaApi.DTOs.ComentarioDTO;

namespace BibliotecaApi.Utilidades
{
	public static class MappingConfig
	{
		public static void RegisterMappings(TypeAdapterConfig config)
		{
			// Autor → AutorResponse
			config.NewConfig<Autor, AutorResponse>()
				.Map(dest => dest.NombreCompleto, src => src.Name + " " + src.SurName)
				.Map(dest => dest.Autentifacion, src => src.Autentificacion);
				

			// AutorCreate → Autor
			config.NewConfig<AutorCreate, Autor>();

			// Libro → LibroResponse
			config.NewConfig<Libro, LibroResponse>();

			//// Libro → LibroDetailResponse
			config.NewConfig<Libro, LibroDetailResponse>()
				.Map(dest => dest.Autores,
					 src => src.Autores != null
							? src.Autores.Select(al => new AutorResponse(
									al.Autor.Id,
									al.Autor.Name + " " + al.Autor.SurName,
									al.Autor.Autentificacion
							  )).ToList()
							: new List<AutorResponse>());

			// LibroDetailResponse → Libro

			config.NewConfig<LibroDetailResponse, Libro>()
				.Map(dest => dest.Autores,
					 src => src.Autores.Select(a => new AutorLibro
					 {
						 AutorId = a.Id,
						 LibroId = src.Id,
						 Autor = new Autor
						 {
							 Id = a.Id,
							 Name = string.Empty,      // requerido
							 SurName = string.Empty    // requerido
						 }
					 }).ToList())
				.AfterMapping((src, dest) =>
				{
					foreach (var autorLibro in dest.Autores)
					{
						var autorDto = src.Autores.First(a => a.Id == autorLibro.AutorId);
						if (!string.IsNullOrWhiteSpace(autorDto.NombreCompleto))
						{
							var partes = autorDto.NombreCompleto.Split(" ", 2, StringSplitOptions.RemoveEmptyEntries);
							autorLibro.Autor.Name = partes.Length > 0 ? partes[0] : string.Empty;
							autorLibro.Autor.SurName = partes.Length > 1 ? partes[1] : string.Empty;
						}
					}
				});




			// LibroCreate → Libro
			config.NewConfig<LibroCreate, Libro>();

			// LibroUpdate → Libro
			config.NewConfig<LibroUpdate, Libro>();


			// Autor → AutorPatchDTO
			config.NewConfig<Autor, AutorPatchDTO>();

			//AutorPatchDTO → Autor
			config.NewConfig<AutorPatchDTO, Autor>();


			config.NewConfig<ComentarioResponse, Comentario>();


			config.NewConfig<Comentario, ComentarioResponse>();
		}
	}

}