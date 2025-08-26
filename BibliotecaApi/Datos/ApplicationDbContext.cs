using BibliotecaApi.Entitys;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Datos
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)	: base(options) { }


		public DbSet<Autor> Autores { get; set; }
		public DbSet<Libro> Libros { get; set; }
	}
}
