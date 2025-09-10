using Microsoft.AspNetCore.Identity;

namespace BibliotecaApi.Entitys
{
	public class Comentario
	{
		public Guid Id { get; set; }
		public required string Content { get; set; }
		public DateTime DatePost { get; set; }
		public int LibroId { get; set; }
		public Libro? Libro { get; set; }

		public required string UsuarioId { get; set; }

		public IdentityUser? Usuario { get; set; }

	}
}
