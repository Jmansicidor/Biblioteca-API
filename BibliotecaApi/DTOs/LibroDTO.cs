namespace BibliotecaApi.DTOs
{
	public sealed record LibroDTO(
		 string Titulo,
		 string Description,
		 int AutorId
		);

	public sealed record LibroResponse(
		int Id,
		string Titulo,
		string Description
	);

	public sealed record LibroCreate(
		int Id,
		string Titulo,
		string Description,
		int AutorId
	);

	public sealed record LibroUpdate(
		
		string Titulo,
		string Description,
		int AutorId
	);

	public sealed record LibroDetailResponse(
		int Id,
		string Titulo,
		string Description,
		int AutorId,
		string AutorNombreCompleto
	);


}
