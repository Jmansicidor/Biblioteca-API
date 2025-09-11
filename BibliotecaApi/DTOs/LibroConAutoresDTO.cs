using BibliotecaApi.DTOs.BibliotecaAPI.DTOs;

namespace BibliotecaApi.DTOs
{
	public class LibroConAutoresDTO : LibroDTO
	{
		public List<AutorDTO> Autores { get; set; } = [];
	}
}
