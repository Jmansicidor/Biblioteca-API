using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.Entitys
{
	public class Libro
	{
		public int Id { get; set; }
		[Required]
		public required string Titulo { get; set; }
		public string Description { get; set; }

		public int AutorId { get; set; }

		public Autor? Autor { get; set; }
	}
}
