namespace BibliotecaApi.DTOs
{
	public class ComentarioDTO
	{

		public sealed record ComentarioResponse(
			Guid Id,
			string Content,
			DateTime DatePost			
		);

		public sealed record ComentarioCreate(
			Guid Id,
			string Content,
			DateTime DatePost
		);

		public sealed record ComentarioUpdate(
			Guid Id,
			string Content,
			DateTime DatePost
		);

		



	}
}
