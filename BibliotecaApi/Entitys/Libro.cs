using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.Entitys
{
	public class Libro
	{
		public int Id { get; set; }
		[Required]
		public required string Titulo { get; set; }
		public string Description { get; set; }

		public List<AutorLibro>? Autores { get; set; }

		public List<Comentario> Comentarios { get; set; }
	}
}
