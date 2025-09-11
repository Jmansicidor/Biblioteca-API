using BibliotecaApi.DTOs;
using BibliotecaApi.DTOs.BibliotecaAPI.DTOs;
using BibliotecaApi.Entitys;

using Mapster;

namespace BibliotecaAPI.Utilidades
{
	public static class MappingConfig
	{
		public static void RegisterMappings(TypeAdapterConfig config)
		{
			// Autor → AutorDTO
			config.NewConfig<Autor, AutorDTO>()
				 .Map(dest => dest.NombreCompleto,
					  src => $"{src.Name} {src.SurName}");

			// Autor → AutorConLibrosDTO
			config.NewConfig<Autor, AutorConLibrosDTO>()
				 .Map(dest => dest.NombreCompleto,
					  src => $"{src.Name} {src.SurName}");

			// AutorCreacionDTO → Autor
			config.NewConfig<AutorCreacionDTO, Autor>();

			// Autor <-> AutorPatchDTO (bidireccional)
			config.NewConfig<Autor, AutorPatchDTO>();
			config.NewConfig<AutorPatchDTO, Autor>();

			// AutorLibro → LibroDTO
			config.NewConfig<AutorLibro, LibroDTO>()
				 .Map(dest => dest.Id, src => src.LibroId)
				 .Map(dest => dest.Titulo, src => src.Libro!.Titulo);

			// Libro → LibroDTO
			config.NewConfig<Libro, LibroDTO>();

			// LibroCreacionDTO → Libro (incluye relación con Autores)
			config.NewConfig<LibroCreacionDTO, Libro>()
				 .Map(dest => dest.Autores,
					  src => src.AutoresIds.Select(id => new AutorLibro { AutorId = id }));

			// Libro → LibroConAutoresDTO
			config.NewConfig<Libro, LibroConAutoresDTO>();

			// AutorLibro → AutorDTO
			config.NewConfig<AutorLibro, AutorDTO>()
				 .Map(dest => dest.Id, src => src.AutorId)
				 .Map(dest => dest.NombreCompleto,
					  src => $"{src.Autor!.Name} {src.Autor!.SurName}");

			// LibroCreacionDTO → AutorLibro
			config.NewConfig<LibroCreacionDTO, AutorLibro>()
				 .Map(dest => dest.Libro,
					  src => new Libro { Titulo = src.Titulo });

			// ComentarioCreacionDTO → Comentario
			config.NewConfig<ComentarioCreacionDTO, Comentario>();

			// Comentario → ComentarioDTO
			config.NewConfig<Comentario, ComentarioDTO>()
				 .Map(dest => dest.UsuarioEmail, src => src.Usuario!.Email);

			// ComentarioPatchDTO <-> Comentario
			config.NewConfig<ComentarioPatchDTO, Comentario>();
			config.NewConfig<Comentario, ComentarioPatchDTO>();

			// Usuario → UsuarioDTO
			config.NewConfig<Usuario, UsuarioDTO>();
		}
	}
}
