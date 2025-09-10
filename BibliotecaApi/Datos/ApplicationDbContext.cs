using BibliotecaApi.Entitys;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Datos
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)	: base(options) { }


		public DbSet<Autor> Autores { get; set; }
		public DbSet<Libro> Libros { get; set; }

		public DbSet<Comentario> Comentarios { get; set; }

		public DbSet <AutorLibro> AutorLibros { get; set; }
	}
}
