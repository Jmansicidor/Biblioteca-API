namespace BibliotecaApi.DTOs
{
	public class RepuestaAutenticacionDTO
	{
		public required string Token { get; set; }
		public DateTime Expiracion { get; set; }
	}
}
