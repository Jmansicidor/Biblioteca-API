namespace BibliotecaApi.DTOs
{


	public sealed record AutorResponse(
		int Id,
		string NombreCompleto,
		string Autentifacion
		);

	public sealed record AutorCreate(
		string Name,
		string SurName,
		string Autentifacion
	);

	public sealed record AutorUpdate(
		string Name,
		string SurName,
		string Autentifacion
	);


	public sealed record AutorDelete(
		int Id,
		string Name,
		string SurName,
		string Autentifacion
		);
}
