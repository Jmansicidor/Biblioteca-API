using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.Entitys
{
	public class Autor
	{
		public int Id { get; set; }

		//reglas de validacion
		[Required(ErrorMessage = "Campo { 0} requerido")]
		[StringLength(150, ErrorMessage = "Campo { 0} debe tener 15 caracteres")]
		public required string Name { get; set; }

		[Required(ErrorMessage = "Campo { 0} requerido")]
		[StringLength(150, ErrorMessage = "Campo { 0} debe tener 15 caracteres")]
		public required string SurName { get; set; }

		protected string? Autentificacion { get; set; }

		public List<Libro> Libros { get; set; } = new List<Libro>();
	}
}
