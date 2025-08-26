using BibliotecaApi.Entitys;
using System.Runtime.CompilerServices;

namespace BibliotecaApi.DTOs
{
	public static class AutorDtoExtensions
	{
		public static AutorResponse ToDto(this Autor autor)
		{
			var nombreCompleto = $"{autor.Name} {autor.SurName}";
			return new AutorResponse(
				autor.Id,
				nombreCompleto
			);
		}
		
		public static void UpdateEntity(this Autor autor, AutorUpdate request)
		{
			autor.Name = request.name;
			autor.SurName = request.surname;
		}


	}
}
