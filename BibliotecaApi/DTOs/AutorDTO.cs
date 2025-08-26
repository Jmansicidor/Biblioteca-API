namespace BibliotecaApi.DTOs
{


	public sealed record AutorResponse(
		int id,
		string NombreCompleto,
		List<LibroResponse> Libros
		);

	public sealed record AutorCreate(
		string Name,
		string SurName
	);

	public sealed record AutorUpdate(
		string Name,
		string SurName
	);


	public sealed record AutorDelete(
		int Id,
		string Name,
		string SurName
		);
}
