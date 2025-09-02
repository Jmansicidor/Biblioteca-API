using BibliotecaApi.Entitys;

namespace BibliotecaApi.DTOs
{
	public class ComentarioPatchDTO
	{
		public Guid Id { get; set; }
		public required string Content { get; set; }
		public DateTime DatePost { get; set; }

	}
}
