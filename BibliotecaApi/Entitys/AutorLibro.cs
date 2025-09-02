using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Entitys
{
	[PrimaryKey(nameof(AutorId), nameof(LibroId))]
	public class AutorLibro
	{
		public int AutorId { get; set; }
		public int LibroId { get; set; }
		public Autor? Autor { get; set; }

		public Libro? Libro { get; set; }
	}
}
