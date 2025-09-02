using BibliotecaApi.Entitys;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.DTOs
{
	public class AutorPatchDTO
	{	
		
		[Required(ErrorMessage = "Campo { 0} requerido")]
		[StringLength(150, ErrorMessage = "Campo { 0} debe tener 15 caracteres")]
		public required string Name { get; set; }

		[Required(ErrorMessage = "Campo { 0} requerido")]
		[StringLength(150, ErrorMessage = "Campo { 0} debe tener 15 caracteres")]
		public required string SurName { get; set; }
		[StringLength(20, ErrorMessage = "Campo { 0} debe tener {1} caracteres")]
		public string? Autentificacion { get; set; }

	
	}
}
