using BibliotecaApi.DTOs.BibliotecaAPI.DTOs;

namespace BibliotecaApi.DTOs
{
	public class AutorConLibrosDTO : AutorDTO
	{
		public List<LibroDTO> Libros { get; set; } = [];
	}
}
