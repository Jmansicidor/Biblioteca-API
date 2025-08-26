namespace BibliotecaApi.DTOs
{


	public sealed record AutorResponse(
		int id,
		string NombreCompleto
		);

	public sealed record AutorCreated(
	int id,
	string name,
	string surname
	);

	public sealed record AutorUpdate(
	int id,
	string name,
	string surname
	);


	public sealed record AutorDelete(
		int id,
		string name,
		string surname
		);
}
