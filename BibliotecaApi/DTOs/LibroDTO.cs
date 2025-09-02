namespace BibliotecaApi.DTOs
{
	

	public sealed record LibroResponse(
		int Id,
		string Titulo,
		string Description
	);

	public sealed record LibroCreate(
		int Id,
		string Titulo,
		string Description,
		List<int> AutoresIds
	);

	public sealed record LibroUpdate(

		int Id,
		string Titulo,
		string Description,
		List<int> AutoresIds
	);

	public sealed record LibroDetailResponse(
		int Id,
		string Titulo,
		string? Description,
		List<AutorResponse> Autores
	);

}
